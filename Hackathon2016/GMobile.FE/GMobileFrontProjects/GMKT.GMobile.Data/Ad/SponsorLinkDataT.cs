using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.Ad
{
    public class SponsorLinkDataT
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string cUrl { get; set; }
        public string vUrl { get; set; }
        public bool IsMobile { get; set; }
		public string PdsLogJson { get; set; }
    }

    public class SponsorLinkRequestT
    {
        public string Channel { get; set; }
        public int Count { get; set; }
        public string PrimeKeyword { get; set; }
        public string MoreKeyword { get; set; }
        public string LargeCategory { get; set; }
        public string MiddleCategory { get; set; }
        public string SmallCategory { get; set; }
    }
}
