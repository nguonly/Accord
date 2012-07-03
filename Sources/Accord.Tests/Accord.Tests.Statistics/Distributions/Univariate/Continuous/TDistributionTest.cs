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
    public class TDistributionTest
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
        public void VarianceTest()
        {
            TDistribution target = new TDistribution(3);
            double actual = target.Variance;
            double expected = 3;
            Assert.AreEqual(expected, actual);

            target = new TDistribution(2);
            actual = target.Variance;
            expected = Double.PositiveInfinity;
            Assert.AreEqual(expected, actual);

            target = new TDistribution(1);
            actual = target.Variance;
            Assert.IsTrue(Double.IsNaN(actual));
        }

        [TestMethod()]
        public void MeanTest()
        {
            TDistribution target;
            double actual;

            target = new TDistribution(1);
            actual = target.Mean;
            Assert.IsTrue(Double.IsNaN(actual));

            target = new TDistribution(2);
            actual = target.Mean;
            double expected = 0;
            Assert.AreEqual(expected, actual);

            target = new TDistribution(3);
            actual = target.Mean;
            expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProbabilityDensityFunctionTest()
        {
            TDistribution target = new TDistribution(1);
            double expected = 0.31830988618379075;
            double actual = target.ProbabilityDensityFunction(0);
            Assert.AreEqual(expected, actual);

            expected = 0.017076710632177614;
            actual = target.ProbabilityDensityFunction(4.2);
            Assert.AreEqual(expected, actual);

            target = new TDistribution(2);
            expected = 0.35355339059327379;
            actual = target.ProbabilityDensityFunction(0);
            Assert.AreEqual(expected, actual);

            expected = 0.011489146700777093;
            actual = target.ProbabilityDensityFunction(4.2);
            Assert.AreEqual(expected, actual);

            target = new TDistribution(3);
            expected = 0.36755259694786141;
            actual = target.ProbabilityDensityFunction(0);
            Assert.AreEqual(expected, actual);

            expected = 0.0077650207237835792;
            actual = target.ProbabilityDensityFunction(4.2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LogProbabilityDensityFunctionTest()
        {
            TDistribution target = new TDistribution(1);
            double expected = System.Math.Log(0.31830988618379075);
            double actual = target.LogProbabilityDensityFunction(0);
            Assert.AreEqual(expected, actual);

            expected = System.Math.Log(0.017076710632177614);
            actual = target.LogProbabilityDensityFunction(4.2);
            Assert.AreEqual(expected, actual,1e-6);

            target = new TDistribution(2);
            expected = System.Math.Log(0.35355339059327379);
            actual = target.LogProbabilityDensityFunction(0);
            Assert.AreEqual(expected, actual, 1e-6);

            expected = System.Math.Log(0.011489146700777093);
            actual = target.LogProbabilityDensityFunction(4.2);
            Assert.AreEqual(expected, actual, 1e-6);

            target = new TDistribution(3);
            expected = System.Math.Log(0.36755259694786141);
            actual = target.LogProbabilityDensityFunction(0);
            Assert.AreEqual(expected, actual, 1e-6);

            expected = System.Math.Log(0.0077650207237835792);
            actual = target.LogProbabilityDensityFunction(4.2);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [TestMethod()]
        public void FitTest()
        {
            bool thrown = false;
            TDistribution target = new TDistribution(1);
            try { target.Fit(null, null, null); }
            catch (NotSupportedException) { thrown = true; }
            Assert.IsTrue(thrown);
        }

        [TestMethod()]
        public void DistributionFunctionTest()
        {
            TDistribution target = new TDistribution(1);
            double expected = 0.5;
            double actual = target.DistributionFunction(0);
            Assert.IsFalse(Double.IsNaN(actual));
            Assert.AreEqual(expected, actual, 1e-15);

            expected = 0.92559723470138278;
            actual = target.DistributionFunction(4.2);
            Assert.AreEqual(expected, actual);

            target = new TDistribution(2);
            expected = 0.5;
            actual = target.DistributionFunction(0);
            Assert.AreEqual(expected, actual);

            expected = 0.97385836652685043;
            actual = target.DistributionFunction(4.2);
            Assert.AreEqual(expected, actual);

            target = new TDistribution(3);
            expected = 0.5;
            actual = target.DistributionFunction(0);
            Assert.IsFalse(Double.IsNaN(actual));
            Assert.AreEqual(expected, actual, 1e-15);

            expected = 0.98768396091153043;
            actual = target.DistributionFunction(4.2);
            Assert.AreEqual(expected, actual);

            expected = 0.16324737815131229;
            actual = target.DistributionFunction(-1.17);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CloneTest()
        {
            int degreesOfFreedom = 5;
            TDistribution target = new TDistribution(degreesOfFreedom);
            TDistribution clone = (TDistribution)target.Clone();

            Assert.AreNotSame(target, clone);
            Assert.AreEqual(target.DegreesOfFreedom, clone.DegreesOfFreedom);
            Assert.AreEqual(target.Mean, clone.Mean);
            Assert.AreEqual(target.Variance, clone.Variance);
        }

        [TestMethod()]
        public void TDistributionConstructorTest()
        {
            int degreesOfFreedom = 4;
            TDistribution target = new TDistribution(degreesOfFreedom);
            Assert.AreEqual(degreesOfFreedom, target.DegreesOfFreedom);

            bool thrown = false;
            try { target = new TDistribution(0); }
            catch (ArgumentOutOfRangeException) { thrown = true; }
            Assert.IsTrue(thrown);

            thrown = false;
            try { target = new TDistribution(-1); }
            catch (ArgumentOutOfRangeException) { thrown = true; }
            Assert.IsTrue(thrown);
        }
    }
}
