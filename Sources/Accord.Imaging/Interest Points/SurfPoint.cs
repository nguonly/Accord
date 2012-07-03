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

    /// <summary>
    ///   Speeded-Up Robust Feature (SURF) Point.
    /// </summary>
    /// 
    /// <seealso cref="SpeededUpRobustFeaturesDetector"/>
    /// <seealso cref="SurfDescriptor"/>
    /// 
    public class SurfPoint
    {

        /// <summary>
        ///   Initializes a new instance of the <see cref="SurfPoint"/> class.
        /// </summary>
        /// 
        /// <param name="x">The x-coordinate of the point in the image.</param>
        /// <param name="y">The y-coordinate of the point in the image.</param>
        /// <param name="scale">The point's scale.</param>
        /// <param name="laplacian">The point's laplacian value.</param>
        /// 
        public SurfPoint(float x, float y, float scale, int laplacian)
        {
            this.X = x;
            this.Y = y;
            this.Scale = scale;
            this.Laplacian = laplacian;
        }

        /// <summary>
        ///   Gets or sets the x-coordinate of this point.
        /// </summary>
        /// 
        public float X { get; set; }

        /// <summary>
        ///   Gets or sets the y-coordinate of this point.
        /// </summary>
        /// 
        public float Y { get; set; }

        /// <summary>
        ///   Gets or sets the scale of the point.
        /// </summary>
        /// 
        public float Scale { get; set; }

        /// <summary>
        ///   Gets or sets the response of the detected feature (strength).
        /// </summary>
        /// 
        public float Response { get; set; }

        /// <summary>
        ///   Gets or sets the orientation of this point
        ///   measured anti-clockwise from +ve x-axis.
        /// </summary>
        /// 
        public float Orientation { get; set; }

        /// <summary>
        ///   Gets or sets the sign of laplacian for this point
        ///   (which may be useful for fast matching purposes).
        /// </summary>
        /// 
        public int Laplacian { get; set; }

        /// <summary>
        ///   Gets or sets the descriptor vector
        ///   associated with this point.
        /// </summary>
        /// 
        public float[] Descriptor { get; set; }
    }
}
