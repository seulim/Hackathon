using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{
    public class NavigationIconT
    {
        public long EventzoneDisplaySeq { get; set; }
        public Nullable<Int16> Priority { get; set; }
        public string MlangBannerName { get; set; }
        public string LinkUrl { get; set; }
        public string IconOnImageUrl { get; set; }
        public string IconOffImageUrl { get; set; }
        public string MlangBannerImageText { get; set; }
        public string EventzoneExposeCodeName { get; set; }
        public string SmallClassCodeName { get; set; }
    }
}
