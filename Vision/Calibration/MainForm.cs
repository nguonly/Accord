// Accord.NET Sample Applications
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
// Note: this sample application links against the libfreenect library, distributed
// under the Apache 2 License. See libfreenect.txt in this folder for more details.
//

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Accord.Controls.Vision;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Video.Kinect;
using AForge.Imaging.Filters;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using AForge;
using Accord.Math;
using Accord.Imaging.Filters;
using Accord.Controls.Imaging;
using Accord.Statistics.Models.Regression.Linear;
using Accord.MachineLearning;
using Accord.Imaging.Converters;
using Accord.Math.Decompositions;
using Accord.Imaging;
using System.Threading;

namespace KinectCalibration
{
    public partial class MainForm : Form
    {

        private Kinect kinectDevice = null;
        private KinectVideoCamera videoCamera = null;
        private KinectDepthCamera depthCamera = null;

        private Bitmap img1;
        private Bitmap img2;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            toolStripStatusLabel1.Text = "Please chose a camera to begin";
        }



        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void btnSelectCamera_Click(object sender, EventArgs e)
        {
            VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                int deviceId = form.DeviceId;

                kinectDevice = Kinect.GetDevice(deviceId);

                if (videoCamera == null)
                {
                    videoCamera = kinectDevice.GetVideoCamera();
                    videoCamera.CameraMode = VideoCameraMode.Color;
                    videoCamera.NewFrame += new AForge.Video.NewFrameEventHandler(videoCamera_NewFrame);
                    videoCamera.Start();
                }

                if (depthCamera == null)
                {
                    depthCamera = new KinectDepthCamera(deviceId, CameraResolution.Medium,
                        provideOriginalDepthImage: true);

                    videoSourcePlayer1.VideoSource = depthCamera;
                    videoSourcePlayer1.Start();
                }


                toolStripStatusLabel1.Text = "Initializing...";
            }
        }

        void vc2_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }


        void videoCamera_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap clone = (Bitmap)eventArgs.Frame.Clone();

            if (pbColor.InvokeRequired)
                pbColor.BeginInvoke((Action)(() => pbColor.Image = clone));
            else
                pbColor.Image = clone;
        }


        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            LevelsLinear16bpp levels = new LevelsLinear16bpp();
            levels.InGray = new IntRange(0, 1000);
            levels.OutGray = new IntRange(0, 65535);
            levels.ApplyInPlace(image);

            if (homography != null)
            {
                Bitmap img = AForge.Imaging.Image.Convert16bppTo8bpp(image);
                Rectification rect = new Rectification(homography.Inverse());
                image = rect.Apply(img);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            img1 = videoSourcePlayer1.GetCurrentVideoFrame();
            img2 = (Bitmap)pbColor.Image.Clone();

            if (img1.PixelFormat != PixelFormat.Format8bppIndexed)
                img1 = AForge.Imaging.Image.Convert16bppTo8bpp(img1);
            img2 = Grayscale.CommonAlgorithms.BT709.Apply(img2);

            MatchForm form = new MatchForm();

            form.pbStill1.Image = img1;
            form.pbStill2.Image = img2;

            form.ShowDialog();

            correlationPoints1 = form.correlationPoints1.ToArray();
            correlationPoints2 = form.correlationPoints2.ToArray();
        }


        private IntPoint[] correlationPoints1;
        private IntPoint[] correlationPoints2;

        private MatrixH homography;


        private void button2_Click(object sender, EventArgs e)
        {
            // Step 3: Create the homography matrix using a robust estimator
            RansacHomographyEstimator ransac = new RansacHomographyEstimator(0.001, 0.99);
            homography = ransac.Estimate(correlationPoints1, correlationPoints2);

            // Plot RANSAC results against correlation results
            IntPoint[] inliers1 = correlationPoints1.Submatrix(ransac.Inliers);
            IntPoint[] inliers2 = correlationPoints2.Submatrix(ransac.Inliers);

            // Concatenate the two images in a single image (just to show on screen)
            Concatenate concat = new Concatenate(img1);
            Bitmap img3 = concat.Apply(img2);

            // Show the marked correlations in the concatenated image
            PairsMarker pairs = new PairsMarker(
                inliers1, // Add image1's width to the X points to show the markings correctly
                inliers2.Apply(p => new IntPoint(p.X + img1.Width, p.Y)));

            pictureBox1.Image = pairs.Apply(img3);

            numA.Value = (decimal)homography.Elements[0];
            numB.Value = (decimal)homography.Elements[1];
            numC.Value = (decimal)homography.Elements[2];

            numD.Value = (decimal)homography.Elements[3];
            numE.Value = (decimal)homography.Elements[4];
            numF.Value = (decimal)homography.Elements[5];

            numG.Value = (decimal)homography.Elements[6];
            numH.Value = (decimal)homography.Elements[7];
        }



    }
}
