using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.HomePlus;
using GMKT.Component.Member.Data.Entity;
using GMKT.GMobile.Data;
using Nova.Thrift;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Biz.HomePlus
{
	public class HomePlusApiBiz : BizBase
	{
		public HomePlusHomeT GetHome(int branchCode = 0)
		{
			return new HomePlusApiDac().GetHome(branchCode).Data;
		}

		public HomePlusSpecialShopT GetSpecialShop(long pageSeq, int branchCode = 0)
		{
			return new HomePlusApiDac().GetSpecialShop(pageSeq, branchCode).Data;
		}

		public List<CPPLPSRPItemModel> GetHomeSectionItem(string area, int pageNo, int pageSize, int branchCode)
		{
			return new HomePlusApiDac().GetHomeSectionItem(area, pageNo, pageSize, branchCode).Data;
		}

		public List<CPPLPSRPItemModel> GetSpecialShopSectionItem(long pageSeq, int pageNo, int pageSize, int branchCode)
		{
			return new HomePlusApiDac().GetSpecialShopSectionItem(pageSeq, pageNo, pageSize, branchCode).Data;
		}

		public SRPResultModel PostSearchItem(SearchRequest input)
		{
			SRPResultModel result = new SRPResultModel();

			ApiResponse<SRPResultModel> response = new HomePlusApiDac().PostSearchItem(input);
			if(response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}

		//주소록 리스트 
		public List<MyAddrListT> GetMyAddressList(string custNo, int pageNo = 1, int pageSize = 100, string kind = "K")
		{
			var response = new HomePlusApiDac().GetMyAddressList(custNo, pageNo, pageSize, kind);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		//우편번호 찾기
		public List<SelectAddressByZipT> GetAddressByZipList(string locationKeyword)
		{
			var response = new HomePlusApiDac().GetAddressByZipList(locationKeyword);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		//배송지점 설정 저장
		public ApiResponse<bool> GetAddPrimaryAddr(int addrNo, string custNo, string custId, string sellCustNo)
		{
			var response = new HomePlusApiDac().GetAddPrimaryAddr(addrNo, custNo, custId, sellCustNo);
			return response;
		}

		//배송지점 설정 저장  with 새로운주소
		public ApiResponse<bool> PostAddPrimaryAddrWithNew(AddPrimaryAddrWithNewRequestT request)
		{
			var response = new HomePlusApiDac().PostAddPrimaryAddrWithNew(request);
			if (response != null)
			{
				//if(response.ResultCode == 0 && response.Data != null){
				//    return response.Data.First();
				//}

				return response;

			}
			else
			{
				return null;
			}
		}

		//배송보낼 고객주소
		public SpecialShopPrimaryAddrT GetSpecialShopPrimaryAddr(string custNo, string sellCustNo)
		{
			var response = new HomePlusApiDac().GetSpecialShopPrimaryAddr(custNo, sellCustNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data.First();
			}
			else
			{
				return null;
			}
		}

		//고객이 설정한 당일배송관 정보 & 배송시간표
		public SpecialShopZipBranchMatchingT GetSpecialShopBranchInfo(string zipCd, string sellCustNo)
		{
			var response = new HomePlusApiDac().GetSpecialShopBranchInfo(zipCd, sellCustNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}


		//고객이 설정한 당일배송관 정보 & 배송시간표
		public SpecialShopBranchInfoTimeTable GetSpecialShopBranchInfoTimeTable(string zipCd, string sellCustNo, DateTime deliveryStartDt, DateTime deliveryEndDt)
		{
			var response = new HomePlusApiDac().GetSpecialShopBranchInfoTimeTable(zipCd, sellCustNo, deliveryStartDt, deliveryEndDt);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		public SpecialShopCategory GetSpecialShopCategoryList(string sellCustNo)
		{
			var response = new HomePlusApiDac().GetSpecialShopCategoryList(sellCustNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		public ItemMinUnitBuyCountT GetItemMinUnitBuyCount(string itemNo)
		{
			var response = new HomePlusApiDac().GetItemMinUnitBuyCount(itemNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		#region nova thrift
		//cartPID
		public CartPIDResultI GetCartPIDResult(string cartPID, string buyerNo, bool isLoginForm)
		{
			var response = new HomePlusApiDac().GetCartPIDResult(cartPID, buyerNo, isLoginForm);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		//장바구니 목록 조회
		public CartListResultI GetCartResult(int branchCd = 0)
		{
			var response = new HomePlusApiDac().GetCartResult(branchCd);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		//장바구니 내 다른지점상품 존재여부 조회
		public ResultI GetCartBranchDataResult(int branchCd = 0)
		{
			var response = new HomePlusApiDac().GetCartBranchDataResult(branchCd);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}

		#endregion
	}
}
