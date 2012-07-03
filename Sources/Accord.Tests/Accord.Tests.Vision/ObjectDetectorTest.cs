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

using Accord.Vision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using Accord.Controls;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;

namespace Accord.Tests.Vision
{


    /// <summary>
    ///This is a test class for ObjectDetectorTest and is intended
    ///to contain all ObjectDetectorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObjectDetectorTest
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
        ///A test for detect
        ///</summary>
        [TestMethod()]
        public void ProcessFrame()
        {
            HaarCascade cascade = new FaceHaarCascade();
            HaarObjectDetector target = new HaarObjectDetector(cascade,
                50, ObjectDetectorSearchMode.NoOverlap);

            Bitmap bmp = Properties.Resources.lena_color;

            target.ProcessFrame(bmp);

            Assert.AreEqual(1, target.DetectedObjects.Length);
            Assert.AreEqual(126, target.DetectedObjects[0].X);
            Assert.AreEqual(112, target.DetectedObjects[0].Y);
            Assert.AreEqual(59, target.DetectedObjects[0].Width);
            Assert.AreEqual(59, target.DetectedObjects[0].Height);
        }

        /// <summary>
        ///A test for detect
        ///</summary>
        [TestMethod()]
        public void ProcessFrame2()
        {
            HaarCascade cascade = new FaceHaarCascade();
            HaarObjectDetector target = new HaarObjectDetector(cascade,
                15, ObjectDetectorSearchMode.NoOverlap);

            Bitmap bmp = Properties.Resources.lena_gray;

            target.ProcessFrame(bmp);

            Assert.AreEqual(1, target.DetectedObjects.Length);
            Assert.AreEqual(255, target.DetectedObjects[0].X);
            Assert.AreEqual(225, target.DetectedObjects[0].Y);
            Assert.AreEqual(123, target.DetectedObjects[0].Width);
            Assert.AreEqual(123, target.DetectedObjects[0].Height);


            target = new HaarObjectDetector(cascade,
                15, ObjectDetectorSearchMode.Default);

            target.ProcessFrame(bmp);

            Assert.AreEqual(6, target.DetectedObjects.Length);
            Assert.AreEqual(255, target.DetectedObjects[0].X);
            Assert.AreEqual(225, target.DetectedObjects[0].Y);
            Assert.AreEqual(123, target.DetectedObjects[0].Width);
            Assert.AreEqual(123, target.DetectedObjects[0].Height);
        }

        /// <summary>
        ///A test for detect
        ///</summary>
        [TestMethod()]
        public void ProcessFrame3()
        {
            HaarCascade cascade = new FaceHaarCascade();
            HaarObjectDetector target = new HaarObjectDetector(cascade,
                15, ObjectDetectorSearchMode.NoOverlap);

            Bitmap bmp = Properties.Resources.three;

            target.ProcessFrame(bmp);

            Assert.AreEqual(2, target.DetectedObjects.Length);
            Assert.AreEqual(168, target.DetectedObjects[0].X);
            Assert.AreEqual(144, target.DetectedObjects[0].Y);
            Assert.AreEqual(49, target.DetectedObjects[0].Width);
            Assert.AreEqual(49, target.DetectedObjects[0].Height);

            Assert.AreEqual(392, target.DetectedObjects[1].X);
            Assert.AreEqual(133, target.DetectedObjects[1].Y);
            Assert.AreEqual(59, target.DetectedObjects[1].Width);
            Assert.AreEqual(59, target.DetectedObjects[1].Height);

            Assert.AreEqual(2, target.DetectedObjects.Length);


            target = new HaarObjectDetector(cascade,
                15, ObjectDetectorSearchMode.Single);

            target.ProcessFrame(bmp);

            Assert.AreEqual(1, target.DetectedObjects.Length);
        }

    }
}
