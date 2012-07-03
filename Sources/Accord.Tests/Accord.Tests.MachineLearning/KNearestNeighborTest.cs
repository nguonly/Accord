using Accord.MachineLearning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accord.Tests.MachineLearning
{
    
    
    /// <summary>
    ///This is a test class for KNearestNeighborTest and is intended
    ///to contain all KNearestNeighborTest Unit Tests
    ///</summary>
    [TestClass()]
    public class KNearestNeighborTest
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
        public void KNearestNeighborConstructorTest()
        {
            double[][] inputs = 
            {
                new double[] { -5, -2, -1 },
                new double[] { -5, -5, -6 },

                new double[] {  2,  1,  1 },
                new double[] {  1,  1,  2 },
                new double[] {  1,  2,  2 },
                new double[] {  3,  1,  2 },

                new double[] { 11,  5,  4 },
                new double[] { 15,  5,  6 },
                new double[] { 10,  5,  6 },
            };

            int[] outputs =
            {
                0, 0,
                1, 1, 1, 1,
                2, 2, 2
            };

            int k = 3;
           
            KNearestNeighbor target = new KNearestNeighbor(k, inputs, outputs);

            for (int i = 0; i < inputs.Length; i++)
            {
                int actual = target.Compute(inputs[i]);
                int expected = outputs[i];

                Assert.AreEqual(expected, actual);
            }

            double[][] test = 
            {
                new double[] { -4, -3, -1 },
                new double[] { -5, -4, -4 },

                new double[] {  5,  3,  4 },
                new double[] {  3,  1,  6 },

                new double[] { 10,  5,  4 },
                new double[] { 13,  4,  5 },
            };

            int[] expectedOutputs =
            {
                0, 0,
                1, 1,
                2, 2,
            };

            for (int i = 0; i < test.Length; i++)
            {
                int actual = target.Compute(test[i]);
                int expected = expectedOutputs[i];

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
