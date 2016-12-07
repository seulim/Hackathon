$(document).ready(function () {
	if (stuff.getMenuName() === 'home' || stuff.getMenuName() === 'specialshop') {
		resizeScroll();
		if ($(".wrap_xscroll.type_move").size() > 0) initSlideActive();
	}
	else {
		resizeScroll();
	}

	Handlebars.registerHelper('ifOptSellDetailNonusePoint', function (OptSellDetailNonusePoint, opts) {
		if (typeof OptSellDetailNonusePoint === 'undefined' || OptSellDetailNonusePoint === null || OptSellDetailNonusePoint === '')
			OptSellDetailNonusePoint = 0;

		if (parseInt(OptSellDetailNonusePoint) == 100)
			return opts.fn(this);
		else
			return opts.inverse(this);
	});

	if (loadData != null) {
		searchParameters.setRequestShopCategory(loadData.requestShopCategory);
		searchParameters.setPageNo(loadData.requestPageNo);
		displaySearchResultForHisoryback(loadData.result, loadData.itemList, loadData.needMoreButtonDisplay);
	}
	else {
		if (stuff.getMenuName() != 'home' && stuff.getMenuName() != 'specialshop') {
			displaySearchResult();
		}
	}
});

var loadData = history.state;
var isBtnClick = false;

var stuff = (function () {
	var menuName = 'home';
	var loginUrl = CONST_MOBILE_WEB_URL + '/Login/Login?type=old&rtnurl=' + encodeURIComponent(location.href);
	//var itemUrl = 'http://mitem.gmarket.co.kr/Item?goodsCode='
	var isLogin = false;
	var hasBrancCode = false;
	return {
		setMenuName: function (input) {
			menuName = input;
		}
		, getMenuName: function () {
			return menuName;
		}
		, getLoginUrl: function () {
			return loginUrl;
		}
		, setIsLogin: function (input) {
			isLogin = input;
		}
		, getIsLogin: function (input) {
			return isLogin;
		}
		, setHasBranchCode: function (input) {
			hasBrancCode = input;
		}
		, getHasBranchCode: function (input) {
			return hasBrancCode;
		}

	};
})();

var histroyData = {
	shopCategory: ""
	, requestShopCategory: ""
	, requestPageNo: ""
	, itemList: ""
	, needMoreButtonDisplay: ""
    , result: ""
}


var searchParameters = (function () {
	var parameters = {
		primeKeyword: ""
		, shopCategory: ""
		, pageNo: 1
		, pageSize: 50
		, requestShopCategory: ""
		, requestPageNo: 0
	}

	var itemListTotalSize = 0;

	var queryDictionary = (function () {
		var dictionary = {};
		var query = window.location.search.substring(1);
		var vars = query.split("&");
		for (var i = 0; i < vars.length; i++) {
			var pair = vars[i].split("=");
			dictionary[pair[0]] = decodeURIComponent(pair[1]);
		}
		return dictionary;
	})();

	parameters.primeKeyword = queryDictionary.primeKeyword || queryDictionary.topKeyword;
	parameters.shopCategory = queryDictionary.shopCategory;

	return {
		setPrimeKeyword: function (primeKeyword) {
			parameters.primeKeyword = primeKeyword;
		}
		, setShopCategory: function (input) {
			parameters.shopCategory = input;
		}
		, getSearchParameters: function () {
			return $.extend(true, {}, parameters);
		}
		, getDefaultParameters: function () {
			return $.extend(true, {}, queryDictionary);
		}
		, setPageSize: function (input) {
			parameters.pageSize = input;
		}
		, setPageNo: function (input) {
			parameters.pageNo = input;
		}
		, getPageNo: function () {
			return parameters.pageNo;
		}
		, setItemListTotalSize: function (size) {
			itemListTotalSize = size;
		}
		, needMoreButton: function () {
			return parameters.pageSize * parameters.pageNo < itemListTotalSize;
		}
		, getShopCategory: function () {
			return parameters.shopCategory;
		}
		, setRequestShopCategory: function (input) {
			parameters.requestShopCategory = input;
		}
		, getRequestShopCategory: function (input) {
			return parameters.requestShopCategory;
		}
		, setRequestPageNo: function (input) {
			parameters.requestPageNo = input;
		}
		, getRequestPageNo: function (input) {
			return parameters.requestPageNo;
		}

	}
})();


var moreParameters = (function () {
	var parameters = {
		area: "4"
		, pageSeq: 0
		, pageNo: 1
		, pageSize: 20
	}

	var itemListTotalSize = 0;

	return {
		setPageNo: function (pageNo) {
			parameters.pageNo = pageNo;
		}
		, getPageNo: function () {
			return parameters.pageNo;
		}
		, setItemListTotalSize: function (size) {
			itemListTotalSize = size;
		}
		, getMoreParameters: function () {
			return $.extend(true, {}, parameters);
		}
		, setArea: function (area) {
			parameters.area = area;
		}
		, needMoreButton: function () {
			return parameters.pageSize * parameters.pageNo < itemListTotalSize;
		}
		, setPageSeq: function (input) {
			parameters.pageSeq = input;
		}
		, setPageSize: function (input) {
			parameters.pageSize = input;
		}
	}
})();

var setCartLayer = function (name, price, gdno) {
	if (!stuff.getIsLogin()) {
		location.href = stuff.getLoginUrl();
	}
	else {
		if (stuff.getHasBranchCode()) {
			$('#hplus_cart').find('h5.item_tit').text(name);
			$('#hplus_cart').find('strong.sum').text(price);
			$('#hidItemNoToCart').val(gdno);
			getMinBuyCnt(gdno, price);
			popBlockUI('hplus_cart');
		}
		else {
			getMyAddrList();
		}
	}
}

var moreItem = function () {
	var url;
	if (stuff.getMenuName() === 'home') {
		moreParameters.setPageNo(moreParameters.getPageNo() + 1);
		displayMoreItem("/Homeplus/GetHomeSectionItem");
	}
	else if (stuff.getMenuName() === 'specialshop') {
		moreParameters.setPageNo(moreParameters.getPageNo() + 1);
		moreParameters.setPageSize(50);
		displayMoreItem('/Homeplus/GetSpecialShopSectionItem');
	}
	else {
		searchParameters.setPageNo(searchParameters.getPageNo() + 1);
		if (searchParameters.getRequestShopCategory()) {
			if (searchParameters.getRequestShopCategory().length > 0) {
				Router.navigate(searchParameters.getRequestShopCategory() + '/' + searchParameters.getPageNo());
			}
			else {
				Router.navigate(searchParameters.getPageNo());
			}
		}


		displaySearchResult();
	}
}

var moreItemForHistoryBack = function (page) {
	for (var i = 1; i < page; i++) {
		moreItem();
	}
}

var moreItemWithCodeForHistoryBack = function (code) {
	moreItem();
}

/*
var moreItemForHistoryback = function(){
searchParameters.setPageNo(searchParameters.getPageNo() + 1);
Router.navigate(searchParameters.getShopCategory() + '/' + searchParameters.getPageNo());
displaySearchResult(true);
}
*/

var displayMoreItem = function (inputUrl) {
	$.ajax({
		type: 'GET',
		url: inputUrl,
		data: moreParameters.getMoreParameters(),
		//contentType: "application/x-www-form-urlencoded",
		xhrFields: {
			withCredentials: true
		},
		success: function (result) {
			var source = $('#item-template').html();
			var template = Handlebars.compile(source);
			$('.week_bx').find('ul.item_bx').append(template(result));
			if (moreParameters.needMoreButton()) {
				$('div.see_more').show();
			}
			else {
				$('div.see_more').hide();
			}
		}
	});
}

var displaySearchResultWithCode = function (code) {
	searchParameters.setRequestShopCategory(code);
	searchParameters.setPageNo(1);
	displaySearchResult(true);
	isBtnClick = true;
}

var displaySearchAll = function () {
	var url = $('#divCategory').find('li.on').find('a').attr('href');
	location.href = url;
}

var displaySearchResult = function (needEmpty) {
	$.ajax({
		type: 'POST',
		url: "/Homeplus/Search",
		data: searchParameters.getSearchParameters(),
		//contentType: "application/x-www-form-urlencoded",
		xhrFields: {
			withCredentials: true
		},
		success: function (result) {
			searchParameters.setItemListTotalSize(result.TotalGoodsCount);
			if (searchParameters.needMoreButton()) {
				$('div.see_more').show();
			}
			else {
				$('div.see_more').hide();
			}

			var $noData = $('#divNoSearchResult');

			if (result.Item.length > 0) {
				$noData.hide();
				var source = $('#item-template').html();
				var template = Handlebars.compile(source);
				if (needEmpty) {
					$('ul.item_bx').empty().append(template(result));
				}
				else {
					$('ul.item_bx').append(template(result));
				}
			} else {
				if (needEmpty) { $('ul.item_bx').empty(); }
				if ($('ul.item_bx li.item').length <= 0) { $noData.show(); }
			}

			var defaultParameter = searchParameters.getDefaultParameters();
			// SRP
			if (defaultParameter.primeKeyword) {
				var $keywordText = $('#divSearchResultKeyword');
				$keywordText.find('strong').text(defaultParameter.primeKeyword);
				$keywordText.find('em').text(result.TotalGoodsCount);
				$noData.find('span').text(defaultParameter.primeKeyword);
				if (result.TotalGoodsCount > 0) { $keywordText.show(); }
				else { $keywordText.hide(); }
			}
			else {
				var cataegoryCode = searchParameters.getShopCategory();
				var requestCategoryCode = searchParameters.getRequestShopCategory();

				// init
				if ($('#divCategory').find('li.on').length == 0) {
					$('#li_' + cataegoryCode).addClass('on');
					//$('#li_' + cataegoryCode).prependTo('div.cate_m1 ul.cate1');

					if (cataegoryCode.indexOf('6') !== 0) {
						source = $('#sub-category-template').html();
						template = Handlebars.compile(source);
						$('#divSubCategory').append(template(result));
						$('#li_SubAll').addClass('on');
						resizeScroll();
					}
					// ?Œì¹´?Œê³ ë¦¬ë©´ ?ˆë³´?¬ì???
					else {
						$('#divSubCategory').empty();
					}

					if (requestCategoryCode.length > 0) {
						$('#divSubCategory').find('li').removeClass('on');
						$('#sub_li_' + requestCategoryCode).addClass('on');
					}

					if ($(".wrap_xscroll.type_move").size() > 0) initSlideActive();
				}
				// ?œë¸Œ ì¹´í…Œê³ ë¦¬ë¥??Œë???
				else {
					$('#divSubCategory').find('li').removeClass('on');
					$('#sub_li_' + cataegoryCode).addClass('on');
					if (requestCategoryCode.length > 0) {
						$('#sub_li_' + requestCategoryCode).addClass('on');
					}
				}
			}

			histroyData.shopCategory = cataegoryCode;
			histroyData.requestShopCategory = requestCategoryCode;
			histroyData.itemList = $('.week_bx').find('ul.item_bx').html();
			histroyData.requestPageNo = searchParameters.getPageNo();
			histroyData.needMoreButtonDisplay = $('.week_bx').find('div.see_more').css('display');
			histroyData.result = result;

			history.replaceState(histroyData, null, null);
		}
	});
}

var displaySearchResultForHisoryback = function (result, itemList, needMoreButtonDisplay) {
	searchParameters.setItemListTotalSize(result.TotalGoodsCount);
	$('div.see_more').css('display', needMoreButtonDisplay);

	var $noData = $('#divNoSearchResult');

	if (result.Item.length > 0) {
		$noData.hide();

		//var source = $('#item-template').html();
		//var template = Handlebars.compile(source);	
		$('ul.item_bx').append(itemList);

	} else {
		if ($('ul.item_bx li.item').length <= 0) { $noData.show(); }
	}

	var defaultParameter = searchParameters.getDefaultParameters();
	// SRP
	if (defaultParameter.primeKeyword) {
		var $keywordText = $('#divSearchResultKeyword');
		$keywordText.find('strong').text(defaultParameter.primeKeyword);
		$keywordText.find('em').text(result.TotalGoodsCount);
		$noData.find('span').text(defaultParameter.primeKeyword);
		if (result.TotalGoodsCount > 0) { $keywordText.show(); }
		else { $keywordText.hide(); }
	}
	else {
		var cataegoryCode = searchParameters.getShopCategory();
		var requestCategoryCode = searchParameters.getRequestShopCategory();

		// init
		if ($('#divCategory').find('li.on').length == 0) {
			$('#li_' + cataegoryCode).addClass('on');
			//$('#li_' + cataegoryCode).prependTo('div.cate_m1 ul.cate1');

			if (cataegoryCode.indexOf('6') !== 0) {
				source = $('#sub-category-template').html();
				template = Handlebars.compile(source);
				$('#divSubCategory').append(template(result));
				$('#li_SubAll').addClass('on');
				resizeScroll();
			}
			// ?Œì¹´?Œê³ ë¦¬ë©´ ?ˆë³´?¬ì???
			else {
				$('#divSubCategory').empty();
			}

			if (requestCategoryCode.length > 0) {
				$('#divSubCategory').find('li').removeClass('on');
				$('#sub_li_' + requestCategoryCode).addClass('on');
			}

			if ($(".wrap_xscroll.type_move").size() > 0) initSlideActive();
		}
		// ?œë¸Œ ì¹´í…Œê³ ë¦¬ë¥??Œë???
		else {
			$('#divSubCategory').find('li').removeClass('on');
			$('#sub_li_' + cataegoryCode).addClass('on');
			if (requestCategoryCode.length > 0) {
				$('#sub_li_' + requestCategoryCode).addClass('on');
			}
		}
	}
}