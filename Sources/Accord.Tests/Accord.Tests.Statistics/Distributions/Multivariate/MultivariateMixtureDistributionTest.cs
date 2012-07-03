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

using Accord.Statistics.Distributions.Multivariate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.Math;
namespace Accord.Tests.Statistics
{


    /// <summary>
    ///This is a test class for MixtureDistributionTest and is intended
    ///to contain all MixtureDistributionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MultivariateMixtureDistributionTest
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
            MultivariateNormalDistribution[] components = new MultivariateNormalDistribution[2];
            components[0] = new MultivariateNormalDistribution(new double[] { 2 }, new double[,] { { 1 } });
            components[1] = new MultivariateNormalDistribution(new double[] { 5 }, new double[,] { { 1 } });

            var mixture = new MultivariateMixture<MultivariateNormalDistribution>(components);

            double[] expected = { 0.5, 0.5 };

            Assert.IsTrue(expected.IsEqual(mixture.Coefficients));
            Assert.AreEqual(components, mixture.Components);
        }

        [TestMethod()]
        public void ProbabilityDensityFunctionTest()
        {
            MultivariateNormalDistribution[] components = new MultivariateNormalDistribution[2];
            components[0] = new MultivariateNormalDistribution(new double[] { 2 }, new double[,] { { 1 } });
            components[1] = new MultivariateNormalDistribution(new double[] { 5 }, new double[,] { { 1 } });

            double[] coefficients = { 0.3, 0.7 };
            var mixture = new MultivariateMixture<MultivariateNormalDistribution>(coefficients, components);

            double[] x = { 1.2 };

            double expected =
                0.3 * components[0].ProbabilityDensityFunction(x) +
                0.7 * components[1].ProbabilityDensityFunction(x);

            double actual = mixture.ProbabilityDensityFunction(x);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LogProbabilityDensityFunctionTest()
        {
            MultivariateNormalDistribution[] components = new MultivariateNormalDistribution[2];
            components[0] = new MultivariateNormalDistribution(new double[] { 2 }, new double[,] { { 1 } });
            components[1] = new MultivariateNormalDistribution(new double[] { 5 }, new double[,] { { 1 } });

            double[] coefficients = { 0.3, 0.7 };
            var mixture = new MultivariateMixture<MultivariateNormalDistribution>(coefficients, components);

            double[] x = { 1.2 };

            double expected = System.Math.Log(
                0.3 * components[0].ProbabilityDensityFunction(x) +
                0.7 * components[1].ProbabilityDensityFunction(x));

            double actual = mixture.LogProbabilityDensityFunction(x);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FitTest()
        {
            double[] coefficients = { 0.50, 0.50 };

            MultivariateNormalDistribution[] components = new MultivariateNormalDistribution[2];
            components[0] = new MultivariateNormalDistribution(new double[] { 2 }, new double[,] { { 1 } });
            components[1] = new MultivariateNormalDistribution(new double[] { 5 }, new double[,] { { 1 } });

            var target = new MultivariateMixture<MultivariateNormalDistribution>(coefficients, components);

            double[][] values = { new double[] { 0 },
                                  new double[] { 1 }, 
                                  new double[] { 1 },
                                  new double[] { 0 },
                                  new double[] { 1 },
                                  new double[] { 6 },
                                  new double[] { 6 },
                                  new double[] { 5 },
                                  new double[] { 7 },
                                  new double[] { 5 } };

            double[][] part1 = values.Submatrix(0, 4);
            double[][] part2 = values.Submatrix(5, 9);



            target.Fit(values);


            var mean1 = Accord.Statistics.Tools.Mean(part1);
            var var1 = Accord.Statistics.Tools.Variance(part1);
            Assert.AreEqual(mean1[0], target.Components[0].Mean[0], 1e-5);
            Assert.AreEqual(var1[0], target.Components[0].Variance[0], 1e-5);

            var mean2 = Accord.Statistics.Tools.Mean(part2);
            var var2 = Accord.Statistics.Tools.Variance(part2);
            Assert.AreEqual(mean2[0], target.Components[1].Mean[0], 1e-5);
            Assert.AreEqual(var2[0], target.Components[1].Variance[0], 1e-5);


            var expectedMean = Accord.Statistics.Tools.Mean(values);
            var expectedVar = Accord.Statistics.Tools.Covariance(values);

            var actualMean = target.Mean;
            var actualVar = target.Covariance;

            Assert.AreEqual(expectedMean[0], actualMean[0], 0.0000001);
            // Assert.AreEqual(expectedVar[0, 0], actualVar[0, 0], 0.0000001);
        }

        [TestMethod()]
        public void FitTest2()
        {
            double[] coefficients = { 0.50, 0.50 };

            MultivariateNormalDistribution[] components = new MultivariateNormalDistribution[2];
            components[0] = new MultivariateNormalDistribution(new double[] { 2 }, new double[,] { { 1 } });
            components[1] = new MultivariateNormalDistribution(new double[] { 5 }, new double[,] { { 1 } });

            var target = new MultivariateMixture<MultivariateNormalDistribution>(coefficients, components);

            double[][] values = { new double[] { 2512512312 },
                                  new double[] { 1 }, 
                                  new double[] { 1 },
                                  new double[] { 0 },
                                  new double[] { 1 },
                                  new double[] { 6 },
                                  new double[] { 6 },
                                  new double[] { 5 },
                                  new double[] { 7 },
                                  new double[] { 5 } };

            double[] weights = { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            weights = weights.Divide(weights.Sum());

            double[][] part1 = values.Submatrix(1, 4);
            double[][] part2 = values.Submatrix(5, 9);


            target.Fit(values, weights);

            var mean1 = Accord.Statistics.Tools.Mean(part1);
            var var1 = Accord.Statistics.Tools.Variance(part1);
            Assert.AreEqual(mean1[0], target.Components[0].Mean[0], 1e-5);
            Assert.AreEqual(var1[0], target.Components[0].Variance[0], 1e-5);

            var mean2 = Accord.Statistics.Tools.Mean(part2);
            var var2 = Accord.Statistics.Tools.Variance(part2);
            Assert.AreEqual(mean2[0], target.Components[1].Mean[0], 1e-5);
            Assert.AreEqual(var2[0], target.Components[1].Variance[0], 1e-5);


            var expectedMean = Accord.Statistics.Tools.WeightedMean(values, weights);
            var expectedVar = Accord.Statistics.Tools.WeightedCovariance(values, weights);

            var actualMean = target.Mean;
            var actualVar = target.Covariance;

            Assert.AreEqual(expectedMean[0], actualMean[0], 0.0000001);
            Assert.AreEqual(expectedVar[0, 0], actualVar[0, 0], 0.68);
        }

    }
}
