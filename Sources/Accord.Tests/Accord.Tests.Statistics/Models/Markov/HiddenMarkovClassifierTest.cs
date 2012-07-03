// Accord Unit Tests
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

namespace Accord.Tests.Statistics
{
    using Accord.Statistics.Models.Markov;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Accord.Statistics.Models.Markov.Learning;
    using Accord.Math;
    using System;

    /// <summary>
    ///This is a test class for HiddenMarkovClassifierTest and is intended
    ///to contain all HiddenMarkovClassifierTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HiddenMarkovClassifierTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Learn
        ///</summary>
        [TestMethod()]
        public void LearnTest()
        {
            // Declare some testing data
            int[][] inputs = new int[][]
            {
                new int[] { 0,1,1,0 },   // Class 0
                new int[] { 0,0,1,0 },   // Class 0
                new int[] { 0,1,1,1,0 }, // Class 0
                new int[] { 0,1,0 },     // Class 0

                new int[] { 1,0,0,1 },   // Class 1
                new int[] { 1,1,0,1 },   // Class 1
                new int[] { 1,0,0,0,1 }, // Class 1
                new int[] { 1,0,1 },     // Class 1
            };

            int[] outputs = new int[]
            {
                0,0,0,0, // First four sequences are of class 0
                1,1,1,1, // Last four sequences are of class 1
            };


            // We are trying to predict two different classes
            int classes = 2;

            // Each sequence may have up to two symbols (0 or 1)
            int symbols = 2;

            // Nested models will have two states each
            int[] states = new int[] { 2, 2 };

            // Creates a new Hidden Markov Model Classifier with the given parameters
            HiddenMarkovClassifier classifier = new HiddenMarkovClassifier(classes, states, symbols);


            // Create a new learning algorithm to train the sequence classifier
            var teacher = new HiddenMarkovClassifierLearning(classifier,

                // Train each model until the log-likelihood changes less than 0.001
                modelIndex => new BaumWelchLearning(classifier.Models[modelIndex])
                {
                    Tolerance = 0.001,
                    Iterations = 0
                }
            );

            // Train the sequence classifier using the algorithm
            double likelihood = teacher.Run(inputs, outputs);


            // Will assert the models have learned the sequences correctly.
            for (int i = 0; i < inputs.Length; i++)
            {
                int expected = outputs[i];
                int actual = classifier.Compute(inputs[i], out likelihood);
                Assert.AreEqual(expected, actual);
            }
        }


        /// <summary>
        ///A test for Learn
        ///</summary>
        [TestMethod()]
        public void LearnTest2()
        {
            // Declare some testing data
            int[][] inputs = new int[][]
            {
                new int[] { 0,0,1,2 },     // Class 0
                new int[] { 0,1,1,2 },     // Class 0
                new int[] { 0,0,0,1,2 }, // Class 0
                new int[] { 0,1,2,2,2 },   // Class 0

                new int[] { 2,2,1,0 },     // Class 1
                new int[] { 2,2,2,1,0 },   // Class 1
                new int[] { 2,2,2,1,0 },   // Class 1
                new int[] { 2,2,2,2,1 },   // Class 1
            };

            int[] outputs = new int[]
            {
                0,0,0,0, // First four sequences are of class 0
                1,1,1,1, // Last four sequences are of class 1
            };


            // We are trying to predict two different classes
            int classes = 2;

            // Each sequence may have up to 3 symbols (0,1,2)
            int symbols = 3;

            // Nested models will have 3 states each
            int[] states = new int[] { 3, 3 };

            // Creates a new Hidden Markov Model Classifier with the given parameters
            HiddenMarkovClassifier classifier = new HiddenMarkovClassifier(classes, states, symbols);


            // Create a new learning algorithm to train the sequence classifier
            var teacher = new HiddenMarkovClassifierLearning(classifier,

                // Train each model until the log-likelihood changes less than 0.001
                modelIndex => new BaumWelchLearning(classifier.Models[modelIndex])
                {
                    Tolerance = 0.001,
                    Iterations = 0
                }
            );

            // Enable support for sequence rejection
            teacher.Rejection = true;

            // Train the sequence classifier using the algorithm
            double likelihood = teacher.Run(inputs, outputs);


            // Will assert the models have learned the sequences correctly.
            for (int i = 0; i < inputs.Length; i++)
            {
                int expected = outputs[i];
                int actual = classifier.Compute(inputs[i], out likelihood);
                Assert.AreEqual(expected, actual);
            }

            HiddenMarkovModel threshold = classifier.Threshold;

            Assert.AreEqual(6, threshold.States);

            Assert.AreEqual(classifier.Models[0].Transitions[0, 0], threshold.Transitions[0, 0], 1e-10);
            Assert.AreEqual(classifier.Models[0].Transitions[1, 1], threshold.Transitions[1, 1], 1e-10);
            Assert.AreEqual(classifier.Models[0].Transitions[2, 2], threshold.Transitions[2, 2], 1e-10);

            Assert.AreEqual(classifier.Models[1].Transitions[0, 0], threshold.Transitions[3, 3], 1e-10);
            Assert.AreEqual(classifier.Models[1].Transitions[1, 1], threshold.Transitions[4, 4], 1e-10);
            Assert.AreEqual(classifier.Models[1].Transitions[2, 2], threshold.Transitions[5, 5], 1e-10);

            Assert.IsFalse(Matrix.HasNaN(threshold.Transitions));

            int[] r0 = new int[] { 1, 1, 0, 0, 2 };


            double logRejection;
            int c = classifier.Compute(r0, out logRejection);

            Assert.AreEqual(-1, c);
            Assert.AreEqual(0.99569011079012049, logRejection);
            Assert.IsFalse(double.IsNaN(logRejection));

            logRejection = threshold.Evaluate(r0);
            Assert.AreEqual(-6.7949285513628528, logRejection, 1e-10);
            Assert.IsFalse(double.IsNaN(logRejection));

            threshold.Decode(r0, out logRejection);
            Assert.AreEqual(-8.902077561009957, logRejection, 1e-10);
            Assert.IsFalse(double.IsNaN(logRejection));
        }
    }
}
