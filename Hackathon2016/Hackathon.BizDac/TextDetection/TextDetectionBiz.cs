using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using System.Drawing;
using System.IO;

namespace Hackathon.BizDac
{
    public class TextDetectionBiz
    {
        const string usage = @"Usage:TextDetectionSample <path_to_image>";
        /// <summary>
        /// Creates an authorized Cloud Vision client service using Application 
        /// Default Credentials.
        /// </summary>
        /// <returns>an authorized Cloud Vision client.</returns>
        public VisionService CreateAuthorizedClient()
        {
            GoogleCredential credential =
                GoogleCredential.GetApplicationDefaultAsync().Result;
            // Inject the Cloud Vision scopes
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    VisionService.Scope.CloudPlatform
                });
            }
            return new VisionService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                GZipEnabled = false
            });
        }

        /// <summary>
        /// Detect text within an image using the Cloud Vision API.
        /// </summary>
        /// <param name="vision">an authorized Cloud Vision client.</param>
        /// <param name="imagePath">the path where the image is stored.</param>
        /// <returns>a list of text detected by the Vision API for the image.
        /// </returns>
        public IList<AnnotateImageResponse> DetectText(
            VisionService vision, string imagePath)
        {
            Console.WriteLine("Detecting Text...");

			MemoryStream stream = new MemoryStream();
			Bitmap bitmap;
			bitmap = ImageFiltering(imagePath);
			bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

			byte[] imageArray = stream.GetBuffer();
			
			// Convert image to Base64 encoded for JSON ASCII text based request   
			//byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
            string imageContent = Convert.ToBase64String(imageArray);
            // Post text detection request to the Vision API
            var responses = vision.Images.Annotate(
                new BatchAnnotateImagesRequest()
                {
                    Requests = new[] {
                    new AnnotateImageRequest() {
                        Features = new [] { new Feature() { Type =
                          "TEXT_DETECTION"}},
                        Image = new Google.Apis.Vision.v1.Data.Image() { Content = imageContent }
                    }
               }
                }).Execute();
            return responses.Responses;
        }

		private Bitmap ImageFiltering(string imagePath)
		{
			//Convert image to gray scale
			Bitmap bitmap;
			
			bitmap = new ImageProcessingTest().CopyAsGrayscale(imagePath);
			bitmap.Save("C:/Users/pjeon/Documents/hackathon/result/gray.jpg");

			//모서리효과
			bitmap = new ImageProcessingTest().FuzzyEdgeBlurFilter(bitmap);
			bitmap.Save("C:/Users/pjeon/Documents/hackathon/result/edge.jpg");

			//Convert image to Negative scale
			bitmap = new ImageProcessingTest().CopyAsNegative(bitmap);
			bitmap.Save("C:/Users/pjeon/Documents/hackathon/result/negative.jpg");

			
			return bitmap;

		}
    }
}
