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

using Accord.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AForge;

namespace Accord.Tests.Math
{


    /// <summary>
    ///This is a test class for ToolsTest and is intended
    ///to contain all ToolsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ToolsTest
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
        ///A test for Scale
        ///</summary>
        [TestMethod()]
        public void ScaleTest1()
        {
            double fromMin = 0;
            double fromMax = 1;
            double toMin = 0;
            double toMax = 100;
            double x = 0.2;

            double actual = Tools.Scale(fromMin, fromMax, toMin, toMax, x);
            Assert.AreEqual(20.0, actual);

            float actualF = Tools.Scale((float)fromMin, (float)fromMax, (float)toMin, (float)toMax, (float)x);
            Assert.AreEqual(20f, actualF);
        }

        /// <summary>
        ///A test for Scale
        ///</summary>
        [TestMethod()]
        public void ScaleTest()
        {
            IntRange from = new IntRange(0, 100);
            IntRange to = new IntRange(0, 50);
            int x = 50;
            int expected = 25;
            int actual = Tools.Scale(from, to, x);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Scale
        ///</summary>
        [TestMethod()]
        public void ScaleTest2()
        {
            DoubleRange from = new DoubleRange(-100, 100);
            DoubleRange to = new DoubleRange(0, 50);
            double x = 0;
            double expected = 25;
            double actual = Tools.Scale(from, to, x);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Scale
        ///</summary>
        [TestMethod()]
        public void ScaleTest3()
        {
            double toMin = 0;
            double toMax = 100;

            double[][] x = 
            { 
                new double[] { -1.0,  1.0 },
                new double[] { -0.2,  0.0 },
                new double[] { -0.6,  0.0 },
                new double[] {  0.0, -1.0 },
            };

            double[][] expected = 
            { 
                new double[] {    0, 100 },
                new double[] {   80,  50 },
                new double[] {   40,  50 },
                new double[] {  100,   0 },
            };

            var actual = Tools.Scale(toMin, toMax, x);

            Assert.IsTrue(Matrix.IsEqual(expected, actual));

        }

        /// <summary>
        ///A test for Scale
        ///</summary>
        [TestMethod()]
        public void ScaleTest4()
        {
            float fromMin = 0f;
            float fromMax = 1f;
            float toMin = 0f;
            float toMax = 100f;
            float[] x = { 0.2f, 0.34f };

            float[] actual = Tools.Scale(fromMin, fromMax, toMin, toMax, x);
            Assert.AreEqual(20.0, actual[0]);
            Assert.AreEqual(34.0, actual[1]);

            float[] actualF = Tools.Scale(fromMin, fromMax, toMin, toMax, x);
            Assert.AreEqual(20f, actualF[0]);
            Assert.AreEqual(34f, actualF[1]);
        }

        /// <summary>
        ///A test for Atanh
        ///</summary>
        [TestMethod()]
        public void AtanhTest()
        {
            double d = 0.42;
            double expected = 0.447692023527421;
            double actual = Tools.Atanh(d);
            Assert.AreEqual(expected, actual, 1e-10);
        }

        /// <summary>
        ///A test for Asinh
        ///</summary>
        [TestMethod()]
        public void AsinhTest()
        {
            double d = 0.42;
            double expected = 0.408540207829808;
            double actual = Tools.Asinh(d);
            Assert.AreEqual(expected, actual, 1e-10);
        }

        /// <summary>
        ///A test for Acosh
        ///</summary>
        [TestMethod()]
        public void AcoshTest()
        {
            double x = 3.14;
            double expected = 1.810991348900196;
            double actual = Tools.Acosh(x);
            Assert.AreEqual(expected, actual, 1e-10);
        }

        /// <summary>
        ///A test for InvSqrt
        ///</summary>
        [TestMethod()]
        public void InvSqrtTest()
        {
            float f = 42f;

            float expected = 1f / (float)System.Math.Sqrt(f);
            float actual = Tools.InvSqrt(f);

            Assert.AreEqual(expected, actual, 0.001);
        }

        /// <summary>
        ///A test for Direction
        ///</summary>
        [TestMethod()]
        public void DirectionTest()
        {
            IntPoint center = new IntPoint(0, 0);

            IntPoint w = new IntPoint(1, 0);
            IntPoint nw = new IntPoint(1, 1);
            IntPoint n = new IntPoint(0, 1);
            IntPoint ne = new IntPoint(-1, 1);
            IntPoint e = new IntPoint(-1, 0);
            IntPoint se = new IntPoint(-1, -1);
            IntPoint s = new IntPoint(0, -1);
            IntPoint sw = new IntPoint(1, -1);


            int actual;
            int expected;

            actual = Accord.Math.Tools.Direction(center, w);
            expected = (int)System.Math.Floor(0 / 18.0);
            Assert.AreEqual(expected, actual);

            actual = Accord.Math.Tools.Direction(center, nw);
            expected = (int)System.Math.Floor(45 / 18.0);
            Assert.AreEqual(expected, actual);

            actual = Accord.Math.Tools.Direction(center, n);
            expected = (int)System.Math.Floor(90 / 18.0);
            Assert.AreEqual(expected, actual);

            actual = Accord.Math.Tools.Direction(center, ne);
            expected = (int)System.Math.Floor(135 / 18.0);
            Assert.AreEqual(expected, actual);

            actual = Accord.Math.Tools.Direction(center, e);
            expected = (int)System.Math.Floor(180 / 18.0);
            Assert.AreEqual(expected, actual);

            actual = Accord.Math.Tools.Direction(center, se);
            expected = (int)System.Math.Floor(225 / 18.0);
            Assert.AreEqual(expected, actual);

            actual = Accord.Math.Tools.Direction(center, s);
            expected = (int)System.Math.Floor(270 / 18.0);
            Assert.AreEqual(expected, actual);

            actual = Accord.Math.Tools.Direction(center, sw);
            expected = (int)System.Math.Floor(315 / 18.0);
            Assert.AreEqual(expected, actual);
        }
    }
}
