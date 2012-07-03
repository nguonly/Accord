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

    [TestClass()]
    public class InverseGaussianTest
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
        public void ConstructorTest()
        {
            InverseGaussianDistribution g = new InverseGaussianDistribution(1.2, 4.2);
            Assert.AreEqual(1.2, g.Mean);
            Assert.AreEqual(4.2, g.Shape);

            Assert.AreEqual(0.41142857142857142, g.Variance);
            Assert.AreEqual(0.64142698058981851, g.StandardDeviation);
        }

        [TestMethod()]
        public void ProbabilityFunctionTest()
        {
            InverseGaussianDistribution g = new InverseGaussianDistribution(1.2, 4.2);

            double expected = 0.363257;
            double actual = g.ProbabilityDensityFunction(0.42);

            Assert.AreEqual(expected, actual, 1e-6);
        }

        [TestMethod()]
        public void ProbabilityFunctionTest2()
        {
            InverseGaussianDistribution g = new InverseGaussianDistribution(4.1, 1.2);

            double[] expected =
            {
                0.0457398, 0.323655, 0.477189, 0.509189, 0.490063, 0.453721, 0.413867, 0.375711, 0.34101, 0.310123
            };

            for (int i = 0; i < expected.Length; i++)
            {
                double x = (i + 1) / 10.0;

                double actual = g.ProbabilityDensityFunction(x);
                Assert.AreEqual(expected[i], actual, 1e-6);
                Assert.IsFalse(double.IsNaN(actual));
            }
            
        }

        [TestMethod()]
        public void CumulativeFunctionTest()
        {
            InverseGaussianDistribution g = new InverseGaussianDistribution(1.2, 4.2);

            double expected = 0.030679;
            double actual = g.DistributionFunction(0.42);

            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));
        }

        [TestMethod()]
        public void CumulativeFunctionTest2()
        {
            InverseGaussianDistribution g = new InverseGaussianDistribution(4.1, 1.2);

            double[] expected =
            {
                0.000710666, 0.0190607, 0.0604859, 0.110461, 0.160665, 0.207921, 0.2513, 0.290755, 0.326559, 0.359084
            };

            for (int i = 0; i < expected.Length; i++)
            {
                double x = (i + 1) / 10.0;
                double actual = g.DistributionFunction(x);

                Assert.AreEqual(expected[i], actual, 1e-6);
                Assert.IsFalse(double.IsNaN(actual));
            }
        }


    }
}