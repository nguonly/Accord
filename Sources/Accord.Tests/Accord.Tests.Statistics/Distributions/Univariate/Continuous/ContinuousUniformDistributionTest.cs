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
    using Accord.Statistics.Distributions.Fitting;

    [TestClass()]
    public class UniformDistributionTest
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
        public void UniformDistributionConstructorTest()
        {
            double a = 1;
            double b = 5;
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);
            Assert.AreEqual(target.Minimum, a);
            Assert.AreEqual(target.Maximum, b);
        }

        [TestMethod()]
        public void UniformDistributionConstructorTest1()
        {
            double a = 6;
            double b = 5;
            
            bool thrown = false;
            try { ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b); }
            catch (ArgumentOutOfRangeException) { thrown = true; }
            
            Assert.IsTrue(thrown);
        }

        [TestMethod()]
        public void VarianceTest()
        {
            double a = 5;
            double b = 10;
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);
            double actual = target.Variance;
            double expected = System.Math.Pow(b - a, 2) / 12.0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MeanTest()
        {
            double a = -1;
            double b = 5;
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);
            double expected = (a + b) / 2.0;
            double actual = target.Mean;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EntropyTest()
        {
            double a = 1;
            double b = 6;
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);
            double expected = System.Math.Log(b - a);
            double actual = target.Entropy;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProbabilityDensityFunctionTest()
        {
            double a = -5;
            double b = 11;
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);
            double x = 4.2;
            double expected = 0.0625;
            double actual = target.ProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = -5;
            expected = 0.0625;
            actual = target.ProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = -6;
            expected = 0.0;
            actual = target.ProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = 11;
            expected = 0.0625;
            actual = target.ProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = 12;
            expected = 0.0;
            actual = target.ProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LogProbabilityDensityFunctionTest()
        {
            double a = -5;
            double b = 11;
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);
            double x = 4.2;
            double expected = System.Math.Log(0.0625);
            double actual = target.LogProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = -5;
            expected = System.Math.Log(0.0625);
            actual = target.LogProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = -6;
            expected = System.Math.Log(0.0);
            actual = target.LogProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = 11;
            expected = System.Math.Log(0.0625);
            actual = target.LogProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);

            x = 12;
            expected =System.Math.Log( 0.0);
            actual = target.LogProbabilityDensityFunction(x);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FitTest()
        {
            ContinuousUniformDistribution target = new ContinuousUniformDistribution();
            double[] observations = { -1, 2, 5, 2, 3, 1, 4 };
            double[] weights = null;
            target.Fit(observations, weights);
            Assert.AreEqual(-1.0, target.Minimum);
            Assert.AreEqual(5.0, target.Maximum);
        }

        [TestMethod()]
        public void DistributionFunctionTest()
        {
            double a = -2;
            double b = 2;
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);

            double actual;

            actual = target.DistributionFunction(-2);
            Assert.AreEqual(0, actual);

            actual = target.DistributionFunction(-1);
            Assert.AreEqual(0.25, actual);

            actual = target.DistributionFunction(0);
            Assert.AreEqual(0.5, actual);

            actual = target.DistributionFunction(1);
            Assert.AreEqual(0.75, actual);

            actual = target.DistributionFunction(2);
            Assert.AreEqual(1, actual);
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod()]
        public void CloneTest()
        {
            double a = 12; 
            double b = 72; 
            ContinuousUniformDistribution target = new ContinuousUniformDistribution(a, b);
            
            ContinuousUniformDistribution clone = (ContinuousUniformDistribution)target.Clone();

            Assert.AreNotSame(target, clone);
            Assert.AreEqual(target.Entropy, clone.Entropy);
            Assert.AreEqual(target.Maximum, clone.Maximum);
            Assert.AreEqual(target.Minimum, clone.Minimum);
            Assert.AreEqual(target.StandardDeviation, clone.StandardDeviation);
            Assert.AreEqual(target.Variance, clone.Variance);
        }
    }
}
