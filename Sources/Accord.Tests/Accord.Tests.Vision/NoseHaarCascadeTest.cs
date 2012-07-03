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

using Accord.Vision.Detection.Cascades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.Vision.Detection;
using System.IO;
namespace Accord.Tests.Vision
{


    /// <summary>
    ///This is a test class for NoseHaarCascadeTest and is intended
    ///to contain all NoseHaarCascadeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NoseHaarCascadeTest
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
        ///A test for NoseHaarCascade Constructor
        ///</summary>
        [TestMethod()]
        public void NoseHaarCascadeConstructorTest()
        {
            NoseHaarCascade actual = new NoseHaarCascade();

            HaarCascade expected = HaarCascade.FromXml(new StringReader(Properties.Resources.haarcascade_mcs_nose));

            Assert.AreNotEqual(expected, actual);
            Assert.AreEqual(expected.HasTiltedFeatures, actual.HasTiltedFeatures);
            Assert.AreEqual(expected.Width, actual.Width);
            Assert.AreEqual(expected.Height, actual.Height);
            Assert.AreEqual(expected.Stages.Length, actual.Stages.Length);

            for (int i = 0; i < actual.Stages.Length; i++)
            {
                var aStage = actual.Stages[i];
                var eStage = expected.Stages[i];

                Assert.AreNotEqual(eStage, aStage);

                Assert.AreEqual(aStage.NextIndex, eStage.NextIndex);
                Assert.AreEqual(aStage.ParentIndex, eStage.ParentIndex);
                Assert.AreEqual(aStage.Threshold, eStage.Threshold);

                Assert.AreEqual(aStage.Trees.Length, eStage.Trees.Length);

                for (int j = 0; j < aStage.Trees.Length; j++)
                {
                    var aTree = aStage.Trees[j];
                    var eTree = eStage.Trees[j];

                    Assert.AreNotEqual(eTree, aTree);
                    Assert.AreEqual(eTree.Length, aTree.Length);

                    for (int k = 0; k < aTree.Length; k++)
                    {
                        var aNode = aTree[k];
                        var eNode = eTree[k];

                        Assert.AreNotEqual(eNode, aNode);
                        Assert.AreEqual(eNode.LeftNodeIndex, aNode.LeftNodeIndex);
                        Assert.AreEqual(eNode.LeftValue, aNode.LeftValue);
                        Assert.AreEqual(eNode.RightNodeIndex, aNode.RightNodeIndex);
                        Assert.AreEqual(eNode.RightValue, aNode.RightValue, 1e-16);
                        Assert.AreEqual(eNode.Threshold, aNode.Threshold);

                        Assert.AreEqual(eNode.Feature.Tilted, aNode.Feature.Tilted);
                        Assert.AreEqual(eNode.Feature.Rectangles.Length, aNode.Feature.Rectangles.Length);

                        for (int l = 0; l < eNode.Feature.Rectangles.Length; l++)
                        {
                            var aRect = aNode.Feature.Rectangles[l];
                            var eRect = eNode.Feature.Rectangles[l];

                            Assert.AreNotEqual(eRect, aRect);
                            Assert.AreEqual(eRect.Area, aRect.Area);
                            Assert.AreEqual(eRect.Height, aRect.Height);
                            Assert.AreEqual(eRect.Weight, aRect.Weight);
                            Assert.AreEqual(eRect.Width, aRect.Width);
                            Assert.AreEqual(eRect.X, aRect.X);
                            Assert.AreEqual(eRect.Y, aRect.Y);
                        }
                    }
                }
            }
        }
    }
}
