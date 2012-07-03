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

namespace Accord.Tests.MachineLearning
{

    using Accord.MachineLearning.VectorMachines;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Accord.Statistics.Kernels;
    using Accord.MachineLearning.VectorMachines.Learning;
    using System.IO;
    using Accord.Math;

    [TestClass()]
    public class MulticlassSupportVectorMachineTest
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
        public void MulticlassSupportVectorMachineConstructorTest()
        {
            int inputs = 1;
            IKernel kernel = new Linear();
            int classes = 4;
            var target = new MulticlassSupportVectorMachine(inputs, kernel, classes);

            Assert.AreEqual(3, target.Machines.Length);
            Assert.AreEqual(classes * (classes - 1) / 2, target.Machines[0].Length + target.Machines[1].Length + target.Machines[2].Length);

            for (int i = 0; i < classes; i++)
            {
                for (int j = 0; j < classes; j++)
                {
                    if (i == j) continue;

                    var machine = target[i, j];
                    Assert.IsNotNull(machine);
                }
            }
        }

        [TestMethod()]
        public void MulticlassSupportVectorMachineConstructorTest2()
        {
            int inputs = 1;
            int classes = 3;

            IKernel kernel = new Linear();

            var target = new MulticlassSupportVectorMachine(inputs, kernel, classes);

            target[0, 1].Kernel = new Gaussian(0.1);
            target[0, 2].Kernel = new Linear();
            target[1, 2].Kernel = new Polynomial(2);

            Assert.AreEqual(target[0, 0], target[0, 0]);
            Assert.AreEqual(target[1, 1], target[1, 1]);
            Assert.AreEqual(target[2, 2], target[2, 2]);
            Assert.AreEqual(target[0, 1], target[1, 0]);
            Assert.AreEqual(target[0, 2], target[0, 2]);
            Assert.AreEqual(target[1, 2], target[1, 2]);

            Assert.AreNotEqual(target[0, 1], target[0, 2]);
            Assert.AreNotEqual(target[1, 2], target[0, 2]);
            Assert.AreNotEqual(target[1, 2], target[0, 1]);
        }

        [TestMethod()]
        public void ComputeTest1()
        {
            double[][] inputs =
            {
                new double[] { 1, 4, 2, 0, 1 },
                new double[] { 1, 3, 2, 0, 1 },
                new double[] { 3, 0, 1, 1, 1 },
                new double[] { 3, 0, 1, 0, 1 },
                new double[] { 0, 5, 5, 5, 5 },
                new double[] { 1, 5, 5, 5, 5 },
                new double[] { 1, 0, 0, 0, 0 },
                new double[] { 1, 0, 0, 0, 0 },
            };

            int[] outputs =
            {
                0, 0,
                1, 1,
                2, 2,
                3, 3,
            };

            IKernel kernel = new Polynomial(2);
            var msvm = new MulticlassSupportVectorMachine(5, kernel, 4);
            var smo = new MulticlassSupportVectorLearning(msvm, inputs, outputs);
            smo.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            double error = smo.Run();

            for (int i = 0; i < inputs.Length; i++)
            {
                double expected = outputs[i];
                double actual = msvm.Compute(inputs[i], MulticlassComputeMethod.Elimination);
                Assert.AreEqual(expected, actual);
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                double expected = outputs[i];
                double actual = msvm.Compute(inputs[i], MulticlassComputeMethod.Voting);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void SerializeTest1()
        {
            double[][] inputs =
            {
                new double[] { 1, 4, 2, 0, 1 },
                new double[] { 1, 3, 2, 0, 1 },
                new double[] { 3, 0, 1, 1, 1 },
                new double[] { 3, 0, 1, 0, 1 },
                new double[] { 0, 5, 5, 5, 5 },
                new double[] { 1, 5, 5, 5, 5 },
                new double[] { 1, 0, 0, 0, 0 },
                new double[] { 1, 0, 0, 0, 0 },
            };

            int[] outputs =
            {
                0, 0,
                1, 1,
                2, 2,
                3, 3,
            };

            IKernel kernel = new Linear();
            var msvm = new MulticlassSupportVectorMachine(5, kernel, 4);
            var smo = new MulticlassSupportVectorLearning(msvm, inputs, outputs);
            smo.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            double expected = smo.Run();


            MemoryStream stream = new MemoryStream();

            // Save the machines
            msvm.Save(stream);

            // Rewind
            stream.Seek(0, SeekOrigin.Begin);

            // Reload the machines
            var target = MulticlassSupportVectorMachine.Load(stream);

            double actual;

            int count = 0; // Compute errors
            for (int i = 0; i < inputs.Length; i++)
            {
                double y = target.Compute(inputs[i]);
                if (y != outputs[i]) count++;
            }

            actual = (double)count / inputs.Length;


            Assert.AreEqual(expected, actual);

            Assert.AreEqual(msvm.Inputs, target.Inputs);
            Assert.AreEqual(msvm.Classes, target.Classes);
            for (int i = 0; i < msvm.Machines.Length; i++)
            {
                for (int j = 0; j < msvm.Machines.Length; j++)
                {
                    var a = msvm[i, j];
                    var b = target[i, j];

                    if (i != j)
                    {
                        Assert.IsTrue(a.SupportVectors.IsEqual(b.SupportVectors));
                    }
                    else
                    {
                        Assert.IsNull(a);
                        Assert.IsNull(b);
                    }
                }
            }
        }
    }
}
