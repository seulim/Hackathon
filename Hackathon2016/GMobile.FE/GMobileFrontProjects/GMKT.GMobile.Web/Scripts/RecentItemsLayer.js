var RviShare;
var rviLayer;
var toggleValue = false;

$(window).unload(function () {
});

$(function () {
	var recentItemsLayerDomainUrl = "//mobile.gmarket.co.kr";
	if (typeof (CONST_RECENT_ITEMS_LAYER_DOMAIN_URL) != "undefined") {
		if (CONST_RECENT_ITEMS_LAYER_DOMAIN_URL != null || CONST_RECENT_ITEMS_LAYER_DOMAIN_URL == "") {
			recentItemsLayerDomainUrl = CONST_RECENT_ITEMS_LAYER_DOMAIN_URL;
		}
	}
	var mLocalStorage = null;
    try {
        mLocalStorage = window.localStorage;
    } catch (e) { }

	var isMobileDomain = (location.href.indexOf(recentItemsLayerDomainUrl) > 0);

	var goodsCodeLength = 9;
	var relDate = new Date("2015-07-02");
	var chkDate = new Date(relDate);
	chkDate.setDate(chkDate.getDate() + 30); //예전 expireDate = +30

	var RviLayer = function (rviWrapper) {
		if (isApp() == "Y") return; //app일경우 return;

		var url = recentItemsLayerDomainUrl + "/Common/RecentItemsLayer";
		var $rviWrapper = $();

		//var recentItems = [];

		this.iframeWrapper = "#extGetRecentItemsIframe";
		this.templateId = "#recently_viewed_items_template";
		this.templateWrapper = "#rvi_lst";

		Handlebars.registerHelper('ifDateHasNotChanged', ifDateHasNotChanged);

		var that = this;

		$.ajax({
			url: url,
			//data: data,
			async: true,
			dataType: "jsonp",
			success: function (template) {
				that.template = template.trim();
				that.$rviWrapper = $(rviWrapper);

				if (that.$rviWrapper[0] != undefined) {
					setTimeout(function () {
						that.render();
					}, 300);
				}
			}
		});
	};

	RviLayer.prototype.render = function () {
		this.$rviWrapper.html(this.template);

		var that = this;

		//from front_v4
		if (typeof $(".btn_recent")[0] !== "undefined") {
			if (typeof initRecentItem === "function") {
				initRecentItem();
			}
		}

		$("#rviGoFavoritePage").on("click", function (e) {
			e.stopPropagation(); e.preventDefault();
			location.href = MmygDomain + "/Favorite/MyFavoriteItems?viewTitle=Y";
		});

		$("#rviAddFavoritePage").on("click", function (e) {
			e.stopPropagation(); e.preventDefault();
			addFavorite();
		});

		$(".ly_del .btn_blue").on("click", function (e) {
			e.stopPropagation(); e.preventDefault();
			that.removeRecentItems();
		});

		$("#rviCheckAll").on("click", function (e) {
			e.stopPropagation(); e.preventDefault();
			if (toggleValue) {
				var tmpToggle = false;
				$.each($(".recen_lst .inp_chk"), function (k, v) {
					if ($(v).is(":checked") === true) {
						tmpToggle = true;
						return false;
					}
				});
				if (!tmpToggle) {
					toggleValue = false;
				}
				/*if($(".recen_lst .inp_chk").is("checked") === false) {
				toggleValue = false;
				return;
				}*/
			} else {
				var tmpToggle = false;
				$.each($(".recen_lst .inp_chk"), function (k, v) {
					if ($(v).is(":checked") === false) {
						tmpToggle = true;
						return false;
					}
				});
				if (!tmpToggle) {
					toggleValue = true;
				}
			}

			if (toggleValue) {
				$(".recen_lst .inp_chk").prop("checked", false);
				toggleValue = false;
			} else {
				$(".recen_lst .inp_chk").prop("checked", true);
				toggleValue = true;
			}
		});

		//		$(".btn_recent").on("click", function (e) {
		//			scrollTo(0, scrollY + 1);
		//			$('.re_dimmed').click(function (e) {
		//				scrollTo(0, scrollY - 1);	
		//			});	
		//		});

		var $extGetRecentItemsIframe = $(this.iframeWrapper);

		$extGetRecentItemsIframe.load(function () {
			//that.recentItemsController = new RecentItemsController(isMobileDomain, that.iframeWrapper);
			that.recentItemsController = new RecentItemsController(false, that.iframeWrapper);
			that.getRecentItems();
		});
		$extGetRecentItemsIframe.attr("src", "https:" + recentItemsLayerDomainUrl + "/Common/RecentThings");
		//$extGetRecentItemsIframe.attr("src", recentItemsLayerDomainUrl + "/Common/RecentThings");
	};

	RviLayer.prototype.getRecentItems = function () {
		var that = this;

		if (mLocalStorage) {
			if (this.recentItemsController) {
				this.recentItemsController.getRecentItems(this, function (items) {
					    if (items && items.length > 0) {
							var itemArr = items;
							that.fillData(itemArr.reverse());
						} else {
					    	$("#rvi_lst").find("li").remove();
					    	resetRecent();
					        var isHideLayer = true;
					        if ($(".btn_top").hasClass("type2") == false) {
					            $(".btn_top").addClass("type2");
					        }
					        if ($(".detail_view_wrap").hasClass("type2") == false) {
					            $(".detail_view_wrap").addClass("type2");
					        }
					        if ((typeof GMFooter.isBottomBarType != "undefined" && GMFooter.isBottomBarType == true) || (typeof IsThirdPartyBottomLayer != "undefined" && IsThirdPartyBottomLayer == true)) {
					        	if ($("li.recent_wrap").hasClass("disable") == false) {
					        		$("li.recent_wrap").addClass("disable");
					        	}
					        	$("#rviLayer").show();
					        	isHideLayer = false;
					        }
					        if ($(".btn_top_main").hasClass("type2") == true) {
					            $(".btn_top_main").removeClass("type2");
					        }
					        if (isHideLayer) {
					            $("#rviLayer").hide();
					        }
					    }
				});
			}
			else {
				var gmktRecentItems = mobileLocalStorage.getItem("GMKTRecentItems");
				if (gmktRecentItems) {
					that.fillData(JSON.parse(gmktRecentItems).reverse());
				}
			}
		}
	};

	RviLayer.prototype.removeRecentItems = function () {
		var that = this;

		if (this.recentItemsController) {
			var selectedCount = 0;
			$.each($(".recen_lst .inp_chk"), function () {
				if ($(this).is(":checked")) {
					var goodsCode = $(this).attr("data-goodscode");
					that.recentItemsController.removeRecentItem(goodsCode);
					++selectedCount;
				}
			});

			if (selectedCount == 0) {
				alert("상품을 선택해주세요.");
				return;
			}

			// header에 위치한 최근본상품 삭제
			try {
				headerExtendedLayerController.render();
			} catch (e) { }

			deleteInterPrd();

			InsDateObj = {};
			this.getRecentItems();
		}

	};

	RviLayer.prototype.fillData = function (items) {
		if (items) {
			var renderingItems = [];

			for (var i = 0; i < items.length; ++i) {
				var itemObj = {};
				itemObj.goodsCode = items[i].goodsCode;
				itemObj.vipUrl = VipDomain + "/Item?goodscode=" + items[i].goodsCode;
				itemObj.imageUrl = items[i].imageUrl;
				itemObj.insDate = toInsDateFromExpDate(items[i].expiredDate);

				renderingItems.push(itemObj);
			}

			if (renderingItems && renderingItems.length > 0) {
				this.rviTemplate = Handlebars.compile($(this.templateId).html());
				$(this.templateWrapper).html(this.rviTemplate(renderingItems));

				$("#rviRepImage").attr("src", renderingItems[0].imageUrl);

				//				var that = this;
				//				setTimeout(function () {
				this.$rviWrapper.show();
				if (typeof $(".btn_top_main")[0] !== "undefined") {
					$(".btn_top_main").addClass("type2");
				}
				//				}, 900);
			} else {
				if (typeof $(".btn_top")[0] !== "undefined") {
					$(".btn_top").addClass("type2");
				}
			}
		} else {
			$("#rvi_lst").find("li").remove();
		}
	};

	function addFavorite() {
		var selectedItemNos = "";
		var selectedCount = 0;
		$.each($(".recen_lst .inp_chk"), function () {
			if ($(this).is(":checked")) {
				selectedItemNos += $(this).attr("data-goodscode") + ",";
				++selectedCount;
			}
		});

		if (selectedCount <= 5 && selectedCount > 0) {
			$.ajax({
				type: "GET",
				url: recentItemsLayerDomainUrl + "/Common/AddFavoriteItems",
				dataType: "json",
				data: {
					itemNos: selectedItemNos
				},
				xhrFields: {
					withCredentials: true
				},
				error: function (request, status, error) {
					alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
				},
				success: function (result) {
					if (result != null && result != undefined) {
						if (result.result != null && result.result == 0) {
							pushInterPrd();
						}
						else if (result.result != null && result.result < 0) {
							alert(result.msg);
							if (result.result == -100) {
								location.href = (LoginUrl) ? LoginUrl : "http://mobile.gmarket.co.kr/Login/login";
							}
						}
						else {
							alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
							return;
						}
					}
				}
			});
		}
		else if (selectedCount == 0) {
			alert("상품을 선택해주세요.");
			return;
		}
		else {
			alert("관심상품은 한번에 최대 5개까지 담으실 수 있습니다. 상품을 5개만 선택해주세요");
			return;
		}
	}

	var ShareTo = function (type) {
		var goodsCode = "";
		var selectedCount = 0;
		$.each($(".recen_lst .inp_chk"), function () {
			if ($(this).is(":checked")) {
				goodsCode = $(this).attr("data-goodscode");
				++selectedCount;
			}
		});
		if (selectedCount == 1) {
			if (goodsCode.length != goodsCodeLength) {
				alert("잘못된 상품 번호입니다. 선택된 상품을 삭제 바랍니다.");
				return;
			}
			var snsUrl = VipDomain + "/Item?GoodsCode=" + goodsCode;
			//var snsUrl = "http://mitem.gmarket.co.kr/Item?GoodsCode=" + goodsCode;
			var dataName = "";
			var data = {};

			if (type != MobileSnsUtil.snsType().FACEBOOK) {
				$.ajax({
					type: 'GET',
					dataType: 'json',
					url: recentItemsLayerDomainUrl + "/Search/SearchItem",
					data: {
					    gdNo: goodsCode
					},
					xhrFields: {
						withCredentials: true
					},
					success: function (result) {
						if (result.Item[0] == undefined) {
							alert("상품정보를 불러오지 못하였습니다. 품절되거나 올바른 상품인지 확인 바랍니다.");
							return;
						}
						if (result.Item[0].GoodsName != undefined) {
							dataName = result.Item[0].GoodsName + " ";
						}
						if (result.Item[0].SalePrice != undefined) {
							dataName += dataName + result.Item[0].SalePrice + "원";
						}
						data = { url: snsUrl, name: dataName, title: "", image: result.Item[0].ImageURL };
						sendSns(type, data);
					},
					error: function (result) {
						data = { url: snsUrl, name: "", title: "" };
						sendSns(type, data);
					}
				});
			}
			else {
				snsUrl = VipDomain + "/Item/FaceBook?GoodsCode=" + goodsCode;
				//snsUrl = "http://mitem.gmarket.co.kr/Item/FaceBook?GoodsCode=" + goodsCode;
				data = { url: snsUrl, name: "", title: "" };
				sendSns(type, data);
			}
		}
		else if (selectedCount == 0) {
			alert("상품을 선택해주세요.");
			return;
		}
		else {
			alert("공유하기는 1개 상품씩만 가능합니다. 상품을 1개만 선택해주세요.");
			return;
		}
	}
	RviShare = ShareTo;

	function sendSns(type, data) {
		if (type == "kakaotalk") {
			if (typeof data != "object" || data.name == "") {
				alert("상품정보를 불러올 수 없어 카카오톡 공유하기를 이용하실 수 없습니다. 다시 시도 바랍니다.");
				return;
			}

			kakaoData = {
				label: data.name,
				webLink: {
					text: data.url,
					url: data.url
				},
				image: {
					src: data.image,
					width: 200,
					height: 200
				},
				webButton: {
					text: "꾹! 눌러보아요",
					url: data.url
				},
				fail: function () {
					console.log("ERROR: cannot create KakaoTalk Link on PC"); //PC 일때 발생하는 Error
				}
			};
			Kakao.Link.sendTalkLink(kakaoData);
		}
		else {
			var sender = MobileSnsUtil.sns(type).sender;
			sender.send(type, new SnsParam(type, data));
		}
	}

	function isApp() {
		var c_name = "pcid";
		var appYn = "N";

		if (document.cookie.length > 0) {
			c_start = document.cookie.indexOf(c_name + "=");
			if (c_start != -1) {
				c_start = c_start + c_name.length + 1;
				c_end = document.cookie.indexOf(";", c_start);
				if (c_end == -1) c_end = document.cookie.length;

				if (unescape(document.cookie.substring(c_start, c_end)) == "" ||
					unescape(document.cookie.substring(c_start, c_end)).indexOf("9") == 0) { //SFC 예외처리
					appYn = "N";
				} else {
					appYn = "Y";
				}
			}
		}
		return appYn;
	}

	function ifDateHasNotChanged(insDate, option) {
		if (typeof insDate === 'undefined' || insDate == null || !InsDateObj.month) {
			InsDateObj = insDate;
			return option.inverse(this);
		}
		if (InsDateObj.month == insDate.month && InsDateObj.date == insDate.date && InsDateObj.day == insDate.day)
			return option.fn(this);

		InsDateObj = insDate;
		return option.inverse(this);
	}

	function toInsDateFromExpDate(expireDate) {
		var expDate = new Date(expireDate);

		//반영일 이전 30일, 이후 180일
		if (expDate > chkDate) {//반영일 + 30일
			expDate.setDate(expDate.getDate() - 180);
		} else {
			expDate.setDate(expDate.getDate() - 30);
		}

		var insDate = {};
		insDate.month = expDate.getMonth() + 1;
		insDate.date = expDate.getDate();
		insDate.day = expDate.toDateString().slice(0, 3);

		return insDate;
	}

	var recentLayer = new RviLayer("#rviLayer");
	rviLayer = recentLayer;
});