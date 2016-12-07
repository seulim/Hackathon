using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using System.Web;

namespace GMKT.GMobile.Data
{
    public class BoardContentSearch
    {
        public int BoardID { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string UserInfo { get; set; }
    }
}
