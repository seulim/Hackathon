using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.Component.Member.Data.Entity;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Util;
using Nova.Thrift;

namespace GMKT.GMobile.Data.HomePlus
{
	public class HomePlusApiDac : ApiBase
	{
		public HomePlusApiDac()
			: base("GMApi"){}

		public ApiResponse<List<CPPLPSRPItemModel>> GetSpecialShopSectionItem(long pageSeq, int pageNo, int pageSize, int branchCode)
		{
			ApiResponse<List<CPPLPSRPItemModel>> result = ApiHelper.CallAPI<ApiResponse<List<CPPLPSRPItemModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetSpecialShopSectionItem"),
				new { branchCode, pageNo, pageSize, pageSeq }
			);
			return result;
		}

		public ApiResponse<List<CPPLPSRPItemModel>> GetHomeSectionItem(string area, int pageNo, int pageSize, int branchCode)
		{
			ApiResponse<List<CPPLPSRPItemModel>> result = ApiHelper.CallAPI<ApiResponse<List<CPPLPSRPItemModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetHomeSectionItem"),
				new { branchCode, pageNo, pageSize, area }
			);
			return result;
		}

		public ApiResponse<HomePlusSpecialShopT> GetSpecialShop(long pageSeq, int branchCode = 0)
		{
			ApiResponse<HomePlusSpecialShopT> result = ApiHelper.CallAPI<ApiResponse<HomePlusSpecialShopT>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetSpecialShop"),
				new { branchCode, pageSeq }
			);
			return result;
		}

		public ApiResponse<HomePlusHomeT> GetHome(int branchCode)
		{
			ApiResponse<HomePlusHomeT> result = ApiHelper.CallAPI<ApiResponse<HomePlusHomeT>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetHome"),
				new { branchCode }
			);
			return result;
		}

		public ApiResponse<SRPResultModel> PostSearchItem(SearchRequest input)
		{
			ApiResponse<SRPResultModel> result = ApiHelper.CallAPI<ApiResponse<SRPResultModel>>(
				"POST",
				ApiHelper.MakeUrl("api/HomePlus/PostSearchItem"),
				input
			);
			return result;
		}


		//주소록 리스트 
		public ApiResponse<List<MyAddrListT>> GetMyAddressList(string custNo, int pageNo = 1, int pageSize = 100, string kind = "K")
		{
			ApiResponse<List<MyAddrListT>> result = ApiHelper.CallAPI<ApiResponse<List<MyAddrListT>>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetMyAddressList"),
				new
				{
					custNo = custNo,
					pageNo = pageNo,
					pageSize = pageSize,
					kind = kind
				}
			);

			return result;
		}

		//우편번호 찾기
		public ApiResponse<List<SelectAddressByZipT>> GetAddressByZipList(string locationKeyword)
		{
			ApiResponse<List<SelectAddressByZipT>> result = ApiHelper.CallAPI<ApiResponse<List<SelectAddressByZipT>>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetAddressByZipList"),
				new
				{
					locationKeyword = locationKeyword
				}
			);

			return result;
		}

		//배송지점 설정 저장
		public ApiResponse<bool> GetAddPrimaryAddr(int addrNo, string custNo, string custId, string sellCustNo)
		{
			ApiResponse<bool> result = ApiHelper.CallAPI<ApiResponse<bool>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetAddPrimaryAddr"),
				new
				{
					addrNo = addrNo,
					custNo = custNo,
					custId = custId,
					sellCustNo = sellCustNo
				}
			);

			return result;
		}

		//배송지점 설정 저장  with 새로운주소
		public ApiResponse<bool> PostAddPrimaryAddrWithNew(AddPrimaryAddrWithNewRequestT request)
		{
			ApiResponse<bool> result = ApiHelper.CallAPI<ApiResponse<bool>>(
				"POST",
				ApiHelper.MakeUrl("api/HomePlus/PostAddPrimaryAddrWithNew"),
				request
			);

			return result;
		}


		//배송보낼 고객주소
		public ApiResponse<List<SpecialShopPrimaryAddrT>> GetSpecialShopPrimaryAddr(string custNo, string sellCustNo)
		{
			ApiResponse<List<SpecialShopPrimaryAddrT>> result = ApiHelper.CallAPI<ApiResponse<List<SpecialShopPrimaryAddrT>>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetSpecialShopPrimaryAddr"),
				new
				{
					custNo = custNo,
					sellCustNo = sellCustNo
				}
			);

			return result;
		}

		//당일배송관 정보
		public ApiResponse<SpecialShopZipBranchMatchingT> GetSpecialShopBranchInfo(string zipCd, string sellCustNo)
		{
			ApiResponse<SpecialShopZipBranchMatchingT> result = ApiHelper.CallAPI<ApiResponse<SpecialShopZipBranchMatchingT>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetSpecialShopBranchInfo"),
				new
				{
					zipCd = zipCd,
					sellCustNo = sellCustNo
				}
			);

			return result;
		}


		//고객이 설정한 당일배송관 정보 & 배송시간표
		public ApiResponse<SpecialShopBranchInfoTimeTable> GetSpecialShopBranchInfoTimeTable(string zipCd, string sellCustNo, DateTime deliveryStartDt, DateTime deliveryEndDt)
		{
			ApiResponse<SpecialShopBranchInfoTimeTable> result = ApiHelper.CallAPI<ApiResponse<SpecialShopBranchInfoTimeTable>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetSpecialShopBranchInfoTimeTable"),
				new
				{
					zipCd = zipCd,
					sellCustNo = sellCustNo,
					deliveryStartDt = deliveryStartDt.ToString("yyyy-MM-dd HH:mm:ss"),
					deliveryEndDt = deliveryEndDt.ToString("yyyy-MM-dd HH:mm:ss")
				}
			);

			return result;
		}

		public ApiResponse<SpecialShopCategory> GetSpecialShopCategoryList(string sellCustNo)
		{
			ApiResponse<SpecialShopCategory> result = ApiHelper.CallAPI<ApiResponse<SpecialShopCategory>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetSpecialShopCategoryList"),
				new
				{
					sellCustNo = sellCustNo
				}
			);

			return result;
		}
		//상품 최저수량, 묶음수량 
		public ApiResponse<ItemMinUnitBuyCountT> GetItemMinUnitBuyCount(string itemNo)
		{
			ApiResponse<ItemMinUnitBuyCountT> result = ApiHelper.CallAPI<ApiResponse<ItemMinUnitBuyCountT>>(
				"GET",
				ApiHelper.MakeUrl("api/HomePlus/GetItemMinUnitBuyCount"),
				new
				{
					itemNo = itemNo
				}
			);

			return result;
		}
		//cartPID
		public ApiResponse<CartPIDResultI> GetCartPIDResult(string cartPID, string buyerNo, bool isLoginForm)
		{
			ApiResponse<Nova.Thrift.CartPIDResultI> result = ApiHelper.CallAPI<ApiResponse<Nova.Thrift.CartPIDResultI>>(
					"GET"
					, ApiHelper.MakeUrl("api/HomePlus/GetCartPIDResult")
					, new
					{
						cartPID= cartPID,
						buyerNo = buyerNo,
						isLoginForm = isLoginForm
					}
			);
			return result;
		}

		//장바구니 목록 조회
		public ApiResponse<Nova.Thrift.CartListResultI> GetCartResult(int branchCd = 0)
		{
			ApiResponse<Nova.Thrift.CartListResultI> result = ApiHelper.CallAPI<ApiResponse<Nova.Thrift.CartListResultI>>(
					"GET"
					, ApiHelper.MakeUrl("api/HomePlus/GetCartResult")
					, new
					{
						branchCd = branchCd
					}
					, ConnApiUtil.GetUserInfoCookieParameter()
					, ConnApiUtil.GetOrderInfoCookieParameter()
					, ConnApiUtil.GetEtcInfoCookieParameter()
			);
			return result;
		}

		//장바구니 내 다른지점상품 존재여부 조회
		public ApiResponse<Nova.Thrift.ResultI> GetCartBranchDataResult(int branchCd = 0)
		{
			ApiResponse<Nova.Thrift.ResultI> result = ApiHelper.CallAPI<ApiResponse<Nova.Thrift.ResultI>>(
					"GET"
					, ApiHelper.MakeUrl("api/HomePlus/GetCartBranchDataResult")
					, new
					{
						branchCd = branchCd
					}
					, ConnApiUtil.GetUserInfoCookieParameter()
					, ConnApiUtil.GetOrderInfoCookieParameter()
					, ConnApiUtil.GetEtcInfoCookieParameter()
			);
			return result;
		}
	}
}
