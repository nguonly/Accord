﻿// Accord Unit Tests
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

using Accord.Imaging.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Accord.Tests.Imaging
{


    /// <summary>
    ///This is a test class for ConcatenateTest and is intended
    ///to contain all ConcatenateTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConcatenateTest
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
        ///A test for Concatenate Constructor
        ///</summary>
        [TestMethod()]
        public void ConcatenateConstructorTest()
        {
            Bitmap img1 = Properties.Resources.image1;
            Bitmap img2 = Properties.Resources.image2;

            Concatenate target = new Concatenate(img1);
            var img3 = target.Apply(img2);

            Assert.AreEqual(System.Math.Max(img1.Height, img2.Height), img3.Height);
            Assert.AreEqual(img1.Width + img2.Width, img3.Width);



            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    Assert.AreEqual(img1.GetPixel(i, j), img3.GetPixel(i, j));

            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    Assert.AreEqual(img2.GetPixel(i, j), img3.GetPixel(i + 16, j));

        }
    }
}