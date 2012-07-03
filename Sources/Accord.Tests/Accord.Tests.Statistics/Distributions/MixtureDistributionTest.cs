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

using Accord.Statistics.Distributions.Univariate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.Math;
using Accord.Statistics.Distributions.Fitting;
namespace Accord.Tests.Statistics
{


    /// <summary>
    ///This is a test class for MixtureDistributionTest and is intended
    ///to contain all MixtureDistributionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MixtureDistributionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        public void ConstructorTest1()
        {
            NormalDistribution[] components = new NormalDistribution[2];
            components[0] = new NormalDistribution(2, 1);
            components[1] = new NormalDistribution(5, 1);

            var mixture = new Mixture<NormalDistribution>(components);

            double[] expected = { 0.5, 0.5 };

            Assert.IsTrue(expected.IsEqual(mixture.Coefficients));
            Assert.AreEqual(components, mixture.Components);
        }

        [TestMethod()]
        public void FitTest()
        {
            double[] coefficients = { 0.50, 0.50 };

            NormalDistribution[] components = new NormalDistribution[2];
            components[0] = new NormalDistribution(2, 1);
            components[1] = new NormalDistribution(5, 1);

            var target = new Mixture<NormalDistribution>(coefficients, components);

            double[] values = { 0, 1, 1, 0, 1, 6, 6, 5, 7, 5 };
            double[] part1 = values.Submatrix(0, 4);
            double[] part2 = values.Submatrix(5, 9);

            MixtureOptions options = new MixtureOptions() { Threshold = 1e-10 };

            target.Fit(values, options);
            var actual = target;

            var mean1 = Accord.Statistics.Tools.Mean(part1);
            var var1 = Accord.Statistics.Tools.Variance(part1);
            Assert.AreEqual(mean1, actual.Components[0].Mean, 1e-6);
            Assert.AreEqual(var1, actual.Components[0].Variance, 1e-6);

            var mean2 = Accord.Statistics.Tools.Mean(part2);
            var var2 = Accord.Statistics.Tools.Variance(part2);
            Assert.AreEqual(mean2, actual.Components[1].Mean, 1e-6);
            Assert.AreEqual(var2, actual.Components[1].Variance, 1e-5);

            var expectedMean = Accord.Statistics.Tools.Mean(values);
            var actualMean = actual.Mean;
            Assert.AreEqual(expectedMean, actualMean, 1e-7);

            var expectedVar = Accord.Statistics.Tools.Variance(values, false);
            var actualVar = actual.Variance;
            Assert.AreEqual(expectedVar, actualVar, 0.15);
        }

        [TestMethod()]
        public void FitTest2()
        {
            double[] coefficients = { 0.50, 0.50 };

            NormalDistribution[] components = new NormalDistribution[2];
            components[0] = new NormalDistribution(2, 1);
            components[1] = new NormalDistribution(5, 1);

            var target = new Mixture<NormalDistribution>(coefficients, components);

            double[] values =  { 12512, 1, 1, 0, 1, 6, 6, 5, 7, 5 };
            double[] weights = {     0, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            weights = weights.Divide(weights.Sum());
            double[] part1 = values.Submatrix(1, 4);
            double[] part2 = values.Submatrix(5, 9);

            MixtureOptions opt = new MixtureOptions()
            {
                Threshold = 0.000001
            };

            target.Fit(values, weights, opt);

            var mean1 = Accord.Statistics.Tools.Mean(part1);
            var var1 = Accord.Statistics.Tools.Variance(part1);
            Assert.AreEqual(mean1, target.Components[0].Mean, 1e-5);
            Assert.AreEqual(var1, target.Components[0].Variance, 1e-5);

            var mean2 = Accord.Statistics.Tools.Mean(part2);
            var var2 = Accord.Statistics.Tools.Variance(part2);
            Assert.AreEqual(mean2, target.Components[1].Mean, 1e-5);
            Assert.AreEqual(var2, target.Components[1].Variance, 1e-5);

            var expectedMean = Accord.Statistics.Tools.WeightedMean(values, weights);
            var actualMean = target.Mean;
            Assert.AreEqual(expectedMean, actualMean, 1e-5);
        }

        [TestMethod()]
        public void ProbabilityDensityFunction()
        {
            NormalDistribution[] components = new NormalDistribution[2];
            components[0] = new NormalDistribution(2, 1);
            components[1] = new NormalDistribution(5, 1);

            double[] coefficients = { 0.4, 0.5 };

            var mixture = new Mixture<NormalDistribution>(coefficients, components);

            double expected = 0.4 * components[0].ProbabilityDensityFunction(0.42) +
                              0.5 * components[1].ProbabilityDensityFunction(0.42);

            double actual = mixture.ProbabilityDensityFunction(0.42);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LogProbabilityDensityFunction()
        {
            NormalDistribution[] components = new NormalDistribution[2];
            components[0] = new NormalDistribution(2, 1);
            components[1] = new NormalDistribution(5, 1);

            double[] coefficients = { 0.4, 0.5 };

            var mixture = new Mixture<NormalDistribution>(coefficients, components);

            double expected = System.Math.Log(
                0.4 * components[0].ProbabilityDensityFunction(0.42) +
                0.5 * components[1].ProbabilityDensityFunction(0.42));

            double actual = mixture.LogProbabilityDensityFunction(0.42);

            Assert.AreEqual(expected, actual);
        }


    }
}
