using Accord.MachineLearning.DecisionTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Text;
using Accord.Math;
using System.Reflection;
using System.Reflection.Emit;

namespace Accord.Tests.MachineLearning
{


    /// <summary>
    ///This is a test class for DecisionTreeCompilerTest and is intended
    ///to contain all DecisionTreeCompilerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DecisionTreeCompilerTest
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
        public void CreateTest()
        {
            DecisionTree tree;
            int[][] inputs;
            int[] outputs;

            ID3LearningTest.CreateMitchellExample(out tree, out inputs, out outputs);

            // Convert to an expression tree
            var expression = tree.ToExpression();

            // Compiles the expression
            var func = expression.Compile();


            for (int i = 0; i < inputs.Length; i++)
            {
                int y = func(inputs[i].ToDouble());
                Assert.AreEqual(outputs[i], y);
            }

        }
    }
}
