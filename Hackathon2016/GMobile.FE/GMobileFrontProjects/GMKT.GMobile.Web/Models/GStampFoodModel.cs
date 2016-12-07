using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web.Models
{
	public class GStampFoodModel
	{
		public string Title { get; set; }

		public string ImageUrl { get; set; }

		public string[] ApplyEidEncryptedString { get; set; }
		public string[] ChangeEidEncryptedString { get; set; }

		public int ApplyCount { get; set; }
		public int ChangeCount { get; set; }

		public int RemainCount { get; set; }
	}
}