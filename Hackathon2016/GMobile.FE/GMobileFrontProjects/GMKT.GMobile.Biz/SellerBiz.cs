using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class SellerBiz : BizBase
	{
		public SellerT GetSeller(string sellerId)
		{
			//string key = typeof(SellerT).Name + sellerId;
			//if (HttpContext.Current.Cache[key] != null)
			//    return (SellerT)HttpContext.Current.Cache[key];

			List<SellerT> sellers = new SellerDac().SelectSeller(sellerId);
			SellerT seller = null;

			if (sellers != null && sellers.Count > 0)
			{
				seller = sellers[0];

				List<SellerT> shopFeedbacks = new SellerDac().SelectFeedbackRate(sellerId);

				seller.Id = sellerId;

				if (shopFeedbacks != null && shopFeedbacks.Count > 0)
				{
					seller.FeedbackRateValue = shopFeedbacks[0].FeedbackRateValue;

					List<SellerT> mileages = new SellerDac().SelectMileageToSave(sellerId);
					if (mileages != null && mileages.Count > 0)
					{
						seller.MileageToSave = mileages[0].MileageToSave;
					}
				}


				//// Cache Insert
				//HttpContext.Current.Cache.Insert(key, seller, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
			}
			return seller;
		}
	}
}