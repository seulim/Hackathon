using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using Newtonsoft.Json;
using GMKT.GMobile.Util;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Web.Controllers
{
	public class SpaceController : GMobileControllerBase
    {
        //
        // GET: /Space/

		public ActionResult Index(Nullable<int> lCd, Nullable<int> mCd)
        {
			ViewBag.Title = "공간 - G마켓 모바일";
			SetHomeTabName("공간");

			PageAttr.IsMain = true;
			PageAttr.HeaderType = HeaderTypeEnum.Normal;

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 공간";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Space";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 공간";
			#endregion

			SpaceInfo model = new SpaceApiBiz_Cache().GetSpaceInfo();
			if (model == null)
			{
				model = new SpaceApiBiz().GetSpaceInfo();
			}

			SpaceModel spaceModel = new SpaceModel();
			spaceModel.ContentsSection = model.ContentsSection;
			spaceModel.SpaceSection = model.SpaceSection;

			/* Landing Banner */
			new LandingBannerSetter(Request).Set(spaceModel, PageAttr.IsApp);

			//대,중분류로 바로 가기 위한 코드
			ViewData["lCd"] = (lCd == null) ? 0 : lCd;
			ViewData["mCd"] = (mCd == null) ? 0 : mCd;

			return View(spaceModel);
        }

		public ActionResult ContentsAll(Nullable<long> id)
		{
			ViewBag.HeaderTitle = "공간 컨텐츠 전체";
			ViewBag.Title = "공간 - G마켓 모바일";
			List<SpaceContents> model = new SpaceApiBiz_Cache().GetSpaceContentsList();
			if (model == null)
			{
				model = new SpaceApiBiz().GetSpaceContentsList();
			}

			bool contentExist = false;
			if (id != null)
			{
				foreach (SpaceContents contents in model)
				{
					if (contents.ContentsSeq == id)
					{
						ViewBag.selectedContentId = id;
						contentExist = true;
						break;
					}
				}
			}
			else
			{
				contentExist = true;
			}

			if (!contentExist)
			{
				string href = Urls.MobileWebUrl + "/Space/ContentsAll";
				return AlertMessageAndLocationChange("컨텐츠 전시기간이 종료되었습니다.", href);
			}

			return View(model);
		}

		public ActionResult ContentsDetail(long id)
		{
			ViewBag.HeaderTitle = "공간 컨텐츠";
			ViewBag.Title = "공간 - G마켓 모바일";
			SpaceContentsDetail model = new SpaceApiBiz_Cache().GetSpaceContentsDetail(id);
			if (model == null)
			{
				model = new SpaceApiBiz().GetSpaceContentsDetail(id);
			}

			if (model.Html == "1301")//api 직접 호출 후에도 없으면~
			{
				string href = Urls.MobileWebUrl + "/Space/ContentsAll";
				return AlertMessageAndLocationChange("컨텐츠 전시기간이 종료되었습니다.", href);
			}

			return View(model);
		}

		public ActionResult BrandDetail(long id)
		{
			ViewBag.Title = "공간 - G마켓 모바일";

			if (id < 1)
			{
				string href = Urls.MobileWebUrl + "/Space";
				return AlertMessageAndLocationChange("해당 컨텐츠가 없습니다.", href);
			}
			SpaceBrandGroupDetail model = new SpaceApiBiz_Cache().GetSpaceBrandDetail(id);
			ViewBag.HeaderTitle = model.BrandName;

			if (model == null)
			{
				model = new SpaceApiBiz().GetSpaceBrandDetail(id);
			}

			if (model.Html == "1306")//api 직접 호출 후에도 없으면~
			{
				string href = Urls.MobileWebUrl + "/Space";
				return AlertMessageAndLocationChange("컨텐츠 전시기간이 종료되었습니다.", href);
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult GetSpaceSectionItem(long lgroupNo = 1, long mgroupNo = 0, int pageNo = 1, int pageSize = 80)
		{
			SpaceSectionItem model = new SpaceApiBiz_Cache().GetSpaceSectionItem(lgroupNo, mgroupNo, pageNo, pageSize);
			if (model == null)
			{
				model = new SpaceApiBiz().GetSpaceSectionItem(lgroupNo, mgroupNo, pageNo, pageSize);
			}

			return Content(JsonConvert.SerializeObject(model), "application/json");
		}

		[HttpPost]
		public ActionResult GetSpaceBrandSectionItem(long lgroupNo = 1, long mgroupNo = 0)
		{
			SpaceBrandSectionItem model = new SpaceApiBiz_Cache().GetSpaceBrandSectionItem(lgroupNo, mgroupNo);
			if (model == null)
			{
				model = new SpaceApiBiz().GetSpaceBrandSectionItem(lgroupNo, mgroupNo);
			}

			return Content(JsonConvert.SerializeObject(model), "application/json");
		}
    }
}
