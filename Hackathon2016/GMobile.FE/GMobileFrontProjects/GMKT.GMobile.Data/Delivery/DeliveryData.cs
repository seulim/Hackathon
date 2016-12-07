using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	public class DaumCoordToAddress
	{
		public string name { get; set; }
		public string name1 { get; set; }
		public string name2 { get; set; }
		public string name3 { get; set; }
		public string fullName { get; set; }
		public string code { get; set; }
		public string zipcode { get; set; }
	}

	public class DaumAddressToCoordChannel
	{
		public DaumAddressToCoordChannel()
		{
			item = new List<DaumAddressSearchItem>();
		}

		public string result { get; set; }
		public string pageCount { get; set; }
		public string totalCount { get; set; }
		public List<DaumAddressSearchItem> item { get; set; }
	}

	public class DaumAddressSearchItem
	{
		public string newAddress { get; set; }
		public string mountain { get; set; }
		public string buildingAddress { get; set; }
		public double lng { get; set; }
		public string placeName { get; set; }
		public string mainAddress { get; set; }
		public string id { get; set; }
		public double point_x { get; set; }
		public double point_y { get; set; }
		public string title { get; set; }
		public string isNewAddress { get; set; }
		public string point_wx { get; set; }
		public string point_wy { get; set; }
		public string subAddress { get; set; }
		public string localName_1 { get; set; }
		public string localName_2 { get; set; }
		public string localName_3 { get; set; }
		public double lat { get; set; }
		public string zipcode { get; set; }
	}

	public class DeliveryBannerCategory
	{
		public DeliveryBannerCategory()
		{
			Banner = new List<DeliveryBannerCategoryT>();
			Category = new List<DeliveryBannerCategoryT>();
		}

		public List<DeliveryBannerCategoryT> Banner { get; set; }
		public List<DeliveryBannerCategoryT> Category { get; set; }
	}

	public class DeliveryBannerCategoryT
	{
		public string BannerName { get; set; }
		public string CategoryName { get; set; }
		public string ImageUrl { get; set; }
		public string LinkUrl { get; set; }
	}

	public class DeliveryMain
	{
		public List<DeliveryBannerCategoryT> Banner { get; set; }
		public List<DeliveryBannerCategoryT> Category { get; set; }
		public List<DeliveryShop> MyShop { get; set; }
		public List<DeliveryShop> MyPositionShop { get; set; }

		public string MyPositionAddress { get; set; }
		public int CartCount { get; set; }
		public string CartLinkUrl { get; set; }
		public string UseDeliveryUrl { get; set; }
		public string FAQUrl { get; set; }
		public string FAQTel { get; set; }
		public string DeliveryServiceRequestUrl { get; set; }
		public string MyPositionBestUrl { get; set; }
		public string SrpUrl { get; set; }
	}

	public class DeliveryShop
	{
		public string Name { get; set; }
		public int StarCount { get; set; }
		public string CommentCount { get; set; }
		public string CanBuyConstraint { get; set; }
		public string DeliveryTime { get; set; }
		public string Benefit { get; set; }
		public string Tel { get; set; }
		public string Address { get; set; }
		public string LinkUrl { get; set; }
		public string ImageUrl { get; set; }
		public string BuyNowShowType { get; set; }
		public bool IsNonContractShop { get; set; }
		public string ShopTagType { get; set; }
	}

	public class DeliveryAgree
	{
		public bool IsAgreePositionInfo { get; set; }
		public bool IsAgreeThirdParty { get; set; }
	}

	// GMAPI 에도 있습니다. 오타있는데 2014.12.11 현재 동일합니다.
	public enum DeliveryPosityionAddType
	{
		None = 0,
		Position = 1,
		ThirdParty = 2
	}
}
