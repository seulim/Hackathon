using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Web.Models;
using GMobile.Data.EventZone;

namespace GMKT.GMobile.Web.Util
{
	public static class EntityToModelConverter
	{
		public static List<ImageCouponM> ToImageCouponMList(this List<MyBenefitBannerT> couponT, string bannerSubType)
		{
			List<ImageCouponM> result = new List<ImageCouponM>();

			if (couponT != null && couponT.Count > 0)
			{
				var couponList = couponT.Where(o => o.banner_sub_type == bannerSubType);
				if (couponList != null && couponList.Count() > 0)
				{
					couponList = couponList.OrderBy(o => o.priority);

					foreach (var eachCoupon in couponList)
					{
						if (eachCoupon != null)
						{
							result.Add(new ImageCouponM()
							{
								ImageUrl = eachCoupon.banner_url2,
								ImageAlt = eachCoupon.banner_text,
								Index = eachCoupon.priority
							});
						}	
					}
				}
			}

			return result;
		}
	}
}