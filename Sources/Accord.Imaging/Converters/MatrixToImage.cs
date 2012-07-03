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

namespace Accord.Imaging.Converters
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using AForge.Imaging;

    /// <summary>
    ///   Double[,] to Bitmap converter.
    /// </summary>
    /// 
    public class MatrixToImage :
        IConverter<double[,], Bitmap>,
        IConverter<double[,], UnmanagedImage>,
        IConverter<byte[,], Bitmap>,
        IConverter<byte[,], UnmanagedImage>
    {

        /// <summary>
        ///   Gets or sets the maximum double value in the
        ///   double array associated with the brightest color.
        /// </summary>
        /// 
        public double Max { get; set; }

        /// <summary>
        ///   Gets or sets the minimum double value in the
        ///   double array associated with the darkest color.
        /// </summary>
        /// 
        public double Min { get; set; }


        /// <summary>
        ///   Initializes a new instance of the <see cref="MatrixToImage"/> class.
        /// </summary>
        /// 
        /// <param name="min">
        ///   The minimum double value in the double array
        ///   associated with the darkest color. Default is 0.
        /// </param>
        /// <param name="max">
        ///   The maximum double value in the double array
        ///   associated with the brightest color. Default is 1.
        /// </param>
        ///   
        public MatrixToImage(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="MatrixToImage"/> class.
        /// </summary>
        /// 
        public MatrixToImage() : this(0, 1) { }


        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="input">The input image to be converted.</param>
        /// <param name="output">The converted image.</param>
        /// 
        public void Convert(double[,] input, out Bitmap output)
        {
            int width = input.GetLength(1);
            int height = input.GetLength(0);

            output = AForge.Imaging.Image.CreateGrayscaleImage(width, height);

            BitmapData data = output.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, output.PixelFormat);

            int offset = data.Stride - width;

            unsafe
            {
                byte* dst = (byte*)data.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, dst++)
                        *dst = (byte)Accord.Math.Tools.Scale(Min, Max, 0, 255, input[y, x]);

                    dst += offset;
                }
            }

            output.UnlockBits(data);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="input">The input image to be converted.</param>
        /// <param name="output">The converted image.</param>
        /// 
        public void Convert(double[,] input, out UnmanagedImage output)
        {
            Bitmap image;

            Convert(input, out image);

            output = UnmanagedImage.FromManagedImage(image);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="input">The input image to be converted.</param>
        /// <param name="bitmap">The converted image.</param>
        /// 
        public void Convert(byte[,] input, out UnmanagedImage bitmap)
        {
            Bitmap image;

            Convert(input, out image);

            bitmap = UnmanagedImage.FromManagedImage(image);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="input">The input image to be converted.</param>
        /// <param name="bitmap">The converted image.</param>
        /// 
        public void Convert(byte[,] input, out Bitmap bitmap)
        {
            int width = input.GetLength(1);
            int height = input.GetLength(0);

            bitmap = AForge.Imaging.Image.CreateGrayscaleImage(width, height);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int offset = data.Stride - width;

            unsafe
            {
                byte* dst = (byte*)data.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, dst++)
                        *dst = (byte)input[y, x];

                    dst += offset;
                }
            }

            bitmap.UnlockBits(data);
        }

    }
}
