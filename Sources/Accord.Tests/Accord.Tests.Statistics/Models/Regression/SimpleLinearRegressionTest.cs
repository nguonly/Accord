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
    using Accord.Statistics.Models.Regression.Linear;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///This is a test class for SimpleLinearRegressionTest and is intended
    ///to contain all SimpleLinearRegressionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SimpleLinearRegressionTest
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
            // Let's say we have some univariate, continuous sets of input data,
            // and a corresponding univariate, continuous set of output data, such
            // as a set of points in R². A simple linear regression is able to fit
            // a line relating the input variables to the output variables in which
            // the minimum-squared-error of the line and the actual output points
            // is minimum.

            // Declare some sample test data.
            double[] inputs =  { 80, 60, 10, 20, 30 };
            double[] outputs = { 20, 40, 30, 50, 60 };

            // Create a new simple linear regression
            SimpleLinearRegression regression = new SimpleLinearRegression();

            // Compute the linear regression
            regression.Regress(inputs, outputs);

            // Compute the output for a given input. The
            double y = regression.Compute(85); // The answer will be 28.088

            // We can also extract the slope and the intercept term
            // for the line. Those will be -0.26 and 50.5, respectively.
            double s = regression.Slope;
            double c = regression.Intercept;

            // Expected slope and intercept
            double eSlope = -0.264706;
            double eIntercept = 50.588235;

            Assert.AreEqual(28.0882352941192, y);
            Assert.AreEqual(eSlope, s, 0.0001);
            Assert.AreEqual(eIntercept, c, 0.0001);
        }
    }
}
