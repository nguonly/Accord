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
    using Accord.Statistics.Distributions.Univariate;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass()]
    public class NormalDistributionTest
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
        public void FitTest()
        {
            double expectedMean = 1.125;
            double expectedSigma = 1.01775897605147;

            NormalDistribution target;

            target = new NormalDistribution();
            double[] observations = { 0.10, 0.40, 2.00, 2.00 };
            double[] weights = { 0.25, 0.25, 0.25, 0.25 };
            target.Fit(observations, weights);

            Assert.AreEqual(expectedMean, target.Mean);
            Assert.AreEqual(expectedSigma, target.StandardDeviation, 1e-6);


            target = new NormalDistribution();
            double[] observations2 = { 0.10, 0.10, 0.40, 2.00 };
            double[] weights2 = { 0.125, 0.125, 0.25, 0.50 };
            target.Fit(observations2, weights2);

            Assert.AreEqual(expectedMean, target.Mean);
            // Assert.AreEqual(expectedSigma, target.StandardDeviation, 1e-6);
        }

        [TestMethod()]
        public void FitTest2()
        {
            NormalDistribution target;

            target = new NormalDistribution();
            double[] observations = { 1, 1, 1, 1 };

            bool thrown = false;
            try { target.Fit(observations); }
            catch (ArgumentException) { thrown = true; }

            Assert.IsTrue(thrown);
        }

        [TestMethod()]
        public void ConstructorTest()
        {
            double mean = 7;
            double dev = 5;
            double var = 25;

            NormalDistribution target = new NormalDistribution(mean, dev);
            Assert.AreEqual(mean, target.Mean);
            Assert.AreEqual(dev, target.StandardDeviation);
            Assert.AreEqual(var, target.Variance);

            target = new NormalDistribution();
            Assert.AreEqual(0, target.Mean);
            Assert.AreEqual(1, target.StandardDeviation);
            Assert.AreEqual(1, target.Variance);

            target = new NormalDistribution(3);
            Assert.AreEqual(3, target.Mean);
            Assert.AreEqual(1, target.StandardDeviation);
            Assert.AreEqual(1, target.Variance);
        }

        [TestMethod()]
        public void ConstructorTest2()
        {
            bool thrown = false;

            try { NormalDistribution target = new NormalDistribution(4.2, 0); }
            catch (ArgumentOutOfRangeException) { thrown = true; }

            Assert.IsTrue(thrown);
        }


        [TestMethod()]
        public void ProbabilityDensityFunctionTest()
        {
            double x = 3;
            double mean = 7;
            double dev = 5;

            NormalDistribution target = new NormalDistribution(mean, dev);

            double expected = 0.0579383105522966;
            double actual = target.ProbabilityDensityFunction(x);

            Assert.IsFalse(double.IsNaN(actual));
            Assert.AreEqual(expected, actual, 1e-15);
        }

        [TestMethod()]
        public void ProbabilityDensityFunctionTest2()
        {
            double expected, actual;

            // Test for small variance
            NormalDistribution target = new NormalDistribution(4.2, double.Epsilon);

            expected = 0;
            actual = target.ProbabilityDensityFunction(0);
            Assert.AreEqual(expected, actual);

            expected = double.PositiveInfinity;
            actual = target.ProbabilityDensityFunction(4.2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LogProbabilityDensityFunctionTest()
        {
            double x = 3;
            double mean = 7;
            double dev = 5;

            NormalDistribution target = new NormalDistribution(mean, dev);

            double expected = System.Math.Log(0.0579383105522966);
            double actual = target.LogProbabilityDensityFunction(x);

            Assert.IsFalse(double.IsNaN(actual));
            Assert.AreEqual(expected, actual, 1e-15);
        }


        [TestMethod()]
        public void DistributionFunctionTest()
        {
            double x = 3;
            double mean = 7;
            double dev = 5;

            NormalDistribution target = new NormalDistribution(mean, dev);

            double expected = 0.211855398583397;
            double actual = target.DistributionFunction(x);

            Assert.IsFalse(double.IsNaN(actual));
            Assert.AreEqual(expected, actual, 1e-15);
        }

        [TestMethod()]
        public void DistributionFunctionTest3()
        {
            double expected, actual;

            // Test small variance
            NormalDistribution target = new NormalDistribution(1.0, double.Epsilon);

            expected = 0;
            actual = target.DistributionFunction(0);
            Assert.AreEqual(expected, actual);

            expected = 0.5;
            actual = target.DistributionFunction(1.0);
            Assert.AreEqual(expected, actual);

            expected = 1.0;
            actual = target.DistributionFunction(1.0 + 1e-15);
            Assert.AreEqual(expected, actual);

            expected = 0.0;
            actual = target.DistributionFunction(1.0 - 1e-15);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void InverseDistributionFunctionTest()
        {
            double[] expected =
            {
                Double.NegativeInfinity, -4.38252, -2.53481, -1.20248,
                -0.0640578, 1.0, 2.06406, 3.20248, 4.53481, 6.38252,
                Double.PositiveInfinity
            };

            NormalDistribution target = new NormalDistribution(1.0, 4.2);

            for (int i = 0; i < expected.Length; i++)
            {
                double x = i / 10.0;
                double actual = target.InverseDistributionFunction(x);
                Assert.AreEqual(expected[i], actual, 1e-5);
                Assert.IsFalse(Double.IsNaN(actual));
            }
        }

        [TestMethod()]
        public void ZScoreTest()
        {
            double x = 5;
            double mean = 3;
            double dev = 6;

            NormalDistribution target = new NormalDistribution(mean, dev);

            double expected = (x - 3) / 6;
            double actual = target.ZScore(x);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CloneTest()
        {
            NormalDistribution target = new NormalDistribution(0.5, 4.2);

            NormalDistribution clone = (NormalDistribution)target.Clone();

            Assert.AreNotSame(target, clone);
            Assert.AreEqual(target.Entropy, clone.Entropy);
            Assert.AreEqual(target.Mean, clone.Mean);
            Assert.AreEqual(target.StandardDeviation, clone.StandardDeviation);
            Assert.AreEqual(target.Variance, clone.Variance);
        }

    }
}
