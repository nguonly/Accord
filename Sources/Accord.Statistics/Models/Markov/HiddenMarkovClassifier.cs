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
    using Accord.Statistics.Models.Markov.Topology;

    /// <summary>
    ///   Discrete-density Hidden Markov Model Set for Sequence Classification.
    /// </summary>
    /// 
    /// <remarks>
    ///   This class uses a set of hidden Markov models to classify integer sequences.
    ///   Each model will try to learn and recognize each of the different output classes.
    /// </remarks>
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
    ///   HiddenMarkovClassifier classifier = new HiddenMarkovClassifier(classes, states, symbols);
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
    [Serializable]
    public class HiddenMarkovClassifier : BaseHiddenMarkovClassifier<HiddenMarkovModel>, IHiddenMarkovClassifier
    {

        /// <summary>
        ///   Gets the number of symbols
        ///   recognizable by the models.
        /// </summary>
        /// 
        public int Symbols
        {
            get { return this[0].Symbols; }
        }


        #region Constructors

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology topology, int symbols, string[] names)
            : base(classes)
        {
            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel(topology, symbols) { Tag = names[i] };
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology topology, int symbols)
            : base(classes)
        {
            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel(topology, symbols);
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology[] topology, int symbols)
            : base(classes)
        {
            if (topology.Length != classes)
                throw new ArgumentException("The number of topology especifications should equal the number of classes", "classes");

            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel(topology[i], symbols);
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, ITopology[] topology, int symbols, string[] names)
            : base(classes)
        {
            if (topology.Length != classes)
                throw new ArgumentException("The number of topology especifications should equal the number of classes", "classes");

            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel(topology[i], symbols) { Tag = names[i] };
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, int[] states, int symbols, string[] names)
            : base(classes)
        {
            if (states.Length != classes)
                throw new ArgumentException("The number of state specifications should equal the number of classes.", "classes");

            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel(new Ergodic(states[i]), symbols) { Tag = names[i] };
        }

        /// <summary>
        ///   Creates a new Sequence Classifier with the given number of classes.
        /// </summary>
        /// 
        public HiddenMarkovClassifier(int classes, int[] states, int symbols)
            : base(classes)
        {
            if (states.Length != classes)
                throw new ArgumentException("The number of state specifications should equal the number of classes.", "classes");

            for (int i = 0; i < classes; i++)
                Models[i] = new HiddenMarkovModel(new Ergodic(states[i]), symbols);
        }
        #endregion


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
        public int Compute(int[] sequence)
        {
            return base.Compute(sequence as Array);
        }

        /// <summary>
        ///   Computes the most likely class for a given sequence.
        /// </summary>
        /// 
        /// <param name="sequence">The sequence of observations.</param>
        /// <param name="response">The class responsibilities (or
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
        public int Compute(int[] sequence, out double response)
        {
            return base.Compute(sequence as Array, out response);
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
        public int Compute(int[] sequence, out double[] responsibilities)
        {
            return base.Compute(sequence as Array, out responsibilities);
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
        public double LogLikelihood(int[] sequence, int output)
        {
            return base.LogLikelihood(sequence, output);
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
        public double LogLikelihood(int[][] sequences, int[] outputs)
        {
            return base.LogLikelihood(sequences, outputs);
        }

        #region ISequenceClassifier implementation

        /// <summary>
        ///   Computes the most likely class for a given sequence.
        /// </summary>
        /// 
        int IHiddenMarkovClassifier.Compute(Array sequence, out double[] likelihoods)
        {
            return base.Compute(sequence, out likelihoods);
        }
        #endregion


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
        public static HiddenMarkovClassifier Load(Stream stream)
        {
            BinaryFormatter b = new BinaryFormatter();
            return (HiddenMarkovClassifier)b.Deserialize(stream);
        }

        /// <summary>
        ///   Loads a classifier from a file.
        /// </summary>
        /// 
        /// <param name="path">The path to the file from which the classifier is to be deserialized.</param>
        /// 
        /// <returns>The deserialized classifier.</returns>
        /// 
        public static HiddenMarkovClassifier Load(string path)
        {
            return Load(new FileStream(path, FileMode.Open));
        }

    }
}
