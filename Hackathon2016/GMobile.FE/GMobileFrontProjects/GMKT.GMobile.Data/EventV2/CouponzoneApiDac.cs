using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.GMobile.Util;


namespace GMKT.GMobile.Data.EventV2
{
    public class CouponzoneApiDac : ApiBase
    {
        public CouponzoneApiDac()
			: base("GMApi")
		{
		}

        public ApiResponse<CouponzoneDataT> GetCouponzoneInfo()
        {
            ApiResponse<CouponzoneDataT> result = ApiHelper.CallAPI<ApiResponse<CouponzoneDataT>>(
                "GET",
                ApiHelper.MakeUrl("api/Pluszone/GetCouponzoneInfo")
            );
            return result;
        }

        public ApiResponse<SpecialCouponDataT> GetSpecialCouponInfo(string custNo)
        {
            ApiResponse<SpecialCouponDataT> result = ApiHelper.CallAPI<ApiResponse<SpecialCouponDataT>>(
                "GET",
                ApiHelper.MakeUrl("api/Pluszone/GetSpecialCoupon"),
                new
                {
                    buyerNo = custNo
                }
            );
            return result;
        }

        public ApiResponse<EventzoneDisplayInfoT> GetEventzoneDisplayInfo(long seq, int eid)
        {
            ApiResponse<EventzoneDisplayInfoT> result = ApiHelper.CallAPI<ApiResponse<EventzoneDisplayInfoT>>(
                "GET",
                ApiHelper.MakeUrl("api/Pluszone/GetEventzoneDisplayInfo"),
                new
                {
                    displayNo = seq,
                    eid = eid
                }
            );
            return result;
        }

		public ApiResponse<CouponPackCustTypeCheckResultT> CheckCouponPackCustType(long packNo, string custNo)
		{
			ApiResponse<CouponPackCustTypeCheckResultT> result = ApiHelper.CallAPI<ApiResponse<CouponPackCustTypeCheckResultT>>(
				"GET",
				ApiHelper.MakeUrl("api/Pluszone/GetCheckCouponPackCustType"),
				new 
				{
					couponPackNo = packNo,
					buyerNo = custNo
				}
			);

			return result;
		}

        public ApiResponse<CouponPackDownloadResultT> ApplyCouponPack(long seq, long packNo, string custNo, string custId, string siteType, string domainType)
        {
            ApiResponse<CouponPackDownloadResultT> result = ApiHelper.CallAPI<ApiResponse<CouponPackDownloadResultT>>(
                "GET",
                ApiHelper.MakeUrl("api/Pluszone/GetApplyCouponPack"),
                new
                {
                    displayNo = seq,
                    couponPackNo = packNo,
                    buyerNo = custNo,
                    buyerId = custId,
                    siteType = siteType,
                    domainType = domainType
                }
            );
            return result;
        }

				public ApiResponse<int> GetDownloadedCouponPackCount( long couponPackNo )
				{
					ApiResponse<int> result = ApiHelper.CallAPI<ApiResponse<int>>(
							"GET",
							ApiHelper.MakeUrl( "api/EventCommon/GetDownloadedCouponPackCount" ),
							new
							{
								couponPackNo = couponPackNo,
							}
							, ConnApiUtil.GetUserInfoCookieParameter()
					);
					return result;
				}

		public ApiResponse<int> GetCouponDownloadedCount(int eid, DateTime startDate, DateTime endDate)
		{
			ApiResponse<int> result = ApiHelper.CallAPI<ApiResponse<int>>(
				"GET",
				ApiHelper.MakeUrl("api/EventCommon/GetCouponDownloadedCount"),
				new
				{
					eid = eid,
					startDate = startDate,
					endDate = endDate
				}
				, ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

        public ApiResponse<CouponValidCheckResultT> GetCouponValidation(string checkType)
        {
            ApiResponse<CouponValidCheckResultT> result = ApiHelper.CallAPI<ApiResponse<CouponValidCheckResultT>>(
                "GET",
                ApiHelper.MakeUrl("api/EventCommon/GetCouponValidation"),
                new
                {
                    checkType  = checkType
                },
                ConnApiUtil.GetUserInfoCookieParameter()
            );
            return result;
        }
    }
}
