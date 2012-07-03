// Accord Statistics Library
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009-2012
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Statistics.Models.Markov.Learning
{
    using System.Threading;
    using System.Threading.Tasks;
    using Accord.Math;

    /// <summary>
    ///   Discrete-density hidden Markov Sequence Classifier learning algorithm.
    /// </summary>
    /// 
    /// <example>
    ///   <code>
    ///   // Declare some testing data
    ///   int[][] inputs = new int[][]
    ///   {
    ///       new int[] { 0,1,1,0 },   // Class 0
    ///       new int[] { 0,0,1,0 },   // Class 0
    ///       new int[] { 0,1,1,1,0 }, // Class 0
    ///       new int[] { 0,1,0 },     // Class 0
    ///   
    ///       new int[] { 1,0,0,1 },   // Class 1
    ///       new int[] { 1,1,0,1 },   // Class 1
    ///       new int[] { 1,0,0,0,1 }, // Class 1
    ///       new int[] { 1,0,1 },     // Class 1
    ///   };
    ///   
    ///   int[] outputs = new int[]
    ///   {
    ///       0,0,0,0, // First four sequences are of class 0
    ///       1,1,1,1, // Last four sequences are of class 1
    ///   };
    ///   
    ///   
    ///   // We are trying to predict two different classes
    ///   int classes = 2;
    ///
    ///   // Each sequence may have up to two symbols (0 or 1)
    ///   int symbols = 2;
    ///
    ///   // Nested models will have two states each
    ///   int[] states = new int[] { 2, 2 };
    ///
    ///   // Creates a new Hidden Markov Model Sequence Classifier with the given parameters
    ///   SequenceClassifier classifier = new HiddenMarkovClassifier(classes, states, symbols);
    ///   
    ///   // Create a new learning algorithm to train the sequence classifier
    ///   var teacher = new HiddenMarkovClassifierLearning(classifier,
    ///   
    ///       // Train each model until the log-likelihood changes less than 0.001
    ///       modelIndex => new BaumWelchLearning(classifier.Models[modelIndex])
    ///       {
    ///           Tolerance = 0.001,
    ///           Iterations = 0
    ///       }
    ///   );
    ///   
    ///   // Train the sequence classifier using the algorithm
    ///   double likelihood = teacher.Run(inputs, outputs);
    ///   
    ///   </code>
    /// </example>
    /// 
    public class HiddenMarkovClassifierLearning :
        BaseHiddenMarkovClassifierLearning<HiddenMarkovClassifier, HiddenMarkovModel>
    {

        private int smoothingKernelSize = 3;
        private double smoothingSigma = 1.0;
        private double[] gaussianKernel;

        /// <summary>
        ///   Gets or sets the smoothing kernel's sigma
        ///   for the threshold model.
        /// </summary>
        /// <value>The smoothing kernel's sigma.</value>
        public double Smoothing
        {
            get { return smoothingSigma; }
            set
            {
                smoothingSigma = value;
                createSmoothingKernel();
            }
        }

        /// <summary>
        ///   Creates a new instance of the learning algorithm for a given 
        ///   Markov sequence classifier using the specified configuration
        ///   function.
        /// </summary>
        public HiddenMarkovClassifierLearning(HiddenMarkovClassifier classifier,
            ClassifierLearningAlgorithmConfiguration algorithm)
            : base(classifier, algorithm)
        {
            createSmoothingKernel();
        }


        /// <summary>
        ///   Trains each model to recognize each of the output labels.
        /// </summary>
        /// <returns>The sum log-likelihood for all models after training.</returns>
        public double Run(int[][] inputs, int[] outputs)
        {
            return base.Run<int[]>(inputs, outputs);
        }


        /// <summary>
        ///   Compute model error for a given data set.
        /// </summary>
        /// 
        /// <param name="inputs">The input points.</param>
        /// <param name="outputs">The output points.</param>
        /// 
        /// <returns>The percent of misclassification errors for the data.</returns>
        /// 
        public double ComputeError(int[][] inputs, int[] outputs)
        {
            int errors = 0;
            Parallel.For(0, inputs.Length, i =>
            {
                int expectedOutput = outputs[i];
                int actualOutput = Classifier.Compute(inputs[i]);

                if (expectedOutput != actualOutput)
                    Interlocked.Increment(ref errors);
            });

            return errors / (double)inputs.Length;
        }

        /// <summary>
        ///   Creates a new <see cref="Threshold">threshold model</see>
        ///   for the current set of Markov models in this sequence classifier.
        /// </summary>
        /// <returns>
        ///   A <see cref="Threshold">threshold Markov model</see>.
        /// </returns>
        public override HiddenMarkovModel Threshold()
        {
            HiddenMarkovModel[] Models = base.Classifier.Models;

            int states = 0;
            int symbols = Models[0].Symbols;

            // Get the total number of states
            for (int i = 0; i < Models.Length; i++)
                states += Models[i].States;

            // Create the transition and emission matrices
            double[,] transition = new double[states, states];
            double[,] emissions = new double[states, symbols];
            double[] initial = new double[states];

            for (int i = 0, m = 0; i < Models.Length; i++)
            {
                var A = Matrix.Exp(Models[i].Transitions);
                var B = Matrix.Exp(Models[i].Emissions);

                for (int j = 0; j < Models[i].States; j++)
                {
                    for (int k = 0; k < Models[i].States; k++)
                    {
                        if (j != k)
                            transition[j + m, k + m] = (1.0 - A[j, k]) / (states - 1.0);
                        else transition[j + m, k + m] = A[j, k];
                    }
                    emissions.SetRow(j + m, B.GetRow(j));
                }

                initial[m] = 1.0 / Models.Length;
                m += Models[i].States;
            }


            if (smoothingSigma > 0)
            {
                // Gaussian smoothing
                for (int i = 0; i < states; i++)
                {
                    double[] e = emissions.GetRow(i);
                    double[] g = e.Convolve(gaussianKernel, true);
                    g = g.Divide(g.Sum()); // Make probabilities
                    emissions.SetRow(i, g);
                }
            }

            return new HiddenMarkovModel(transition, emissions, initial) { Tag = "Threshold" };
        }


        private void createSmoothingKernel()
        {
            AForge.Math.Gaussian g = new AForge.Math.Gaussian(smoothingSigma);
            gaussianKernel = g.Kernel(smoothingKernelSize);

            // Normalize
            double norm = gaussianKernel.Euclidean();
            gaussianKernel = gaussianKernel.Divide(norm);
        }
    }
}
