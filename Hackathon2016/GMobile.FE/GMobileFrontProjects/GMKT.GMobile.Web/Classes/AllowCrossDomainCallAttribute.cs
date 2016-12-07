using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMKT.GMobile.Web
{
	public class AllowCrossDomainCallAttribute : ActionFilterAttribute
	{
		public string AllowedDomain { get; set; }

		public AllowCrossDomainCallAttribute()
			: this("*")
		{
		}

		public AllowCrossDomainCallAttribute(string allowedDomain)
		{
			this.AllowedDomain = allowedDomain;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.HttpContext.Response.AddHeader(
				"Access-Control-Allow-Origin", 
				this.AllowedDomain);
			base.OnActionExecuting(filterContext);
		}
	}
}