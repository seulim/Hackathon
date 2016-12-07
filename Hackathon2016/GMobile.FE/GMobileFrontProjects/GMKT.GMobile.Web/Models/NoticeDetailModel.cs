using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.DisplayDB;

namespace GMKT.GMobile.Web.Models
{
	public partial class NoticeDetailModel
	{
		public string queryKind { get; set; }
		public NoticeT Notice { get; set; }
	}
}