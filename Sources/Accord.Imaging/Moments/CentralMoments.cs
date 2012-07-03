// Accord Imaging Library
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

namespace Accord.Imaging.Moments
{
    using System;
    using System.Drawing;
    using AForge.Imaging;

    /// <summary>
    ///   Central image moments.
    /// </summary>
    /// 
    public class CentralMoments : IMoments
    {

        /// <summary>
        ///   Central moment of order (0,0).
        /// </summary>
        /// 
        public float Mu00 { get; private set; }

        /// <summary>
        ///   Central moment of order (1,0).
        /// </summary>
        /// 
        public float Mu10 { get; private set; }

        /// <summary>
        ///   Central moment of order (0,1).
        /// </summary>
        /// 
        public float Mu01 { get; private set; }

        /// <summary>
        ///   Central moment of order (1,1).
        /// </summary>
        /// 
        public float Mu11 { get; private set; }

        /// <summary>
        ///   Central moment of order (2,0).
        /// </summary>
        /// 
        public float Mu20 { get; private set; }

        /// <summary>
        ///   Central moment of order (0,2).
        /// </summary>
        /// 
        public float Mu02 { get; private set; }


        private float invM00;


        /// <summary>
        ///   Initializes a new instance of the <see cref="CentralMoments"/> class.
        /// </summary>
        /// 
        public CentralMoments()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="CentralMoments"/> class.
        /// </summary>
        /// 
        /// <param name="moments">The raw moments to construct central moments.</param>
        /// 
        public CentralMoments(RawMoments moments)
        {
            Compute(moments);
        }

        /// <summary>
        ///   Computes the center moments from the specified raw moments.
        /// </summary>
        /// 
        /// <param name="moments">The raw moments to use as base of calculations.</param>
        /// 
        public void Compute(RawMoments moments)
        {
            Mu00 = moments.M00;
            Mu01 = Mu10 = 0;

            Mu20 = moments.M20 - moments.M10 * moments.CenterX;
            Mu02 = moments.M02 - moments.M01 * moments.CenterY;
            Mu11 = moments.M11 - moments.M01 * moments.CenterX;

            invM00 = moments.InvM00;
        }

        /// <summary>
        ///   Computes the center moments for the specified image.
        /// </summary>
        /// 
        /// <param name="image">The image.</param>
        /// <param name="area">The region of interest in the image to compute moments for.</param>
        /// 
        public unsafe void Compute(float[,] image, Rectangle area)
        {
            RawMoments raw = new RawMoments();
            raw.Compute(image, area, true);
            this.Compute(raw);
        }

        /// <summary>
        ///   Computes the center moments for the specified image.
        /// </summary>
        /// 
        /// <param name="image">The image.</param>
        /// <param name="area">The region of interest in the image to compute moments for.</param>
        /// 
        public unsafe void Compute(UnmanagedImage image, Rectangle area)
        {
            RawMoments raw = new RawMoments();
            raw.Compute(image, area, true);
            this.Compute(raw);
        }

        /// <summary>
        ///   Gets the size of the ellipse containing the image.
        /// </summary>
        /// 
        /// <returns>The size of the ellipse containing the image.</returns>
        /// 
        public SizeF GetSize()
        {
            // Compute the covariance matrix
            //
            double a = Mu20 * invM00; //                | a    b |
            double b = Mu11 * invM00; //  Cov[I(x,y)] = |        |
            double c = Mu02 * invM00; //                | b    c |

            double d = a + c, e = a - c;
            double s = Math.Sqrt((4.0 * b * b) + (e * e));

            // Compute size
            return new SizeF((float)Math.Sqrt((d - s) * 0.5) * 4,
                             (float)Math.Sqrt((d + s) * 0.5) * 4);
        }

        /// <summary>
        ///   Gets the orientation of the ellipse containing the image.
        /// </summary>
        /// 
        /// <returns>The angle of orientation of the ellipse, in radians.</returns>
        /// 
        public float GetOrientation()
        {
            // Compute the covariance matrix
            //
            double a = Mu20 * invM00; //                | a    b |
            double b = Mu11 * invM00; //  Cov[I(x,y)] = |        |
            double c = Mu02 * invM00; //                | b    c |

            // Compute eigenvalues of the covariance matrix
            double d = a + c, e = a - c;
            double s = Math.Sqrt((4.0 * b * b) + (e * e));

            // Compute angle
            float angle = (float)Math.Atan2(2.0 * b, e + s);
            if (angle < 0) angle = (float)(angle + Math.PI);

            return angle;
        }

        /// <summary>
        ///   Gets both size and orientation of the ellipse containing the image.
        /// </summary>
        /// 
        /// <param name="angle">The angle of orientation of the ellipse, in radians.</param>
        /// <returns>The size of the ellipse containing the image.</returns>
        /// 
        public SizeF GetSizeAndOrientation(out float angle)
        {
            // Compute the covariance matrix
            //
            double a = Mu20 * invM00; //                | a    b |
            double b = Mu11 * invM00; //  Cov[I(x,y)] = |        |
            double c = Mu02 * invM00; //                | b    c |

            double d = a + c, e = a - c;
            double s = Math.Sqrt((4.0 * b * b) + (e * e));

            // Compute angle
            angle = (float)Math.Atan2(2.0 * b, e + s);
            if (angle < 0) angle = (float)(angle + Math.PI);

            // Compute size
            return new SizeF((float)Math.Sqrt((d - s) * 0.5) * 4,
                             (float)Math.Sqrt((d + s) * 0.5) * 4);
        }



    }
}
