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

using Accord.Statistics.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accord.Tests.Statistics
{


    /// <summary>
    ///This is a test class for OneWayAnovaTest and is intended
    ///to contain all OneWayAnovaTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OneWayAnovaTest
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
        public void OneWayAnovaConstructorTest()
        {
            // Example from http://en.wikipedia.org/wiki/F_test

            double[][] samples =
            {
               new double[] { 6, 8, 4, 5, 3, 4 },
               new double[] { 8, 12, 9, 11, 6, 8 },
               new double[] { 13, 9, 11, 8, 7, 12 },
            };


            OneWayAnova target = new OneWayAnova(samples);

            Assert.AreEqual(target.Table.Count, 3);
            Assert.AreEqual("Between-Groups", target.Table[0].Source);
            Assert.AreEqual(84, target.Table[0].SumOfSquares); // Sb
            Assert.AreEqual(2, target.Table[0].DegreesOfFreedom); // df
            Assert.AreEqual(42, target.Table[0].MeanSquares); // MSb
            Assert.AreEqual(9.264705882352942, target.Table[0].Statistic);
            Assert.AreEqual(0.0023987773293928649, target.Table[0].Significance.PValue);

            Assert.AreEqual("Within-Groups", target.Table[1].Source);
            Assert.AreEqual(68, target.Table[1].SumOfSquares); // Sw
            Assert.AreEqual(15, target.Table[1].DegreesOfFreedom); // df
            Assert.AreEqual(4.5333333333333332, target.Table[1].MeanSquares); // MSw
            Assert.IsNull(target.Table[1].Statistic);
            Assert.IsNull(target.Table[1].Significance);

            Assert.AreEqual("Total", target.Table[2].Source);
            Assert.AreEqual(152, target.Table[2].SumOfSquares); // Sw
            Assert.AreEqual(53, target.Table[2].DegreesOfFreedom); // df
            // Assert.IsNull(target.Table[2].MeanSquares); // MSw
            Assert.IsNull(target.Table[2].Statistic);
            Assert.IsNull(target.Table[2].Significance);

        }
    }
}
