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

using Accord.Math.Decompositions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.Math;
namespace Accord.Tests.Math
{


    /// <summary>
    ///This is a test class for QrDecompositionTest and is intended
    ///to contain all QrDecompositionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QrDecompositionTest
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


        /// <summary>
        ///A test for QrDecomposition Constructor
        ///</summary>
        [TestMethod()]
        public void QrDecompositionConstructorTest()
        {
            double[,] value =
            {
               {  2, -1,  0 },
               { -1,  2, -1 },
               {  0, -1,  2 }
            };


            QrDecomposition target = new QrDecomposition(value);

            // Decomposition Identity
            var Q = target.OrthogonalFactor;
            var QQt = Q.Multiply(Q.Transpose());
            Assert.IsTrue(Matrix.IsEqual(QQt, Matrix.Identity(3), 0.0000001));


            // Linear system solving
            double[,] B = Matrix.ColumnVector(new double[] { 1, 2, 3 });
            double[,] expected = Matrix.ColumnVector(new double[] { 2.5, 4.0, 3.5 });
            double[,] actual = target.Solve(B);

            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0000000000001));
        }

        /// <summary>
        ///A test for QrDecomposition Constructor
        ///</summary>
        [TestMethod()]
        public void InverseTest()
        {
            double[,] value =
            {
               {  2, -1,  0 },
               { -1,  2, -1 },
               {  0, -1,  2 }
            };

            double[,] expected =
            {
                { 0.7500,    0.5000,    0.2500},
                { 0.5000,    1.0000,    0.5000},
                { 0.2500,    0.5000,    0.7500},
            };


            QrDecomposition target = new QrDecomposition(value);

            double[,] actual = target.Inverse();
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0000000000001));

            target = new QrDecomposition(value.Transpose(), true);
            actual = target.Inverse();
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0000000000001));
        }

        /// <summary>
        ///A test for QrDecomposition Constructor
        ///</summary>
        [TestMethod()]
        public void SolveTest()
        {
            double[,] value =
            {
               {  2, -1,  0 },
               { -1,  2, -1 },
               {  0, -1,  2 }
            };

            double[] b = { 1, 2, 3 };

            double[] expected = { 2.5000, 4.0000, 3.5000 };

            QrDecomposition target = new QrDecomposition(value);
            double[] actual = target.Solve(b);
            
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0000000000001));
        }

        /// <summary>
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void SolveTransposeTest()
        {
            double[,] a = 
            {
                { 2, 1, 4 },
                { 6, 2, 2 },
                { 0, 1, 6 },
            };

            double[,] b =
            {
                { 1, 0, 7 },
                { 5, 2, 1 },
                { 1, 5, 2 },
            };

            double[,] expected =
            {
                 { 0.5062,    0.2813,    0.0875 },
                 { 0.1375,    1.1875,   -0.0750 },
                 { 0.8063,   -0.2188,    0.2875 },
            };

            double[,] actual = new QrDecomposition(b, true).SolveTranspose(a);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.001));
        }
    }
}
