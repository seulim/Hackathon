using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using Arche.Data.Voyager;
using GMobile.Data.Voyager;
using GMobile.Service.Search;
using System.Collections;
using GMKT.GMobile.Util;
using GMKT.GMobile.Constant;
using System.Web;

namespace GMKT.GMobile.Biz
{
	public class LPSRPSmartClickBiz
	{
		public LPSRPSmartClickBiz()
		{
		}

		public class SmartADT
		{
			public string GdNo { get; set; }
			public string Name { get; set; }
			public string ClickURL { get; set; }
			public string ImageURL { get; set; }
			public string OriginalPrice { get; set; }
			public string Price { get; set; }
			public string DeliveryInfo { get; set; }
		}

		public List<SmartADT> GetAds(string channelID, string adType, string category, string keyword,
			string serverURL, bool isAdultUse, bool isLogin, string custNo, string UA, string Ref, string userIP)
		{
			string url = Urls.NVistaURL + "/rest/adRequest?" +
			"Query=" + (adType.Equals("category") ? category : keyword ) + "&" +
			"ChannelID=" + channelID +
			"&MaxCount=5&StartRank=1&AdultFilter=1&ProductType=gmkt&" +
			"ServerURL=" + serverURL + "&" +
			"AccTime=" + HttpUtility.UrlEncode(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) +
			"&UserIP=" + userIP + "&" +
			"UA=" + UA + "&Ref=" + Ref + "&" +
			"AdType=" + adType + "&LQuery=" + category;
			WebClient client = new WebClient();
			client.Encoding = Encoding.UTF8;
			string html = client.DownloadString(url);

			if ( html == null || "".Equals( html.Trim() ) ) return null;

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(html);

			List<SmartADT> itemList = new List<SmartADT>();

			if ( doc.SelectNodes("Results/Item").Count < 1 ) return null;

			List<string> list = new List<string>();
			foreach (XmlNode node in doc.SelectNodes("Results/Item"))
			{
				SmartADT item = new SmartADT();
				item.GdNo = node.SelectSingleNode("ItemID").InnerText;
				item.Name = node.SelectSingleNode("ItemTitle").InnerText;
				item.ClickURL = node.SelectSingleNode("ClickURL").InnerText;
				//item.ClickURL = item.ClickURL + "&clk=list&fwurl=" + HttpUtility.UrlEncode( node.SelectSingleNode("FwURL").InnerText );
				item.ClickURL = item.ClickURL + "&clk=list&fwurl=" + Urls.MItemWebURL + "/Item?goodscode=" + item.GdNo;

				list.Add(item.GdNo);
				itemList.Add(item);
			}

			if (list.Count < 1) return null;

			SearchItemT[] vResult = GetItems(list, "focus_rank_desc" );

			for (int i = itemList.Count - 1; i >= 0 ; i--)
			{
				bool bFound = false;

				for (int j = 0; j < vResult.Length; j++)
				{
					if (long.Parse(itemList[i].GdNo) == vResult[j].GdNo)
					{
						SmartADT item = itemList[i];

						item.ImageURL = GetImageURL(vResult[j], isAdultUse, isLogin, custNo);

						string originalPrice = CurrencyUtil.ToMoney(vResult[j].SellPrice.ToString(), true);
						decimal price = vResult[j].SellPrice;

						if (vResult[j].Discount.DiscountPrice > 0)
							price = price - vResult[j].Discount.DiscountPrice;
						else if (vResult[j].Discount.DiscountPrice < 0)
							price = price + vResult[j].Discount.DiscountPrice;
						else
							originalPrice = "";

						if ( price < 10 )
						{
							item.Price = "무료";
							item.OriginalPrice = "";
						}
						else
						{
							item.Price = CurrencyUtil.ToMoney(price.ToString(), true);
							item.OriginalPrice = originalPrice;
						}

						item.DeliveryInfo = vResult[j].Delivery.DeliveryInfo;

						bFound = true;
						break;
					}
				}

				if ( bFound == false )
				{
					itemList.Remove( itemList[i] );
				}
			}

			return itemList;
		}

		public SearchItemT[] GetItems(List<string> itemNoList, string sortType )
		{
			SearchItemT[] result = null;

			ItemFilter filter = new ItemFilter();

			filter.ItemNoList = itemNoList;

			List<string> categoryNotOrList = new List<string>();
			categoryNotOrList.Add("200001957");
			categoryNotOrList.Add("200001468");
			categoryNotOrList.Add("200001955");
			categoryNotOrList.Add("200001578");
			categoryNotOrList.Add("200000892");
			categoryNotOrList.Add("200001530");
			categoryNotOrList.Add("200002122");
			categoryNotOrList.Add("200001456");
			categoryNotOrList.Add("200001078");
			categoryNotOrList.Add("200002064");
			filter.CategoryNotOrList = categoryNotOrList;

			List<int> tradewayNotOrList = new List<int>();
			tradewayNotOrList.Add((int)TradeWay.ReservedItem);
			filter.TradewayNotOrList = tradewayNotOrList;

			#region ::: Set Sort Option
			SortCollection sc = new SortCollection();
			switch (sortType.ToLower())
			{
				case "focus_rank_desc":
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "rank_point_desc":
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "sell_point_desc":
					sc.Add(Sort.Create("SellPointInfo", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "disc_price_desc":
					sc.Add(Sort.Create("MACRO(DiscountPrice)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					break;
				case "sell_price_asc":
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Increasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "sell_price_desc":
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "new":
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "con_point_desc":
					sc.Add(Sort.Create("ConPoint", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				default:
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
			}
			#endregion

			result = new SearchItemBiz().GetItems(filter, 0, itemNoList.Count, sc);

			return result;
		}

		private string GetImageURL(SearchItemT item, bool isAdultUse, bool isLogin, string custNo)
		{
			if (item.AdultYN && !isAdultUse)
			{
				return Urls.MobileImageUrlV2 + Const.ADULT_IMAGE_210.Replace("images/", string.Empty);
			}
			else
				return BizUtil.GetGoodsImagePath(item.GdNo.ToString(), "M3");
		}
	}
}
