using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.BizDac
{
	public class ImageProcessingTest2
	{
		/// <summary>
		/// 이거안됨
		/// </summary>
		/// <param name="filepath"></param>
		/// <returns></returns>
		public Bitmap ConvertToGrayScale(string filepath)
		{
			Bitmap c = new Bitmap(filepath);
			Bitmap d;
			int x, y;

			// Loop through the images pixels to reset color.
			for (x = 0; x < c.Width; x++)
			{
				for (y = 0; y < c.Height; y++)
				{
					Color pixelColor = c.GetPixel(x, y);
					Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
					c.SetPixel(x, y, newColor); // Now greyscale
				}
			}
			d = c;   // d is grayscale version of c  
			d.Save("C:/Users/pjeon/Documents/hackathon/test2.png");
			return d;
		}
	}
}