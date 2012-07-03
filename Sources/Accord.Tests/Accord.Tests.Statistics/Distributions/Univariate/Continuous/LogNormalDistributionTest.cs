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

using Accord.Statistics.Distributions.Univariate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Accord.Statistics.Distributions.Fitting;

namespace Accord.Tests.Statistics
{


    /// <summary>
    ///This is a test class for LogNormalDistributionTest and is intended
    ///to contain all LogNormalDistributionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogNormalDistributionTest
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


        [TestMethod()]
        public void LogNormalDistributionConstructorTest()
        {
            double location = 9.2;
            double shape = 4.4;
            LognormalDistribution target = new LognormalDistribution(location, shape);
            Assert.AreEqual(location, target.Location);
            Assert.AreEqual(shape, target.Shape);
        }

        [TestMethod()]
        public void LogNormalDistributionConstructorTest1()
        {
            double location = 4.1;
            LognormalDistribution target = new LognormalDistribution(location);
            Assert.AreEqual(location, target.Location);
        }

        [TestMethod()]
        public void LogNormalDistributionConstructorTest2()
        {
            LognormalDistribution target = new LognormalDistribution();
            Assert.AreEqual(0, target.Location);
            Assert.AreEqual(1, target.Shape);
        }

        [TestMethod()]
        public void CloneTest()
        {
            LognormalDistribution target = new LognormalDistribution(1.7, 4.2);

            LognormalDistribution clone = (LognormalDistribution)target.Clone();

            Assert.AreNotSame(target, clone);
            Assert.AreEqual(target.Entropy, clone.Entropy);
            Assert.AreEqual(target.Location, clone.Location);
            Assert.AreEqual(target.Mean, clone.Mean);
            Assert.AreEqual(target.Shape, clone.Shape);
            Assert.AreEqual(target.StandardDeviation, clone.StandardDeviation);
            Assert.AreEqual(target.Variance, clone.Variance);
        }

        [TestMethod()]
        public void DistributionFunctionTest()
        {
            LognormalDistribution target = new LognormalDistribution(1.7, 4.2);

            double x = 2.2;
            double expected = 0.414090938987083;
            double actual = target.DistributionFunction(x);

            Assert.AreEqual(expected, actual, 1e-15);
        }

        [TestMethod()]
        public void EstimateTest()
        {
            double[] observations = { 2, 2, 2, 2, 2 };

            NormalOptions options = new NormalOptions() { Regularization = 0.1 };

            LognormalDistribution actual = LognormalDistribution.Estimate(observations, options);
            Assert.AreEqual(System.Math.Log(2), actual.Location);
            Assert.AreEqual(System.Math.Sqrt(0.1), actual.Shape);
        }

        [TestMethod()]
        public void EstimateTest1()
        {
            double[] observations = 
            { 
                1.26, 0.34, 0.70, 1.75, 50.57, 1.55, 0.08, 0.42, 0.50, 3.20, 
                0.15, 0.49, 0.95, 0.24, 1.37, 0.17, 6.98, 0.10, 0.94, 0.38 
            };

            LognormalDistribution actual = LognormalDistribution.Estimate(observations);


            double expectedLocation = -0.307069523211925;
            double expectedShape = 1.51701553338489;

            Assert.AreEqual(expectedLocation, actual.Location, 1e-15);
            Assert.AreEqual(expectedShape, actual.Shape, 1e-14);
        }

        [TestMethod()]
        public void EstimateTest2()
        {
            double[] observations = { 0.04, 0.12, 1.52 };

            double[] weights = { 0.25, 0.50, 0.25 };
            LognormalDistribution actual = LognormalDistribution.Estimate(observations, weights);

            Assert.AreEqual(-1.76017314060255, actual.Location, 1e-15);
            Assert.AreEqual(1.6893403335885702, actual.Shape);
        }

        [TestMethod()]
        public void ProbabilityDensityFunctionTest()
        {
            LognormalDistribution target = new LognormalDistribution(1.7, 4.2);

            double x = 2.2;
            double expected = 0.0421705870979553; 
            double actual = target.ProbabilityDensityFunction(x);

            Assert.AreEqual(expected, actual, 1e-15);
        }

        [TestMethod()]
        public void LogProbabilityDensityFunctionTest()
        {
            LognormalDistribution target = new LognormalDistribution(1.7, 4.2);

            double x = 2.2;
            double expected = System.Math.Log(0.0421705870979553);
            double actual = target.LogProbabilityDensityFunction(x);

            Assert.AreEqual(expected, actual, 1e-15);
        }

        [TestMethod()]
        public void MeanTest()
        {
            LognormalDistribution target = new LognormalDistribution(0.42, 0.56);
            double actual = target.Mean;
            Assert.AreEqual(1.7803322425420858, actual);
        }

        [TestMethod()]
        public void StandardTest()
        {
            LognormalDistribution actual = LognormalDistribution.Standard;
            Assert.AreEqual(0, actual.Location);
            Assert.AreEqual(1, actual.Shape);

            bool thrown = false;
            try { actual.Fit(new[] { 0.0 }); }
            catch (InvalidOperationException) { thrown = true; }
            Assert.IsTrue(thrown);
        }

        [TestMethod()]
        public void VarianceTest()
        {
            LognormalDistribution target = new LognormalDistribution(0.42, 0.56);
            double actual = target.Variance;
            Assert.AreEqual(1.1674914219333172, actual);
        }
    }
}
