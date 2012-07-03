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

namespace Accord.Tests.Statistics
{
    using Accord.Statistics.Analysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Accord.Math;
    using System;  
    
    [TestClass()]
    public class GeneralConfusionMatrixTest
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
        public void GeneralConfusionMatrixConstructorTest()
        {
            int classes = 3;

            int[] expected = { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 };
            int[] predicted = { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 };

            GeneralConfusionMatrix target = new GeneralConfusionMatrix(classes, expected, predicted);


            Assert.AreEqual(3, target.Classes);
            Assert.AreEqual(12, target.Samples);

            int[,] expectedMatrix = 
            {
                { 4, 0, 0 },
                { 0, 4, 0 },
                { 0, 4, 0 },
            };

            int[,] actualMatrix = target.Matrix;

            Assert.IsTrue(expectedMatrix.IsEqual(actualMatrix));
        }

        [TestMethod()]
        public void GeneralConfusionMatrixConstructorTest2()
        {
            int[,] matrix = 
            {
                { 4, 0, 0 },
                { 0, 4, 4 },
                { 0, 0, 0 },
            };

            GeneralConfusionMatrix target = new GeneralConfusionMatrix(matrix);


            Assert.AreEqual(3, target.Classes);
            Assert.AreEqual(12, target.Samples);
            Assert.AreEqual(matrix, target.Matrix);
        }

        [TestMethod()]
        public void KappaTest()
        {
            // Example from Graziano & Raulin:
            // http://www.mikeraulin.org/graziano7e/supplements/kappa.htm

            int[,] matrix =
            {
                { 29,  6,  5 },
                {  8, 20,  7 },
                {  1,  2, 22 },
            };

            GeneralConfusionMatrix target = new GeneralConfusionMatrix(matrix);

            
            Assert.AreEqual(3, target.Classes);
            Assert.AreEqual(100, target.Samples);

            Assert.AreEqual(0.563, target.Kappa, 1e-3);
        }

        [TestMethod()]
        public void TotalTest()
        {
            int[,] matrix = 
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            };

            GeneralConfusionMatrix target = new GeneralConfusionMatrix(matrix);

            int[] colTotals = target.ColumnTotals;
            int[] rowTotals = target.RowTotals;

            Assert.AreEqual(1 + 2 + 3, rowTotals[0]);
            Assert.AreEqual(4 + 5 + 6, rowTotals[1]);
            Assert.AreEqual(7 + 8 + 9, rowTotals[2]);

            Assert.AreEqual(1 + 4 + 7, colTotals[0]);
            Assert.AreEqual(2 + 5 + 8, colTotals[1]);
            Assert.AreEqual(3 + 6 + 9, colTotals[2]);
            
        }

        [TestMethod()]
        public void ChiSquareTest()
        {
            int[,] matrix =
            {
                {  10,      9,      5,      7,      8     },
                {   1,      2,      0,      1,      2     },
                {   0,      0,      1,      0,      1     },
                {   1,      0,      0,      3,      0     },
                {   0,      2,      0,      0,      2     },
            };

            GeneralConfusionMatrix target = new GeneralConfusionMatrix(matrix);

            double actual = target.ChiSquare;

            Assert.AreEqual(19.43,actual , 0.01);
            Assert.IsFalse(Double.IsNaN(actual));
        }


    }
}