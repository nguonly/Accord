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
    using Accord.Statistics.Distributions.Fitting;
    using Accord.Statistics.Distributions.Univariate;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class HypergeometricDistributionTest
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
        public void HypergeometricDistributionConstructorTest()
        {
            bool thrown;

            thrown = false;
            try { new HypergeometricDistribution(10, 11, 0); }
            catch (ArgumentException) { thrown = true; }
            Assert.IsTrue(thrown);

            thrown = false;
            try { new HypergeometricDistribution(10, 9, 11); }
            catch (ArgumentException) { thrown = true; }
            Assert.IsTrue(thrown);

            thrown = false;
            try { new HypergeometricDistribution(0, 1, 0); }
            catch (ArgumentException) { thrown = true; }
            Assert.IsTrue(thrown);

            thrown = false;
            try { new HypergeometricDistribution(1, 0, 0); }
            catch (ArgumentException) { thrown = true; }
            Assert.IsTrue(thrown);

            thrown = false;
            try { new HypergeometricDistribution(1, 1, -1); }
            catch (ArgumentException) { thrown = true; }
            Assert.IsTrue(thrown);

            int N = 10;
            int n = 8;
            int m = 9;

            var target = new HypergeometricDistribution(N, n, m);
            Assert.AreEqual(N, target.PopulationSize);
            Assert.AreEqual(n, target.SampleSize);
            Assert.AreEqual(m, target.PopulationSuccess);

            double dN = N;
            double dn = n;
            double dm = m;

            Assert.AreEqual(dn * (dm / dN), target.Mean);
            Assert.AreEqual(dn * dm * (dN - dm) * (dN - dn) / (dN * dN * (dN - 1.0)), target.Variance);
        }

        [TestMethod()]
        public void CloneTest()
        {
            int populationSize = 12;
            int draws = 5;
            int success = 4;
            var target = new HypergeometricDistribution(populationSize, draws, success);

            var actual = (HypergeometricDistribution)target.Clone();

            Assert.AreNotSame(target, actual);
            Assert.AreNotEqual(target, actual);

            Assert.AreEqual(target.PopulationSize, actual.PopulationSize);
            Assert.AreEqual(target.SampleSize, actual.SampleSize);
            Assert.AreEqual(target.PopulationSuccess, actual.PopulationSuccess);
        }

        [TestMethod()]
        public void DistributionFunctionTest()
        {
            int populationSize = 15;
            int draws = 7;
            int success = 8;
            var target = new HypergeometricDistribution(populationSize, draws, success);

            int k = 5;
            double expected = 0.96829836829836835;
            double actual = target.DistributionFunction(k);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LogProbabilityMassFunctionTest()
        {
            int populationSize = 15;
            int draws = 7;
            int success = 8;
            var target = new HypergeometricDistribution(populationSize, draws, success);

            int k = 5;
            double expected = Math.Log(0.182750582750583);
            double actual = target.LogProbabilityMassFunction(k);
            Assert.AreEqual(expected, actual, 1e-10);
            Assert.IsFalse(Double.IsNaN(actual));
        }

        [TestMethod()]
        public void ProbabilityMassFunctionTest()
        {
            int N = 50;
            int n = 10;
            int m = 5;
            var target = new HypergeometricDistribution(N, n, m);

            int k = 4;
            double expected = 0.0039645830580150657;
            double actual = target.ProbabilityMassFunction(k);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProbabilityMassFunctionTest2()
        {
            int populationSize = 15;
            int draws = 7;
            int success = 8;
            var target = new HypergeometricDistribution(populationSize, draws, success);

            int k = 5;
            double expected = 0.182750582750583;
            double actual = target.ProbabilityMassFunction(k);
            Assert.AreEqual(expected, actual, 1e-10);
            Assert.IsFalse(Double.IsNaN(actual));
        }

    }
}
