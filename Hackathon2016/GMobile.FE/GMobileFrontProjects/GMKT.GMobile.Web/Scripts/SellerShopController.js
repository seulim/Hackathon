var SellerShopController = function () {
	var that = this;
	this.shopInterestController = new ShopInterestController(Const.ALIAS, Const.SHOP_NAME);

	//this.historyController = new ShopHistoryController(Const.ALIAS);

	//this.mosaicItemController = new MosaicItemController();

	Handlebars.registerHelper('ifIsApp', ifIsApp);
	
	if (Const.BOOKMARK_IMAGE_PATH != "") {
		$("link[rel='apple-touch-icon-precomposed']").attr("href", Const.BOOKMARK_IMAGE_PATH);
	}

	$("._favoriteFunc").on("click", function (e) {
		e.stopPropagation(); e.preventDefault();
		var goodscode = $(this).attr("data-goodscode");
		$.ajax({
			type: "GET",
			url: "/SellerShop/" + Const.ALIAS + "/AddFavoriteItem",
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

SellerShopController.prototype.addFavoriteItem = function (goodscode) {
	$.ajax({
		type: "GET",
		url: "/SellerShop/" + Const.ALIAS + "/AddFavoriteItem",
		dataType: "json",
		data: {
			itemNo: goodscode
		},
		error: function (request, status, error) {
			alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
		},
		success: function (favResult) {
			if (favResult != null && favResult != undefined) {
				alert(favResult.msg);

				if (favResult.result != null && favResult.result == -100) {
					location.href = (Const && Const.LOGIN_URL) ? Const.LOGIN_URL : "http://mobile.gmarket.co.kr/Login/Login";
				}
			}
		}
	});
};

SellerShopController.prototype.addHistoryAndGo = function (actionName, message, url, imageUrl, errorMessage) {
	var NUMBER_OF_HISTORY = 20;

	if (errorMessage) {
		alert(errorMessage);
	} else {
		try {
			if (window.localStorage) {
				var shopHistory = JSON.parse(window.localStorage.getItem("GMKTSellerShopHistory"));

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

				window.localStorage.setItem("GMKTSellerShopHistory", JSON.stringify(shopHistory));
			}
		} catch (e) { }

		location.href = url;
	}
};

var SellerShopHistoryController = function (alias) {
	var $historyWrapper = $("#shopHistoryItem");
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
		//showItems();

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

		$(document).ready(function () {
			if (alias) {
				//if (!historyTemplate) {
				//	historyTemplate = Handlebars.compile($("#history-template").html());
				//}

				var shopHistory = JSON.parse(window.localStorage.getItem("GMKTSellerShopHistory"));

				if (shopHistory != null) {
					var aliasHistory = shopHistory[alias];

					if (!aliasHistory) {
						aliasHistory = [];
					}

					//$historyWrapper.html(historyTemplate({ data: aliasHistory.reverse() }));
					$("#historyCount").text("(" + aliasHistory.length +")");

					// paging initialization
					currentPageCount = 1;
					totalPageCount = Math.ceil($historyWrapper.find(".swipe--item").length);
					shopSwipe('shopHistoryItem');

					$prevButton.on("click", function () { that.prev() });
					$nextButton.on("click", function () { that.next() });

					that.refresh();
				} else {
					//$historyWrapper.html(historyTemplate({ data: null }));
					$prevButton.hide();
					$nextButton.hide();
				}
			}
		});
	} else {
		$historyWrapper.hide();
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

	$addShopInterestWrapper.on("click", function () {
		if (false == isInProgress) {
			//var answer = confirm("미니샵을 관심매장으로 등록하시겠습니까?");

			//if (answer) {
				isInProgress = true;
				$.ajax({
					url: Const && Const.ADD_SHOP_INTEREST ? Const.ADD_SHOP_INTEREST : "http://mobile.gmarket.co.kr/SellerShop/" + alias + "/AddFavoriteShop",
					type: "GET",
					async: true,
					success: function (result) {
						if (result.success) {
							//$addShopInterestWrapper.detach();
							//$tabWrapper.append($delShopInterestWrapper)
							$addShopInterestWrapper.hide();
							$delShopInterestWrapper.show();

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
			//}
		} else {
			alert("처리 중입니다. 잠시만 기다려주세요.");
		}
	});

	$delShopInterestWrapper.on("click", function () {
		if (false == isInProgress) {
			//var answer = confirm("미니샵을 고객님의 관심매장목록에서 삭제하시겠습니까?");

			//if (answer) {
				isInProgress = true;
				$.ajax({
					url: Const && Const.DEL_SHOP_INTEREST ? Const.DEL_SHOP_INTEREST : "http://mobile.gmarket.co.kr/SellerShop/" + alias + "/DelFavoriteShop",
					type: "GET",
					async: true,
					success: function (result) {
						if (result.success) {
							//$tabWrapper.append($addShopInterestWrapper);
							//$delShopInterestWrapper.detach();
							$addShopInterestWrapper.show();
							$delShopInterestWrapper.hide();

							alert("관심매장 등록이 해제 되었습니다.");
						} else {
							alert("해제 중 오류가 발생했습니다.");
						}

						isInProgress = false;
					},
					error: function (result) {
						alert("해제 중 오류가 발생했습니다.");
						isInProgress = false;
					}
				});
			//}
		} else {
			alert("처리 중입니다. 잠시만 기다려주세요.");
		}
	});
};

var DisplayItem = function (dispType, sortType, areaNo, index, title, addYN, areaUseType) {
	this.DispType = dispType;
	this.SortType = sortType;
	this.AreaNo = areaNo;
	this.PageNo = 1;
	this.Index = index;

	this.AddYN = (typeof addYN !== "undefined") ? addYN : "Y";
	this.AreaUseType = (typeof areaUseType !== "undefined") ? areaUseType : 0;

	this.ReqParams = {
		dispType: this.DispType,
		sortType: this.SortType,
		areaNo: this.AreaNo,
		pageNo: this.PageNo,
		addYN: this.AddYN
	};

	this.$moreListWrapper = $("#list_more_" + this.Index);
	this.$displayItemWrapper = $("#displayItem_" + this.Index);

	this.itemDisplayTemplate = Handlebars.compile($(((dispType == 7) ? "#selleritem_pinterest_template" : "#selleritem_template")).html());
	
	var that = this;

	this.$moreListWrapper.on("click", function (e) {
	    //location.href = Const.LPSRP_URL + "?sort=" + that.SortType + "&title=" + encodeURIComponent(title);
	    that.PageNo += 1;
	    that.ReqParams.pageNo += 1;
	    that.getItems();
	});

	$(".btn_top_main").on("click", function (e) {
		e.stopPropagation();
		e.preventDefault();
		$('html, body').animate({ scrollTop: 0 }, "fast");
	});

	this.show();

};

DisplayItem.prototype.show = function () {
	if (ConstItem.IsMain != 1) {
		this.$moreListWrapper.hide();
	}
	else {
		if (this.SortType == 7) {
			this.$moreListWrapper.hide();
		}

		/*if (ConstItem.AddVideoId && this.Index == 0 && ConstItem.MainImageDispType != 5 && ConstItem.MainImageDispType != 6) {
			getYoutubeVodInfo($("#seller_add_video_" + this.Index), ConstItem.AddVideoId, Handlebars.compile($("#add_vod_template").html()));
			$("#seller_add_video_" + this.Index).show();
		}*/
	}

	if (this.DispType == 5) {
		this.itemDisplayMagazineTemplate = Handlebars.compile($("#selleritem_magazine_template").html());
		this.itemDisplayMagazineNewLayerTemplate = Handlebars.compile($("#selleritem_magazine_newlayer_template").html());
	}
}


DisplayItem.prototype.getItems = function () {
    var that = this;

    $.ajax({
        type: "POST",
        url: "/SellerShop/" + Const.ALIAS + "/GetItemDisplay",
        data: that.ReqParams,
        async: true,
        success: function (result) {
            if (result.success) {
                result.displayData.displayItemIndex = that.Index;

                if (that.DispType == 7) {
                    shopPinterest("productPinterestList", $(that.itemDisplayTemplate(pinterViewClass(result))));
                }
                else if (that.DispType == 5) {
                    $("#shopPrevImgSwipe_" + that.Index).html(that.itemDisplayMagazineNewLayerTemplate(result));

                    result.magazineData = that.setMagazineData(result);

                    //that.$displayItemWrapper.html(that.itemDisplayMagazineTemplate(result));
                    that.$displayItemWrapper.append(that.itemDisplayMagazineTemplate(result));

                    if (result.displayData.TotalGoodsCount <= 18) {
                        that.$moreListWrapper.hide();
                    }
                }
                else {
                    that.$displayItemWrapper.append(that.itemDisplayTemplate(result))

                    var itemCount = that.$displayItemWrapper.find("li").length;

                    if (itemCount >= result.displayData.TotalGoodsCount) {
                        that.$moreListWrapper.hide();
                    }
                }

                if (that.DispType == 8)
                    displaySwipeItems(that.$displayItemWrapper.parent());

                /*var $img = $('.lazyImage');
                $img.lazy({
                effect: "fadeIn",
                effectTime: 500,
                threshold: 350,
                attribute: "data-original",
                bind: "event",
                onError: function ($this) {
                if ($this.attr("src") != $this.data("onerror")) {
                $this.attr("src", $this.data("onerror"));
                }
                }
                });*/
            } else {
                alert("상품 정보를 가져오는 도중 오류가 발생했습니다.");
            }
        },
        error: function () {
            alert("상품 정보를 가져오는 중 오류가 발생했습니다.");
        }
    });
};

DisplayItem.NUMBER_OF_MAGAZINE_ITEMS_IN_A_ROW = 3;
DisplayItem.prototype.setMagazineData = function (result) {
	var MagazineData = {};
	MagazineData.ItemList = {};

	if (result.displayData == undefined || result.displayData.ItemList == undefined) return MagazineData;

	ShopItemList[this.Index] = result.displayData.ItemList; //20150515

	var i = 0;
	var listLength = result.displayData.ItemList.length;
	for (var sliceIndex = 0; sliceIndex < listLength; sliceIndex += DisplayItem.NUMBER_OF_MAGAZINE_ITEMS_IN_A_ROW) {
		var ItemList = result.displayData.ItemList.slice(0 + sliceIndex, DisplayItem.NUMBER_OF_MAGAZINE_ITEMS_IN_A_ROW + sliceIndex);
		MagazineData.ItemList[i] = ItemList;
		++i;
	}
	return MagazineData;
};

DisplayItem.prototype.updateHashTag = function (pageNo) {
	if (ConstItem.IsMain == 1) return;

	location.hash = "#/" + this.ReqParams.sortType + "/" + (pageNo || this.ReqParams.pageNo);
};

DisplayItem.prototype.goPage = function(pageNo) {
	if (this.ReqParams.pageNo == pageNo) return;

	this.ReqParams.pageNo = pageNo;

	this.updateHashTag(pageNo);

	$('html, body').animate({ scrollTop: $('div.product-title').offset().top }, "fast");
}


function registerDisplayItemHelpers() {
	Handlebars.registerHelper('ifShowDiscount', ifShowDiscount);
	Handlebars.registerHelper('ifNotMagazineDisplayType', ifNotMagazineDisplayType);
	Handlebars.registerHelper('getLinkUrl', getLinkUrl);
	Handlebars.registerHelper('ifIsNotMain', ifIsNotMain);
	Handlebars.registerHelper('ifIsMoreButton', ifIsMoreButton);
	Handlebars.registerHelper('ifNotMosiacDisplayType', ifNotMosiacDisplayType);
	//Handlebars.registerHelper('printPaging', printPaging);
	Handlebars.registerHelper('ifPinterestBannerType', ifPinterestBannerType);
}

function ifShowDiscount(discountRate, dispType, option) {
	if (typeof discountRate === 'undefined' || discountRate == null || discountRate <= 0)
		return option.inverse(this);

	if (typeof dispType === 'undefined' || dispType == null || dispType == '')
		return option.fn(this); //그외 Case에서도 할인율 노출

	if (dispType != 3) //박스타입이 아닐경우 노출
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifNotMagazineDisplayType(dispType, option) {
	if (typeof dispType === 'undefined' || dispType == null || dispType == '')
		return option.fn(this);

	if (dispType != 5)
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifNotMosiacDisplayType(dispType, option) {
	if (typeof dispType === 'undefined' || dispType == null || dispType == '')
		return option.fn(this);

	if (dispType != 4)
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifPinterestBannerType(dispType, option) {
    if (typeof dispType === 'undefined' || dispType == null || dispType == '')
        return option.fn(this);

    if (dispType == "banner")
        return option.fn(this);
    else
        return option.inverse(this);
}

function ifIsApp(option) {
	if (typeof Const.IS_APP === 'undefined' || Const.IS_APP == null || Const.IS_APP == false)
		return option.inverse(this);
	else
		return option.fn(this);
}

function getLinkUrl(dispType, url) {
	var linkUrl = url;

	if (typeof dispType === 'undefined' || dispType == null || dispType == '')
		return new Handlebars.SafeString(linkUrl);

	if (dispType != 4)
		return new Handlebars.SafeString(linkUrl);


	return "#";
}

//상품전시 영역 페이징 노출 여부 처리
function ifIsNotMain(option) {
	if (typeof ConstItem.IsMain === 'undefined' || ConstItem.IsMain == null || ConstItem.IsMain === '')
		return option.inverse(this);

	if (ConstItem.IsMain != 1) //메인이 아닌경우에 노출
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifIsMoreButton(addYN, option) {
    if (typeof ConstItem.IsMain === 'undefined' || ConstItem.IsMain == null || ConstItem.IsMain === '')
        return option.inverse(this);

    if (addYN == "N")
        return option.fn(this);
    else
        return option.inverse(this);
} 

function printPaging(itemList, totalGoodsCount, pageSize, pageNo, controllerStr) {
	var numOfPagesToShow = 5;

	if (typeof itemList === 'undefined' || itemList == null || itemList.length <= 0) return;

	var totalPage = parseInt(totalGoodsCount / pageSize);
	if (totalGoodsCount % pageSize != 0)
		totalPage++;

	// 화면 첫번째 보이는 페이지 수
	var firstPage = pageNo;
	while (firstPage % numOfPagesToShow != 1 && firstPage > 1)
		firstPage--;

	// 화면 마지막 페이지 수
	var lastPage = pageNo;
	while (lastPage % numOfPagesToShow != 0 && lastPage < totalPage)
		lastPage++;

	var pagingHTML = '<div class="ui_pagination">';

	// 이전 페이지 버튼
	pagingHTML += '<a href="javascript:' + controllerStr + '.goPage(1)" class="ui_pagination__first"><span class="icon icon_arrow-double_prev">처음</span></a>';
	if (pageNo > 1) {
		pagingHTML += '<a href="javascript:' + controllerStr + '.goPage(' + (pageNo - 1) + ')" class="ui_pagination__prev"><span class="icon icon_arrow_prev">이전 상품리스트</span></a>';
	}
	else {
		pagingHTML += '<a class="ui_pagination__prev"><span class="icon icon_arrow_prev">이전 상품리스트 없음</span></a>';
	}

	var count = totalPage > numOfPagesToShow ? numOfPagesToShow : totalPage;

	for (var i = firstPage; i <= lastPage; i++) {
		var css = '';
		if (pageNo == i)
			css = ' ui_pagination__link--current';
		pagingHTML += '<a href="javascript:' + controllerStr + '.goPage(' + i + ');" class="ui_pagination__link' + css + '">' + i + '</a>'
	}

	// 이후 페이지 버튼
	if (totalPage > pageNo) {
		pagingHTML += '<a href="javascript:' + controllerStr + '.goPage(' + (pageNo + 1) + ')" class="ui_pagination__next"><span class="icon icon_arrow_next">다음 상품리스트</span></a>';
	}
	else {
		pagingHTML += '<a class="ui_pagination__next"><span class="icon icon_arrow_next">다음 상품리스트 없음</span></a>';
	}
	pagingHTML += '<a href="javascript:' + controllerStr + '.goPage(' + totalPage + ')" class="ui_pagination__end"><span class="icon icon_arrow-double_next">마지막</span></a>';

	pagingHTML += '</div>';

	return new Handlebars.SafeString(pagingHTML);
}


function getYoutubeVodInfo($addVideoWrapper, videoId, template) {
	$.ajax({
		type: "POST",
		url: "/SellerShop/" + Const.ALIAS + "/GetYoutubeVodInfo?vodKey=" + videoId,
		data: { vodKey: videoId },
		async: true,
		success: function (result) {
			if (result) {
				if (result.vodInfo != undefined) {
					$addVideoWrapper.html(template(result.vodInfo));
				}
			}
		}
	});
}

var MenuPageController = function (menuNo, menuUseType, sellerId, shopDispNo, directNoticeSeq) {

	registerMenuPageHelpers();
	this.getMenuContentByUseType(menuNo, menuUseType, sellerId, shopDispNo, directNoticeSeq);
}

MenuPageController.prototype.getMenuContentByUseType = function (menuNo, menuUseType, sellerId, shopDispNo, directNoticeSeq) {

	$(".shop--menu-list li").removeClass("on");
	$(".shop--menu-list li[id=" + menuNo + "]").addClass("on");

	if (menuUseType == 3)//공지사항
	{
		if (directNoticeSeq > 0) {
			this.getNoticeDetailAjax(directNoticeSeq);
		} else {
			this.getNoticeListAjax(Const.NOTICE_PAGENO, Const.NOTICE_PAGESIZE);
		}
	}
	else if (menuUseType == 2 || menuUseType == 5)//상품전시, 기획전 (image or html)
	{
		this.getMenuContentAjax(sellerId, shopDispNo, menuNo);
	}
}

MenuPageController.prototype.setDisplayByContentType = function (ajaxresult, contentType) {
	//contentType : noticelist, noticedetail, eventoritem (html, image, item)

	$("#noticeDetail_wrapper").hide();
	$("#noticeList_wrapper").hide();
	$("#noticeDetailHeader").hide();
	$("#item_wrapper").hide();
	$("#event_wrapper").hide();
	$("#eventImageDiv").hide();
	$("#eventHtml_wrapper").hide();

	if (contentType == "noticelist") {
		Const.NOTICE_PAGENO = ajaxresult.PageNo;
		Const.NOTICE_PAGESIZE = ajaxresult.PageSize;
		var template = Handlebars.compile($('#noticeList_template').html());
		$("#noticeList_wrapper").html(template(ajaxresult));
		$("#noticeList_wrapper").show();
	}
	else if (contentType == "noticedetail") {
		var template = Handlebars.compile($('#noticeDetail_template').html());
		$("#noticeDetail_wrapper").html(template(ajaxresult));
		$("#noticeDetail_wrapper").show();
		$("#noticeDetailHeader").show();
	}
	else if (contentType == "eventoritem") {
		if (ajaxresult.AreaUseType == 1) //item
		{
			var itemDispType = ajaxresult.ItemDispType;
			var itemSortType = ajaxresult.ItemSortType;
			var itemBgColorCd = (typeof ajaxresult.ItemBgColorCd !== "undefined" && ajaxresult.ItemBgColorCd != null) ? ajaxresult.ItemBgColorCd.replace("#", "") : "";
			var itemAreaNo = ajaxresult.AreaNo;
			var title = ajaxresult.ItemTitle;
			var loadUrl = Const.MENU_ITEMPARTIAL_URL + "/ItemDisplayFromMenu?dispType=" + itemDispType + "&sortType=" + itemSortType + "&areaNo=" + itemAreaNo + "&title=" + title + "&isMain=0&itemBgColorCd=" + itemBgColorCd;

			$("#item_wrapper").show();
			$("#item_wrapper").load(loadUrl);

			if (itemDispType == 8)
			    displaySwipeItems($('[data-product-list-view="rolling"]'));

			location.hash = "#/" + itemSortType + "/" + 1;
		}
		else if (ajaxresult.AreaUseType == 2)//image
		{
			$("#eventImageDiv img").attr("src", ajaxresult.ImageUrl);
			$("#eventImageDiv img").on('click', function () { location.href = ajaxresult.Link });
			$("#eventImageDiv").show();
			$("#event_wrapper").show();
		}
		else if (ajaxresult.AreaUseType == 4)//html
		{
			$("#eventHtml_wrapper").html(ajaxresult.Html);
			$("#eventHtml_wrapper").show();
			$("#event_wrapper").show();
		}
	}
}

MenuPageController.prototype.getMenuContentAjax = function (sellCustNo, shopDispNo, menuNo) {
	var that = this;
		$.ajax({
			url: Const.MENU_CONTENT_URL,
			type: "POST",
			data: { "sellCustNo": sellCustNo, "shopDispNo": shopDispNo, "menuNo": menuNo },
			success: function (result) {
				if (result.success) {
					that.setDisplayByContentType(result.menu, "eventoritem");
				} else {
					alert("메뉴 정보를 가져오던 중에 오류가 발생했습니다.");

				}
			},
			error: function (result) {
				alert("메뉴 정보을 가져오던 중에 오류가 발생했습니다.");
			}
		});
	}

MenuPageController.prototype.goPage = function (pageNo) {
	this.getNoticeListAjax(pageNo, Const.NOTICE_PAGESIZE);
}

MenuPageController.prototype.getNoticeListAjax = function (pageNo, pageSize) {
	var that = this;
		$.ajax({
			url: Const.NOTICE_LIST_URL,
			type: "POST",
			data: { "pageNo": pageNo, "pageSize": pageSize },
			success: function (result) {
				if (result.success) {
					that.setDisplayByContentType({ List: result.noticeList, TotalCount: result.noticeTotalCount, PageNo: pageNo, PageSize: pageSize }, "noticelist");
				} else {
					alert("공지사항을 가져오던 중에 오류가 발생했습니다.");
				}
			},
			error: function (result) {
				alert("공지사항을 가져오던 중에 오류가 발생했습니다.");
			}
		});
	}

MenuPageController.prototype.getNoticeDetailAjax = function (noticeSeq) {
	var that = this;
		$.ajax({
			url: Const.NOTICE_DETAIL_URL,
			type: "POST",
			data: { "noticeSeq": noticeSeq },
			success: function (result) {
				if (result.success) {
					that.setDisplayByContentType(result.notice, "noticedetail");
				} else {
					alert("공지사항 상세내용을 가져오던 중에 오류가 발생했습니다.");
				}
			},
			error: function (result) {
				alert("공지사항 상세내용을 가져오던 중에 오류가 발생했습니다.");
			}
		});
	}

function registerMenuPageHelpers() {
	Handlebars.registerHelper('ifNoNotice', function (val, options) {
		if (val.length <= 0) return options.fn(this);
		else return options.inverse(this);
	});

	Handlebars.registerHelper('getNoticeDetailHref', getNoticeDetailHref);
	Handlebars.registerHelper('getNoticeListHref', getNoticeListHref);
	Handlebars.registerHelper('printPaging', printPaging);
}

function getNoticeDetailHref(noticeSeq) {
	var getNoticeDetailScript = "javascript:menuPageController.getNoticeDetailAjax(" + noticeSeq + ")";
	return new Handlebars.SafeString(getNoticeDetailScript);
}

function getNoticeListHref(currentPageNo) {
	var getNoticeListString = "javascript:menuPageController.getNoticeListAjax(" + Const.NOTICE_PAGENO + "," + Const.NOTICE_PAGESIZE + ")";
	return new Handlebars.SafeString(getNoticeListString);
}

function registerNavigationHelpers() {
	Handlebars.registerHelper('ifNot', ifNot);
	Handlebars.registerHelper('ifOpenUl', ifOpenUl);
	Handlebars.registerHelper('ifCloseUl', ifCloseUl);

	Handlebars.registerHelper('getCategoryLink', getCategoryLink);	
}

function ifNot(val, option) {
	if (!val)
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifOpenUl(index, option) {
	if (index % 3 == 0)
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifCloseUl(index, option) {
	if (index % 3 == 2)
		return option.fn(this);
	else
		return option.inverse(this);
}

function getCategoryLink(isLeaf, categoryId, isShopCategory) {
	if (lsLeaf) { 
		return getLPUrl(categoryId, isShopCategory);
	}
		
	return Handlebars.SafeString(url);
}

var Navigation = function (alias, level) {
	this.alias = alias;
	this.headerTemplate = Handlebars.compile($("#navi-category-header-template").html());
	this.categoryTemplate = Handlebars.compile($("#navi-category-template").html());
	this.$headerWrapper = $("#hcatewrap");
	this.$categoryWrapper = $("#catewrap");
	this.currentId = "";
	this.currentNm = "";
	this.level = level;
	this.lastCategoryInfos = [];
}

Navigation.prototype.setCurrent = function (categoryId, categoryNm, level) {
	this.currentId = categoryId;
	this.currentNm = categoryNm;
	this.level = level;
}

Navigation.prototype.getCurrent = function () {	
	return { id: this.currentId, nm: this.currentNm };
}

Navigation.prototype.fetch = function () {
    var _this = this;
    var alias = this.alias;

    $.ajax({
        type: "POST",
        url: "/SellerShop/" + alias + "/GetCategories",
        async: false,
        cache: false,
        data: {
            categoryId: this.currentId,
            categoryNm: this.currentNm,
            level: this.level
        },
        dataType: 'json',
        success: function (data) {
            _this.render(data);
        },
        error: function (msg) { console.log(msg); alert(msg); return false; }
    });
}

Navigation.prototype.render = function (data) {
	data.Alias = this.alias;

	this.$headerWrapper.html(this.headerTemplate(data));
	this.$categoryWrapper.html(this.categoryTemplate(data));
}

function goToLP(alias, categoryId) {
	var url = "/SellerShop/" + alias + "/List?";

	if (categoryId !== "") url += "category=" + categoryId;	

	location.href = url;
}

function childOrLP(isLeaf, alias, categoryId, categoryNm, level) {
	navCtrl.lastCategoryInfos.push({ categoryId: navCtrl.currentId, categoryNm: navCtrl.currentNm, level: navCtrl.level });
//	$('#catewrap').data('lastCategoryInfo', (navCtrl.currentId || '') + '|' + (navCtrl.currentNm || '') + '|' + (navCtrl.level || ''));
	if (isLeaf) {
		goToLP(alias, categoryId);
	} else {
		navCtrl.setCurrent(categoryId, categoryNm, level);
		navCtrl.fetch();
	}
}

function goToParent() {
	var data = navCtrl.lastCategoryInfos.pop();
	if (data) {
		navCtrl.setCurrent(data.categoryId, data.categoryNm, data.level);
	}
	else {
		navCtrl.setCurrent("", "", "");
	}
	navCtrl.fetch();
}

function goBack() {
	if (Const.IS_APP) {
		location.href = "gmarket://webview?action=goback";
	} else {
		history.back();
	}
}

function addToHome(btn) {
	var closeBtn = $(btn).next(),
		confirmMsg = $('#addToHomeConfirm'),
		completeMsg = $('#addToHomeComplete');

	var scheme = "gmarket://createWebShortcut?name=" + encodeURIComponent(Const.SHOP_NAME) + "&image=" + encodeURIComponent(Const.BOOKMARK_IMAGE_PATH) + "&url=" + encodeURIComponent(Const.SHORTCUT_URL);
	location.href = scheme;

	confirmMsg.hide();
	completeMsg.show();
	window.setTimeout(function () {
		closeBtn.click();
		confirmMsg.show();
		completeMsg.hide();
	}, 500);
}

////////////////// SNS 연동 /////////////////////////////
function ShareSnSTo(type) {
    if (type == "kakaotalk") {
        KakaoController('#SnsKakaoTalk', new SnsParam(type, snsData), 'home'); 
    }

    else {
        var sender = MobileSnsUtil.sns(type).sender;
        sender.send(type, new MinShopSnsParam(type, snsData));
    }
}

var MinShopSnsParam = function (snsType, data) {
    switch (snsType) {
        case MobileSnsUtil.snsType().TWITTER:
            return {
                source: "webclient",
                text: data.name + " " + data.text + " " + data.url
            };
        case MobileSnsUtil.snsType().FACEBOOK:
            return {
                u: data.url,
                t: data.name + " " + data.text + " " + data.url
            };
        case MobileSnsUtil.snsType().KAKAOTALK:
            return {
                msg: data.text,
                url: data.url,
                appid: "gmarket",
                appver: "0.1",
                type: "link",
                appname: "G마켓",
                name: data.name
            };
        case MobileSnsUtil.snsType().KAKAOSTORY:
            return {
                post: data.text + " " + data.url,
                appid: "gmarket",
                appver: "0.1",
                apiver: "1.0",
                appname: "G마켓",
                urlinfo: JSON.stringify({
                    title: data.name,
                    desc: data.text,
                    //imageurl: [data.image],
                    type: "website"
                })
            };
        case MobileSnsUtil.snsType().LINE:
            return {
                message: data.text + " " + data.url
            };
    }
}

var _LineAppSender = function () {
    var thisObject = new _MobileSnsAppSender();

    thisObject.send = function (snsType, params) {
        var targetSns = MobileSnsUtil.sns(snsType);
        var mobileOS = this.getMobileOS();

        var isAndroidWeb = (mobileOS == "android" && MobileSnsUtil.getAgentType() == "");

        var fullUrl = targetSns.baseUrl + this.serialize(params);
        var installUrl;
        if (mobileOS === "ios") {
            installUrl = targetSns.store.ios;
        } else if (mobileOS === "android") {
            installUrl = targetSns.store[mobileOS].intentPrefix + this.serialize(params) + targetSns.store[mobileOS].intentSuffix;
        }

        var install = function () {
            location.href = installUrl;
        };

        if (!this.isMobileBrowser()) {
            alert(targetSns.message["pc"]);
            return false;
        }

        setTimeout(install, 2000);
        if (!isAndroidWeb) {
            location.href = fullUrl;
        }
    };

    thisObject.serialize = function (params) {
        return encodeURIComponent(params.message);
    }

    return thisObject;
};
////////////////// SNS 연동 /////////////////////////////

////////////////// 핀터레스트형 /////////////////////////////
var pinterViewClass = function (result) {
    var newItemList = new Array();
    var isBanner = (typeof result.displayData.PinterestBanner !== "undefined" && result.displayData.PinterestBanner != null && typeof result.displayData.PinterestBanner.ImagePath !== "undefined" && result.displayData.PinterestBanner.ImagePath != null) ? true : false;
    var bannerIndex = Math.floor(Math.random() * 3) + 2;

    $.each(result.displayData.ItemList, function (idx, item) {
        var randomValue = Math.floor(Math.random() * 100);
        var viewClass = (randomValue > 40) ? " is-vertical" : "";
        item.ViewClass = viewClass;
        item.Type = "goods";

        if (isBanner && bannerIndex == idx)
            newItemList.push({ ImagePath: result.displayData.PinterestBanner.ImagePath, ImageLnkUrl: result.displayData.PinterestBanner.ImageLnkUrl, Type: "banner" });

        newItemList.push(item);
    });

    result.displayData.ItemList = newItemList;
    return result;
}

//핀터레스트형
function shopPinterest(id, element) {
    var root = $('#' + id);

    if (!root.data('masonry')) {
        root.append(element).masonry({
            itemSelector: '.product-list__item',
            containerStyle: null,
            hiddenStyle: { opacity: 0 }
        });
    } else {
        root.append(element).masonry('appended', element).masonry();
    }
}
////////////////// 핀터레스트형 /////////////////////////////