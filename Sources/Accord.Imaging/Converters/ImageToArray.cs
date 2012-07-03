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
    ///   Bitmap to double[] converter.
    /// </summary>
    /// 
    public class ImageToArray :
        IConverter<Bitmap, double[]>,
        IConverter<UnmanagedImage, double[]>,
        IConverter<Bitmap, double[][]>,
        IConverter<UnmanagedImage, double[][]>
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
        ///   Gets or sets the channel to be extracted.
        /// </summary>
        /// 
        public int Channel { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ImageToArray"/> class.
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
        /// <param name="channel">The channel to extract. Default is 0.</param>
        ///   
        public ImageToArray(double min, double max, int channel)
        {
            this.Min = min;
            this.Max = max;
            this.Channel = channel;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ImageToArray"/> class.
        /// </summary>
        /// 
        public ImageToArray() : this(0, 1) { }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ImageToArray"/> class.
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
        public ImageToArray(double min, double max) : this(min, max, 0) { }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="image">The input image to be converted.</param>
        /// <param name="pixels">The converted image.</param>
        /// 
        public void Convert(Bitmap image, out double[][] pixels)
        {
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, image.PixelFormat);

            Convert(new UnmanagedImage(bitmapData), out pixels);

            image.UnlockBits(bitmapData);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="image">The input image to be converted.</param>
        /// <param name="pixels">The converted image.</param>
        /// 
        public void Convert(Bitmap image, out double[] pixels)
        {
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, image.PixelFormat);

            Convert(new UnmanagedImage(bitmapData), out pixels);

            image.UnlockBits(bitmapData);
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="image">The input image to be converted.</param>
        /// <param name="pixels">The converted image.</param>
        /// 
        public void Convert(UnmanagedImage image, out double[][] pixels)
        {
            int width = image.Width;
            int height = image.Height;
            int pixelSize = System.Drawing.Image.GetPixelFormatSize(image.PixelFormat) / 8;
            int offset = image.Stride - image.Width * pixelSize;

            pixels = new double[width * height][];

            unsafe
            {
                byte* src = (byte*)image.ImageData.ToPointer();
                int dst = 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, dst++)
                    {
                        double[] pixel = pixels[dst] = new double[pixelSize];
                        for (int i = pixel.Length - 1; i >= 0; i--, src++)
                            pixel[i] = Accord.Math.Tools.Scale(0, 255, Min, Max, *src);
                    }
                    src += offset;
                }
            }
        }

        /// <summary>
        ///   Converts an image from one representation to another.
        /// </summary>
        /// 
        /// <param name="image">The input image to be converted.</param>
        /// <param name="pixels">The converted image.</param>
        /// 
        public void Convert(UnmanagedImage image, out double[] pixels)
        {
            int width = image.Width;
            int height = image.Height;
            int pixelSize = System.Drawing.Image.GetPixelFormatSize(image.PixelFormat) / 8;
            int offset = image.Stride - image.Width * pixelSize;

            pixels = new double[width * height];

            unsafe
            {
                if (image.PixelFormat == PixelFormat.Format16bppGrayScale)
                {
                    short* src = (short*)image.ImageData.ToPointer();
                    int dst = 0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++, dst++, src ++)
                            pixels[dst] = Accord.Math.Tools.Scale(0, 65535, Min, Max, *src);

                        src += offset;
                    }
                }
                else
                {
                    byte* src = (byte*)image.ImageData.ToPointer();
                    int dst = 0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++, dst++, src += pixelSize)
                            pixels[dst] = Accord.Math.Tools.Scale(0, 255, Min, Max, *src);

                        src += offset;
                    }
                }
            }
        }

    }
}
