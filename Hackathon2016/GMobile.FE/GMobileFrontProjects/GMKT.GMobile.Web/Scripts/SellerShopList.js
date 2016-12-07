$(document).ready(function () {
	//$('#productListView').hide().fadeIn();
	registerLPSRPHelpers();

	var router = new Grapnel();

	router.get(getRouteParam(), function (req) {
		if (params.isAjax) {
			params.scKeyword = req.params.scKeyword || "";
			params.shopCategory = req.params.shopCategory || "";
			params.pageNo = req.params.pageNo ? Number(req.params.pageNo) : 1;
			params.sort = req.params.sort ? Number(req.params.sort) : 1;
			params.lcId = req.params.lcId || "";
			params.mcId = req.params.mcId || "";
			params.scId = req.params.scId || "";
			params.minPrice = req.params.minPrice ? Number(req.params.minPrice) : 0;
			params.maxPrice = req.params.maxPrice ? Number(req.params.maxPrice) : 0;
			params.isExKeyword = req.params.isExKeyword || "N";
			params.isFreeDelivery = req.params.isFreeDelivery || "N";
			params.isMileage = req.params.isMileage || "N";
			params.isDiscount = req.params.isDiscount || "N";
			params.isStamp = req.params.isStamp || "N";
			params.isSmartDelivery = req.params.isSmartDelivery || "N";
			params.listType = req.params.listType || "img";

			getPageList();
		}

		params.listType = req.params.listType;

		$("#selListSort").val(params.sort);
		setDetailSearch();
	});

	updateHashTag(true);

	$('#btnPrev').click(function () {
		location.href = Const.LPSRP_URL + "?category=" + $(this).attr('data-category');
	});

	$("#selListSort").bind('change', function () {
		params.sort = Number($("#selListSort").val());
		updateHashTag(true);
		$(this).blur();
	});

	$("#resetDetail").bind('click', function () {
		resetDetails();
	});

	$("#searchDetail").bind('click', function () {
		detailSearch();
	});
});

var getPageList = function () {
    $("#productListLoading").show();

    if (params.pageNo < 1 || (params.lastPage > 1 && params.pageNo > params.lastPage)) {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/SellerShop/" + params.shopAlias + "/ListPost",
        dataType: "json",
        data: {
            shopCategory: params.shopCategory,
            isShop: params.isShop,
            keyword: params.keyword,
            lcId: params.lcId,
            mcId: params.mcId,
            scId: params.scId,
            scKeyword: params.scKeyword,
            pageNo: params.pageNo,
            pageSize: params.pageSize,
            sort: params.sort,
            alias: params.alias,
            minPrice: params.minPrice,
            maxPrice: params.maxPrice,
            isExKeyword: params.isExKeyword,
            isFreeDelivery: params.isFreeDelivery,
            isMileage: params.isMileage,
            isDiscount: params.isDiscount,
            isStamp: params.isStamp,
            isSmartDelivery: params.isSmartDelivery
        },
        error: function (request, status, error) {
            alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
        },
        success: function (result) {
            displayCategoryLayer(result);
            displayListItems(result);

            $("#productListLoading").hide();
        }
    });
}

var categoryIndex = 0;
var categoryCount = 0;

function displayCategoryLayer(result) {
    var isLP = MENU_NAME == "LP";

    var subCategroyCount = (typeof result.Category !== "undefined" && result.Category != null) ? result.Category.length : 0;

    if (result.TotalGoodsCount > 0 && subCategroyCount == 0)
    	subCategroyCount = 1;

    if (!isLP) {
        /*var searchHeaderTemplate = Handlebars.compile($("#search-header-template").html());
        $("#searchHader").empty().append(searchHeaderTemplate({ keyword: params.keyword, categoryCount: subCategroyCount, totalCount: result.TotalGoodsCount }));
        $("#searchHader").show();*/
    }
    else {
        if (result.CategoryBannerInfo != null && result.CategoryBannerInfo.ImagePath != null && result.CategoryBannerInfo.ImagePath.length > 0) {
            var categoryBannerTemplate = Handlebars.compile($("#category-banner-template").html());

            $("#LpCategoryBanner").empty().append(categoryBannerTemplate({ ImagePath: result.CategoryBannerInfo.ImagePath, ImageLnkUrl: result.CategoryBannerInfo.ImageLnkUrl }));
            $("#LpCategoryBanner").show();
        }
        else
            $("#LpCategoryBanner").hide();
    }

    var totalCountTemplate = Handlebars.compile($("#total-count-template").html());
    $("#btnTotalCount").empty().append(totalCountTemplate({categoryCount: subCategroyCount, goodsCount: result.TotalGoodsCount}));
    
    var categoryHeaderTemplate = Handlebars.compile($("#category-header-template").html());
    $("#categoryHeader").empty().append(categoryHeaderTemplate({ isLP: isLP, data: result }));

    categoryIndex = 0;
    var depth = getCategoryDepth(result);
	categoryCount = (typeof result.Category !== "undefined" && result.Category != null) ? result.Category.length : 0;
	
    if (depth < 3) {
	    var cateTemplate = Handlebars.compile($("#category-template").html());
	    $("#LpCategoryList").empty().append(cateTemplate({ depth: depth, data: result })).show();
	    shopSwipe("LpCategoryList");
    }
    else
        $("#LpCategoryList").empty().hide();
}

function getCategoryListStartTag() {
	var tag = "";

	if (categoryIndex == 0 || categoryIndex % 6 == 0)
		tag = "<ul class='category_list swipe--item'>";

	return new Handlebars.SafeString(tag);
}

function getCategoryListEndTag() {
    var tag = "";
    var compareIndex = categoryIndex + 1;

    if (compareIndex % 6 == 0 || categoryCount == compareIndex)
        tag = "</ul>";

    return new Handlebars.SafeString(tag);
}

function setCategoryIndex() {
    categoryIndex++;
    return "";
}

function getCategoryDepth(result) {
	if (result.LcId == "") return 0;
	else if (result.McId == "") return 1;
	else if (result.ScId == "") return 2;
	else return 3;
}

function selectParent(level, category, isShopCategory) {
    if (IS_SHOP_CATEGORY)
        params.shopCategory = (typeof category !== "undefined") ? category : "";
    else {
        switch (level) {
            case "A": params.lcId = ""; params.mcId = ""; params.scId = ""; break;
            case "L": params.lcId = category; params.mcId = ""; params.scId = ""; break;
            case "M": params.mcId = category; params.scId = ""; break;
            case "S": params.scId = category; break;
        }
    }

    params.pageNo = 1;

	updateHashTag(true);
}

function selectChild(depth, category) {
    params.pageNo = 1;

    if (IS_SHOP_CATEGORY) {
        params.shopCategory = category;
        params.lcId = "";
        params.mcId = "";
        params.scId = "";
    }
    else {
        switch (depth) {
            case 0: params.lcId = category; break;
            case 1: params.mcId = category; break;
            case 2: params.scId = category; break;
            case 3: break;
        }
    }

	updateHashTag(true);
}

function categoryBannerLnk(imageLnkUrl) {
    if (imageLnkUrl != null)
        location.href = imageLnkUrl;
}

function displayListItems(result) {
	params.lastPage = Math.ceil(result.TotalGoodsCount / PAGE_SIZE);

	if (result.TotalGoodsCount > 0) {			
		var listTemplate = Handlebars.compile($("#item-template").html());
		$("#productListWrap").empty().append(listTemplate(result));

		$("#productListWrap").find(".button--favorite").on("click", function (e) {
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
				success: function (response) {
					if (response != null && response != undefined) {
						alert(response.msg);

						if (response.result != null && response.result == -100) {
							location.href = (Const && Const.LOGIN_URL) ? Const.LOGIN_URL : "http://m.gmarket.co.kr/Login/login_mw.asp";
						}
					}
				}
			});
		});

		$("#productListWrap").find(".button--cart").on("click", function (e) {
			e.stopPropagation(); e.preventDefault();

			if (isLogin == false) {
				alert("로그인이 필요합니다.");
				var currentUrl = location.href;
				location.href = (Const && Const.LOGIN_URL) ? Const.LOGIN_URL : "http://m.gmarket.co.kr/Login/login_mw.asp"; ;
				return;
			}

			var $button = $(this);

			var param = { 
				itemNo: $(this).attr("data-goodscode"),
				orderQty: 1,
				isInstantOrder: false
			};

			$.ajax({
				type: 'POST',
				dataType: 'json',
				url: "/Search/GetAddCartResult",
				data: param,
				success: function (result) {
					if (result.success) {
						alert("장바구니 담기 성공");
					} else {
						alert(result.message);
					}
				},
				error: function (result) {
					alert("장바구니 담기에 실패했습니다.");
				}
			});

		});
        paging(true);
	} else {
		var listTemplate = Handlebars.compile($("#no-item-template").html());
		var isLP = MENU_NAME == "LP";
		var data = { isLP: isLP, keyword: params.keyword };
		$("#productListWrap").empty().append(listTemplate(data));
		$("div.shop--list_category").hide();
		$("div.shop-item_filter").hide();
		$("div.shop--search_category").hide();
		paging(false);
	}
	
	changeListTypeStyle(params.listType);	
}

function changeListType(type) {
	params.listType = type;
	updateHashTag(false);
	changeListTypeStyle(type);
}

function changeListTypeStyle(type) {
    $('.filter_view li').removeClass('is-active');

	var $productImg = $('div.product__image')
	var $listResult = $('#productListView');

	if (type == "list") {
	    $('#liViewTypeGallery').addClass('is-active');
		$listResult.attr('data-product-list-view', 'listing_list');
	}
	else {
	    $('#liViewTypeList').addClass('is-active');
		$listResult.attr('data-product-list-view', 'listing_gallery');
	}
	/*$('#productListView').hide().fadeIn({
	    complete: function () {
	        setTimeout(window.scrollTo(0, Cookies.get("sellerShopLpHeight")), 100);
	        Cookies.remove("sellerShopLpHeight");
	    }
	});*/
}

function updateHashTag(isAjax) {	
	params.isAjax = isAjax;
	location.hash = "#" + getHashParam(params);
}

function getHashParam() {	
	var paramArr = [];

	for (var i = 0; i < hashParams.length; i++) {
		paramArr.push(hashParams[i] + "=" + encodeURIComponent(params[hashParams[i]]));
	}	
	return paramArr.join("&");
}

function getRouteParam() {	
	var paramArr = [];

	for (var i = 0; i < hashParams.length; i++) {
		paramArr.push(hashParams[i] + "=:" + hashParams[i] + "?");
	}
	return paramArr.join("&");
}

function setDetailSearch() {
	if (params.isExKeyword == 'Y')
		$('input[name=searchType][value=Y]').prop('checked', true);
	else
		$('input[name=searchType][value=N]').prop('checked', true);

	if (params.isFreeDelivery == 'Y')
		$('#free_delivery').prop('checked', true);
	else
		$('#free_delivery').prop('checked', false);

	if (params.isSmartDelivery == 'Y')
		$('#smart_delivery').prop('checked', true);
	else
		$('#smart_delivery').prop('checked', false);

	if (params.isMileage == 'Y')
		$('#mileage').prop('checked', true);
	else
		$('#mileage').prop('checked', false);

	if (params.isStamp == 'Y')
		$('#stamp').prop('checked', true);
	else
		$('#stamp').prop('checked', false);

	if (params.scKeyword != '')
		$('#scKeyword').val(params.scKeyword);

	if (params.minPrice == 0 && params.maxPrice == 0) {
		$('#minPrice').val('');
		$('#maxPrice').val('');
	} else {
		$('#minPrice').val(params.minPrice);
		$('#maxPrice').val(params.maxPrice);
	}	
}

function detailSearch() {
	if ($('#free_delivery').is(':checked'))
		params.isFreeDelivery = 'Y';
	else
		params.isFreeDelivery = 'N';

	if ($('#smart_delivery').is(':checked'))
		params.isSmartDelivery = 'Y';
	else
		params.isSmartDelivery = 'N';

	if ($('#mileage').is(':checked'))
		params.isMileage = 'Y';
	else
		params.isMileage = 'N';

	if ($('#discount').is(':checked'))
		params.isDiscount = 'Y';
	else
		params.isDiscount = 'N'; 

	if ($('#stamp').is(':checked'))
		params.isStamp = 'Y';
	else
		params.isStamp = 'N';

	params.isExKeyword = $('input[name=searchType]:checked').val();
	params.scKeyword = $('#scKeyword').val();
	params.minPrice = $('#minPrice').val();
	params.maxPrice = $('#maxPrice').val();
	params.pageNo = 1;
	updateHashTag(true);
	$('.dp-filter--detail .ui_detail-search__close').trigger('click');
}

function resetDetails() {
	$('#scKeyword').val('').blur();
	$('span.ui_search-input_has_value').removeClass('ui_search-input_has_value');
	$('#minPrice').val('');
	$('#maxPrice').val('');
	$('#detailSearchLayer input[type=checkbox]').prop('checked', false);
	$('input[name=searchType][value=N]').prop('checked', true);
}

function paging(isShowPagination) {
	var pageModel = {};
	pageModel.pages = [];

	if (isShowPagination) {
		var start, last, halfSize = Math.floor(PAGENATION_SIZE / 2);
		if (params.lastPage < PAGENATION_SIZE) {
			start = 1;
			last = params.lastPage;
		}
		else if (params.pageNo - halfSize < 1) {
			start = 1;
			last = PAGENATION_SIZE;
		}
		else if (params.pageNo + halfSize > params.lastPage) {
			start = params.lastPage - PAGENATION_SIZE + 1;
			last = params.lastPage;
		}
		else {
			start = params.pageNo - halfSize;
			last = params.pageNo + halfSize;
		}
		for (var i = start; i <= last; i++) {
		    var css = (i === params.pageNo) ? "pagination_current" : "";
			pageModel.pages.push({ no: i, cssStyle: css });
		}

        pageModel.prevClass = ((params.pageNo == 1) ? "disable " : "") + "pagination_prev";
        pageModel.nextClass = ((params.pageNo == params.lastPage) ? "disable " : "") + "pagination_next";
        
		var paginationTemplate = Handlebars.compile($("#pagination-template").html());
		$("#paginationWrap").empty().append(paginationTemplate(pageModel));
		$("#paginationWrap").show();
	} else {
		$("#paginationWrap").empty();
		$("#paginationWrap").hide();
	}
}

function getPageListAndGoTop(pageNo) {
	if (pageNo !== params.pageNo && pageNo >= 1 && pageNo <= params.lastPage) {
		params.pageNo = pageNo;
		updateHashTag(true);		
		$('html, body').animate({ scrollTop: $('div.dp-filter').offset().top }, "fast");
	};
	return false;
}

function imageOnErrorHandler($this) {	
	if ($this.attr("src") != $this.data("onerror")) {
		$this.attr("src", $this.data("onerror"));
	}
}

//Handlebar Helpers
function registerLPSRPHelpers() {
	Handlebars.registerHelper('ifOver', ifOver);
	Handlebars.registerHelper('ifFreeDelivery', ifFreeDelivery);
	Handlebars.registerHelper('ifDeliveryShow', ifDeliveryShow);
	Handlebars.registerHelper("getItemPrice", getItemPrice);
	Handlebars.registerHelper("getDeliveryText", getDeliveryText);
	Handlebars.registerHelper("getPurchaseClass", getPurchaseClass);
	Handlebars.registerHelper('ifCartVisible', ifCartVisible);

	Handlebars.registerHelper('ifCatetory', ifCatetory);
	Handlebars.registerHelper("setCategoryIndex", setCategoryIndex);
	Handlebars.registerHelper("getCategoryListStartTag", getCategoryListStartTag);
	Handlebars.registerHelper("getCategoryListEndTag", getCategoryListEndTag);
}


function ifOver(base, val, option) {
	if (val > base)
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifFreeDelivery(shipping, option) {
	if (shipping === '무료배송')
		return option.fn(this);
	else
		return option.inverse(this);
}

function ifDeliveryShow(shipping, option) {
    if (shipping != null && shipping.length > 0)
        return option.fn(this);
    else
        return option.inverse(this);
}

function ifCartVisible(IsCartVisible, option) {
    if (IsCartVisible)
        return option.fn(this);
    else
        return option.inverse(this);
}

function ifCatetory(code, option) {
    if (code != "" && code.length > 0)
        return option.fn(this);
    else
        return option.inverse(this);
}

function getItemPrice(price) {
    var tag = "";
    if (price == "가입상품")
        tag = "<span class='txt'>가입상품</span>";
    else
        tag = price.replace("원", "") + "<span class='unit'>원</span>";

    return new Handlebars.SafeString(tag);
}

function getDeliveryText(item) {
    if (item.Delivery.DeliveryType == 'SMART')
        return new Handlebars.SafeString('<em>스마트</em>배송');
    else
        return new Handlebars.SafeString(item.Delivery.DeliveryText);
}

function getPurchaseClass(item) {
    return new Handlebars.SafeString((item.IsPurchasedSeller) ? ' purchase_seller' : '');
}

function setCookie(cookieName, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var cookieValue = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toGMTString());
    document.cookie = cookieName + "=" + cookieValue;
}

function getCookie(cookieName) {
    cookieName = cookieName + '=';
    var cookieData = document.cookie;
    var start = cookieData.indexOf(cookieName);
    var cookieValue = '';
    if (start != -1) {
        start += cookieName.length;
        var end = cookieData.indexOf(';', start);
        if (end == -1) end = cookieData.length;
        cookieValue = cookieData.substring(start, end);
    }
    return unescape(cookieValue);
}

$(".product-list__item").live("click", function () {
    Cookies.set("sellerShopLpHeight", $(document).scrollTop());
});

function renderSearchTextBox(param) {
    if (param == "SRP" || $("#navi-search-box-template").length > 0) {
        $("#shopKeyword").val(params.keyword);

        var currentKeywordArray = params.keyword.split(" ");
        var newKeywordArray = [];
        for (var i = 0; i < currentKeywordArray.length; i++) {
            if (currentKeywordArray[i] != "") {
                newKeywordArray.push(currentKeywordArray[i]);
            }
        }
        
        var searchTextBoxTemplate = Handlebars.compile($("#navi-search-box-template").html());
        $("#ulSearchTextBox").html(searchTextBoxTemplate(newKeywordArray));
    }

    $(".button-keyword-delete").on("click", function (e) {
    	var $word = $(".keyword-item span");

    	if ($word.length > 1) {
    		var $removedWord = $(this).parent().parent().find("span");

    		var newKeyword = "";
    		$word.each(function (i, n) {
    			if (false === $removedWord.is(n)) {
    				newKeyword += $(n).find(".button-keyword").html() + " ";
    			}
    		});

    		$("#shopKeyword").val(newKeyword.trim());
    		$("#shopSearchForm").submit();
    	}
    	else {
    		$("#shopKeyword").val("");
    		$("#searchKeywordre").trigger("click");
    	}
    });
}