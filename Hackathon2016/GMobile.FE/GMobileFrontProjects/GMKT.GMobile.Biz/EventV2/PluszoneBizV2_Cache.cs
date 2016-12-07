using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Biz.EventV2
{
    public class PluszoneBizV2_Cache : CacheContextObject
    {

        [CacheDuration(DurationSeconds = 300)]
        public ApiResponse<PluszoneDataT> GetPluszoneInfo()
        {
            return new PluszoneBizV2().GetPluszoneInfo();
        }
    }
}
