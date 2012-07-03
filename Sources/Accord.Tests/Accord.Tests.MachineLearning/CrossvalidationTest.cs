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
    using Accord.MachineLearning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Accord.MachineLearning.VectorMachines;
    using Accord.MachineLearning.VectorMachines.Learning;
    using Accord.Math;
    using Accord.Statistics.Kernels;

    [TestClass()]
    public class CrossvalidationTest
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
        public void CrossvalidationConstructorTest()
        {

            Accord.Math.Tools.SetupGenerator(0);

// This is a sample code on how to use Cross-Validation
// to access the performance of Support Vector Machines.

// Consider the example binary data. We will be trying
// to learn a XOR problem and see how well does SVMs
// perform on this data.

double[][] data =
{
    new double[] { -1, -1 }, new double[] {  1, -1 },
    new double[] { -1,  1 }, new double[] {  1,  1 },
    new double[] { -1, -1 }, new double[] {  1, -1 },
    new double[] { -1,  1 }, new double[] {  1,  1 },
    new double[] { -1, -1 }, new double[] {  1, -1 },
    new double[] { -1,  1 }, new double[] {  1,  1 },
    new double[] { -1, -1 }, new double[] {  1, -1 },
    new double[] { -1,  1 }, new double[] {  1,  1 },
};

int[] xor = // result of xor for the sample input data
{
    -1,       1,
        1,      -1,
    -1,       1,
        1,      -1,
    -1,       1,
        1,      -1,
    -1,       1,
        1,      -1,
};


// Create a new Cross-validation algorithm passing the data set size and the number of folds
var crossvalidation = new CrossValidation<KernelSupportVectorMachine>(size: data.Length, folds: 3);

// Define a fitting function using Support Vector Machines. The objective of this
// function is to learn a SVM in the subset of the data dicted by cross-validation.

crossvalidation.Fitting = delegate(int k, int[] indicesTrain, int[] indicesValidation)
{
    // The fitting function is passing the indices of the original set which
    // should be considered training data and the indices of the original set
    // which should be considered validation data.

    // Lets now grab the training data:
    var trainingInputs = data.Submatrix(indicesTrain);
    var trainingOutputs = xor.Submatrix(indicesTrain);

    // And now the validation data:
    var validationInputs = data.Submatrix(indicesValidation);
    var validationOutputs = xor.Submatrix(indicesValidation);


    // Create a Kernel Support Vector Machine to operate on the set
    var svm = new KernelSupportVectorMachine(new Polynomial(2), 2);

    // Create a training algorithm and learn the training data
    var smo = new SequentialMinimalOptimization(svm, trainingInputs, trainingOutputs);

    double trainingError = smo.Run();

    // Now we can compute the validation error on the validation data:
    double validationError = smo.ComputeError(validationInputs, validationOutputs);

    // Return a new information structure containing the model and the errors achieved.
    return new CrossValidationValues<KernelSupportVectorMachine>(svm, trainingError, validationError);
};


// Compute the cross-validation
var result = crossvalidation.Compute();

// Finally, access the measured performance.
double trainingErrors = result.Training.Mean;
double validationErrors = result.Validation.Mean;

            Assert.AreEqual(3, crossvalidation.K);
            Assert.AreEqual(0, result.Training.Mean);
            Assert.AreEqual(0, result.Validation.Mean);

            Assert.AreEqual(3, crossvalidation.Folds.Length);
            Assert.AreEqual(3, result.Models.Length);
        }

    }
}
