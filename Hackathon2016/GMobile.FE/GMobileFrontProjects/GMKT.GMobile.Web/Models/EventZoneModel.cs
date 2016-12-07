using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.Diver;
using GMobile.Data.DisplayDB;
using System.Dynamic;
using GMobile.Data.EventZone;

namespace GMKT.GMobile.Web.Models
{
    public class MyBenefitCouponM
    {
		public List<bool> IsAbleToDownload { get; set; }

		public List<MyBenefitCouponDataM> CouponList { get; set; }
    }

	public class MyBenefitCouponDataM
	{
		public string MainImageUrl { get; set; }
		public string MainImageAlt { get; set; }
		public string MainImageName { get; set; }

		public List<ImageCouponM> CouponList { get; set; }
	}

	public class ImageCouponM
	{
		public string ImageUrl { get; set; }
		public string ImageAlt { get; set; }
		public int Index { get; set; }
	}

	public class MyBenefitVip_CheckEmailM
	{
		public bool IsLogin { get; set; }
		public bool AllowReceiveEmail { get; set; }
	}

	public class MyBenefitM
	{
		public string CRYPT_MD5_FOOTER { get; set; }
		public int CouponCount { get; set; }

		public Newtonsoft.Json.Linq.JArray CouponCollection { get; set; }

		public string EndDate { get; set; }

		public int MileagePoint { get; set; }

		public MyProfileT MyProfile { get; set; }

		public string NextGradeStr { get; set; }

		public string NextMileage { get; set; }

		public int NextPurchase { get; set; }

		public string NowDate { get; set; }

		public int RecentlyCount { get; set; }

		public string StartDate { get; set; }

		public int StickerCount { get; set; }

		public string StickerText { get; set; }

		public string StickerValue { get; set; }

		public string ThisGradeChr { get; set; }

		public string ThisGradeStr { get; set; }

		public string ThisMonth { get; set; }

		public int TotalCount { get; set; }

		public List<ImageCouponM> SVIPCouponList { get; set; }
		public List<ImageCouponM> VIPCouponList { get; set; }
		public List<ImageCouponM> GoldCouponList { get; set; }
		public List<ImageCouponM> SilverCouponList { get; set; }
		public List<ImageCouponM> NewCouponList { get; set; }
	}

	public class MyBenefitVipM
	{
		public string CRYPT_MD5_FOOTER { get; set; }
		public string LastDay { get; set; }

		public MyProfileT MyProfile { get; set; }

		public string NowTime { get; set; }

		public string StartTime { get; set; }

		public string ThisDay { get; set; }

		public DateTime ThisEndTime { get; set; }

		public string ThisMonth { get; set; }

		public DateTime ThisStartTime { get; set; }

		public string TotalCount { get; set; }

		public string TotalPrice { get; set; }

		public Newtonsoft.Json.Linq.JArray BannerCollection { get; set; }
		public Newtonsoft.Json.Linq.JArray GiftCollection { get; set; }
	}
}