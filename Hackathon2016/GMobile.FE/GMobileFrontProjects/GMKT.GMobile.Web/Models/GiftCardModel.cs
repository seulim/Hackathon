using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class GiftCardModel
	{		
	}
	public class TokenGiftCountM
	{
		public string Goodscode { get; set; }
		public string OrderCnts { get; set; }
		public string OrderPrice { get; set; }
		public string GdNm { get; set; }
		public string GdGubun { get; set; }

		public int iOffLineStockCnt { get; set; }
		public GBankGiftOfflineDeliveryYnT GiftOfflineDelivery { get; set; }

		public string UserName { get; set; }
		public string UserHPNum { get; set; }
		public string UserCustNo { get; set; }
	}
	public class TokenGateM
	{
		public string RegistCode { get; set; }		
	}
	public class GiftSendM
	{
		//public string UserCustNo { get; set; }
		public string OptionValue { get; set; }
		public List<GiftCardListT> GiftSend { get; set; }
	}
	public class GiftSendListM
	{
		public string UserCustNo { get; set; }
		public string OptionValue { get; set; }
		public List<GiftCardSendListT> GiftSend { get; set; }
	}
	public class GiftSendDetailM
	{
		public GiftCardDetailListT GiftSend { get; set; }
	}

	public class GiftSendKakaoM
	{
		public GiftCardKakaoDataT GiftKakao { get; set; }
	}
	public class GiftOrderDetail
	{
		public string TOrderValue { get; set; }
		public string TUseDT { get; set; }
		public string ThddHPReserveDT { get; set; }
		public string ThddGDReserveDT { get; set; }
		public string ThddHPRowIndex { get; set; }
		public string ThddGDRowIndex { get; set; }
		public string TOrderType { get; set; }
		public string THPRSVYN { get; set; }
		public string TGDRSVYN { get; set; }
		public string TUserCustNo { get; set; }
		public string TUserName { get; set; }
		public string TUserHPNum { get; set; }
		public string Treturn_url { get; set; }
		public string Torder_type { get; set; }
		public string Tgoodscode { get; set; }
		public string Tgoods_type { get; set; }
		public string Torder_cnts { get; set; }
		public string Torder_price { get; set; }
		public string Torderprice { get; set; }
		public string Tgd_nm { get; set; }
		public string Ttrad_way { get; set; }
		public string Ttrad_way_nm { get; set; }
		public string Tbuyer_mileage { get; set; }
		public string Tbuddy_mileage { get; set; }
		public string Ttax_issue_yn { get; set; }
		public string Tcash_issue_yn { get; set; }
		public string Tgdlc_cd { get; set; }
		public string Toffline_stock_cnt { get; set; }
		public string Tdelivery_fee_yn { get; set; }
		public string Tdelivery_group_no { get; set; }
		public string Tdelivery_group_nm { get; set; }
		public string Tbasis_money { get; set; }
		public string Tdelivery_fee { get; set; }
		public string Tdelivery_fee_condition { get; set; }
		public string Tdelivery_group_type { get; set; }
		public string Twho_fee { get; set; }
		public string TGiftType { get; set; }
		public string TGiftHPList { get; set; }
		public string TGiftToken { get; set; }
		public string Tseller_cust_no { get; set; }
	}
}