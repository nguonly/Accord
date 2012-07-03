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

namespace Accord.Tests.Math
{

    /// <summary>
    ///This is a test class for SpecialTest and is intended
    ///to contain all SpecialTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpecialTest
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
        ///A test for Binomial
        ///</summary>
        [TestMethod()]
        public void BinomialTest()
        {
            int n = 63;
            int k = 6;

            double expected = 67945521;
            double actual;

            actual = Special.Binomial(n, k);
            Assert.AreEqual(expected, actual);

            n = 42;
            k = 12;
            expected = 11058116888;

            actual = Special.Binomial(n, k);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Log1p
        ///</summary>
        [TestMethod()]
        public void Log1pTest()
        {
            double precision = 1e-16;
            Assert.IsTrue(double.IsNaN(Special.Log1p(double.NaN)));
            Assert.IsTrue(double.IsNaN(Special.Log1p(-32.0482175)));
            Assert.AreEqual(double.PositiveInfinity, Special.Log1p(double.PositiveInfinity));

            double b = System.Math.Log(1 - 7e-32);
            double c = Special.Log1p(7e-32);
            Assert.AreEqual(c, 7e-32);


            Assert.AreEqual(-0.2941782295312541, Special.Log1p(-0.254856327), precision);
            Assert.AreEqual(7.368050685564151, Special.Log1p(1583.542), precision);
            Assert.AreEqual(0.4633708685409921, Special.Log1p(0.5894227), precision);
            Assert.AreEqual(709.782712893384, Special.Log1p(double.MaxValue), precision);
            Assert.AreEqual(double.MinValue, Special.Log1p(double.MinValue), precision);
        }


        /// <summary>
        ///A test for Gamma
        ///</summary>
        [TestMethod()]
        public void GammaTest()
        {
            double x = 171;
            double expected = 7.257415615308056e+306;
            double actual = Gamma.Function(x);
            Assert.AreEqual(expected, actual, 1e+293);
        }

        /// <summary>
        ///A test for Factorial
        ///</summary>
        [TestMethod()]
        public void FactorialTest()
        {
            int n = 3;
            double expected = 6;
            double actual = Special.Factorial(n);
            Assert.AreEqual(expected, actual);

            n = 35;
            expected = 1.033314796638614e+40;
            actual = Special.Factorial(n);
            Assert.AreEqual(expected, actual, 1e27);
        }

        /// <summary>
        ///A test for LnFactorial
        ///</summary>
        [TestMethod()]
        public void LnFactorialTest()
        {
            int n = 4;
            double expected = 3.178053830347946;
            double actual = Special.LogFactorial(n);
            Assert.AreEqual(expected, actual, 0.0000000001);

            n = 170;
            expected = 7.065730622457874e+02;
            actual = Special.LogFactorial(n);
            Assert.AreEqual(expected, actual, 0.0000000001);
        }

        /// <summary>
        ///A test for Epsilon
        ///</summary>
        [TestMethod()]
        public void EpsilonTest()
        {
            double x = 0.5;
            double expected = 0.00000000000000011102230246251565;
            double actual = Special.Epslon(x);
            Assert.AreEqual(expected, actual);

            x = 0.0;
            expected = 0.0;
            actual = Special.Epslon(x);
            Assert.AreEqual(expected, actual);

            x = 1.0;
            expected = 0.00000000000000022204460492503131;
            actual = Special.Epslon(x);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Binomial
        ///</summary>
        [TestMethod()]
        public void BinomialTest2()
        {
            int n = 6;
            int k = 4;
            double expected = 15;
            double actual = Special.Binomial(n, k);
            Assert.AreEqual(expected, actual);

            n = 100;
            k = 47;
            expected = 8.441348728306404e+28;
            actual = Special.Binomial(n, k);
            Assert.AreEqual(expected, actual, 1e+16);
        }

        /// <summary>
        ///A test for Lgamma
        ///</summary>
        [TestMethod()]
        public void LgammaTest()
        {
            double x = 57;
            double expected = 172.35279713916282;

            double actual = Gamma.Log(x);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for BesselY0
        ///</summary>
        [TestMethod()]
        public void BesselY0Test()
        {
            double actual;

            actual = Bessel.Y0(64);
            Assert.AreEqual(0.037067103232088, actual, 0.000001);
        }

        /// <summary>
        ///A test for BesselJ
        ///</summary>
        [TestMethod()]
        public void BesselJTest()
        {
            double actual;

            actual = Bessel.J(0, 1);
            Assert.AreEqual(0.765197686557967, actual, 0.000001);

            actual = Bessel.J(0, 5);
            Assert.AreEqual(-0.177596771314338, actual, 0.000001);

            actual = Bessel.J(2, 17.3);
            Assert.AreEqual(0.117351128521774, actual, 0.000001);
        }

        /// <summary>
        ///A test for BesselY
        ///</summary>
        [TestMethod()]
        public void BesselYTest()
        {
            double actual;

            actual = Bessel.Y(2, 4);
            Assert.AreEqual(0.215903594603615, actual, 0.000001);

            actual = Bessel.Y(0, 64);
            Assert.AreEqual(0.037067103232088, actual, 0.000001);
        }

        /// <summary>
        ///A test for BesselJ0
        ///</summary>
        [TestMethod()]
        public void BesselJ0Test()
        {
            double actual;

            actual = Bessel.J0(1);
            Assert.AreEqual(0.765197686557967, actual, 0.000001);

            actual = Bessel.J0(5);
            Assert.AreEqual(-0.177596771314338, actual, 0.000001);
        }

        /// <summary>
        ///A test for Digamma
        ///</summary>
        [TestMethod()]
        public void DigammaTest()
        {
            double x = 42;
            double expected = 3.7257176179372822;
            double actual = Gamma.Digamma(x);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Ibeta
        ///</summary>
        [TestMethod()]
        public void IbetaTest()
        {
            double xx = 0.42;
            double aa = 2;
            double bb = 4;
            double expected = 0.696717907200000;

            double actual = Beta.Incomplete(aa, bb, xx);
            Assert.AreEqual(expected, actual, 0.0000001);
        }


        /// <summary>
        ///A test for Beta
        ///</summary>
        [TestMethod()]
        public void BetaTest()
        {
            double a = 4.2;
            double b = 3.0;
            double expected = 0.014770176060499;
            double actual = Beta.Function(a, b);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        /// <summary>
        ///A test for Normal
        ///</summary>
        [TestMethod()]
        public void NormalTest()
        {
            // p = 0.5 * erfc(-z ./ sqrt(2))
            double value = 0.42;
            double expected = 0.662757273151751;
            double actual = Normal.Function(value);
            Assert.AreEqual(expected, actual, 1e-10);
        }

        [TestMethod()]
        public void NormalInverseTest()
        {
            double value = 0.662757273151751;
            double expected = 0.42;
            double actual = Normal.Inverse(value);
            Assert.AreEqual(expected, actual, 1e-10);
        }

        [TestMethod()]
        public void GammaUpperRTest()
        {
            // Example values from
            // http://opensource.zyba.com/code/maths/special/gamma/gamma_upper_reg.php

            double expected, actual;

            actual = Gamma.UpperIncomplete(0.000000, 2);
            expected = 1.000000;
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(0.250000, 2);
            expected = 0.017286;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(0.500000, 2);
            expected = 0.045500;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(0.750000, 2);
            expected = 0.085056;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(1.000000, 2);
            expected = 0.135335;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(1.250000, 2);
            expected = 0.194847;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(1.500000, 2);
            expected = 0.261464;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(1.750000, 2);
            expected = 0.332706;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(2.000000, 2);
            expected = 0.406006;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(2.250000, 2);
            expected = 0.478944;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(2.500000, 2);
            expected = 0.549416;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));

            actual = Gamma.UpperIncomplete(2.750000, 2);
            expected = 0.615734;
            Assert.AreEqual(expected, actual, 1e-6);
            Assert.IsFalse(double.IsNaN(actual));
        }
    }
}
