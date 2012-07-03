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

using Accord.DirectSound;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accord.Audio;
namespace Accord.Tests.Audio
{


    /// <summary>
    ///This is a test class for WaveFileAudioSourceTest and is intended
    ///to contain all WaveFileAudioSourceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WaveFileAudioSourceTest
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
        ///A test for WaveFileAudioSource Constructor
        ///</summary>
        [TestMethod()]
        public void WaveFileAudioSourceConstructorTest()
        {
            string fileName = @"..\..\..\Accord.Tests\Accord.Tests.Audio\Resources\Grand Piano - Fazioli - major A middle.wav";

            WaveFileAudioSource target = new WaveFileAudioSource(fileName);

            Signal s = null;

            target.NewFrame += delegate(object sender, NewFrameEventArgs e)
            {
                if (s == null)
                    s = e.Signal;

                Assert.AreEqual(s.SampleRate, 44100);
                Assert.AreEqual(s.Channels, 2);
                Assert.AreEqual(s.Length, 8192);
                Assert.AreEqual(s.Channels * s.Length, s.Samples);
            };


            target.Start();

            target.WaitForStop();

            Assert.AreEqual(180224, target.FramesReceived);
            Assert.AreEqual(705600, target.BytesReceived);

        }

    }
}
