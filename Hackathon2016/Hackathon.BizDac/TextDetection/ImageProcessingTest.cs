using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hackathon.BizDac
{
	public class ImageProcessingTest
	{
		private Bitmap GetArgbCopy(Bitmap sourceImage)
		{
			Bitmap bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height);

			using (Graphics graphics = Graphics.FromImage(bmpNew))
			{
				graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
				graphics.Flush();
			}

			return bmpNew;
		}

		/// <summary>
		/// 흑백효과
		/// </summary>
		/// <param name="filepath"></param>
		/// <returns></returns>
		public Bitmap CopyAsGrayscale(string filepath)
		{
			Bitmap sourceImage = new Bitmap(filepath);
			Bitmap bmpNew = GetArgbCopy(sourceImage);
			BitmapData bmpData = bmpNew.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

			IntPtr ptr = bmpData.Scan0;

			byte[] byteBuffer = new byte[bmpData.Stride * bmpNew.Height];

			Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);

			float rgb = 0;

			for (int k = 0; k < byteBuffer.Length; k += 4)
			{
				rgb = byteBuffer[k] * 0.11f;
				rgb += byteBuffer[k + 1] * 0.59f;
				rgb += byteBuffer[k + 2] * 0.3f;

				byteBuffer[k] = (byte)rgb;
				byteBuffer[k + 1] = byteBuffer[k];
				byteBuffer[k + 2] = byteBuffer[k];

				byteBuffer[k + 3] = 255;
			}

			Marshal.Copy(byteBuffer, 0, ptr, byteBuffer.Length);

			bmpNew.UnlockBits(bmpData);

			bmpData = null;
			byteBuffer = null;

			//bmpNew.Save("C:/Users/pjeon/Documents/hackathon/1_gray.png");
			return bmpNew;
		}

		/// <summary>
		/// 반전효과
		/// </summary>
		/// <param name="sourceImage"></param>
		/// <returns></returns>
		public Bitmap CopyAsNegative(Bitmap sourceImage)
		{
			Bitmap bmpNew = GetArgbCopy(sourceImage);
			BitmapData bmpData = bmpNew.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

			IntPtr ptr = bmpData.Scan0;

			byte[] byteBuffer = new byte[bmpData.Stride * bmpNew.Height];

			Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);
			byte[] pixelBuffer = null;

			int pixel = 0;

			for (int k = 0; k < byteBuffer.Length; k += 4)
			{
				pixel = ~BitConverter.ToInt32(byteBuffer, k);
				pixelBuffer = BitConverter.GetBytes(pixel);

				byteBuffer[k] = pixelBuffer[0];
				byteBuffer[k + 1] = pixelBuffer[1];
				byteBuffer[k + 2] = pixelBuffer[2];
			}

			Marshal.Copy(byteBuffer, 0, ptr, byteBuffer.Length);

			bmpNew.UnlockBits(bmpData);

			bmpData = null;
			byteBuffer = null;

			return bmpNew;
		}

		/// <summary>
		/// 모서리효과
		/// </summary>
		/// <param name="sourceImage"></param>
		/// <returns></returns>
		public Bitmap FuzzyEdgeBlurFilter(Bitmap sourceImage)
		{
			return FuzzyEdgeBlurFilter(sourceImage, 3, (float)0.5, 2);
		}

		public Bitmap FuzzyEdgeBlurFilter(Bitmap sourceBitmap,
												 int filterSize,
												 float edgeFactor1,
												 float edgeFactor2)
		{
			Bitmap b = BooleanEdgeDetectionFilter(sourceBitmap, edgeFactor1);
			Bitmap c = MeanFilter(b, filterSize);

			return BooleanEdgeDetectionFilter(c, edgeFactor2);

			//return
			//	BooleanEdgeDetectionFilter(sourceBitmap, edgeFactor1).
			//	MeanFilter(filterSize).BooleanEdgeDetectionFilter(edgeFactor2);
		}
		
		/// <summary>
		/// 노이즈 제거
		/// </summary>
		/// <param name="bmap"></param>
		/// <returns></returns>
		public Bitmap RemoveNoise(Bitmap bmap)
		{

			for (var x = 0; x < bmap.Width; x++)
			{
				for (var y = 0; y < bmap.Height; y++)
				{
					var pixel = bmap.GetPixel(x, y);
					if (pixel.R < 162 && pixel.G < 162 && pixel.B < 162)
						bmap.SetPixel(x, y, Color.Black);
				}
			}

			for (var x = 0; x < bmap.Width; x++)
			{
				for (var y = 0; y < bmap.Height; y++)
				{
					var pixel = bmap.GetPixel(x, y);
					if (pixel.R > 162 && pixel.G > 162 && pixel.B > 162)
						bmap.SetPixel(x, y, Color.White);
				}
			}

			return bmap;
		}
		/// <summary>
		/// 블러
		/// </summary>
		/// <param name="sourceImage"></param>
		/// <returns></returns>
		public Bitmap GaussianBlur(Bitmap sourceImage)
		{
			return ImageBlurFilter(sourceImage, BlurType.Mean9x9);
		}


		#region helper
		private Bitmap ImageBlurFilter(Bitmap sourceBitmap,
												  BlurType blurType)
		{
			Bitmap resultBitmap = null;

			switch (blurType)
			{
				case BlurType.Mean3x3:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   new Matrix().Mean3x3, 1.0 / 9.0, 0);
					}
					break;

				case BlurType.Mean5x5:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   new Matrix().Mean5x5, 1.0 / 25.0, 0);
					}
					break;

				case BlurType.Mean7x7:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   new Matrix().Mean7x7, 1.0 / 49.0, 0);
					}
					break;

				case BlurType.Mean9x9:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   new Matrix().Mean9x9, 1.0 / 81.0, 0);
					}
					break;

				case BlurType.GaussianBlur3x3:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
								new Matrix().GaussianBlur3x3, 1.0 / 16.0, 0);
					}
					break;

				case BlurType.GaussianBlur5x5:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
							   new Matrix().GaussianBlur5x5, 1.0 / 159.0, 0);
					}
					break;

				case BlurType.MotionBlur5x5:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
								   new Matrix().MotionBlur5x5, 1.0 / 10.0, 0);
					}
					break;

				case BlurType.MotionBlur5x5At45Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						new Matrix().MotionBlur5x5At45Degrees, 1.0 / 5.0, 0);
					}
					break;

				case BlurType.MotionBlur5x5At135Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						new Matrix().MotionBlur5x5At135Degrees, 1.0 / 5.0, 0);
					}
					break;

				case BlurType.MotionBlur7x7:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						new Matrix().MotionBlur7x7, 1.0 / 14.0, 0);
					}
					break;

				//case BlurType.MotionBlur7x7At45Degrees:
				//	{
				//		resultBitmap = sourceBitmap.ConvolutionFilter(
				//		Matrix.MotionBlur7x7At45Degrees, 1.0 / 7.0, 0);
				//	}
				//	break;

				//case BlurType.MotionBlur7x7At135Degrees:
				//	{
				//		resultBitmap = sourceBitmap.ConvolutionFilter(
				//		Matrix.MotionBlur7x7At135Degrees, 1.0 / 7.0, 0);
				//	}
				//	break;

				//case BlurType.MotionBlur9x9:
				//	{
				//		resultBitmap = sourceBitmap.ConvolutionFilter(
				//		Matrix.MotionBlur9x9, 1.0 / 18.0, 0);
				//	}
				//	break;

				//case BlurType.MotionBlur9x9At45Degrees:
				//	{
				//		resultBitmap = sourceBitmap.ConvolutionFilter(
				//		Matrix.MotionBlur9x9At45Degrees, 1.0 / 9.0, 0);
				//	}
				//	break;

				//case BlurType.MotionBlur9x9At135Degrees:
				//	{
				//		resultBitmap = sourceBitmap.ConvolutionFilter(
				//		Matrix.MotionBlur9x9At135Degrees, 1.0 / 9.0, 0);
				//	}
				//	break;

				//case BlurType.Median3x3:
				//	{
				//		resultBitmap = sourceBitmap.MedianFilter(3);
				//	}
				//	break;

				//case BlurType.Median5x5:
				//	{
				//		resultBitmap = sourceBitmap.MedianFilter(5);
				//	}
				//	break;

				//case BlurType.Median7x7:
				//	{
				//		resultBitmap = sourceBitmap.MedianFilter(7);
				//	}
				//	break;

				//case BlurType.Median9x9:
				//	{
				//		resultBitmap = sourceBitmap.MedianFilter(9);
				//	}
				//	break;

				//case BlurType.Median11x11:
				//	{
				//		resultBitmap = sourceBitmap.MedianFilter(11);
				//	}
					break;
			}

			return resultBitmap;
		}
		private  Bitmap ConvolutionFilter(Bitmap sourceBitmap,
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

		private enum BlurType
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

		private Bitmap BooleanEdgeDetectionFilter(
			   Bitmap sourceBitmap, float edgeFactor)
		{
			byte[] pixelBuffer = GetByteArray(sourceBitmap);
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

			return GetImage(resultBuffer, sourceBitmap.Width, sourceBitmap.Height);
		}

		private List<string> GetBooleanEdgeMasks()
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

		private byte ClipByte(double colour)
		{
			return (byte)(colour > 255 ? 255 :
				   (colour < 0 ? 0 : colour));
		}

		private Bitmap GetImage(byte[] resultBuffer, int width, int height)
		{
			Bitmap resultBitmap = new Bitmap(width, height);

			BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
									resultBitmap.Width, resultBitmap.Height),
									ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

			Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		private byte[] GetByteArray(Bitmap sourceBitmap)
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

		private Bitmap MeanFilter(Bitmap sourceBitmap,
										   int meanSize)
		{
			byte[] pixelBuffer = GetByteArray(sourceBitmap);
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

			return GetImage(resultBuffer, sourceBitmap.Width, sourceBitmap.Height);
		}
	}
	#endregion
}
