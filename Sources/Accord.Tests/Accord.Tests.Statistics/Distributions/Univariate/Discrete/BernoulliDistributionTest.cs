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
    public class BernoulliDistributionTest
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
            BernoulliDistribution target = new BernoulliDistribution(0.6);

            double expected = 0.6; 
            double actual = target.ProbabilityMassFunction(1);

            Assert.AreEqual(expected, actual, 1e-6);
        }

        [TestMethod()]
        public void LogProbabilityMassFunctionTest()
        {
            BernoulliDistribution target = new BernoulliDistribution(0.6);

            double expected = System.Math.Log(0.6);
            double actual = target.LogProbabilityMassFunction(1);

            Assert.AreEqual(expected, actual, 1e-6);
        }

        [TestMethod()]
        public void DistributionFunctionTest()
        {
            BernoulliDistribution target = new BernoulliDistribution(0.6);

            double expected = 0.4;
            double actual = target.DistributionFunction(0);

            Assert.AreEqual(expected, actual, 1e-6);
        }
    }
}
