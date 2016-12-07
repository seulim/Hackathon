using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMobile.Data.Diver;
using GMobile.Data.DisplayDB;
using GMobile.Service.Search;
using GMobile.Service.Home;
using GMobile.Service.Display;
using GMKT.GMobile.Util;
using GMKT.GMobile.Web.Models;
using System.IO;

using GMobile.Data.Voyager;
using GMKT.Framework.Constant;
using GMKT.Framework.Security;
using GMKT.GMobile.Biz.EventV2;

namespace GMKT.GMobile.Web.Controllers
{
	public class DisplayController : EventControllerBase
	{
		/*
		public ActionResult Index()
		{
			return View();
		}
		*/

		[OutputCache(Duration = 600)]
		public ActionResult BestSellerList(string pageKind = "A", string groupCode = "", string ecp_gdlc = "", string ecp_gdmc = "", string ecp_gdsc= "", string listView = "H")
		{
            ViewBag.Title = "G마켓 베스트 - G마켓 모바일";
			DisplayModel model = new DisplayModel
			{
				pageKind = pageKind,
				groupCode = groupCode,
				ecp_gdlc= ecp_gdlc,
				ecp_gdmc= ecp_gdmc,
				ecp_gdsc = ecp_gdsc,
				listView = listView
			};

			if (pageKind.Equals("A"))
			{
				model.Categorys = CategoryInfo(ecp_gdlc, ecp_gdmc, ecp_gdsc);
                 #region
                //정렬
                SortedList sorter2 = new SortedList(model.Categorys);
                
                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                };
               
                foreach (DictionaryEntry data in sorter2)
                {
                    if (!data.Key.ToString().Equals("100000051") && !data.Key.ToString().Equals("100000087") && !data.Key.ToString().Equals("100000088")
                        && !data.Key.ToString().Equals("100000081") && !data.Key.ToString().Equals("100000052") && !data.Key.ToString().Equals("100000059"))
                    {
                        dic.Add(data.Key.ToString(), data.Value.ToString());
                    }        
                    //  model.sortedCategoryGroups.Add(data.Key.ToString(), data.Value.ToString());
                }
                //정렬

                //글자순으로 정렬
                List<KeyValuePair<string, string>> tempList = new List<KeyValuePair<string, string>>(dic);

                tempList.Sort(delegate(KeyValuePair<string, string> firstPair, KeyValuePair<string, string> secondPair)
                {
                    return firstPair.Value.CompareTo(secondPair.Value);
                }
                             );

                Dictionary<string, string> mySortedDictionary = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> pair in tempList)
                {
                    mySortedDictionary.Add(pair.Key, pair.Value);
                }
                //글자순으로 정렬

                Dictionary<string, string> lastDictionary = new Dictionary<string, string>();
                
                // 기존출력
                //int intCout = mySortedDictionary.Count;
                //int intTotalCount = mySortedDictionary.Count;
                //int intRealTotalCount = mySortedDictionary.Count;
                //intCout = intCout / 2;
                //intTotalCount = intCout + 1;
                //intRealTotalCount = intRealTotalCount / 2;
                //for (int i = 0; i <= intCout; i++)
                //{
                //    lastDictionary.Add(mySortedDictionary.Keys.ElementAt(i), mySortedDictionary.Values.ElementAt(i));
                //    if (intRealTotalCount - 1 > i)
                //    {
                //        lastDictionary.Add(mySortedDictionary.Keys.ElementAt(i + intTotalCount), mySortedDictionary.Values.ElementAt(i + intTotalCount));
                //    }
                //}
                #endregion

                // 변경출력 : 카테고리 출력 오류로 인해 변경 20131204
                if (mySortedDictionary.Count % 2 == 0)
                {
                    for (int i = 0; i < mySortedDictionary.Count / 2; i++)
                    {
                        lastDictionary.Add(mySortedDictionary.Keys.ElementAt(i), mySortedDictionary.Values.ElementAt(i));
                        lastDictionary.Add(mySortedDictionary.Keys.ElementAt(i + mySortedDictionary.Count / 2),
                            mySortedDictionary.Values.ElementAt(i + mySortedDictionary.Count / 2));
                    }
                }
                else
                {
                    for (int i = 0; i <= mySortedDictionary.Count / 2; i++)
                    {
                        lastDictionary.Add(mySortedDictionary.Keys.ElementAt(i), mySortedDictionary.Values.ElementAt(i));

                        if (i != (mySortedDictionary.Count / 2))
                        {
                            lastDictionary.Add(mySortedDictionary.Keys.ElementAt(i + (mySortedDictionary.Count / 2) + 1),
                                mySortedDictionary.Values.ElementAt(i + (mySortedDictionary.Count / 2) + 1));
                        }
                    }
                }                

                model.dicData = lastDictionary;


				List<SearchGoods> result = BestSellerBiz.GetBestSellerGoodsList("1", "100");

				model.Items = result;
			}
			else
			{
				//Response.Cache.SetNoServerCaching();
				//Response.Cache.SetNoStore();
				if (pageKind.Equals("G"))
				{
					// 대카테고리 코드를 gdlcCd에 컴마로 붙여서 여러개 넘김
					// 카테고리 그룹명
					string[] groupCategoryName;
					groupCategoryName = new string[] { "패션의류/잡화", "화장품/헤어", "식품/유아동", "가구/생활/건강", "컴퓨터/가전", "스포츠/자동차", "도서/티켓", "e쿠폰/여행" };

					// 카테고리 그룹
					string[] groupCategory;
					groupCategory = new string[] { "100000003,100000046,100000063,100000070,100000049,100000027",
								"100000005,100000071",
								"100000057,100000073,100000006,100000035,100000042,100000020,100000036,100000068",
								"100000031,100000039,100000014,100000085,100000074,100000076,100000083,100000045,100000038,100000041,100000084",
								"100000002,100000055,100000082,100000075,100000056,100000033,100000072,100000078,100000032,100000008,100000077",
								"100000058,100000017,100000037,100000043,100000030,100000079",
								"100000028,100000059",
								"100000048,100000052,100000013" };

					SearchTGoods result = new SearchListBiz().GetSearchBestSellerList(groupCategory[int.Parse(groupCode)], "", "", 1, 100);

					model.Items = result.goods;

					model.ListName = groupCategoryName[int.Parse(groupCode)];
				}
				else
				{
                    model.dicData = CategoryInfo02(ecp_gdlc, ecp_gdmc, ecp_gdsc);

					SearchTGoods result = new SearchListBiz().GetSearchBestSellerList(ecp_gdlc, ecp_gdmc, ecp_gdsc, 1, 100);

					model.Items = result.goods;

					string listName = "";
					if (ecp_gdsc != "") listName = CategoryUtil.GetCategoryName(ecp_gdsc);
					else if (ecp_gdmc != "") listName = CategoryUtil.GetCategoryName(ecp_gdmc);
					else listName = CategoryUtil.GetCategoryName(ecp_gdlc);
					model.ListName = listName;
				}
			}

			return View(model);
		}

		[OutputCache(Duration = 600)]
		public ActionResult LazyloadBestSellerList(string pageKind = "A", string groupCode = "", string ecp_gdlc = "", string ecp_gdmc = "", string ecp_gdsc = "", string listView = "H")
		{
            ViewBag.Title = "BestSellerList";
			DisplayModel model = new DisplayModel
			{
				pageKind = pageKind,
				groupCode = groupCode,
				ecp_gdlc = ecp_gdlc,
				ecp_gdmc = ecp_gdmc,
				ecp_gdsc = ecp_gdsc,
				listView = listView
			};

			if (pageKind.Equals("A"))
			{
				model.Categorys = CategoryInfo(ecp_gdlc, ecp_gdmc, ecp_gdsc);

				List<SearchGoods> result = BestSellerBiz.GetBestSellerGoodsList("1", "100");

				model.Items = result;
			}
			else
			{
				//Response.Cache.SetNoServerCaching();
				//Response.Cache.SetNoStore();
				if (pageKind.Equals("G"))
				{
					// 대카테고리 코드를 gdlcCd에 컴마로 붙여서 여러개 넘김
					// 카테고리 그룹명
					string[] groupCategoryName;
					groupCategoryName = new string[] { "패션의류/잡화", "화장품/헤어", "식품/유아동", "가구/생활/건강", "컴퓨터/가전", "스포츠/자동차", "도서/티켓", "e쿠폰/여행" };

					// 카테고리 그룹
					string[] groupCategory;
					groupCategory = new string[] { "100000003,100000046,100000063,100000070,100000049,100000027",
								"100000005,100000071",
								"100000057,100000073,100000006,100000035,100000042,100000020,100000036,100000068",
								"100000031,100000039,100000014,100000085,100000074,100000076,100000083,100000045,100000038,100000041,100000084",
								"100000002,100000055,100000082,100000075,100000056,100000033,100000072,100000078,100000032,100000008,100000077",
								"100000058,100000017,100000037,100000043,100000030,100000079",
								"100000028,100000059",
								"100000048,100000052,100000013" };

					SearchTGoods result = new SearchListBiz().GetSearchBestSellerList(groupCategory[int.Parse(groupCode)], "", "", 1, 100);

					model.Items = result.goods;

					model.ListName = groupCategoryName[int.Parse(groupCode)];
				}
				else
				{
					model.Categorys = CategoryInfo(ecp_gdlc, ecp_gdmc, ecp_gdsc);

					SearchTGoods result = new SearchListBiz().GetSearchBestSellerList(ecp_gdlc, ecp_gdmc, ecp_gdsc, 1, 100);

					model.Items = result.goods;

					string listName = "";
					if (ecp_gdsc != "") listName = CategoryUtil.GetCategoryName(ecp_gdsc);
					else if (ecp_gdmc != "") listName = CategoryUtil.GetCategoryName(ecp_gdmc);
					else listName = CategoryUtil.GetCategoryName(ecp_gdlc);
					model.ListName = listName;
				}
			}

			return View(model);
		}

		[HttpPost]
		[ActionName("SpecialShopping")]
		public ActionResult SpecialShopping_Post(string lcId, string sid, int pageNo = 1)
		{
			if (String.IsNullOrEmpty(lcId))
			{
				lcId = "";
			}

			DisplayModel model = new DisplayModel { 
				lcId = lcId,
				sid = sid,
				pageNo = pageNo
			};


			if (String.IsNullOrEmpty(sid))
			{
				return Json("");
			}

			SearchTGoods result = new SearchListBiz().GetSearchSpecialGoodsList(sid, "", pageNo, 10);			

			model.Items = result.goods;

			model.totalCount = result.totCnt[0].totalcount;

			return Json(new { hasRsMsg = "False", rsCd = "SUCCESS", rsMsg = "", list = RenderPartialViewToString("SpecialShopping_Post", model, this.ControllerContext) }, "application/json", JsonRequestBehavior.AllowGet);
		}

		[OutputCache(Duration = 300)]
		public ActionResult SpecialShopping(string lcId, string sid, int pageNo = 1)
		{
            ViewBag.HeaderTitle = "기획전";
            PageAttr.HeaderType = CommonData.HeaderTypeEnum.Simple;

            int convSid = 0;

            //sid validate
            try
            {
                convSid = Convert.ToInt32(sid);
            }
            catch (Exception)
            {
                convSid = -1;
            }

						if (String.IsNullOrEmpty(sid) || convSid <= 0)
						{
							return Redirect("http://mobile.gmarket.co.kr/Display/SpecialShoppingList");
						}

            if (String.IsNullOrEmpty(lcId))
            {
                lcId = "";
            }

            DisplayModel model = new DisplayModel
            {
                lcId = lcId,
                sid = sid,
                pageNo = pageNo,
								groupInfos = new MobileShopEventGroupBiz().GetShopEventGroupInfo(convSid),
								listView = ""
            };

            //SpecialShoppingList planResult = new SearchListBiz().GetSearchSpecialShopList(pageNo, 10, lcId, sid);
            MobileShopPlanT planResult = new MobilePlanBiz().GetMobilePlanDetail(convSid);
						if(planResult == null)
						{
						//	return Redirect(Urls.MobileWebUrl + "/Pluszone");
						}
						int maxCnt = model.groupInfos.Sum(a => a.GoodsMaxDisplayCount);
						//SearchTGoods result = new SearchListBiz().GetSearchSpecialGoodsList(sid, "", pageNo, maxCnt);
                        SearchTGoods result = new SearchTGoods() {goods = new List<SearchGoods>(),cateogory = new List<CategoryGroup>(), totCnt = new List<TotCount>(), QueryString = "" };
						List<ShopMobileEventGroupGoods> dbGoods = new List<ShopMobileEventGroupGoods>();
						foreach (var g in model.groupInfos)
						{
							if (g.MobileExposeYn != "N" && g.GroupKind == "1")
							{
                                SearchTGoods gidResult = new SearchListBiz().GetSearchSpecialGoodsList(sid, g.GID.ToString(), pageNo, g.GoodsMaxDisplayCount);
                                result.goods.AddRange(gidResult.goods);
								var goods = new MobileShopEventGroupBiz().GetShopEventGoodsList(convSid, g.GID).Where(a => (a.MobileExposeYn == null || a.MobileExposeYn.Equals("Y")));
                                goods = goods.OrderBy(x => x.Priority).Take(g.GoodsMaxDisplayCount);
								dbGoods.AddRange(goods);

                                if (dbGoods.Count > 0)
                                {
                                    foreach (var gds in dbGoods)
                                    {
                                        if (registProductYN(gds.GdmcCd, gds.GdscCd))
                                        {
                                            gds.RegisterProductYN = "Y";
                                        }
                                        else
                                        {
                                            gds.RegisterProductYN = "N";
                                        }
                                    }
                                }
							}
                            else if((g.GroupKind == "2" || g.GroupKind == "3") && g.MobileExposeYn != "N")
                            {
								SearchTGoods gidResult = new SearchListBiz().GetSearchSpecialGoodsList(sid, g.GID.ToString(), pageNo, g.GoodsMaxDisplayCount);
								result.goods.AddRange(gidResult.goods);

								var goods = gidResult.goods.Where(a => a.group_no == g.GID.ToString());

								if (Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("mobiledev") > -1){
									//goods = result.goods.Where(a => a.group_no == "556897");
								}
								foreach(var g2 in goods)
								{
									ShopMobileEventGroupGoods gd = new ShopMobileEventGroupGoods(){
										Gid = int.Parse(g2.group_no),
										GdNo = g2.gd_no,
										GdNm = g2.gd_nm,
										GroupNm = g.GroupNm,
										delivery_info = g2.delivery_info,
										adult_yn = g2.adult_yn,
										discount_price = (decimal)g2.discount_price,
										sell_price = (decimal)g2.sell_price
									};

									dbGoods.Add(gd);
								}
							}						
						}

                        if (planResult != null)
                        {
                            var date = DateTime.Now.ToString("G");

                            if (planResult.EndDate < Convert.ToDateTime(date))
                            {
                                return Content("<script language='javascript' type='text/javascript'> alert('종료된 기획전 입니다.'); location.replace('http://mobile.gmarket.co.kr'); </script>");
                            }

                            model.Plan = planResult;
                        } 
                        else
                        {
                            return Redirect(Urls.MobileWebUrl);
                            //return Redirect(Urls.MobileWebUrl + "/Pluszone");
                            //model.Plan = new MobileShopPlanT();
                        }


						if (dbGoods.Count > 0)
						{
							
                            //if (Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("mobiledev") > -1)
                            //{
                            //    // dev 테스트 QA때만
                            //    if(result.goods.Count > 0){
                            //        foreach(var g in dbGoods){
                            //            var sg = result.goods.Where(a => a.gd_no == g.GdNo).FirstOrDefault();
                            //            if(sg != null){
                            //                g.adult_yn = sg.adult_yn;
                            //                g.delivery_info = sg.delivery_info;
                            //                g.discount_price = sg.discount_price;
                            //                g.sell_price = sg.sell_price;
                            //            }else{
                            //                g.adult_yn = "N";
                            //                g.delivery_info = "착불선결제";
                            //                g.discount_price = 800;
                            //                g.sell_price = 800;
                            //            }
                            //        }
                            //    }else{
                            //        foreach (var g in dbGoods)
                            //        {
                            //            g.adult_yn = "N";
                            //            g.delivery_info = "무료";
                            //            //g.discount_price = 800;
                            //            //g.sell_price = 800;
                            //        }
                            //    }
                            //}else{
								if (result.goods.Count > 0)
								{
									foreach (var g in dbGoods)
									{
										var sg = result.goods.Where(a => a.gd_no == g.GdNo).FirstOrDefault();
										if (sg != null)
										{
											g.adult_yn = sg.adult_yn;
											g.delivery_info = sg.delivery_info;
											g.discount_price = sg.discount_price;
											g.sell_price = sg.sell_price;
										}else{
											g.sell_price = 0;
										}										
									}
								}
							//}

							var filterdGoods = result.goods.Join(dbGoods, a => a.gd_no, b => b.GdNo, (a, b) => a);

							/*foreach (var fdg in filterdGoods)
							{
								if (dbGoods.Where(a => a.GdNo == fdg.gd_no).Count() > 0)
									fdg.group_no = dbGoods.Where(a => a.GdNo == fdg.gd_no).ToList()[0].Gid.ToString();
							}*/
							model.Items = filterdGoods.ToList();
							
							model.totalCount = filterdGoods.Count();
						}

            ViewBag.Title = model.Plan.Title + " - G마켓 모바일";

            /* Landing Banner */
            new LandingBannerSetter(Request).Set(model, PageAttr.IsApp);
						dbGoods = dbGoods.Where(a => a.sell_price > 0).ToList();
						model.GoodsItems = dbGoods;
						model.totalCount = dbGoods.Count;
						
            //return View(model);
						return View("SpecialShopping2", model);
		}

		[HttpPost]
		[ActionName("SpecialShoppingList")]
		public ActionResult SpecialShoppingList_Post(string groupCd, int pageNo = 1)
		{
			List<MobilePlanT> planResult = new MobilePlanBiz().GetMobilePlanList(groupCd, pageNo, 10);

			DisplayModel model = new DisplayModel
			{
				groupCd = groupCd,
				pageNo = pageNo,
				totalCount = (int)planResult[0].TotalCounts,
				planList = planResult
			};

			return Json(new { hasRsMsg = "False", rsCd = "SUCCESS", rsMsg = "", list = RenderPartialViewToString("SpecialShoppingList_Post", model, this.ControllerContext) }, "application/json", JsonRequestBehavior.AllowGet);
		}

		public ActionResult SpecialShoppingList(string groupCd = "", int pageNo = 1)
		{
            ViewBag.Title = "모바일 기획전 - G마켓 모바일";
            ViewBag.HeaderTitle = "기획전";
            PageAttr.HeaderType = CommonData.HeaderTypeEnum.Simple;
			//PageAttr.MainCss = "main.css?v=9";
			//SpecialShoppingList specialShoppingResult = new SearchListBiz().GetSearchSpecialShopList(pageNo, 10, groupCd, "");
			List<MobilePlanT> planResult = new MobilePlanBiz().GetMobilePlanList(groupCd, pageNo, 10);

			DisplayModel model = new DisplayModel
			{
				groupCd = groupCd,
				pageNo = pageNo,
				ListName = BizUtil.getPlanGroupName(groupCd)
			};

			if (planResult != null && planResult.Count > 0)
			{
				model.planList = planResult;
				model.totalCount = (int)planResult[0].TotalCounts;
			}
			else
			{
				model.planList = new List<MobilePlanT>();
				model.totalCount = 0;
			}

            /* Landing Banner */
            new LandingBannerSetter(Request).Set(model, PageAttr.IsApp);

            #region 페이스북 공유하기
            string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
            PageAttr.FbTitle = "G마켓 인기기획전";
            PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Display/SpecialShoppingList?groupCd=" + groupCd;
            PageAttr.FbTagImage = faceBookImage;
            PageAttr.FbTagDescription = "G마켓 인기기획전";
            #endregion

			return View(model);
		}

		public ActionResult Today(int id = 0)
		{
            ViewBag.Title = "오늘만 특가";
			TodayModel model = new TodayModel 
			{
				TodaySpecial = new TodaySpecialBiz().GetTodaySpecialPrice(12),
				GoodSeries = new TodaySpecialBiz().GetMainPageBanner(8),

				// 2013-05-09 이윤호
				// 링크 만을 이용해 굿시리즈 탭으로 이동할 수 있도록 수정
				CurrentTabNo = id
			};

			return View(model);
		}

		[NonAction]
		public Hashtable CategoryInfo(string gdlcCd, string gdmcCd, string gdscCd)
		{
			ArrayList categoryList = new ArrayList();
			Hashtable categoryTable = new Hashtable();
			string code = "", name = "";

			if (gdlcCd == "" && gdmcCd == "" && gdscCd == "")
			{
				categoryList = CategoryUtil.GetXMLCategoryList("T", "");
			}
			else if (gdlcCd != "" && gdmcCd == "" && gdscCd == "")
			{
				categoryList = CategoryUtil.GetXMLCategoryList("L", gdlcCd);
			}
			else
			{
				categoryList = CategoryUtil.GetXMLCategoryList("M", gdmcCd);
			}

			for (int i = 0; i < categoryList.Count; i++)
			{
				if (i % 2 == 0)
				{
					code = categoryList[i].ToString();
				}
				else
				{
					name = categoryList[i].ToString();
					categoryTable.Add(code, name);
				}
			}

			//System.Web.HttpContext.Current.Response.Write(categoryTable.Count);

            return categoryTable;
          
		}


        [NonAction]
        public Dictionary<string, string> CategoryInfo02(string gdlcCd, string gdmcCd, string gdscCd)
        {
            ArrayList categoryList = new ArrayList();
            Hashtable categoryTable = new Hashtable();
            string code = "", name = "";

            if (gdlcCd == "" && gdmcCd == "" && gdscCd == "")
            {
                categoryList = CategoryUtil.GetXMLCategoryList("T", "");
            }
            else if (gdlcCd != "" && gdmcCd == "" && gdscCd == "")
            {
                categoryList = CategoryUtil.GetXMLCategoryList("L", gdlcCd);
            }
            else
            {
                categoryList = CategoryUtil.GetXMLCategoryList("M", gdmcCd);
            }

            for (int i = 0; i < categoryList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    code = categoryList[i].ToString();
                }
                else
                {
                    name = categoryList[i].ToString();
                    categoryTable.Add(code, name);
                }
            }

            #region 정렬
            //정렬
            SortedList sorter2 = new SortedList(categoryTable);

            Dictionary<string, string> dic = new Dictionary<string, string>
            {
            };

            foreach (DictionaryEntry data in sorter2)
            {
                dic.Add(data.Key.ToString(), data.Value.ToString());
                //  model.sortedCategoryGroups.Add(data.Key.ToString(), data.Value.ToString());
            }
            //정렬

            //글자순으로 정렬
            List<KeyValuePair<string, string>> tempList = new List<KeyValuePair<string, string>>(dic);

            tempList.Sort(delegate(KeyValuePair<string, string> firstPair, KeyValuePair<string, string> secondPair)
            {
                return firstPair.Value.CompareTo(secondPair.Value);
            }
                         );

            Dictionary<string, string> mySortedDictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in tempList)
            {
                mySortedDictionary.Add(pair.Key, pair.Value);
            }
            //글자순으로 정렬

      
            #endregion


            //System.Web.HttpContext.Current.Response.Write(categoryTable.Count);

            return mySortedDictionary;
            //return categoryTable;
        }



		public string RenderPartialViewToString(string viewPath, object model, ControllerContext context)
		{
			var viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);
			var view = viewEngineResult.View;

			context.Controller.ViewData.Model = model;

			string result = String.Empty;
			using (var sw = new StringWriter())
			{
				var ctx = new ViewContext(context, view, 
								context.Controller.ViewData, 
								context.Controller.TempData, 
								sw);
				view.Render(ctx, sw);
				result = sw.ToString();
			}

			return result;
		}

		[OutputCache(Duration=300)]
		public ActionResult EventShoppingGroupList(string groupNo, string sgno = null, string bannerImageUrl = "")
		{

			EventShoppingGroupModel eventGroupGoodsModel = new MobileGroupEntity().GetEventGroupGoodsInfo(groupNo, null);

			if(eventGroupGoodsModel.LargeGroup == null){
				return new EmptyResult();
			}

			if (eventGroupGoodsModel.SmallGroups.Count < 1)
			{
				return new EmptyResult();
			}

			ViewBag.SmallGroup = sgno;
			
			string szItemNo = "";
			foreach (var model in eventGroupGoodsModel.GoodsList) { 
				if(model.GdNo.Length > 1)
					szItemNo += model.GdNo + ",";

				if (!string.IsNullOrEmpty(model.CouponEid) && model.CouponEid.Length > 1)
				{
					string[] result = new string[2];

					string cryptData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + CRYPT_MAIN_DELIMITER +
								int.Parse(model.CouponEid) + CRYPT_MAIN_DELIMITER +
								"" + CRYPT_MAIN_DELIMITER +
								DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + CRYPT_MAIN_DELIMITER;

					result[0] = GMKTCryptoLibrary.AesGCryptoEncrypt(cryptData);
					result[1] = GMKTCryptoLibraryOption.MD5(result[0] + CRYPT_MD5_FOOTER);

					//string[] enc = EncryptForEventPlatform(int.Parse(model.CouponEid));
					if (result.Length > 1)
						model.EidEncrytStr = string.Format("'{0}','{1}','N'", result[0], result[1]);
				}
			}

			List<SearchGoodsModel> apiSearchModel = new List<SearchGoodsModel>();
			try
			{
				if (!string.IsNullOrEmpty(szItemNo) & szItemNo.Length > 1)
				{ 
					System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://searchapi.gmarket.co.kr/search/items/getitemsinfo/" + szItemNo);
					webRequest.Method = "get";
				
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webRequest.GetResponse();

					Stream stReadData = response.GetResponseStream();
					StreamReader srReadData = new StreamReader(stReadData, System.Text.Encoding.UTF8);

					string strResult = srReadData.ReadToEnd();

					srReadData.Close();
					stReadData.Close();


					apiSearchModel = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<SearchGoodsModel>>(strResult);

					foreach (var model in eventGroupGoodsModel.GoodsList)
					{
						if (model.GdNo.Length > 0) { 
							var gd = apiSearchModel.Where(a => a.GoodsCode == model.GdNo);
							if (gd != null && gd.Count() > 0)
							{
								model.SearchGoodsModel = gd.ToList()[0];
							}
						}
					}

					if(apiSearchModel != null && apiSearchModel.Count > 0)
						apiSearchModel = apiSearchModel.Where(a => a.Price != "").ToList();
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}			

			eventGroupGoodsModel.GoodsList = eventGroupGoodsModel.GoodsList.Where(a => a.SearchGoodsModel != null || a.CouponEid.Length > 1).ToList();
			string szJson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(eventGroupGoodsModel);
			ViewBag.ScriptData = szJson;

			if(eventGroupGoodsModel != null && eventGroupGoodsModel.LargeGroup != null)
				ViewBag.Title = eventGroupGoodsModel.LargeGroup.GroupName + " - G마켓 모바일";
			
			if(false == string.IsNullOrEmpty(bannerImageUrl)){
				ViewBag.BannerImgUrl = bannerImageUrl;
			}

			return View(eventGroupGoodsModel);
		}

		public override RedirectResult CommonApplyEventPlatformGmarketEx(string str, string encStr, string groupYn, string returlUrl)
		{
			if (groupYn == "")
				groupYn = "N";

			// 쿠키 설정
			HttpCookie cookie = new HttpCookie("ECif");
			cookie.Value = encStr;
			cookie.Path = "/";
			cookie.Domain = "gmarket.co.kr";

			// 쿠키 추가
			Response.Cookies.Add(cookie);

			string href = COMMON_APPLY_EVENT_PLATFORM_GMARKET_URL +
				"?epif=" + str +
				"&openerURL=" + Server.UrlEncode(returlUrl) +
				"&groupYn=" + groupYn +
				"&isMobile=Y";

			return Redirect(href);
		}

        //가입상품여부 : 중분류/소분류로 가입 상품 여부 판단
        private bool registProductYN(string gdmc_cd, string gdsc_cd)
        {
            if (gdmc_cd == "200000801" || gdmc_cd == "200001255" || gdmc_cd == "200001256" || gdmc_cd == "200000800" || gdmc_cd == "200001253" || gdmc_cd == "200001254"
                || gdmc_cd == "200000802" || gdmc_cd == "200001257" || gdmc_cd == "200001258" || gdmc_cd == "200002090" || gdmc_cd == "200001152" || gdmc_cd == "200002528")
            {
                if (gdmc_cd == "200002528" && !(gdsc_cd == "300025780" || gdsc_cd == "300025781" || gdsc_cd == "300025782" || gdsc_cd == "300025783" || gdsc_cd == "300025784"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
	}
}