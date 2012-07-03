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

namespace Accord.Statistics.Models.Markov
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Accord.Statistics.Distributions;
    using Accord.Statistics.Models.Markov.Topology;

    /// <summary>
    ///   Arbitrary-density Hidden Markov Model Set for Sequence Classification.
    /// </summary>
    /// 
    /// <remarks>
    ///   This class uses a set of hidden Markov models to classify sequences of
    ///   real (double-precision floating point) numbers or arrays of those numbers.
    ///   Each model will try to learn and recognize each of the different output classes.
    /// </remarks>
    /// 
    /// <example>
    ///   <para>
    ///   The following example creates a continuous-density hidden Markov model sequence
    ///   classifier to recognize two classes of univariate sequence of observations.</para>
    ///   
    ///   <code>
    ///   // Create a Continuous density Hidden Markov Model Sequence Classifier
    ///   // to detect a univariate sequence and the same sequence backwards.
    ///   double[][] sequences = new double[][] 
    ///   {
    ///       new double[] { 0,1,2,3,4 }, // This is the first  sequence with label = 0
    ///       new double[] { 4,3,2,1,0 }, // This is the second sequence with label = 1
    ///   };
    ///   
    ///   // Labels for the sequences
    ///   int[] labels = { 0, 1 };
    ///
    ///   // Creates a new Continuous-density Hidden Markov Model Sequence Classifier
    ///   //  containing 2 hidden Markov Models with 2 states and an underlying Normal
    ///   //  distribution as the continuous probability density.
    ///   NormalDistribution density = new NormalDistribution();
    ///   var classifier = new HiddenMarkovClassifier&lt;NormalDistribution&gt;(2, new Ergodic(2), density);
    ///
    ///   // Create a new learning algorithm to train the sequence classifier
    ///   var teacher = new HiddenMarkovClassifierLearning&lt;NormalDistribution&gt;(classifier,
    ///
    ///       // Train each model until the log-likelihood changes less than 0.001
    ///       modelIndex => new BaumWelchLearning&lt;NormalDistribution&gt;(classifier.Models[modelIndex])
    ///       {
    ///           Tolerance = 0.0001,
    ///           Iterations = 0
    ///       }
    ///   );
    ///   
    ///   // Train the sequence classifier using the algorithm
    ///   teacher.Run(sequences, labels);
    ///   
    ///   
    ///   // Calculate the probability that the given
    ///   //  sequences originated from the model
    ///   double likelihood;
    ///   
    ///   // Try to classify the first sequence (output should be 0)
    ///   int c1 = classifier.Compute(sequences[0], out likelihood);
    ///   
    ///   // Try to classify the second sequence (output should be 1)
    ///   int c2 = classifier.Compute(sequences[1], out likelihood);
    ///   </code>
    ///   
    ///   <para>
    ///   The following example creates a continuous-density hidden Markov model sequence
    ///   classifier to recognize two classes of multivariate sequence of observations.</para>
    ///   
    ///   <code>
    ///   // Create a Continuous density Hidden Markov Model Sequence Classifier
    ///   // to detect a multivariate sequence and the same sequence backwards.
    ///   double[][][] sequences = new double[][][]
    ///   {
    ///       new double[][] 
    ///       { 
    ///           // This is the first  sequence with label = 0
    ///           new double[] { 0 },
    ///           new double[] { 1 },
    ///           new double[] { 2 },
    ///           new double[] { 3 },
    ///           new double[] { 4 },
    ///       }, 
    ///       
    ///       new double[][]
    ///       {
    ///           // This is the second sequence with label = 1
    ///           new double[] { 4 },
    ///           new double[] { 3 },
    ///           new double[] { 2 },
    ///           new double[] { 1 },
    ///           new double[] { 0 },
    ///       }
    ///   };
    ///   
    ///   // Labels for the sequences
    ///   int[] labels = { 0, 1 };
    ///   
    ///   // Creates a sequence classifier containing 2 hidden Markov Models
    ///   //  with 2 states and an underlying Normal distribution as density.
    ///   MultivariateNormalDistribution density = new MultivariateNormalDistribution(1);
    ///   var classifier = new SequenceClassifier&lt;MultivariateNormalDistribution&gt;(2, new Ergodic(2), density);
    ///   
    ///   // Configure the learning algorithms to train the sequence classifier
    ///   var teacher = new SequenceClassifierLearning&lt;NormalDistribution&gt;(classifier,
    ///
    ///      // Train each model until the log-likelihood changes less than 0.001
    ///      modelIndex => new BaumWelchLearning&lt;NormalDistribution&gt;(classifier.Models[modelIndex])
    ///      {
    ///           Tolerance = 0.0001,
    ///           Iterations = 0
    ///      {
    ///   );
    ///   
    ///   // Train the sequence classifier using the algorithm
    ///   double logLikelihood = teacher.Run(sequences, labels);
    ///   
    ///    
    ///   // Calculate the probability that the given
    ///   //  sequences originated from the model
    ///   double likelihood1, likelihood2;
    ///   
    ///   // Try to classify the first sequence (output should be 0)
    ///   int c1 = classifier.Compute(sequences[0], out likelihood1);
    ///
    ///   // Try to classify the second sequence (output should be 1)
    ///   int c2 = classifier.Compute(sequences[1], out likelihood2);
    ///   </code>
    /// </example>
    /// 
    [Serializable]
    public class HiddenMarkovClassifier<TDistribution> :
        BaseHiddenMarkovClassifier<HiddenMarkovModel<TDistribution>>,
        IHiddenMarkovClassifier where TDistribution : IDistribution
    {

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology topology, TDistribution initial)
            : base(classes)
        {
            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel<TDistribution>(topology, initial);
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology topology, TDistribution initial, string[] names)
            : base(classes)
        {
            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel<TDistribution>(topology, initial) { Tag = names[i] };
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology topology, TDistribution[] initial, string[] names)
            : base(classes)
        {
            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel<TDistribution>(topology, initial[i]) { Tag = names[i] };
        }


        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology[] topology, TDistribution[] initial, string[] names)
            : base(classes)
        {
            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel<TDistribution>(topology[i], initial[i]) { Tag = names[i] };
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(HiddenMarkovModel<TDistribution>[] models)
            : base(models) { }


        /// <summary>
        ///   Computes the most likely class for a given sequence.
        /// </summary>
        /// 
        /// <param name="sequence">The sequence of observations.</param>
        /// 
        /// <returns>Return the label of the given sequence, or -1 if it has
        /// been rejected by the <see cref="BaseHiddenMarkovClassifier{T}.Threshold">
        /// threshold model</see>.</returns>
        /// 
        public new int Compute(Array sequence)
        {
            return base.Compute(sequence);
        }

        /// <summary>
        ///   Computes the most likely class for a given sequence.
        /// </summary>
        /// 
        /// <param name="sequence">The sequence of observations.</param>
        /// <param name="response">The probability of the assigned class.</param>
        /// 
        /// <returns>Return the label of the given sequence, or -1 if it has
        /// been rejected by the <see cref="BaseHiddenMarkovClassifier{T}.Threshold">
        /// threshold model</see>.</returns>
        /// 
        public new int Compute(Array sequence, out double response)
        {
            return base.Compute(sequence, out response);
        }

        /// <summary>
        ///   Computes the most likely class for a given sequence.
        /// </summary>
        /// 
        /// <param name="sequence">The sequence of observations.</param>
        /// <param name="responsibilities">The class responsibilities (or
        /// the probability of the sequence to belong to each class). When
        /// using threshold models, the sum of the probabilities will not
        /// equal one, and the amount left was the threshold probability.
        /// If a threshold model is not being used, the array should sum to
        /// one.</param>
        /// 
        /// <returns>Return the label of the given sequence, or -1 if it has
        /// been rejected by the <see cref="BaseHiddenMarkovClassifier{T}.Threshold">
        /// threshold model</see>.</returns>
        /// 
        public new int Compute(Array sequence, out double[] responsibilities)
        {
            return base.Compute(sequence, out responsibilities);
        }

        /// <summary>
        ///   Computes the log-likelihood of a sequence
        ///   belong to a given class according to this
        ///   classifier.
        /// </summary>
        /// <param name="sequence">The sequence of observations.</param>
        /// <param name="output">The output class label.</param>
        /// 
        /// <returns>The log-likelihood of the sequence belonging to the given class.</returns>
        /// 
        public new double LogLikelihood(Array sequence, int output)
        {
            double[] responsabilities;
            base.Compute(sequence, out responsabilities);
            return Math.Log(responsabilities[output]);
        }

        /// <summary>
        ///   Computes the log-likelihood of a set of sequences
        ///   belonging to their given respective classes according
        ///   to this classifier.
        /// </summary>
        /// <param name="sequences">A set of sequences of observations.</param>
        /// <param name="outputs">The output class label for each sequence.</param>
        /// 
        /// <returns>The log-likelihood of the sequences belonging to the given classes.</returns>
        /// 
        public new double LogLikelihood(Array[] sequences, int[] outputs)
        {
            double[] responsabilities;

            double logLikelihood = 0;
            for (int i = 0; i < sequences.Length; i++)
            {
                base.Compute(sequences[i], out responsabilities);
                logLikelihood += Math.Log(responsabilities[outputs[i]]);
            }
            return logLikelihood;
        }


        /// <summary>
        ///   Saves the classifier to a stream.
        /// </summary>
        /// 
        /// <param name="stream">The stream to which the classifier is to be serialized.</param>
        /// 
        public void Save(Stream stream)
        {
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(stream, this);
        }

        /// <summary>
        ///   Saves the classifier to a stream.
        /// </summary>
        /// 
        /// <param name="path">The stream to which the classifier is to be serialized.</param>
        /// 
        public void Save(string path)
        {
            Save(new FileStream(path, FileMode.Create));
        }

        /// <summary>
        ///   Loads a classifier from a stream.
        /// </summary>
        /// 
        /// <param name="stream">The stream from which the classifier is to be deserialized.</param>
        /// 
        /// <returns>The deserialized classifier.</returns>
        /// 
        public static HiddenMarkovClassifier<TDistribution> Load(Stream stream)
        {
            BinaryFormatter b = new BinaryFormatter();
            return (HiddenMarkovClassifier<TDistribution>)b.Deserialize(stream);
        }

        /// <summary>
        ///   Loads a classifier from a file.
        /// </summary>
        /// 
        /// <param name="path">The path to the file from which the classifier is to be deserialized.</param>
        /// 
        /// <returns>The deserialized classifier.</returns>
        /// 
        public static HiddenMarkovClassifier<TDistribution> Load(string path)
        {
            return Load(new FileStream(path, FileMode.Open));
        }

    }
}
