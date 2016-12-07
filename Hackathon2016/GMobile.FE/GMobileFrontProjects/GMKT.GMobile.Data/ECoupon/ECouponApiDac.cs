using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMobile.Data;

using System.IO;
using System.Net;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data;

using ConnApi.Client;


namespace GMKT.GMobile.Data
{
	public class ECouponApiDac : ApiBase
	{
		public ECouponApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<ECouponHome> GetECouponHome()
		{
			ApiResponse<ECouponHome> result = ApiHelper.CallAPI<ApiResponse<ECouponHome>>(
					"GET",
					ApiHelper.MakeUrl("api/ECoupon/GetECouponHome")
			);
			return result;
		}

		public ApiResponse<Nova.Thrift.AddCartResultI> GetAddCartResult(string itemNo, short orderQty, bool isInstantOrder, string branchZipCode = "")
		{
			//request형식문제 error나서 임의값 "0" 부여..
			if (branchZipCode == "") branchZipCode = "0";

			ApiResponse<Nova.Thrift.AddCartResultI> result = ApiHelper.CallAPI<ApiResponse<Nova.Thrift.AddCartResultI>>(
					"GET"
					,ApiHelper.MakeUrl("api/ECoupon/GetAddCartResult")
					,new
					{
						itemNo = itemNo ,
						orderQty = orderQty,
						isInstantOrder = isInstantOrder,
						branchZipCode = branchZipCode
					}
					, ConnApiUtil.GetUserInfoCookieParameter()
					, ConnApiUtil.GetOrderInfoCookieParameter()
					, ConnApiUtil.GetEtcInfoCookieParameter()
					, ConnApiUtil.GetPguidCookieParameter()
					, ConnApiUtil.GetCguidCookieParameter()
					, ConnApiUtil.GetSguidCookieParameter()
			);
			return result;
		}

		public ApiResponse<Nova.Thrift.ResultI> GetRemoveCartResult(string cartPID, string orderIdxString)
		{
			ApiResponse<Nova.Thrift.ResultI> result = ApiHelper.CallAPI<ApiResponse<Nova.Thrift.ResultI>>(
					"GET"
					, ApiHelper.MakeUrl("api/ECoupon/GetRemoveCartResult")
					, new
					{
						cartPID = cartPID,
						orderIdxString = orderIdxString
					}
			);
			return result;
		}

        /// <summary>
        /// 브랜드 LP 리스트 조회
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        public ApiResponse<ECouponBrandLpInfo> GetECouponBrandLp(string categoryCode)
        {
            ApiResponse<ECouponBrandLpInfo> result = ApiHelper.CallAPI<ApiResponse<ECouponBrandLpInfo>>(
                    "GET",
                    ApiHelper.MakeUrl("api/ECoupon/GetECouponBrandLp"),
                    new 
                    {
                        categoryCode = categoryCode
                    }
            );
            return result;
        }

        /// <summary>
        /// 브랜드 LP 리스트 더보기 조회
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ApiResponse<ECouponBrandLpMore> GetECouponBrandLpMore(string categoryCode, int pageIndex, int pageSize)
        {
            ApiResponse<ECouponBrandLpMore> result = ApiHelper.CallAPI <ApiResponse<ECouponBrandLpMore>>(
                    "GET",
                    ApiHelper.MakeUrl("api/ECoupon/GetECouponBrandLpInfoMore"),
                    new
                    {
                        categoryCode = categoryCode,
                        pageIndex = pageIndex,
                        pageSize = pageSize
                    }
            );
            return result;
        }

        /// <summary>
        /// 브랜드홈 데이터 조회
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <param name="brandCode"></param>
        /// <returns></returns>
        public ApiResponse<ECouponBrandHomeInfo> GetECouponBrandHomeInfo(string categoryCode, int brandCode)
        {
            ApiResponse<ECouponBrandHomeInfo> result = ApiHelper.CallAPI<ApiResponse<ECouponBrandHomeInfo>>(
                "GET",
                ApiHelper.MakeUrl("api/ECoupon/GetECouponBrandHomeInfo"),
                new
                {
                    categoryCode = categoryCode,
                    brandCode = brandCode
                }
            );
            return result;
        }

        public ApiResponse<ECouponBrandMenuMore> GetECouponBrandHomeMenuMore(int brandCode, int pageIndex, int pageSize)
        {
            ApiResponse<ECouponBrandMenuMore> result = ApiHelper.CallAPI<ApiResponse<ECouponBrandMenuMore>>(
                "GET",
                ApiHelper.MakeUrl("api/ECoupon/GetECouponBrandHomeMenuMore"),
                new
                {
                    brandCode = brandCode,
                    pageIndex = pageIndex,
                    pageSize = pageSize
                }
            );
            return result;
        }
	}
}
