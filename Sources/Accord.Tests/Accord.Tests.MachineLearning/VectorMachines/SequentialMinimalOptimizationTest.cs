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

    [TestClass()]
    public class SequentialMinimalOptimizationTest
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
        public void TrainTest()
        {

            double[][] inputs =
            {
                new double[] { -1, -1 },
                new double[] { -1,  1 },
                new double[] {  1, -1 },
                new double[] {  1,  1 }
            };

            int[] xor =
            {
                -1,
                 1,
                 1,
                -1
            };

            // Create Kernel Support Vector Machine with a Polynomial Kernel of 2nd degree
            KernelSupportVectorMachine machine = new KernelSupportVectorMachine(new Polynomial(2), inputs[0].Length);

            // Create the sequential minimal optimization teacher
            SequentialMinimalOptimization learn = new SequentialMinimalOptimization(machine, inputs, xor);

            // Run the learning algorithm
            learn.Run();


            double[] output = machine.Compute(inputs);

            for (int i = 0; i < output.Length; i++)
                Assert.AreEqual(System.Math.Sign(xor[i]), System.Math.Sign(output[i]));
        }

        [TestMethod()]
        public void TrainTest2()
        {

            double[][] inputs =
            {
                new double[] { -1, -1 },
                new double[] { -1,  1 },
                new double[] {  1, -1 },
                new double[] {  1,  1 }
            };

            int[] or =
            {
                -1,
                -1,
                -1,
                +1
            };

            // Create Kernel Support Vector Machine with a Polynomial Kernel of 2nd degree
            SupportVectorMachine machine = new SupportVectorMachine(inputs[0].Length);

            // Create the sequential minimal optimization teacher
            SequentialMinimalOptimization learn = new SequentialMinimalOptimization(machine, inputs, or);
            learn.Complexity = 1;

            // Run the learning algorithm
            learn.Run();


            double[] output = machine.Compute(inputs);

            for (int i = 0; i < output.Length; i++)
            {
                bool sor = or[i] >= 0;
                bool sou = output[i] >= 0;
                Assert.AreEqual(sor, sou);
            }


        }

        [TestMethod()]
        public void SequentialMinimalOptimizationConstructorTest()
        {
            double[][] inputs =
            {
                new double[] { -1, -1 },
                new double[] { -1,  1 },
                new double[] {  1, -1 },
                new double[] {  1,  1 }
            };

            int[] or =
            {
                0,
                0,
                0,
                +1
            };

            // Create Kernel Support Vector Machine with a Polynomial Kernel of 2nd degree
            SupportVectorMachine machine = new SupportVectorMachine(inputs[0].Length);

            bool thrown = false;
            try
            {
                SequentialMinimalOptimization learn = new SequentialMinimalOptimization(machine, inputs, or);
            }
            catch (ArgumentOutOfRangeException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

    }
}
