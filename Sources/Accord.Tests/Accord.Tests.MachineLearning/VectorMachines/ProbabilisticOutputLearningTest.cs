

namespace Accord.Tests.MachineLearning
{
    using Accord.MachineLearning.VectorMachines.Learning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Accord.MachineLearning.VectorMachines;
    using Accord.Statistics.Kernels;


    [TestClass()]
    public class ProbabilisticOutputLearningTest
    {


        private TestContext testContextInstance;

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
        public void RunTest1()
        {
            double[][] inputs = 
            {
			    new double[] { -1, -1 },
			    new double[] { -1,  1 },
			    new double[] {  1, -1 },
			    new double[] {  1,  1 }
			};

            int[] outputs =
            {
				 -1,
				  1,
				  1,
				 -1 
			};

            KernelSupportVectorMachine svm = new KernelSupportVectorMachine(new Gaussian(3.6), 2);

            SequentialMinimalOptimization smo = new SequentialMinimalOptimization(svm, inputs, outputs);

            double error1 = smo.Run();

            Assert.AreEqual(0, error1);

            double[] distances = new double[outputs.Length];
            for (int i = 0; i < outputs.Length; i++)
            {
                int y = svm.Compute(inputs[i], out distances[i]);
                Assert.AreEqual(outputs[i], y);
            }


            ProbabilisticOutputLearning target = new ProbabilisticOutputLearning(svm, inputs, outputs);

            double ll0 = target.LogLikelihood(inputs, outputs);

            double ll1 = target.Run();

            double ll2 = target.LogLikelihood(inputs, outputs);

            Assert.AreEqual(3.4256203116918824, ll1);
            Assert.AreEqual(ll1, ll2);
            Assert.IsTrue(ll1 > ll0);

            double[] probs = new double[outputs.Length];
            for (int i = 0; i < outputs.Length; i++)
            {
                int y = svm.Compute(inputs[i], out probs[i]);
                Assert.AreEqual(outputs[i], y);
            }

            Assert.AreEqual(0.25, probs[0], 1e-5);
            Assert.AreEqual(0.75, probs[1], 1e-5);
            Assert.AreEqual(0.75, probs[2], 1e-5);
            Assert.AreEqual(0.25, probs[3], 1e-5);

            foreach (var p in probs)
                Assert.IsFalse(Double.IsNaN(p));

        }


        [TestMethod()]
        public void RunTest2()
        {
            double[][] inputs =
            {
                new double[] { 0, 1, 1, 0 }, // 0
                new double[] { 0, 1, 0, 0 }, // 0
                new double[] { 0, 0, 1, 0 }, // 0
                new double[] { 0, 1, 1, 0 }, // 0
                new double[] { 0, 1, 0, 0 }, // 0
                new double[] { 1, 0, 0, 0 }, // 1
                new double[] { 1, 0, 0, 0 }, // 1
                new double[] { 1, 0, 0, 1 }, // 1
                new double[] { 0, 0, 0, 1 }, // 1
                new double[] { 0, 0, 0, 1 }, // 1
                new double[] { 1, 1, 1, 1 }, // 2
                new double[] { 1, 0, 1, 1 }, // 2
                new double[] { 1, 1, 0, 1 }, // 2
                new double[] { 0, 1, 1, 1 }, // 2
                new double[] { 1, 1, 1, 1 }, // 2
            };

            int[] outputs =
            {
                0, 0, 0, 0, 0,
                1, 1, 1, 1, 1,
                2, 2, 2, 2, 2,
            };

            IKernel kernel = new Linear();
            MulticlassSupportVectorMachine machine = new MulticlassSupportVectorMachine(4, kernel, 3);
            MulticlassSupportVectorLearning target = new MulticlassSupportVectorLearning(machine, inputs, outputs);

            target.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            double error1 = target.Run();
            Assert.AreEqual(0, error1);

            target.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new ProbabilisticOutputLearning(svm, classInputs, classOutputs);

            double error2 = target.Run();
            Assert.AreEqual(0, error2);


        }


    }
}
