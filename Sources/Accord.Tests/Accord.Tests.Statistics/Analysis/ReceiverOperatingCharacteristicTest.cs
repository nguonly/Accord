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
    using Accord.Math;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class ReceiverOperatingCharacteristicTest
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
        public void ComputeTest()
        {
            // Example from
            // http://faculty.vassar.edu/lowry/roc1.html

            double[,] data = 
            { 
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 },
                { 4,  1 },                { 4,  1 }, // 18
                { 4,  0 },

                { 6,  1 },                 { 6,  1 }, 
                { 6,  1 },                 { 6,  1 }, 
                { 6,  1 },                 { 6,  1 }, 
                { 6,  1 }, // 7

                { 6,  0 },                 { 6,  0 },
                { 6,  0 },                 { 6,  0 },
                { 6,  0 },                 { 6,  0 },
                { 6,  0 },                 { 6,  0 },
                { 6,  0 },                 { 6,  0 },
                { 6,  0 },                 { 6,  0 },
                { 6,  0 },                 { 6,  0 },
                { 6,  0 },                 { 6,  0 },
                { 6,  0 }, // 17

                { 8,  1 },                { 8,  1 },
                { 8,  1 },                { 8,  1 }, // 4

                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 },
                { 8,  0 },                { 8,  0 }, // 36

                { 9, 1 },                 { 9, 1 },
                { 9, 1 }, // 3

                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 },
                { 9, 0 },                { 9, 0 }, 
                { 9, 0 },                { 9, 0 }, 
                { 9, 0 }, // 39
            };


            double[] measurement = data.GetColumn(1);
            double[] prediction = data.GetColumn(0);

            var roc = new ReceiverOperatingCharacteristic(measurement, prediction);
            double[] cutpoints = { 5, 7, 9, double.PositiveInfinity };

            roc.Compute(cutpoints);

            Assert.AreEqual(32, roc.Positives);
            Assert.AreEqual(93, roc.Negatives);

            Assert.AreEqual(4, roc.Points.Count);
            var p1 = roc.Points[0];
            var p2 = roc.Points[1];
            var p3 = roc.Points[2];
            var p4 = roc.Points[3];

            Assert.AreEqual(18, p1.FalseNegatives);
            Assert.AreEqual(18 + 7, p2.FalseNegatives);
            Assert.AreEqual(18 + 7 + 4, p3.FalseNegatives);
            Assert.AreEqual(18 + 7 + 4 + 3, p4.FalseNegatives);

            Assert.AreEqual(1, p1.TrueNegatives);
            Assert.AreEqual(1 + 17, p2.TrueNegatives);
            Assert.AreEqual(1 + 17 + 36, p3.TrueNegatives);
            Assert.AreEqual(1 + 17 + 36 + 39, p4.TrueNegatives);


            double area = roc.Area;
            double error = roc.Error;

            // Area should be near 0.87
            Assert.IsTrue(System.Math.Abs(area - 0.875) < roc.Error);

        }

        [TestMethod()]
        public void ReceiverOperatingCharacteristicConstructorTest2()
        {
            double[] measurement = { 0, 0, 0, 0, 0, 1, 1, 1 };
            double[] prediction = { 0, 0, 0.5, 0.5, 1, 1, 1, 1 };
            ReceiverOperatingCharacteristic target = new ReceiverOperatingCharacteristic(measurement, prediction);

            target.Compute(0.5, true);
            Assert.AreEqual(target.Points.Count, 4);
            var p1 = target.Points[0];
            var p2 = target.Points[1];
            var p3 = target.Points[2];
            var p4 = target.Points[3];

            Assert.AreEqual(p1.Sensitivity, 1);
            Assert.AreEqual(1 - p1.Specificity, 1);
            Assert.AreEqual(p4.Sensitivity, 0);
            Assert.AreEqual(1 - p4.Specificity, 0);

            target.Compute(0.5, false);
            Assert.AreEqual(target.Points.Count, 3);


            target.Compute(new double[] { 0.0, 0.4, 0.6, 1.0 });

            Assert.AreEqual(target.Points.Count, 4);
            Assert.AreEqual(target.Negatives, 5);
            Assert.AreEqual(target.Positives, 3);
            Assert.AreEqual(target.Observations, 8);

            foreach (var point in target.Points)
            {
                Assert.AreEqual(point.Observations, 8);
                Assert.AreEqual(point.ActualNegatives, 5);
                Assert.AreEqual(point.ActualPositives, 3);

                if (point.Cutoff == 0.0)
                {
                    Assert.AreEqual(point.PredictedNegatives, 0);
                    Assert.AreEqual(point.PredictedPositives, 8);
                }
                else if (point.Cutoff == 0.4)
                {
                    Assert.AreEqual(point.PredictedNegatives, 2);
                    Assert.AreEqual(point.PredictedPositives, 6);
                }
                else
                {
                    Assert.AreEqual(point.PredictedNegatives, 4);
                    Assert.AreEqual(point.PredictedPositives, 4);
                }

            }

            Assert.AreEqual(target.Area, 0.8);
            Assert.AreEqual(target.Error, 0.1821680136170595);

        }

    }
}
