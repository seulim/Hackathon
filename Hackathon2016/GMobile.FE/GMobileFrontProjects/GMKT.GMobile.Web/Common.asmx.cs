using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using GMobile.Data.DisplayDB;
using GMobile.Service.Home;

namespace GMKT.GMobile.Web
{
	/// <summary>
	/// Common의 요약 설명입니다.
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
	// [System.Web.Script.Services.ScriptService]
	public class Common : System.Web.Services.WebService
	{
		[WebMethod(CacheDuration = 600)]
		public List<MobileHomeServiceT> GetMobileHomeService()
		{
			return new MobileHomeServiceBiz().GetMobileHomeService();
		}

		[WebMethod(CacheDuration = 600)]
		public List<MobileNoticeT> GetMobileNotice(int pageNo, int iRowCount)
		{
			return new MobileNoticeBiz().GetMobileNotice(pageNo, iRowCount);	
		}
	}
}
