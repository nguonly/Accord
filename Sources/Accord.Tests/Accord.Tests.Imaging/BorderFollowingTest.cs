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

using Accord.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.Math;
using System.Drawing;

using Tools = Accord.Imaging.Tools;
using Accord.Controls;
using System.Windows.Forms;
using System.Drawing.Imaging;
using AForge;
using System.Collections.Generic;
using Accord.Controls.Imaging;
using System;
using Accord.Imaging.Filters;
using AForge.Imaging;

namespace Accord.Tests.Imaging
{


    /// <summary>
    ///This is a test class for ToolsTest and is intended
    ///to contain all ToolsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BorderFollowingTest
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
        public void FindContourTest()
        {
            Bitmap bmp = Properties.Resources.sample_black;

            Bitmap gray = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(bmp);

            BlobCounter bc = new BlobCounter(gray);
            bc.ObjectsOrder = ObjectsOrder.Size;
            Blob[] blobs = bc.GetObjectsInformation();
            bc.ExtractBlobsImage(bmp, blobs[0], true);
            List<IntPoint> expected = bc.GetBlobsEdgePoints(blobs[0]);
            Bitmap blob = blobs[0].Image.ToManagedImage();

            BorderFollowing bf = new BorderFollowing();
            List<IntPoint> actual = bf.FindContour(blob);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (IntPoint point in expected)
                Assert.IsTrue(actual.Contains(point));

            foreach (IntPoint point in actual)
                Assert.IsTrue(expected.Contains(point));

            IntPoint prev = actual[0];
            for (int i = 1; i < actual.Count; i++)
            {
                IntPoint curr = actual[i];
                Assert.IsTrue(System.Math.Abs(prev.X - curr.X) <= 1 &&
                              System.Math.Abs(prev.Y - curr.Y) <= 1);
                prev = curr;
            }

            IntPoint first = actual[0];
            IntPoint last = actual[actual.Count - 1];
            Assert.IsTrue(System.Math.Abs(first.X - last.X) <= 1 &&
                          System.Math.Abs(first.Y - last.Y) <= 1);
        }

        [TestMethod()]
        public void FindContourTest2()
        {
            Bitmap bmp = Properties.Resources.hand2;

            BlobCounter bc = new BlobCounter(bmp);
            bc.ObjectsOrder = ObjectsOrder.Size;
            Blob[] blobs = bc.GetObjectsInformation();
            bc.ExtractBlobsImage(bmp, blobs[0], true);
            List<IntPoint> expected = bc.GetBlobsEdgePoints(blobs[0]);
            Bitmap blob = blobs[0].Image.ToManagedImage();

            BorderFollowing bf = new BorderFollowing();
            List<IntPoint> actual = bf.FindContour(blob);

            foreach (IntPoint point in expected)
                Assert.IsTrue(actual.Contains(point));

            IntPoint prev = actual[0];
            for (int i = 1; i < actual.Count; i++)
            {
                IntPoint curr = actual[i];
                Assert.IsTrue(System.Math.Abs(prev.X - curr.X) <= 1 &&
                              System.Math.Abs(prev.Y - curr.Y) <= 1);
                prev = curr;
            }

            IntPoint first = actual[0];
            IntPoint last = actual[actual.Count - 1];
            Assert.IsTrue(System.Math.Abs(first.X - last.X) <= 1 &&
                          System.Math.Abs(first.Y - last.Y) <= 1);
        }


    }
}
