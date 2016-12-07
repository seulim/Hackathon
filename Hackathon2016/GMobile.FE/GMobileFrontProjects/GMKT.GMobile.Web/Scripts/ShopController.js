var ShopController = function () {
	var that = this;
		
	this.myShopTemplate = Handlebars.compile($("#myshop_template").html());

	this.tabController = new TabController("#shop_info");

	this.tabController.add("tab_history", new TabContents("#tabcontents_history"), "#tab_history", new TabIndicator("#tab_indicator_history"));

	var myShopAjaxOption = {
		type: "POST",
		url: (Const && Const.MY_SHOP_URL) ? Const.MY_SHOP_URL : "http://mobile.gmarket.co.kr/Shop/" + Const.ALIAS + "/GetMyShoppingInfo",
		data: null,
		async: true,
		success: function (result) {
			if (result.success) {
				$("#tabcontents_myshop").html(that.myShopTemplate(result.result));
			} else {
				alert("나의쇼핑 정보를 가져오는 중 오류가 발생했습니다.");
			}
		},
		error: function () {
			alert("나의쇼핑 정보를 가져오는 중 오류가 발생했습니다.");
		}
	};
	this.tabController.add("tab_myshop", new AjaxTabContents("#tabcontents_myshop", myShopAjaxOption), "#tab_myshop", new TabIndicator("#tab_indicator_myshop"));
	this.tabController.add("tab_sellerinfo", new TabContents("#tabcontents_sellerinfo"), "#tab_sellerinfo", new TabIndicator("#tab_indicator_sellerinfo"));

	var that = this;
	$("#tab_contents_wrapper_close").on("click", function () {
		that.tabController.hideAll();
	});

	this.shopInterestController = new ShopInterestController(Const.ALIAS, Const.SHOP_NAME);

	this.historyController = new ShopHistoryController(Const.ALIAS);

	this.mosaicItemController = new MosaicItemController();

	$("._favoriteFunc").on("click", function (e) {
		e.stopPropagation(); e.preventDefault();
		var goodscode = $(this).attr("data-goodscode");
		$.ajax({
			type: "GET",
			url: "/Shop/" + Const.ALIAS + "/AddFavoriteItem",
			dataType: "json",
			data: {
				itemNo: goodscode
			},
			error: function (request, status, error) {
				alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
			},
			success: function (result) {
				if (result != null && result != undefined) {
					alert(result.msg);

					if (result.result != null && result.result == -100) {
						location.href = (Const && Const.LOGIN_URL) ? Const.LOGIN_URL : "http://m.gmarket.co.kr/Login/login_mw.asp";
					}
				}
			}
		});
	});
};

ShopController.prototype.addHistoryAndGo = function (actionName, message, url, imageUrl, errorMessage) {
	var NUMBER_OF_HISTORY = 20;
	
	if (errorMessage) {
		alert(errorMessage);
	} else {
		try {
			if (window.localStorage) {
				var shopHistory = JSON.parse(window.localStorage.getItem("GMKTShopHistory"));

				if (shopHistory == null) {
					shopHistory = {};
				}
			
				var aliasHistory = shopHistory[Const.ALIAS];

				if (!aliasHistory) {
					aliasHistory = [];
				}

				var thisHistory = {
					Alias: Const.ALIAS,
					ActionName: actionName,
					Message: message,
					Url: url,
					ImageUrl: imageUrl
				};

				if (aliasHistory.length <= 0 || thisHistory.Url != aliasHistory[aliasHistory.length - 1].Url) {
					aliasHistory.push(thisHistory);
				}
			
				if (aliasHistory.length > NUMBER_OF_HISTORY) {
					var howMany = aliasHistory.length - NUMBER_OF_HISTORY;
					aliasHistory.splice(0, howMany);
				}

				shopHistory[Const.ALIAS] = aliasHistory;

				window.localStorage.setItem("GMKTShopHistory", JSON.stringify(shopHistory));
			}
		} catch (e) { }

		location.href = url;
	}
};




var TabController = function (tabContentsWrapperSelector) {
	this.currentTabName = null;
	this.tabContentsList = {};
	this.tabIndicatorList = {};
	this.$tabContentsWrapper = $();

	if (tabContentsWrapperSelector) { 
		this.$tabContentsWrapper = $(tabContentsWrapperSelector);
	}
};

TabController.prototype.add = function (tabContentsName, tabContents, tabButtonSelector, tabIndicator) {
	if (tabContentsName && tabContents) {
		this.tabContentsList[tabContentsName] = tabContents;
	}

	if (tabButtonSelector) {
		var that = this;
		$(tabButtonSelector).on("click", function () {
			var href = $(this).attr("href");
			if (href.indexOf("http") < 0) {
				that.show(tabContentsName);	
			}
		});
	}

	if (tabIndicator) {
		this.tabIndicatorList[tabContentsName] = tabIndicator;
	}
};

TabController.prototype.get = function (tabContentsName) {
	if (this.tabContentsList[tabContentsName]) {
		return this.tabContentsList[tabContentsName];
	} else {
		return null;
	}
}

TabController.prototype.show = function (tabContentsName) {
	if (thisTabContents = this.tabContentsList[tabContentsName]) {
		this.hideAll();

		this.$tabContentsWrapper.show();

		thisTabContents.show();

		if (this.tabIndicatorList[tabContentsName]) {
			this.tabIndicatorList[tabContentsName].on();
		}

		this.currentTabName = tabContentsName;
	}
};

TabController.prototype.hideAll = function () {
	for (var key in this.tabContentsList) {
		this.tabContentsList[key].hide();

		if (this.tabIndicatorList[key]) {
			this.tabIndicatorList[key].off();
		}
	}
};



var TabContents = function (wrapperSelector) {
	this.$wrapper = null;

	if (wrapperSelector) {
		this.$wrapper = $(wrapperSelector);
	}
};

TabContents.prototype.show = function () {
	if (this.$wrapper) this.$wrapper.show();
};

TabContents.prototype.hide = function () {
	if (this.$wrapper) this.$wrapper.hide();
};



var AjaxTabContents = function (wrapperSelector, jQueryAjaxOptions) {
	TabContents.apply(this, [wrapperSelector]);

	this.isRendered = false;
	this.jQueryAjaxOptions = null;

	if (jQueryAjaxOptions) {
		this.jQueryAjaxOptions = jQueryAjaxOptions;
	}
};

AjaxTabContents.prototype = new TabContents();
AjaxTabContents.prototype.constructor = AjaxTabContents;
AjaxTabContents.prototype.parent = TabContents.prototype;

AjaxTabContents.prototype.show = function () {
	if (false == this.isRendered) {
		var that = this;
		that.parent.show.apply(that);
		$.ajax(this.jQueryAjaxOptions).done(function () {
			that.isRendered = true;
		});
	} else {
		this.parent.show.apply(this);
	}
};


var TabIndicator = function (indicatorSelector, classToBeAdded) {
	this.$indicator = null;
	this.classToBeAdded = "on";

	if (indicatorSelector) {
		this.$indicator = $(indicatorSelector);
	}
	if (classToBeAdded) {
		this.classToBeAdded = classToBeAdded;
	}
};

TabIndicator.prototype.on = function () {
	if (this.$indicator && this.classToBeAdded) {
		this.$indicator.addClass(this.classToBeAdded);
	}
};

TabIndicator.prototype.off = function () {
	if (this.$indicator && this.classToBeAdded) {
		this.$indicator.removeClass(this.classToBeAdded);
	}
};




var MosaicItemController = function () {
	// touch input이 있는 device
	if ('ontouchstart' in window) {
		$(".mosaic").on("click", function () {
			if ($(this).hasClass("over")) {
				$(this).removeClass("over");
			} else {
				$(this).addClass("over");
			}
		});
	} else { // touch input이 없는 device
		$(".mosaic_for_mouse").on("mouseover", function () {
			$(this).parent().addClass("over");
		});
		$(".mosaic_for_mouse").on("mouseout", function () {
			$(this).parent().removeClass("over");
		});
	}
}



var ShopHistoryController = function (alias) {
	var $historyWrapper = $("#history_wrapper");
	var $prevButton = $("#history_prev");
	var $nextButton = $("#history_next");
	var historyTemplate = null;

	var currentPageCount = -1;
	var totalPageCount = -1;
	var PAGE_SIZE = 3;

	var that = this;

	this.prev = function () {
		if (currentPageCount > 1) {
			currentPageCount--;
		}

		this.refresh();
	};

	this.next = function () {
		if (currentPageCount < totalPageCount) {
			currentPageCount++;
		}

		this.refresh();
	};

	this.refresh = function () {
		showItems();

		this.refreshButtons();
	};

	this.refreshButtons = function () {
		if (currentPageCount <= 1) {
			$prevButton.hide();
		} else {
			$prevButton.show();
		}

		if (currentPageCount >= totalPageCount) {
			$nextButton.hide();
		} else {
			$nextButton.show();
		}
	};

	function showItems() {
		var $items = $historyWrapper.children();

		if (currentPageCount > totalPageCount) {
			currentPageCount = totalPageCount;
		}

		var $willShowItems = $();
		var startIndex = (currentPageCount - 1) * PAGE_SIZE;
		var endIndex = startIndex + PAGE_SIZE;
		if ($items.length < endIndex) {
			endIndex = $items.length;
		}

		$items.hide();

		for (var i = startIndex; i < endIndex; i++) {
			$willShowItems.push($items[i]);
		}

		$willShowItems.show();
	}

	if (window.localStorage) {
		Handlebars.registerHelper("historyActionName", function (actionName) {
			if (actionName.toLowerCase() == "search") {
				return "검색결과";
			} else if (actionName.toLowerCase() == "list") {
				return "카테고리";
			} else {
				return actionName;
			}
		});

		Handlebars.registerHelper("historyMessage", function (actionName, message) {
			if (actionName.toLowerCase() == "search") {
				return "(" + message + ")로 검색한 결과";
			} else {
				return message;
			}
		});

		$(document).ready(function () {
			if (alias) {
				if (!historyTemplate) {
					historyTemplate = Handlebars.compile($("#history_template").html());
				}

				var shopHistory = JSON.parse(window.localStorage.getItem("GMKTShopHistory"));

				if (shopHistory != null) {
					var aliasHistory = shopHistory[alias];

					if (!aliasHistory) {
						aliasHistory = [];
					}	

					$historyWrapper.html(historyTemplate({ data: aliasHistory.reverse() }));

					// paging initialization
					currentPageCount = 1;
					totalPageCount = Math.ceil($historyWrapper.children().length / 3);

					$prevButton.on("click", function () { that.prev() });
					$nextButton.on("click", function () { that.next() });

					that.refresh();
				} else {
					$historyWrapper.html(historyTemplate({ data: null }));					
					$prevButton.hide();
					$nextButton.hide();
				}
			}
		});
	} else {
		$("#history_wrapper").hide();
		$("#history_fallback").show();
		$prevButton.hide();
		$nextButton.hide();
	}
};



var ShopInterestController = function (alias, shopName) {
	var $addShopInterestWrapper = $("#add_shop_interest");
	var $delShopInterestWrapper = $("#del_shop_interest");

	var $tabWrapper = $("#tab_wrapper");

	var isInProgress = false;

	$addShopInterestWrapper.find("a").on("click", function () {
		if (this.href.indexOf("http") < 0) {
			if (false == isInProgress) {
				var answer = confirm("미니샵을 관심매장으로 등록하시겠습니까?");

				if (answer) {
					isInProgress = true;
					$.ajax({
						url: Const && Const.ADD_SHOP_INTEREST ? Const.ADD_SHOP_INTEREST : "http://mobile.gmarket.co.kr/Shop/" + alias + "/AddFavoriteShop",
						type: "GET",
						async: true,
						success: function (result) {
							if (result.success) {
								$addShopInterestWrapper.detach();
								$tabWrapper.append($delShopInterestWrapper)

								alert("관심매장으로 등록되었습니다.");
							} else {
								if (result.message) {
									alert(result.message)
								} else {
									alert("관심매장 등록 중 오류가 발생했습니다.");
								}
							}

							isInProgress = false;	
						},
						error: function (result) {
							alert("관심매장 등록 중 오류가 발생했습니다.");
							isInProgress = false;
						}
					});
				}
			} else {
				alert("처리 중입니다. 잠시만 기다려주세요.");
			}
		}
	});

	$delShopInterestWrapper.find("a").on("click", function () {
		if (this.href.indexOf("http") < 0) {
			if (false == isInProgress) {
				var answer = confirm("미니샵을 고객님의 관심매장목록에서 삭제하시겠습니까?");

				if (answer) {
					isInProgress = true;
					$.ajax({
						url: Const && Const.DEL_SHOP_INTEREST ? Const.DEL_SHOP_INTEREST : "http://mobile.gmarket.co.kr/Shop/" + alias + "/DelFavoriteShop",
						type: "GET",
						async: true,
						success: function (result) {
							if (result.success) {
								$tabWrapper.append($addShopInterestWrapper);
								$delShopInterestWrapper.detach();

								alert("삭제되었습니다.");	
							} else {
								alert("삭제 중 오류가 발생했습니다.");
							}
							
							isInProgress = false;
						},
						error: function (result) {
							alert("삭제 중 오류가 발생했습니다.");
							isInProgress = false;
						}
					});
				}
			} else {
				alert("처리 중입니다. 잠시만 기다려주세요.");
			}
		}
	});
};