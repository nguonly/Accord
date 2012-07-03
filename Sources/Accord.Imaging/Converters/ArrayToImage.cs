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
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using AForge.Imaging;

    /// <summary>
    ///   Array[] to Bitmap converter.
    /// </summary>
    /// 
    public class ArrayToImage :
        IConverter<double[], Bitmap>,
        IConverter<double[], UnmanagedImage>,
        IConverter<double[][], Bitmap>,
        IConverter<double[][], UnmanagedImage>
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
        ///   Gets or sets the height of the image
        ///   stored in the double array.
        /// </summary>
        /// 
        public int Height { get; set; }

        /// <summary>
        ///   Gets or sets the width of the image
        ///   stored in the double array.
        /// </summary>
        /// 
        public int Width { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ArrayToImage"/> class.
        /// </summary>
        /// 
        /// <param name="width">The width of the image to be created.</param>
        /// <param name="height">The height of the image to be created.</param>
        /// 
        public ArrayToImage(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Min = 0;
            this.Max = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayToImage"/> class.
        /// </summary>
        /// 
        /// <param name="width">The width of the image to be created.</param>
        /// <param name="height">The height of the image to be created.</param>
        /// <param name="min">
        ///   The minimum double value in the double array
        ///   associated with the darkest color. Default is 0.
        /// </param>
        /// <param name="max">
        ///   The maximum double value in the double array
        ///   associated with the brightest color. Default is 1.
        /// </param>
        /// 
        public ArrayToImage(int width, int height, double min, double max)
        {
            this.Width = width;
            this.Height = height;
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="pixels">The input image to be converted.</param>
        /// <param name="image">The converted image.</param>
        /// 
        public void Convert(double[] pixels, out Bitmap image)
        {
            image = AForge.Imaging.Image.CreateGrayscaleImage(Width, Height);

            BitmapData data = image.LockBits(new Rectangle(0, 0, Width, Height),
                ImageLockMode.WriteOnly, image.PixelFormat);

            int offset = data.Stride - Width;
            int src = 0;

            unsafe
            {
                byte* dst = (byte*)data.Scan0.ToPointer();

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++, src++, dst++)
                    {
                        *dst = (byte)Accord.Math.Tools.Scale(Min, Max, 0, 255, pixels[src]);
                    }
                    dst += offset;
                }
            }

            image.UnlockBits(data);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="pixels">The input image to be converted.</param>
        /// <param name="image">The converted image.</param>
        /// 
        public void Convert(double[] pixels, out UnmanagedImage image)
        {
            Bitmap bitmap;
            Convert(pixels, out bitmap);
            image = UnmanagedImage.FromManagedImage(bitmap);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="pixels">The input image to be converted.</param>
        /// <param name="image">The converted image.</param>
        /// 
        public void Convert(double[][] pixels, out Bitmap image)
        {
            PixelFormat format;
            int channels = pixels[0].Length;

            switch (channels)
            {
                case 1:
                    format = PixelFormat.Format8bppIndexed;
                    break;

                case 3:
                    format = PixelFormat.Format24bppRgb;
                    break;

                case 4:
                    format = PixelFormat.Format32bppArgb;
                    break;

                default:
                    throw new ArgumentException("Unsupported image pixel format.", "pixels");
            }


            image = new Bitmap(Width, Height, format);

            BitmapData data = image.LockBits(new Rectangle(0, 0, Width, Height),
                ImageLockMode.WriteOnly, format);

            int pixelSize = System.Drawing.Image.GetPixelFormatSize(format) / 8;
            int offset = data.Stride - Width * pixelSize;
            int src = 0;

            unsafe
            {
                byte* dst = (byte*)data.Scan0.ToPointer();

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++, src++)
                    {
                        for (int c = channels - 1; c >= 0; c--, dst++)
                        {
                            *dst = (byte)Accord.Math.Tools.Scale(Min, Max, 0, 255, pixels[src][c]);
                        }
                    }
                    dst += offset;
                }
            }

            image.UnlockBits(data);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="pixels">The input image to be converted.</param>
        /// <param name="image">The converted image.</param>
        /// 
        public void Convert(double[][] pixels, out UnmanagedImage image)
        {
            Bitmap bitmap;
            Convert(pixels, out bitmap);
            image = UnmanagedImage.FromManagedImage(bitmap);
        }

    }
}
