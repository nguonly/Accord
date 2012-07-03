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
    using Accord.Math;

    [TestClass()]
    public class GaussianMixtureModelTest
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
        public void GaussianMixtureModelConstructorTest()
        {
            Accord.Math.Tools.SetupGenerator(0);

            // Test Samples
            double[][] samples =
            {
                new double[] { 0, 1 },
                new double[] { 1, 2 }, 
                new double[] { 1, 1 },
                new double[] { 0, 7 },
                new double[] { 1, 1 },
                new double[] { 6, 2 },
                new double[] { 6, 5 },
                new double[] { 5, 1 },
                new double[] { 7, 1 },
                new double[] { 5, 1 }
            };

            double[] sample = samples[0];


            // Create a new Gaussian Mixture Model with 2 components
            GaussianMixtureModel gmm = new GaussianMixtureModel(2);

            // Compute the model (estimate)
            gmm.Compute(samples, 0.0001);

            // Classify a single sample
            int c = gmm.Classify(sample);

            Assert.AreEqual(2, gmm.Gaussians.Count);

            for (int i = 0; i < samples.Length; i++)
            {
                sample = samples[i];
                c = gmm.Classify(sample);

                Assert.AreEqual(c, i >= 5 ? 1 : 0);
            }
        }
        
        [TestMethod]
        public void GaussianMixtureModelTest2()
        {
            Accord.Math.Tools.SetupGenerator(0);

            int height = 16;
            int width = 16;

            var gmm = new GaussianMixtureModel(3);
           // gmm.Regularization = 0;

            Assert.AreEqual(3, gmm.Gaussians.Count);
            Assert.IsNull(gmm.Gaussians[0].Covariance);
            Assert.IsNull(gmm.Gaussians[0].Mean);


            double[][][] A = new double[3][][];
            A[0] = new double[height][];
            A[1] = new double[height][];
            A[2] = new double[height][];

            for (int j = 0; j < height; j++)
            {
                A[0][j] = new double[width];
                A[1][j] = new double[width];
                A[2][j] = new double[width];

                for (int k = 0; k < width; k++)
                {
                    A[0][j][k] = 102;
                    A[1][j][k] = 57;
                    A[2][j][k] = 200;
                }
            }

            double[][] B = Matrix.Stack(A);

            bool thrown = false;
            try
            {
                double result = gmm.Compute(B);
            }
            catch (NonPositiveDefiniteMatrixException )
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void GaussianMixtureModelTest3()
        {
            Accord.Math.Tools.SetupGenerator(0);

            var gmm = new GaussianMixtureModel(3);
            Assert.AreEqual(3, gmm.Gaussians.Count);
            Assert.IsNull(gmm.Gaussians[0].Covariance);
            Assert.IsNull(gmm.Gaussians[0].Mean);


            double[][] B = Matrix.Random(56, 12).ToArray();

            double result = gmm.Compute(B);
        }

    }
}
