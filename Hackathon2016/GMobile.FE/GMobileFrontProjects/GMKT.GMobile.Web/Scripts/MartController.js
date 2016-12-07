function SnaUrlPost(fcd, url) {
	var snaUrl = "";
	if (fcd != undefined && fcd != null && fcd != "")
		snaUrl = 'http://sna.gmarket.co.kr/?fcd=' + fcd + '&url=' + encodeURIComponent(url);

	if (typeof url !== "undefined")
		location.href = snaUrl;
	else
		$.post(snaUrl);
}

(function (GMobile, $, Handlebars, undefined) {
	(function (Mart, undefined) {
		var homeCategory = '000000000';
		var iconCss = ['ico_mart', 'ico_processed', 'ico_pure', 'ico_life', 'ico_baby'];

		// ResgisterHelpers
		Handlebars.registerHelper('ifBannerType', typeHelper('banner'));
		Handlebars.registerHelper('ifItemType', typeHelper('item'));
		Handlebars.registerHelper('ifCategoryTitle', ifCategoryTitle);
		Handlebars.registerHelper('ifGreaterThan', ifGreaterThan);
		Handlebars.registerHelper('ifSelectedItem', ifSelectedItem);
		Handlebars.registerHelper('printMartItemGroupCss', printMartItemGroupCss);
		Handlebars.registerHelper('renderDeliveryTag', renderDeliveryTag);
		Handlebars.registerHelper('getItemUrl', getItemUrl);
		Handlebars.registerHelper('getCategoryUrl', getCategoryUrl);
		Handlebars.registerHelper('getCategoryIconCss', getCategoryIconCss);
		Handlebars.registerHelper('getSnaUrl', getSnaUrl);

		Handlebars.registerHelper("getBrandListStartTag", getBrandListStartTag);
		Handlebars.registerHelper("getBrandListEndTag", getBrandListEndTag);

		Handlebars.registerHelper("getCategoryListStartTag", getCategoryListStartTag);
		Handlebars.registerHelper("getCategoryListEndTag", getCategoryListEndTag);
		Handlebars.registerHelper("setCategoryIndex", setCategoryIndex);

		Handlebars.registerHelper("getContentsUrl", getContentsUrl);
		Handlebars.registerHelper("getContentsListStartTag", getContentsListStartTag);
		Handlebars.registerHelper("getContentsListEndTag", getContentsListEndTag);

		Handlebars.registerHelper('getVipUrl', getVipUrl);
		Handlebars.registerHelper("categorySelectedCss", categorySelectedCss);
		Handlebars.registerHelper("getContentsListClass", getContentsListClass);

		Handlebars.registerPartial('PartialMartBanner', $('#mart_banner_template').html());
		Handlebars.registerPartial('PartialMartItem', $('#mart_item_template').html());

		// HelperFunctions
		function typeHelper(typeName) {
			return function (type, opts) {
				if (getTypeName(type) == typeName) {
					return opts.fn(this);
				} else {
					return opts.inverse(this);
				}
			}
		}

		function getTypeName(type) {
			switch (type) {
				case "X1":
					return "topBanner";
				case "X2":
				case "X3":
					return "banner";
				case "A":
				case "B":
				case "C1":
				case "C2":
				case "Z":
				case "S":
					return "item";
			}
		}

		function ifCategoryTitle(type, opts) {
			if (type == 'X3') {
				return opts.fn(this);
			} else {
				return opts.inverse(this);
			}
		}

		function ifGreaterThan(base, value, opts) {
			if (value > base) {
				return opts.fn(this);
			} else {
				return opts.inverse(this);
			}
		}

		function ifSelectedItem(type, opts) {
			if (type == 'S') {
				return opts.fn(this);
			} else {
				return opts.inverse(this);
			}
		}

		function printMartItemGroupCss(itemType) {
			var cssClass = '';

			switch (itemType) {
				case 'X1':
					cssClass = 'mart_tit';
					break;
				case 'X2':
					cssClass = 'mart_stit';
					break;
				case 'X3':
					cssClass = 'item_tit';
					break;
				case 'A':
					cssClass = 'bx_mart mart_type1';
					break;
				case 'B':
					cssClass = 'bx_mart mart_type2';
					break;
				case 'C1':
					cssClass = 'bx_mart mart_type4';
					break;
				case 'C2':
					cssClass = 'bx_mart mart_type4 mart_type4_v2';
					break;
				case 'Z':
					cssClass = 'bx_mart mart_type3';
					break;
				case 'S':
					cssClass = 'bx_mart mart_type1 select_type';
					break;
			}

			return new Handlebars.SafeString(cssClass);
		}

		function renderDeliveryTag(deliveryType) {
			var str = '';

			if (deliveryType == 'BLUE') {
				str = '<span class="ico_del">무료배송</span>';
			} else if (deliveryType == 'SMART') {
				str = '<span class="ico_smart">스마트배송</span>';
			}

			return new Handlebars.SafeString(str);
		}

		function getItemUrl(type, categoryCode, goodsCode, url) {
			var itemUrl, fcd = '';

			if (categoryCode != undefined && categoryCode != null && categoryCode != '') {
				url = Mart.mobileWebUrl + '/Mart?categoryCode=' + categoryCode;

				if (goodsCode != undefined && goodsCode != null && goodsCode != '') {
					url += '&goodsCode=' + goodsCode;
				}
			}

			if (url != undefined && url != null && url != '') {
				itemUrl = url;
			}

			if (!Mart.isMain) {
				fcd = "715700003";
			}
			else {
				if (type == 'S')
					fcd = '706100001';
				else
					fcd = '711700129';
			}

			return getSnaUrl(fcd, itemUrl);
		}

		function getVipUrl(goodsCode, fcd) {
			var itemUrl = Mart.mItemWebUrl + "/Item?goodsCode=" + goodsCode
			return getSnaUrl(fcd, itemUrl);
		}

		function getCategoryUrl(fcdPrefix, categoryCode, categorySeq, isPop) {
			var url = Mart.mobileWebUrl + '/Mart';
			var fcd = (isPop) ? fcdPrefix : getCategoryFcdCodeV3(fcdPrefix);

			if (categoryCode != undefined && categoryCode != null && categoryCode != '' && categoryCode != homeCategory)
				url += "?categoryCode=" + categoryCode + "&categorySeq=" + categorySeq;

			return getSnaUrl(fcd, url);
		}

		function getContentsUrl(id) {
			var url = Mart.mobileWebUrl + "/Mart/Contents?id=" + id;
			return getSnaUrl("715700004", url);
		}

		function getCategoryIconCss(index, categoryCode) {
			//마트홈 비어있어서 api에서 빼고 보내줄 경우 아이콘 밀림 방지
			if (index == 0 && categoryCode != homeCategory) {
				iconCss.shift();
			}
			return iconCss[index];
		}

		function getSnaUrl(fcd, url) {
			var snaUrl = url || '';

			if (fcd != undefined && fcd != null && fcd != '') {
				snaUrl = 'http://sna.gmarket.co.kr/?fcd=' + fcd + '&url=' + encodeURIComponent(url);
			}

			return new Handlebars.SafeString(snaUrl);
		}

		function getCategoryFcdCode(fcdPrefix, categoryCode) {
			var fcdPrefix = fcdPrefix || "";
			var categoryCode = (categoryCode || "").substr(5, 4);

			if (fcdPrefix != '' && categoryCode != '') {
				return fcdPrefix + categoryCode;
			} else {
				return '';
			}
		}

		function getCategoryFcdCodeV3(fcdPrefix) {
			var fcdPrefix = fcdPrefix || "";

			var retFcdCode = "";
			if (fcdPrefix.length == 9)
				retFcdCode = fcdPrefix;
			else {
				var fcdSuffix = (categoryIndex + 1 < 10) ? "0" + (categoryIndex + 1) : (categoryIndex + 1);
				retFcdCode = fcdPrefix + "00" + fcdSuffix;
			}

			return retFcdCode;
		}

		var brandIndex = 0;
		function getBrandListStartTag() {
			var tag = "";

			if (brandIndex == 0 || brandIndex % 9 == 0)
				tag = "<li><ul>";

			return new Handlebars.SafeString(tag);
		}

		function getBrandListEndTag() {
			var tag = "";
			var compareIndex = brandIndex + 1;

			if (compareIndex % 9 == 0 || Mart.brandController.brandList.length == compareIndex)
				tag = "</ul></li>";

			brandIndex++;

			return new Handlebars.SafeString(tag);
		}

		var categoryIndex = 0;
		function getCategoryListStartTag() {
			var tag = "";

			if (categoryIndex == 0 || categoryIndex % 8 == 0)
				tag = "<li><ul>";

			return new Handlebars.SafeString(tag);
		}

		function getCategoryListEndTag() {
			var tag = "";
			var compareIndex = categoryIndex + 1;

			if (compareIndex % 8 == 0 || Mart.categoryController.categoryList.length == compareIndex)
				tag = "</ul></li>";

			//categoryIndex++;

			return new Handlebars.SafeString(tag);
		}

		function setCategoryIndex() {
			categoryIndex++;
			return "";
		}

		var contentsIndex = 0;
		function getContentsListStartTag() {
			var tag = "";

			if (contentsIndex == 0 || contentsIndex % 3 == 0)
				tag = "<li><ul>";

			return new Handlebars.SafeString(tag);
		}

		function getContentsListEndTag() {
			var tag = "";
			var compareIndex = contentsIndex + 1;

			if (compareIndex % 3 == 0 || Mart.contentsListController.contentsList.length == compareIndex)
				tag = "</ul></li>";

			contentsIndex++;

			return new Handlebars.SafeString(tag);
		}

		function categorySelectedCss(isSelected) {
			return new Handlebars.SafeString((isSelected) ? "class='on'" : "");
		}

		function getContentsListClass(seq) {
			return new Handlebars.SafeString((Mart.contentsSeq == seq) ? "class='cur'" : "");
		}

		//CookieUtils
		var CookieUtils = {};

		CookieUtils.setCookie = function (name, value) {
			document.cookie = name + '=' + encodeURIComponent(value) + ';domain=.gmarket.co.kr;path=/';
		}

		CookieUtils.getCookie = function (name) {
			var cname = name + '=';
			var ca = document.cookie.split(';');
			for (var i = 0; i < ca.length; i++) {
				var c = ca[i];
				while (c.charAt(0) == ' ') c = c.substring(1);
				if (c.indexOf(name) == 0) return decodeURIComponent(c.substring(cname.length, c.length));
			}
			return '';
		}

		//Renderer for template
		var Renderer = function (wrapperId, templateId) {
			this.$wrapper = $(wrapperId);
			this.template = Handlebars.compile($(templateId).html());
		};

		Renderer.prototype.render = function (data, appendId) {
			if (typeof appendId !== "undefined")
				$(appendId).append(this.getHtml(data));
			else
				this.$wrapper.html(this.getHtml(data));
		};

		Renderer.prototype.getHtml = function (data) {
			return this.template(data);
		}

		//TimedealController
		var TimedealController = function (timedeal) {
			var endDate = new Date(timedeal.EndDate);
			this.timedeal = timedeal || {};
			this.endDate = new Date(endDate.getUTCFullYear(), endDate.getUTCMonth(), endDate.getUTCDate(), endDate.getUTCHours(), endDate.getUTCMinutes(), endDate.getUTCSeconds());
			this.isExpire = new Date() > this.endDate;
			this.remain = { hours: 0, minutes: 0, seconds: 0 };
			this.cookieName = 'marttimedeal';
			this.timedeal.Fcd = '711900001';
			this.timedealRenderer = new Renderer('#layer_timeDeal', '#mart_timedeal_template');
		};

		//This function must be called after rendering Timedeal template
		TimedealController.prototype.$ = function () {
			var elements = {};

			elements.dimmedLayer = $('#mask');
			elements.remainTime = $('#txtRemainTime');
			elements.btnClose = $('#btn_timedeal_close');
			elements.btnDone = $('#btn_timedeal_done');

			return elements;
		};

		TimedealController.prototype.render = function (callback) {
			if (this.isRenderable()) {
				this.setTimer();
				this.timedealRenderer.render(this.timedeal);
				this.calculateTime();
				this.$().dimmedLayer.show();

				if (typeof callback === 'function') {
					callback.apply(this);
				}
			}
		};

		TimedealController.prototype.isRenderable = function () {
			var cookiedId = CookieUtils.getCookie(this.cookieName);

			//1. TimeDeal이 존재하며
			//2. EndDate가 현재 시각보다 늦고
			//3. '다시 보지 않기' 클릭 하지 않은 경우 타임딜 노출
			return this.timedeal != undefined && this.timedeal != null && this.timedeal.HasTimeDeal && !this.isExpire && this.timedeal.Id != cookiedId;
		};

		TimedealController.prototype.setTimer = function (callback) {
			var _this = this;
			this.timer = setInterval(function () { _this.calculateTime(); }, 1000);
		};

		TimedealController.prototype.clearTimer = function () {
			clearInterval(this.timer);
		}

		TimedealController.prototype.calculateTime = function () {
			var remainTime = Math.round((this.endDate - new Date()) / 1000);

			if (remainTime < 0) {
				this.isExpire = true;
				this.clearTimer();
			} else {
				this.isExpire = false;
				this.remain.hours = Math.floor((remainTime / 3600) % 3600);
				this.remain.minutes = Math.floor((remainTime - this.remain.hours * 3600) / 60 % 60);
				this.remain.seconds = Math.floor((remainTime - this.remain.minutes * 60) % 60);
				this.changeRemainTime();
			}
		};

		TimedealController.prototype.changeRemainTime = function () {
			var str = this.getTimeString(this.remain.hours, this.remain.minutes, this.remain.seconds);
			this.$().remainTime.text(str);
		}

		TimedealController.prototype.getTimeString = function (hours, minutes, seconds) {
			return this.getFormattedTime(hours) + ':' + this.getFormattedTime(minutes) + ':' + this.getFormattedTime(seconds);
		};

		TimedealController.prototype.getFormattedTime = function (time) {
			return (time > 9) ? time : '0' + time;
		};

		TimedealController.prototype.setEventHandler = function () {
			var _this = this;
			this.$().btnClose.on('click', function () {
				_this.clearTimer();
			});

			this.$().btnDone.on('click', function () {
				_this.neverDisplayAgain();
			});
		}

		TimedealController.prototype.neverDisplayAgain = function () {
			CookieUtils.setCookie(this.cookieName, this.timedeal.Id);
			this.clearTimer();
		};

		//ItemController
		var ItemController = function (itemGroups, bannerList) {
			this.itemGroups = itemGroups || [];
			this.bannerList = bannerList || [];
			this.itemRenderer = new Renderer('#mprd_lst', '#mart_group_template');
		};

		ItemController.prototype.getTopBannerList = function () {
			var arrList = new Array();

			$.each(this.bannerList, function (idx, item) {
				var render = new Renderer("", "#mart_topBanner_template");

				//if (getTypeName(item.Type) == "topBanner") {
				//if (item.ItemList.length > 0) {
				//	$.each(item.ItemList, function (inx, subItem) {
				//		arrList.push(render.getHtml(subItem));
				//	});
				//}
				//else
				arrList.push(render.getHtml({ ImageUrl: item.ImageUrl, LandingUrl: item.LandingUrl, Title: item.Title }));
				//}
			});

			return arrList;
		}

		//ItemController.prototype.swipeGmk = function (data, areaId, pageId, random, auto) {
		ItemController.prototype.swipeGmk = function (data, areaId, pageId, auto) {
			var dots;
			var tempSwipe = new SwipeView(areaId, { numberOfPages: data.length, autoSwipe: auto });
			if (pageId != null && pageId != '') {
				$('.pag').text(tempSwipe.pageIndex + 1);
				$('.pag2').text(data.length);
			}

			for (var i = 0; i < 3; i++) {
				var page = i == 0 ? data.length - 1 : data.length == 1 ? 0 : i - 1;
				tempSwipe.masterPages[i].innerHTML = data[page];
			}

			tempSwipe.onFlip(function () {
				var upcoming;

				for (var i = 0; i < 3; i++) {
					upcoming = tempSwipe.masterPages[i].dataset.upcomingPageIndex;

					if (upcoming != tempSwipe.masterPages[i].dataset.pageIndex) {
						tempSwipe.masterPages[i].innerHTML = data[upcoming];
					}
				}
				if (pageId != null && pageId != '') {
					$('.pag').text(tempSwipe.pageIndex + 1);
				}
			});

			var index = 0;

			/*if (random) {
			index = Math.floor(Math.random() * data.length) % data.length;
			tempSwipe.goToPage(index);
			}

			if (auto)
			tempSwipe.t = setTimeout(function () { tempSwipe.next(); }, 3000);*/

			return tempSwipe;
		}

		ItemController.prototype.topBannerRender = function () {
			var topBannerList = this.getTopBannerList();

			if (topBannerList.length > 1)
				spcListScroller = this.swipeGmk(topBannerList, "#bnrList", "bnrPage", true);
			else if (topBannerList.length == 0)
				$("#top_banner").hide();
			else if (topBannerList.length == 1) {
				document.getElementById("bnrList").innerHTML = topBannerList;
				$('.display_bnr .bnr_w .h_page').hide();
			}
		}

		ItemController.prototype.render = function () {
			if (Mart.isMain)
				this.topBannerRender();

			this.itemRenderer.render({ ItemGroupList: this.itemGroups }, "div.mart_wrap");
		};

		//BrandController
		var BrandController = function (brandList) {
			this.brandList = brandList || [];			
			this.brandRenderer = new Renderer('#brand_list', '#mart_brand_template');
		};

		BrandController.prototype.render = function () {
			if (this.brandList.length > 0)
				var fcd = Mart.isMain ? '715700006' : '715700007';
				this.brandRenderer.render({ BrandList: this.brandList, Fcd: fcd }, "div.mart_wrap");
			//else
			//	$("#brand_list").hide();
		};

		var MainCategoryController = function (categoryList, types) {
			this.types = types;
			this.categoryList = categoryList || [];

			var categoryTemplate = "#mart_category_template";
			this.categoryRender = new Renderer(types[0].wrapperId, categoryTemplate);
		}

		MainCategoryController.prototype.render = function () {
			this.categoryRender.render({ CategoryList: this.categoryList, FcdPrefix: this.types[0].fcdPrefix, IsSepared: true, IsTop: false, IsPop: false });
		};

		//CategoryController
		var CategoryController = function (categoryList, types) {
			this.types = types;
			this.categoryList = categoryList || [];

			var categoryTemplate = "#mart_category_template";
			this.categoryRenderTop = new Renderer(types[0].wrapperId, categoryTemplate);
		}

		CategoryController.prototype.render = function () {
			this.categoryRenderTop.render({ CategoryList: this.categoryList, FcdPrefix: this.types[0].fcdPrefix, IsSepared: false, IsTop: true, IsPop: false });
		};

		//MartCategoryController
		var MartCategoryController = function (categoryList, types) {
			this.types = types;
			this.categoryList = categoryList || [];

			var categoryTemplate = "#mart_category_template";
			this.categoryRenderPop = new Renderer(types[0].wrapperId, categoryTemplate);
		}

		MartCategoryController.prototype.render = function () {
			this.categoryRenderPop.render({ CategoryList: this.categoryList, FcdPrefix: "715700001", IsSepared: false, IsTop: false, IsPop: true });
		};

		var ContentsController = function (contentsList) {
			this.contentsList = contentsList || [];
			this.contentsRenderer = new Renderer('#contents_lst', '#mart_contents_template');
		}

		ContentsController.prototype.render = function () {
			var list;

			if (this.contentsList.length == 0)
				$("#contents_lst").hide();
			else {
				if (this.contentsList.length > 1) {
					list = new Array();
					list.push(this.contentsList[Math.floor(Math.random() * this.contentsList.length)]);
				}
				else
					list = this.contentsList;

				this.contentsRenderer.render({ ContentsList: list });
			}
		}

		var ContentsListController = function (contentsList) {
			this.contentsList = contentsList || [];
			this.contentsRenderer = new Renderer('#contents_lst', '#mart_contents_template');
		}

		ContentsListController.prototype.render = function () {
			this.contentsRenderer.render({ ContentsList: this.contentsList });
		}

		var ContentsItemListController = function (ItemList) {
			this.ItemList = ItemList || [];
			this.itemRenderer = new Renderer('#goods_lst', '#mart_item_template');
		}

		ContentsItemListController.prototype.render = function () {
			this.itemRenderer.render({ ItemList: this.ItemList });
		}

		if (!Mart.isContents) {
			Mart.timedealController = new TimedealController(Mart.Data.TimeDeal);
			Mart.timedealController.render(Mart.timedealController.setEventHandler);

			Mart.itemController = new ItemController(Mart.Data.ItemGroupList, Mart.Data.BannerList);
			Mart.itemController.render();

			Mart.categoryController = new CategoryController(Mart.Data.CategoryList, [{ "fcdPrefix": "71180", "wrapperId": "#category_lst_top"}]);
			Mart.maincategoryController = new MainCategoryController(Mart.Data.MartCategoryList, [{ "fcdPrefix": "71560", "wrapperId": "#category_lst"}]);
			Mart.maincategoryController.render();
			Mart.categoryController.render();

			Mart.martcategoryController = new MartCategoryController(Mart.Data.MartCategoryList, [{ "fcdPrefix": "71560", "wrapperId": "#category_lst_pop"}]);
			Mart.martcategoryController.render();

			if ($("#mart_brand_template").length > 0) {
				Mart.brandController = new BrandController(Mart.Data.BrandList);
				Mart.brandController.render();
			}

			if ($("#mart_contents_template").length > 0) {
				Mart.contentsController = new ContentsController(Mart.Data.ContentsList);
				Mart.contentsController.render();
			}
		}
		else {
			Mart.contentsListController = new ContentsListController(Mart.Data.ContentsList);
			Mart.contentsListController.render();

			Mart.contentsItemListController = new ContentsItemListController(Mart.Data.ItemList)
			Mart.contentsItemListController.render();

			if (Mart.Data.ItemList.length == 0) {
				$("#tit_goods").hide();
				$("#goods_lst").hide();
			}
		}
	})(GMobile.Mart = GMobile.Mart || {});
})(window.GMobile = window.GMobile || {}, jQuery, Handlebars);