using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Biz.EventV2
{
    public class CouponzoneBiz
    {
        public TotalCouponzoneDataT GetCouponzoneInfo(string custNo)
        {
            if (custNo == "")
            {
                custNo = "NON";
            }

            TotalCouponzoneDataT total = new TotalCouponzoneDataT();
            ApiResponse<CouponzoneDataT> couponZone = new CouponzoneApiDac().GetCouponzoneInfo();
            ApiResponse<SpecialCouponDataT> specialCoupon = new CouponzoneApiDac().GetSpecialCouponInfo(custNo);

            total.Couponzone = new CouponzoneDataT();
            total.Couponzone = couponZone.Data;
            total.SpecialCoupon = new SpecialCouponDataT();
            total.SpecialCoupon = specialCoupon.Data;

            return total;
        }

		public CouponPackCustTypeCheckResultT CheckCouponPackCustType(long packNo, string custNo)
		{
			CouponPackCustTypeCheckResultT data = new CouponPackCustTypeCheckResultT();
			
			ApiResponse<CouponPackCustTypeCheckResultT> couponPackCheck = new CouponzoneApiDac().CheckCouponPackCustType(packNo, custNo);
			if(couponPackCheck != null) data = couponPackCheck.Data;

			return data;
		}

        public CouponPackDownloadResultT ApplyCouponPack(int seq, long packNo, string custNo, string custId, string siteType, string domainType)
        {
            //EventzoneDisplayInfoT display = GetEventzoneDisplayInfo(seq, eid);
            //이게 필요한건지 아닌지 모르겠다...

            ApiResponse<CouponPackDownloadResultT> couponZone = new CouponzoneApiDac().ApplyCouponPack((long)seq, packNo, custNo, custId, siteType, domainType);

            return couponZone.Data;
        }


        public EventzoneDisplayInfoT GetEventzoneDisplayInfo(int seq, int eid)
        {
            ApiResponse<EventzoneDisplayInfoT> couponZone = new CouponzoneApiDac().GetEventzoneDisplayInfo((long)seq, eid);

            return couponZone.Data;
        }

				public int GetDownloadedCouponPackCount( long couponPackNo )
				{
					ApiResponse<int> couponZone = new CouponzoneApiDac().GetDownloadedCouponPackCount( couponPackNo );
					return couponZone.Data;
				}

		public int GetCouponDownloadedCount(int eid, DateTime startDate, DateTime endDate)
		{
			ApiResponse<int> response = new CouponzoneApiDac().GetCouponDownloadedCount(eid, startDate, endDate);
			if(response != null)
			{
				return response.Data;
			}
			return 0;
		}

        public CouponValidCheckResultT GetCouponValidation(string checkType)
        {
            CouponValidCheckResultT result = new CouponValidCheckResultT()
            {
                ResultCode = CouponValidCheckResult.Fail,
                ResultMessage = "쿠폰 발급 과정에서 문제가 발생했습니다.",
                ResultType = string.Empty
            };
            ApiResponse<CouponValidCheckResultT> response = new CouponzoneApiDac().GetCouponValidation(checkType);
            if (response != null)
            {
                result = response.Data;
            }
            return result;
        }
    }
}
