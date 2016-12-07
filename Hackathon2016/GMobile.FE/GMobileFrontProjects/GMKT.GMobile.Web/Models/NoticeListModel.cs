using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GMobile.Data.DisplayDB;

namespace GMKT.GMobile.Web.Models
{
	public partial class NoticeListModel
	{
		public int pageNo { get; set; }
		public string nKind { get; set; }
		public int totalCount { get; set; }
		public List<NoticeT> NoticeList { get; set; }
	}
}