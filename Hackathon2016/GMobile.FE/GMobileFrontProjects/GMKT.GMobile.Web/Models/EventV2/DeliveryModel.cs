using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class DeliveryAgreementModel
	{
		public DeliveryAgree Agreement { get; set; }
	}

	public class DeliveryModel : DeliveryAgreementModel
	{
	}

	public class BannerAllM
	{
		public List<DeliveryBannerCategoryT> BannerList { get; set; }
	}
}