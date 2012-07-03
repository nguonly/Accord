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

using Accord.Math.Optimization;
using Accord.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.Math.Differentiation;
namespace Accord.Tests.Math
{


    /// <summary>
    ///This is a test class for FiniteDifferencesTest and is intended
    ///to contain all FiniteDifferencesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FiniteDifferencesTest
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
        ///A test for Compute
        ///</summary>
        [TestMethod()]
        public void ComputeTest()
        {
            int numberOfParameters = 2;
            FiniteDifferences target = new FiniteDifferences(numberOfParameters);



            double[] inputs = { -1, 0.4 };

            target.Function = BroydenFletcherGoldfarbShannoTest.rosenbrockFunction;

            double[] expected = BroydenFletcherGoldfarbShannoTest.rosenbrockGradient(inputs);
            double[] actual = target.Compute(inputs);

            Assert.IsTrue(expected.IsEqual(actual, 0.05));
        }


    }
}
