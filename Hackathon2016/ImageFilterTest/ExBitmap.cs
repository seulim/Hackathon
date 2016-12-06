/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FilterControl
{
    public static class ExtBitmap
    {
        public static Bitmap CopyToSquareCanvas(this Bitmap sourceBitmap, int canvasWidthLenght)
        {
            float ratio = 1.0f;
            int maxSide = sourceBitmap.Width > sourceBitmap.Height ?
                          sourceBitmap.Width : sourceBitmap.Height;

            ratio = (float)maxSide / (float)canvasWidthLenght;

            Bitmap bitmapResult = (sourceBitmap.Width > sourceBitmap.Height ?
                                    new Bitmap(canvasWidthLenght, (int)(sourceBitmap.Height / ratio))
                                    : new Bitmap((int)(sourceBitmap.Width / ratio), canvasWidthLenght));

            using (Graphics graphicsResult = Graphics.FromImage(bitmapResult))
            {
                graphicsResult.CompositingQuality = CompositingQuality.HighQuality;
                graphicsResult.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsResult.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphicsResult.DrawImage(sourceBitmap,
                                        new Rectangle(0, 0,
                                            bitmapResult.Width, bitmapResult.Height),
                                        new Rectangle(0, 0,
                                            sourceBitmap.Width, sourceBitmap.Height),
                                            GraphicsUnit.Pixel);
                graphicsResult.Flush();
            }

            return bitmapResult;
        }

        public static Bitmap CartoonEffectFilter(
                                        this Bitmap sourceBitmap,
                                        byte threshold = 0,
                                        SmoothingFilterType smoothFilter
                                        = SmoothingFilterType.None)
        {
            sourceBitmap = sourceBitmap.SmoothingFilter(smoothFilter);

            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];

            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            int byteOffset = 0;
            int blueGradient, greenGradient, redGradient = 0;
            double blue = 0, green = 0, red = 0;

            bool exceedsThreshold = false;

            for (int offsetY = 1; offsetY <
                 sourceBitmap.Height - 1; offsetY++)
            {
                for (int offsetX = 1; offsetX <
                    sourceBitmap.Width - 1; offsetX++)
                {
                    byteOffset = offsetY * sourceData.Stride +
                                 offsetX * 4;

                    blueGradient =
                    Math.Abs(pixelBuffer[byteOffset - 4] -
                    pixelBuffer[byteOffset + 4]);

                    blueGradient +=
                    Math.Abs(pixelBuffer[byteOffset - sourceData.Stride] -
                    pixelBuffer[byteOffset + sourceData.Stride]);

                    byteOffset++;

                    greenGradient =
                    Math.Abs(pixelBuffer[byteOffset - 4] -
                    pixelBuffer[byteOffset + 4]);

                    greenGradient +=
                    Math.Abs(pixelBuffer[byteOffset - sourceData.Stride] -
                    pixelBuffer[byteOffset + sourceData.Stride]);

                    byteOffset++;

                    redGradient =
                    Math.Abs(pixelBuffer[byteOffset - 4] -
                    pixelBuffer[byteOffset + 4]);

                    redGradient +=
                    Math.Abs(pixelBuffer[byteOffset - sourceData.Stride] -
                    pixelBuffer[byteOffset + sourceData.Stride]);

                    if (blueGradient + greenGradient + redGradient > threshold)
                    { exceedsThreshold = true; }
                    else
                    {
                        byteOffset -= 2;

                        blueGradient = Math.Abs(pixelBuffer[byteOffset - 4] -
                                                pixelBuffer[byteOffset + 4]);
                        byteOffset++;

                        greenGradient = Math.Abs(pixelBuffer[byteOffset - 4] -
                                                 pixelBuffer[byteOffset + 4]);
                        byteOffset++;

                        redGradient = Math.Abs(pixelBuffer[byteOffset - 4] -
                                               pixelBuffer[byteOffset + 4]);

                        if (blueGradient + greenGradient + redGradient > threshold)
                        { exceedsThreshold = true; }
                        else
                        {
                            byteOffset -= 2;

                            blueGradient =
                            Math.Abs(pixelBuffer[byteOffset - sourceData.Stride] -
                            pixelBuffer[byteOffset + sourceData.Stride]);

                            byteOffset++;

                            greenGradient =
                            Math.Abs(pixelBuffer[byteOffset - sourceData.Stride] -
                            pixelBuffer[byteOffset + sourceData.Stride]);

                            byteOffset++;

                            redGradient =
                            Math.Abs(pixelBuffer[byteOffset - sourceData.Stride] -
                            pixelBuffer[byteOffset + sourceData.Stride]);

                            if (blueGradient + greenGradient +
                                      redGradient > threshold)
                            { exceedsThreshold = true; }
                            else
                            {
                                byteOffset -= 2;

                                blueGradient =
                                Math.Abs(pixelBuffer[byteOffset - 4 - sourceData.Stride] -
                                pixelBuffer[byteOffset + 4 + sourceData.Stride]);

                                blueGradient +=
                                Math.Abs(pixelBuffer[byteOffset - sourceData.Stride + 4] -
                                pixelBuffer[byteOffset + sourceData.Stride - 4]);

                                byteOffset++;

                                greenGradient =
                                Math.Abs(pixelBuffer[byteOffset - 4 - sourceData.Stride] -
                                pixelBuffer[byteOffset + 4 + sourceData.Stride]);

                                greenGradient +=
                                Math.Abs(pixelBuffer[byteOffset - sourceData.Stride + 4] -
                                pixelBuffer[byteOffset + sourceData.Stride - 4]);

                                byteOffset++;

                                redGradient =
                                Math.Abs(pixelBuffer[byteOffset - 4 - sourceData.Stride] -
                                pixelBuffer[byteOffset + 4 + sourceData.Stride]);

                                redGradient +=
                                Math.Abs(pixelBuffer[byteOffset - sourceData.Stride + 4] -
                                pixelBuffer[byteOffset + sourceData.Stride - 4]);

                                if (blueGradient + greenGradient + redGradient > threshold)
                                { exceedsThreshold = true; }
                                else
                                { exceedsThreshold = false; }
                            }
                        }
                    }

                    byteOffset -= 2;

                    if (exceedsThreshold)
                    {
                        blue = 0;
                        green = 0;
                        red = 0;
                    }
                    else
                    {
                        blue = pixelBuffer[byteOffset];
                        green = pixelBuffer[byteOffset + 1];
                        red = pixelBuffer[byteOffset + 2];
                    }

                    blue = (blue > 255 ? 255 :
                           (blue < 0 ? 0 :
                            blue));

                    green = (green > 255 ? 255 :
                            (green < 0 ? 0 :
                             green));

                    red = (red > 255 ? 255 :
                          (red < 0 ? 0 :
                           red));

                    resultBuffer[byteOffset] = (byte)blue;
                    resultBuffer[byteOffset + 1] = (byte)green;
                    resultBuffer[byteOffset + 2] = (byte)red;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);

            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap MedianFilter(this Bitmap sourceBitmap,
                                          int matrixSize)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];

            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    neighbourPixels.Clear();

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            neighbourPixels.Add(BitConverter.ToInt32(
                                             pixelBuffer, calcOffset));
                        }
                    }

                    neighbourPixels.Sort();

                    middlePixel = BitConverter.GetBytes(
                                       neighbourPixels[filterOffset]);

                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);

            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap SmoothingFilter(this Bitmap sourceBitmap,
                                    SmoothingFilterType smoothFilter =
                                    SmoothingFilterType.None)
        {
            Bitmap inputBitmap = null;

            switch (smoothFilter)
            {
                case SmoothingFilterType.None:
                    {
                        inputBitmap = sourceBitmap;
                    }
                    break;

                case SmoothingFilterType.Gaussian3x3:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                   Matrix.Gaussian3x3, 1.0 / 16.0, 0);
                    }
                    break;

                case SmoothingFilterType.Gaussian5x5:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                  Matrix.Gaussian5x5, 1.0 / 159.0, 0);
                    }
                    break;

                case SmoothingFilterType.Gaussian7x7:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                  Matrix.Gaussian7x7, 1.0 / 136.0, 0);
                    }
                    break;

                case SmoothingFilterType.Median3x3:
                    {
                        inputBitmap = sourceBitmap.MedianFilter(3);
                    }
                    break;

                case SmoothingFilterType.Median5x5:
                    {
                        inputBitmap = sourceBitmap.MedianFilter(5);
                    }
                    break;

                case SmoothingFilterType.Median7x7:
                    {
                        inputBitmap = sourceBitmap.MedianFilter(7);
                    }
                    break;

                case SmoothingFilterType.Median9x9:
                    {
                        inputBitmap = sourceBitmap.MedianFilter(9);
                    }
                    break;

                case SmoothingFilterType.Mean3x3:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                      Matrix.Mean3x3, 1.0 / 9.0, 0);
                    }
                    break;

                case SmoothingFilterType.Mean5x5:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                      Matrix.Mean5x5, 1.0 / 25.0, 0);
                    }
                    break;

                case SmoothingFilterType.LowPass3x3:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                      Matrix.LowPass3x3, 1.0 / 16.0, 0);
                    }
                    break;

                case SmoothingFilterType.LowPass5x5:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                      Matrix.LowPass5x5, 1.0 / 60.0, 0);
                    }
                    break;

                case SmoothingFilterType.Sharpen3x3:
                    {
                        inputBitmap = sourceBitmap.ConvolutionFilter(
                                      Matrix.Sharpen3x3, 1.0 / 8.0, 0);
                    }
                    break;
            }

            return inputBitmap;
        }

        private static Bitmap ConvolutionFilter(this Bitmap sourceBitmap,
                                                  double[,] filterMatrix,
                                                       double factor = 1,
                                                            int bias = 0)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    blue = (blue > 255 ? 255 :
                           (blue < 0 ? 0 :
                            blue));

                    green = (green > 255 ? 255 :
                            (green < 0 ? 0 :
                             green));

                    red = (red > 255 ? 255 :
                          (red < 0 ? 0 :
                           red));

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public enum SmoothingFilterType
        {
            None,
            Gaussian3x3,
            Gaussian5x5,
            Gaussian7x7,
            Median3x3,
            Median5x5,
            Median7x7,
            Median9x9,
            Mean3x3,
            Mean5x5,
            LowPass3x3,
            LowPass5x5,
            Sharpen3x3,
        }

        public static byte[] GetByteArray(this Bitmap sourceBitmap)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);

            byte[] sourceBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];

            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0,
                                       sourceBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            return sourceBuffer;
        }

        public static Bitmap GetImage(this byte[] resultBuffer, int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static List<string> GetBooleanEdgeMasks()
        {
            List<string> edgeMasks = new List<string>();

            edgeMasks.Add("011011011");
            edgeMasks.Add("000111111");
            edgeMasks.Add("110110110");
            edgeMasks.Add("111111000");
            edgeMasks.Add("011011001");
            edgeMasks.Add("100110110");
            edgeMasks.Add("111011000");
            edgeMasks.Add("111110000");
            edgeMasks.Add("111011001");
            edgeMasks.Add("100110111");
            edgeMasks.Add("001011111");
            edgeMasks.Add("111110100");
            edgeMasks.Add("000011111");
            edgeMasks.Add("000110111");
            edgeMasks.Add("001011011");
            edgeMasks.Add("110110100");

            return edgeMasks;
        }

        public static Bitmap FuzzyEdgeBlurFilter(this Bitmap sourceBitmap,
                                                 int filterSize,
                                                 float edgeFactor1,
                                                 float edgeFactor2)
        {
            return
            sourceBitmap.BooleanEdgeDetectionFilter(edgeFactor1).
            MeanFilter(filterSize).BooleanEdgeDetectionFilter(edgeFactor2);
        }

        public static Bitmap BooleanEdgeDetectionFilter(
               this Bitmap sourceBitmap, float edgeFactor)
        {
            byte[] pixelBuffer = sourceBitmap.GetByteArray();
            byte[] resultBuffer = new byte[pixelBuffer.Length];
            Buffer.BlockCopy(pixelBuffer, 0, resultBuffer,
                             0, pixelBuffer.Length);

            List<string> edgeMasks = GetBooleanEdgeMasks();

            int imageStride = sourceBitmap.Width * 4;
            int matrixMean = 0, pixelTotal = 0;
            int filterY = 0, filterX = 0, calcOffset = 0;
            string matrixPatern = String.Empty;

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                matrixPatern = String.Empty;
                matrixMean = 0; pixelTotal = 0;
                filterY = -1; filterX = -1;

                while (filterY < 2)
                {
                    calcOffset = k + (filterX * 4) +
                    (filterY * imageStride);

                    calcOffset = (calcOffset < 0 ? 0 :
                    (calcOffset >= pixelBuffer.Length - 2 ?
                    pixelBuffer.Length - 3 : calcOffset));

                    matrixMean += pixelBuffer[calcOffset];
                    matrixMean += pixelBuffer[calcOffset + 1];
                    matrixMean += pixelBuffer[calcOffset + 2];

                    filterX += 1;

                    if (filterX > 1)
                    { filterX = -1; filterY += 1; }
                }

                matrixMean = matrixMean / 9;
                filterY = -1; filterX = -1;

                while (filterY < 2)
                {
                    calcOffset = k + (filterX * 4) +
                    (filterY * imageStride);

                    calcOffset = (calcOffset < 0 ? 0 :
                    (calcOffset >= pixelBuffer.Length - 2 ?
                    pixelBuffer.Length - 3 : calcOffset));

                    pixelTotal = pixelBuffer[calcOffset];
                    pixelTotal += pixelBuffer[calcOffset + 1];
                    pixelTotal += pixelBuffer[calcOffset + 2];

                    matrixPatern += (pixelTotal > matrixMean
                                                 ? "1" : "0");
                    filterX += 1;

                    if (filterX > 1)
                    { filterX = -1; filterY += 1; }
                }

                if (edgeMasks.Contains(matrixPatern))
                {
                    resultBuffer[k] =
                    ClipByte(resultBuffer[k] * edgeFactor);

                    resultBuffer[k + 1] =
                    ClipByte(resultBuffer[k + 1] * edgeFactor);

                    resultBuffer[k + 2] =
                    ClipByte(resultBuffer[k + 2] * edgeFactor);
                }
            }

            return resultBuffer.GetImage(sourceBitmap.Width, sourceBitmap.Height);
        }

        private static Bitmap MeanFilter(this Bitmap sourceBitmap,
                                            int meanSize)
        {
            byte[] pixelBuffer = sourceBitmap.GetByteArray();
            byte[] resultBuffer = new byte[pixelBuffer.Length];

            double blue = 0.0, green = 0.0, red = 0.0;
            double factor = 1.0 / (meanSize * meanSize);

            int imageStride = sourceBitmap.Width * 4;
            int filterOffset = meanSize / 2;
            int calcOffset = 0, filterY = 0, filterX = 0;

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = 0; green = 0; red = 0;
                filterY = -filterOffset;
                filterX = -filterOffset;

                while (filterY <= filterOffset)
                {
                    calcOffset = k + (filterX * 4) +
                    (filterY * imageStride);

                    calcOffset = (calcOffset < 0 ? 0 :
                    (calcOffset >= pixelBuffer.Length - 2 ?
                    pixelBuffer.Length - 3 : calcOffset));

                    blue += pixelBuffer[calcOffset];
                    green += pixelBuffer[calcOffset + 1];
                    red += pixelBuffer[calcOffset + 2];

                    filterX++;

                    if (filterX > filterOffset)
                    {
                        filterX = -filterOffset;
                        filterY++;
                    }
                }

                resultBuffer[k] = ClipByte(factor * blue);
                resultBuffer[k + 1] = ClipByte(factor * green);
                resultBuffer[k + 2] = ClipByte(factor * red);
                resultBuffer[k + 3] = 255;
            }

            return resultBuffer.GetImage(sourceBitmap.Width, sourceBitmap.Height);
        }

        private static byte ClipByte(double colour)
        {
            return (byte)(colour > 255 ? 255 :
                   (colour < 0 ? 0 : colour));
        }

        public enum BlurType
        {
            Mean3x3,
            Mean5x5,
            Mean7x7,
            Mean9x9,
            GaussianBlur3x3,
            GaussianBlur5x5,
            MotionBlur5x5,
            MotionBlur5x5At45Degrees,
            MotionBlur5x5At135Degrees,
            MotionBlur7x7,
            MotionBlur7x7At45Degrees,
            MotionBlur7x7At135Degrees,
            MotionBlur9x9,
            MotionBlur9x9At45Degrees,
            MotionBlur9x9At135Degrees,
            Median3x3,
            Median5x5,
            Median7x7,
            Median9x9,
            Median11x11
        }

        public static Bitmap ImageBlurFilter(this Bitmap sourceBitmap,
                                                   BlurType blurType)
        {
            Bitmap resultBitmap = null;

            switch (blurType)
            {
                case BlurType.Mean3x3:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                                       Matrix.Mean3x3, 1.0 / 9.0, 0);
                    }
                    break;

                case BlurType.Mean5x5:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                                       Matrix.Mean5x5, 1.0 / 25.0, 0);
                    }
                    break;

                case BlurType.Mean7x7:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                                       Matrix.Mean7x7, 1.0 / 49.0, 0);
                    }
                    break;

                case BlurType.Mean9x9:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                                       Matrix.Mean9x9, 1.0 / 81.0, 0);
                    }
                    break;

                case BlurType.GaussianBlur3x3:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                                Matrix.GaussianBlur3x3, 1.0 / 16.0, 0);
                    }
                    break;

                case BlurType.GaussianBlur5x5:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                               Matrix.GaussianBlur5x5, 1.0 / 159.0, 0);
                    }
                    break;

                case BlurType.MotionBlur5x5:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                                   Matrix.MotionBlur5x5, 1.0 / 10.0, 0);
                    }
                    break;

                case BlurType.MotionBlur5x5At45Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur5x5At45Degrees, 1.0 / 5.0, 0);
                    }
                    break;

                case BlurType.MotionBlur5x5At135Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur5x5At135Degrees, 1.0 / 5.0, 0);
                    }
                    break;

                case BlurType.MotionBlur7x7:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur7x7, 1.0 / 14.0, 0);
                    }
                    break;

                case BlurType.MotionBlur7x7At45Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur7x7At45Degrees, 1.0 / 7.0, 0);
                    }
                    break;

                case BlurType.MotionBlur7x7At135Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur7x7At135Degrees, 1.0 / 7.0, 0);
                    }
                    break;

                case BlurType.MotionBlur9x9:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur9x9, 1.0 / 18.0, 0);
                    }
                    break;

                case BlurType.MotionBlur9x9At45Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur9x9At45Degrees, 1.0 / 9.0, 0);
                    }
                    break;

                case BlurType.MotionBlur9x9At135Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilter(
                        Matrix.MotionBlur9x9At135Degrees, 1.0 / 9.0, 0);
                    }
                    break;

                case BlurType.Median3x3:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(3);
                    }
                    break;

                case BlurType.Median5x5:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(5);
                    }
                    break;

                case BlurType.Median7x7:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(7);
                    }
                    break;

                case BlurType.Median9x9:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(9);
                    }
                    break;

                case BlurType.Median11x11:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(11);
                    }
                    break;
            }

            return resultBitmap;
        }
    }
}