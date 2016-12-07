function registerHelpers() {
	// 조건 helper
	Handlebars.registerHelper('ifNotZero', ifNotZero);
	Handlebars.registerHelper('ifValidPrice', ifValidPrice);
	Handlebars.registerHelper('ifOverOne', ifOverOne);
	Handlebars.registerHelper('ifDisplayCategoryList', ifDisplayCategoryList);

	// 일반 helper
	Handlebars.registerHelper('addCommas', addCommas);
	Handlebars.registerHelper('printImage', printImage);
	Handlebars.registerHelper('printItemCSSStyle', printItemCSSStyle);
	Handlebars.registerHelper('printParentCategories', printParentCategories);
	Handlebars.registerHelper('printPaging', printPaging);
	Handlebars.registerHelper('printLargeImageVisibility', printLargeImageVisibility);
}

function ifNotZero(a, opts) {
	if (typeof a === 'undefined' || a == null || a == '')
		a = 0;

	if (parseInt(a) != 0)
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

function ifOverOne(val, opts) {	
	if (val > 1)
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
function printParentCategories(result) {

	var innerHTML = '';

	if (reqParams.menuName == 'SRP' || reqParams.menuName == 'SPP') {
		innerHTML = '<li class="dim"><a href="javascript:setCategoryParams(\'\',\'\',\'\');search(false);" class="inp_box">' +
			'<strong class="tit">전체상품</strong><span class="sp_mw arr"></span></a>' +
			'</li>';
	}

	if (typeof result.LcIdName !== 'undefined' && result.LcIdName != null && result.LcIdName != '') {

		var css = 'dim';

		if (result.McIdName == '')
			css = 'selected';

		innerHTML += '<li class="' + css + '"><a href="javascript:setCategoryParams(\'' +
				result.LcId + '\',\'\',\'\');search(false);" class="inp_box">' +
				'<strong class="tit">' + result.LcIdName + '</strong><span class="sp_mw arr"></span></a>' +
				'</li>';
	}

	if (typeof result.McIdName !== 'undefined' && result.McIdName != null && result.McIdName != '') {

		var css = 'dim';

		if (result.ScIdName == '')
			css = 'selected';

		innerHTML += '<li class="' + css + '"><a href="javascript:setCategoryParams(\'' +
				result.LcId + '\',\'' + result.McId + '\',\'' + defaultReqParams.scId + '\');search(false);" class="inp_box">' +
				'<strong class="tit">' + result.McIdName + '</strong><span class="sp_mw arr"></span></a>' +
				'</li>';
	}

	if (typeof result.ScIdName !== 'undefined' && result.ScIdName != null && result.ScIdName != '') {
		innerHTML += '<li class="selected"><a href="javascript:setCategoryParams(\'' +
				reqParams.lcId + '\',\'' + reqParams.mcId + '\',\'' + result.ScId + '\');search(false);" class="inp_box">' +
				'<strong class="tit">' + result.ScIdName + '</strong><span class="sp_mw arr"></span></a>' +
				'</li>';
	}

	return new Handlebars.SafeString(innerHTML);
}

function printPaging(result) {

	var numOfPagesToShow = 5;

	if ( typeof result.Item === 'undefined' || result.Item == null || result.Item.length <= 0) return;

	var totalPage = parseInt(result.TotalGoodsCount / result.PageSize);
	if (result.TotalGoodsCount % result.PageSize != 0)
		totalPage++;

	// 화면 첫번재 보이는 페이지 수
	var firstPage = result.PageNo;
	while (firstPage % numOfPagesToShow != 1 && firstPage > 1)
		firstPage--;

	// 화면 마지막 페이지 수
	var lastPage = result.PageNo;
	while (lastPage % numOfPagesToShow != 0 && lastPage < totalPage)
		lastPage++;

	var pagingHTML = '<div class="paginate">';

	// 이전 페이지 버튼
	if (result.PageNo > numOfPagesToShow)
		pagingHTML += '<a href="javascript:goPage(' + (firstPage - 1) + ')" class="prev selected"><span class="sp_mw">이전 상품리스트</span></a>';
	else
		pagingHTML += '<span class="prev"><span class="sp_mw">이전 상품리스트 없음</span></span>';

	var count = totalPage > numOfPagesToShow ? numOfPagesToShow : totalPage;

	for (var i = firstPage; i <= lastPage; i++) {
		var css = '';
		if (result.PageNo == i)
			css = 'selected';
		pagingHTML += '<a href="javascript:goPage(' + i + ');" class="' + css + '">' + i + '</a>'
	}

	// 이후 페이지 버튼
	if (totalPage > lastPage)
		pagingHTML += '<a href="javascript:goPage(' + (lastPage + 1) + ')" class="next selected"><span class="sp_mw">다음 상품리스트</span></a>';
	else
		pagingHTML += '<span class="next"><span class="sp_mw">다음 상품리스트 없음</span></span>';

	pagingHTML += '</div>';

	return new Handlebars.SafeString(pagingHTML);
}
/* Helper function 들 */

function goPage(pageNo) {
	reqParams.pageNo = pageNo;
	search(true);
}

function displayItemList() {

	// 전체 갯수 출력
	//$('#TotalGoodsCount').html(addCommas(returnData.TotalGoodsCount));
	if (reqParams.menuName == 'SRP') {
		var countText = '(' + addCommas(returnData.TotalGoodsCount) + '개의 상품)';
		$('#TotalGoodsCount').html(countText);
	}

	var source = $('#item-template').html();
	var template = Handlebars.compile(source);
	$('.kind_artwrap').html(template(returnData));

	if (typeof returnData.Item === 'undefined' || returnData.Item == null || returnData.Item.length <= 0) {
		$('#itemEmptyDiv').show();

		if (bFirstLoading) {
			$('#searchOptionDiv').hide();			
		}
		else {
			$('#searchOptionDiv').show();			
		}
	}
	else {
		$('#itemEmptyDiv').hide();
		$('#searchOptionDiv').show();
	}

	$("img.thumbnail").lazyload();
	$(window).trigger('resize');
}

function displayCategory() {
	if (returnData.ScId != undefined && returnData.ScId.length != 0) {
		$("#categoryDiv > .slide_lst > li").first().attr("class", "dim");
		$("#categoryDiv > .slide_lst > li.selected").remove();
		var $selected = $('<li class=\"selected\"><a href=\"javascript:;\" class=\"inp_box\"><strong class=\"tit\">' + returnData.ScIdName + '</strong><span class=\"sp_mw arr\"></span></a></li>');
		$("#categoryDiv > .slide_lst > li").first().after($selected);

	} else {
		var source = $('#category-template').html();
		var template = Handlebars.compile(source);
		$('#categoryDiv').html(template(returnData));
	}
}

function displayBrandList() {

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
}

function brandSearch() {
	var brandAr = $('#brandListDiv input[type=checkbox]:checked').closest('li');
	var brandList = '';

	if (brandAr.length > 0) {
		for (var i = 0; i < brandAr.length; i++) {
			var liElement = $(brandAr[i]);

			if (i < brandAr.length - 1)
				brandList += liElement.attr('brandno') + ',';
			else
				brandList += liElement.attr('brandno');
		}

		reqParams.brandList = brandList;
	}
	else
		reqParams.brandList = '';

	search(true);
}

function deselectBrands() {
	$('#brandListDiv input[type=checkbox]:checked').attr('checked', false);
}

function selectSortOption(sortType) {
	$('#sortOption li').attr('class', '');
	$('#' + sortType).attr('class', 'selected');

	reqParams.sortType = sortType;

	closeSortLayer();
}

function deselectSortOption() {
	$('#sortOption li').attr('class', '');
}

function selectCategory(level, code) {
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

	reqParams.pageNo = 1;
}

function setCategoryParams(lcId, mcId, scId) {
	reqParams.lcId = lcId;
	reqParams.mcId = mcId;
	reqParams.scId = scId;
}

function changeViewStyle() {

	$('.last .sp_mw.ico_view').hide();
	$('.last .sp_mw.ico_view2').hide();
	$('.last .sp_mw.ico_view3').hide();

	if (currentViewStyle == 'Gallery') {
		currentViewStyle = 'List';
		$('.last .sp_mw.ico_view3').show();
		
	}
	else if (currentViewStyle == 'List') {
		currentViewStyle = 'Image';
		$('.last .sp_mw.ico_view').show();
	}
	else if (currentViewStyle == 'Image') {
		currentViewStyle = 'Gallery';
		$('.last .sp_mw.ico_view2').show();
	}

	displayItemList();
}

function printItemCSSStyle() {
	if (currentViewStyle == 'Gallery') {		
		return 'best_lst type';
	}
	else if (currentViewStyle == 'List') {
		return 'best_lst best_lst2';
	}
	else if (currentViewStyle == 'Image') {
		return 'best_lst best_lst3';
	}

	return 'best_lst';
}

function detailSearch() {
	reqParams.isSmartDelivery = 'Y';

	if (reqParams.menuName == "SPP" || $('#smallPacking').is(':checked'))
		reqParams.isSmallPacking = 'Y';
	else
		reqParams.isSmallPacking = 'N';

	reqParams.moreKeyword = $('#scKeyword').val();
	reqParams.minPrice = $('#minPrice').val() || 0;
	reqParams.maxPrice = $('#maxPrice').val() || 0;

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
		imgSrc = item.ImageURL;

	if (imgSrc == null || imgSrc == '')
		return defaultImage;

	return imgSrc;
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

function startLoading() {
	$('#itemLoading').show();	
	$('#searchOptionDiv').hide();
	$('.kind_artwrap').hide();	
	$('#itemEmptyDiv').hide();
}

function endedLoading() {
	$('#itemLoading').hide();	
	$('#searchOptionDiv').show();
	$('.kind_artwrap').show();	
}

function toggleSearchOption() {
	try {
		
		if (reqParams.brandList == '')
			$('#tb_btn_brand').attr('class', '');
		else
			$('#tb_btn_brand').attr('class', 'selected');

		if (reqParams.menuName == 'SRP' || reqParams.menuName == 'SPP') {

			if (reqParams.lcId == '')
				$('#tb_btn_category').attr('class', '');
			else
				$('#tb_btn_category').attr('class', 'selected');

			if (reqParams.sortType == '' || reqParams.sortType == 'ACCURACY_LQS')
				$('#tb_btn_sort').attr('class', '');
			else 
				$('#tb_btn_sort').attr('class', 'selected');
		}
		else if (reqParams.menuName == 'LP') {

			$('#tb_btn_category').attr('class', 'selected');

			if (reqParams.sortType == '' || reqParams.sortType == 'PREMIUM_RANK_LQS') 
				$('#tb_btn_sort').attr('class', '');
			else 
				$('#tb_btn_sort').attr('class', 'selected');
		}

		if (reqParams.menuName == "SPP") {
			if (reqParams.moreKeyword == '')
				$('#tb_btn_detail').attr('class', '');
			else
				$('#tb_btn_detail').attr('class', 'selected');
		} else {
			if (reqParams.moreKeyword == '' && reqParams.minPrice == 0 && reqParams.maxPrice == 0 && reqParams.isSmallPacking == 'N')
				$('#tb_btn_detail').attr('class', '');
			else
				$('#tb_btn_detail').attr('class', 'selected');

			if (reqParams.isSmallPacking == 'Y')
				$('#smallPacking').prop('checked', true);
		}

		var brandList = reqParams.brandList.split(',');
		for (var i = 0; i < brandList.length; i++) {
			
			if (brandList[i] == null || brandList[i] == '') continue;

			$('#chk_' + brandList[i]).prop('checked', true);
		}
	}
	catch (ex) {
	}
}

function resetDetailSearchOption() {
	$('#scKeyword').val('');
	$('#minPrice').val('');
	$('#maxPrice').val('');
	$('input[name=del_type]').prop('checked', false);
	$('#tb_btn_detail input[type=checkbox]').prop('checked', false);
	$('#delivery_none').prop('checked', true);
}

function displayCategoryInfo()
{
	if (reqParams.menuName == 'LP') {
		if (returnData.LcIdName != '') {
			$('#lcIdName').html(returnData.LcIdName);
		}

		if (returnData.McIdName != '') {
			$('#mcIdNameArrow').show();
			$('#mcIdName').show();
			$('#mcIdName').html(returnData.McIdName);
			$('#lcIdName').removeClass('cate_txt');
			$('#lcIdName').addClass('kind_cate');
		} else {
			$('#mcIdNameArrow').hide();
			$('#mcIdName').hide();
			$('#mcIdName').html('');
			$('#lcIdName').removeClass('kind_cate');
			$('#lcIdName').addClass('cate_txt');
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
			$('#mcIdName').removeClass('kind_cate');
			$('#mcIdName').addClass('cate_txt');
		}
	}
}