using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;
using GMKT.MobileCache;

namespace GMKT.GMobile.Biz.EventV2
{
    public class CouponzoneBiz_Cache : CacheContextObject
    {
        [CacheDuration(DurationSeconds = 300)]
        public TotalCouponzoneDataT GetCouponzoneInfo(string custNo)
        {
            return new CouponzoneBiz().GetCouponzoneInfo(custNo);
        }
    }
}
