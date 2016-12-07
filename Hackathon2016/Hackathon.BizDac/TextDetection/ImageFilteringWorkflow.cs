using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace Hackathon.BizDac
{
	public class ImageFilteringWorkflow
	{
		public Bitmap DoWorkFlow(string imagePath)
		{
			string savePath = "C:/git_repo/Hackathon2016/Hackathon2016/Hackathon2016/TestImage/result/";
			string middleFix = DateTime.Now.Millisecond.ToString() + "_";

			Bitmap bitmap;
			
			//흑백
			bitmap = new ImageProcessingTest().CopyAsGrayscale(imagePath);
			bitmap.Save(savePath + middleFix + "1.jpg");

			//밝기조절
			bitmap = new ImageProcessingTest().AdjustBrightness(bitmap, 50);
			bitmap.Save(savePath + middleFix + "2_bright.jpg");

			//Median filtering
			bitmap = new ImageProcessingTest().MedianFilter(bitmap, 2);
			bitmap.Save(savePath + middleFix + "2_Median.jpg");

			//모서리효과 (sobel)
			bitmap = new EdgeDetection().DoSobelEdgeDetection(bitmap);
			bitmap.Save(savePath + middleFix + "3_edge.jpg");

			//모서리효과3
			//bitmap = new BooleanEdgeDetection().BooleanEdgeDetectionFilter(bitmap, BooleanEdgeDetection.BooleanFilterType.EdgeDetect);
			//bitmap.Save(savePath + middleFix + "3_edge.jpg");

			//블러
			bitmap = new ImageProcessingTest().GaussianBlurByType(bitmap, ImageProcessingTest.BlurType.Median3x3);
			bitmap.Save(savePath + middleFix + "4_blur.jpg");



			//노이즈 제거
			//bitmap = new ImageProcessingTest().RemoveNoise(bitmap);
			//bitmap.Save(savePath + middleFix + "4_noise.jpg");

			//반전효과
			//bitmap = new ImageProcessingTest().CopyAsNegative(bitmap);
			//bitmap.Save(savePath + middleFix + "3.jpg");


			//모서리효과2 (쓰지말것)
			//bitmap = new ImageProcessingTest().FuzzyEdgeBlurFilter(bitmap);
			//bitmap.Save(savePath + middleFix + "3_edge.jpg");

			return bitmap;

		}
	}
}
