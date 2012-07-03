using Accord.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accord.Tests.Math
{
    
    
    /// <summary>
    ///This is a test class for ReducedRowEchelonFormTest and is intended
    ///to contain all ReducedRowEchelonFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReducedRowEchelonFormTest
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
        public void ReducedRowEchelonFormConstructorTest()
        {
            double[,] matrix = 
            {
                { 1, 2, -3 },
                { 3, 5,  9 },
                { 5, 9,  3 },
            };

            ReducedRowEchelonForm target = new ReducedRowEchelonForm(matrix);

            var actual = target.Result;
            double[,] expected = 
            {
                { 1, 0,  33 },
                { 0, 1, -18 },
                { 0, 0,   0 },
            };


            Assert.IsTrue(expected.IsEqual(actual));
        }

        [TestMethod()]
        public void ReducedRowEchelonFormConstructorTest2()
        {
            double[,] matrix = 
            {
                {3,2,2,3,1},
                {6,4,4,6,2},
                {9,6,6,9,1},
            };

            ReducedRowEchelonForm target = new ReducedRowEchelonForm(matrix);

            var actual = target.Result;

            double[,] expected = 
            {
                { 1, 2/3.0,  2/3.0,   1,   0   },
                { 0,     0,      0,   0,   1   },
                { 0,     0,      0,   0,   0   },
            };


            Assert.IsTrue(expected.IsEqual(actual));
        }
   
    }
}
