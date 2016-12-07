/*	swipe Image 
CD팀에서 온 스크립트 그대로 사용 하였습니다
디자인쪽 css가 틀려서 기존에 사용하던 거와 틀려서 어쩔수 없었어요.
근데 별반 틀린건 없네요.
*/
var Swiping = function (id) {
	var _id = id;
	var _lstId;
	var $wrp = $('#' + _id);
	var $imglist = $wrp.find('.sw-viwer');
	var $pager = $wrp.find('.sw-paging');
	var aData = [];
	var oSwipe;
	var page;
	var $indicator;
	var $indiType = -1;

	var oSwiping = {
		init: init
	}
	init();
	return oSwiping;

	function init() {
		if ($imglist.children().size() > 1) {
			initSwipe();
			addSwipe();
		}
		else {
			$wrp.find('.btn_prev').css({ display: 'none' });
			$wrp.find('.btn_next').css({ display: 'none' });
		}
		initIndicator();
	}
	function initSwipe() {
		if (typeof $imglist.attr('id') !== 'undefined') {
			_lstId = $imglist.attr('id');
		}
		else {
			_lstId = _id + '-view';
			$imglist.attr({ 'id': _lstId });
		}

		$imglist.children().each(function () {
			aData.push($(this)[0].outerHTML)
			$(this).remove();
		});

		oSwipe = new SwipeView('#' + _lstId, { numberOfPages: aData.length });

		// Load initial data
		for (var i = 0; i < 3; i++) {
			page = i == 0 ? aData.length - 1 : aData.length == 1 ? 0 : i - 1;
			oSwipe.masterPages[i].innerHTML = aData[page];
			$(oSwipe.masterPages[i]).children().css({ display: "block" });

			if ($(oSwipe.masterPages[i]).hasClass('swipeview-active')) {
				$(oSwipe.masterPages[i]).css({ position: 'relative' });
			}
			else {
				$(oSwipe.masterPages[i]).css({ position: 'absolute' });
			}

		}

		if ($wrp.find('.btn_prev').size() > 0) {
			$wrp.find('.btn_prev').on({
				click: function (e) {
					e.preventDefault();
					oSwipe.prev();
				}
			});
		}
		if ($wrp.find('.btn_next').size() > 0) {
			$wrp.find('.btn_next').on({
				click: function (e) {
					e.preventDefault();
					oSwipe.next();
				}
			});
		}
	}
	function initIndicator() {
		if ($wrp.find('.sw_idc').size() > 0) {
			$indicator = $wrp.find('.sw_idc');
			if ($wrp.find('.sw_idc').hasClass('type2')) {
				//인디케이터 도트
				$indiType = 2;
				var indiHTML = '';
				for (var i = 0; i < aData.length; i++) {
					indiHTML += '<a href="#">' + i + '</a>';
				}
				$indicator.append(indiHTML);
				$indicator.children().eq(0).addClass('on');
				$indicator.find(">a").on({
					click: function (e) {
						e.preventDefault();
						var idx = $(this).index();
						oSwipe.goToPage(idx)
					}
				});
			}
			else {
				//숫자 변환
				$indiType = 1;
				var indiHTML = '';
				if (aData.length != 0) {
					indiHTML += '<span class="page"><span class="pag"><strong>1</strong></span>/<span class="pag2">' + (aData.length == 0 ? 1 : aData.length) + '</span></span>';
					$indicator.prepend(indiHTML);
				}
			}
		}
	}
	function addSwipe() {
		oSwipe.onFlip(function () {
			var el, upcoming, i;
			var upcoming;
			for (var i = 0; i < 3; i++) {
				var masterPage = oSwipe.masterPages[i];
				upcoming = masterPage.dataset.upcomingPageIndex;

				if (upcoming != oSwipe.masterPages[i].dataset.pageIndex) {
					masterPage.innerHTML = aData[upcoming];
					$(masterPage).children().css({ display: "block" });
				}

				($(masterPage).hasClass('swipeview-active')) ? $(masterPage).css({ position: 'relative' }) : $(masterPage).css({ position: 'absolute' });
			}

			if (typeof $indicator[0] !== 'undefined' && $indiType == 1) {
				$indicator.find(".pag").html(parseInt(oSwipe.pageIndex) + 1);
			}
			else if (typeof $indicator[0] !== 'undefined' && $indiType == 2) {
				$indicator.find("a").removeClass("on");
				$indicator.find("a").eq(oSwipe.pageIndex).addClass("on");
			}
		});
	}
};

(function (GMobile, $, SwipeView, Handlebars, undefined) {
	(function (Space, undefined) {
		$("#content").on("click", "a, button", function () {
			var fcd = $(this).data("fcd");

			if (fcd) {
				SnaUrlFooter(fcd);
			}
		});

		var contentsList = Space.Data.ContentsSection.ContentsList;

		if (contentsList != null && contentsList.length > 0) {
			var html = '';
			for (var i = 0; i < contentsList.length; i++) {
				html += '<li><a href="http://sna.gmarket.co.kr/?fcd=713100001&url=' + encodeURIComponent(contentsList[i].ConnectUrl) + '">'
						+ '<img src="' + contentsList[i].MainBannerImageUrl + '" alt="' + contentsList[i].Title + '" /></a></li>';
			}
			$('#liContentsList').html(html);
		}

		function saleType() {
			return function (item, opts) {
				if (typeof item === "object" && typeof item.CouponImageUseYN === "string" && item.CouponImageUseYN.toUpperCase() == "Y") {
					return opts.fn(this);
				} else {
					return opts.inverse(this);
				}
			};
		}

		Handlebars.registerHelper("ifSale", saleType());

		function typeHelper(type1, type2) {
			return function (item, opts) {
				if (typeof item === "object" && typeof item.Type === "string" &&
						(item.Type.toUpperCase() == type1 || item.Type.toUpperCase() == type2)) {
					return opts.fn(this);
				} else {
					return opts.inverse(this);
				}
			};
		};

		Handlebars.registerHelper("ifTypeA", typeHelper("A1", "A2"));
		Handlebars.registerHelper("ifTypeB", typeHelper("B1", "B2"));

		Handlebars.registerHelper("eachBTop", function (array, type, opts) {
			if (typeof array === "object" && array.length > 0) {
				var ret = "";
				for (var i = 0; i < array.length; i++) {
					if (type == "B1") {
						if (array[i].Type == "LargeRectangle") {
							ret = ret + opts.fn(array[i]);
						}
					} else if (type == "B2") {
						if (array[i].Type == "Square") {
							ret = ret + opts.fn(array[i]);
						}
					}
				}
				return ret;
			}
		});
		Handlebars.registerHelper("eachBBottom", function (array, type, opts) {
			if (typeof array === "object" && array.length > 0) {
				var ret = "";
				for (var i = 0; i < array.length; i++) {
					if (type == "B1") {
						if (array[i].Type == "Square") {
							ret = ret + opts.fn(array[i]);
						}
					} else if (type == "B2") {
						if (array[i].Type == "LargeRectangle") {
							ret = ret + opts.fn(array[i]);
						}
					}
				}
				return ret;
			}
		});

		Handlebars.registerHelper("spaceItemType", function (type) {
			if (type == "A1") {
				return "type";
			} else if (type == "A2") {
				return "type2";
			} else if (type == "B1") {
				return "type3";
			} else if (type == "B2") {
				return "type4";
			} else if (type == "C") {
				return "type5";
			} else {
				return "type";
			}
		});

		Handlebars.registerPartial("PartialSpaceItem", $("#tmpl_partial_space_item").html());

		Handlebars.registerHelper("getSnaUrl", function (landingUrl, fcd) {
			return "http://sna.gmarket.co.kr/?fcd=" + fcd + "&url=" + encodeURIComponent(landingUrl);
		});

		Handlebars.registerHelper("getSnaUrlForBrand", function (seq, fcd, sUrl) {
			var landingUrl = mobileWebUrl + "/Space/BrandDetail?id=" + seq;
			if (sUrl != null && sUrl != "") {
				landingUrl = sUrl;
			}
			return "http://sna.gmarket.co.kr/?fcd=" + fcd + "&url=" + encodeURIComponent(landingUrl);
		});

		Handlebars.registerHelper("getSnaUrlForItem", function (landingUrl, type, fcd) {
			return "http://sna.gmarket.co.kr/?fcd=" + fcd + "&url=" + encodeURIComponent(landingUrl);
		});

		Space.SpaceRenderController = function (wrapperSelector, templateSelector, url) {
			this.$wrapper = $(wrapperSelector);
			this.template = Handlebars.compile($(templateSelector).html());
			this.url = url;

			this.$moreButton = $("#btn_more");
			this.$loadingIcon = $("#ico_loading");

			this.isRendering = false;
		};

		Space.SpaceRenderController.prototype.render = function (data, append, callback) {
			var that = this;

			if (!this.isRendering) {
				if (!append) {
					this.$wrapper.html("");
					that.$moreButton.hide();
				}

				this.$loadingIcon.show();
				this.isRendering = true;

				$.ajax({
					url: this.url,
					data: data,
					type: "POST",
					success: function (data) {
						that.$wrapper.append(that.template(data));

						if (data.Paging.HasNext) {
							that.$moreButton.show();
						} else {
							that.$moreButton.hide();
						}

						that.$loadingIcon.hide();
						that.isRendering = false;

						if (typeof callback === "function") {
							callback.apply();
						}
					},
					error: function (e) {
						that.$loadingIcon.hide();
						that.isRendering = false;

						if (typeof callback === "function") {
							callback.apply();
						}
					}
				});
			}
		}

		Space.BrandRenderController = function (wrapperSelector, templateSelector, url) {
			this.$wrapper = $(wrapperSelector);
			this.template = Handlebars.compile($(templateSelector).html());
			this.url = url;

			this.$loadingIcon = $("#ico_loading");

			this.isRendering = false;
		};

		Space.BrandRenderController.prototype.render = function (data, append) {
			var that = this;

			if (!this.isRendering) {
				if (!append) {
					this.$wrapper.html("");
				}

				this.$loadingIcon.show();
				this.isRendering = true;

				$.ajax({
					url: this.url,
					data: data,
					type: "POST",
					success: function (data) {
						that.$wrapper.append(that.template(data));

						that.$loadingIcon.hide();
						that.isRendering = false;
					},
					error: function (e) {
						that.$loadingIcon.hide();
						that.isRendering = false;
					}
				});
			}
		};

		var lGroupNo = 1;
		var mGroupNo = 0;
		var pageNo = 1;

		Space.SpaceRenderController = new Space.SpaceRenderController("#item_list_wrapper", "#tmpl_Space_item", "/Space/GetSpaceSectionItem");
		Space.BrandRenderController = new Space.BrandRenderController("#item_list_wrapper", "#tmpl_Brand_item", "/Space/GetSpaceBrandSectionItem");

		function renderSpace(append) {
			var data = {
				lgroupNo: lGroupNo,
				mgroupNo: mGroupNo,
				pageNo: pageNo++
			}

			Space.SpaceRenderController.render(data, append, function () {
				$moreButton.on("click", moreButtonClickCallback);
			});
		}

		var $moreButton = $("#btn_more");
		function moreButtonClickCallback() {
			$moreButton.off("click", moreButtonClickCallback);

			renderSpace(true);
		}
		$moreButton.on("click", moreButtonClickCallback);

		function renderBrand(append) {
			var data = {
				lgroupNo: lGroupNo,
				mgroupNo: mGroupNo
			}

			Space.BrandRenderController.render(data, append);
		}

		var $tabWrapper = $("#divTab");

		$tabWrapper.find(".tab_mnu").children().on("click", function () {
			$tabWrapper.find(".tab_mnu").removeClass("on");
			$(this).parent().addClass("on");

			var linkFcd = $(this).attr("data-fcd");
			if (linkFcd == "713200001") {
				lGroupNo = 1; mGroupNo = 0; pageNo = 1;
				$moreButton.show();
				$('.bx_swipe').find('li').removeClass("on");
				renderSpace(false);
			} else if (linkFcd == "713300001") {
				lGroupNo = 2; mGroupNo = 0;
				$moreButton.hide();
				$('.bx_swipe').find('li').removeClass("on");
				renderBrand(false);
			}
		});

		$('.bx_swipe').find('li').on("click", function () {
			$('.bx_swipe').find('li').removeClass("on");
			$(this).addClass("on");
		});

		$("#aSpaceCategory01").on("click", function (e) {
			lGroupNo = 1; mGroupNo = 1; pageNo = 1;
			$moreButton.show();
			renderSpace(false);
		});

		$("#aSpaceCategory02").on("click", function (e) {
			lGroupNo = 1; mGroupNo = 2; pageNo = 1;
			$moreButton.show();
			renderSpace(false);
		});

		$("#aSpaceCategory03").on("click", function (e) {
			lGroupNo = 1; mGroupNo = 3; pageNo = 1;
			$moreButton.show();
			renderSpace(false);
		});

		$("#aSpaceCategory04").on("click", function (e) {
			lGroupNo = 1; mGroupNo = 4; pageNo = 1;
			$moreButton.show();
			renderSpace(false);
		});

		$("#aSpaceCategory05").on("click", function (e) {
			lGroupNo = 1; mGroupNo = 5; pageNo = 1;
			$moreButton.show();
			renderSpace(false);
		});

		$("#aBrandCategory06").on("click", function (e) {
			lGroupNo = 2; mGroupNo = 6;
			$moreButton.hide();
			renderBrand(false);
		});

		$("#aBrandCategory07").on("click", function (e) {
			lGroupNo = 2; mGroupNo = 7;
			$moreButton.hide();
			renderBrand(false);
		});

		$("#aBrandCategory08").on("click", function (e) {
			lGroupNo = 2; mGroupNo = 8;
			$moreButton.hide();
			renderBrand(false);
		});

		$("#aBrandCategory09").on("click", function (e) {
			lGroupNo = 2; mGroupNo = 9;
			$moreButton.hide();
			renderBrand(false);
		});


		$(function () {
			if (lCd > 0 && mCd > 0) {
				if (lCd == 1) {
					var spaceCtrl = $("#aSpaceCategory0" + mCd.toString());
					if (spaceCtrl != undefined && spaceCtrl != null && spaceCtrl.length > 0) {
						spaceCtrl.click();
					}
				} else if (lCd == 2) {
					var brandCtrl = $("#aBrandCategory0" + mCd.toString());
					if (brandCtrl != undefined && brandCtrl != null && brandCtrl.length > 0) {
						brandCtrl.click();
					}
				}
			}
            else if(lCd > 0){
                if(lCd == 1){
                    $("#spaceSpaceTab").click();
                }
                else if(lCd == 2){
                    $("#spaceBrandTab").click();
                }
            }
			renderSpace(false);
		});
	})(GMobile.Space = GMobile.Space || {});
})(window.GMobile = window.GMobile || {}, jQuery, SwipeView, Handlebars);

$(document).ready(function () {
	$('.swipewrp').each(function (i) {
		var _id = 'swipewrp' + i;
		(typeof $(this).attr('id') !== 'undefined') ? _id = $(this).attr('id') : $(this).attr({ 'id': _id });
		var swipe = new Swiping(_id);
	});
});