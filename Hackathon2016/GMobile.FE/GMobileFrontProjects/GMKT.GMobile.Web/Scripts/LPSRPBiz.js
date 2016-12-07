function registerHelpers() {
	// 조건 helper
    Handlebars.registerHelper('ifNotZero', ifNotZero);
    Handlebars.registerHelper('ifNotZeroForItemTemplate', ifNotZeroForItemTemplate);
	Handlebars.registerHelper('ifNotEmptyArray', ifNotEmptyArray);
	Handlebars.registerHelper('ifValidPrice', ifValidPrice);
	Handlebars.registerHelper('hasMorePP', hasMorePP);
	Handlebars.registerHelper('ifOverOne', ifOverOne);
	Handlebars.registerHelper('ifDisplayPlusIcon', ifDisplayPlusIcon);
	Handlebars.registerHelper('ifDisplayCategoryList', ifDisplayCategoryList);
	Handlebars.registerHelper('ifDisplayGroupTitle', ifDisplayGroupTitle);
	Handlebars.registerHelper('ifDisplayAdIcon', ifDisplayAdIcon);
	Handlebars.registerHelper('ifDisplayBrandUl', ifDisplayBrandUl);
	Handlebars.registerHelper('ifDisplayCategoryMore', ifDisplayCategoryMore);
	Handlebars.registerHelper('ifDisplayBrandName', ifDisplayBrandName);
	Handlebars.registerHelper('ifItemGroup', ifItemGroup);
	Handlebars.registerHelper('ifPrintItemUL', ifPrintItemUL);
	Handlebars.registerHelper('isFreeShipping', isFreeShipping);	

	
	// 일반 helper
	Handlebars.registerHelper('addCommas', addCommas);
	Handlebars.registerHelper('printImage', printImage);
	Handlebars.registerHelper('printItemCSSStyle', printItemCSSStyle);
	Handlebars.registerHelper('printPlusItemCSSStyle', printPlusItemCSSStyle);
	Handlebars.registerHelper('printAddCartCSSStyle', printAddCartCSSStyle);	
	Handlebars.registerHelper('printSellerInfo', printSellerInfo);
	Handlebars.registerHelper('printDeliveryInfo', printDeliveryInfo);
	Handlebars.registerHelper('printDeliveryInfoNew', printDeliveryInfoNew);
	Handlebars.registerHelper('printParentCategories', printParentCategories);
	Handlebars.registerHelper('printPaging', printPaging);
	Handlebars.registerHelper('printLargeImageVisibility', printLargeImageVisibility);
	Handlebars.registerHelper('printItemGroupTitle', printItemGroupTitle);
	Handlebars.registerHelper('printItemGroupDescription', printItemGroupDescription);
	Handlebars.registerHelper('printItemGroupStyle', printItemGroupStyle);
	Handlebars.registerHelper('printSmartDeliveryMoreText', printSmartDeliveryMoreText);
	Handlebars.registerHelper('getLinkUrl', getLinkUrl);
	Handlebars.registerHelper('getSRPClickLogUrl', getSRPClickLogUrl);
	Handlebars.registerHelper('getSmartClickLinkUrl', getSmartClickLinkUrl);
	Handlebars.registerHelper('getSmartDeliveryLandingUrl', getSmartDeliveryLandingUrl);
	Handlebars.registerHelper('getNavFinderATagName', getNavFinderATagName);

	Handlebars.registerHelper("inc", function (value, options) {
		return parseInt(value) + 1;
    });
    Handlebars.registerHelper("getCount", function (index) {
        return index + 1;
    });


	//Partial Template
	Handlebars.registerPartial("partialItemGroupTitle", $("#partial-itemgrouptitle-template").html());
}

function getLinkUrl(url, isPlusAd) {
	var linkUrl = url;
	var fcdCode = "";

	if (url.indexOf("http", 0) != 0) {
		new Handlebars.SafeString(linkUrl);
	}

	if (menuName != undefined) {
		if (menuName == "LP") {
			if (isPlusAd == true) {
				fcdCode = 706600001;
			} else {
				fcdCode = 706600002;
			}
		} else if (menuName == "SRP") {
			if (isPlusAd == true) {
				fcdCode = 706900001;
			} else {
				fcdCode = 706900002;
			}
		}
		if (fcdCode != "") {
			linkUrl = "http://sna.gmarket.co.kr/?fcd=" + fcdCode + "&url=" + encodeURIComponent(url);
		}
		
	}

	return new Handlebars.SafeString(linkUrl);
}

function getSmartClickLinkUrl(url, isPlusAd) {
	var linkUrl = url;
	var fcdCode = "";

	if (url.indexOf("http", 0) != 0) {
		new Handlebars.SafeString(linkUrl);
	}

	if (menuName != undefined) {
		if (menuName == "LP") {
			fcdCode = 706600003;
		} else if (menuName == "SRP") {
			fcdCode = 706900003;
		}
		if (fcdCode != "") {
			linkUrl = "http://sna.gmarket.co.kr/?fcd=" + fcdCode + "&url=" + encodeURIComponent(url);
		}
	}

	return new Handlebars.SafeString(linkUrl);
}

function getSmartDeliveryLandingUrl() {
	return new Handlebars.SafeString(returnData.SmartDeliveryLandingUrl);
}

function getNavFinderATagName(name) {
	var tagName = "";
	if (name != undefined) {
		if (name == "브랜드") {
			tagName = "#brand";
		} else if (name == "카테고리") {
			tagName = "#cate";
		}
	}
	return new Handlebars.SafeString(tagName);
}

function ifNotZero(a, opts) {

	if (typeof a === 'undefined' || a == null || a == '')
		a = 0;

	if (parseInt(a) != 0)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifNotZeroForItemTemplate(discountRate, discountPrice, opts) {

    if (typeof discountRate === 'undefined' || discountRate == null || discountRate == '')
        discountRate = 0;

    if (typeof discountPrice === 'undefined' || discountPrice == null || discountPrice == '')
        discountPrice = 0;

    if (parseInt(discountRate) != 0)
        return opts.fn(this);
    else if (parseInt(discountPrice) > 0)
        return opts.fn(this);
    else
        return opts.inverse(this);
}

function ifNotEmptyArray(a, opts) {

	if (!($.isArray(a))) {
		a = [];
	}

	if (a.length > 0)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifValidPrice(price, opts) {

	if (price == null || price == '' || typeof price === 'undefined')
		price = 0;
	else {
		// remove comma
		price = price.replace(/^\D+/g, '');
		price = parseInt(price);
	}

	if (price <= 10)
		return opts.inverse(this);
	else
		return opts.fn(this);
}

function hasMorePP(ppList, opts) {
	if (typeof ppList === 'undefined' || ppList == null || ppList == '')
		return opts.inverse(this);

	if (ppList.length <= 3)
		return opts.inverse(this);
	else
		return opts.fn(this);
}

function ifOverOne(val, opts) {
	if (typeof ppList === 'undefined' || ppList == null || ppList == '')
		return opts.inverse(this);

	if (val > 1)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifDisplayPlusIcon(isPlusAD, opts) {
	if ((currentViewStyle == 'Image') && isPlusAD)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifDisplayCategoryList(scIdName, length, opts) {
	if (scIdName !== 'undefined' && scIdName != null && scIdName != '')
		return opts.inverse(this);

	if (length > 0)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifDisplayGroupTitle(index, anotherTypeStart, opts) {	
	if (isDisplayItemGroup() && anotherTypeStart && (veryFirstItemGroup != 4))
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifDisplayAdIcon(listingItemGroup, opts) {
	if (getItemGroupInfo(listingItemGroup).displayAdIcon)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifDisplayBrandUl(index, isOpenUl, parent, opts) {
	//console.log("parent", parent);
	//TODO : 홀수 개일 경우... last 비교?
	var remainder = isOpenUl ? 0 : 1;
	var isLastItem = parent.Items !== null && index === parent.Items.length;
	if (isLastItem || index % 2 === remainder)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifDisplayCategoryMore(categoryType, opts) {
	var isDisplayCategoryMore = false;

	if (categoryType != undefined && categoryType == "C") {
		isDisplayCategoryMore = true;
	}

	if (isDisplayCategoryMore)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifDisplayBrandName(brandName, opts) {
	if (brandName != undefined && brandName != null && brandName != "기타" && brandName != "기타 (미입력)") {
		return opts.fn(this);
	} else {
		return opts.inverse(this);
	}
}

function ifItemGroup(groupType, listingItemGroup, opts) {
	if (getItemGroupInfo(listingItemGroup).type == groupType)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function ifPrintItemUL(type, item, itemCount, index, opts) {
	var isPrintUl = false;
	if (type == "start") {
		if (index == 0 || item.AnotherTypeStart) {
			isPrintUl = true;
		}
	} else if (type == 'end') {
		if (index == itemCount - 1 || item.AnotherTypeEnd ) {
			isPrintUl = true;
		}
	}

	if (isPrintUl)
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function isFreeShipping(item, opts) {
	if (item.DeliveryInfo === '무료배송')
		return opts.fn(this);
	else
		return opts.inverse(this);
}

function addCommas(nStr) {
		nStr += '';
		x = nStr.split('.');
		x1 = x[0];
		x2 = x.length > 1 ? '.' + x[1] : '';
		var rgx = /(\d+)(\d{3})/;
		while (rgx.test(x1)) {
			x1 = x1.replace(rgx, '$1' + ',' + '$2');
		}
		return x1 + x2;
}

/* Helper function 들 */
function printSellerInfo(item) {
	if (item.IsPurchasedSeller)
		return new Handlebars.SafeString('<span class="opt_buy">구매매장</span>');
	else if (item.ShopGroupCode === 14) {
		return new Handlebars.SafeString('<span class="ico_hplus">홈플러스</span>');
	}
}

function printDeliveryInfo(item) {
	if (item.ShopGroupCode !== 14) {
		if (item.Delivery.DeliveryType == 'BLUE')
			return new Handlebars.SafeString('<span class="opt_free">' + item.Delivery.DeliveryText + '</span>');
		else if (item.Delivery.DeliveryType == 'SMART')
			return new Handlebars.SafeString('<span class="opt_smart">' + item.Delivery.DeliveryText + '</span>');
		else if (item.Delivery.DeliveryType == 'MOUNT')
			return new Handlebars.SafeString('<span class="opt_tire">' + item.Delivery.DeliveryText + '</span>');
		else
			return new Handlebars.SafeString('<span class="opt_delivery">' + item.Delivery.DeliveryText + '</span>');
	}
}

function printDeliveryInfoNew(item) {
		var deliveryInfoHtml = '<span class="info_delivery">';
		if (item.ShopGroupCode === 14) {
			deliveryInfoHtml += '<span>홈플러스<em class="emp"> 당일배송</em></span>';
		}
		else if (item.Delivery.DeliveryType == 'BLUE') {
			deliveryInfoHtml += '<span>' + item.Delivery.DeliveryText + '</span>';
		}
		else if (item.Delivery.DeliveryType == 'SMART') {
			deliveryInfoHtml += '<span><em class="emp">스마트</em>배송</span>';
		}
		else if (item.Delivery.DeliveryType == 'MOUNT') {
			deliveryInfoHtml += '<span>' + item.Delivery.DeliveryText + '</span>';
		}
		else {
			deliveryInfoHtml += '<span>' + item.Delivery.DeliveryText + '</span>';
		}
		deliveryInfoHtml += '</span>';
		return new Handlebars.SafeString(deliveryInfoHtml);
}

function printParentCategories(result) {

	var innerHTML = '';

	if (reqParams.menuName == 'SRP') {
		innerHTML = '<li class="dim"><a href="javascript:setCategoryParams(\'\',\'\',\'\');search(false);" class="inp_box">' +
			'<strong class="tit">전체상품</strong><span class="sp_mw arr"></span></a>' +
			'</li>';
	}

	if (typeof result.LcIdName !== 'undefined' && result.LcIdName != null && result.LcIdName != '') {

		var css = 'dim';

		if (result.McIdName == '')
			css = 'on';

		innerHTML += '<li class="' + css + '"><a href="javascript:setCategoryParams(\'' +
				result.LcId + '\',\'\',\'\');search(false);">' +
				result.LcIdName + '</a>' +
				'</li>';
	}

	if (typeof result.McIdName !== 'undefined' && result.McIdName != null && result.McIdName != '') {

		var css = 'dim';

		if (result.ScIdName == '')
			css = 'on';

		innerHTML += '<h4 class=\"cate_tit nav_ctrl on\"><a href="javascript:sendCategoryLayerPdsLog(\'' + result.McId + '\');setCategoryParams(\'' +
				result.LcId + '\',\'' + result.McId + '\',\'' + defaultReqParams.scId + '\');search(false);">' +
				result.McIdName + '</a></h4>';

		//innerHTML += '<li class="' + css + '"><a href="javascript:setCategoryParams(\'' +
		//		result.LcId + '\',\'' + result.McId + '\',\'' + defaultReqParams.scId + '\');search(false);">' +
		//		result.McIdName + '</a>' +
		//		'</li>';
	}

	if (typeof result.ScIdName !== 'undefined' && result.ScIdName != null && result.ScIdName != '') {
		innerHTML += '<li class="on"><a href="javascript:setCategoryParams(\'' +
				reqParams.lcId + '\',\'' + reqParams.mcId + '\',\'' + result.ScId + '\');search(false);">' +
				result.ScIdName + '</a>' +
				'</li>';
	}

	return new Handlebars.SafeString(innerHTML);
}

function printItemGroupStyle(elType) {
	var itemGroupStyle = getItemGroupStyle();

	switch (elType) {
		case 'div': return itemGroupStyle.divClass;
		case 'h3': return itemGroupStyle.h3Class;
	}

	return '';
}

function printItemGroupTitle(listingItemGroup) {
	return getItemGroupInfo(listingItemGroup).title;
}

function printItemGroupDescription(listingItemGroup) {
    return getItemGroupInfo(listingItemGroup).description;
}

function printSmartDeliveryMoreText() {
	if (currentViewStyle == 'Gallery') {
		return "더보기";
	} else if (currentViewStyle == 'List') {
		return "스마트 배송 상품 더보기";
	}

	return '';
}

function printPaging(result) {

	var numOfPagesToShow = 5;

	if ( typeof result.Item === 'undefined' || result.Item == null || result.Item.length <= 0) return;

	var totalPage = parseInt(result.TotalGoodsCount / result.PageSize);
	if (result.TotalGoodsCount % result.pageSize != 0)
		totalPage++;

	// 화면 첫번재 보이는 페이지 수
	var firstPage = result.PageNo;
	while (firstPage % numOfPagesToShow != 1 && firstPage > 1)
		firstPage--;

	// 화면 마지막 페이지 수
	var lastPage = result.PageNo;
	while (lastPage % numOfPagesToShow != 0 && lastPage < totalPage)
		lastPage++;

	var prevPagePdsCode = ""
	var nextPagePdsCode = "";
	var pageClickPdsCode = "";

	if (reqParams.menuName === "SRP") {
		prevPagePdsCode = "200000052";
		nextPagePdsCode = "200000053";
		pageClickPdsCode = "200000054";
	} else {
		prevPagePdsCode = "200000115";
		nextPagePdsCode = "200000116";
		pageClickPdsCode = "200000117";
	}

	var pagingHTML = '<div class="paginate card">';

	// 이전 페이지 버튼
	if (result.PageNo > numOfPagesToShow) {
		pagingHTML += '<a href="javascript:;"' +
			'onclick="javascript:sendPDSLogBasic(\'' + prevPagePdsCode + '\', \'Utility\');goPage(' + (firstPage - 1) + ');" ' +
			'class="prev selected"><span class="sp_lpsrp">이전 상품리스트</span></a>';
	}
	else {
		pagingHTML += '<span class="prev"><span class="sp_lpsrp">이전 상품리스트 없음</span></span>';
	}

	var count = totalPage > numOfPagesToShow ? numOfPagesToShow : totalPage;

	for (var i = firstPage; i <= lastPage; i++) {
		var css = '';
		if (result.PageNo == i)
			css = 'selected';
		pagingHTML += '<a href="javascript:;"' +
			'onclick="javascript:sendPDSLogBasic(\'' + pageClickPdsCode + '\', \'Utility\');goPage(' + i + ');" ' +
			'class="' + css + '">' + i + '</a>'
	}

	// 이후 페이지 버튼
	if (totalPage > lastPage) {
		pagingHTML += '<a href="javascript:;"' +
			'onclick="javascript:sendPDSLogBasic(\'' + nextPagePdsCode + '\', \'Utility\');goPage(' + (lastPage + 1) + ');" ' +
			'class="next selected"><span class="sp_lpsrp">다음 상품리스트</span></a>';
	}
	else {
		pagingHTML += '<span class="next"><span class="sp_lpsrp">다음 상품리스트 없음</span></span>';
	}

	pagingHTML += '</div>';

	return new Handlebars.SafeString(pagingHTML);
}
/* Helper function 들 */

function goPage(pageNo) {
	reqParams.pageNo = pageNo;
	searchJson(true, true);
}

function displayDirectLink() {
	if (returnData.DirectLinkURL == null ||
			typeof returnData.DirectLinkURL === 'undefined' || returnData.DirectLinkURL.length == 0) {
		$('#directLink').hide();
		return;
	}

	$('#directLink').show();

	$('#directLink a').attr('href', returnData.DirectLinkURL);
	$('#directLink #directLinkText').html(returnData.DirectLinkText);
	$('#directLink #directLinkDesc').html(returnData.DirectLinkDesc);
}

var displayRecommendKeyword = function () {
	$.ajax({
		type: 'POST',
		dataType: 'json',
		url: "/Search/GetRecommedKeyword",
		data: { primeKeyword: reqParams.primeKeyword },
		xhrFields: {
			withCredentials: true
		},
		success: function (result) {
			var $ul = $('#divRecommedKeyword').find('ul.recom_lst');
			$ul.empty();
			if (!$.isArray(result) || result.length === 0) {
				$('#divRecommedKeyword').hide();
				return;
			}
			
			var size = result.length;
			var $result = $();
			for (var i = 0; i < size; i++) {
				var item = result[i];
				var $element = $('<li><a href="#" onclick="javascript:selectKeyword(this);" class="in_txt">' + item.Keyword + '</a></li>')
				$element.find('a').data('srpData', item.SrpPostJson);
				$element.find('a').data('smartboxData', item.SmartBoxPostJson);
				$element.find('a').data('pdslog', item.PdsLogJson);
				$result = $result.add($element);
			}

			$ul.append($result);

			$('#divRecommedKeyword').show();
		}
	});
}

var selectKeyword = function (tag) {
	var $this = $(tag);
	var srpData = JSON.parse($this.data('srpData'));
	var smartboxData = JSON.parse($this.data('smartboxData'));
	var pdsData = JSON.parse($this.data('pdslog'));

	sendPDSLog(pdsData);

	document.location.href = '/Search/Search?' + $.param(srpData);

	return false;
}

var searchCategoryMore = function () {
	if (reqParams.menuName == 'SRP' || searchEx.isDepartmentStore) {
		detailTileManager.search();
	}
}

var searchBrandMore = function () {
	if (reqParams.menuName == 'SRP') {
		if ($("#brandListDiv").attr("update") == "yes") {
			var originalTile = reqParams.tiles;
			if (!$.isArray(originalTile)) {
				originalTile = [];
			}
			var selectedBrand = brandMoreTiles.getAllTile();
			if (selectedBrand.length > 0) {
				for (var i = 0; i < selectedBrand.length; i++) {
					$('#li_tile_' + selectedBrand[i].Type + '_' + selectedBrand[i].Code).addClass('selected');
				}
				reqParams.tiles = selectedBrand.concat(originalTile);
				brandMoreTiles.clearTile();
			}
			setSmartBoxGroupType('B');
			reqParams.sellCustNo = "";
			$("#brandListDiv").attr("update", "no");
			bForceTopMove = true;
			displaySmartBox(true);
			search(true);
		}
	} else if (reqParams.menuName == 'LP') {
		brandSearch();
	}
}

var brandMoreTiles = (function () {
	var tiles = [];

	return {
		addTile: function (tile) {
			tiles.push(tile);
		}
		, getAllTile: function () {
			return tiles.slice(0);
		}
		, removeTile: function (code) {
			var size = tiles.length;
			for (var i = size - 1; i >= 0; i--) {
				if (tiles[i].Code == code) {
					tiles.splice(i, 1);
				}
			}
		}
		, clearTile: function () {
			tiles = [];
		}
	}
})();

var TILE_TIMER_SECONDS = 1;
var resetTileTimer = (function () {
	var timer;

	return function () {
		clearInterval(timer);
		timer = setInterval(function () {
			clearInterval(timer);
			reqParams.sellCustNo = "";
			displaySmartBox(true);
			search(true);
		}, TILE_TIMER_SECONDS * 1000);	
	}
})();

var setSmartBoxGroupType = function (type) {
	if (!reqParams.smartBoxGroupType) {
		reqParams.smartBoxGroupType = type;
		return true;
	}

	if (reqParams.smartBoxGroupType !== type) {
		return false;
	}
	else {
		return true;
	}
}

var selectBrandMoreTile = (function () {
	var lastClickTime = 0;
	return function (type, code, name, tag) {
		sendBrandLayerPdsLog(code);
		var current = new Date().getTime();
		var delta = current - lastClickTime;
		if (delta < 400) {
			return false;
		}
		var $li = $(tag).parent();
		var tiles = reqParams.tiles;

		if (!setSmartBoxGroupType(type)) {
			return;
		}

		if ($li.hasClass('selected')) {
			$('#li_tile_' + type + '_' + code).removeClass('selected');
			brandMoreTiles.removeTile(code);
			var size = reqParams.tiles.length;
			for (var i = size - 1; i >= 0; i--) {
				if (reqParams.tiles[i].Code == code) {
					reqParams.tiles.splice(i, 1);
				}
			}
			$li.removeClass('selected');
		}
		else {
			brandMoreTiles.addTile({ Type: type, Code: code, Name: name, Selected: true });
			$li.addClass('selected');
		}
		lastClickTime = current;

		$("#brandListDiv").attr("update", "yes");
		return false;
	}
})();

var selectTile = function (type, code, name, tag) {
	var $li = $(tag).parent();
	var tiles = reqParams.tiles;

	if (!setSmartBoxGroupType(type)) {
		return;
	}
	if (type == "C") {
		detailTileManager.selectCategoryLTile(code);
		if ($li.hasClass('selected')) {
			detailTileManager.displayToggleTile(code, true);
		} else {
			detailTileManager.displayToggleTile(code, false);
		}
	}

	if ($li.hasClass('selected')) {
		$li.removeClass('selected');
		$('#li_tile_detail_' + type + '_' + code).removeClass('selected');
		$li.find('button').hide();
		var size = tiles.length;
		if (size !== 0) {
			for (var i = size - 1; i >= 0; i--) {
				if (tiles[i].Code == code) {
					tiles.splice(i, 1);
				}
			}
		}
	}
	else {
		if (type == "C") {
			var selectedTile = detailTileManager.getTileCategory(TileHashEnum.Type.Category, code, false);
			if (selectedTile) {
				tiles.unshift(selectedTile);
				$li.addClass('selected');
				$li.find('button').show();
			}

		} else if (type == "B") {
			$('#li_tile_detail_' + type + '_' + code).addClass('selected');
			tiles.unshift({ Type: type, Code: code, Name: name, Selected: true });
			$li.addClass('selected');
		}
	}
	$('#divTileHistory').find('div.txt_category').hide();
	displaySmartBoxHistory();
	resetTileTimer();
	smartScroll();
	return false;
}

function selectSmartDeliveryFilter(obj) {
	var $checkBox = $(obj);
	if ($checkBox.is(':checked')) {
		reqParams.isSmartDelivery = "Y";
	} else {
		reqParams.isSmartDelivery = "N";
	}

	if (reqParams.menuName == 'SRP') {
		$('#divSmartBox').show();
	}

	bForceTopMove = true;
	search(true);

	return false;
}

function selectBrandGoodsFilter(obj) {
	var $checkBox = $(obj);
	if ($checkBox.is(':checked')) {
		reqParams.isBrand = "Y";
	} else {
		reqParams.isBrand = "N";
	}

	if (reqParams.menuName == 'SRP') {
		$('#divSmartBox').show();
	}

	bForceTopMove = true;
	search(true);

	return false;
}

function TileHashEnum() { }
TileHashEnum.Type = { Category: 0, MDetail: 1, SDetail: 2, History: 3 };

var detailTileManager = (function () {
	var liPrefix = "li_tile_detail_C_";
	var categoryHash = new Object();
	var mDetailHash = new Object();
	var sDetailHash = new Object();
	var historyHash = new Object();
	var historyTiles = [];

	var isDetailLayerChanged = false;

	function getHash(htype) {
		var hash;
		switch (htype) {
			case TileHashEnum.Type.Category:
				hash = categoryHash;
				break;
			case TileHashEnum.Type.MDetail:
				hash = mDetailHash;
				break;
			case TileHashEnum.Type.SDetail:
				hash = sDetailHash;
				break;
			case TileHashEnum.Type.History:
				hash = historyHash;
				break;
			default:
				hash = null;
				break;
		}
		return hash;
	}

	function historyUpdate(tile) {
		var size = historyTiles.length;
		var code = tile.Code;
		// remove
		if (size !== 0) {
			for (var i = size - 1; i >= 0; i--) {
				if (historyTiles[i].Code == code) {
					historyTiles.splice(i, 1);
				}
			}
		}

		if (tile.Selected) {
			historyTiles.unshift(tile);
		}
	}

	return {
		initCategoryHash: function (data) {
			if (!data || data.length < 1) {
				return;
			}

			var categoryHash = new Object();

			for (var i = 0; i < data.length; i++) {
				this.setTileCategory(TileHashEnum.Type.Category, data[i].Code, data[i]);
			}
		},
		initDetailCategoryHash: function (categoryType, lCode, mCode, data) {
			if (!data || data.length < 1) {
				return;
			}

			for (var i = 0; i < data.length; i++) {
				if (categoryType == "L") {
					if (data[i] && data[i].DetailTile && data[i].DetailTile.Mtile) {
						var tile = this.getTileCategory(TileHashEnum.Type.MDetail, data[i].DetailTile.Mtile.Code, false);
						if (!tile) {
							this.setTileCategory(TileHashEnum.Type.MDetail, data[i].DetailTile.Mtile.Code, data[i]);
						}
					}
				} else if (categoryType == "M") {
					if (data[i] && data[i].DetailTile && data[i].DetailTile.Stile) {
						var tile = this.getTileCategory(TileHashEnum.Type.SDetail, data[i].DetailTile.Stile.Code, false);
						if (!tile) {
							this.setTileCategory(TileHashEnum.Type.SDetail, data[i].DetailTile.Stile.Code, data[i]);
						}
					}
				}
			}

			this.displaySmartBoxDetail(categoryType, lCode, mCode, data);

		},
		getTileCategory: function (htype, code, isClone) {
			var hash;
			hash = getHash(htype);

			if (hash) {
				if (hash[code]) {
					if (isClone) {
						return $.extend(true, {}, hash[code]);
					} else {
						return hash[code];
					}
				}
			}
			return null;
		},
		setTileCategory: function (htype, code, tile) {
			var hash = getHash(htype);
			hash[code] = tile;

			if (htype == TileHashEnum.Type.History) {
				historyUpdate(tile);
			}
		},
		selectCategoryLTile: function (code, isGetChild, obj) {
			isDetailLayerChanged = true;
			var selectedTile = this.getTileCategory(TileHashEnum.Type.History, code, false);
			if (!selectedTile) {
				selectedTile = this.getTileCategory(TileHashEnum.Type.Category, code, false);
			}

			if (selectedTile) {
				if (selectedTile.Selected) {
					if (selectedTile.DetailTile) {
						if (selectedTile.DetailTile.Mtile) {
							var mTile = this.getTileCategory(TileHashEnum.Type.MDetail, selectedTile.DetailTile.Mtile.Code, false);
							if (mTile) {
								mTile.Selected = false;
								mTile.DetailTile.Mtile.Selected = false;
								this.setTileCategory(TileHashEnum.Type.MDetail, selectedTile.DetailTile.Mtile.Code, mTile);
							}
						}
						if (selectedTile.DetailTile.Stile) {
							var sTile = this.getTileCategory(TileHashEnum.Type.SDetail, selectedTile.DetailTile.Stile.Code, false);
							if (sTile) {
								sTile.Selected = false;
								sTile.DetailTile.Stile.Selected = false;
								this.setTileCategory(TileHashEnum.Type.SDetail, selectedTile.DetailTile.Stile.Code, sTile);
							}
						}
					}
					selectedTile.Selected = false;
				} else {
					selectedTile.Selected = true;
					// 20160924 -- start --
					if (selectedTile.DetailTile) {
						if (selectedTile.DetailTile.Mtile) {
							var mTile = this.getTileCategory(TileHashEnum.Type.MDetail, selectedTile.DetailTile.Mtile.Code, false);
							if (mTile) {
								mTile.Selected = true;
								mTile.DetailTile.Mtile.Selected = false;
								this.setTileCategory(TileHashEnum.Type.MDetail, selectedTile.DetailTile.Mtile.Code, mTile);
							}
						}
						if (selectedTile.DetailTile.Stile) {
							var sTile = this.getTileCategory(TileHashEnum.Type.SDetail, selectedTile.DetailTile.Stile.Code, false);
							if (sTile) {
								sTile.Selected = true;
								sTile.DetailTile.Stile.Selected = false;
								this.setTileCategory(TileHashEnum.Type.SDetail, selectedTile.DetailTile.Stile.Code, sTile);
							}
						}
					}
					// 20160924 -- end --
				}
				this.setTileCategory(TileHashEnum.Type.History, code, selectedTile);
				if (isGetChild) {
					this.getSmartBoxDetail("L", code);
				}
				smartBoxLCategoryAUpdate(obj);
			}
		},
		selectCategoryLTileButton: function (code, obj) {
			this.getSmartBoxDetail("L", code);
			smartBoxLCategoryButtonUpdate(obj);
		},
		selectCategoryMTile: function (code, obj) {
			isDetailLayerChanged = true;
			var selectedTile = this.getTileCategory(TileHashEnum.Type.MDetail, code, false);

			if (selectedTile) {
				selectedTile.Selected = true;
				if (selectedTile.DetailTile.Mtile.Selected) {
					selectedTile.DetailTile.Mtile.Selected = false;

					var selectedHistoryTile = this.getTileCategory(TileHashEnum.Type.History, selectedTile.Code, false);
					if (selectedHistoryTile) {
						if (selectedHistoryTile.DetailTile && selectedHistoryTile.DetailTile.Stile) {
							var sTile = this.getTileCategory(TileHashEnum.Type.SDetail, selectedHistoryTile.DetailTile.Stile.Code, false);
							if (sTile) {
								sTile.Selected = false;
								sTile.DetailTile.Stile.Selected = false;
								this.setTileCategory(TileHashEnum.Type.SDetail, sTile.DetailTile.Stile.Code, sTile);
							}
						}
					}

					var lTile = this.getTileCategory(TileHashEnum.Type.Category, selectedTile.Code, false);
					lTile.Selected = true;
					this.setTileCategory(TileHashEnum.Type.History, lTile.Code, lTile);
					this.getSmartBoxDetail("M", selectedTile.Code, code);
				} else {
					selectedTile.DetailTile.Mtile.Selected = true;
					this.setTileCategory(TileHashEnum.Type.History, selectedTile.Code, selectedTile);
					this.getSmartBoxDetail("M", selectedTile.Code, code);
				}

				smartBoxMCategoryUpdate(obj, true);
			}
		},
		selectCategorySTile: function (code, obj) {
			isDetailLayerChanged = true;
			var selectedTile = this.getTileCategory(TileHashEnum.Type.SDetail, code, false);
			if (selectedTile) {
				if (selectedTile.DetailTile.Stile.Selected) {
					selectedTile.Selected = false;
					selectedTile.DetailTile.Stile.Selected = false;
					var mTile = this.getTileCategory(TileHashEnum.Type.MDetail, selectedTile.DetailTile.Mtile.Code, false);
					mTile.Selected = true;
					mTile.DetailTile.Mtile.Selected = true;
					this.setTileCategory(TileHashEnum.Type.History, selectedTile.Code, mTile);
				} else {
					selectedTile.Selected = true;
					selectedTile.DetailTile.Stile.Selected = !selectedTile.DetailTile.Stile.Selected;
					this.setTileCategory(TileHashEnum.Type.History, selectedTile.Code, selectedTile);
				}
				smartBoxSCategoryUpdate(obj);
			}
		},
		sendPdsLog: function (code, isButton) {
			if (reqParams.menuName === "SRP") {
				var pdsData = {};
				if (isButton) {
					pdsData.AreaCode = "200000070";
				} else {
					pdsData.AreaCode = "200000069";
				}
				pdsData.AreaType = "Utility";
				pdsData.Parameter = {};
				pdsData.Parameter.keyword = reqParams.primeKeyword;
				pdsData.Parameter.category_code = code;
				sendPDSLog(pdsData);
			}
		},
		search: function () {
			if (isDetailLayerChanged) {
				if (reqParams.tiles && reqParams.tiles.length > 0) {
					var brandTiles = [];

					for (var i = 0; i < reqParams.tiles.length; i++) {
						if (reqParams.tiles[i].Type == "B") {
							brandTiles.push(reqParams.tiles[i]);
						}
					}
					reqParams.tiles = this.getHistoryTile();
					if (brandTiles && brandTiles.length > 0) {
						reqParams.tiles = reqParams.tiles.concat(brandTiles);
					}

				} else {
					reqParams.tiles = this.getHistoryTile();
				}

				bForceTopMove = true;
				setSmartBoxGroupType("C");
				displaySmartBoxHistory();
				resetTileTimer();
				smartScroll();
			}
		},
		getAllTile: function (htype, isArray) {
			var hash = getHash(htype);
			if (isArray) {
				var array = $.map(hash, function (value, index) {
					return value;
				});
				return array;
			} else {
				return hash;
			}
		},
		getHistoryTile: function () {
			return historyTiles;
		},
		setHistoryTile: function (htype, code) {
			var tile = this.getTileCategory(htype, code, false);
			if(tile){
				this.setTileCategory(TileHashEnum.Type.History, code, tile);
			}
		},
		displaySmartBoxDetailLayer: function (code) {
			isDetailLayerChanged = false;
			if (code) {
				setTimeout(function () {
					var button = $('#' + liPrefix + code).find("span > button");
					if ($('#' + liPrefix + code).find(".dep2 li").length > 0) {
						smartBoxLCategoryButtonUpdate(button);
					} else {
						detailTileManager.selectCategoryLTileButton(code, button);
					}

				}, 500);

			} else {
				smartBoxDetailLayer();
			}
		},
		displayToggleTile: function (code, isRemove) {
			if (isRemove) {
				$('#' + liPrefix + code + '> ul').attr("style", "");
				$('#' + liPrefix + code + '> ul').removeClass('dep_selected');
				$('#' + liPrefix + code + '> ul').html("");
			}
			$('#' + liPrefix + code).toggleClass('selected');
		},
		displaySmartBoxDetail: function (categoryType, lCode, mCode, data) {
			if (!data)
				return;
			if (categoryType == "L") {
				var source = $('#smartbox-mcategory-template').html();
				var template = Handlebars.compile(source);
				$('#' + liPrefix + lCode).find(".dep2").html(template(data));
			} else if (categoryType == "M") {
				var source = $('#smartbox-scategory-template').html();
				var template = Handlebars.compile(source);
				$('#' + liPrefix + mCode).find(".dep3").html(template(data));
			}
		},
		getSmartBoxDetail: function (categoryType, lCode, mCode) {
			var req;
			var reqBody = null;
			if (categoryType == "L") {
				if ($('#' + liPrefix + lCode).find(".dep2 li").length > 0) {
					return false;
				}
				req = this.getTileCategory(TileHashEnum.Type.Category, lCode, false);
				if (req) {
					reqBody = req.DetailApiBody;
				}
			} else if (categoryType == "M") {
				if ($('#' + liPrefix + mCode).find("ul > li").length > 0) {
					return false;
				}
				req = this.getTileCategory(TileHashEnum.Type.MDetail, mCode, false);
				if (req) {
					reqBody = req.DetailTile.Mtile.DetailApiBody;
				}
			}

			if (reqBody) {
				$.ajax({
					type: 'POST',
					dataType: 'json',
					url: "/Search/GetSmartBoxDetail",
					data: reqBody,
					async: false,
					contentType: "application/json",
					xhrFields: {
						withCredentials: true
					},
					success: function (data) {
						ajaxHistory.addSmartBoxDetailTiles(categoryType, lCode, mCode, data);
						detailTileManager.initDetailCategoryHash(categoryType, lCode, mCode, data);
					}
				});
			}

		}

	}
})();

var moreCategoryView = function (sLCategoryCode, index) {
	alert("selectedCategoryCode = " + sLCategoryCode + "  index = " + index);

}

var removeTile = function (tag) {
	var $this = $(tag);
	var code = $this.data('code');
	var type = $this.data('grouptype');

	if (!setSmartBoxGroupType(type)) {
		return;
	}

	var tiles = reqParams.tiles;
	var size = tiles.length;
	if (size !== 0) {
		for (var i = size - 1; i >= 0; i--) {
			if (tiles[i].Code == code) {
				tiles.splice(i, 1);
			}
		}
	}
	$(tag).remove();
	$('#li_tile_' + type + '_' + code).removeClass('selected');
	$('#li_tile_' + type + '_' + code).find('button').hide();

	if (type == "C") {
		detailTileManager.selectCategoryLTile(code);
		detailTileManager.displayToggleTile(code, true);
	}

	displaySmartBoxHistory();
	resetTileTimer();
	smartScroll();
	return false;
}

var displaySmartBoxHistory = function (tiles) {
	var tiles = tiles || reqParams.tiles;
	var $list = $('#divTileHistory').find('ul.recom_lst');
	$list.empty();

	if (!$.isArray(tiles) || tiles.length === 0) {
		if (searchEx.isDepartmentStore) {
			$('#scroll_in2').hide();
			$('#divTileHistory').find('div.txt_category').show();
		}
		else {
			$('#divTileHistory').hide();
		}
		return;
	}

	if (searchEx.isDepartmentStore) {
		$('#scroll_in2').show();
		$('#divTileHistory').find('div.txt_category').hide();
	}

	var size = tiles.length;
	var $result = $();
	for (var i = 0; i < size; i++) {
		var item = tiles[i];
		var historyName = item.HistoryName;
		if (!historyName) {
			historyName = item.Name;
		}
		var $element = $('<li class="' + (!item.Selected ? 'less' : '') + '"><span class="in_txt">' + historyName + '</span><button type="button" class="btn_del" onclick="javascript:sendPDSLogFromEliment(this);removeTile(this);"><span class="sp_lpsrp">삭제</span></button></li>')
		$element.find('button').data('code', item.Code);
		$element.find('button').data('grouptype', item.Type);
		$element.find('button').data('pdslog', item.PdsLogJson);
		$result = $result.add($element);
	}
	$list.append($result);
	$('#divTileHistory').show();
}

var displayMoreBrand = function (tileMore) {
	var $indicator = $('#brand_path').find('div.indicator');
	var $ul = $('#brand_path').find('ul.slide_lst');

	$ul.empty();
	$indicator.empty();

	if (!$.isArray(tileMore) || tileMore.length === 0) {
		return;
	}

	var $indicator = $('#brand_path').find('div.indicator');
	$indicator.attr('onclick', "javascript:sendPDSLogBasic('200000067','Utility');");
	var $ul = $('#brand_path').find('ul.slide_lst');
	var size = tileMore.length;
	var chosung = '', $indicatorItem, $group, $groupUl, $item, item;
	for (var i = 0; i < size; i++) {
		var moreItem = tileMore[i];

		if (chosung !== moreItem.MoreGroup) {
			$ul.append($group);

			chosung = moreItem.MoreGroup;
			$indicatorItem = $('<a href="#type' + encodeURIComponent(chosung).replace(/%/g, '') + '"><span>' + chosung + '</span></a>');
			$indicator.append($indicatorItem);

			$group = $('<li id="type' + encodeURIComponent(chosung).replace(/%/g, '') + '"><h4 class="grp_txt">' + chosung + '</h4><ul class="brand_logo"></ul></li>');
		}
		$item = $('<li id="li_tile_detail_B_' + moreItem.Code + '" class="' + (moreItem.Selected ? 'selected' : '') + '"><a href="javascript:;" onclick="javascript:selectBrandMoreTile(\'' + moreItem.Type + '\', \'' + moreItem.Code + '\', \'' + moreItem.Name.replace(/'/g, '\\\'') + '\', this);"><img class="lazyload" data-original="' + moreItem.Image + '" alt="' + moreItem.Name.replace(/'/g, '\\\'') + '" src="http://pics.gmarket.co.kr/mobile/lpsrp/img_logo.gif"/></a></li>');
		$group.find('ul.brand_logo').append($item);
	}
	$ul.append($group);
	$ul.find("img.lazyload").lazyload({
		container: $('.slide_lst.brand')
	});
	$indicator.append($indicatorItem);
}

var sendBrandLayerPdsLog = function (brandCode) {
	var pdsData = {};

	pdsData.Parameter = {};
	pdsData.Parameter.brand_code = brandCode;
	pdsData.AreaType = "Utility";

	if (reqParams.menuName === "SRP") {
		pdsData.AreaCode = "200000066";
		pdsData.Parameter.keyword = reqParams.primeKeyword;
	} else {
		pdsData.AreaCode = "200000129";
		if (reqParams.scId !== "undefined" && reqParams.scId !== null && reqParams.scId !== "") {
			pdsData.Parameter.category_code = reqParams.scId;
		} else {
			pdsData.Parameter.category_code = reqParams.mcId;
		}
	}
	sendPDSLog(pdsData);
}

var displayBrandDirect = function (data) {
	if (data === undefined || data === null || !data.HasSRPDirect || searchEx.isDepartmentStore) {
		$('#srpBrandDirectWrap').remove();
		return;
	}
	var $wrapper = $('#srpBrandDirectWrap');
	var source = $('#branddirect-template').html();
	var template = Handlebars.compile(source);
	$wrapper.html(template(data));
	$wrapper.show();
	srpBanner();
}

var renderSelectedTileInSmartBoxMoreLayer = function (selectedTiles) {
	$("li[etype|='B']").removeClass('selected');
	$("li[etype|='C']").removeClass('selected');
	$("li[etype|='C']").find("button").hide();

	selectedTiles.forEach(function (selectedTile) {
		$('#li_tile_' + selectedTile.Type + '_' + selectedTile.Code).addClass('selected');
		$('#li_tile_' + selectedTile.Type + '_' + selectedTile.Code).find("button").show();
		$('#li_tile_detail_' + selectedTile.Type + '_' + selectedTile.Code).addClass('selected');

		if (selectedTile.DetailTile && selectedTile.DetailTile.Mtile) {
			var $detailMTile = $('#li_tile_detail_' + selectedTile.Type + '_' + selectedTile.DetailTile.Mtile.Code);
			$detailMTile.addClass('selected');
			detailTileManager.setHistoryTile(TileHashEnum.Type.MDetail, selectedTile.DetailTile.Mtile.Code);
			smartBoxMCategoryUpdate($detailMTile.find('a'), false);
		}

		if (selectedTile.DetailTile && selectedTile.DetailTile.Stile) {
			var $detailSTile = $('#li_tile_detail_' + selectedTile.Type + '_' + selectedTile.DetailTile.Stile.Code);
			$detailSTile.parent().removeClass('dep_selected2');
			$detailSTile.removeClass('selected'); //smartBoxSCategoryUpdate 에서 toggle 해주기때문에....
			detailTileManager.setHistoryTile(TileHashEnum.Type.SDetail, selectedTile.DetailTile.Stile.Code);
			smartBoxSCategoryUpdate($detailSTile.find('a'));
		}
	});
};

var tempSmartBox; // todo tolhm 삭제 할 것 
var renderSmartBox = function (needOnlyTileSwap, result) {
	ajaxHistory.setSmartBoxGroupType(reqParams.smartBoxGroupType);
	var $swipe = $('#divSmartBoxSwipe');
	if (!result || !result.TabEntities || result.TabEntities.length === 0) {
		$swipe.hide();
		return false;
	}
	
	tempSmartBox = result;

	$('#divSmartBox').show();
	$swipe.show();
	displayBrandDirect(result.SRPDirect);

	var $tile = $('#divSmartBoxTile');

	var isRenewSwiper = false;
	var roofCount = $tile.find('ul').length;
	if (roofCount < result.TabEntities.length) {
		needOnlyTileSwap = false;
		isRenewSwiper = true;
	}
	var source = $('#smartbox-template').html();
	var template = Handlebars.compile(source);

	if (needOnlyTileSwap) {
		ajaxHistory.setCurrentSmartBoxResult(result);
		var $template = $(template(result));


		var $tab = $('#divSmartBoxTab');
		var selectedTabName = $tab.find('a.selected').text();

		$tab.empty();
		$tab.append($template.find('#divSmartBoxTab').find('a'));
		var $selectedA = $tab.find('a:contains(' + selectedTabName + ')');
		if ($selectedA.length > 0) {
			$selectedA.addClass('selected');
		}
		else {
			$($tab.find('a').get(0)).addClass('selected');
		}

		var $uls = $(template(result)).find('ul.lst_smart');

		var i, j = 0;
		for (i = 0; i < roofCount; i++) {
			if (result.TabEntities[i]) {
				if (reqParams.smartBoxGroupType !== result.TabEntities[i].GroupType) {
					$($tile.find('ul').get(i)).empty();
					var $pureLi = $($uls[i]).find('li').unwrap();
					$($tile.find('ul').get(i)).append($pureLi);

					var ulTag = $(template(result)).find('ul.lst_smart').get(i);
					var aTag = ulTag ? $(ulTag).siblings().get(0) : null;
					//var aTag = $(template(result)).find('ul').siblings().get(i);
					//					if (!aTag) {
					//						aTag = $(template(result)).find('ul').siblings().get(i - 1);
					//						if (!aTag) {
					//							aTag = $(template(result)).find('ul').siblings().get(i + 1);
					//						}
					//					}
					var $currnetTileDiv = $($tile.find('div').find('div').get(i));
					$currnetTileDiv.children('a').remove();
					if (aTag) $currnetTileDiv.append(aTag);

					if (result.TabEntities[i].GroupType == "C") {
						detailTileManager.initCategoryHash(result.TabEntities[i].TileMore);
						displaySmartBoxCategory(result.TabEntities[i]);
					}
				}
			} else {
				var temp = $($tile.find('ul').get(i)).parent().children('a').attr("href");
				if (temp == "#brand") {
					$($tile.find('ul').get(i)).parent().remove();
				} else {
					$($tile.find('ul').get(i - 1)).parent().remove();
				}
			}
		}
		renderSelectedTileInSmartBoxMoreLayer(reqParams.tiles);
		$tile.find('ul:empty').offsetParent().remove();
	}
	else {
		ajaxHistory.setInitialSmartBoxResult(result);
		for (var i = 0; i < result.TabEntities.length; i++) {
			if (result.TabEntities[i].GroupType == "C") {
				detailTileManager.initCategoryHash(result.TabEntities[i].TileMore);
				displaySmartBoxCategory(result.TabEntities[i]);
				ajaxHistory.getSmartBoxDetailTiles("L").forEach(function (tile) {
					detailTileManager.initDetailCategoryHash(tile.categoryType, tile.lCode, tile.mCode, tile.data);
				});

				ajaxHistory.getSmartBoxDetailTiles("M").forEach(function (tile) {
					detailTileManager.initDetailCategoryHash(tile.categoryType, tile.lCode, tile.mCode, tile.data);
				});
			}
			else if (result.TabEntities[i].GroupType === 'B' && result.TabEntities[i].TileMore.length > 0) {
				$('#divSmartBoxTile').find('a.btn_more').show();
				displayMoreBrand(result.TabEntities[i].TileMore);
			}
		}
		$swipe.find('#divNavFinder,#divSmartBoxTileContainer').remove();
		$swipe.prepend(template(result));
		$($swipe.find('#divSmartBoxTab').find('a').get(0)).addClass('selected');

		if (searchEx.isDepartmentStore) {
			$('div.sort_area').insertAfter('#scroll_in2');
			if (reqParams.menuName === "SRP") {
				$("#primeKeyword").val(reqParams.primeKeyword);
				$('#divTileHistory').prepend('<div class="txt_category">"' + reqParams.primeKeyword + '"의 G마켓 백화점 상품입니다.</div>');
			}
			else if (reqParams.menuName === "LP") {
				$('#divTileHistory').prepend('<div class="txt_category">"' + searchEx.selectedCategoryGroupName + '"의 G마켓 백화점 상품입니다.</div>');
				$('#searchOptionDiv').remove();
				$('h2.kind_tit.kind_tit2').remove();
				$('div.sch_dept').find('input').prop("placeholder", "백화점 상품 검색");
			}
			$('#divTileHistory').show();
		}
	}

	displaySmartBoxHistory(result.TileHistory);

	// 코더분 코드
	if (isRenewSwiper) {
		smartSwiper = undefined;
	}

	if (searchEx.isDepartmentStore) {
		$("#divNavFinder").addClass("simple_h");
	} else {
	}

	bindAdLayerEvent('.sort_layer');
	$('[data-layer-ctrl=true]').off('click');
	openLayer();

	//brandLayerView();
	smartBoxCnt();
	smartScroll();
	smartBoxHeight();
	// 퍼블리싱 코드
	finderScrollFixTop();

	return true;
}

var displaySmartBox = function (needOnlyTileSwap) {	
	reqParams.sellCustNo = '';
	$.ajax({
		type: 'POST',
		dataType: 'json',
		url: "/Search/GetSmartBox",
		data: JSON.stringify(reqParams),
		contentType: "application/json",
		xhrFields: {
			withCredentials: true
		},
		success: function (result) {
			if(renderSmartBox(needOnlyTileSwap, result) === false) return;

			reqParams.smartBoxGroupType = '';
			//reqParams.tiles = result.TileHistory;
			reqParams.tiles = result.TileHistory.map(function (tile) { var copiedTile = $.extend(true, {}, tile); copiedTile.Selected = true; return copiedTile; });
		}
	});
}

var displaySmartBoxAtPageLoad = function () {
	var resultObj = ajaxHistory.getSmartBoxResult();
	if (resultObj && resultObj.initial) {
		if (resultObj.initial) renderSmartBox(false, resultObj.initial);
		if (resultObj.current) renderSmartBox(true, resultObj.current);
		//renderSelectedTileInSmartBoxMoreLayer(ajaxHistory.getReqParams().tiles);
		reqParams.smartBoxGroupType = '';
	} else {
		displaySmartBox();
	}
}

function displaySmartFinder() {
	if (returnData.SmartFinder != null && returnData.SmartFinder.HasSmartFinder) {
		var $tireBanner = $("#tire_banner");

		$tireBanner.show();
		$tireBanner.find("#tire_banner_link").attr("href", returnData.SmartFinder.BannerLandingUrl);
		$tireBanner.find("#tire_banner_link").data("pdslog", returnData.SmartFinder.PdsLogJson);
		$("#tire_banner_image").attr("src", returnData.SmartFinder.BannerImageUrl);

		if (returnData.SmartFinder.SearchOption.HasSearchOption) {
			var $wrapper = $("#smart_finder_option");

			var sizeText = " 외" + (returnData.SmartFinder.SearchOption.OptionValues.length - 1).toString() + "종";
			$wrapper.find("#s_finder_colored_title").text(returnData.SmartFinder.SearchOption.ColoredTitle);
			$wrapper.find("#s_finder_normal_title").text(returnData.SmartFinder.SearchOption.NormalTitle);
			$wrapper.find("#s_finder_colored_desc").text(returnData.SmartFinder.SearchOption.ColoredDescription);
			$wrapper.find("#s_finder_size").text(sizeText);
			$wrapper.find("#s_finder_option_name").html(returnData.SmartFinder.SearchOption.OptionName);
			if (menuName == "SRP") {
				$wrapper.find("#s_finder_layer").attr('onclick', "javascript:sendPDSLogBasic('200000029', 'Utility');");
			} else {
				$wrapper.find("#s_finder_layer").attr('onclick', "javascript:sendPDSLogBasic('200000092', 'Utility');");
			}
			var optionValueTemplate = Handlebars.compile($("#smart-finder-option-value-template").html());



			$wrapper.find("#s_finder_option_value_wrapper").html(optionValueTemplate(returnData.SmartFinder.SearchOption));

			if (reqParams.valueId != "") {
				var valueIdArray = reqParams.valueId.split(",");
				for (var i = 0; i < valueIdArray.length; i++) {
					$wrapper.find("input[value=" + valueIdArray[i] + "]").attr("checked", true);
				}
			}

			$wrapper.show();
		}
	} else {
		$("#tire_banner").remove();
	}
}

function displayPPList(data) {
	ajaxHistory.setPPList(data.SPPLists);
	if (searchEx.isDepartmentStore) {
		$('#PPListBoxDiv').hide();
		return;
	}
	var source = $('#pp-item-template').html();
	var template = Handlebars.compile(source);
	$('#PPListBoxDiv').html(template(data));
	if (reqParams.isBizOn == "Y") {
		$(".linebn_bizon").show();
	}
	else {
		$('#PPListBoxDiv').show();
	}
	bPPUpdate = true; 
}

function displayPPLogo() {
	if (returnData.SellCustNo != '' && $.isArray(returnData.SPPLists) && returnData.SPPLists.length > 0) {

		var pp = returnData.SPPLists[0];

		if (pp == null || pp == '' || typeof pp === 'undefined') {
			$('.bnr_area2').hide();
			$('.pp_bnr').hide();
			$('#promotionBanner').hide();
			return;
		}
		else {
			$('.bnr_area2').show();
			$('.pp_bnr').show();
		}

		if (pp.IsForSale) {
			$('.bnr_area2').hide();
			$('.pp_bnr').hide();
			$('#promotionBanner').show();
			$('#promotionBanner a').attr('href', pp.PRMTBannerUrl);
			$('#promotionBanner a').data('pdslog', pp.PRMTPdsLogJson);
			$('#promotionBanner a').attr('onclick', "javascript:sendPDSLogFromEliment(this);");
			$('#promotionBanner img').attr('src', pp.PRMTBannerImg);
		}
		else {
			$('.bnr_area2 #pp_img').attr('src', pp.ImageUrl);
			$('#pp_img').attr('src', pp.ImageUrl);
			$('#promotionBanner').hide();
		}

        $('.bnr_area2 #pp_name').html(pp.PartnerNm);
	}
	else {
		$('.bnr_area2').hide();
		$('.pp_bnr').hide();
	    $('#promotionBanner').hide();
	}
}

function displayKeywordBanner() {
    var existPP = false;
    if (returnData.SellCustNo != '' && $.isArray(returnData.SPPLists) && returnData.SPPLists.length > 0){
        var pp = returnData.SPPLists[0];
        if (pp == null || pp == '' || typeof pp === 'undefined') {
            existPP = false;
        } else {
            existPP = true; 
        }
    }

    if (existPP == false){
        var bannerInfo = returnData.KeywordBanner;
        if (bannerInfo == null || bannerInfo == '' || typeof bannerInfo === 'undefined') {
            $('.bnr_area').hide();
        } else {
            $('.bnr_area').show();
            $('.bnr_area a').attr('href', bannerInfo.LinkUrl);
            $('.bnr_area img').attr('src', bannerInfo.ImageUrl);
        }
    }
}

function displaySmartBoxAndBrnadFilter() {
	if (returnData) {
		var source = $('#brand-tpl-view-template').html();
		var template = Handlebars.compile(source);
		$('.detail_sch_lst').html(template(returnData));

		if (reqParams.isSmartDelivery == "Y") {
			$("#smart_delivery").prop('checked', true);
		} else {
			$("#smart_delivery").prop('checked', false);
		}

		if (reqParams.isBrand == "Y") {
			$("#brand_goods").prop('checked', true);
		} else {
			$("#brand_goods").prop('checked', false);
		}

		if (!returnData.IsShowSmartDeliveryFilter && !returnData.IsShowBrandOnlyFilter) {
			$("#atag_detail_layer_srp").addClass("off");
			$("#atag_detail_layer_srp").off("click");
			$(".detail_sch_lst").hide();
		} else {
			$("#atag_detail_layer_srp").show();
			$(".detail_sch_lst").show();
			$("#atag_detail_layer_srp").removeClass("off");
			$("#atag_detail_layer_srp").removeClass("on");
			$("#detail_layer_srp").hide();
		}

//		var isShowSmartDeliveryFilter = returnData.IsShowSmartDeliveryFilter;
//		var isShowBrandOnlyFilter = returnData.IsShowBrandOnlyFilter;

//		if (isShowSmartDeliveryFilter || isShowBrandOnlyFilter) {
//			$(".sort_smart").show();
//		} else {
//			$(".sort_smart").hide();
//		}

//		if (isShowSmartDeliveryFilter) {
//			$(".sort_smart > li").first().show();
//		} else {
//			$(".sort_smart > li").first().hide();
//		}
//		if (isShowBrandOnlyFilter) {
//			$(".sort_smart > li").last().show();
//		} else {
//			$(".sort_smart > li").last().hide();
//		}
	}
}

function displayItemList() {

	// 전체 갯수 출력
	//$('#TotalGoodsCount').html(addCommas(returnData.TotalGoodsCount));

	if (searchEx.isDepartmentStore) {
		var source = $('#departmetstore-item-template').html();
		var template = Handlebars.compile(source);
		$('.kind_artwrap').html(template(returnData));
	}
	else {
		var source = $('#item-template').html();
		var template = Handlebars.compile(source);
		$('.kind_artwrap').html(template(returnData));
		ad_layer('.kind_artwrap');
	}

	if (typeof returnData.Item === 'undefined' || returnData.Item == null || returnData.Item.length <= 0) {
		if (searchEx.isDepartmentStore) {
			displayDepartmentBestForNoResult();
		}
		else {
			$('#itemEmptyDiv').show();
			displayBestForNoResult();

			if (bFirstLoading) {
				$('#searchOptionDiv').hide();
				$('#PPListBoxDiv').hide();
				$('#divSmartBox').hide();
				$('#divTileHistory').hide();
			}
			else {
				$('#searchOptionDiv').show();
				//$('#PPListBoxDiv').hide();
			}
		}
	}
	else {
		$('#itemEmptyDiv').hide();
		$('.mbest_wrap').remove();
		$('#searchOptionDiv').show();
	}

	if (typeof lpAdBox == "function") {
		lpAdBox(); //front_v4의 포커스 상품 ? 버튼 이벤트 바인딩
	}

	SetPreventDefaultButton();
	//if (typeof btnToggleAction == "function") {
	//	btnToggleAction();
	//}

	$("img.thumbnail").lazyload();
	$(window).trigger('resize');
}

function displayBestForNoResult() {
	return;
	$.ajax({
		type: 'GET',
		data: {
			pageSize: 10
		},
		dataType: 'json',
		url: "/Search/GetBest100Items",
		contentType: "application/json",
		xhrFields: {
			withCredentials: true
		},
		success: function (result) {
			if (result && result.length) {
				var source = $('#best-item-template').html();
				var template = Handlebars.compile(source);
				$('#itemEmptyDiv').after(template(result));
				$("img.thumbnail").lazyload();
				$(window).trigger('resize');
			}
		},
		error: function (result) {
		}
	});
}

function displayDepartmentBestForNoResult() {
	$('section.sec_dept').remove();
	menuName === 'LP' && $('#divTileHistory').hide();
	$('#content').addClass('nodata');
	$.ajax({
		type: 'GET',
		dataType: 'json',
		url: "/DepartmentStore/GetDepartmentBestItem",
		contentType: "application/json",
		xhrFields: {
			withCredentials: true
		},
		success: function (result) {
			if (result && result.Item && result.Item.length) {
				var source = $('#departmetstore-best-item-template').html();
				var template = Handlebars.compile(source);
				$('#divDeptNoResult').after(template(result));
				$('<p class="noti_prd">이런 상품은 어떠세요?</p><h4 class="tit2">Best</h4>').prependTo($('section.sec_dept'));
				$('#divDeptNoResult').show();
				$("img.thumbnail").lazyload();
				$(window).trigger('resize');
			}
		},
		error: function (result) {
			$('div.paginate').remove();
			$('#divDeptNoResult').show();
		}
	});
}

function displayCategory(data) {
	if (searchEx.isDepartmentStore)
		return;

	if (data.ScId != undefined && data.ScId.length != 0) {
		$("#categoryDiv .cate_lst_lp li.on").remove();
		var $selected = $('<li class=\"on\"><a href=\"javascript:;\">' + data.ScIdName + '</a></li>');
		$("#categoryDiv .cate_lst_lp li").first().before($selected);

	} else if (menuName == "LP") {
		ajaxHistory.setCategoryData(data);
		var source = $('#category-template').html();
		var template = Handlebars.compile(source);
		$('#categoryDiv').html(template(data));
	}
}

function displaySmartBoxCategory(data) {
	if (data && data.TileMore) {
		smartBoxCategoryMoreData = data.TileMore;
		var source = $('#smartbox-lcategory-template').html();
		var template = Handlebars.compile(source);
		$('#cate_path').find('div.scroll_box').html(template(data));
	}
}

function displayBrandList(brandFinderList) {
	var $indicator = $('#brand_path').find('div.indicator');
	var $ul = $('#brand_path').find('ul.slide_lst');

	$ul.empty();
	$indicator.empty();

	if (!$.isArray(brandFinderList) || brandFinderList.length === 0) {
		return;
	}

	var $indicator = $('#brand_path').find('div.indicator');
	$indicator.attr('onclick', "javascript:sendPDSLogBasic('200000130','Utility');");
	var $ul = $('#brand_path').find('ul.slide_lst');
	var size = brandFinderList.length;
	var chosung = '', $indicatorItem, $group, $groupUl, $item, item;
	for (var i = 0; i < size; i++) {
		var moreItem = brandFinderList[i];

		if (chosung !== moreItem.ChoSung) {
			$ul.append($group);

			chosung = moreItem.ChoSung;
			$indicatorItem = $('<a href="#type' + encodeURIComponent(chosung).replace(/%/g, '') + '"><span>' + chosung + '</span></a>');
			$indicator.append($indicatorItem);

			$group = $('<li id="type' + encodeURIComponent(chosung).replace(/%/g, '') + '"><h4 class="grp_txt">' + chosung + '</h4><ul class="brand_logo"></ul></li>');
		}
		$item = $('<li id="' + moreItem.BrandNo + '" onclick="javascript:' + 'sendBrandLayerPdsLog(\'' + moreItem.BrandNo + '\');toggleBrandSelect(this);"><a href="javascript:;" ><img class="lazyload" data-original="' + moreItem.BrandonSubImg + '" alt="' + moreItem.BrandName.replace(/'/g, '\\\'') + '" src="http://pics.gmarket.co.kr/mobile/lpsrp/img_logo.gif"/></a></li>');
		$group.find('ul.brand_logo').append($item);
	}
	$ul.append($group);
	$ul.find("img.lazyload").lazyload({
		container: $('.slide_lst.brand')
	});
	$indicator.append($indicatorItem);

	/*
	if (typeof returnData.BrandFinderList === 'undefined' || 
		returnData.BrandFinderList == null || returnData.BrandFinderList.length <= 0) {
		$('#brandEmptyDiv').show();
		$('#wrapper2').hide();
		$('#brand_btn_area').hide();
	}
	else {
		$('#brandEmptyDiv').hide();
		$('#wrapper2').show();
		$('#brand_btn_area').show();

		var source = $('#brand-template').html();
		var template = Handlebars.compile(source);
		$('#brandListDiv').html(template(returnData));
	}
	*/
}

function toggleBrandSelect(obj) {
	if (obj) {
		var $obj = $(obj);
		if ($obj.hasClass("selected")) {
			$obj.removeClass("selected");
		} else {
			$obj.addClass("selected");
		}

		$("#brandListDiv").attr("update", "yes");
	}
}

function displayG9Box() {
	var $g9Box = $('#g9Box');

	if (g9BoxTimer != null) clearInterval(g9BoxTimer);

	if (returnData.G9BoxItem === 'undefined' || returnData.G9BoxItem == null || !returnData.G9BoxItem.HasG9BoxItem) {
		$g9Box.hide();
	} else {
		if (returnData.G9BoxItem.IsDisplayRemainTime) {
			g9BoxTimer = setInterval(function () { calculateG9RemainTime(returnData.G9BoxItem.CloseTime); }, 1000);
		} 

		var source = $('#g9-item-template').html();
		var template = Handlebars.compile(source);

		$g9Box.html(template(returnData.G9BoxItem));
		$g9Box.show();
	}
}

function calculateG9RemainTime(closeTimeStr) {
	var $remainTime = $('#remainTime');
	var closeTime = new Date(closeTimeStr);
	var remainTime = Math.round((new Date(closeTime.getUTCFullYear(), closeTime.getUTCMonth(), closeTime.getUTCDate(), closeTime.getUTCHours(), closeTime.getUTCMinutes(), closeTime.getUTCSeconds()) - new Date()) / 1000);
	
	var days = 0;
	var hours = 0;
	var minutes = 0;
	var seconds = 0;

	if (remainTime > 0) {
		days = Math.floor(remainTime / 86400);
		hours = Math.floor((remainTime - days * 86400) / 3600 % 24);
		minutes = Math.floor((remainTime - hours * 3600) / 60 % 60);
		seconds = Math.floor((remainTime - minutes * 60) % 60);
	} else {
		if (g9BoxTimer != null) clearInterval(g9BoxTimer);
	}
	
	var str = "<em class=\"sp_lpsrp\">시간</em>";
	str += days + "일 ";
	str += (hours > 9) ? hours : '0' + hours;
	str += ":";
	str += (minutes > 9) ? minutes : '0' + minutes;
	str += ":";
	str += (seconds > 9) ? seconds : '0' + seconds;
	$remainTime.html(str);
}

function displaySmartClickAds() {

	reqParams.startRank = eval(returnData.SmartClickCntAsPlus) + 1;  // set startRank
	reqParams.keywordSeqNo = returnData.KeywordSeqNo;

	$.ajax({
	    type: 'POST',
	    dataType: 'json',
	    url: "/Search/GetSmartClickItemsJson",
	    contentType: "application/json",
	    data: JSON.stringify(reqParams),
	    xhrFields: {
	        withCredentials: true
	    },
	    success: function (result) {
	        var items = result;

	        if (items == 'undefined' || items == null) {
	            return;
	        }

	        source = $('#cpcblock-item-template').html();
	        template = Handlebars.compile(source);
	        $('#cpcBlockList').html(template({ CPCBlockAdList: items }));
	        ad_layer('#cpcBlockList');

	    },
	    error: function (result) {
	    }
	});
}

function displayPowerClickAds() {

	reqParams.startRank = eval(returnData.BlockAdStartRank);  // set startRank
	reqParams.keywordSeqNo = returnData.KeywordSeqNo;

	$.ajax({
		type: 'POST',
		dataType: 'json',
		url: "/Search/GetPowerClickItemsJson",
		contentType: "application/json",
		data: JSON.stringify(reqParams),
		xhrFields: {
			withCredentials: true
		},
		success: function (result) {
			var items = result.Item;

			if (items == 'undefined' || items == null) {
				return;
			}

			source = $('#cpcblock-item-template').html();
			template = Handlebars.compile(source);
			$('#cpcBlockList').html(template({ CPCBlockAdList: items }));
			$('#cpcBlockList').find('.btn_addcart,.btn_zzim').on('click', function (e) {
				e.preventDefault();
			});
			sendImpressionLog(result.TrackingUrlList);
			ad_layer('#cpcBlockList');
		},
		error: function (result) {
		}
	});
}

function displaySponsorLinks(channel, count, primeKeyword, moreKeyword, largeCategory, middleCategory, smallCategory) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Ad/GetSponsorLinkList",
        contentType: "application/json",
        data: JSON.stringify({
            channel: channel,
            count: count,
            primeKeyword: primeKeyword,
            moreKeyword: moreKeyword,
            largeCategory: largeCategory,
            middleCategory: middleCategory,
            smallCategory: smallCategory,
            referrer: document.referrer
        }),
        success: function (result) {
            if (result == null || result == "undefined" || result.length == 0) {
                return;
            }

            var source = $("#sponsor-link-view-template").html();
            var template = Handlebars.compile(source);
            $("#srpSponsorLinkList").html(template({ Items: result }));
            $("#srpSponsorLinkList").show();
            ad_layer("#srpSponsorLinkList");
        },
        error: function (result) {

        }
    });
}

function sendImpressionLog(trackingUrlList) {
	if (trackingUrlList != undefined && trackingUrlList.length != undefined && trackingUrlList.length > 0) {
		for (var i = 0; i < trackingUrlList.length; i++) {
			sendPdsUrlDirect(trackingUrlList[i]);
		}
	}
}

function sendClickLog(pdsClickUrl) {
	if (pdsClickUrl != undefined && pdsClickUrl != null && pdsClickUrl.length > 0) {
		sendPdsUrlDirect(pdsClickUrl);
	}
}

function sendPdsUrlDirect(psdUrl) {
	var url = psdUrl;

	if (url != undefined && url != "") {
		url = url.replace("http://pds.gmarket.co.kr", "");
		url = url.replace("https://pds.gmarket.co.kr", "");
		if (typeof ubprofiler === 'object' && typeof ubprofiler.send === 'function' && url != "") {
			ubprofiler.sendDirect(url);
		}
	}
}

function getSRPClickLogUrl(item, focusCount) {
	var result = "";
	return new Handlebars.SafeString(result);

	var itemGroup = getItemGroupInfo(item.ListingItemGroup);
	if (!itemGroup) {
		new Handlebars.SafeString("");
	}

	var totalRank = item.TotalSearchResultRank;
	var areaRank = item.TotalSearchResultRank;

	if (itemGroup.type == "general") {
		areaRank = totalRank - focusCount;
		if (areaRank < 0) {
			areaRank = totalRank;
		}
	}

	switch (itemGroup.type) {
		case "power":
		case "plus":
		case "smart":
			result = "sendSRPClickLog('" + itemGroup.areaCode + "','" + item.GoodsCode + "');";
			break;
		case "focus":
		case "general":
			result = "sendSRPClickLog('" + itemGroup.areaCode + "','" + item.GoodsCode + "','" + areaRank + "','" + totalRank + "');";
			break;
		default:
			break;
	}

	return new Handlebars.SafeString(result);
}

function sendSRPClickLog(channelCode, goodsCode, areaRank, totalRank) {
	var areaValue;
	if (typeof goodsCode !== "undefined" && goodsCode != null) {
		areaValue = {};
		areaValue.goodscode = goodsCode;
	}
	if (typeof areaRank !== "undefined" && areaRank != null) {
		if (typeof areaValue === "undefined") {
			areaValue = {};
		}
		areaValue.area_rank = areaRank;
	}
	if (typeof totalRank !== "undefined" && totalRank != null) {
		if (typeof areaValue === "undefined") {
			areaValue = {};
		}
		areaValue.total_rank = totalRank;
	}

	try {
		if (typeof pvprofiler !== "undefined" && typeof pvprofiler.sendEvt !== "undefined")
			pvprofiler.sendEvt("click", channelCode, "button", areaValue);
	}
	catch (ex) { }
}


//function sendPDSLogFromSmartBoxTab(tag) {
//	if (!tag) return;
//	var $this = $(tag);
//	var pdsData = $this.data('pdslog');

//	if (typeof pdsData !== "undefined") {
//		if (typeof pdsData == "object") {
//			if ($("#divNavFinder").hasClass("fixed")) {
//				pdsData.Parameter.smart_box_openclose = "close";
//			} else {
//				pdsData.Parameter.smart_box_openclose = "open";
//			}
//			sendPDSLog(pdsData);
//		} 
//	}
//}

function sendPDSLogBasic(areaCode, areaType) {
	if (areaCode === "undefined" || areaType === "undefined")
		return;

	var pdsData = {};
	pdsData.AreaCode = areaCode;
	pdsData.AreaType = areaType;
	pdsData.Parameter = {};

	sendPDSLog(pdsData);
}

function sendPDSLogItemAdArea(listingItemGroup, isOpen) {
	var pdsData = {};
	pdsData.AreaType = "Utility";
	pdsData.Parameter = {};

	var isSendLog = true;
	var isSrp = reqParams.menuName === "SRP" ? true : false;
	switch (listingItemGroup) {
		case 1: // 플러스
			if (isSrp) {
				if (isOpen) {
					pdsData.AreaCode = "200000030";
				} else {
					pdsData.AreaCode = "200000031";
				}
			} else {
				if (isOpen) {
					pdsData.AreaCode = "200000093";
				} else {
					pdsData.AreaCode = "200000094";
				}
			}
			break;
		case 3: // 포커스
			if (isSrp) {
				if (isOpen) {
					pdsData.AreaCode = "200000044";
				} else {
					pdsData.AreaCode = "200000045";
				}
			} else {
				if (isOpen) {
					pdsData.AreaCode = "200000107";
				} else {
					pdsData.AreaCode = "200000108";
				}
			}
			break;
		case 6: // 파워클릭
			if (isSrp) {
				if (isOpen) {
					pdsData.AreaCode = "200000035";
				} else {
					pdsData.AreaCode = "200000036";
				}
			} else {
				if (isOpen) {
					pdsData.AreaCode = "200000098";
				} else {
					pdsData.AreaCode = "200000099";
				}
			}
			break;
		default:
			isSendLog = false;
			break;
	}

	if (isSendLog) {
		sendPDSLog(pdsData);
	}

}

function sendPDSLogFromPPAll() {
	var pdsData = {};
	pdsData.AreaType = "Utility";
	pdsData.Parameter = {};
	pdsData.Parameter.asn = "0";
	pdsData.Parameter.pp = "all";

	if (reqParams.menuName === "SRP") {
		pdsData.AreaCode = "200000025";
		pdsData.Parameter.keyword = reqParams.primeKeyword;
	} else {
		pdsData.AreaCode = "200000088";
		if (reqParams.scId !== "undefined" && reqParams.scId !== null && reqParams.scId !== "") {
			pdsData.Parameter.category_code = reqParams.scId;
		} else {
			pdsData.Parameter.category_code = reqParams.mcId;
		}
	}

	sendPDSLog(pdsData);
}

function sendPDSLogFromSortArea(type, data, toViewStyle) {
	if (type === "undefined")
		return;

	var pdsData = {};
	pdsData.AreaType = "Utility";
	pdsData.Parameter = {};

	var isSrp = reqParams.menuName === "SRP" ? true : false;
	if (type == "sort") {
		// 정렬
		if (isSrp) {
			pdsData.AreaCode = "200000017";
			pdsData.Parameter.keyword = data;
		} else {
			pdsData.AreaCode = "200000080";
			pdsData.Parameter.category_code = data;
		}
	} else if (type == "view") {
		// ViewType 변경
		if (isSrp) {
			pdsData.AreaCode = "200000023";
		} else {
			pdsData.AreaCode = "200000086";
		}

		if (data) {
			pdsData.Parameter.category_code = data;
		}
		if (toViewStyle) {
			if (toViewStyle == "List") {
				pdsData.Parameter.view_type = "L";
			} else {
				pdsData.Parameter.view_type = "G";
			}
		}
	} else if (type == "detail") {
		// 상세 검색
		if (isSrp) {
			pdsData.AreaCode = "200000024";
		} else {
			pdsData.AreaCode = "200000087";
		}
	}

	sendPDSLog(pdsData);
}


function sendPDSLogFromItemButton(obj, type, listingItemGroup, isBottomCPC) {
	if (!obj) return;
	var $this = $(obj);

	var pdsData = $this.parent().parent().parent().data('pdslog');
	var isSrp = reqParams.menuName === "SRP" ? true : false;

	if (typeof pdsData !== "undefined" && pdsData.AreaCode !== "undefined") {
		if (type == "zzim") {
			pdsData.AreaType = "Utility";
			switch (listingItemGroup) {
				case 1: // 플러스
					if (isSrp) {
						pdsData.AreaCode = "200000033";
					} else {
						pdsData.AreaCode = "200000096";
					}
					break;
				case 2: // 스마트배송
					if (isSrp) {
						pdsData.AreaCode = "200000042";
					} else {
						pdsData.AreaCode = "200000105";
					}
					break;
				case 3: // 포커스
					if (isSrp) {
						pdsData.AreaCode = "200000047";
					} else {
						pdsData.AreaCode = "200000110";
					}
					break;
				case 4: // 일반
					if (isSrp) {
						pdsData.AreaCode = "200000050";
					} else {
						pdsData.AreaCode = "200000113";
					}
					break;
				case 5: // Bizon
					pdsData.AreaCode = "200000258";
					break;
				case 6: // 파워클릭
					if (isSrp) {
						if (isBottomCPC) { // 하단
							pdsData.AreaCode = "200000059";
						} else { // 상단
							pdsData.AreaCode = "200000038";
						}
					} else {
						if (isBottomCPC) { // 하단
							pdsData.AreaCode = "200000122";
						} else {
							pdsData.AreaCode = "200000101";
						}
					}
					break;
				default:
					break;
			}
		} else if (type == "addcart") {
			switch (listingItemGroup) {
				case 1: // 플러스
					if (isSrp) {
						pdsData.AreaCode = "200000034";
					} else {
						pdsData.AreaCode = "200000097";
					}
					break;
				case 2: // 스마트배송
					if (isSrp) {
						pdsData.AreaCode = "200000043";
					} else {
						pdsData.AreaCode = "200000106";
					}
					break;
				case 3: // 포커스
					if (isSrp) {
						pdsData.AreaCode = "200000048";
					} else {
						pdsData.AreaCode = "200000111";
					}
					break;
				case 4: // 일반
					if (isSrp) {
						pdsData.AreaCode = "200000051";
					} else {
						pdsData.AreaCode = "200000114";
					}
					break;
				case 5: // Bizon
					pdsData.AreaCode = "200000259";
					break;
				case 6: // 파워클릭
					if (isSrp) {
						if (isBottomCPC) { // 하단
							pdsData.AreaCode = "200000060";
						} else { // 상단
							pdsData.AreaCode = "200000039";
						}
					} else {
						if (isBottomCPC) { // 하단
							pdsData.AreaCode = "200000123";
						} else { // 상단
							pdsData.AreaCode = "200000102";
						}
					}
					break;
				default:
					break;
			}
		}
		sendPDSLog(pdsData);
	}
}


function sendPDSLogFromEliment(tag,haslink) {
	if (!tag) return;
	var $this = $(tag);
	var pdsData = $this.data('pdslog');

	if (typeof pdsData !== "undefined") {
		if (typeof pdsData == "object") {
			sendPDSLog(pdsData);
		} else {
			try {
				var pdslog = JSON.parse(pdsData);
				sendPDSLog(pdslog);
			} catch (ex) {}
		}
	}

	if (haslink) {
		var linkUrl = $this.data('link');
		if (linkUrl && linkUrl.length > 0) {
			location.href = linkUrl;
		}
	}
}

function sendPDSLog(data) {
	if (!data)
		return;

	try {
		if (typeof pvprofiler !== "undefined" && typeof pvprofiler.sendEvt !== "undefined")
			pvprofiler.sendEvt("click", data.AreaCode, data.AreaType, data.Parameter);
	}
	catch (ex) { }
}

function brandSearch() {
	var brandAr = $('#brandListDiv .brand_logo > li.selected');
	var brandList = '';

	if ($("#brandListDiv").attr("update") == "yes") {
		for (var i = 0; i < brandAr.length; i++) {
			var liElement = $(brandAr[i]);

			if (i < brandAr.length - 1)
				brandList += liElement.attr('id') + ',';
			else
				brandList += liElement.attr('id');
		}
		reqParams.brandList = brandList;

		$("#brandListDiv").attr("update", "no");
		bForceTopMove = true;

		search(true);
	}
}

function deselectBrands() {
	$('#brandListDiv input[type=checkbox]:checked').attr('checked', false);
}

function smartFinderSearch() {
	var $checkedInput = $("#s_finder_option_value_wrapper input[type=checkbox]:checked");
	if ($checkedInput.length > 0) {
		var valueIdArray = [];
		$checkedInput.each(function () {
			valueIdArray.push($(this).val());
		});

		reqParams.valueId = valueIdArray.join(",");
	} else {
		reqParams.valueId = "";
	}

	var pdsData = {};
	pdsData.AreaType = "Utility";
	pdsData.Parameter = {};
	pdsData.Parameter.car = reqParams.sClassSeq;
	pdsData.Parameter.attr_code = encodeURIComponent(reqParams.valueId);

	if (reqParams.menuName === "SRP") {
		pdsData.AreaCode = "200000076";
		pdsData.Parameter.keyword = reqParams.primeKeyword;
	} else {
		pdsData.AreaCode = "200000138";
		if (reqParams.scId !== "undefined" && reqParams.scId !== null && reqParams.scId !== "") {
			pdsData.Parameter.category_code = reqParams.scId;
		} else {
			pdsData.Parameter.category_code = reqParams.mcId;
		}
	}
	sendPDSLog(pdsData);
	search(true);

	closeSortLayer();
}

function deselectSmartFinder() {
	$("#s_finder_option_value_wrapper input[type=checkbox]:checked").attr("checked", false);
}

function renderSelectSortOption(sortType) {
	if (sortType && sortType !== "") {
		$('#sortOption li').attr('class', '');
		$('#' + sortType).attr('class', 'selected');
	}
}

function selectSortOption(sortType) {
	renderSelectSortOption(sortType);
	reqParams.sortType = sortType;

	bForceTopMove = true;

	closeSortLayer();
}

function deselectSortOption() {
	$('#sortOption li').attr('class', '');
}

function selectPPAll() {
	ajaxHistory.setSelectedPP('', -1);
	reqParams.sellCustNo = '';	
	$('#ppList li').attr('class', '');
	$('#pp_all').attr('class', 'selected');

	$('.bnr_area').hide();
	closePPLayer();

	$('#sortOption li').removeClass('selected');
	$('#sortOption li').first().addClass('selected');
}

function selectPP(pp, ppIndex) {
	//initReqParams();	
	ajaxHistory.setSelectedPP(pp, ppIndex);
	reqParams.sellCustNo = pp;	
	bPPUpdate = false;

	$('#pp_all').removeClass("selected");

	$('#ppList li').not('.intotal').attr('class', '');
	$('#pp_' + ppIndex).attr('class', 'selected');

	closePPLayer();
	$('.bnr_area').hide();

	//var index = $('#ppList li:visible').index($('#pp_' + ppIndex));

	//if (index <= 3) return;

	//for (var i = 0; i < ppIndex; i++) {
	//	$('#pp_' + i).hide();
	//}
}

function sendCategoryLayerPdsLog(code) {
	var pdsData = {};

	pdsData.Parameter = {};
	pdsData.AreaType = "Utility";

	if (reqParams.menuName === "LP") {
		pdsData.AreaCode = "200000132";
		pdsData.Parameter.category_code = code;
		sendPDSLog(pdsData);
	} 
}

function selectCategory(level, code, name) {
	if (level == 'L') {
		reqParams.lcId = code;
		reqParams.mcId = '';
		reqParams.scId = '';
	}
	if (level == 'M') {
		reqParams.lcId = '';
		reqParams.mcId = code;
		reqParams.scId = '';
	}
	if (level == 'S') {
		reqParams.lcId = '';
		reqParams.mcId = '';
		reqParams.scId = code;
	}

	if (name) {
		gmHeader.setAppHeaderTitle(name)
	}

	reqParams.pageNo = 1;
	$('#divSmartBoxTile').hide();

	if (reqParams.menuName === "SRP") {
		$('#divSmartBoxTab').find('a').hide();
	}

	bForceTopMove = true;
}

function setCategoryParams(lcId, mcId, scId) {
	reqParams.lcId = lcId;
	reqParams.mcId = mcId;
	reqParams.scId = scId;
	if (!lcId && !mcId && !scId && reqParams.menuName === 'SRP') {
		reqParams.tiles = [];
		reqParams.sellCustNo = "";
		displaySmartBox();
	}
}

function displayViewStyle() {
	if (currentViewStyle == 'Gallery') {
		$("#gallery_view_style").show();
		$("#list_view_style").hide();
	} else if (currentViewStyle == 'List') {
		$("#list_view_style").show();
		$("#gallery_view_style").hide();
	}
}

function changeViewStyle(toViewStyle) {

//	$('.last .sp_mw.ico_view').hide();
//	$('.last .sp_mw.ico_view2').hide();
//	$('.last .sp_mw.ico_view3').hide();

	if (toViewStyle == 'List' || (currentViewStyle == 'Gallery' && !toViewStyle)) {
		currentViewStyle = 'List';
//		$('.last .sp_mw.ico_view3').show();
		
	}
	else if (toViewStyle == 'Image' || (currentViewStyle == 'List' && !toViewStyle)) {
		currentViewStyle = 'Image';
//		$('.last .sp_mw.ico_view').show();
	}
	else if (toViewStyle == 'Gallery' || (currentViewStyle == 'Image' && !toViewStyle)) {
		currentViewStyle = 'Gallery';
//		$('.last .sp_mw.ico_view2').show();
	}

	displayViewStyle();
	ajaxHistory.setCurrentViewStyle(currentViewStyle);
	ajaxHistory.saveState();
	displayItemList();
}

function printItemCSSStyle() {
	if (currentViewStyle == 'Gallery') {
		if (returnData.IsDetailSearch || veryFirstItemGroup == 4) return 'best_lst type type2';
		return 'best_lst type';
	}
	else if (currentViewStyle == 'List') {
		return 'best_lst type_list';
	}
	else if (currentViewStyle == 'Image') {
		return 'best_lst best_lst3';
	}

	return 'best_lst';
}

function printPlusItemCSSStyle(isPlusAD, anotherTypeStart) {
	if (currentViewStyle == 'List' && isPlusAD && !anotherTypeStart) {
		if (returnData.IsDetailSearch != undefined && returnData.IsDetailSearch)
			return 'plus_list v2'
		return 'plus_list';
	}

	return '';
}

function printAddCartCSSStyle(item) {
	var isDisplaySellerInfo = item.IsPurchasedSeller || item.ShopGroupCode === 14;
	var isDisplayDeliveryInfo = item.Delivery.ShowDeliveryInfo && item.ShopGroupCode !== 14;

	if (!item.IsCartVisible) return "";

	if (isDisplaySellerInfo && isDisplayDeliveryInfo) {
		return "type3";
	} else if (isDisplaySellerInfo || isDisplayDeliveryInfo) {
		return "type2";
	} else {
		return "";
	}
}

function detailSearch() {

	if ($('#lp_free_delivery').is(':checked'))
		reqParams.isShippingFree = 'Y';
	else
		reqParams.isShippingFree = 'N';

	if ($('#lp_smart_delivery').is(':checked'))
		reqParams.isSmartDelivery = 'Y';
	else
		reqParams.isSmartDelivery = 'N';

	if ($('#mileage').is(':checked'))
		reqParams.isMileage = 'Y';
	else
		reqParams.isMileage = 'N';

	if ($('#discount').is(':checked'))
		reqParams.isDiscount = 'Y';
	else
		reqParams.isDiscount = 'N';

	if ($('#onlybrand').is(':checked')) {
		reqParams.isBrand = "Y";
	} else {
		reqParams.isBrand = "N";
	}

	if ($('#stamp').is(':checked'))
		reqParams.isStamp = 'Y';
	else
		reqParams.isStamp = 'N';

	var moreKeyword = $('#lp_moreKeyword').val();
	if (moreKeyword == "검색어를 입력해 주세요.") {
		moreKeyword = "";
	}

	reqParams.moreKeyword = moreKeyword;
	reqParams.minPrice = $('#minPrice').val();
	reqParams.maxPrice = $('#maxPrice').val();

	bForceTopMove = true;

	if (reqParams.menuName === "LP") {
		var pdsData = {};
		pdsData.AreaType = "Utility";
		pdsData.AreaCode = "200000135";
		pdsData.Parameter = {};
		if (reqParams.scId !== "undefined" && reqParams.scId !== null && reqParams.scId !== "") {
			pdsData.Parameter.category_code = reqParams.scId;
		} else {
			pdsData.Parameter.category_code = reqParams.mcId;
		}

		pdsData.Parameter.price_range_from = reqParams.minPrice;
		pdsData.Parameter.price_range_to = reqParams.maxPrice;
		pdsData.Parameter.add_keyword = reqParams.moreKeyword;
		pdsData.Parameter.free_delivery = reqParams.isShippingFree;
		pdsData.Parameter.smart_delivery = reqParams.isSmartDelivery;
		pdsData.Parameter.brand_product = reqParams.isBrand;
		pdsData.Parameter.discounted_product = reqParams.isDiscount;
		pdsData.Parameter.smile_cash = reqParams.isMileage;

		sendPDSLog(pdsData);
	}
	search(false);

	$("#closeBtnDetailLayer").trigger("click");
}

function detailSmartBoxSearch() {

	if ($('#free_delivery').is(':checked'))
		reqParams.isShppingFree = 'Y';
	else
		reqParams.isShippingFree = 'N';

	if ($('#smart_delivery').is(':checked'))
		reqParams.isSmartDelivery = 'Y';
	else
		reqParams.isSmartDelivery = 'N';

	reqParams.moreKeyword = $('#moreKeyword').val();

	search(true);
}

function printLargeImageVisibility() {

	if (currentViewStyle == 'Image') {
		return '';
	}
	else
		return 'display:none';
}

function printImage(item, index) {

	var imgSrc = '';

	if (currentViewStyle == 'Image') {
		imgSrc = returnData.Item[index].LargeImageList[0];
	}
	else
		imgSrc = item.ImageURL || item.ImageUrl;

	if (imgSrc == null || imgSrc == '')
		return defaultImage;

	return imgSrc;
}

function isDisplayItemGroup() {
	return (currentViewStyle == 'Gallery' || currentViewStyle == 'List');
}

function getItemGroupStyle() {
	var itemGroupStyle = { divClass: '', h3Class: '' };

	itemGroupStyle.divClass = "best_tit";
	itemGroupStyle.h3Class = "best_stit";

	return itemGroupStyle;
}

function getItemGroupInfo(listingItemGroup) {
	var itemGroupInfo = { type: '', title: '', displayAdIcon: false, areaCode: '', description:'' };

	switch (listingItemGroup) {
	    case 1:
	        itemGroupInfo.type = 'plus';
	        itemGroupInfo.title = '플러스상품';
	        itemGroupInfo.displayAdIcon = true;
	        itemGroupInfo.areaCode = 'CSP0G002';
	        itemGroupInfo.description = '플러스 상품 광고를 구매한 상품으로 입찰가 순으로 전시됩니다.';
	        break;
		case 2:
			itemGroupInfo.type = 'smart';
			itemGroupInfo.title = '스마트 배송 상품';
			itemGroupInfo.displayAdIcon = false;
			itemGroupInfo.areaCode = 'CLP0Q025';
			break;
        case 3:
            itemGroupInfo.type = 'focus';
            itemGroupInfo.title = '포커스상품';
            itemGroupInfo.displayAdIcon = true;
            itemGroupInfo.areaCode = 'CSP0G003';
            itemGroupInfo.description = '포커스 광고를 구매한 상품으로 G마켓 랭크순으로 전시됩니다.';
            break;
		case 4:
			itemGroupInfo.type = 'general';
			itemGroupInfo.title = '일반 상품';
			itemGroupInfo.displayAdIcon = false;
			itemGroupInfo.areaCode = 'CSP0G001';
			break;
		case 5:
			itemGroupInfo.type = 'mileage';
			itemGroupInfo.title = 'Smile Point + 0.3%';
			itemGroupInfo.displayAdIcon = false;
			break;
        case 6:
            itemGroupInfo.type = 'power';
            itemGroupInfo.title = '파워클릭';
            itemGroupInfo.displayAdIcon = true;
            itemGroupInfo.areaCode = 'CSP0E001';
            itemGroupInfo.description = '파워클릭 광고를 구매한 상품으로 입찰가순으로 전시됩니다.';
            break;
	}

	return itemGroupInfo;
}

function showNextImage(seq) {

	var liElement = $('.best_lst li[seq="' + seq + '"]');

	var moreImageCnt = liElement.attr('moreimagecnt');
	moreImageCnt = parseInt( moreImageCnt );

	var imageIndex = liElement.find('#moreImageIndex').html();
	imageIndex = parseInt(imageIndex);

	var item = returnData.Item[seq];

	if (imageIndex == item.LargeImageList.length)
		return;

	liElement.find('#moreImageIndex').html(imageIndex + 1);
	liElement.find('#itemImage').attr('src', item.LargeImageList[imageIndex]);

	liElement.find('.btn_prev').show();

	if (imageIndex == moreImageCnt - 1)
		liElement.find('.btn_next').hide();
}

function showPreviousImage(seq) {

	var liElement = $('.best_lst li[seq="' + seq + '"]');

	var moreImageCnt = liElement.attr('moreimagecnt');
	moreImageCnt = parseInt(moreImageCnt);

	var imageIndex = liElement.find('#moreImageIndex').html();
	imageIndex = parseInt(imageIndex) - 1;

	if (imageIndex < 0) return;

	var item = returnData.Item[seq];

	liElement.find('#moreImageIndex').html(imageIndex);
	liElement.find('#itemImage').attr('src', item.LargeImageList[imageIndex - 1]);

	liElement.find('.btn_next').show();

	if (imageIndex == 1)
		liElement.find('.btn_prev').hide();
}

function directCart(url) {
	$("#directCartIframe").remove();
	var iframe = document.createElement('iframe');
	iframe.setAttribute("src", url);
	iframe.setAttribute("id", "directCartIframe");
	iframe.style.width = 0 + "px";
	iframe.style.height = 0 + "px";
	iframe.style.display = 'none';
	document.body.appendChild(iframe);
}

function addCartBaro(goodsCode, btn, directCartUrl) {

	if (isLogin == false) {
		alert("로그인이 필요합니다.");
		var currentUrl = location.href;
		location.href = loginUrl;
		return;
	}

	var $button = $(btn);

	var param = { itemNo: goodsCode,
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
				//animation
				addCartAni(btn);
				//result.data.CartCount;
				//result.data.CartPid
				updateCartCount(result.data.CartPid);
				if (directCartUrl && directCartUrl.length > 0) {
					directCart(directCartUrl);
				}
				$button.addClass("on");
			} else {
				alert(result.message);
			}
		},
		error: function (result) {
			alert("장바구니 담기에 실패했습니다.");
		}
	});
}

function updateCartCount(pid) {
	if (window.cartCountController) {
		// api에서 count 내려주지만 장바구니에 담긴 품절 상품은 포함되지 않은 count라서 setCount 하지 않고 pid로 sync
		window.cartCountController.syncAfterAddCart(1, pid);
	}
}

function startLoading() {
	$('#itemLoading').show();
	$('#divSmartBox').hide();
	$('.pp_bnr').hide();
	$('#searchOptionDiv').hide();
	$('.kind_artwrap').hide();
	$('#cpcBlockList').hide();
	$('#itemEmptyDiv').hide();
	$('#PPListBoxDiv').hide();
}

function endedLoading() {
	$('#itemLoading').hide();
	$('#divSmartBox').show();
	$('.pp_bnr').show();
	$('#searchOptionDiv').show();
	$('.kind_artwrap').show();
	$('#cpcBlockList').show();
	$('#PPListBoxDiv').show();
}

function toggleSearchOption() {
	try {
		
		if (reqParams.brandList == '')
			$('#tb_btn_brand').attr('class', '');
		else
			$('#tb_btn_brand').attr('class', 'selected');

		if (reqParams.menuName == 'SRP') {

			if (reqParams.lcId == '')
				$('#tb_btn_category').attr('class', '');
			else
				$('#tb_btn_category').attr('class', 'selected');

			if (reqParams.sortType == '' || reqParams.sortType == 'ACCURACY_LQS')
				$('#tb_btn_sort').attr('class', '');
			else 
				$('#tb_btn_sort').attr('class', 'selected');

			$("#a_sort_layer_lpsrp").removeClass("on");
			$("#sort_layer_lpsrp").hide();
		}
		else if (reqParams.menuName == 'LP') {

			$('#tb_btn_category').attr('class', 'selected');

			if (reqParams.sortType == '' || reqParams.sortType == 'PREMIUM_RANK_LQS') 
				$('#tb_btn_sort').attr('class', '');
			else 
				$('#tb_btn_sort').attr('class', 'selected');
		}

		if (reqParams.moreKeyword == '' && reqParams.minPrice == 0 && reqParams.maxPrice == 0 &&
			reqParams.isShippingFree == 'N' && reqParams.isMileage == 'N' && reqParams.isDiscount == 'N' &&
			reqParams.isStamp == 'N' && reqParams.isSmartDelivery == 'N' && reqParams.isBrand == 'N') {
			$('#tb_btn_detail').attr('class', '');
			$(".sort_area .detail").removeClass("on");
			$(".btn_sort.detail").removeClass("selected");
		}
		else {
			$('#tb_btn_detail').attr('class', 'selected');
			$(".sort_area .detail").addClass("on");
			$(".btn_sort.detail").addClass("selected");
		}

		var brandList = reqParams.brandList.split(',');
		for (var i = 0; i < brandList.length; i++) {
			
			if (brandList[i] == null || brandList[i] == '') continue;

			//$('#chk_' + brandList[i]).prop('checked', true);
			$('#' + brandList[i]).addClass('selected');
		}
	}
	catch (ex) {
	}
}

function resetDetailSearchOption() {
	$('#lp_moreKeyword').val('');
	$('#minPrice').val('');
	$('#maxPrice').val('');
	$('input[name=del_type]').prop('checked', false);
	$('#tb_btn_detail input[type=checkbox]').prop('checked', false);
	$('.benefit_lst input[type=checkbox]').prop('checked', false);
	$('#delivery_none').prop('checked', true);
}

function displayCategoryInfo()
{
	if (reqParams.menuName == 'LP') {
		if (returnData.LcIdName != '') {
			$('#lcIdName').html(returnData.LcIdName);
		}

		if (returnData.McIdName != '') {
			$('#mcIdName').html(returnData.McIdName);
		}

		if (returnData.ScIdName != '') {
			$('#scIdNameArrow').show();
			$('#scIdName').show();
			$('#scIdName').html(returnData.ScIdName);
			$('#mcIdName').removeClass('cate_txt');
			$('#mcIdName').addClass('kind_cate');
		}
		else {
			$('#scIdNameArrow').hide();
			$('#scIdName').hide();
			$('#scIdName').html('');
		}
	}
}

function displayLpNavFinder() {
	var brandAr = $('#brandListDiv .brand_logo > li.selected');
	if (brandAr && brandAr.length > 0) {
		$("#lp_brand_layer").addClass("selected");
	} else {
		$("#lp_brand_layer").removeClass("selected");
	}
	if (returnData.BrandFinderList === "undefined" || returnData.BrandFinderList.length == 0) {
		$("#lp_brand_layer").find('span').addClass("off");
	} else {
		$("#lp_brand_layer").find('span').removeClass("off");
	}
}

function forceTopMove() {
	if (bForceTopMove) {
		bForceTopMove = false;
		$("html, body").animate({ scrollTop: 0 }, 300);
	}
}

function displaySearchOrderInfo() {
	//var isHide = $("#ACCURACY_LQS").hasClass('selected');
	//if (isHide) {
	//	$(".sort_layer .btn_info").hide();
	//} else {
	//	$(".sort_layer .btn_info").show();
	//}
}

function SetPreventDefaultButton() {
	var $btn = $('.btn_addcart,.btn_zzim');
	$btn.on('click', function (e) {
		e.preventDefault();
	});
}

function AddFavoriteItem(goodsCode, obj) {
	if (isLogin == false) {
		alert("로그인이 필요합니다.");
		var currentUrl = location.href;
		location.href = loginUrl;
		return;
	}

	var $button = $(obj);

	$.ajax({
		type: "GET",
		url: "/Common/AddFavoriteItems",
		dataType: "json",
		data: {
			itemNos: goodsCode
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
					$button.addClass("on");
					addInterAni();
				}
				else if (result.result != null && result.result < 0) {
					alert(result.msg);
					if ($button && $button.hasClass("on")) {
						$button.removeClass("on");
					}
					if (result.result == -100) {
						location.href = (loginUrl) ? loginUrl : "http://mobile.gmarket.co.kr/Login/login";
					}
				}
				else {
					alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
					if ($button && $button.hasClass("on")) {
						$button.removeClass("on");
					}
					return;
				}
			}
		}
	});
}
// 코더분 js
// -- start --
function smartBoxLCategoryAUpdate(obj) {
	var $this = $(obj);
	if ($this.parent().parent().has('.dep2').length > 0) {
		if ($this.parent().parent().hasClass('selected') == true) {
			$this.parent().parent().removeClass('selected');
			$('.cate_lst > li').css('padding-bottom', 0);
			$this.parent().next().removeClass('dep_selected');
			$this.parent().next().find('li').removeClass('selected');
			$this.parent().next().find('ul').removeClass('dep_selected');
		} else {
			$('.cate_lst > li').css('padding-bottom', 0);
			$('.cate_lst > li').removeClass('open');
			$this.parent().parent().addClass('selected');
			$('.cate_lst > .selected').find('button').show();
		}
		$('.cate_lst > li').removeClass('open');
	} else {
		$this.parent().parent().toggleClass('selected');
		$('.cate_lst > li').css('padding-bottom', 0);
		$('.cate_lst > li').removeClass('open');
	}
}

function smartBoxLCategoryButtonUpdate(obj) {
	var $this = $(obj);
	$('.cate_lst > li').removeClass('open');
	$('.cate_lst > li').css('padding-bottom', 0);
	$('.cate_lst > .selected').find('button').show();
	$this.parent().parent().addClass('open');

	$('.cate_lst li.open .dep3').each(function () {
		if ($this.hasClass('dep_selected2')) { $this.show(); }
		else { $this.hide(); $this.prev().removeClass('line'); }
	});

	if ($this.parent().parent().hasClass('open') == true) {
		var posDep2 = $this.parent().height() + $this.parent().position().top;
		var heightDep2 = $this.parent().height() + $this.parent().next().height();
		$this.parent().next().css('top', posDep2);
		$this.parent().parent().css('padding-bottom', heightDep2);
		if ($this.parent().parent().find('.dep3').hasClass('dep_selected2')) {
			var heightDep6 = $('.cate_lst li.open > span > a').height() + $('.cate_lst li.open .dep2').innerHeight() + $('.cate_lst li.open li.selected .dep3').innerHeight();
			$this.parent().parent().css('padding-bottom', heightDep6);
		}
	}

	if ($('.cate_lst li:nth-child(2n)').hasClass('open') == true) {
		if ($this.parent().next().find('> li').length == 1) {
			$this.parent().next().css('left', '50%');
		} else {
			$this.parent().next().css('left', '0');
		}
	}

	var lcate = $this.parent().parent();
	var selectedMcate = lcate.find(".dep_selected");
	if (selectedMcate.length > 0) {
		smartBoxMCategoryUpdate(selectedMcate, false);
	}
}

function smartBoxMCategoryUpdate(obj, isToggleParent) {
	var $this = $(obj);

	if (isToggleParent) {
		$this.parent().toggleClass('selected');
		$this.parent().parent().toggleClass('dep_selected');
	} else {
		$this.parent().parent().addClass('dep_selected');
	}

	

	if ($this.parent().parent().hasClass('dep_selected') == true) {
		var heightDep3 = $('.cate_lst li.open > span > a').height() + $('.cate_lst li.open .dep2 > li.selected').innerHeight() + $('.cate_lst li.open li.selected .dep3').innerHeight();
		$('.cate_lst > li.open').css('padding-bottom', heightDep3+1);
		if ($this.next().length != 0) {
			$this.next().show();
			$this.addClass('line');
		}
	} else {
		var heightDep4 = $('.cate_lst li.open > span > a').height() + $('.cate_lst li.open .dep2').innerHeight();
		$('.cate_lst > li.open').css('padding-bottom', heightDep4);
		if ($this.next().length != 0) {
			$this.next().hide();
		}
	}

	if ($this.parent().hasClass('selected') == false) {
		$('.cate_lst .dep3').removeClass('dep_selected2');
		$('.cate_lst .dep3 li').removeClass('selected');
	}

	if ($('.cate_lst li:nth-child(2n)').hasClass('open') == true) {
		if ($this.next().find('> li').length == 1) {
			$this.next().css('left', '0');
		} else {
			$this.next().css('left', '-50%');
		}
	}
	$this.parent().find('li[style*="display: list-item"]').attr("style", "");
}

function smartBoxSCategoryUpdate(obj) {
	var $this = $(obj);
	$this.parent().parent().toggleClass('dep_selected2');
	$this.parent().toggleClass('selected');
	if ($this.parent().parent().hasClass('dep_selected2') == true) {
		var heightDep5 = $('.cate_lst li.open > span > a').height() + $('.cate_lst li.open .dep2 > li.selected').innerHeight() + $('.cate_lst li.open li.selected .dep3 li.selected').innerHeight() + 1;
		$('.cate_lst > li.open').css('padding-bottom', heightDep5);
	} else {
		var heightDep6 = $('.cate_lst li.open > span > a').height() + $('.cate_lst li.open .dep2').innerHeight() + $('.cate_lst li.open li.selected .dep3').innerHeight();
		$('.cate_lst > li.open').css('padding-bottom', heightDep6);
	}
}

// 이건 추가
function smartBoxDetailLayer() {
	$('.cate_lst > li').css('padding-bottom', 0);
	$('.cate_lst > li').removeClass('open');
	$('.cate_lst > .selected').find('button').show();
}

function bindAdLayerEvent(value) {
	$(value).find('.ad_area .btn_ad').off('click');
	$(value).find('.ad_area>.ad_layer>.btn_close').off('click');
	ad_layer(value);
}
// -- end --
