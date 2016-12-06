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

			bmpNew.Save("C:/Users/pjeon/Documents/hackathon/1_gray.png");
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

		public Bitmap BooleanEdgeDetectionFilter(
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

		public List<string> GetBooleanEdgeMasks()
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

		public Bitmap GetImage(byte[] resultBuffer, int width, int height)
		{
			Bitmap resultBitmap = new Bitmap(width, height);

			BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
									resultBitmap.Width, resultBitmap.Height),
									ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

			Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		public byte[] GetByteArray(Bitmap sourceBitmap)
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
}
