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
    using Accord.Statistics.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class KappaTestTest
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
        public void KappaTestConstructorTest()
        {
            int[,] matrix =
            {
                { 44,  5,  1},
                {  7, 20,  3 },
                {  9,  5,  6 },
            };

            GeneralConfusionMatrix a = new GeneralConfusionMatrix(matrix);

            Assert.AreEqual(0.4915, a.Kappa, 1e-4);
            Assert.IsFalse(double.IsNaN(a.Kappa));

            KappaTest target = new KappaTest(a);

            Assert.AreEqual(0.0777, target.StandardError, 1e-2);
            Assert.IsFalse(double.IsNaN(target.StandardError));

            Assert.AreEqual(0.3393, target.Confidence.Min, 1e-2);
            Assert.AreEqual(0.6437, target.Confidence.Max, 1e-2);
        }

        [TestMethod()]
        public void KappaTestConstructorTest2()
        {
            int[,] matrix =
            {
                { 317,  23,  0,  0 },
                {  61, 120,  0,  0 },
                {   2,   4, 60,  0 },
                {  35,  29,  0,  8 },
            };

            GeneralConfusionMatrix a = new GeneralConfusionMatrix(matrix);

            Assert.AreEqual(0.7663, a.OverralAgreement, 1e-4);
            Assert.IsFalse(double.IsNaN(a.OverralAgreement));

            Assert.AreEqual(0.4087, a.ChanceAgreement, 1e-5);
            Assert.IsFalse(double.IsNaN(a.ChanceAgreement));

            KappaTest target = new KappaTest(a);

            Assert.AreEqual(0.605, target.Kappa, 1e-3);
            Assert.AreEqual(a.Kappa, target.Kappa);
            Assert.IsFalse(double.IsNaN(target.Kappa));

            Assert.AreEqual(0.00073735, target.Variance, 1e-5);
            Assert.IsFalse(double.IsNaN(target.Variance));



            Assert.AreEqual(22.272, target.Statistic, 0.15);
            Assert.IsFalse(double.IsNaN(target.Statistic));


            Assert.AreEqual(0.658, target.Confidence.Max, 1e-3);
            Assert.AreEqual(0.552, target.Confidence.Min, 1e-3);

            Assert.AreEqual(0.0, target.PValue, 1e-6);
            Assert.IsFalse(double.IsNaN(target.PValue));

            Assert.IsTrue(target.Significant);
        }

        [TestMethod()]
        public void KappaTestConstructorTest3()
        {
            int[,] matrix =
            {
                { 377,  79,  0,  0 },
                {   2,  72,  0,  0 },
                {  33,   5, 60,  0 },
                {   3,  20,  0,  8 },
            };

            GeneralConfusionMatrix a = new GeneralConfusionMatrix(matrix);

            Assert.AreEqual(0.7845, a.OverralAgreement, 1e-4);
            Assert.IsFalse(double.IsNaN(a.OverralAgreement));

            Assert.AreEqual(0.47986, a.ChanceAgreement, 1e-5);
            Assert.IsFalse(double.IsNaN(a.ChanceAgreement));

            Assert.AreEqual(0.586, a.Kappa, 1e-3);
            Assert.IsFalse(double.IsNaN(a.Kappa));

            KappaTest target = new KappaTest(a);


            Assert.AreEqual(0.586, target.Kappa, 1e-3);
            Assert.AreEqual(a.Kappa, target.Kappa);
            Assert.IsFalse(double.IsNaN(target.Kappa));

            Assert.AreEqual(0.00087457, target.Variance, 1e-5);
            Assert.IsFalse(double.IsNaN(target.Variance));

            Assert.AreEqual(19.806, target.Statistic, 0.1);
            Assert.IsFalse(double.IsNaN(target.Statistic));


            Assert.AreEqual(0.644, target.Confidence.Max, 1e-3);
            Assert.AreEqual(0.528, target.Confidence.Min, 1e-3);

            Assert.AreEqual(0.0, target.PValue, 1e-6);
            Assert.IsFalse(double.IsNaN(target.PValue));

            Assert.IsTrue(target.Significant);
        }

    }
}
