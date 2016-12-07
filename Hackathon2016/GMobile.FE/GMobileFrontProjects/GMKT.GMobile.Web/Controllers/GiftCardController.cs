using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using System.Configuration;

using GMKT.Framework;

namespace GMKT.GMobile.Web.Controllers
{
	public class GiftCardController : GMobileControllerBase
	{
		/// <summary>
		/// 선물권 메인페이지
		/// </summary>
		/// <returns></returns>
		public ActionResult GiftCard()
		{
			ViewBag.HeaderTitle = "GIFT CARD";
            LandingBannerModel landingBannermodel = new LandingBannerModel();

            new LandingBannerSetter(Request).Set(landingBannermodel, PageAttr.IsApp);

            return View(landingBannermodel);
		}

        /// <summary>
        /// Landing Banner Inner Class Model
        /// </summary>
        public class LandingBannerModel : ILandingBannerModel
        {
            public ILandingBannerEntityT LandingBanner { get; set; }
            public ICampaign Campaign { get; set; }
        }

		/// <summary>
		/// 선물권 보낸선물 페이지
		/// </summary>
		/// <returns></returns>
		public ActionResult GiftCardListSend(string shMonth = "1")
		{
			ViewBag.HeaderTitle = "GIFT CARD";
			DateTime now = DateTime.Now;
			DateTime startDate;
			if (shMonth == "1")
				startDate = DateTime.Now.AddMonths(-1);
			else if (shMonth == "3")
				startDate = DateTime.Now.AddMonths(-3);
			else if (shMonth == "6")
				startDate = DateTime.Now.AddMonths(-6);
			else if (shMonth == "12")
				startDate = DateTime.Now.AddMonths(-12);
			else if (shMonth == "24")
				startDate = DateTime.Now.AddMonths(-24);
			else
				startDate = DateTime.Now.AddMonths(-1);

			DateTime endDate = DateTime.Now;
			string custNo = gmktUserProfile.CustNo;
			List<GiftCardSendListT> GiftcardList = null;
			GiftSendListM data;

			GiftcardList = new GBankBiz().GetGiftCardMemberSendList(custNo, startDate, endDate);

			data = new GiftSendListM
			{
				UserCustNo = gmktUserProfile.CustNo,
				OptionValue = shMonth,
				GiftSend = GiftcardList
			};

			return View(data);
		}
		#region 선물권 재발송
		/// <summary>
		/// 선물권 재발송
		/// </summary>
		/// <returns></returns>			
		public ActionResult GiftCardDetailViewListResend(string gubunCheck, string tab, int ticketNo, string CustNo, string telNo)
		{
			ReSendAuthCodeM AjaxModel = new ReSendAuthCodeM();

			string callbackUrl = string.Empty;
			int cntThreeTimes = 0;		// 3번 재발송 확인
			string retCode = string.Empty;					// 재발송 결과 코드 

			if (this.Request.Url.Host.Contains("mobiledev") || this.Request.Url.Host.Contains("local-mobile"))
				callbackUrl = "http://mobiledev.gmarket.co.kr/GiftCard/GiftGate";		// test - mms callbackurl 
			else
				callbackUrl = "http://mobile.gmarket.co.kr/GiftCard/GiftGate";			// real - mms callbackurl 

			cntThreeTimes = new GBankBiz().CountMobileRetrans(ticketNo);

			if (cntThreeTimes < 3)
			{
				//선물권 인증확인
				retCode = new GBankBiz().SetGiftTokenSendMobileMessage(ticketNo, callbackUrl);

				AjaxModel = new ReSendAuthCodeM
				{
					RetCode = retCode,
					RetReason = "재발송 처리가 완료되었습니다."
				};
			}
			else
			{
				AjaxModel = new ReSendAuthCodeM
				{
					RetCode = cntThreeTimes.ToString(),
					RetReason = "재발송은 3회까지 가능합니다. 재발송이 3회 이상 초과하였습니다."
				};
			}

			return Json(AjaxModel);
		}

		public ActionResult GiftCardCancelResend(int ticketNo, string telNo)
		{
			ReSendAuthCodeM AjaxModel = new ReSendAuthCodeM();
			string retCode = string.Empty;					// 재발송 결과 코드 

			//선물권 인증확인
			retCode = new GBankBiz().SetGiftTokenCancelSendMobile(ticketNo, telNo);

			AjaxModel = new ReSendAuthCodeM
			{
				RetCode = retCode,
				RetReason = "재발송 처리가 완료되었습니다."
			};

			return Json(AjaxModel);
		}

		#endregion
		#region 선물권 아이템 디테일 가져오기
		public ActionResult GetItemDetail(string TOKENSEQ = "1", string TYPE = "")
		{


			GiftCardDetailListT GiftcardList = null;
			GiftSendDetailM data;

			GiftcardList = new GBankBiz().GetGiftCardItemDetail(TOKENSEQ, TYPE);

			data = new GiftSendDetailM
			{
				GiftSend = GiftcardList
			};

			return Json(data);
		}
		#endregion
		#region 선물권 카카오 발송 데이터 가져오기
		public ActionResult GetKakaoSendData(string TOKENSEQ = "1")
		{
			GiftCardKakaoDataT GiftcardList = null;
			GiftSendKakaoM data;

			GiftcardList = new GBankBiz().GetKakaoSendData(TOKENSEQ);

			string decriptAuth16 = string.Empty;

			decriptAuth16 = GMKT.Framework.Security.GMKTCryptoLibraryOption.DecodeFrom64(GiftcardList.Auth16Code);
			GiftcardList.Auth16Code = decriptAuth16;

			data = new GiftSendKakaoM
			{
				GiftKakao = GiftcardList
			};

			return Json(data);
		}
		#endregion
		/// <summary>
		/// 선물권 받은선물 페이지
		/// </summary>
		/// <returns></returns>
		public ActionResult GiftCardListRsv(string shMonth = "1")
		{
			ViewBag.HeaderTitle = "GIFT CARD";
			DateTime now = DateTime.Now;
			DateTime startDate;
			if (shMonth == "1")
				startDate = DateTime.Now.AddMonths(-1);
			else if (shMonth == "3")
				startDate = DateTime.Now.AddMonths(-3);
			else if (shMonth == "6")
				startDate = DateTime.Now.AddMonths(-6);
			else if (shMonth == "12")
				startDate = DateTime.Now.AddMonths(-12);
			else if (shMonth == "24")
				startDate = DateTime.Now.AddMonths(-24);
			else
				startDate = DateTime.Now.AddMonths(-1);

			DateTime endDate = DateTime.Now;
			string custNo = gmktUserProfile.CustNo;
			List<GiftCardListT> GiftcardList = null;
			GiftSendM data;

			GiftcardList = new GBankBiz().GetGiftCardMemberRsvList(1, 50000, custNo, startDate, endDate);

			data = new GiftSendM
			{
				//UserCustNo = gmktUserProfile.CustNo,
				OptionValue = shMonth,
				GiftSend = GiftcardList
			};

			return View(data);
			//return View();
		}

		/// <summary>
		/// 선물권 구매페이지
		/// </summary>
		/// <returns></returns>
		public ActionResult GiftCardOrder(string orderprice)
        {
            Response.Redirect("http://mobile.gmarket.co.kr/GiftCard/GiftCard");
            Response.End();

			//Response.Cookies.Add(new HttpCookie("pcid", "434343"));
			TokenGiftCountM data;

			int istockcnt = 0;	//오프라인 선물권 구매가능 수량
			string goodscode = "";

			istockcnt = new GBankBiz().GetGBankGiftCount(int.Parse(orderprice));

				//실서버용
				switch (orderprice)
				{
					case "10000":
						goodscode = "522977152";							//선물권 상품코드	
						break;
					case "30000":
						goodscode = "488791597";							//선물권 상품코드							
						break;
					case "50000":
						goodscode = "488791917";							//선물권 상품코드							
						break;
					case "100000":
						goodscode = "488792128";							//선물권 상품코드
						break;
					case "200000":
						goodscode = "488792380";							//선물권 상품코드
						break;
					default:
						goodscode = "522977152";							//선물권 상품코드
						break;
				}

			GBankGiftOfflineDeliveryYnT GiftOfflineDelivery = new GBankBiz().GetGBankOfflineDeliveryYn(goodscode, int.Parse(orderprice), int.Parse("1"), 0);

			switch (orderprice)
			{
				case "10000":
					data = new TokenGiftCountM
					{
						GdNm = "G마켓 Gift Card 10,000원권",	//선물권명
						Goodscode = goodscode,							//선물권 상품코드
						iOffLineStockCnt = istockcnt,					//오프라인 선물권 구매가능 수
						OrderPrice = orderprice,								//선물권 금액
						GiftOfflineDelivery = GiftOfflineDelivery,
						UserName = gmktUserProfile.CustName,
						UserHPNum = gmktUserProfile.UserHPNo,
                        UserCustNo = gmktUserProfile.CustNo
					};
					break;
				case "30000":
					data = new TokenGiftCountM
					{
						GdNm = "G마켓 Gift Card 30,000원권",	//선물권명
						Goodscode = goodscode,							//선물권 상품코드
						iOffLineStockCnt = istockcnt,					//오프라인 선물권 구매가능 수
						OrderPrice = orderprice,								//선물권 금액
						GiftOfflineDelivery = GiftOfflineDelivery,
						UserName = gmktUserProfile.CustName,
						UserHPNum = gmktUserProfile.UserHPNo,
                        UserCustNo = gmktUserProfile.CustNo
					};
					break;
				case "50000":
					data = new TokenGiftCountM
					{
						GdNm = "G마켓 Gift Card 50,000원권",	//선물권명
						Goodscode = goodscode,							//선물권 상품코드
						iOffLineStockCnt = istockcnt,					//오프라인 선물권 구매가능 수
						OrderPrice = orderprice,								//선물권 금액
						GiftOfflineDelivery = GiftOfflineDelivery,
						UserName = gmktUserProfile.CustName,
						UserHPNum = gmktUserProfile.UserHPNo,
                        UserCustNo = gmktUserProfile.CustNo
					};
					break;
				case "100000":
					data = new TokenGiftCountM
					{
						GdNm = "G마켓 Gift Card 100,000원권",	//선물권명
						Goodscode = goodscode,							//선물권 상품코드
						iOffLineStockCnt = istockcnt,					//오프라인 선물권 구매가능 수
						OrderPrice = orderprice,
						UserName = gmktUserProfile.CustName,
						UserHPNum = gmktUserProfile.UserHPNo,
						UserCustNo = gmktUserProfile.CustNo
					};
					break;
				case "200000":
					data = new TokenGiftCountM
					{
						GdNm = "G마켓 Gift Card 200,000원권",	//선물권명
						Goodscode = goodscode,							//선물권 상품코드
						iOffLineStockCnt = istockcnt,					//오프라인 선물권 구매가능 수
						OrderPrice = orderprice,
						UserName = gmktUserProfile.CustName,
						UserHPNum = gmktUserProfile.UserHPNo,
						UserCustNo = gmktUserProfile.CustNo							//선물권 금액
					};
					break;
				default:
					data = new TokenGiftCountM
					{
						GdNm = "G마켓 Gift Card 5,000원권",		//선물권명
						Goodscode = goodscode,							//선물권 상품코드
						iOffLineStockCnt = istockcnt,					//오프라인 선물권 구매가능 수
						OrderPrice = orderprice,
						UserName = gmktUserProfile.CustName,
						UserHPNum = gmktUserProfile.UserHPNo,
						UserCustNo = gmktUserProfile.CustNo							//선물권 금액
					};
					break;
			}

			//data = new TokenGiftCountM
			//{
			//  GdNm = "",	//선물권명
			//  Goodscode = "",							//선물권 상품코드
			//  iOffLineStockCnt = 0,					//오프라인 선물권 구매가능 수
			//  OrderPrice = orderprice,								//선물권 금액
			//  //GiftOfflineDelivery = "",
			//  UserName = gmktUserProfile.CustName,
			//  UserHPNum = gmktUserProfile.UserHPNo,
			//  UserCustNo = gmktUserProfile.CustNo,
			//  UserPCID = gmktUserProfile.PcidJCN
			//};

			return View(data);
		}
		/// <summary>
		/// 선물권 구매 디테일페이지
		/// </summary>
		/// <returns></returns>
		public ActionResult GiftCardOrderDetail(string OrderValue, string UseDT, string hddHPReserveDT, string hddGDReserveDT,
			string hddHPRowIndex, string hddGDRowIndex, string OrderType, string HPRSVYN, string GDRSVYN, string UserCustNo,
			string UserName, string UserHPNum, string return_url, string order_type, string goodscode, string goods_type,
			string order_cnts, string order_price, string orderprice, string gd_nm, string trad_way, string trad_way_nm,
			string buyer_mileage, string buddy_mileage, string tax_issue_yn, string cash_issue_yn, string gdlc_cd,
			string offline_stock_cnt, string delivery_fee_yn, string delivery_group_no, string delivery_group_nm,
			string basis_money, string delivery_fee, string delivery_fee_condition, string delivery_group_type, string who_fee,
			string GiftType, string GiftHPList, string GiftToken, string seller_cust_no)
        {
            Response.Redirect("http://mobile.gmarket.co.kr/GiftCard/GiftCard");
            Response.End();

			GiftOrderDetail data;
			data = new GiftOrderDetail
			{
				TOrderValue = OrderValue,
				TUseDT = UseDT,
				ThddHPReserveDT = hddHPReserveDT,
				ThddGDReserveDT = hddGDReserveDT,
				ThddHPRowIndex = hddHPRowIndex,
				ThddGDRowIndex = hddGDRowIndex,
				TOrderType = OrderType,
				THPRSVYN = HPRSVYN,
				TGDRSVYN = GDRSVYN,
				TUserCustNo = UserCustNo,
				TUserName = UserName,
				TUserHPNum = UserHPNum,
				Treturn_url = return_url,
				Torder_type = order_type,
				Tgoodscode = goodscode,
				Tgoods_type = goods_type,
				Torder_cnts = order_cnts,
				Torder_price = order_price,
				Torderprice = orderprice,
				Tgd_nm = gd_nm,
				Ttrad_way = trad_way,
				Ttrad_way_nm = trad_way_nm,
				Tbuyer_mileage = buyer_mileage,
				Tbuddy_mileage = buddy_mileage,
				Ttax_issue_yn = tax_issue_yn,
				Tcash_issue_yn = cash_issue_yn,
				Tgdlc_cd = gdlc_cd,
				Toffline_stock_cnt = offline_stock_cnt,
				Tdelivery_fee_yn = delivery_fee_yn,
				Tdelivery_group_no = delivery_group_no,
				Tdelivery_group_nm = delivery_group_nm,
				Tbasis_money = basis_money,
				Tdelivery_fee = delivery_fee,
				Tdelivery_fee_condition = delivery_fee_condition,
				Tdelivery_group_type = delivery_group_type,
				Twho_fee = who_fee,
				TGiftType = GiftType,
				TGiftHPList = GiftHPList,
				TGiftToken = GiftToken,
				Tseller_cust_no = seller_cust_no
			};

			return View(data);
		}
		/// <summary>
		/// 선물권 인증페이지
		/// </summary>
		/// <returns></returns>
		public ActionResult GiftCardRegist(string RegCode = "")
		{
			ViewBag.HeaderTitle = "GIFT CARD";
			TokenGateM data;
			data = new TokenGateM
			{
				RegistCode = RegCode
			};
			return View(data);
		}
		/// <summary>
		/// 선물권 게이트페이지
		/// </summary>
		/// <returns></returns>
		public ActionResult GiftGate(string RegCode)
		{
			TokenGateM data;
			data = new TokenGateM
			{
				RegistCode = RegCode
			};
			return View(data);
		}
		#region 선물권 재고 체크 Ajax 연동
		public ActionResult GetGiftTokenOffStockCount(string price = "", string gd_no = "")
		{

			int istockcnt = 0;

			istockcnt = new GBankBiz().GetGBankGiftCount(gd_no, int.Parse(price));

			return Json(istockcnt);
		}
		#endregion
		#region 선물권 마스터 테이블 데이터 & 디테일 테이블 데이터 입력
		public ActionResult SetGiftTokenOrderData(string CUST_NO, string STAT, string TRANSWAY, string DESIGN_TYPE, string SEND_MSG_TYPE, string SEND_MSG, string SENDER_INFO_CP, string SENDER_INFO_NA = "", string RSV_TRANS_YN = "", string RSV_DT = "", string OrderValue = "", string orderPrice = "", string orderCnts = "", string GD_NO = "")
		{
			string cust_no = CUST_NO;
			string stat = STAT;
			string transway = TRANSWAY;
			string designtype = DESIGN_TYPE;
			string sendmsgtype = SEND_MSG_TYPE;
			string sendmsg = SEND_MSG;
			string senderinfocp = SENDER_INFO_CP;
			string senderinfona = SENDER_INFO_NA;
			string rsvtransyn = RSV_TRANS_YN;
			string[] rsvdatetime = RSV_DT.Split('-');
			DateTime rsvdt;
			string bulkyn = "N";
			string ordervalue = OrderValue;
			string ordercnts = orderCnts;
			//string hddpcval = hddPCVAL;
			//string hddgdval = hddGDVAL;
			string gd_no = GD_NO;
			int orderprice = Convert.ToInt32(orderPrice);

			int buyqty = 0;

			if (rsvtransyn == "Y")
			{
				rsvdt = new DateTime(Convert.ToInt32(rsvdatetime[0]), Convert.ToInt32(rsvdatetime[1]), Convert.ToInt32(rsvdatetime[2]), Convert.ToInt32(rsvdatetime[3]), Convert.ToInt32(rsvdatetime[4]), 0);
			}
			else
			{
				rsvdt = new DateTime(1990, 1, 1, 1, 1, 1);
			}
			string[] Orderrowdata = OrderValue.Split('/');
			if (Orderrowdata.Length > 1)
				bulkyn = "Y";
			//string[] gdrowdata = Orderrowdata.Split('/');
			//if (gdrowdata.Length > 1)
			//  bulkyn = "Y";
			//string[] pcrowdatadtl = Orderrowdata.Split(',');

			if (stat == "01")
			{
				transway = "1";
			}
			else if (stat == "02")
			{
				transway = "4";
			}
			else if (stat == "03")
			{
				transway = "2";
			}
			else if (stat == "04")
			{
				transway = "3";
			}

			DateTime now = DateTime.Now;
			// string GD_NO = "")
			int masterseq = 0;
			masterseq = new GBankBiz().SetGiftTokenOrderMst(now, cust_no, transway, Convert.ToInt32(ordercnts), designtype, sendmsgtype, sendmsg, senderinfocp, senderinfona, rsvtransyn, rsvdt, bulkyn);

			if (masterseq > 0)
			{
				if (stat == "01")
				{
					for (int i = 0; i < Orderrowdata.Length; i++)
					{
						string[] hprowdatadtl = Orderrowdata[i].Split(',');

						new GBankBiz().SetGiftTokenOrderDtl(masterseq, "", hprowdatadtl[0], "", gd_no, orderprice, Convert.ToInt32(hprowdatadtl[1]), 0, "Y");
					}
				}
				else if (stat == "04")
				{
					for (int i = 0; i < Orderrowdata.Length; i++)
					{
						string[] gdrowdatadtl = Orderrowdata[i].Split(',');

						new GBankBiz().SetGiftTokenOrderDtl(masterseq, gdrowdatadtl[0], "", "", gd_no, orderprice, Convert.ToInt32(gdrowdatadtl[1]), 0, "Y");
					}
				}
				else if (stat == "02")
				{
					string[] kakaodata = OrderValue.Split(',');
					new GBankBiz().SetGiftTokenOrderDtl(masterseq, kakaodata[0], "", "", gd_no, orderprice, Convert.ToInt32(kakaodata[1]), 0, "Y");
				}
				else if (stat == "03")
				{
					string[] pcarddata = OrderValue.Split(',');
					new GBankBiz().SetGiftTokenOrderDtl(masterseq, pcarddata[0], "", "", gd_no, orderprice, Convert.ToInt32(pcarddata[1]), 0, "Y");
				}
				//new GBankBiz().SetGiftTokenOrderDtl(masterseq,
			}

			int data = masterseq;

			return Json(data);
		}
		#endregion
		#region 선물권 ID 찾기
		public ActionResult GetSearchID(string ID, string NAME)
		{
			DateTime now = DateTime.Now;
			DateTime startDate = new DateTime(now.Year, now.Month, 1);
			GiftSearchIDT data = new GBankBiz().GetSearchID(gmktUserProfile.CustNo, ID, NAME, now, startDate);


			return Json(data);
		}
		#endregion
	}
}
