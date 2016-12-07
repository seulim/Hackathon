using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class ECouponApiBiz : BizBase
	{
		#region 모바일 e쿠폰 홈
		public ECouponHomeApiResponse GetECouponHome()
		{
			ECouponHomeApiResponse result = new ECouponHomeApiResponse();

			ApiResponse<ECouponHome> response = new ECouponApiDac().GetECouponHome();
			if (response != null)
			{
				result.Data = response.Data;
			}

			if (result == null) 
			{
				result = new ECouponHomeApiResponse();
				result.Data = new ECouponHome();
			}

			return result;

		}
		#endregion

		#region 모바일 e쿠폰 장바구니 추가
		/// <summary>
		/// Nova 장바구니 추가 호출 후 결과 Return
		/// </summary>
		/// <param name="itemNo">상품번호</param>
		/// <param name="orderQty">구매수량</param>
		/// <returns></returns>
		public ApiResponse<Nova.Thrift.AddCartResultI> GetAddCartResult(string itemNo, short orderQty, bool isInstantOrder, string branchZipCode ="")
		{
			ApiResponse<Nova.Thrift.AddCartResultI> addCartResultI = new ECouponApiDac().GetAddCartResult(itemNo, orderQty, isInstantOrder, branchZipCode);
			return addCartResultI;
		}
		#endregion

		#region 모바일 e쿠폰 장바구니 삭제
		/// <summary>
		/// Nova 장바구니 삭제 호출 후 결과 Return
		/// </summary>
		public ApiResponse<Nova.Thrift.ResultI> GetRemoveCartResult(string cartPID, string orderIdxString)
		{
			ApiResponse<Nova.Thrift.ResultI> removeCartResult = new ECouponApiDac().GetRemoveCartResult(cartPID, orderIdxString);
			return removeCartResult;
		}
		#endregion
	}
}
