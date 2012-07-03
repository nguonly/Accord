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
    public class ID3LearningTest
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


        public static void CreateMitchellExample(out DecisionTree tree, out int[][] inputs, out int[] outputs)
        {
            DataTable data = new DataTable("Mitchell's Tennis Example");

            data.Columns.Add("Day", "Outlook", "Temperature", "Humidity", "Wind", "PlayTennis");

            data.Rows.Add("D1", "Sunny",     "Hot",  "High",   "Weak",   "No");
            data.Rows.Add("D2", "Sunny",     "Hot",  "High",   "Strong", "No");
            data.Rows.Add("D3", "Overcast",  "Hot",  "High",   "Weak",   "Yes");
            data.Rows.Add("D4", "Rain",      "Mild", "High",   "Weak",   "Yes");
            data.Rows.Add("D5", "Rain",      "Cool", "Normal", "Weak",   "Yes");
            data.Rows.Add("D6", "Rain",      "Cool", "Normal", "Strong", "No");
            data.Rows.Add("D7", "Overcast",  "Cool", "Normal", "Strong", "Yes");
            data.Rows.Add("D8", "Sunny",     "Mild", "High",   "Weak",   "No");
            data.Rows.Add("D9", "Sunny",     "Cool", "Normal", "Weak",   "Yes");
            data.Rows.Add("D10", "Rain",     "Mild", "Normal", "Weak",   "Yes");
            data.Rows.Add("D11", "Sunny",    "Mild", "Normal", "Strong", "Yes");
            data.Rows.Add("D12", "Overcast", "Mild", "High",   "Strong", "Yes");
            data.Rows.Add("D13", "Overcast", "Hot",  "Normal", "Weak",   "Yes");
            data.Rows.Add("D14", "Rain",     "Mild", "High",   "Strong", "No");

            // Create a new codification codebook to
            // convert strings into integer symbols
            Codification codebook = new Codification(data);

            DecisionVariable[] attributes =
            {
               new DecisionVariable("Outlook",     codebook["Outlook"].Symbols),     // 3 possible values (Sunny, overcast, rain)
               new DecisionVariable("Temperature", codebook["Temperature"].Symbols), // 3 possible values (Hot, mild, cool)
               new DecisionVariable("Humidity",    codebook["Humidity"].Symbols),    // 2 possible values (High, normal)
               new DecisionVariable("Wind",        codebook["Wind"].Symbols)         // 2 possible values (Weak, strong)
            };

            int classCount = codebook["PlayTennis"].Symbols; // 2 possible values (yes, no)

            tree = new DecisionTree(attributes, classCount);
            ID3Learning id3 = new ID3Learning(tree);

            // Extract symbols from data and train the classifier
            DataTable symbols = codebook.Apply(data);
            inputs = symbols.ToIntArray("Outlook", "Temperature", "Humidity", "Wind");
            outputs = symbols.ToIntArray("PlayTennis").GetColumn(0);

            id3.Run(inputs, outputs);
        }

        public static void CreateXORExample(out DecisionTree tree, out int[][] inputs, out int[] outputs)
        {
            inputs = new int[][]
            {
                new int[] { 1, 0, 0, 1 },
                new int[] { 0, 1, 0, 0 },
                new int[] { 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0 },
                new int[] { 0, 1, 1, 1 },
                new int[] { 0, 0, 1, 1 },
                new int[] { 1, 0, 1, 1 }
            };
            
            outputs = new int[]
            {
                1, 1, 0, 0, 1, 0, 1
            };

            DecisionVariable[] attributes =
            {
               new DecisionVariable("a1", 2), 
               new DecisionVariable("a2", 2), 
               new DecisionVariable("a3", 2), 
               new DecisionVariable("a4", 2)  
            };

            int classCount = 2;

            tree = new DecisionTree(attributes, classCount);
            ID3Learning id3 = new ID3Learning(tree);


            double error = id3.Run(inputs, outputs);
        }

        [TestMethod()]
        public void RunTest()
        {
            int[][] inputs =
            {
                new int[] { 0, 0 },
                new int[] { 0, 1 },
                new int[] { 1, 0 },
                new int[] { 1, 1 },
            };

            int[] outputs = // xor
            {
                0,
                1,
                1,
                0
            };

            DecisionVariable[] attributes = 
            {
                new DecisionVariable("x", DecisionAttributeKind.Discrete),
                new DecisionVariable("y", DecisionAttributeKind.Discrete),
            };


            DecisionTree tree = new DecisionTree(attributes, 2);

            ID3Learning teacher = new ID3Learning(tree);

            double error = teacher.Run(inputs, outputs);

            Assert.AreEqual(0, error);

            Assert.AreEqual(0, tree.Root.Branches.AttributeIndex); // x
            Assert.AreEqual(2, tree.Root.Branches.Count);
            Assert.IsNull(tree.Root.Value);
            Assert.IsNull(tree.Root.Value);

            Assert.AreEqual(0.0, tree.Root.Branches[0].Value); // x = [0]
            Assert.AreEqual(1.0, tree.Root.Branches[1].Value); // x = [1]

            Assert.AreEqual(tree.Root, tree.Root.Branches[0].Parent);
            Assert.AreEqual(tree.Root, tree.Root.Branches[1].Parent);

            Assert.AreEqual(2, tree.Root.Branches[0].Branches.Count);
            Assert.AreEqual(2, tree.Root.Branches[1].Branches.Count);

            Assert.IsTrue(tree.Root.Branches[0].Branches[0].IsLeaf);
            Assert.IsTrue(tree.Root.Branches[0].Branches[1].IsLeaf);

            Assert.IsTrue(tree.Root.Branches[1].Branches[0].IsLeaf);
            Assert.IsTrue(tree.Root.Branches[1].Branches[1].IsLeaf);

            Assert.AreEqual(0.0, tree.Root.Branches[0].Branches[0].Value); // y = [0]
            Assert.AreEqual(1.0, tree.Root.Branches[0].Branches[1].Value); // y = [1]

            Assert.AreEqual(0.0, tree.Root.Branches[1].Branches[0].Value); // y = [0]
            Assert.AreEqual(1.0, tree.Root.Branches[1].Branches[1].Value); // y = [1]

            Assert.AreEqual(0, tree.Root.Branches[0].Branches[0].Output); // 0 ^ 0 = 0
            Assert.AreEqual(1, tree.Root.Branches[0].Branches[1].Output); // 0 ^ 1 = 1
            Assert.AreEqual(1, tree.Root.Branches[1].Branches[0].Output); // 1 ^ 0 = 1
            Assert.AreEqual(0, tree.Root.Branches[1].Branches[1].Output); // 1 ^ 1 = 0
        }

        [TestMethod()]
        public void RunTest2()
        {
            DecisionTree tree;
            int[][] inputs;
            int[] outputs;

            CreateMitchellExample(out tree, out inputs, out outputs);

            Assert.AreEqual(0, tree.Root.Branches.AttributeIndex); // Outlook
            Assert.AreEqual(3, tree.Root.Branches.Count);
            Assert.IsNull(tree.Root.Output);
            Assert.IsNull(tree.Root.Value);

            Assert.AreEqual(0, tree.Root.Branches[0].Value); // Outlook = Sunny
            Assert.AreEqual(2, tree.Root.Branches[0].Branches.AttributeIndex); // Decide over Humidity
            Assert.AreEqual(2, tree.Root.Branches[0].Branches.Count);
            Assert.IsTrue(tree.Root.Branches[0].Branches[0].IsLeaf);
            Assert.IsTrue(tree.Root.Branches[0].Branches[1].IsLeaf);

            Assert.AreEqual(1, tree.Root.Branches[1].Value); // Outlook = Overcast
            Assert.IsNull(tree.Root.Branches[1].Branches);
            Assert.IsTrue(tree.Root.Branches[1].IsLeaf);

            Assert.AreEqual(2, tree.Root.Branches[2].Value); // Outlook = Rain
            Assert.AreEqual(3, tree.Root.Branches[2].Branches.AttributeIndex); // Decide over Wind
            Assert.AreEqual(2, tree.Root.Branches[2].Branches.Count);
            Assert.IsTrue(tree.Root.Branches[2].Branches[0].IsLeaf);
            Assert.IsTrue(tree.Root.Branches[2].Branches[1].IsLeaf);

            Assert.AreEqual(0, tree.Root.Branches[0].Branches[0].Value); // Humidity = High
            Assert.IsTrue(tree.Root.Branches[0].Branches[0].IsLeaf);

            Assert.AreEqual(1, tree.Root.Branches[0].Branches[1].Value); // Humidity = Normal
            Assert.IsTrue(tree.Root.Branches[0].Branches[1].IsLeaf);

            Assert.AreEqual(0, tree.Root.Branches[2].Branches[0].Value); // Wind = Weak
            Assert.IsTrue(tree.Root.Branches[2].Branches[0].IsLeaf);

            Assert.AreEqual(1, tree.Root.Branches[2].Branches[1].Value); // Wind = Strong
            Assert.IsTrue(tree.Root.Branches[2].Branches[1].IsLeaf);
        }

        [TestMethod()]
        public void RunTest3()
        {
            DecisionTree tree;
            int[][] inputs;
            int[] outputs;

            CreateXORExample(out tree, out inputs, out outputs);

            Assert.AreEqual(3, tree.Root.Branches.AttributeIndex); // a4
            Assert.AreEqual(2, tree.Root.Branches.Count);
            Assert.IsNull(tree.Root.Output);
            Assert.IsNull(tree.Root.Value);

            Assert.AreEqual(0, tree.Root.Branches[0].Value); // a4 = 0
            Assert.AreEqual(0, tree.Root.Branches[0].Branches.AttributeIndex); // Decide over a1
            Assert.AreEqual(2, tree.Root.Branches[0].Branches.Count);
            Assert.IsFalse(tree.Root.Branches[0].Branches[0].IsLeaf);
            Assert.IsTrue(tree.Root.Branches[0].Branches[1].IsLeaf);
            Assert.AreEqual(0, tree.Root.Branches[0].Branches[1].Output);

            Assert.AreEqual(1, tree.Root.Branches[1].Value); // a4 = 1
            Assert.AreEqual(0, tree.Root.Branches[1].Branches.AttributeIndex); // Decide over a1
            Assert.AreEqual(2, tree.Root.Branches[1].Branches.Count);
            Assert.IsFalse(tree.Root.Branches[1].Branches[0].IsLeaf);
            Assert.IsTrue(tree.Root.Branches[1].Branches[1].IsLeaf);
            Assert.AreEqual(1, tree.Root.Branches[1].Branches[1].Output);

            Assert.AreEqual(0, tree.Root.Branches[0].Branches[0].Value); // a1 = 0
            Assert.AreEqual(1, tree.Root.Branches[0].Branches[0].Branches.AttributeIndex); // Decide over a2
            Assert.AreEqual(2, tree.Root.Branches[0].Branches[0].Branches.Count);
            Assert.IsTrue(tree.Root.Branches[0].Branches[0].Branches[0].IsLeaf);
            Assert.IsTrue(tree.Root.Branches[0].Branches[0].Branches[1].IsLeaf);
        }

    }
}
