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
    using System;
    using Accord.Statistics.Distributions.Univariate;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class GammaDistributionTest
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
        public void GammaDistributionConstructorTest()
        {
            double shape = 0.4;
            double scale = 4.2;

            double[] expected = 
            {
                double.NegativeInfinity, 0.987114, 0.635929, 0.486871, 0.400046,
                0.341683, 0.299071, 0.266236, 0.239956, 0.218323, 0.200126
            };

            GammaDistribution target = new GammaDistribution(scale, shape);

            Assert.AreEqual(shape, target.Shape);
            Assert.AreEqual(scale, target.Scale);

            Assert.AreEqual(shape*scale, target.Mean);
            Assert.AreEqual(shape * scale * scale, target.Variance);
            
        }

        [TestMethod()]
        public void DensityFunctionTest()
        {
            double shape = 0.4;
            double scale = 4.2;

            double[] pdf = 
            {
                double.PositiveInfinity, 0.987114, 0.635929, 0.486871, 0.400046,
                0.341683, 0.299071, 0.266236, 0.239956, 0.218323, 0.200126
            };

            GammaDistribution target = new GammaDistribution(scale, shape);

            for (int i = 0; i < 11; i++)
            {
                double x = i / 10.0;
                double actual = target.ProbabilityDensityFunction(x);
                double expected = pdf[i];

                Assert.AreEqual(expected, actual, 1e-6);
                Assert.IsFalse(double.IsNaN(actual));

                double logActual = target.LogProbabilityDensityFunction(x);
                double logExpected = Math.Log(pdf[i]);

                Assert.AreEqual(logExpected, logActual, 1e-5);
                Assert.IsFalse(double.IsNaN(logActual));
            }
        }

        [TestMethod()]
        public void CumulativeFunctionTest()
        {
            double shape = 0.4;
            double scale = 4.2;

            double[] cdf = 
            {
                0, 0.251017, 0.328997, 0.38435, 0.428371, 0.465289,
                0.497226, 0.525426, 0.55069, 0.573571, 0.594469
            };

            GammaDistribution target = new GammaDistribution(scale, shape);

            for (int i = 0; i < 11; i++)
            {
                double x = i / 10.0;
                double actual = target.DistributionFunction(x);
                double expected = cdf[i];

                Assert.AreEqual(expected, actual, 1e-5);
                Assert.IsFalse(double.IsNaN(actual));
            }
        }
    }
}