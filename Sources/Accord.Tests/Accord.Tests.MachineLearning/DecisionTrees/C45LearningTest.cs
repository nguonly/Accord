using Accord.MachineLearning.DecisionTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Accord.Statistics.Filters;
using Accord.Math;
using System.Data;
using Accord.MachineLearning.DecisionTrees.Learning;

namespace Accord.Tests.MachineLearning
{


    /// <summary>
    ///This is a test class for IterativeDichotomizer3Test and is intended
    ///to contain all IterativeDichotomizer3Test Unit Tests
    ///</summary>
    [TestClass()]
    public class C45LearningTest
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


        public static void CreateMitchellExample(out DecisionTree tree, out double[][] inputs, out int[] outputs)
        {
            DataTable data = new DataTable("Mitchell's Tennis Example");

            data.Columns.Add("Day", typeof(string));
            data.Columns.Add("Outlook", typeof(string));
            data.Columns.Add("Temperature", typeof(double));
            data.Columns.Add("Humidity", typeof(double));
            data.Columns.Add("Wind", typeof(string));
            data.Columns.Add("PlayTennis", typeof(string));

            data.Rows.Add("D1", "Sunny",     85, 85, "Weak", "No");
            data.Rows.Add("D2", "Sunny",     80, 90, "Strong", "No");
            data.Rows.Add("D3", "Overcast",  83, 78, "Weak", "Yes");
            data.Rows.Add("D4", "Rain",      70, 96, "Weak", "Yes");
            data.Rows.Add("D5", "Rain",      68, 80, "Weak", "Yes");
            data.Rows.Add("D6", "Rain",      65, 70, "Strong", "No");
            data.Rows.Add("D7", "Overcast",  64, 65, "Strong", "Yes");
            data.Rows.Add("D8", "Sunny",     72, 95, "Weak", "No");
            data.Rows.Add("D9", "Sunny",     69, 70, "Weak", "Yes");
            data.Rows.Add("D10", "Rain",     75, 80, "Weak", "Yes");
            data.Rows.Add("D11", "Sunny",    75, 70, "Strong", "Yes");
            data.Rows.Add("D12", "Overcast", 72, 90, "Strong", "Yes");
            data.Rows.Add("D13", "Overcast", 81, 75, "Weak", "Yes");
            data.Rows.Add("D14", "Rain",     71, 80, "Strong", "No");

            // Create a new codification codebook to
            // convert strings into integer symbols
            Codification codebook = new Codification(data);

            DecisionVariable[] attributes =
            {
               new DecisionVariable("Outlook",     codebook["Outlook"].Symbols),      // 3 possible values (Sunny, overcast, rain)
               new DecisionVariable("Temperature", DecisionAttributeKind.Continuous), // continuous values
               new DecisionVariable("Humidity",    DecisionAttributeKind.Continuous), // continuous values
               new DecisionVariable("Wind",        codebook["Wind"].Symbols)          // 2 possible values (Weak, strong)
            };

            int classCount = codebook["PlayTennis"].Symbols; // 2 possible values (yes, no)

            tree = new DecisionTree(attributes, classCount);
            C45Learning c45 = new C45Learning(tree);

            // Extract symbols from data and train the classifier
            DataTable symbols = codebook.Apply(data);
            inputs = symbols.ToArray("Outlook", "Temperature", "Humidity", "Wind");
            outputs = symbols.ToIntArray("PlayTennis").GetColumn(0);

            double error = c45.Run(inputs, outputs);
        }


        [TestMethod()]
        public void RunTest()
        {
            DecisionTree tree;
            double[][] inputs;
            int[] outputs;

            CreateMitchellExample(out tree, out inputs, out outputs);

            Assert.AreEqual(1, tree.Root.Branches.AttributeIndex); // Temperature
            Assert.AreEqual(2, tree.Root.Branches.Count);
            Assert.IsNull(tree.Root.Output);
            Assert.IsNull(tree.Root.Value);

            Assert.AreEqual(84, tree.Root.Branches[0].Value); // Temperature <= 84.0
            Assert.AreEqual(2, tree.Root.Branches[0].Branches.AttributeIndex); // Decide over Humidity
            Assert.AreEqual(ComparisonKind.LessThanOrEqual, tree.Root.Branches[0].Comparison);
            Assert.AreEqual(2, tree.Root.Branches[0].Branches.Count);
            Assert.IsFalse(tree.Root.Branches[0].Branches[0].IsLeaf);
            Assert.IsFalse(tree.Root.Branches[0].Branches[1].IsLeaf);

            Assert.AreEqual(84, tree.Root.Branches[1].Value); // Temperature > 84.0
            Assert.AreEqual(0, tree.Root.Branches[1].Output.Value); // Output is "No"
            Assert.AreEqual(ComparisonKind.GreaterThan, tree.Root.Branches[1].Comparison);
            Assert.IsNull(tree.Root.Branches[1].Branches);
            Assert.IsTrue(tree.Root.Branches[1].IsLeaf);

            Assert.AreEqual(80, tree.Root.Branches[0].Branches[0].Value); // Humidity <= 80
            Assert.AreEqual(ComparisonKind.LessThanOrEqual, tree.Root.Branches[0].Branches[0].Comparison);
            Assert.AreEqual(2, tree.Root.Branches[0].Branches.Count);
            Assert.AreEqual(3, tree.Root.Branches[0].Branches[0].Branches.AttributeIndex); // Decide over Wind
            Assert.AreEqual(0, tree.Root.Branches[0].Branches[0].Branches[0].Value);
            Assert.AreEqual(ComparisonKind.Equal, tree.Root.Branches[0].Branches[0].Branches[0].Comparison);
            Assert.AreEqual(ComparisonKind.Equal, tree.Root.Branches[0].Branches[0].Branches[1].Comparison);


        }

    }
}
