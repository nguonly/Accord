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

using Accord.MachineLearning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines.Learning;
using System;
namespace Accord.Tests.MachineLearning
{


    /// <summary>
    ///This is a test class for GridsearchTest and is intended
    ///to contain all GridsearchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GridsearchTest
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
        public void GridsearchConstructorTest()
        {
            Accord.Math.Tools.SetupGenerator(0);

            // Example binary data
            double[][] inputs =
            {
                new double[] { -1, -1 },
                new double[] { -1,  1 },
                new double[] {  1, -1 },
                new double[] {  1,  1 }
            };

            int[] xor = // xor labels
            {
                -1, 1, 1, -1
            };

            // Declare the parameters and ranges to be searched
            GridSearchRange[] ranges = 
            {
                new GridSearchRange("complexity", new double[] { 0.00000001, 5.20, 0.30, 0.50 } ),
                new GridSearchRange("degree",     new double[] { 1, 10, 2, 3, 4, 5 } ),
                new GridSearchRange("constant",   new double[] { 0, 1, 2 } )
            };


            // Instantiate a new Grid Search algorithm for Kernel Support Vector Machines
            var gridsearch = new GridSearch<KernelSupportVectorMachine>(ranges);

            // Set the fitting function for the algorithm
            gridsearch.Fitting = delegate(GridSearchParameterCollection parameters, out double error)
            {
                // The parameters to be tried will be passed as a function parameter.
                int degree = (int)parameters["degree"].Value;
                double constant = parameters["constant"].Value;
                double complexity = parameters["complexity"].Value;

                // Use the parameters to build the SVM model
                Polynomial kernel = new Polynomial(degree, constant);
                KernelSupportVectorMachine ksvm = new KernelSupportVectorMachine(kernel, 2);

                // Create a new learning algorithm for SVMs
                SequentialMinimalOptimization smo = new SequentialMinimalOptimization(ksvm, inputs, xor);
                smo.Complexity = complexity;

                // Measure the model performance to return as an out parameter
                error = smo.Run();

                return ksvm; // Return the current model
            };


            // Declare some out variables to pass to the grid search algorithm
            GridSearchParameterCollection bestParameters; double minError;

            // Compute the grid search to find the best Support Vector Machine
            KernelSupportVectorMachine bestModel = gridsearch.Compute(out bestParameters, out minError);


            // A linear kernel can't solve the xor problem.
            Assert.AreNotEqual((int)bestParameters["degree"].Value, 1);

            // The minimum error should be zero because the problem is well-known.
            Assert.AreEqual(minError, 0.0);


            Assert.IsNotNull(bestModel);
            Assert.IsNotNull(bestParameters);
            Assert.AreEqual(bestParameters.Count, 3);
        }
    }
}
