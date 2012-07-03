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
    using Accord.Statistics.Analysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Accord.Math;

    [TestClass()]
    public class LogisticRegressionAnalysisTest
    {


        private TestContext testContextInstance;

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


        [TestMethod()]
        public void ComputeTest1()
        {

            double[][] inputs = training.Submatrix(null, 0, 3);
            double[] outputs = training.GetColumn(4);

            LogisticRegressionAnalysis regression = new LogisticRegressionAnalysis(inputs, outputs);

            bool converged = regression.Compute();
            Assert.IsTrue(converged);

            double[] actual = regression.Result;

            double[] expected = 
            {
                0.000012, 0.892611, 0.991369, 0.001513, 0.904055,
                0.001446, 0.998673, 0.001260, 0.629312, 0.004475,
                0.505362, 0.999791, 0.000050, 1.000000, 0.990362,
                0.985265, 1.000000, 1.000000, 0.001319, 0.000001,
                0.000001, 0.000050, 0.702488, 0.003049, 0.000046,
                0.000419, 0.026276, 0.036813, 0.000713, 0.001484,
                0.000008, 0.000009, 0.278950, 0.001402, 0.025764,
                0.002464, 0.000219, 0.007328, 0.000106, 0.002619,
                0.002913, 0.000002,
            };

            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i], 1e-6);
        }

        [TestMethod()]
        public void ComputeTest2()
        {
            // Test instance 01
            double[][] trainInput =
            {
               new double[] { 1, 1 },
               new double[] { 0, 0 },
            };

            double[] trainOutput = { 1, 0 };
            double[] testInput = { 0, 0.2 };

            LogisticRegressionAnalysis target = new LogisticRegressionAnalysis(trainInput, trainOutput);

            target.Compute();

            foreach (var coefficient in target.Coefficients)
                Assert.IsFalse(double.IsNaN(coefficient.Value));

            Assert.AreEqual(0, target.Regression.Compute(testInput));

            // Test instance 02
            trainInput = new double[][]
            {
                new double[] { 1, 0, 1, 1, 0, 1, 1, 0, 1, 0 },
                new double[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 1 },
                new double[] { 1, 1, 0, 0, 1, 1, 0, 1, 1, 1 },
                new double[] { 1, 0, 1, 1, 0, 1, 1, 0, 1, 0 },
                new double[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 1 },
                new double[] { 1, 1, 0, 0, 1, 1, 0, 1, 1, 1 },
            };

            trainOutput = new double[6] { 1, 1, 0, 0, 1, 1 };

            target = new LogisticRegressionAnalysis(trainInput, trainOutput);

            bool expected = true;
            bool actual = target.Compute();

            foreach (LogisticCoefficient coefficient in target.Coefficients)
                Assert.IsFalse(double.IsNaN(coefficient.Value));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ComputeTest3()
        {
            double[][] inputs = training.Submatrix(null, 0, 3);
            double[] outputs = training.GetColumn(4);

            LogisticRegressionAnalysis regression = new LogisticRegressionAnalysis(inputs, outputs);

            bool expected = false;
            bool actual = regression.Compute(maxIterations: 3);
            Assert.AreEqual(expected, actual);
        }



        private static double[][] training = new double[][]
        {
                #region sample data
                new double[] { 0567, 0568, 0001, 0002,    0 },
                new double[] { 0839, 1043, 0204, 0011,    1 },
                new double[] { 0506, 1400, 0894, 0020,    1 },
                new double[] { 0066, 0066, 0000, 0001,    0 },
                new double[] { 0208, 0223, 0015, 0005,    1 },
                new double[] { 0069, 0069, 0000, 0001,    0 },
                new double[] { 0417, 0458, 0041, 0008,    1 },
                new double[] { 0078, 0078, 0000, 0001,    0 },
                new double[] { 0137, 0150, 0013, 0004,    1 },
                new double[] { 0108, 0136, 0028, 0002,    0 },
                new double[] { 0235, 0294, 0059, 0005,    0 },
                new double[] { 0350, 0511, 0161, 0010,    1 },
                new double[] { 0271, 0418, 0147, 0003,    0 },
                new double[] { 0195, 0217, 0022, 0010,    1 },
                new double[] { 0259, 0267, 0008, 0006,    1 },
                new double[] { 0298, 0372, 0074, 0007,    1 },
                new double[] { 0709, 0994, 0285, 0016,    1 },
                new double[] { 1041, 1348, 0307, 0039,    1 },
                new double[] { 0075, 0075, 0000, 0001,    0 },
                new double[] { 0529, 0597, 0068, 0002,    0 },
                new double[] { 0509, 0584, 0075, 0002,    0 },
                new double[] { 0289, 0289, 0000, 0001,    0 },
                new double[] { 0110, 0125, 0015, 0004,    0 },
                new double[] { 0020, 0020, 0000, 0001,    0 },
                new double[] { 0295, 0295, 0000, 0001,    0 },
                new double[] { 0250, 0283, 0033, 0002,    0 },
                new double[] { 0031, 0044, 0013, 0002,    0 },
                new double[] { 0178, 0198, 0020, 0003,    0 },
                new double[] { 0835, 0848, 0013, 0005,    0 },
                new double[] { 0132, 0178, 0046, 0002,    0 },
                new double[] { 0429, 0632, 0203, 0004,    0 },
                new double[] { 0740, 0894, 0154, 0005,    0 },
                new double[] { 0056, 0065, 0009, 0003,    1 },
                new double[] { 0071, 0071, 0000, 0001,    0 },
                new double[] { 0248, 0321, 0073, 0004,    0 },
                new double[] { 0034, 0034, 0000, 0001,    0 },
                new double[] { 0589, 0652, 0063, 0004,    0 },
                new double[] { 0124, 0134, 0010, 0002,    0 },
                new double[] { 0426, 0427, 0001, 0002,    0 },
                new double[] { 0030, 0030, 0000, 0001,    0 },
                new double[] { 0023, 0023, 0000, 0001,    0 },
                new double[] { 0499, 0499, 0000, 0001,    0 },
                #endregion
        };
    }
}
