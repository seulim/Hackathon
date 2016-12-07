using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
    public class FaqModel
    {
		/************* FAQ CateGory 시작 *************/
		public string DefaultCd { get; set; }

		public List<FaqCategoryT> FaqCategoryList { get; set; }
		public List<FaqListT> Items { get; set; }		

		public Int32 PageNo { get; set; }
		public Int32 NextPageNo { get; set; }
		public Int32 TotalCnt { get; set; }
		public Int32 PageCnt { get; set; }
    }
}