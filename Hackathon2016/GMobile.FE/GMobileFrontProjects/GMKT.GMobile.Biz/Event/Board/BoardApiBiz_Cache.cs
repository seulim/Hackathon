using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    public class BoardApiBiz_Cache : CacheContextObject
    {
        [CacheDuration(DurationSeconds = 600)]
        public BoardEntityT GetFromCacheBy(int id)
        {
            return new BoardApiBiz().GetBy(id);
        }
    }
}