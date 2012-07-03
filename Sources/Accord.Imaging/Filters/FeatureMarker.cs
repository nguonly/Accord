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

namespace Accord.Imaging.Filters
{
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    ///   Filter to mark (highlight) feature points in a image.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>The filter highlights feature points on the image using a given set of points.</para>
    /// 
    /// <para>The filter accepts 8 bpp grayscale and 24 color images for processing.</para>
    /// </remarks>
    /// 
    public class FeaturesMarker
    {

        private SurfPoint[] points;

        /// <summary>
        ///   Gets or sets the set of points to mark.
        /// </summary>
        /// 
        public SurfPoint[] Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="FeaturesMarker"/> class.
        /// </summary>
        /// 
        public FeaturesMarker(SurfPoint[] points)
        {
            this.points = points;
        }


        /// <summary>
        ///   Process the filter on the specified image.
        /// </summary>
        /// 
        /// <param name="image">Source image data.</param>
        ///
        public Bitmap Apply(Bitmap image)
        {
            image = AForge.Imaging.Image.Clone(image, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(image))
            using (Pen positive = new Pen(Color.Red))
            using (Pen negative = new Pen(Color.Blue))
            using (Pen line = new Pen(Color.FromArgb(0, 255, 0)))
            {
                // mark all points
                foreach (SurfPoint p in points)
                {
                    int S = 2 * (int)(2.5f * p.Scale);
                    int R = (int)(S / 2f);

                    Point pt = new Point((int)p.X, (int)p.Y);
                    Point ptR = new Point((int)(R * System.Math.Cos(p.Orientation)),
                                          (int)(R * System.Math.Sin(p.Orientation)));

                    Pen myPen = (p.Laplacian > 0 ? negative : positive);

                    g.DrawEllipse(myPen, pt.X - R, pt.Y - R, S, S);
                    g.DrawLine(line, new Point(pt.X, pt.Y), new Point(pt.X + ptR.X, pt.Y + ptR.Y));
                }
            }

            return image;
        }
    }
}