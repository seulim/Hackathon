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
	public class EdgeDetection
	{

		/// <summary>
		/// Sobel is 3x3,
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public Bitmap DoSobelEdgeDetection(Bitmap original)
		{
			Bitmap b = original;
			Bitmap bb = original;
			int width = b.Width;
			int height = b.Height;
			int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
			int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

			int[,] allPixR = new int[width, height];
			int[,] allPixG = new int[width, height];
			int[,] allPixB = new int[width, height];

			int limit = 128 * 128;

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					allPixR[i, j] = b.GetPixel(i, j).R;
					allPixG[i, j] = b.GetPixel(i, j).G;
					allPixB[i, j] = b.GetPixel(i, j).B;
				}
			}

			int new_rx = 0, new_ry = 0;
			int new_gx = 0, new_gy = 0;
			int new_bx = 0, new_by = 0;
			int rc, gc, bc;
			for (int i = 1; i < b.Width - 1; i++)
			{
				for (int j = 1; j < b.Height - 1; j++)
				{

					new_rx = 0;
					new_ry = 0;
					new_gx = 0;
					new_gy = 0;
					new_bx = 0;
					new_by = 0;
					rc = 0;
					gc = 0;
					bc = 0;

					for (int wi = -1; wi < 2; wi++)
					{
						for (int hw = -1; hw < 2; hw++)
						{
							rc = allPixR[i + hw, j + wi];
							new_rx += gx[wi + 1, hw + 1] * rc;
							new_ry += gy[wi + 1, hw + 1] * rc;

							gc = allPixG[i + hw, j + wi];
							new_gx += gx[wi + 1, hw + 1] * gc;
							new_gy += gy[wi + 1, hw + 1] * gc;

							bc = allPixB[i + hw, j + wi];
							new_bx += gx[wi + 1, hw + 1] * bc;
							new_by += gy[wi + 1, hw + 1] * bc;
						}
					}
					if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit || new_bx * new_bx + new_by * new_by > limit)
						bb.SetPixel(i, j, Color.Black);

					//bb.SetPixel (i, j, Color.FromArgb(allPixR[i,j],allPixG[i,j],allPixB[i,j]));
					else
						bb.SetPixel(i, j, Color.Transparent);
				}
			}
			return bb;

		}
	}
}
