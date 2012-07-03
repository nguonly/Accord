// Accord Imaging Library
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © Christopher Evans, 2009-2011
// http://www.chrisevansdev.com/
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

namespace Accord.Imaging
{
    using System.Collections.Generic;
    using AForge.Imaging;

    /// <summary>
    ///   Speeded-Up Robust Features (SURF) Descriptor.
    /// </summary>
    /// 
    /// <seealso cref="SpeededUpRobustFeaturesDetector"/>
    /// <seealso cref="SurfPoint"/>
    ///
    public class SurfDescriptor
    {

        private bool invariant = true;
        private bool extended = false;
        private IntegralImage integral;

        /// <summary>
        ///   Gets or sets a value indicating whether the features
        ///   described by this <see cref="SurfDescriptor"/> should
        ///   be invariant to rotation. Default is true.
        /// </summary>
        /// 
        /// <value><c>true</c> for rotation invariant features; <c>false</c> otherwise.</value>
        /// 
        public bool Invariant
        {
            get { return invariant; }
            set { invariant = value; }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether the features
        ///   described by this <see cref="SurfDescriptor"/> should
        ///   be computed in extended form. Default is false.
        /// </summary>
        /// 
        /// <value><c>true</c> for extended features; <c>false</c> otherwise.</value>
        /// 
        public bool Extended
        {
            get { return extended; }
            set { extended = value; }
        }

        /// <summary>
        ///   Gets the <see cref="IntegralImage"/> of
        ///   the original source's feature detector.
        /// </summary>
        /// 
        /// <value>The integral image from where the
        /// features have been detected.</value>
        /// 
        public IntegralImage Image
        {
            get { return integral; }
        }


        /// <summary>
        ///   Initializes a new instance of the <see cref="SurfDescriptor"/> class.
        /// </summary>
        /// 
        /// <param name="integralImage">
        ///   The integral image which is the source of the feature points.
        /// </param>
        /// 
        public SurfDescriptor(IntegralImage integralImage)
        {
            this.integral = integralImage;
        }


        /// <summary>
        ///   Describes the specified point (i.e. computes and
        ///   sets the orientation and descriptor vector fields
        ///   of the <see cref="SurfPoint"/>.
        /// </summary>
        /// 
        /// <param name="point">The point to be described.</param>
        /// 
        public void Describe(SurfPoint point)
        {
            // Get rounded feature point data
            int x = (int)System.Math.Round(point.X, 0);
            int y = (int)System.Math.Round(point.Y, 0);
            int s = (int)System.Math.Round(point.Scale, 0);

            if (this.invariant)
            {
                // Get the orientation (for rotation invariance)
                point.Orientation = this.GetOrientation(x, y, s);
            }

            // Extract SURF descriptor
            point.Descriptor = this.GetDescriptor(x, y, s, point.Orientation);
        }

        /// <summary>
        ///   Describes all specified points (i.e. computes and
        ///   sets the orientation and descriptor vector fields
        ///   of each <see cref="SurfPoint"/>.
        /// </summary>
        /// 
        /// <param name="points">The list of points to be described.</param>
        /// 
        public void Describe(IEnumerable<SurfPoint> points)
        {
            foreach (SurfPoint point in points)
            {
                Describe(point);
            }
        }


        /// <summary>
        ///   Determine dominant orientation for InterestPoint
        /// </summary>
        /// 
        public float GetOrientation(int x, int y, int scale)
        {
            const byte responses = 109;
            float[] resX = new float[responses];
            float[] resY = new float[responses];
            float[] ang = new float[responses];
            int[] id = { 6, 5, 4, 3, 2, 1, 0, 1, 2, 3, 4, 5, 6 };

            // Calculate Haar responses for points within radius of 6*scale
            for (int i = -6, idx = 0; i <= 6; i++)
            {
                for (int j = -6; j <= 6; j++)
                {
                    if (i * i + j * j < 36)
                    {
                        float g = gauss25[id[i + 6], id[j + 6]];
                        resX[idx] = g * haarX(y + j * scale, x + i * scale, 4 * scale);
                        resY[idx] = g * haarY(y + j * scale, x + i * scale, 4 * scale);
                        ang[idx] = Math.Tools.Angle(resX[idx], resY[idx]);
                        idx++;
                    }
                }
            }

            // Calculate the dominant direction 
            float orientation = 0, max = 0;

            // Loop slides pi/3 window around feature point
            for (float ang1 = 0; ang1 < 2 * PI; ang1 += 0.15f)
            {
                float ang2 = (ang1 + PI / 3f > 2 * PI ? ang1 - 5 * PI / 3f : ang1 + PI / 3f);
                float sumX = 0;
                float sumY = 0;

                for (int k = 0; k < responses; k++)
                {
                    // determine whether the point is within the window
                    if (ang1 < ang2 && ang1 < ang[k] && ang[k] < ang2)
                    {
                        sumX += resX[k];
                        sumY += resY[k];
                    }
                    else if (ang2 < ang1 && 
                        ((ang[k] > 0 && ang[k] < ang2) || (ang[k] > ang1 && ang[k] < PI)))
                    {
                        sumX += resX[k];
                        sumY += resY[k];
                    }
                }

                // If the vector produced from this window is longer than all 
                // previous vectors then this forms the new dominant direction
                if (sumX * sumX + sumY * sumY > max)
                {
                    // store largest orientation
                    max = sumX * sumX + sumY * sumY;
                    orientation = Math.Tools.Angle(sumX, sumY);
                }
            }

            // Return orientation of the 
            // dominant response vector
            return orientation;
        }

        /// <summary>
        ///   Construct descriptor vector for this interest point
        /// </summary>
        /// 
        public float[] GetDescriptor(int x, int y, int scale, float orientation)
        {
            // Determine descriptor size
            float[] descriptor = (this.extended) ? new float[128] : new float[64];

            int count = 0;
            float cos, sin;
            float length = 0f;

            float cx = -0.5f; // Subregion centers for the
            float cy = +0.0f; // 4x4 gaussian weighting.

            if (!this.invariant)
            {
                cos = 1;
                sin = 0;
            }
            else
            {
                cos = (float)System.Math.Cos(orientation);
                sin = (float)System.Math.Sin(orientation);
            }

            // Calculate descriptor for this interest point
            int i = -8;
            while (i < 12)
            {
                int j = -8;
                i = i - 4;

                cx += 1f;
                cy = -0.5f;

                while (j < 12)
                {
                    cy += 1f;
                    j = j - 4;

                    int ix = i + 5;
                    int jx = j + 5;

                    int xs = (int)System.Math.Round(x + (-jx * scale * sin + ix * scale * cos), 0);
                    int ys = (int)System.Math.Round(y + (+jx * scale * cos + ix * scale * sin), 0);

                    // zero the responses
                    float dx = 0, dy = 0;
                    float mdx = 0, mdy = 0;
                    float dx_yn = 0, dy_xn = 0;
                    float mdx_yn = 0, mdy_xn = 0;

                    for (int k = i; k < i + 9; k++)
                    {
                        for (int l = j; l < j + 9; l++)
                        {
                            // Get coordinates of sample point on the rotated axis
                            int sample_x = (int)System.Math.Round(x + (-l * scale * sin + k * scale * cos), 0);
                            int sample_y = (int)System.Math.Round(y + (+l * scale * cos + k * scale * sin), 0);

                            // Get the gaussian weighted x and y responses
                            float gauss_s1 = gaussian(xs - sample_x, ys - sample_y, 2.5f * scale);
                            float rx = haarX(sample_y, sample_x, 2 * scale);
                            float ry = haarY(sample_y, sample_x, 2 * scale);

                            // Get the gaussian weighted x and y responses on rotated axis
                            float rrx = gauss_s1 * (-rx * sin + ry * cos);
                            float rry = gauss_s1 * (rx * cos + ry * sin);


                            if (this.extended)
                            {
                                // split x responses for different signs of y
                                if (rry >= 0)
                                {
                                    dx += rrx;
                                    mdx += System.Math.Abs(rrx);
                                }
                                else
                                {
                                    dx_yn += rrx;
                                    mdx_yn += System.Math.Abs(rrx);
                                }

                                // split y responses for different signs of x
                                if (rrx >= 0)
                                {
                                    dy += rry;
                                    mdy += System.Math.Abs(rry);
                                }
                                else
                                {
                                    dy_xn += rry;
                                    mdy_xn += System.Math.Abs(rry);
                                }
                            }
                            else
                            {
                                dx += rrx;
                                dy += rry;
                                mdx += System.Math.Abs(rrx);
                                mdy += System.Math.Abs(rry);
                            }
                        }
                    }

                    // Add the values to the descriptor vector
                    float gauss_s2 = gaussian(cx - 2f, cy - 2f, 1.5f);

                    descriptor[count++] = dx * gauss_s2;
                    descriptor[count++] = dy * gauss_s2;
                    descriptor[count++] = mdx * gauss_s2;
                    descriptor[count++] = mdy * gauss_s2;

                    // Add the extended components
                    if (this.extended)
                    {
                        descriptor[count++] = dx_yn * gauss_s2;
                        descriptor[count++] = dy_xn * gauss_s2;
                        descriptor[count++] = mdx_yn * gauss_s2;
                        descriptor[count++] = mdy_xn * gauss_s2;
                    }

                    length += (dx * dx + dy * dy + mdx * mdx + mdy * mdy
                          + dx_yn + dy_xn + mdx_yn + mdy_xn) * gauss_s2 * gauss_s2;

                    j += 9;
                }
                i += 9;
            }

            // Normalize to obtain an unitary vector
            length = (float)System.Math.Sqrt(length);

            if (length > 0)
            {
                for (int d = 0; d < descriptor.Length; ++d)
                    descriptor[d] /= length;
            }

            return descriptor;
        }

        private float haarX(int y, int x, int size)
        {
            int hsize = size / 2;
            int y1 = y - hsize;
            int y2 = y1 + size - 1;

            float a = integral.GetRectangleSum(x, y1, x + hsize - 1, y2);
            float b = integral.GetRectangleSum(x - hsize, y1, x - 1, y2);

            return (a - b) / 255f;
        }

        private float haarY(int y, int x, int size)
        {
            int hsize = size / 2;
            int x1 = x - hsize;
            int x2 = x1 + size - 1;

            float a = integral.GetRectangleSum(x1, y, x2, y + hsize - 1);
            float b = integral.GetRectangleSum(x1, y - hsize, x2, y - 1);

            return (a - b) / 255f;
        }



        #region Gaussian calculation

        private static float PI = (float)System.Math.PI;

        /// <summary>
        ///   Get the value of the gaussian with std dev sigma at the point (x,y)
        /// </summary>
        /// 
        private static float gaussian(int x, int y, float sigma)
        {
            return (1f / (2f * PI * sigma * sigma)) * (float)System.Math.Exp(-(x * x + y * y) / (2.0f * sigma * sigma));
        }

        /// <summary>
        ///   Get the value of the gaussian with std dev sigma at the point (x,y)
        /// </summary>
        private static float gaussian(float x, float y, float sigma)
        {
            return 1f / (2f * PI * sigma * sigma) * (float)System.Math.Exp(-(x * x + y * y) / (2.0f * sigma * sigma));
        }

        /// <summary>
        ///   Gaussian look-up table for sigma = 2.5
        /// </summary>
        /// 
        private static readonly float[,] gauss25 = 
        {
            { 0.02350693969273f, 0.01849121369071f, 0.01239503121241f, 0.00708015417522f, 0.00344628101733f, 0.00142945847484f, 0.00050524879060f},
            { 0.02169964028389f, 0.01706954162243f, 0.01144205592615f, 0.00653580605408f, 0.00318131834134f, 0.00131955648461f, 0.00046640341759f},
            { 0.01706954162243f, 0.01342737701584f, 0.00900063997939f, 0.00514124713667f, 0.00250251364222f, 0.00103799989504f, 0.00036688592278f},
            { 0.01144205592615f, 0.00900063997939f, 0.00603330940534f, 0.00344628101733f, 0.00167748505986f, 0.00069579213743f, 0.00024593098864f},
            { 0.00653580605408f, 0.00514124713667f, 0.00344628101733f, 0.00196854695367f, 0.00095819467066f, 0.00039744277546f, 0.00014047800980f},
            { 0.00318131834134f, 0.00250251364222f, 0.00167748505986f, 0.00095819467066f, 0.00046640341759f, 0.00019345616757f, 0.00006837798818f},
            { 0.00131955648461f, 0.00103799989504f, 0.00069579213743f, 0.00039744277546f, 0.00019345616757f, 0.00008024231247f, 0.00002836202103f}
        };

        #endregion


    }
}
