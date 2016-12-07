using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMobile.Service.Search;
using GMobile.Service.Vertical;
using GMobile.Data.Diver;
using GMobile.Data.Voyager;
using GMobile.Data.DisplayDB;
using Arche.Data.Voyager;
using GMKT.GMobile.Web.Models;
using GMobile.Service.Home;
using GMobile.Data.EventZone;
using GMobile.Service.EventZone;
using ArcheFx.EnterpriseServices;
using GMKT.Web.Context;
using System.Data;
using Newtonsoft.Json.Linq;
using GMKT.Framework.Security;
using GMKT.Web.Context;
using GMKT.Framework.Constant;
using GMKT.GMobile.Util;
using GMKT.GMobile.Web.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public partial class EventZoneController : EventControllerBase
	{
		#region [ Constants ]
		public static readonly int LENGTH_OF_COUPONS_ON_MYBENEFITCOUPON = 14;

		public static readonly string SVIP_BANNER_SUB_TYPE = "SV";
		public static readonly string VIP_BANNER_SUB_TYPE = "VI";
		public static readonly string GOLD_BANNER_SUB_TYPE = "GO";
		public static readonly string SILVER_BANNER_SUB_TYPE = "SI";
		public static readonly string NEW_BANNER_SUB_TYPE = "NE";
		#endregion

		#region [ Protected Functions ]
		protected DateTime GetLastDayOfMonth(DateTime dtDate)
		{
			DateTime dtTo = dtDate;
			dtTo = dtTo.AddMonths(1);
			dtTo = dtTo.AddDays(-(dtTo.Day));
			return dtTo;
		}

		protected string EidNo(string codeNo)
		{
			return this.EidNo(Convert.ToInt32(codeNo));
		}

		protected string EidNo(int codeNo)
		{
			string code = string.Empty;
			string strNowCode = string.Empty;
			string strKey = string.Empty;
			string[] result;

			Dictionary<String, String> dicCode = new Dictionary<String, String>();
			dicCode.Add("201304", "|120140|120141|120143|120144|120145|120146|120147|120148|120149|120150|120151|120152");
			dicCode.Add("201305", "|120814|120815|120816|120817|120818|120819|120820|120821|120822|120823|120824|120825");
			dicCode.Add("201306", "|121400|121440|121441|121442|121443|121444|121445|121446|121447|121448|121449|121450");
			dicCode.Add("201307", "|122196|122197|122198|122199|122200|122201|122202|122203|122204|122205|122206|122207");
			dicCode.Add("201308", "|122882|122883|122884|122885|122886|122887|122888|122889|122890|122891|122892|122893");
			dicCode.Add("201309", "|123514|123515|123516|123517|123518|123519|123520|123521|123522|123523|123524|123525");
			dicCode.Add("201310", "|124108|124109|124110|124111|124112|124113|124114|124115|124116|124117|124118|124119|124125");
			dicCode.Add("201311", "|124752|124755|124756|124757|124758|124759|124760|124761|124762|124763|124764|124765");
			dicCode.Add("201312", "|125256|125257|125258|125259|125260|125261|125262|125263|125264|125265|125266|125267");
			dicCode.Add("201401", "|126212|126213|126214|126215|126216|126217|126218|126219|126220|126221|126222|126223");
			dicCode.Add("201402", "|126747|126748|126749|126750|126751|126752|126753|126754|126755|126756|126757|126758|126759");
			dicCode.Add("201403", "|127108|127109|127110|127111|127112|127113|127114|127115|127116|127117|127118|127119");
			dicCode.Add("201404", "|127546|127547|127548|127549|127550|127551|127552|127553|127554|127555|127556|127557");
			dicCode.Add("201405", "|128097|128098|128099|128100|128101|128102|128103|128104|128105|128106|128107|128108|128111|127570");
			dicCode.Add("201406", "|128527|128528|128529|128530|128531|128532|128533|128534|128535|128536|128537|128538|128541|127570");
			dicCode.Add("201407", "|128979|128980|128981|128982|128983|128984|128985|128986|128987|128988|128989|128990|128993|127570");
			dicCode.Add("201408", "|129383|129384|129385|129386|129387|129388|129389|129390|129391|129392|129393|129394|129397|127570");
			dicCode.Add("201409", "|129898|129899|129900|129901|129902|129903|129904|129905|129906|129907|129908|129909|129912|127570");

			string strMonth = DateTime.Now.Month.ToString();
			if (strMonth.Length == 1)
				strMonth = "0" + strMonth;

			strKey = DateTime.Now.Year.ToString() + strMonth;

			if (dicCode.ContainsKey(strKey) == false) return "";

			strNowCode = dicCode[strKey];

			result = strNowCode.Split(new char[] { '|' });

			code = result[codeNo];

			return code;
		}

		protected string GetFirstBuyingCouponEID()
		{
			string FirstCouponEID = string.Empty;

			DateTime dt = new DateTime(2014, 02, 01, 00, 00, 00);

			if (DateTime.Now < dt)
			{
				FirstCouponEID = "126224";
			}
			else
			{
				FirstCouponEID = "126759";
			}
			return FirstCouponEID;
		}
		#endregion

		public ActionResult MyBenefit_CouponIssuedCheck(string eid, string cust_no)
		{
			string g_custNo = base.gmktUserProfile.CustNo; //"114653826"; //
			if (string.IsNullOrEmpty(g_custNo))
			{
				g_custNo = cust_no;
			}
			eid = GMKTCryptoLibraryOption.DecodeFrom64(eid);
			eid = GMKTCryptoLibraryOption.DesDecrypt(eid, CRYPT_MD5_FOOTER);

			int ret = 0;                        
			string start_date = DateTime.Now.ToString("yyyy-MM-01 00:00:00");
			string end_date = this.GetLastDayOfMonth(DateTime.Now).ToString("yyyy-MM-dd 23:59:59");
			bool null_flag = false;

			MyBenefitT t = new MyBenefitBiz().SelectCheckEventWinner(eid, g_custNo, start_date, end_date);
			if (t != null)
			{
				ret = t.cnt;
			}
			else
			{
				null_flag = true;
			}

			return Json(new { ret_value = ret, start_date = start_date, end_date = end_date, cust_no = cust_no, eid = eid, null_flag = null_flag, message="success" }, "application/json", JsonRequestBehavior.AllowGet);
		}

		public ActionResult MyBenefit()
		{
			ViewBag.Title = "등급별 추가혜택 - G마켓 모바일";

			string g_custNo = base.gmktUserProfile.CustNo;// "114653826";
			int g_buyerGrade = base.gmktUserProfile.BuyerGrade.xToInt32(); // "30"; 
			string g_custNm = base.gmktUserProfile.CustName; //"홍길동";

			MyBenefitM model = new MyBenefitM();
			model.CRYPT_MD5_FOOTER = CRYPT_MD5_FOOTER;
			
			model.MyProfile = new MyProfileT { CustNo = g_custNo, BuyerGradeNum = g_buyerGrade, CustNm = g_custNm };
			model.ThisMonth = DateTime.Now.Month.ToString();
			model.StartDate = DateTime.Now.AddMonths(-2).ToString("yyyy-MM-01");
			model.EndDate = DateTime.Now.AddDays(1).ToShortDateString();
			model.NowDate = DateTime.Now.ToString("yyyy-MM-dd");

			MyBenefitT t1 = new MyBenefitBiz().SelectGetBuyerTotalGradePoint(g_custNo);
			MyBenefitT t2 = new MyBenefitBiz().SelectGetBuyerPeriodGradePoint(g_custNo, model.StartDate, model.EndDate);
			List<MyBenefitT> t3 = new MyBenefitBiz().SelectGetLargeIssueList(g_custNo, model.NowDate);
			MyBenefitT t4 = new MyBenefitBiz().SelectMyBenefitSummary(g_custNo, model.NowDate);
			MyBenefitT t5 = new MyBenefitBiz().SelectCheckLimitByUpgrade(g_custNo, g_buyerGrade);

			model.TotalCount = t1.total_count;
			model.RecentlyCount = t2.this_month_count;      
			string lis_no = string.Empty;

			foreach (MyBenefitT t in t3) lis_no += t.lis_no + ",";
			
			if (t4 != null)
			{
				model.MileagePoint = t4.MileageSum;
				model.StickerCount = t4.StickerSum;
				model.CouponCount = t4.coupon_cnt;
				model.StickerValue = t4.check_yn;

				if (model.StickerValue.Equals("Y"))
				{
					model.StickerText = "휴면";
				}
				else
				{
					model.StickerText = "비휴면";
				}

				if (lis_no.Trim().Equals(string.Empty))
				{
					MyBenefitT t = new MyBenefitBiz().SelectGetLargeIssueCouponCount(g_custNo, DateTime.Now.ToString("yyyy-MM-dd"), lis_no);
					if (t != null) model.CouponCount += t.coupon_cnt;
				}
			}
			else
			{
				model.MileagePoint = 0;
				model.StickerCount = 0;
				model.CouponCount = 0;
				model.StickerValue = "N";
				model.StickerText = "비휴면";
			}

			if (model.StickerCount <= 0 && model.StickerValue.Equals("Y"))
			{
				model.StickerCount = 0;
			}

			List<MyBenefitBannerT> couponT = new MyBenefitBiz().SelectGetBannerSubList(
				DateTime.Now.ToString("yyyy-MM-01 00:00:00"), 
				GetLastDayOfMonth(DateTime.Now).ToString("yyyy-MM-dd 23:59:00"), 
				"GC", 
				string.Empty, 
				"Y"
			);

			if (couponT != null)
			{
				model.SVIPCouponList = couponT.ToImageCouponMList(SVIP_BANNER_SUB_TYPE);
				model.VIPCouponList = couponT.ToImageCouponMList(VIP_BANNER_SUB_TYPE);
				model.GoldCouponList = couponT.ToImageCouponMList(GOLD_BANNER_SUB_TYPE);
				model.SilverCouponList = couponT.ToImageCouponMList(SILVER_BANNER_SUB_TYPE);
				model.NewCouponList = couponT.ToImageCouponMList(NEW_BANNER_SUB_TYPE);
			}

			model.CouponCollection = JArray.FromObject(couponT);

			#region 구매등급 결정
		
			string grade_chr = "L";
			string grade_str = string.Empty;            
			string upgrade = "N";
			
			string next_grade = string.Empty;
			string next_mileage = string.Empty;
			int next_purchase = 0;

			// 지난달 등급과 현재 등급을 비교하여 승급하였는지 여부
			if (t5 != null) upgrade = t5.ret_code;

			switch (g_buyerGrade)
			{
				case 50:
					grade_chr = "N";
					grade_str = "new";

					next_grade = "silver";
					next_mileage = "100";

					if (model.RecentlyCount >= 1)                    
						next_purchase = 0;                    
					else                    
						next_purchase = 1 - model.RecentlyCount;                            
					break;

				case 40:
					grade_chr = "S";
					grade_str = "silver";
					next_grade = "gold";
					next_mileage = "100";

					if (model.RecentlyCount >= 2)
						next_purchase = 0;
					else
						next_purchase = 2 - model.RecentlyCount;
					break;

				case 30:
					grade_chr = "G";
					grade_str = "gold";
					next_grade = "vip";
					next_mileage = "300";

					if (model.TotalCount >= 50)
					{
						if (model.RecentlyCount >= 5)
							next_purchase = 0;
						else
							next_purchase = 5 - model.RecentlyCount;
					}
					else
					{
						next_purchase = 50 - model.RecentlyCount;
					}
					break;

				case 20:
					grade_chr = "V";
					grade_str = "vip";
					next_grade = "svip";
					next_mileage = "500";

					if (model.TotalCount >= 50)
					{
						if (model.RecentlyCount >= 30)
							next_purchase = 0;
						else
							next_purchase = 30 - model.RecentlyCount;
					}
					else
					{
						next_purchase = 50 - model.RecentlyCount;
					}
					break;

				case 10:
					grade_chr = "SV";
					grade_str = "svip";
					next_grade = "svip";
					next_mileage = "1000";

					if (model.TotalCount >= 50)
					{
						if (model.RecentlyCount >= 30)
							next_purchase = 0;
						else
							next_purchase = 30 - model.RecentlyCount;
					}
					else
					{
						next_purchase = 50 - model.RecentlyCount;
					}
					break;
			}
				   
			#endregion
			
			model.ThisGradeChr = grade_chr;
			model.ThisGradeStr = grade_str.ToUpper();
			
			model.NextGradeStr = next_grade;

			model.NextMileage = next_mileage;
			model.NextPurchase = next_purchase;

			// 4월 1일 페이지 변경 처리
			if (DateTime.Now < new DateTime(2014, 4, 1, 0, 0, 0))
			{
				return View(model);
			}
			else if (DateTime.Now < new DateTime(2014, 7, 1, 0, 0, 0))
			{
				return View("MyBenefitApril", model);
			}
			else // 7월 1일 페이지 변경 처리
			{
				return View("MyBenefitJuly", model);
			}
		}

		public ActionResult MyBenefitVip_CheckEventApplyCount(string cust_no, string timestamp_dt, string eid1, string eid2, string eid3, string eid4)
		{
			eid1 = GMKTCryptoLibraryOption.DecodeFrom64(eid1);
			eid2 = GMKTCryptoLibraryOption.DecodeFrom64(eid2);
			eid3 = GMKTCryptoLibraryOption.DecodeFrom64(eid3);
			eid4 = GMKTCryptoLibraryOption.DecodeFrom64(eid4);

			eid1 = GMKTCryptoLibraryOption.DesDecrypt(eid1, CRYPT_MD5_FOOTER);
			eid2 = GMKTCryptoLibraryOption.DesDecrypt(eid2, CRYPT_MD5_FOOTER);
			eid3 = GMKTCryptoLibraryOption.DesDecrypt(eid3, CRYPT_MD5_FOOTER);
			eid4 = GMKTCryptoLibraryOption.DesDecrypt(eid4, CRYPT_MD5_FOOTER);

			string g_custNo = base.gmktUserProfile.CustNo;
			if (string.IsNullOrEmpty(g_custNo))
			{
				g_custNo = cust_no;
			}

			int apply_cnt = 0;

			MyBenefitVipT t = new MyBenefitVipBiz().SelectGetEventApplyCount(cust_no, timestamp_dt, eid1, eid2, eid3, eid4);
			if (t != null)
			{
				apply_cnt = t.apply_cnt.xToInt32();
			}

			return Json(new { ret_value = apply_cnt }, "application/json", JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult MyBenefitVip_CheckEmail()
		{
			MyBenefitVip_CheckEmailM model = new MyBenefitVip_CheckEmailM();

			if (PageAttr.IsLogin)
			{
				MyProfileT myProfile = new MyBenefitVipBiz().SelectMyProfile(gmktUserProfile.CustNo);

				if (myProfile != null && false == string.IsNullOrEmpty(myProfile.e_rcv_yn))
				{
					model.IsLogin = true;
					model.AllowReceiveEmail = (myProfile.e_rcv_yn.ToUpper() == "Y");
				}
				else
				{
					model.IsLogin = false;
				}
			}
			else
			{
				model.IsLogin = false;
			}

			return Json(model, JsonRequestBehavior.DenyGet);
		}

		public ActionResult MyBenefitVip()
		{
			if (DateTime.Now < new DateTime(2014, 4, 1, 0, 0, 0))
			{
				ViewBag.Title = "SVIP/VIP 사은품 - G마켓 모바일";

				string g_custNo = base.gmktUserProfile.CustNo;
				int g_buyerGrade = base.gmktUserProfile.BuyerGrade.xToInt32();
				string g_custNm = base.gmktUserProfile.CustName;

				MyBenefitVipM model = new MyBenefitVipM();
				model.CRYPT_MD5_FOOTER = CRYPT_MD5_FOOTER;
				model.MyProfile = new MyProfileT() { CustNo = g_custNo, BuyerGradeNum = g_buyerGrade, CustNm = g_custNm };

				model.ThisMonth = DateTime.Now.Month.ToString();
				model.ThisDay = DateTime.Now.Day.ToString();

				model.ThisStartTime = DateTime.Now.ToString("yyyy-MM-01 00:00:00").xToDateTime();
				model.ThisEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00").xToDateTime();

				model.StartTime = DateTime.Now.ToString("yyyy-MM-20 10:00:00");
				model.NowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

				model.LastDay = this.GetLastDayOfMonth(DateTime.Now).Day.ToString();

				MyBenefitVipT t = new MyBenefitVipBiz().SelectMyBenefitVip(
					g_custNo,
					model.ThisStartTime.ToString("yyyy-MM-dd HH:mm:ss"),
					model.ThisEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
					"N"
				);

				if (t != null)
				{
					model.TotalPrice = t.acnt_money.xToDecimal().ToString("#,###");
					model.TotalCount = t.total_contr_cnt.xToDecimal().ToString("#,###");

					if (model.TotalPrice.Equals(string.Empty)) model.TotalPrice = "0";
					if (model.TotalCount.Equals(string.Empty)) model.TotalCount = "0";
				}
				else
				{
					model.TotalPrice = "0";
					model.TotalCount = "0";
				}

				List<MyBenefitGiftT> giftT = new MyBenefitVipBiz().SelectGetGiftList(
					"GA", model.NowTime.Substring(0, 10), 9
				);
				model.GiftCollection = JArray.FromObject(giftT);

				List<MyBenefitBannerT> bannerT = new MyBenefitVipBiz().SelectGetBannerList(
					"VG", model.NowTime.Replace("-", string.Empty).Substring(0, 6), model.NowTime, 3
				);
				model.BannerCollection = JArray.FromObject(bannerT);

				return View(model);
			}
			else if (DateTime.Now < new DateTime(2014, 8, 1, 0, 0, 0))
			{
				return new RedirectResult(Urls.MWebUrl + "/Event/2014/07/0701_MybenefitVip/m_index.asp");
			}
			else if (DateTime.Now < new DateTime(2014, 9, 1, 0, 0, 0))
			{
				return new RedirectResult(Urls.MWebUrl + "/Event/2014/08/0801_MybenefitVip/m_index.asp");
			}
			else
			{
				return new RedirectResult(Urls.MWebUrl + "/Event/2014/09/0901_MybenefitVip/m_index.asp");
			}
		}

		public ActionResult MyBenefitHome()
		{
			return new RedirectResult("/Pluszone/");

			//EventZoneModel model = new EventZoneModel();

			//string custNo;
			//string dateTime;
			//custNo = GMobileWebContext.Current.UserProfile.CustNo.ToString();
			//dateTime = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

			//MyBenefitT MyBenefitList = new MyBenefitBiz().SelectMyBenefitSummary(custNo, dateTime);
			//List<MyBenefitT> CouponList = new MyBenefitBiz().SelectGetLargeIssueList(custNo, dateTime);

			//for (int i = 0; i < CouponList.Count; i++)
			//{
			//    MyBenefitList.lis_no += CouponList[i].lis_no + ",";
			//}

			//if (!string.IsNullOrEmpty(MyBenefitList.lis_no))
			//{
			//    MyBenefitT CouponCnt = new MyBenefitBiz().SelectGetLargeIssueCouponCount(custNo, dateTime, MyBenefitList.lis_no);

			//    if (CouponCnt.coupon_cnt > 0)
			//    {
			//        MyBenefitList.coupon_cnt += CouponCnt.coupon_cnt;
			//    }
			//}

			//model.MybenefitModel = MyBenefitList;


			//#region BC - 6070 - 이벤트/쿠폰/혜택 홈 내 신규 띠배너 영역 반영 요청
			//// 프로시저에서 시간을 체크하지 않음, 날자형식으로 10자리
			//// exec dbo.up_gmkt_admin_get_eventzone_banner_sub_list '2013-06-19', '2013-06-19', 'PB', 'ME', 'Y'
			//List<MyBenefitBannerT> bandT = new MyBenefitBiz().SelectGetBannerSubList(
			//    DateTime.Now.ToString("yyyy-MM-dd"),
			//    DateTime.Now.ToString("yyyy-MM-dd"), // GetLastDayOfMonth(DateTime.Now).ToString("yyyy-MM-dd"),                 
			//    "PB", 
			//    "ME", 
			//    "Y"
			//);

			//model.BandBannerDisplay = false;
			//model.BandBannerUrl = string.Empty;
			//model.BandBannerTitle = string.Empty;
			//model.BandBannerLink = string.Empty;

			//if (bandT != null && bandT.Count > 0)
			//{
			//    var first = bandT.Min(x => x.priority);
			//    var where = bandT.Where(x => x.use_yn.Equals("Y") && x.priority == first);
			//    if (where != null && where.Count() > 0)
			//    {
			//        MyBenefitBannerT bannerT = where.First();
			//        model.BandBannerDisplay = true;
			//        model.BandBannerUrl = bannerT.banner_url;
			//        model.BandBannerTitle = bannerT.banner_title;
			//        model.BandBannerLink = bannerT.lnk_url;
			//    }
			//}
			//#endregion

			//return View(model);
		}

		public ActionResult MyBenefitCoupon()
		{
			return Redirect( Urls.MobileWebUrl + "/CouponZone" );
		}

		[HttpPost]
		public ActionResult Encrypt(string Num)
		{
			string tempEncrypt = string.Empty;
			string eid = string.Empty;

			string sDate;
			string year = DateTime.Now.Year.ToString();
			string month = DateTime.Now.Month.ToString();
			string day = DateTime.Now.Day.ToString();
			string hour = DateTime.Now.Hour.ToString();
			string min = DateTime.Now.Minute.ToString();
			string sec = DateTime.Now.Second.ToString();
			if (month.Length == 1)
				month = "0" + month;
			if (day.Length == 1)
				day = "0" + day;
			if (hour.Length == 1)
				hour = "0" + hour;
			if (min.Length == 1)
				min = "0" + min;
			if (sec.Length == 1)
				sec = "0" + sec;
			sDate = year + "-" + month + "-" + day + " " + hour + ":" + month + ":" + sec;

			switch (Num)
			{
				case "1":
					eid = EidNo("1");
					break;
				case "2":
					eid = EidNo("2");
					break;
				case "3":
					eid = EidNo("3");
					break;
				case "4":
					eid = EidNo("4");
					break;
				case "5":
					eid = EidNo("5");
					break;
				case "6":
					eid = EidNo("6");
					break;
				case "7":
					eid = EidNo("7");
					break;
				case "8":
					eid = EidNo("8");
					break;
				case "9":
					eid = EidNo("9");
					break;
				case "10":
					eid = EidNo("10");
					break;
				case "11":
					eid = EidNo("11");
					break;
				case "12":
					eid = EidNo("12");
					break;
				case "13":
					eid = EidNo("13");
					break;
				case "14":
					eid = EidNo("14");
					break;
			}

			tempEncrypt = sDate + GMKTConstants.Hex7F.ToString() + eid + GMKTConstants.Hex7F.ToString() + gmktWebContext.UserProfile.CustNo.ToString() + GMKTConstants.Hex7F.ToString() + sDate + GMKTConstants.Hex7F.ToString();

			string[] strData = new string[2];

			strData[0] = GMKTCryptoLibrary.AesGCryptoEncrypt(tempEncrypt);
			strData[1] = GMKTCryptoLibraryOption.MD5(strData[0] + "id6znjen28n5119");

			return Json(strData);
		}

		[HttpPost]
		public ActionResult Coupon(string Num)
		{
			string custNo;
			custNo = gmktWebContext.UserProfile.CustNo.ToString();
			string eid = string.Empty;
			string start_date = string.Empty;
			string end_date = string.Empty;

			switch (Num)
			{
				case "1":
					eid = EidNo("1");
					break;
				case "2":
					eid = EidNo("2");
					break;
				case "3":
					eid = EidNo("3");
					break;
				case "4":
					eid = EidNo("4");
					break;
				case "5":
					eid = EidNo("5");
					break;
				case "6":
					eid = EidNo("6");
					break;
				case "7":
					eid = EidNo("7");
					break;
				case "8":
					eid = EidNo("8");
					break;
				case "9":
					eid = EidNo("9");
					break;
				case "10":
					eid = EidNo("10");
					break;
				case "11":
					eid = EidNo("11");
					break;
				case "12":
					eid = EidNo("12");
					break;
				case "13":
					eid = EidNo("13");
					break;
				case "14":
					eid = EidNo("14");
					break;
			}

			string year = DateTime.Now.Year.ToString();
			string month = DateTime.Now.Month.ToString();
			string day = DateTime.Now.Day.ToString();
			if (month.Length == 1)
				month = "0" + month;

			DateTime First = DateTime.Parse(year + "-" + month + "- 01");
			string Lday = First.AddMonths(1).AddDays(-1).Day.ToString();
			if (Lday.Length == 1)
				Lday = "0" + Lday;

			start_date = year + "-" + month + "-01 00:00:00";
			end_date = year + "-" + month + "-" + Lday + " 23:59:59";

			MyBenefitCouponT model = new MyBenefitCouponT();

			model = new MyBenefitBiz().SelectCoupon2(eid, custNo, start_date, end_date);

			return Json(model.CouponCnt);
		}

		[HttpPost]
		public ActionResult BuyCount()
		{
			string custNo;
			custNo = gmktWebContext.UserProfile.CustNo.ToString();

			MyMobileBuyingT model = new MyMobileBuyingT();

			model = new MyBenefitBiz().SelectMobileBuyingCount(custNo,
				DateTime.Now.AddYears(-1).ToString("yyyyMM"),
				DateTime.Now.AddMonths(-1).ToString("yyyyMM"));

			return Json(model.BuyingCount);
		}

		[HttpPost]
		public ActionResult FirstBuyCoupon()
		{
			string custNo;
			custNo = gmktWebContext.UserProfile.CustNo.ToString();			
			string start_date = string.Empty;
			string end_date = string.Empty;
			
			string year = DateTime.Now.Year.ToString();
			string month = DateTime.Now.Month.ToString();
			string day = DateTime.Now.Day.ToString();
			if (month.Length == 1)
				month = "0" + month;

			DateTime First = DateTime.Parse(year + "-" + month + "- 01");
			string Lday = First.AddMonths(1).AddDays(-1).Day.ToString();
			if (Lday.Length == 1)
				Lday = "0" + Lday;

			start_date = year + "-" + month + "-01 00:00:00";
			end_date = year + "-" + month + "-" + Lday + " 23:59:59";

			MyBenefitCouponT model = new MyBenefitCouponT();

			string FirstCouponEID = GetFirstBuyingCouponEID();
			model = new MyBenefitBiz().SelectCoupon2(FirstCouponEID, custNo, start_date, end_date);

			return Json(model.CouponCnt);
		}

		[HttpPost]
		public ActionResult FirstBuyEncrypt()
		{
			string tempEncrypt = string.Empty;
			
			string sDate;
			string year = DateTime.Now.Year.ToString();
			string month = DateTime.Now.Month.ToString();
			string day = DateTime.Now.Day.ToString();
			string hour = DateTime.Now.Hour.ToString();
			string min = DateTime.Now.Minute.ToString();
			string sec = DateTime.Now.Second.ToString();
			if (month.Length == 1)
				month = "0" + month;
			if (day.Length == 1)
				day = "0" + day;
			if (hour.Length == 1)
				hour = "0" + hour;
			if (min.Length == 1)
				min = "0" + min;
			if (sec.Length == 1)
				sec = "0" + sec;
			sDate = year + "-" + month + "-" + day + " " + hour + ":" + month + ":" + sec;

			string FirstCouponEID = GetFirstBuyingCouponEID();
			tempEncrypt = sDate + GMKTConstants.Hex7F.ToString() + FirstCouponEID + GMKTConstants.Hex7F.ToString() + gmktWebContext.UserProfile.CustNo.ToString() + GMKTConstants.Hex7F.ToString() + sDate + GMKTConstants.Hex7F.ToString();

			string[] strData = new string[2];

			strData[0] = GMKT.Framework.Security.GMKTCryptoLibrary.AesGCryptoEncrypt(tempEncrypt);
			strData[1] = GMKT.Framework.Security.GMKTCryptoLibraryOption.MD5(strData[0] + "id6znjen28n5119");

			return Json(strData);
		}

		public ActionResult CouponCategory()
		{
			return View();
		}

		public ActionResult MobileFirstBuy()
		{
			return View();
		}
	}







	public static class ConvertExtension
	{
		public static int xToInt32(this object obj)
		{
			if (obj == null || obj == DBNull.Value || obj.ToString().Trim().Length == 0)
			{
				return 0;
			}

			return Convert.ToInt32(obj);
		}

		public static decimal xToDecimal(this object obj)
		{
			if (obj == null || obj == DBNull.Value || obj.ToString().Trim().Length == 0)
			{
				return 0;
			}

			return Convert.ToDecimal(obj);
		}

		public static DateTime xToDateTime(this object obj, string format = "yyyy-MM-dd HH:mm:ss")
		{

			if (obj == null || obj == DBNull.Value || obj.ToString().Trim().Length == 0)
			{
				return DateTime.Now;
			}

			return DateTime.ParseExact(obj.ToString(), format, System.Globalization.CultureInfo.InvariantCulture);
			
		}

		public static string xToString(this object obj)
		{
			if (obj == null || obj == DBNull.Value)
			{
				return string.Empty;
			}

			return obj.ToString();
		}
	}
}
