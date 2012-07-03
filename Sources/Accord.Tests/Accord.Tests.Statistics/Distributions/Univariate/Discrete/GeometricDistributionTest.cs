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
    using Accord.Math;
    using Accord.Statistics.Distributions.Fitting;
    using Accord.Statistics.Distributions.Univariate;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass()]
    public class GeometricDistributionTest
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
        public void GeometricDistributionConstructorTest()
        {
            double successProbability = 0.9;
            GeometricDistribution target = new GeometricDistribution(successProbability);
            Assert.AreEqual(0.9, target.ProbabilityOfSuccess);
            Assert.AreEqual((1 - 0.9) / 0.9, target.Mean);
            Assert.AreEqual((1 - 0.9) / (0.9 * 0.9), target.Variance);
        }

        [TestMethod()]
        public void CloneTest()
        {
            double successProbability = 1;
            GeometricDistribution target = new GeometricDistribution(successProbability);
            GeometricDistribution actual = (GeometricDistribution)target.Clone();

            Assert.AreNotEqual(target, actual);
            Assert.AreNotSame(target, actual);

            Assert.AreEqual(target.ProbabilityOfSuccess, actual.ProbabilityOfSuccess);
        }

        [TestMethod()]
        public void DistributionFunctionTest()
        {
            double successProbability = 0.42;
            GeometricDistribution target = new GeometricDistribution(successProbability);

            double[] values = { -1, 0, 1, 2, 3, 4, 5 };
            double[] expected = { 0, 0.42, 0.6636, 0.804888, 0.88683504, 0.9343643232, 0.961931307456 };


            for (int i = 0; i < values.Length; i++)
            {
                double actual = target.DistributionFunction(i - 1);
                Assert.AreEqual(expected[i], actual, 1e-10);
                Assert.IsFalse(Double.IsNaN(actual));
            }
        }

        [TestMethod()]
        public void FitTest()
        {
            double successProbability = 0;
            GeometricDistribution target = new GeometricDistribution(successProbability);

            double[] observations = { 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0 };
            double[] weights = null;
            IFittingOptions options = null;

            target.Fit(observations, weights, options);

            Assert.AreEqual(1 / (1 - 4 / 12.0), target.ProbabilityOfSuccess);
        }

        [TestMethod()]
        public void FitTest2()
        {
            double successProbability = 0;
            GeometricDistribution target = new GeometricDistribution(successProbability);

            double[] observations = { 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0 };
            double[] weights = { 0, 1, 1, 2, 0, 2, 1, 0, 0, 0, 1, 0 };
            weights = weights.Divide(weights.Sum());
            IFittingOptions options = null;

            target.Fit(observations, weights, options);

            Assert.AreEqual(1 / (1 - 4.0 / 8.0), target.ProbabilityOfSuccess);
        }


        [TestMethod()]
        public void ProbabilityMassFunctionTest()
        {
            double successProbability = 0.42;
            GeometricDistribution target = new GeometricDistribution(successProbability);

            double[] expected = { 0, 0.42, 0.2436, 0.141288, 0.08194704, 0.0475292832, 0.027566984256 };

            for (int i = 0; i < expected.Length; i++)
            {
                double actual = target.ProbabilityMassFunction(i - 1);
                Assert.AreEqual(expected[i], actual, 1e-10);
                Assert.IsFalse(Double.IsNaN(actual));
            }
        }

    }
}
