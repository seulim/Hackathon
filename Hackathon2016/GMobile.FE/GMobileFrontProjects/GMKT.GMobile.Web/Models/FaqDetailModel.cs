using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
    public class FaqDetailModel
    {
		public FaqDetailT FaqDetail { get; set; }

		public List<FaqDetailT> FaqRelList { get; set; }	
    }
}