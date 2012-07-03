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
    using System;
    using Accord.Math;
    using System.Threading.Tasks;

    /// <summary>
    ///   Configuration function delegate for Sequence Classifier Learning algorithms.
    /// </summary>
    public delegate IUnsupervisedLearning ClassifierLearningAlgorithmConfiguration(int modelIndex);

    /// <summary>
    ///   Submodel learning event arguments.
    /// </summary>
    public class GenerativeLearningEventArgs : EventArgs
    {
        /// <summary>
        ///   Gets the generative class model to 
        ///   which this event refers to.
        /// </summary>
        public int Class { get; set; }

        /// <summary>
        ///   Gets the total number of models
        ///   to be learned.
        /// </summary>
        /// 
        public int Total { get; set; }


        /// <summary>
        ///   Initializes a new instance of the <see cref="GenerativeLearningEventArgs"/> class.
        /// </summary>
        /// 
        /// <param name="classLabel">The class label.</param>
        /// <param name="classes">The total number of classes.</param>
        /// 
        public GenerativeLearningEventArgs(int classLabel, int classes)
        {
            this.Class = classLabel;
            this.Total = classes;
        }

    }

    /// <summary>
    ///   Abstract base class for Sequence Classifier learning algorithms.
    /// </summary>
    /// 
    public abstract class BaseHiddenMarkovClassifierLearning<TClassifier, TModel>
        where TClassifier : BaseHiddenMarkovClassifier<TModel>
        where TModel : IHiddenMarkovModel
    {


        /// <summary>
        ///   Gets the classifier being trained by this instance.
        /// </summary>
        /// <value>The classifier being trained by this instance.</value>
        /// 
        public TClassifier Classifier { get; private set; }

        /// <summary>
        ///   Gets or sets the configuration function specifying which
        ///   training algorithm should be used for each of the models
        ///   in the hidden Markov model set.
        /// </summary>
        /// 
        public ClassifierLearningAlgorithmConfiguration Algorithm { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether a threshold model
        ///   should be created or updated after training to support rejection.
        /// </summary>
        /// <value><c>true</c> to update the threshold model after training;
        /// otherwise, <c>false</c>.</value>
        /// 
        public bool Rejection { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether the class priors
        ///   should be estimated from the data, as in an empirical bayes method.
        /// </summary>
        /// 
        public bool Empirical { get; set; }

        /// <summary>
        ///   Occurs when the learning of a class model has started.
        /// </summary>
        /// 
        public event EventHandler<GenerativeLearningEventArgs> ClassModelLearningStarted;

        /// <summary>
        ///   Occurs when the learning of a class model has finished.
        /// </summary>
        /// 
        public event EventHandler<GenerativeLearningEventArgs> ClassModelLearningFinished;

        /// <summary>
        ///   Creates a new instance of the learning algorithm for a given 
        ///   Markov sequence classifier using the specified configuration
        ///   function.
        /// </summary>
        /// 
        protected BaseHiddenMarkovClassifierLearning(TClassifier classifier,
            ClassifierLearningAlgorithmConfiguration algorithm)
        {
            this.Classifier = classifier;
            this.Algorithm = algorithm;
        }



        /// <summary>
        ///   Trains each model to recognize each of the output labels.
        /// </summary>
        /// <returns>The sum log-likelihood for all models after training.</returns>
        /// 
        protected double Run<T>(T[] inputs, int[] outputs)
        {
            if (inputs == null) throw new ArgumentNullException("inputs");
            if (outputs == null) throw new ArgumentNullException("outputs");

            if (inputs.Length != outputs.Length)
                throw new DimensionMismatchException("outputs", 
                    "The number of inputs and outputs does not match.");

            for (int i = 0; i < outputs.Length; i++)
                if (outputs[i] < 0 || outputs[i] >= Classifier.Classes)
                    throw new ArgumentOutOfRangeException("outputs");


            int classes = Classifier.Classes;
            double[] logLikelihood = new double[classes];
            int[] classCounts = new int[classes];


            // For each model,
#if !DEBUG
            Parallel.For(0, classes, i =>
#else
            for (int i = 0; i < classes; i++)
#endif
            {
                // We will start the class model learning problem
                var args = new GenerativeLearningEventArgs(i, classes);
                OnGenerativeClassModelLearningStarted(args);

                // Select the input/output set corresponding
                //  to the model's specialization class
                int[] inx = outputs.Find(y => y == i);
                T[] observations = inputs.Submatrix(inx);

                classCounts[i] = observations.Length;

                if (observations.Length > 0)
                {
                    // Create and configure the learning algorithm
                    IUnsupervisedLearning teacher = Algorithm(i);

                    // Train the current model in the input/output subset
                    logLikelihood[i] = teacher.Run(observations as Array[]);
                }

                // Update and report progress
                OnGenerativeClassModelLearningFinished(args);
            }
#if !DEBUG
            );
#endif

            if (Empirical)
            {
                for (int i = 0; i < classes; i++)
                    Classifier.Priors[i] = (double)classCounts[i] / inputs.Length;
            }

            if (Rejection)
            {
                Classifier.Threshold = Threshold();
            }

            // Returns the sum log-likelihood for all models.
            return logLikelihood.Sum();
        }


        /// <summary>
        ///   Creates a new <see cref="Threshold">threshold model</see>
        ///   for the current set of Markov models in this sequence classifier.
        /// </summary>
        /// <returns>A <see cref="Threshold">threshold Markov model</see>.</returns>
        /// 
        public abstract TModel Threshold();

        /// <summary>
        ///   Raises the <see cref="E:GenerativeClassModelLearningFinished"/> event.
        /// </summary>
        /// 
        /// <param name="args">The <see cref="Accord.Statistics.Models.Markov.Learning.GenerativeLearningEventArgs"/> instance containing the event data.</param>
        /// 
        protected void OnGenerativeClassModelLearningFinished(GenerativeLearningEventArgs args)
        {
            if (ClassModelLearningFinished != null)
                ClassModelLearningFinished(this, args);
        }

        /// <summary>
        ///   Raises the <see cref="E:GenerativeClassModelLearningStarted"/> event.
        /// </summary>
        /// 
        /// <param name="args">The <see cref="Accord.Statistics.Models.Markov.Learning.GenerativeLearningEventArgs"/> instance containing the event data.</param>
        /// 
        protected void OnGenerativeClassModelLearningStarted(GenerativeLearningEventArgs args)
        {
            if (ClassModelLearningStarted != null)
                ClassModelLearningStarted(this, args);
        }
    }
}
