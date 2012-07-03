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
    using Accord.Math;
    using Accord.Statistics.Distributions.Multivariate;

    [TestClass()]
    public class MultinomialDistributionTest
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
        public void ProbabilityMassFunctionTest()
        {
            MultinomialDistribution dist = new MultinomialDistribution(5, 0.25, 0.25, 0.25, 0.25);

            int[] observation = { 1, 1, 1, 2 };

            double actual = dist.ProbabilityMassFunction(observation);
            double expected = 0.05859375;

            Assert.AreEqual(expected, actual, 1e-6);
        }

        [TestMethod()]
        public void LogProbabilityMassFunctionTest()
        {
            MultinomialDistribution dist = new MultinomialDistribution(5, 0.25, 0.25, 0.25, 0.25);

            int[] observation = { 1, 1, 1, 2 };

            double actual = dist.LogProbabilityMassFunction(observation);
            double expected = System.Math.Log(0.058593750);

            Assert.AreEqual(expected, actual, 1e-6);
        }

        [TestMethod()]
        public void FitTest()
        {
            MultinomialDistribution dist = new MultinomialDistribution(7, new double[2]);

            double[][] observation =
            { 
                new double[] { 0, 2 },
                new double[] { 1, 2 },
                new double[] { 5, 1 },
            };

            dist.Fit(observation);

            Assert.AreEqual(dist.Probabilities[0], 0.857142857142857, 0.000000001);
            Assert.AreEqual(dist.Probabilities[1], 0.714285714285714, 0.000000001);
        }

    }
}
