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
    public class VonMisesDistributionTest
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
            double[] angles = 
            {
               2.537498, 0.780449, 3.246623, 1.835845, 1.525273,
               2.821987, 1.783134, 1.165753, 3.298262, 2.941366,
               2.485515, 2.090029, 2.460631, 2.804243, 1.626327,
            };


            var distribution = VonMisesDistribution.Estimate(angles);

            Assert.AreEqual(2.411822, distribution.Concentration, 1e-6);
            Assert.AreEqual(2.249981, distribution.Mean, 1e-6);

            Assert.AreEqual(0.2441525, distribution.Variance, 1e-3);
        }

        [TestMethod()]
        public void ProbabilityDensityFunctionTest()
        {
            VonMisesDistribution dist = new VonMisesDistribution(2.249981, 2.411822);

            double actual = dist.ProbabilityDensityFunction(2.14);
            double expected = 0.5686769438969197;

            Assert.AreEqual(expected, actual, 1e-10);
        }

        [TestMethod()]
        public void LogProbabilityDensityFunctionTest()
        {
            VonMisesDistribution dist = new VonMisesDistribution(2.249981, 2.411822);
            double x = 2.14;

            double actual = dist.LogProbabilityDensityFunction(x);
            double expected = System.Math.Log(dist.ProbabilityDensityFunction(x));

            Assert.AreEqual(expected, actual, 1e-10);
        }
    }
}
