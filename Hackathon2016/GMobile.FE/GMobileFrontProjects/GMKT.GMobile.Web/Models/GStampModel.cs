using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web.Models
{
	public class GStampModel
	{
		public int CurrentTabNo { get; set; }

		public int PossibleGStampCount { get; set; }

		public string MesssageGStampBlacklist { get; set; }

		public List<GStampFoodModel> GStampFoods { get; set; }

		public GStampModel()
		{
			GStampFoods = new List<GStampFoodModel>();

			CurrentTabNo = 0;
		}
	}
}