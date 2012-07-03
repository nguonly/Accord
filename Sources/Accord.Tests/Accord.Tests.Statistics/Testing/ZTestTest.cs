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
namespace Accord.Tests.Statistics
{
    
    
    /// <summary>
    ///This is a test class for ZTestTest and is intended
    ///to contain all ZTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ZTestTest
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
        ///A test for ZTest Constructor
        ///</summary>
        [TestMethod()]
        public void ZTestConstructorTest()
        {
            /* Suppose that in a particular geographic region, the mean and standard
             * deviation of scores on a reading test are 100 points, and 12 points, 
             * respectively. Our interest is in the scores of 55 students in a particular
             * school who received a mean score of 96. We can ask whether this mean score
             * is significantly lower than the regional mean — that is, are the students
             * in this school comparable to a simple random sample of 55 students from the
             * region as a whole, or are their scores surprisingly low?
             */

            ZTest target;
            
            target = new ZTest(100, 12, 96, 55, Hypothesis.OneLower);
            Assert.AreEqual(target.Statistic, -2.47, 0.01);

            
            Assert.AreEqual(target.PValue, 0.0068, 0.001);

            /* This is the one-sided p-value for the null hypothesis that the 55 students 
             * are comparable to a simple random sample from the population of all test-takers.
             * The two-sided p-value is approximately 0.014 (twice the one-sided p-value).
             */

            target = new ZTest(100, 12, 96, 55, Hypothesis.TwoTail);
            Assert.AreEqual(target.PValue, 0.014, 0.005);

            
        }

    }
}
