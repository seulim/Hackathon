using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.GMobile.Util;
using GMKT.GMobile.Data;
using ConnApi.Client;
using System.Collections.Specialized;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Biz
{
	public class MobileCommonBiz
	{
		public ApiResponse<List<HomeTabNames>> GetMobileHomeTabNames()
		{
			return new GMobile.Data.MobileCommonApiDac().GetMobileHomeTabNames();
		}

		public List<HomeTabNames> GetMobileHomeTabList()
		{
			List<HomeTabNames> result = new List<HomeTabNames>();

			result.Add(new HomeTabNames()
			{
				TabName = "홈",
                ImagePath = "type1",
				LinkPath = Urls.MobileWebUrl
			});

			//result.Add(new HomeTabNames()
			//{
			//    TabName = "슈퍼딜",
			//    ImagePath = "",
			//    LinkPath = Urls.MobileWebUrl + "/SuperDeal"
			//});

			result.Add(new HomeTabNames()
			{
				TabName = "베스트",
                ImagePath = "type2",
				LinkPath = Urls.MobileWebUrl + "/Best"
			});

			//result.Add(new HomeTabNames()
			//{
			//    TabName = "쿠폰",
			//    ImagePath = "",
			//    LinkPath = Urls.MobileWebUrl + "/Pluszone"
			//});

			result.Add(new HomeTabNames()
			{
				TabName = "백화점",
                ImagePath = "type2",
				LinkPath = Urls.MobileWebUrl + "/DepartmentStore"
			});

			result.Add(new HomeTabNames()
			{
				TabName = "패션",
                ImagePath = "type3",
				LinkPath = Urls.MobileWebUrl + "/Look"
			});

			result.Add(new HomeTabNames()
			{
				TabName = "공간",
                ImagePath = "type3",
				LinkPath = Urls.MobileWebUrl + "/Space"
			});

			result.Add(new HomeTabNames()
			{
				TabName = "마트",
                ImagePath = "type3",
				LinkPath = Urls.MobileWebUrl + "/Mart"
			});

			result.Add(new HomeTabNames()
			{
				TabName = "배달",
                ImagePath = "type3",
				LinkPath = Urls.MobileWebUrl + "/Delivery"
			});

			result.Add(new HomeTabNames()
			{
				TabName = "여행",
                ImagePath = "type3",
				LinkPath = Urls.MobileWebUrl + "/Tour"
			});
            result.Add(new HomeTabNames()
            {
                TabName = "e쿠폰",
                ImagePath = "type2",
                LinkPath = Urls.MobileWebUrl + "/ECoupon"
               
            });
			//result.Add(new HomeTabNames()
			//{
			//    TabName = "기프트",
			//    ImagePath = "",
			//    LinkPath = Urls.MobileWebUrl + "/ECoupon"
			//});

			return result;
		}

		public PushAgreementResultT SetPushServiceAgreementInfo(string pif, string sif, int appNo, string pushAgreementYn, string regId)
		{
			PushAgreementResultT result = new PushAgreementResultT();

			ApiResponse<PushAgreementResultT> response = new MobileCommonApiDac().SetPushServiceAgreementInfo(pif, sif, appNo, pushAgreementYn.ToUpper(), regId);

			if(response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}

			if(result == null) result = new PushAgreementResultT();

			return result;
		}

		public PushAgreementInfoT GetPushServiceAgreementInfo(string pif, string sif, int appNo)
		{
			PushAgreementInfoT result = new PushAgreementInfoT();

			ApiResponse<PushAgreementInfoT> response = new MobileCommonApiDac().GetPushServiceAgreementInfo(pif, sif, appNo);

			if(response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}

			if(result == null) result = new PushAgreementInfoT();

			return result;
		}

		public RegInterestItemsInfo RegInterestItems(string custNo, string itemNos)
		{
			RegInterestItemsInfo result = new RegInterestItemsInfo();

			ApiResponse<RegInterestItemsInfo> response = new MobileCommonApiDac().RegInterestItems(custNo, itemNos);

			if(response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}

			if(result == null) result = new RegInterestItemsInfo();

			return result;
		}

		public int GetCartCount(string pid)
		{
			int result = 0;

			ApiResponse<int> response = new MobileCommonApiDac().GetCartCount(pid);

			if(response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}

			return result;
		}

		public DynamicHeader GetDynamicHeader(HeaderTypeEnum type, string goodsCode)
		{
			DynamicHeader result = new DynamicHeader();

			ApiResponse<DynamicHeader> response = new MobileCommonApiDac().GetDynamicHeader(type, goodsCode);
			if(response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}
	}
}
