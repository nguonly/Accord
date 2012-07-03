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
using AForge.Imaging;
using AForge;
using System.Collections.Generic;
using System.Drawing;
using Accord.Controls;
using System.Windows.Forms;
using Accord.Imaging.Filters;

namespace Accord.Tests.Imaging
{


    /// <summary>
    ///This is a test class for HarrisCornersDetectorTest and is intended
    ///to contain all HarrisCornersDetectorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SURFTest
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
        ///A test for ProcessImage
        ///</summary>
        [TestMethod()]
        public void ProcessImageTest()
        {
            // Load an Image
            Bitmap img = Properties.Resources.sample_trans;

            // Extract the interest points
            var surf = new SpeededUpRobustFeaturesDetector(0.0002f, 5, 2);
            List<SurfPoint> points = surf.ProcessImage(img);

            // Describe the interest points
            SurfDescriptor descriptor = surf.GetDescriptor();
            descriptor.Describe(points);

            Assert.AreEqual(8, points.Count);

            SurfPoint p;

            p = points[0];
            Assert.AreEqual(0, p.Laplacian);
            Assert.AreEqual(25.3803387, p.X, 1e-2);
            Assert.AreEqual(14.7987738, p.Y, 1e-2);
            Assert.AreEqual(1.98713827, p.Scale, 1e-2);
            Assert.AreEqual(0.0, p.Response, 1e-2);
            Assert.AreEqual(4.78528404, p.Orientation, 1e-2);
            Assert.AreEqual(64, p.Descriptor.Length);
            Assert.AreEqual(0.22572951, p.Descriptor[23], 1e-2);
            Assert.AreEqual(0.0962982625, points[1].Descriptor[42], 1e-2);

            p = points[1];
            Assert.AreEqual(1, p.Laplacian, 1e-2);
            Assert.AreEqual(20.4856224, p.X, 1e-2);
            Assert.AreEqual(20.4817181, p.Y, 1e-2);
            Assert.AreEqual(1.90549147, p.Scale, 1e-2);
            Assert.AreEqual(0.0, p.Response, 1e-2);
            Assert.AreEqual(4.89748764, p.Orientation, 1e-2);
            Assert.AreEqual(64, p.Descriptor.Length);
            Assert.AreEqual(0.14823015, p.Descriptor[23], 1e-2);
            Assert.AreEqual(0.0861000642, p.Descriptor[54], 1e-2);

            p = points[2];
            Assert.AreEqual(0, p.Laplacian, 1e-2);
            Assert.AreEqual(14.7991896, p.X, 1e-2);
            Assert.AreEqual(25.3776169, p.Y, 1e-2);
            Assert.AreEqual(1.9869982, p.Scale, 1e-2);
            Assert.AreEqual(0.0, p.Response, 1e-2);
            Assert.AreEqual(3.07735944, p.Orientation, 1e-2);
            Assert.AreEqual(64, p.Descriptor.Length);
            Assert.AreEqual(0.209485427, p.Descriptor[23], 1e-2);
            Assert.AreEqual(0.0112418151, p.Descriptor[12], 1e-2);

            p = points[6];
            Assert.AreEqual(1, p.Laplacian, 1e-2);
            Assert.AreEqual(22.4346638, p.X, 1e-2);
            Assert.AreEqual(41.4026527, p.Y, 1e-2);
            Assert.AreEqual(2.83586049, p.Scale, 1e-2);
            Assert.AreEqual(0.0, p.Response, 1e-2);
            Assert.AreEqual(3.13142157, p.Orientation, 1e-2);
            Assert.AreEqual(64, p.Descriptor.Length);
            Assert.AreEqual(0.0467314087, p.Descriptor[23], 1e-2);
            Assert.AreEqual(0.0266618263, p.Descriptor[12], 1e-2);


            descriptor.Extended = true;
            descriptor.Invariant = false;
            descriptor.Describe(points);

            p = points[5];
            Assert.AreEqual(1, p.Laplacian, 1e-3);
            Assert.AreEqual(41.4027748, p.X, 1e-3);
            Assert.AreEqual(22.4343891, p.Y, 1e-3);
            Assert.AreEqual(2.83486962, p.Scale, 1e-3);
            Assert.AreEqual(0.0, p.Response, 1e-3);
            Assert.AreEqual(4.72728586, p.Orientation, 1e-3);
            Assert.AreEqual(0.00786296651, p.Descriptor[67], 1e-3);
            Assert.AreEqual(-0.0202884115, p.Descriptor[97], 1e-2);
        }



    }
}
