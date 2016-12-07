using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;

namespace GMKT.GMobile.Biz
{
    public class LandingBannerApiBiz_Cache : CacheContextObject
    {
        [CacheDuration(DurationSeconds = 600)]
        public Data.ILandingBannerEntityT GetFromCacheBy(Data.LandingBannerType type)
        {
            return new LandingBannerApiBiz().GetBy(new Search() { Date = DateTime.Now, Type = type });
        }

        class Search : Data.ILandingBannerSearch
        {
            public DateTime Date { get; set; }

            public Data.LandingBannerType Type { get; set; }
        }
    }
}
