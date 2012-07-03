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

    using Accord.Statistics.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class TTestTest
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
        public void TTestConstructorTest()
        {

            // mean = 0.5, var = 1
            double[] sample = 
            { 
                -0.849886940156521,	3.53492346633185,  1.22540422494611, 0.436945126810344, 1.21474290382610,
                 0.295033941700225, 0.375855651783688, 1.98969760778547, 1.90903448980048,	1.91719241342961
            };

            double hypothesizedMean = 0;
            TTestHypotesis hypothesis = TTestHypotesis.MeanIsDifferentThanHypothesis;
            TTest target = new TTest(sample, hypothesizedMean, hypothesis);

            Assert.AreEqual(3.1254485381338246, target.Statistic);
            Assert.AreEqual(Hypothesis.TwoTail, target.Hypothesis);
            Assert.AreEqual(0.012210924322697769, target.PValue);


            hypothesis = TTestHypotesis.MeanIsGreaterThanHypothesis;
            target = new TTest(sample, hypothesizedMean, hypothesis);

            Assert.AreEqual(3.1254485381338246, target.Statistic);
            Assert.AreEqual(Hypothesis.OneUpper, target.Hypothesis); // right tail
            Assert.AreEqual(0.0061054621613488846, target.PValue);


            hypothesis = TTestHypotesis.MeanIsSmallerThanHypothesis;
            target = new TTest(sample, hypothesizedMean, hypothesis);

            Assert.AreEqual(3.1254485381338246, target.Statistic);
            Assert.AreEqual(Hypothesis.OneLower, target.Hypothesis); // left tail
            Assert.AreEqual(0.99389453783865112, target.PValue);
        }

        [TestMethod()]
        public void TTestConstructorTest2()
        {

            // Consider a sample generated from a Gaussian
            // distribution with mean 0.5 and unit variance.

            double[] sample = 
            { 
                -0.849886940156521,	3.53492346633185,  1.22540422494611, 0.436945126810344, 1.21474290382610,
                 0.295033941700225, 0.375855651783688, 1.98969760778547, 1.90903448980048,	1.91719241342961
            };

            // One may rise the hypothesis that the mean of the sample is not
            // significantly different from zero. In other words, the fact that
            // this particular sample has mean 0.5 may be attributed to chance.

            double hypothesizedMean = 0;

            // Create a T-Test to check this hypothesis
            TTest test = new TTest(sample, hypothesizedMean,
                TTestHypotesis.MeanIsDifferentThanHypothesis);

            // Check if the mean is significantly different
            Assert.AreEqual(true, test.Significant);

            // Now, we would like to test if the sample mean is
            // significantly greater than the hypothetised zero.

            // Create a T-Test to check this hypothesis
            TTest greater = new TTest(sample, hypothesizedMean,
                TTestHypotesis.MeanIsGreaterThanHypothesis);

            // Check if the mean is significantly larger
            Assert.AreEqual(true, greater.Significant);

            // Now, we would like to test if the sample mean is
            // significantly smaller than the hypothetised zero.

            // Create a T-Test to check this hypothesis
            TTest smaller = new TTest(sample, hypothesizedMean,
                TTestHypotesis.MeanIsSmallerThanHypothesis);

            // Check if the mean is significantly smaller
            Assert.AreEqual(false, smaller.Significant);

        }

    }
}
