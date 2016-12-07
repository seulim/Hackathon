using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMobile.Data;
using System.IO;
using System.Net;
using GMKT.GMobile.Util;
using GMKT.GMobile;
using ConnApi.Client;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Data
{
	public class MobileCommonApiDac : ApiBase
	{
		public MobileCommonApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<List<HomeTabNames>> GetMobileHomeTabNames()
		{
			ApiResponse<List<HomeTabNames>> result = ApiHelper.CallAPI<ApiResponse<List<HomeTabNames>>>(
				"GET",
				ApiHelper.MakeUrl("api/MobileHome/GetMobileHomeTabNames")
			);
			return result;
		}

		public ApiResponse<PushAgreementResultT> SetPushServiceAgreementInfo(string pif, string sif, int appNo, string pushAgreementYn, string regId)
		{
			ApiResponse<PushAgreementResultT> result = ApiHelper.CallAPI<ApiResponse<PushAgreementResultT>>(
				"POST",
				ApiHelper.MakeUrl("api/Login/SetPushServiceAgreementInfo"),
				new
				{
					pif = pif,
					sif = sif,
					appNo = appNo,
					pushServiceAgreementYN = pushAgreementYn,
					regId = regId					
				},
				ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<PushAgreementInfoT> GetPushServiceAgreementInfo(string pif, string sif, int appNo)
		{
			ApiResponse<PushAgreementInfoT> result = ApiHelper.CallAPI<ApiResponse<PushAgreementInfoT>>(
				"POST",
				ApiHelper.MakeUrl("api/Login/GetPushServiceAgreementInfo"),
				new
				{
					pif = pif,
					sif = sif,
					appNo = appNo
				},
				ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<RegInterestItemsInfo> RegInterestItems(string custNo, string itemNos)
		{
			ApiResponse<RegInterestItemsInfo> result = ApiHelper.CallAPI<ApiResponse<RegInterestItemsInfo>>(
				"POST",
				ApiHelper.MakeUrl("api/Item/RegInterestItems"),
				new
				{
					CustNo = custNo,
					GoodsCodeList = itemNos
				}
			);
			return result;
		}

		public ApiResponse<int> GetCartCount(string pid)
		{
			string url = ApiHelper.MakeUrl("api/Header/GetCartCount");

			if(!string.IsNullOrEmpty(pid))
			{
				url = string.Format("{0}?pid={1}", url, pid);
			}

			ApiResponse<int> result = ApiHelper.CallAPI<ApiResponse<int>>(
				"GET",
				url,
				ConnApiUtil.GetUserInfoCookieParameter(),
				ConnApiUtil.GetOrderInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<DynamicHeader> GetDynamicHeader(HeaderTypeEnum type, string code)
		{
			ApiResponse<DynamicHeader> result = ApiHelper.CallAPI<ApiResponse<DynamicHeader>>(
				"GET",
				ApiHelper.MakeUrl("api/Common/GetDynamicHeader"),
				new
				{
					type = type,
					code = code
				}
			);

			return result;
		}
	}
}
