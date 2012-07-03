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
    using System;
    using Accord.MachineLearning.VectorMachines;
    using Accord.MachineLearning.VectorMachines.Learning;
    using Accord.Statistics.Kernels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass()]
    public class SupportVectorMachineTest
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
        public void ComputeTest()
        {
            // Example AND problem
            double[][] inputs =
            {
                new double[] { 0, 0 }, // 0 and 0: 0 (label -1)
                new double[] { 0, 1 }, // 0 and 1: 0 (label -1)
                new double[] { 1, 0 }, // 1 and 0: 0 (label -1)
                new double[] { 1, 1 }  // 1 and 1: 1 (label +1)
            };

            // Dichotomy SVM outputs should be given as [-1;+1]
            int[] labels =
            {
                // 0,  0,  0, 1
                  -1, -1, -1, 1
            };

            // Create a Support Vector Machine for the given inputs
            KernelSupportVectorMachine machine = new KernelSupportVectorMachine(new Gaussian(0.1), inputs[0].Length);

            // Instantiate a new learning algorithm for SVMs
            SequentialMinimalOptimization smo = new SequentialMinimalOptimization(machine, inputs, labels);

            // Set up the learning algorithm
            smo.Complexity = 1.0;

            // Run
            double error = smo.Run();

            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[0])));
            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[1])));
            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[2])));
            Assert.AreEqual(+1, Math.Sign(machine.Compute(inputs[3])));

            Assert.AreEqual(error, 0);

            Assert.AreEqual(-0.6640625, machine.Threshold);
            Assert.AreEqual(1, machine.Weights[0]);
            Assert.AreEqual(-0.34375, machine.Weights[1]);
            Assert.AreEqual(-0.328125, machine.Weights[2]);
            Assert.AreEqual(-0.328125, machine.Weights[3]);
        }

        [TestMethod()]
        public void ComputeTest3()
        {
            // Example AND problem
            double[][] inputs =
            {
                new double[] { 0, 0 }, // 0 and 0: 0 (label -1)
                new double[] { 0, 1 }, // 0 and 1: 0 (label -1)
                new double[] { 1, 0 }, // 1 and 0: 0 (label -1)
                new double[] { 1, 1 }  // 1 and 1: 1 (label +1)
            };

            // Dichotomy SVM outputs should be given as [-1;+1]
            int[] labels =
            {
                // 0,  0,  0, 1
                  -1, -1, -1, 1
            };

            // Create a Support Vector Machine for the given inputs
            KernelSupportVectorMachine machine = new KernelSupportVectorMachine(new Linear(), inputs[0].Length);

            // Instantiate a new learning algorithm for SVMs
            SequentialMinimalOptimization smo = new SequentialMinimalOptimization(machine, inputs, labels);

            // Set up the learning algorithm
            smo.Complexity = 100000.0;

            // Run
            double error = smo.Run();

            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[0])));
            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[1])));
            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[2])));
            Assert.AreEqual(+1, Math.Sign(machine.Compute(inputs[3])));

            Assert.AreEqual(error, 0);

            Assert.AreEqual(-3.0, machine.Threshold);
            Assert.AreEqual(4, machine.Weights[0]);
            Assert.AreEqual(-2, machine.Weights[1]);
            Assert.AreEqual(-2, machine.Weights[2]);
        }


        [TestMethod()]
        public void ComputeTest2()
        {
            // XOR
            double[][] inputs =
            {
                new double[] { 0, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 }
            };

            int[] labels =
            {
                -1,
                 1,
                 1,
                -1
            };

            KernelSupportVectorMachine machine = new KernelSupportVectorMachine(new Gaussian(0.1), inputs[0].Length);
            SequentialMinimalOptimization smo = new SequentialMinimalOptimization(machine, inputs, labels);

            smo.Complexity = 1;
            double error = smo.Run();

            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[0])));
            Assert.AreEqual(+1, Math.Sign(machine.Compute(inputs[1])));
            Assert.AreEqual(+1, Math.Sign(machine.Compute(inputs[2])));
            Assert.AreEqual(-1, Math.Sign(machine.Compute(inputs[3])));

            Assert.AreEqual(error, 0);
        }

        [TestMethod()]
        public void LoadTest1()
        {
            MemoryStream stream = new MemoryStream(Properties.Resources.SVM_014);
            var svm = MulticlassSupportVectorMachine.Load(stream);

            Assert.IsNotNull(svm.Machines);
            Assert.IsFalse(svm.IsProbabilistic);
            Assert.AreEqual(351, svm.MachinesCount);
        }
    }
}
