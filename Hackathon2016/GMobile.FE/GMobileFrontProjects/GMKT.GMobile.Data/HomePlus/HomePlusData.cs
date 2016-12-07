using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Data.HomePlus
{

	public class HomePlusSpecialShopT
	{
		public List<HomePlusSpecialshopNavi> Navigations { get; set; }
		public List<HomePlusSpecialshopBanner> Banners { get; set; }
		public List<CPPLPSRPItemModel> Items { get; set; }
		public int ItemTotalSize { get; set; }
	}

	public class HomePlusSpecialshopNavi
	{
		public long PageSeq { get; set; }
		public string PageName { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndDate { get; set; }
		public string PcTopHtml { get; set; }
		public string DisplayYn { get; set; }
	}

	public class HomePlusSpecialshopBanner
	{
		public long PageSeq { get; set; }
		public string PageName { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndDate { get; set; }
		public string PcTopHtml { get; set; }
		public string MobileTopImagePath { get; set; }
		public string DisplayYn { get; set; }
		public string NaviYn { get; set; }
	}

	public class HomePlusHomeT
	{
		public List<HomePlusBanner> Banners { get; set; }
		public Dictionary<string, HomePlusSectinoModel> Sections { get; set; }
		public int ItemTotalSize { get; set; }
	}

	public class HomePlusSectinoModel
	{
		public List<CPPLPSRPItemModel> Items { get; set; }
		public string Title { get; set; }
		public string PageSeq { get; set; }
	}

	public class HomePlusBanner
	{
		public string BannerType { get; set; }
		public string BannerName { get; set; }
		public string ImagePath { get; set; }
		public string LinkUrl { get; set; }
		public string ExposeYn { get; set; }
	}

	public class SpecialShopPrimaryAddrT
	{
		public int AddrNo { get; set; }
		public string FriendZipCode { get; set; }
		public string FriendFrontAddress { get; set; }
		public string FriendBackAddress { get; set; }
	}

	public class SpecialShopZipBranchMatchingT
	{
		public string SellCustNo { get; set; }
		public string ZipCd { get; set; }
		public int BranchCd { get; set; }
		public string BranchAddrNm1 { get; set; }
		public string BranchAddrNm2 { get; set; }
		public string BranchAddrNm3 { get; set; }
		public string BranchNm { get; set; }
	}

	public class SpecialShopAllocationTimeT
	{
		public DateTime DeliveryDt { get; set; }
		public int DeliveryOrderBy { get; set; }
		public char HolidayYn { get; set; }
		public DateTime StartDt { get; set; }
		public DateTime EndDt { get; set; }
		public char DelvieryEnableYn { get; set; }
		public int DelvieryEnableCnt { get; set; }
		public DateTime DeliveryAplnEndDt { get; set; }
	}
	public class SpecialShopCategoryT
	{
		public string DisplayAreaTypeValue { get; set; }
		public int Priority { get; set; }
		public string ImageTitle { get; set; }
		public char Type { get; set; }
		public string ShopCategoryCd { get; set; }
		public string PcURL { get; set; }
		public string MobileURL { get; set; }
	}

	public class ItemMinUnitBuyCountT
	{
		public int BuyUnitCnt { get; set; }
		public int MinBuyCnt { get; set; }
        public ItemOrderLimitInfoT ItemOrderLimitInfo { get; set; }
	}

    public class ItemOrderLimitInfoT
    {
        public int OrderLimitCnt { get; set; }
        public int OrderLimitPeriod { get; set; }
        public int OrderPossibleCnt { get; set; }
    }

	public class AddPrimaryAddrWithNewRequestT
	{
		public ShippingAddressRequestT ShippingAddress { get; set; }
		public string SellCustNo { get; set; }
	}

	public class ShippingAddressRequestT
	{
		public string WorkMode { get; set; }
		public int AddrNo { get; set; }
		public int GroupId { get; set; }
		public string CustNo { get; set; }
		public string FriendLoginId { get; set; }
		public string FriendName { get; set; }
		public string FriendTelNo { get; set; }
		public string FriendHandPhoneNo { get; set; }
		public string FriendEmail { get; set; }
		public string FriendAddressAlias { get; set; }
		public string FriendNationISOCode { get; set; }
		public string FriendFrontAddress { get; set; }
		public string FriendBackAddress { get; set; }
		public string FriendZipCode { get; set; }
		public string FriendMemo { get; set; }
		public bool IsFriendDefaultAddress { get; set; }
		public DateTime ChangeDate { get; set; }
		public string ChangeId { get; set; }
		public int AddressNo { get; set; }
		public string IsDeliverySeqNo { get; set; }
		public string IsFaceBook { get; set; }
		public string RoadNameFrontAddress { get; set; }
		public string RoadNameBackAddress { get; set; }
		public string AddressCode { get; set; }
		public string LoginId { get; set; }
		public string globalCityNm { get; set; }
		public string globalSzNm { get; set; }
	}

	public class SpecialShopBranchInfoTimeTable
	{
		public SpecialShopZipBranchMatchingT BranchInfo { get; set; }
		public List<BranchDeliveryTimeTableOneDay> TimeTable { get; set; }
		public List<TimeSlot> TimeSlot { get; set; }
	}

	public class TimeSlot
	{
		public string StrStartHour { get; set; }
		public string StrEndHour { get; set; }
	}

	public class BranchDeliveryTimeTableOneDay
	{
		public DateTime Date { get; set; }
		public string StrDate { get; set; } //ex) 오늘, 5/5
		public string StrDay { get; set; } // 목, 금
		public char HolidayYn { get; set; }
		public Dictionary<string, DeliveryEnableT> DeliveryYnDict { get; set; }
	}

	public class DeliveryEnableT
	{
		public DateTime DeliveryAplnEndDt { get; set; }
		public char DelvieryEnableYn { get; set; }
        public char LongManageYn { get; set; }
        public char LongDeliveryEnableYn { get; set; }
	}

	public class SpecialShopCategoryImageT
	{
		public string PcImagePath { get; set; }
		public string MobileImagePath { get; set; }
	}

	public class SpecialShopCategory
	{
		public List<SpecialShopCategoryT> CategoryList { get; set; }
		public SpecialShopCategoryImageT CategoryImage { get; set; }
	}
}
