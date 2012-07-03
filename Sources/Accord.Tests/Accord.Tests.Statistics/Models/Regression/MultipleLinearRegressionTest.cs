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

using Accord.Statistics.Models.Regression.Linear;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Accord.Tests.Statistics
{


    /// <summary>
    ///This is a test class for MultipleLinearRegressionTest and is intended
    ///to contain all MultipleLinearRegressionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MultipleLinearRegressionTest
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


        /// <summary>
        ///A test for Regress
        ///</summary>
        [TestMethod()]
        public void RegressTest()
        {
            MultipleLinearRegression target = new MultipleLinearRegression(1, true);

            double[][] inputs = 
            {
                new double[] { 80 },
                new double[] { 60 },
                new double[] { 10 },
                new double[] { 20 },
                new double[] { 30 },
            };

            double[] outputs = { 20, 40, 30, 50, 60 };


            double error = target.Regress(inputs, outputs);

            double slope = target.Coefficients[0];
            double intercept = target.Coefficients[1];

            Assert.AreEqual(-0.264706, slope, 1e-5);
            Assert.AreEqual(50.588235, intercept, 1e-5);
            Assert.AreEqual(761.764705, error, 1e-5);


            double r = target.CoefficientOfDetermination(inputs, outputs);
            Assert.AreEqual(0.23823529, r, 1e-6);

            string str = target.ToString();

            Assert.AreEqual("y(x0) = -0,264705882352942*x0 + 50,5882352941177", str);
        }

        /// <summary>
        ///A test for Regress
        ///</summary>
        [TestMethod()]
        public void RegressTest2()
        {
            MultipleLinearRegression target = new MultipleLinearRegression(1, false);

            double[][] inputs = 
            {
                new double[] { 80, 1 },
                new double[] { 60, 1 },
                new double[] { 10, 1 },
                new double[] { 20, 1 },
                new double[] { 30, 1 },
            };

            double[] outputs = { 20, 40, 30, 50, 60 };


            double error = target.Regress(inputs, outputs);

            double slope = target.Coefficients[0];
            double intercept = target.Coefficients[1];

            Assert.AreEqual(-0.264706, slope, 1e-5);
            Assert.AreEqual(50.588235, intercept, 1e-5);
            Assert.AreEqual(761.764705, error, 1e-5);


            double r = target.CoefficientOfDetermination(inputs, outputs);
            Assert.AreEqual(0.23823529, r, 1e-6);

            string str = target.ToString();

            Assert.AreEqual("y(x0, x1) = -0,264705882352942*x0 + 50,5882352941177*x1", str);
        }

        /// <summary>
        ///A test for Regress
        ///</summary>
        [TestMethod()]
        public void RegressTest3()
        {
            // We will try to model a plane as an equation in the form
            // "ax + by + c = z". We have two input variables (x and y)
            // and we will be trying to find two parameters a and b and 
            // an intercept term c.

            // Create a multiple linear regression for two input and an intercept
            MultipleLinearRegression target = new MultipleLinearRegression(2, true);

            // Now suppose you have some points
            double[][] inputs = 
            {
                new double[] { 1, 1 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 0, 0 },
            };

            // located in the same Z (z = 1)
            double[] outputs = { 1, 1, 1, 1 };


            // Now we will try to fit a regression model
            double error = target.Regress(inputs, outputs);

            // As result, we will be given the following:
            double a = target.Coefficients[0]; // a = 0
            double b = target.Coefficients[1]; // b = 0
            double c = target.Coefficients[2]; // c = 1

            // This is the plane described by the equation
            // ax + by + c = z => 0x + 0y + 1 = z => 1 = z.


            Assert.AreEqual(0.0, a, 1e-6);
            Assert.AreEqual(0.0, b, 1e-6);
            Assert.AreEqual(1.0, c, 1e-6);
            Assert.AreEqual(0.0, error, 1e-6);


            double r = target.CoefficientOfDetermination(inputs, outputs);
            Assert.AreEqual(1.0, r);

        }
    }
}
