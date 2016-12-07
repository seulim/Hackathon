(function (GMobile, $, SwipeView, Handlebars, undefined) {
	(function () {
		Handlebars.registerHelper("ifExist", ifExist);
		Handlebars.registerHelper("ifEquals", ifEquals);
		Handlebars.registerHelper("ifOver", ifOver);

		Handlebars.registerHelper("GetTourSubItemFcd", GetTourSubItemFcd);
		Handlebars.registerHelper("GetBannerFcd", GetBannerFcd);

		Handlebars.registerPartial("PartialMainItem", $("#partial_main_item_template").html());
		Handlebars.registerPartial("PartialMainBanner", $("#partial_main_banner_template").html());
	})();

	function GetBannerFcd(opts) {
		return $("#partial_main_banner_template").data('fcd_index') + 716400000;
	}

	function ifExist(list, opts) {
		if (list !== undefined && list !== null && list.length > 0)
			return opts.fn(this);
		else
			return opts.inverse(this);
	}

	function ifEquals(val1, val2, opts) {
		if (val1 === val2)
			return opts.fn(this);
		else
			return opts.inverse(this);
	}

	function ifOver(base, val, opts) {
		if (val > base)
			return opts.fn(this);
		else
			return opts.inverse(this);
	}

	function GetTourSubItemFcd(topExposeYn) {
		if (topExposeYn !== undefined && topExposeYn.toUpperCase() == "Y") {
			return "714300001";
		} else {
			return "714300002";
		}
	}

	var OrderType = {
		RANK_POINT_DESC: "RankPointDesc",
		NEW: "New",
		SELL_PRICE_ASC: "SellPriceAsc"
	};

	var scrollListener, mainCtrl, subCtrl;

	var MainCtrl = function () {
		this.$wrapper = $("#tourMainContWrap");
		this.template = Handlebars.compile($("#tour_main_contents_template").html());

		this.param = {
			middleGroupNo: 0,
			smallGroupNo: 0,
			pageNo: 1,
			pageSize: 80,
			order: OrderType.RANK_POINT_DESC
		}
	};

	MainCtrl.prototype.fetch = function () {
		var _this = this;
		$.ajax({
			url: "/Tour/GetTourItem",
			data: _this.param,
			type: "POST",
			success: function (data) {
				_this.render(data);
			},
			error: function (e) {

			}
		});
	};

	MainCtrl.prototype.render = function (data) {
		if (data.TopItemList) data.TopItemList.FcdCode = "714100001";
		if (data.ItemList) data.ItemList.FcdCode = "714100002";

		this.$wrapper.html(this.template(data));
		this.$wrapper.show();
		$("#tourSubSelectWrap").hide();
		$("#tourSubItemWrap").hide();
		this.bannerSwipe();
		$(window).trigger("resize");

		window.removeEventListener("scroll", scrollListener);
	};

	MainCtrl.prototype.bannerSwipe = function () {
		//CD에 있던 script	
		var el, i, page, dots1 = document.querySelectorAll('#bnTurn button'), gallery1;
		var bnGrpList1 = $("#bnGrp li");

		if (1 < bnGrpList1.length) {
			var bnGrpListAtag1 = [];
			for (var j = 0; j < bnGrpList1.length; j++) {
				bnGrpListAtag1[j] = bnGrpList1[j].innerHTML;
			}
			var globalBnGrpListAtag1 = bnGrpListAtag1.join(",");
			/* //HTML 파싱 */

			gallery1 = new SwipeView('#bnGrp', {
				numberOfPages: bnGrpListAtag1.length,
				hastyPageFlip: true,
				flickTime: 1000
			});
			//console.log(gallery1);

			// Load initial data
			for (i = 0; i < 3; i++) {
				if (1 == bnGrpListAtag1.length) {
					page = i == 0 || 2 ? bnGrpListAtag1.length - 1 : i - 1;
				} else {
					page = i == 0 ? bnGrpListAtag1.length - 1 : i - 1;
				}
				el = document.createElement('span');
				el.innerHTML = bnGrpListAtag1[page];
				gallery1.masterPages[i].appendChild(el)

				el.width = bnGrpListAtag1[page].width;
				el.height = bnGrpListAtag1[page].height;
				el.onload = function () { this.className = ''; }
			}

			gallery1.onFlip(function () {
				var el, upcoming, i;

				for (i = 0; i < 3; i++) {
					upcoming = gallery1.masterPages[i].dataset.upcomingPageIndex;

					if (upcoming != gallery1.masterPages[i].dataset.pageIndex) {
						el = gallery1.masterPages[i].querySelector('span');
						el.innerHTML = bnGrpListAtag1[upcoming];
					}
				}
				//setImgHeight();//140307추가
			});

			flicking("bnGrp", "prev", "next");

			var randIdx = Math.floor(Math.random() * bnGrpList1.size());
			gallery1.goToPage(randIdx);

			$("#prev").on("click", function () {
				gallery1.prev();
				return false;
			});

			$("#next").on("click", function () {
				gallery1.next();
				return false;
			});

		} else if (1 == bnGrpList1.length) {
			var bnGrpListAtag1 = [];

			for (var j = 0; j < bnGrpList1.length; j++) {
				bnGrpListAtag1[j] = bnGrpList1[j].innerHTML;
			}

			var globalBnGrpListAtag1 = bnGrpListAtag1.join(",");
			/*140128*/
			var elSpan = document.createElement('span');
			elSpan.innerHTML = globalBnGrpListAtag1;
			document.querySelector('#bnGrp').appendChild(elSpan);
			//$("#bnGrp").append(globalBnGrpListAtag1);
			/* //140128*/
			flicking("bnGrp", "prev", "next");
		}
	};

	var SubCtrl = function (middleGroupNo) {
		this.$selectWrapper = $("#tourSubSelectWrap");
		this.$bannerWrapper = $("#tourSubBannerWrap");
		this.bannerTemplate = Handlebars.compile($("#tour_sub_banner_template").html());

		this.selectTemplate = Handlebars.compile($("#tour_sub_select_template").html());

		this.$itemWrapper = $("#tourSubItemWrap");
		this.itemTemplate = Handlebars.compile($("#tour_sub_item_template").html());

		this.param = {
			middleGroupNo: middleGroupNo || 0,
			smallGroupNo: 0,
			pageNo: 1,
			pageSize: 50,
			order: OrderType.RANK_POINT_DESC
		};

		this.hasNext = true;
	};

	SubCtrl.prototype.getMiddleGroupNo = function () {
		return this.param.middleGroupNo;
	};

	SubCtrl.prototype.setSmallGroupNo = function (smallGroupNo) {
		this.param.pageNo = 1;
		this.param.smallGroupNo = smallGroupNo;
	};

	SubCtrl.prototype.setOrder = function (order) {
		this.param.pageNo = 1;
		this.param.order = order;
	};

	SubCtrl.prototype.moreItems = function () {
		if (this.hasNext) {
			this.param.pageNo++;
			this.fetch();
		}
	};

	SubCtrl.prototype.fetch = function () {
		var _this = this;
		window.removeEventListener("scroll", scrollListener);
		$.ajax({
			url: "/Tour/GetTourItem",
			data: _this.param,
			type: "POST",
			success: function (data) {
				_this.hasNext = data.Paging.HasNext;

				if (data.Paging.CurrentPageNo > 1) {
					_this.append(data);
				} else {
					_this.render(data);
				}
			},
			error: function (e) {

			}
		});
	};

	SubCtrl.prototype.render = function (data) {
		//console.log(data);

		var _this = this;

		data.SGroupList = data.GroupList.filter(function (el) {
			return el.MiddleGroupNo == data.SelectedMGroupNo;
		})[0].SGroupList;

		this.$bannerWrapper.html(this.bannerTemplate(data));
		this.$bannerWrapper.show();

		this.$selectWrapper.html(this.selectTemplate(data));
		this.$selectWrapper.show();

		this.$itemWrapper.html(this.itemTemplate(data));
		this.$itemWrapper.show();
		$("#tourMainContWrap").hide();

		$("#tourSmallGroupList a").on("click", function () {
			var smallGroupNo = $(this).data("groupno");

			location.hash = "#" + _this.getMiddleGroupNo() + "/" + smallGroupNo;
		});

		$("#tourSortOrderList a").on("click", function () {
			var sortOrder = $(this).data("sort");

			_this.setOrder(sortOrder);
			_this.fetch();
		});

		/*
		//CD 수정 전 임시로 테스트 위해
		$(".country_select").on("click", function () {
		$(".select_lst").toggle();
		});

		$(".btn_align").on("click", function () {
		$(this).toggleClass('on');
		$(".align_lst").toggle();
		});
		*/
		scrollListener = function () {
			if ($(window).scrollTop() + $(window).height() >= $("#content").height()) {
				//console.log($(window).scrollTop(), $(window).height(), $("#content").height())
				_this.moreItems();
			}
		};
		window.addEventListener("scroll", scrollListener);
	}

	SubCtrl.prototype.append = function (data) {
		this.$itemWrapper.append(this.itemTemplate(data));
		window.addEventListener("scroll", scrollListener);
	}

	//Hashbang url routing
	var router = new Grapnel();

	mainCtrl = new MainCtrl();
	mainCtrl.render(GMobile.TourModel);

	router.get(":middleGroupNo?/:smallGroupNo?", function (req) {
		var middleGroupNo = Number(req.params.middleGroupNo) || 0;
		var smallGroupNo = Number(req.params.smallGroupNo) || 0;

		var ctrl = mainCtrl;

		if (middleGroupNo != 0) {
			ctrl = new SubCtrl(middleGroupNo);
			ctrl.setSmallGroupNo(smallGroupNo);
		}

		ctrl.fetch();

		var $li = $("#tourMenu>li").removeClass("on");
		$li.find(".tour_on_img").hide();
		$li.find(".tour_off_img").show();

		var $selected = $("#tourMenu>li[data-groupno='" + middleGroupNo + "']");
		$selected.addClass("on");
		$selected.find(".tour_off_img").hide();
		$selected.find(".tour_on_img").show();

		$("#partial_main_banner_template").data('fcd_index', $selected.index());
	});
})(window.GMobile = window.GMobile || {}, jQuery, SwipeView, Handlebars);