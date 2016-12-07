using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.Web.Membership;

namespace GMKT.GMobile.Web.Controllers
{
    public class CustomerCenterController : GMobileControllerBase
    {
		private const int _MaxPageSize = 10;
		private const string _DefaultCd = "302";

		public ActionResult Index(string catecd ="")
        {
			ViewBag.HeaderTitle = "모바일 고객센터";
			ViewBag.Title = "모바일 고객센터 - G마켓 모바일";
			ViewBag.CCName = "G마켓";
			ViewBag.CCCallText = "모바일 고객센터 전화하기";
			ViewBag.CCNumber = "1644-5715";

			if(PageAttr.Exid != null && PageAttr.Exid == ExIdAppEnum.SFC)
			{
				ViewBag.Title = "모바일 고객센터 - SFC 모바일";
				ViewBag.CCName = "SFC";
				ViewBag.CCCallText = "SFC 고객센터 전화하기";
				ViewBag.CCNumber = "1644-5783";
			}

			return base.View(this.GetFaq(5, catecd));
        }

		public ActionResult FaqList(string catecd = _DefaultCd)
		{
			ViewBag.HeaderTitle = "모바일 고객센터";
            ViewBag.Title = "FAQ - G마켓 모바일";
			return base.View(GetFaq(_MaxPageSize, catecd));
		}

		public ActionResult FaqSearch(string search)
		{
			ViewBag.HeaderTitle = "모바일 고객센터";
            ViewBag.Title = "FAQ 검색 - G마켓 모바일";
			return base.View(GetFaq(_MaxPageSize, string.Empty, search));
		}

		[HttpPost]
		public ActionResult GetFaqList(string cateCd, string search, int pageIndex, int type = 0)
		{
			return base.Json(GetFaq(type != 1 ? _MaxPageSize : 5, cateCd, search, pageIndex));
		}		

		private FaqModel GetFaq(int maxPageSize = 5, string catecd = "", string search = "" , int pageIndex = 1)
		{
			FaqModel model = new FaqModel();

			model.FaqCategoryList = new CustomerCenterBiz().GetFaqCategoryListFromDB("L", catecd);
			model.DefaultCd = catecd;
			model.NextPageNo = 0;
			model.PageNo = pageIndex;
			model.TotalCnt = 0;
			model.PageCnt = 0;

			if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(catecd) && maxPageSize == 5)
			{
				model.Items = new CustomerCenterBiz().GetBestFaqListFromDB();
				model.DefaultCd = "";
				return model;
			}

			model.Items = new CustomerCenterBiz().GetFaqListFromDB(search, catecd, pageIndex, maxPageSize);

			if (model.Items == null)
			{
				model.Items = new List<Data.FaqListT>();
				return model;
			}

			if (!string.IsNullOrEmpty(search))
			{
				model.Items.ForEach(row => {
					row.title = System.Web.HttpUtility.HtmlEncode(row.title);
					row.faqLclassNm = this.GetClassNameByCD(model.FaqCategoryList, row.faqLclassCd);
				}
				);
			}

			if (model.Items.Count > 0)
			{
				model.TotalCnt = model.Items.FirstOrDefault().totalCnt;
				model.PageCnt = model.Items.FirstOrDefault().pageCnt;
			}
			
			model.NextPageNo = model.Items.Count < maxPageSize ? 0 : pageIndex + 1;			

			return model;
		}

		private string GetClassNameByCD(List<FaqCategoryT> faqCategoryList, string faqLclassCd)
		{
			var _Query = from rows in faqCategoryList
						 where rows.cd == faqLclassCd
						 select rows;

			if (_Query.Count() == 0)
			{
				return string.Empty;
			}
			
			return _Query.FirstOrDefault().cdNm;
		}

		public ActionResult FaqDetail(Int32 seq = 0)
		{
			ViewBag.HeaderTitle = "모바일 고객센터";
            ViewBag.Title = "FAQ - G마켓 모바일";
			FaqDetailModel model = new FaqDetailModel();

			if (seq == 0)
			{
				return base.View(model);
			}			

			model.FaqDetail = new CustomerCenterBiz().GetFaqDetailFromDB(seq);
			model.FaqRelList = new CustomerCenterBiz().GetRelFaqDetailFromDB(seq);

			// 조회수 증가
			new CustomerCenterBiz().SetFaqDetailReadCntFromDB(seq);

			return base.View(model);
		}

		public void SetFaqReadCount(Int32 seq = 0)
		{
			// 조회수 증가
			new CustomerCenterBiz().SetFaqDetailReadCntFromDB(seq);
		}		
	
		public ActionResult AskQuestion()
		{
			return View();
		}
    }
}
