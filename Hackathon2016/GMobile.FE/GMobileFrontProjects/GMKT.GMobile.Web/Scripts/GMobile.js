var __HEADER_FOOTER_DOMAIN_URL = "//mobile.gmarket.co.kr";
﻿var mobileLocalStorage = null;
try
{
    mobileLocalStorage = window.localStorage;
} catch (e) {}



(function (GMobile, $, undefined) {
	GMobile.rpmAjaxCall = function (eventId) {
		var pdsUrl = "http://pds.gmarket.co.kr/scriptBrokerMsgJsonp";
		var params = { id: eventId };

		if (eventId == "l" && !!window.performance) {
			var t = window.performance.timing;

			if (!!t) {
				var paramO = {
					id: eventId,
					NaviStTick: t.navigationStart,
					LookupStTick: t.domainLookupStart,
					RequestStTick: t.requestStart,
					ResponseEdTick: t.responseEnd,
					DomLoadedStTick: t.domContentLoadedEventStart,
					LoadStTick: t.loadEventStart
				}

				if (paramO != null)
					params = paramO;
			}
		}

		$.ajax({
			url: pdsUrl,
			data: params,
			type: "GET",
			contentType: "application/javascript; charset=utf-8",
			scriptCharset: "utf-8",
			dataType: "jsonp",
			crossdomain: true,
			error: function (request, error) {
				//alert("Error: " + error);
			}
		});
	};

	GMobile.registerRPM = function () {
		var startCall = new Image().src = "http://pds.gmarket.co.kr/scriptBrokerMsg?id=b";

		$(window).load(function () {
			GMobile.rpmAjaxCall("l");
		});
		$(document).ready(function () {
			GMobile.rpmAjaxCall("r");
		});
	};
})(window.GMobile = window.GMobile || {}, jQuery);

function getAppCookieValue(name) {
	var ret = "";

	if (document.cookie.length > 0) {
		start = document.cookie.indexOf(name + "=");
		if (start != -1) {
			start = start + name.length + 1;
			end = document.cookie.indexOf(";", start);
			if (end == -1) end = document.cookie.length;

			ret = unescape(document.cookie.substring(start, end));
		}
	}

	return ret;
}

function getQueryStringValue(name) {
	var url = document.URL.split("?");
	var arr, temp, i;
	var qs = {};
	var ret = "";

	if (url.length >= 2) {
		arr = url[1].split("&");
		for (i = 0; i < arr.length; i++) {
			temp = arr[i].split("=");
			if (temp.length === 2 && temp[0]) qs[temp[0].toLowerCase()] = temp[1];
		}
		ret = qs[name.toLowerCase()];
	}

	return ret || "";
}

var prevFcd = "";
var prevTimeStamp = 0;
function SnaUrlFooter(fcd) {
	var currentTimeStamp = new Date().getTime();
	if (fcd == prevFcd) {
		if (currentTimeStamp - prevTimeStamp > 100) {
			$.get("http://sna.gmarket.co.kr/?fcd=" + fcd);
			prevFcd = fcd;
			prevTimeStamp = currentTimeStamp;
		}
	} else {
		$.get("http://sna.gmarket.co.kr/?fcd=" + fcd);
		prevFcd = fcd;
		prevTimeStamp = currentTimeStamp;
	}
}

function doSubmit() {
	//뒤공백 제거 후 비교
	if ((document.searchForm.topKeyword.value).replace(/\s*$/, "") == "") {
		alert("검색어를 입력해 주세요");
	} else {
		try {
			recentKeywordsController.setRecentKeyword(document.searchForm.topKeyword.value);
		} catch (e) { }

		SnaUrlFooter('704100005');
		document.searchForm.submit();
	}
}

function displayResult() {
	if (httpRequest.readyState == 4) {
		if (httpRequest.status == 200) {
			var html = httpRequest.responseText;
			var listView = document.getElementById('relate_keyword');
			if (listView) {
				if (html.length > 100) {
					listView.innerHTML = html;
					hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'Y');
					displayLayer('header_recent_wrapper', 'none');
					$(listView).addClass("reflow").removeClass("reflow");
					$("#header_extended_input_wrapper").hide();
					$("#list_recent").show();
					getRecommendKeyword();
				} else {
					listView.innerHTML = '';
					$("#list_recent").hide();
				}
			}
		} else {
			hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'N');
		}
	}
}

function getRecommendKeyword() {
	var keyword = $("#scb_ip").val();
	if ( keyword == undefined || keyword.length < 2) {
		return;
	}

	var params = { primeKeyword: keyword, needDiver: false };

	$.ajax({
		type: "POST",
		url: __HEADER_FOOTER_DOMAIN_URL + "/Search/GetRecommedKeywordFromHeader",
		dataType: "json",
		data: params,
		success: function (data) {
			var $ul = $('#scb_lyr').find('ul.srch_lst');
			$ul.empty();

			if (!$.isArray(data) || data.length === 0) {
				$("#scb_lyr").removeClass("wide_wrap");
				$('#recommend_keyword').hide();				
				return;
			}

			var size = data.length;
			var $result = $();
			for (var i = 0; i < size; i++) {
				var item = data[i];
				var $element = $('<li><a href="#" onclick="javascript:goSearchRecommendedKeyword(this);" class="srch_txt"><span>' + item.Keyword + '</span></a></li>')
				$element.find('a').data('srpData', item.SrpPostJson);
				$result = $result.add($element);
			}

			$ul.append($result);
			$("#scb_lyr").addClass("wide_wrap");
			$("#recommend_keyword").show();
		},
		error: function(){
			$("#scb_lyr").removeClass("wide_wrap");
			$('#scb_lyr').find('ul.srch_lst').empty();
			$("#recommend_keyword").hide();	
		}
     
	});
	

}

function clearTopSearch() {
	var obj = document.getElementById('scb_ip');
	obj.value = '';
	hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'N');
	$("#scb_lyr").removeClass("wide_wrap");
	$('#scb_lyr').find('ul.srch_lst').empty();
	$("#recommend_keyword").hide();	
}

function goSearch(keyword) {
	searchForm.topKeyword.value = keyword;
	doSubmit();
}

function goSearchRecommendedKeyword(tag) {
	searchForm.topKeyword.value = tag.text;
	
	//뒤공백 제거 후 비교
	if ((document.searchForm.topKeyword.value).replace(/\s*$/, "") == "") {
		alert("검색어를 입력해 주세요");
	} else {
		var $this = $(tag);
		var srpData = JSON.parse($this.data('srpData'));

		SnaUrlFooter('704100005');
		document.location.href = '/Search/Search?' + $.param(srpData);
	}
}

function declinePushMessage(id, isDevicePush) {
	if((isDevicePush || "").toLowerCase() === "true") {
		location.href = "gmarket://goSet";
	} else {
		jQuery.ajax({
			type: "POST",
			url: __HEADER_FOOTER_DOMAIN_URL + "/Common/DeclinePushMessage",
			dataType: "jsonp",
			data: {},
			success: function (data) {
				if (data.success) {
					var date = new Date();				
					var message = "G마켓 알림 서비스 안내\n\n1. 전송자 : 이베이코리아\n2. 수신거부 일시 : " + date.getFullYear().toString().substr(2, 2) + "년 " + (date.getMonth() + 1) + "월 " + date.getDate() + "일 " + date.getHours() + "시 \n3. 처리 내용 : 수신거부 처리완료\n\n언제든지 APP의 설정>알림설정에서\n설정 변경 가능합니다.";
					alert(message);
					document.getElementById(id).style.display = 'none';
				} else {
					var message = "알림 서비스 수신거부 설정 실패하였습니다.";
					alert(message);
				}
			},
			error: function () { }
		});
	}
}

if (!jQuery.fn.on) jQuery.fn.on = jQuery.fn.bind;


var cosem_Request = function () {
	var cookieDay = 30;
	var accountCode = "473";
	this.getParameter = function (name) {
		var rtnval = ''; var nowAddress = unescape(location.href); var parameters = (nowAddress.slice(nowAddress.indexOf('?') + 1, nowAddress.length)).split('&');
		for (var i = 0; i < parameters.length; i++) { var varName = parameters[i].split('=')[0]; if (varName.toUpperCase() == name.toUpperCase()) { rtnval = parameters[i].split('=')[1]; break; }; }; return rtnval;
	}
	this.imageURL = function () {
		var cosem = this.getParameter('cosemkid'); var cosem_kid = ""; var cosemProtocol = (location.protocol == "https:") ? "https:" : "http:";
		if (cosem.length == 0) cosem = this.getParameter('cosem');
		if (cosem.length > 0) cosem_kid = "&kid=" + cosem + "&referer=" + encodeURIComponent(location.href);
		var image = new Image(); image.src = cosemProtocol + "//" + "gmktr.icomas.co.kr" + "/Script/script3.php" + "?aid=" + accountCode + "&ctime=" + cookieDay + cosem_kid;
	};
	this.tracking = function () {
		var obj = this; setTimeout(function () { obj.imageURL(); }, 10);
	};
};


var GMAjaxLayer = function (url, data, wrapperSelector, renderCallback, errorCallback) {
	var that = this;

	this.template = null;
	this.$wrapper = $();

	if (wrapperSelector) {
		this.$wrapper = $(wrapperSelector);
	}

	if (url) {
		$.ajax({
			url: url,
			data: data,
			async: true,
			dataType: "jsonp",
			success: function (template) {
				that.template = template.trim();

				that.render(renderCallback);
			},
			error: errorCallback
		});
	}
};

GMAjaxLayer.prototype.render = function (callback) {
	this.$wrapper.html(this.template);

	if (callback) {
		callback.apply(this);
	}
};


function HeaderTypeEnum() { }
HeaderTypeEnum.Type = { Normal: 0, Simple: 1, Vip: 2, Srp: 3, Minishop: 4, SimpleBar: 9, None: 10 };
// normal : 홈, simple : 기본 헤더

var GMHeader = function (isMain, type, code) {
	var appHeaderTitle = (type === HeaderTypeEnum.Type.SimpleBar ? "주문/결제" : 'G마켓');
	var isRendered = false;


	this.setAppHeaderTitle = function(title){
		appHeaderTitle = title;
		if (isRendered) {
			if (isAppHeader()) {
				doAppHeaderScheme();
				return true;
			}
			$headerTitle = $('#simple_header_title');
			$headerBarTitle = $('#simple_header_bar_title');
			if($headerTitle.length){
				$headerTitle.html(appHeaderTitle);
			}
			
			if($headerBarTitle.length){
				$headerBarTitle.html(appHeaderTitle);
			}
		}
	}

	if (typeof guidProfiler === "undefined" || guidProfiler === null) {
		$.ajax({
			url: "//script.gmarket.co.kr/js/mobile/guidGeneration.js",
			dataType: "script",
			cache: true
		});
	}

	if (!isMain) 
		isMain = false;
	else if (isMain != true)
		isMain = false;

	var headerType;

	if (type === undefined) {
		headerType = HeaderTypeEnum.Type.Simple;
	} else {
		headerType = type;
	}

	GMHeader.headerType = headerType;

	var url = __HEADER_FOOTER_DOMAIN_URL + "/Common/Header";

	var gCode = "";
	if (headerType == HeaderTypeEnum.Type.Vip 
		|| headerType == HeaderTypeEnum.Type.Minishop
		&& code) {
		gCode = code;
	}

	var data = {
		isMain: isMain,
		type : type,
		code: gCode
	};

	var wrapperSelector = "#header_wrapper";

	var currentKeyword;

	var $header = $();
	var $searchInput = $();
	var $searchInputApp = $();
	var $searchDelButton = $();
	var $exceptSearchLayer = $();
	var $cancelSearchLayer = $();
	var $recentKeywordsLayer = $();
	var $recentItemsLayer = $();
	var $homeTabsLayer = $();
	var $simpleHeaderLayer = $();
	var $simpleHeaderGoSearchButton = $();
	var $vipHeaderLayer = $();
	var $vipGoSearchButton = $();
	var $searchBox = $();
	var $searchButton = $();

	var searchBoxTemplate = Handlebars.compile("");

	var renderCallback = function () {
		$header = $("#header_st");
		$searchInput = $("#scb_ip");
		$searchInputApp = $("#scb_ip_app");
		$searchDelButton = $("#scb_del");
		$exceptSearchLayer = $("#header_st").children().not("#header_search_layer").not('h1');
		$cancelSearchLayer = $("#cancel_search_layer");
		$cancelSearchButton = $("#cancel_search_button");
		$recentItemsLayer = $("#list_recent");
		$homeTabsLayer = $("#home_tabs");
		$simpleHeaderLayer = $("#simple_header");
		$simpleHeaderGoSearchButton = $("#simple_header_go_search");
		$vipHeaderLayer = $("#vip_header");
		$vipGoSearchButton = $("#vip_header_go_search");
		$simpleHeaderBar = $('#simple_header_bar');
		$searchBox = $("#search_box");
		$searchButton = $("#search_button");

		if($("#search_box_template").length){
			searchBoxTemplate = Handlebars.compile($("#search_box_template").html());
		}

		isRendered = true;

		if(isAppHeader()){
			doAppHeaderScheme();
			if(GMHeader.headerType !== HeaderTypeEnum.Type.Srp){
				return
			}
		}
		
		if(headerType == HeaderTypeEnum.Type.SimpleBar){
			$simpleHeaderBar.show();
			$(wrapperSelector).parent().addClass("simple");
			$header.hide();
			$simpleHeaderLayer.hide();
			$('#simple_header_bar_title').html(appHeaderTitle);
		}
		else if (headerType == HeaderTypeEnum.Type.Vip 
			|| headerType == HeaderTypeEnum.Type.Minishop
			|| headerType == HeaderTypeEnum.Type.Simple 
			|| headerType == true) {
			showSimpleHeader();

			if(headerType === HeaderTypeEnum.Type.Minishop){
				$("#head").addClass("vip");
			}
			if(headerType === HeaderTypeEnum.Type.Simple){
				$('#simple_header_title').html(appHeaderTitle);
				$("#head").addClass("new");
			}
			
			$simpleHeaderGoSearchButton.on("click", function (e) {
				hideSimpleHeader();

				showExpandSearchLayer();

				$searchInput.focus();

				e.preventDefault();
			});

			$cancelSearchButton.on("click", showSimpleHeader);

			$("#closeRecent").on("click", showSimpleHeader);

			function showSimpleHeader() {
				SnaUrlFooter("717000015");
                pdsClickLog('200000153','utility',{});
				$(wrapperSelector).parent().addClass("simple");
				$header.hide();
				$simpleHeaderLayer.show();
			}

			function hideSimpleHeader() {
				$(wrapperSelector).parent().removeClass("simple");
				$header.show();
				$simpleHeaderLayer.hide();
			}
		}
		else if (headerType == HeaderTypeEnum.Type.Srp) {
			$exceptSearchLayer = $exceptSearchLayer.not(".left_mn");

			showSRPHeader();

			$searchBox.on("click", "button.word", function (e) {
                var index = $(this).attr("index");
				SnaUrlFooter("717000011");
                pdsClickLog('200000149','utility',{'asn':index});
				showKeywordLayer();

				$searchInput.scrollLeft = $searchInput.scrollWidth;
				var wordIdx = $('.keyword_swipe li');
				var myIdx = $(this).parents('li').index() + 1;
				var posCursor = 0;
				for (i = 0; i < myIdx; i++) {
					var posCursor2 = wordIdx.eq(i).find('.word').text().length + 1;
					posCursor = posCursor + posCursor2;
				}
				posCursor = posCursor - 1;

				$searchInput.focus();
				$searchInput[0].setSelectionRange(posCursor, posCursor);
			});

			$searchBox.on("click", "button.del", function (e) {
				SnaUrlFooter("717000012");
				pdsClickLog('200000150','utility',{});
				var $word = $searchBox.find(".word");
				var $removedWord = $(this).parent().find(".word");
				if ($word.length > 1) {
					var newKeyword = "";
					$word.each(function (i, n) {
						if (false === $removedWord.is(n)) {
							newKeyword += $(n).html() + " ";
						}
					});
					$searchInput.val(newKeyword.trim());
					doSubmit();
				} else {
					$(this).parent().parent().remove();

					$searchDelButton.hide();

					$searchInput.val("");
					currentKeyword = "";

					showKeywordLayer();
				}
			});

			$searchBox.on("click", ".inp_blank", function (e) {
				showKeywordLayer();

				var posCursor = $searchInput.val().length;
				$searchInput[0].setSelectionRange(posCursor, posCursor);
			});

			$cancelSearchButton.on("click", function (e) {
				SnaUrlFooter("717000015");
                pdsClickLog('200000153','utility',{});
				e.preventDefault();

				showSRPHeader();

				$searchInput.val(currentKeyword);
			});

			$("#closeRecent").on("click", function (e) {
				e.preventDefault();

				showSRPHeader();

				$searchInput.val(currentKeyword);
			});

			if (typeof srpFront === "function") {
				srpFront();
			}


			function showSRPHeader() {
				$(wrapperSelector).parent().addClass("srp");

				$header.find(".left_mn").hide();
				$header.find(".my").hide();

				$searchBox.show();
			}

			function hideSRPHeader() {
				$(wrapperSelector).parent().removeClass("srp");

				$header.find(".my").show();
			}

			function showKeywordLayer() {
				hideSRPHeader();

				$searchBox.hide();
				showExpandSearchLayer();

				$searchInput.focus();
			}
		}
		else if (headerType === HeaderTypeEnum.Type.None) {
			$("#header_wrapper").hide();
		}

		$searchInput.on("focus", showExpandSearchLayer);
		$searchInputApp.on("focus", function () {
			var scheme = "gmarket://opensearch";
			var keyword = $(this).val();
			if (keyword) {
				scheme = scheme + "?keyword=" + encodeURIComponent(keyword);
			}

			location.href = scheme;
			$(this).blur();
		});

		$searchButton.on("click", function () {
			SnaUrlFooter("717000013");
            pdsClickLog('200000151','utility',{});
			if ($searchButton.hasClass("submit")) {
				doSubmit();
			} else {
				showKeywordLayer();

				var posCursor = $searchInput.val().length;
				$searchInput[0].setSelectionRange(posCursor, posCursor);
			}
		});

		$cancelSearchButton.on("click", function (e) {
			$searchInput.val(currentKeyword);
			clearInterval(sendKeywordOnceIntervalId);
			$searchDelButton.hide();
			showNormalSearchLayer(e);
			$('#head').removeClass('main_search');
			$('#wrap').removeClass('vip_search');
		});

		renderCurrentKeyword();
		
		if (document.readyState == "complete") {
			$("#progress_icon").hide();
		} else {
			$(document).ready(function () {
				$("#progress_icon").hide();
			});
		}
	};

	GMAjaxLayer.apply(this, [
		url,
		data,
		wrapperSelector,
		renderCallback, 
		function () {
			new GMStaticHeader(wrapperSelector);
			renderCallback.apply();
		}
	]);




	
	this.setCurrentKeyword = function (keyword) {
		if (keyword) {
			currentKeyword = keyword;

			renderCurrentKeyword();
		}
	};

	function showExpandSearchLayer() {
		$exceptSearchLayer.hide();
		$header.addClass("full type2");
		if (currentKeyword && currentKeyword.length > 0) {
			$searchButton.hide();
		} else {
			$searchButton.show();
		}
		$cancelSearchLayer.show();

		$homeTabsLayer.hide();
		$searchButton.addClass("submit");
	}

	function showNormalSearchLayer() {
		$exceptSearchLayer.show();
		$header.removeClass("full type2");
		$cancelSearchLayer.hide();

		$recentItemsLayer.hide();

		$homeTabsLayer.show();
		$searchButton.removeClass("submit");
		$searchButton.show();
	}

	function renderCurrentKeyword() {
		var $searchInputAll = $searchInput.add($searchInputApp);

		if (currentKeyword && $searchInputAll.length > 0 && $searchInputAll.val() != currentKeyword) {
			if (currentKeyword.length > 0) {
				$searchDelButton.show();
			} else {
				$searchDelButton.hide();
			}

			var currentKeywordArray = currentKeyword.split(" ");
			var newKeywordArray = [];
			for (var i = 0; i < currentKeywordArray.length; i++) {
				if (currentKeywordArray[i] != "") {
					newKeywordArray.push(currentKeywordArray[i]);
				}
			}
			$searchBox.find(".keyword_swipe").html(searchBoxTemplate(newKeywordArray));
			$searchInputAll.val(newKeywordArray.join(" "));
			$searchInputAll.removeClass("text-placeholder");

			SelectText();
		}
	}

	function doAppHeaderScheme(){
		$('#appHeaderScheme').attr('src', 'gmarket://webHeader?title=' + encodeURIComponent(appHeaderTitle) 
		+ '&type=' + encodeURIComponent(headerType === HeaderTypeEnum.Type.Srp ? HeaderTypeEnum.Type.None : headerType) 
		+ '&subType=' + encodeURIComponent($('#subType').val()) 
		+ '&subImageUrl=' + encodeURIComponent($('#subImageUrl').val()) 
		+ '&subLandingUrl=' + encodeURIComponent($('#subLandingUrl').val()) 
		+ '&subTitle=' + encodeURIComponent($('#subTitle').val()));
	}

	function isAppHeader(){
		var $appHeaderScheme = $('#appHeaderScheme');
		return $appHeaderScheme.length != 0;
	}
};

GMHeader.goHome = function (url) {
	try {

		if (GMHeader.headerType === HeaderTypeEnum.Type.Normal) {
			SnaUrlFooter("717000002");
            pdsClickLog('200000140','link',{});
		}
		else if (GMHeader.headerType === HeaderTypeEnum.Type.Simple) {
			SnaUrlFooter("717000006");
            pdsClickLog('200000144','link',{});
		}
		else if (GMHeader.headerType === HeaderTypeEnum.Type.Srp) {
			SnaUrlFooter("717000010");
            pdsClickLog('200000148','link',{});
		}
		else if (GMHeader.headerType === HeaderTypeEnum.Type.Vip) {
			SnaUrlFooter("717000016");
            pdsClickLog('200000154','link',{});
		}
		var app_pcid = getAppCookieValue("pcid");
		var app_navimode = getAppCookieValue("navigation_mode");

		// 안드로이드 어플일 경우 스킴처리.
		if ( app_pcid.length > 0 && app_pcid.substring(0,1) == "3") {
			document.location.href = "gmarket://home";
		} else if ( app_pcid.length > 0 && app_pcid.substring(0,1) == "4" && app_navimode == "pc") {
			document.location.href = "http://www.gmarket.co.kr";
		} else {
			document.location.href = url;
		}
	}	catch (e)	{
		document.location.href = url;
	}
};

GMHeader.goMmyg = function (url, oldUrl) {
	var c_name = "pcid";

	if (document.cookie.length > 0) {
		c_start = document.cookie.indexOf(c_name + "=");

		if (c_start != -1) {
			c_start = c_start + c_name.length + 1;
			c_end = document.cookie.indexOf(";", c_start);

			if (c_end == -1) c_end = document.cookie.length;

			if (unescape(document.cookie.substring(c_start, c_end)) == "") {
				document.location.href = url;
			} else {
				var app_mmyglink = getAppCookieValue("mmyglink");

				if (app_mmyglink.length > 0 && app_mmyglink == "Y") {
					document.location.href = url;
				} else {
					document.location.href = oldUrl;
				}
			}
		} else {
			document.location.href = url;
		}
	}
};

GMHeader.prototype = new GMAjaxLayer();
GMHeader.prototype.constructor = GMHeader;
GMHeader.prototype.parent = GMAjaxLayer.prototype;




var GMFooter = function (isMain) {
	if (!isMain)
		isMain = false;
	else if (isMain != true)
		isMain = false;
	
	var isPushLanding = (getQueryStringValue("frompush").substr(0,1).toUpperCase() === "Y") ? true : false;
	var that = this;
	var url = __HEADER_FOOTER_DOMAIN_URL + "/Common/Footer";
	var data = {
		isMain: isMain,
		isPushLanding: isPushLanding
	};
	var wrapperSelector = "#foot";

	var cosemRequest = new cosem_Request();
	cosemRequest.tracking();

	this.hideBottomBar = function(){
		$bottomBar = $('#footer_bar_menu');
		if($bottomBar.length){
			$bottomBar.hide();
			$('#foot').removeClass('bar');
		}
	}

	GMFooter.isBottomBarType = GMHeader.headerType === HeaderTypeEnum.Type.Simple
			|| GMHeader.headerType === HeaderTypeEnum.Type.Srp
			|| GMHeader.headerType === HeaderTypeEnum.Type.Minishop;

	if (GMFooter.isBottomBarType) {
		if ($("#rviLayer").length > 0) {
			$("#rviLayer").remove();
		}
	}

	var showBackButton = false;

	this.showBottomBar = function(){
		$bottomBar = $('#footer_bar_menu');
		if($bottomBar.length){
			$bottomBar.hide();
			$('#foot').addClass('bar');
		}
	}

	GMAjaxLayer.apply(this, [
		url,
		data,
		wrapperSelector,
		function(){
			if (GMFooter.isBottomBarType) {
				$bottomBar = $('#footer_bar_menu');
				if($bottomBar.length){
					$('#foot').addClass('bar');
					$bottomBar.show();
					if(showBackButton){
						
					}
				}
			}
			else if(GMHeader.headerType === HeaderTypeEnum.Type.Vip){
				$('#footer_back').show();
			}
			
		},
		function () {
			new GMStaticFooter(wrapperSelector);
		}
	]);
};

GMFooter.goPCWeb = function (defaultUrl) {
	if (!defaultUrl) defaultUrl = "http://www.gmarket.co.kr/?sL=MW";

	if (location.toString().indexOf("mmyg") >= 0) {
		location.href = "http://myg.gmarket.co.kr";
	} else {
		var currentUrlpath = window.location.pathname.toLowerCase();
		var laststr = currentUrlpath.substr(-1, 1);
		if (laststr == "/") {
			currentUrlpath = currentUrlpath.substr(0, currentUrlpath.length - 1);
		}

		switch (currentUrlpath) {
			case "/giftcard/giftcard":
				location.href = "http://gbank.gmarket.co.kr/GiftCard";
				break;
			case "/giftcard/giftcardregist":
				location.href = "http://gbank.gmarket.co.kr/GiftCard";
				break;
			case "/giftcard/giftcardlistsend":
				location.href = "http://gbank.gmarket.co.kr/GiftCard";
				break;
			case "/giftcard/giftcardlistrsv":
				location.href = "http://gbank.gmarket.co.kr/GiftCard";
				break;
			case "/giftcard/giftcardorder":
				location.href = "http://gbank.gmarket.co.kr/GiftCard";
				break;
			case "/giftcard/giftcardorderdetail":
				location.href = "http://gbank.gmarket.co.kr/GiftCard";
				break;
			case "/pluszone":
				location.href = "http://pluszone.gmarket.co.kr/";
				break;
			case "/best":
			case "/best/bestsellerlist":
				location.href = "http://promotion.gmarket.co.kr/bestseller/BestSeller.asp";
				break;
			case "/superdeal":
				location.href = "http://promotion.gmarket.co.kr/superdeal/superdealmain.asp";
				break;
	        case "/ecoupon":
	            location.href = "http://category.gmarket.co.kr/listview/L100000048.aspx";
	            break;
			case "/eventzone/mybenefithome":
			case "/eventzone/mybenefit":
			case "/eventzone/mybenefitcoupon":
			case "/eventzone/couponcategory":
			case "/eventzone/mybenefitvip":
			case "/gstamp":
			case "/gstamp/1":
				location.href = "http://promotion.gmarket.co.kr/eventzone/Info.asp";
				break;
			case "/display/bestsellerlist":
				location.href = "http://promotion.gmarket.co.kr/bestseller/BestSeller.asp";
				break;
			case "/display/today":
				location.href = "http://promotion.gmarket.co.kr/eventzone/Today.asp";
				break;
			case "/display/specialshoppinglist":
				location.href = "http://promotion.gmarket.co.kr/planshop/planshop.asp"
				break;
			case "/search/search":
				var topkeyword = document.sCategoryFrm.keyword.value;
				var frm = document.createElement("form");
				frm.name = "frmPcSearch";
				frm.method = "GET";
				frm.acceptCharset = "euc-kr";
				frm.action = "http://search.gmarket.co.kr/search.aspx";
				var keyword = document.createElement("input");
				keyword.type = "hidden";
				keyword.name = "keyword";
				keyword.value = topkeyword;
				frm.appendChild(keyword);
				document.body.appendChild(frm);
				frm.submit();
				break;
			case "/category/category":
				var lcId = document.categoryForm.lcId.value;
				location.href = "http://category.gmarket.co.kr/listview/L" + lcId + ".aspx";
				break;
			case "/category/list":
				var lcId = document.categoryForm.lcId.value;
				var mcId = document.categoryForm.mcId.value;
				var scId = document.categoryForm.scId.value;
				if (scId == "" && mcId == "") {
					location.href = "http://category.gmarket.co.kr/listview/L" + lcId + ".aspx";
				}
				else if (scId == "") {
					location.href = "http://category.gmarket.co.kr/listview/List.aspx?gdmc_cd=" + mcId + "&ecp_gdlc=" + lcId;
				}
				else {
					location.href = "http://category.gmarket.co.kr/listview/List.aspx?gdsc_cd=" + scId + "&ecp_gdlc=" + lcId + "&ecp_gdmc=" + mcId + "";
				}
				break;
			case "/display/specialshopping":
				var sid = document.getElementById("sid").value;
				location.href = "http://promotion.gmarket.co.kr/planview/plan.asp?sid=" + sid;
				break;
			default:
				location.href = defaultUrl;
				break;
		}
	}
};

GMFooter.prototype = new GMAjaxLayer();
GMFooter.prototype.constructor = GMFooter;
GMFooter.prototype.parent = GMAjaxLayer.prototype;




var GMStaticHeader = function (wrapperSelector) {
	$("#head").removeClass("vip");
	var headerHtml = "";

	headerHtml += '<div class="hd_wrap" id="header_st">';
	headerHtml += '	<a href="http://mobile.gmarket.co.kr/Home/LeftDrawer" class="left_mn"><span>메뉴보기</span></a>';
	headerHtml += '	<h1><a href="#" onclick="GMHeader.goHome(\'http://mobile.gmarket.co.kr\'); return false;">Gmarket</a></h1>';
	headerHtml += '	<div id="header_search_layer" class="scb_hd">';
	headerHtml += '		<div class="scb">';
	headerHtml += '			<form id="search" name="searchForm" action="http://mobile.gmarket.co.kr/Search/Search" method="get" onsubmit="doSubmit(); return false;">';
	headerHtml += '				<fieldset>';
	headerHtml += '					<div class="scb_sub">';
	headerHtml += '						<input id="scb_ip" type="text" name="topKeyword" onkeyup="sendKeywordOnce();" value="" autocomplete="off" spellcheck="false" autocorrect="off" autocapitalize="off" />';
	headerHtml += '						<label for="scb_ip" class="hp">상품검색하기</label>';
	headerHtml += '					</div>';
	headerHtml += '					<button id="scb_del" type="button" class="del_btn" style="display: none;" onclick="clearTopSearch();"><span>입력내용 삭제</span></button>';
	headerHtml += '					<button type="submit" class="sc_btn"><span>검색</span></button>';
	headerHtml += '				</fieldset>';
	headerHtml += '			</form>';
	headerHtml += '		</div>';
	headerHtml += '	</div>';
	headerHtml += '	<div class="dret_btn">';
	headerHtml += '		<a href="http://m.gmarket.co.kr/order/mpay/basket.asp" class="cart"><span class="hp">장바구니</span></a>';
	headerHtml += '		<a href="#" onclick="GMHeader.goMmyg(\'http://mmyg.gmarket.co.kr\', \'http://mw.gmarket.co.kr/mypage/myMain.do\'); return false;" class="my"><span class="hp">나의쇼핑정보</span></a>';
	headerHtml += '	</div>';
	headerHtml += '	<div id="cancel_search_layer" class="cancl_btn" style="display: none;">';
	headerHtml += '		<a id="cancel_search_button" href="#" class="cancl"><span>닫기</span></a>';
	headerHtml += '	</div>';
	headerHtml += '</div>';

	$(wrapperSelector).html(headerHtml);
};




var GMStaticFooter = function (wrapperSelector) {
	var footerHtml = "";

	footerHtml += '<div class="btn_lf">';
	footerHtml += '	<a href="http://m.gmarket.co.kr/Login/login_mw.asp">로그인</a>';
	footerHtml += '	<a href="#" onclick="GMFooter.goPCWeb(\'http://www.gmarket.co.kr/?sL=MW\'); return false;">PC버전</a>';
	footerHtml += '	<a href="http://mobile.gmarket.co.kr/CustomerCenter">고객센터</a>';
	footerHtml += '</div>';
	footerHtml += '<div class="ft_cnt">';
	footerHtml += '	<div class="ftx or1">';
	footerHtml += '	 <span>㈜이베이코리아  대표이사 :  변광윤</span><br />';
	footerHtml += '  <span class="t1">사업자등록번호 : 220 - 81 - 83676</span><span>통신판매업신고 :  강남 10630호</span><br />';
	footerHtml += '  <span class="t1">서울시 강남구 테헤란로 152 (역삼동 강남파이낸스센터)</span><address><a href="mailto:gmarket@corp.gmarket.co.kr">gmarket@corp.gmarket.co.kr</a></address><br />';
	footerHtml += '  <span class="t1">고객센터 : 1566 - 5701</span><span>상담가능시간 : 오전 9시 - 오후 6시(토요일, 공휴일 휴무)</span>';
	footerHtml += ' </div>';
	footerHtml += '	<div class="ft_link">';
	footerHtml += '  <a href="http://www.ftc.go.kr/info/bizinfo/communicationList.jsp">사업자정보확인</a>';
	footerHtml += '  <a href="http://www.gmarket.co.kr/challenge/neo_help/policy01.asp">개인정보취급방침</a>';
	footerHtml += '  <a href="http://mmyg.gmarket.co.kr/Service">이용약관</a>';
	footerHtml += ' </div>';
	footerHtml += '	<p class="ftx or2">G마켓은 통신판매중개자이며 통신판매의 당사자가 아닙니다. <br />따라서 G마켓은 상품/거래정보 및 가격에 대하여 책임을 지지 않습니다.</p>';
	footerHtml += '	<p class="ftx or3">Copyright &copy;eBay Korea Co., Ltd. All rights reserved</p>';
	footerHtml += '</div>';

	$(wrapperSelector).html(footerHtml);
};





var RecentItemsController = function (isMain, iframeSelector) {
	this.isMain = false;
	this.$iframe = $();

	if (isMain) {
		this.isMain = true;
	}

	if (iframeSelector) {
		this.$iframe = $(iframeSelector);
	}

	this.refreshItems();
};

RecentItemsController.GOODSCODE_LENGTH = 9;
RecentItemsController.NUMBER_OF_RECENT_ITEMS = 500;
RecentItemsController.EXPIRED_DATE = 30 * 6;

RecentItemsController.prototype.getRecentItems = function (context, callback) {
	if (mobileLocalStorage) {
		if (this.isMain) {
			this.refreshItems();

			var gmktRecentItems = mobileLocalStorage.getItem("GMKTRecentItems");
			if (gmktRecentItems) {
				return JSON.parse(gmktRecentItems);
			} else {
				return null;
			}
		} else {
			var listener = function (e) {
				if (e.data.method == "getRecentItems") {
					if (callback) {
						callback.apply(context, [e.data.result]);
					}
					window.removeEventListener("message", listener);
				}
			};

			window.addEventListener("message", listener);
			
			this.$iframe[0].contentWindow.postMessage({
				method: "getRecentItems"
			}, __MOBILE_WEB_URL_SECURE);
		}
	}
};

RecentItemsController.prototype.setRecentItem = function (goodsCode, imageUrl) {
	goodsCode = goodsCode.toString();

	if (goodsCode.length == RecentItemsController.GOODSCODE_LENGTH) {
		if (mobileLocalStorage) {
			this.refreshItems();

			var gmktRecentItems = mobileLocalStorage.getItem("GMKTRecentItems");
			var recentItems;
			if (gmktRecentItems) {
				recentItems = JSON.parse(gmktRecentItems);
			} else {
				recentItems = [];
			}

			if (recentItems.length > RecentItemsController.NUMBER_OF_RECENT_ITEMS) {
				var howMany = recentItems.length - RecentItemsController.NUMBER_OF_RECENT_ITEMS;
				recentItems.splice(0, howMany);
			}

			for (var i = 0; i < recentItems.length; i++) {
				var eachRecentItems = recentItems[i];
				if (eachRecentItems.goodsCode == goodsCode) {
					recentItems.splice(i, 1);
				}
			}

			var expiredDate = new Date();
			expiredDate.setDate(expiredDate.getDate() + RecentItemsController.EXPIRED_DATE);

			recentItems.push({
				goodsCode: goodsCode,
				imageUrl: imageUrl,
				expiredDate: expiredDate
			});

			mobileLocalStorage.setItem("GMKTRecentItems", JSON.stringify(recentItems));
		}
	}
};

RecentItemsController.prototype.removeRecentItem = function (goodsCode) {
	goodsCode = goodsCode.toString();

	if (goodsCode.length == RecentItemsController.GOODSCODE_LENGTH) {
		if (mobileLocalStorage) {
			if (this.isMain) {
				this.refreshItems();

				var gmktRecentItems = mobileLocalStorage.getItem("GMKTRecentItems");
				var recentItems;
				if (gmktRecentItems) {
					recentItems = JSON.parse(gmktRecentItems);
				} else {
					recentItems = null;
				}

				if (recentItems == null) {
					return;
				} else {
					for (var i = 0; i < recentItems.length; i++) {
						var eachRecentItems = recentItems[i];
						if (eachRecentItems.goodsCode == goodsCode) {
							recentItems.splice(i, 1);
						}
					}

					mobileLocalStorage.setItem("GMKTRecentItems", JSON.stringify(recentItems));
				}
			} else {
				this.$iframe[0].contentWindow.postMessage({
					method: "removeRecentItem",
					goodsCode: goodsCode
				}, __MOBILE_WEB_URL_SECURE);
			}
		}
	}
};

RecentItemsController.prototype.refreshItems = function () {
	if (mobileLocalStorage) {
		if (this.isMain) {
			var gmktRecentItems = mobileLocalStorage.getItem("GMKTRecentItems");
			var recentItems;
			if (gmktRecentItems) {
				recentItems = JSON.parse(gmktRecentItems);
			} else {
				recentItems = [];
			}
			
			var today = new Date();
			for (var i = 0; i < recentItems.length; i++) {
				if (new Date(recentItems[i].expiredDate) > today) {
					recentItems.splice(i, 1);
				}
			}
		} else {
			this.$iframe[0].contentWindow.postMessage({
				method: "refreshItems"
			}, __MOBILE_WEB_URL_SECURE);
		}
	}
};





var RecentKeywordsController = function (isMain, iframeSelector) {
	this.isMain = false;
	this.$iframe = $();

	if (isMain) {
		this.isMain = true;
	}

	if (iframeSelector) {
		this.$iframe = $(iframeSelector);
	}

	if (mobileLocalStorage) {
		if (this.isMain) {
			var gmktRecentKeywordsOn = mobileLocalStorage.getItem("GMKTRecentKeywordsOn");
			if (gmktRecentKeywordsOn) {
				this.isRecentKeywordsOn = JSON.parse(gmktRecentKeywordsOn);
			} else {
				this.isRecentKeywordsOn = null;
			}

			if (this.isRecentKeywordsOn == null || this.isRecentKeywordsOn == true) {
				this.isRecentKeywordsOn = true;

				this.refreshItems();
			}
		} else {
			this.isOn(this, function (isOn) {
				if (isOn == null) {
					this.on();
				}
				
				if (isOn == null || isOn == true) {
					this.isRecentKeywordsOn = true;

					this.refreshItems();
				}
			})
		}
	}
};

RecentKeywordsController.NUMBER_OF_RECENT_ITEMS = 10;
RecentKeywordsController.EXPIRED_DATE = 30;

RecentKeywordsController.prototype.isOn = function (context, callback) {
	if (mobileLocalStorage) {
		if (this.isMain) {
			var gmktRecentKeywordsOn = mobileLocalStorage.getItem("GMKTRecentKeywordsOn");
			if (gmktRecentKeywordsOn) {
				return JSON.parse(gmktRecentKeywordsOn);
			} else {
				return null;
			}
		} else {
			var listener = function (e) {
				if (e.data.method == "isOn") {
					if (callback) {
						callback.apply(context, [e.data.result]);	
					}
					window.removeEventListener("message", listener);
				}
			};

			window.addEventListener("message", listener);

			this.$iframe[0].contentWindow.postMessage({
				method: "isOn"
			}, __MOBILE_WEB_URL_SECURE);
		}
	}
};

RecentKeywordsController.prototype.getRecentKeywords = function (context, callback) {
	if (mobileLocalStorage) {
		if (this.isMain) {
			this.refreshItems();

			var gmktRecentKeywords = mobileLocalStorage.getItem("GMKTRecentKeywords");
			if (gmktRecentKeywords) {
				return JSON.parse(gmktRecentKeywords);
			} else {
				return null;
			}
		} else {
			var listener = function (e) {
				if (e.data.method == "getRecentKeywords") {
					if (callback) {
						callback.apply(context, [e.data.result]);
					}
					window.removeEventListener("message", listener);
				}
			}
			
			window.addEventListener("message", listener);

			this.$iframe[0].contentWindow.postMessage({
				method: "getRecentKeywords"
			}, __MOBILE_WEB_URL_SECURE);
		}
	}
};

RecentKeywordsController.prototype.setRecentKeyword = function (keyword) {
	keyword = keyword.toString();

	if (mobileLocalStorage) {
		if (this.isMain) {
			if (this.isRecentKeywordsOn) {
				this.refreshItems();

				var gmktRecentKeywords = mobileLocalStorage.getItem("GMKTRecentKeywords");
				var recentKeywords;
				if (gmktRecentKeywords) {
					recentKeywords = JSON.parse(gmktRecentKeywords);
				} else {
					recentKeywords = [];
				}

				if (recentKeywords.length > RecentKeywordsController.NUMBER_OF_RECENT_ITEMS) {
					var howMany = recentKeywords.length - RecentKeywordsController.NUMBER_OF_RECENT_ITEMS;
					recentKeywords.splice(0, howMany);
				}

				for (var i = 0; i < recentKeywords.length; i++) {
					var eachRecentKeywords = recentKeywords[i];
					if (eachRecentKeywords.keyword == keyword) {
						recentKeywords.splice(i, 1);
					}
				}

				var expiredDate = new Date();
				expiredDate.setDate(expiredDate.getDate() + RecentKeywordsController.EXPIRED_DATE);

				recentKeywords.push({
					keyword: keyword,
					expiredDate: expiredDate
				});

				mobileLocalStorage.setItem("GMKTRecentKeywords", JSON.stringify(recentKeywords));
			}
		} else {
			this.$iframe[0].contentWindow.postMessage({
				method: "setRecentKeyword",
				keyword: keyword
			}, __MOBILE_WEB_URL_SECURE);
		}
	}
};

RecentKeywordsController.prototype.removeRecentKeyword = function (keyword) {
	keyword = keyword.toString();

	if (mobileLocalStorage && this.isRecentKeywordsOn) {
		if (this.isMain) {
			this.refreshItems();

			var gmktRecentKeywords = mobileLocalStorage.getItem("GMKTRecentKeywords");
			var recentKeywords;
			if (gmktRecentKeywords) {
				recentKeywords = JSON.parse(gmktRecentKeywords);
			} else {
				recentKeywords = null;
			}

			if (recentKeywords == null) {
				return;
			} else {
				for (var i = 0; i < recentKeywords.length; i++) {
					var eachRecentKeywords = recentKeywords[i];
					if (eachRecentKeywords.keyword == keyword) {
						recentKeywords.splice(i, 1);
					}
				}

				mobileLocalStorage.setItem("GMKTRecentKeywords", JSON.stringify(recentKeywords));
			}
		} else {
			this.$iframe[0].contentWindow.postMessage({
				method: "removeRecentKeyword",
				keyword: keyword
			}, __MOBILE_WEB_URL_SECURE);
		}
	}
};

RecentKeywordsController.prototype.removeAll = function () {
	if (mobileLocalStorage) {
		if (this.isMain) {
			if (this.isRecentKeywordsOn) {
				mobileLocalStorage.removeItem("GMKTRecentKeywords");
			}
		} else {
			this.$iframe[0].contentWindow.postMessage({
				method: "removeRecentKeywordsAll"
			}, __MOBILE_WEB_URL_SECURE);
		}
	}
};

RecentKeywordsController.prototype.refreshItems = function () {
	if (mobileLocalStorage) {
		var gmktRecentKeywords = mobileLocalStorage.getItem("GMKTRecentKeywords");
		var recentKeywords;
		if (gmktRecentKeywords) {
			recentKeywords = JSON.parse(gmktRecentKeywords);
		} else {
			recentKeywords = [];
		}

		var today = new Date();
		for (var i = 0; i < recentKeywords.length; i++) {
			if (new Date(recentKeywords[i].expiredDate) > today) {
				recentKeywords.splice(i, 1);
			}
		}
	}
};

RecentKeywordsController.prototype.on = function () {
	if (mobileLocalStorage) {
		if (this.isMain) {
			mobileLocalStorage.setItem("GMKTRecentKeywordsOn", JSON.stringify(true));
		} else {
			this.$iframe[0].contentWindow.postMessage({
				method: "on"
			}, __MOBILE_WEB_URL_SECURE);
		}

		this.isRecentKeywordsOn = true;
	}
};

RecentKeywordsController.prototype.off = function () {
	if (mobileLocalStorage) {
		if (this.isMain) {
			mobileLocalStorage.setItem("GMKTRecentKeywordsOn", JSON.stringify(false));
		} else {
			this.$iframe[0].contentWindow.postMessage({
				method: "off"
			}, __MOBILE_WEB_URL_SECURE);
		}

		this.isRecentKeywordsOn = false;
	}
};



var searchSlide;

var HeaderExtendedLayerController = function (recentItemsController, recentKeywordsController) {
	this.$recentWrapper = $("#list_recent");
	this.$suggestWrapper = $("#scb_lyr");

	this.$recentWrapper.insertAfter("#head");
	this.$suggestWrapper.insertAfter("#header_recent_wrapper");

	this.recentItemsLayer = new RecentItemsLayer("#header_recent_item_wrapper", "#headerRecentItem", recentItemsController);
	this.recentKeywordsLayer = new RecentKeywordsLayer("#header_recent_keyword_wrapper", "#headerRecentKeyword", recentKeywordsController);

	var that = this;

	$("#onRecentKeywords").on("click", function (e) {
		e.preventDefault();
		that.recentKeywordsLayer.recentKeywordsController.on();

		that.render();
	});

	$("#offRecentKeywords").on("click", function (e) {
		e.preventDefault();
		that.recentKeywordsLayer.recentKeywordsController.off();

		that.render();
	});

	var $searchInput = $("#scb_ip");
	$searchInput.on("focus click", function () {
		if(GMHeader.headerType === HeaderTypeEnum.Type.Normal){
			$('#head').addClass('main_search');
		}
	    $(this).addClass("focus");
	    $("#wrap").addClass("search_focus");
	    $(window).scrollTop(0);
		that.$recentWrapper.show();
		if (searchSlide)
			searchSlide.refresh();

		var keyword = $(this).val();

		if (keyword.length == 0) {
			that.$suggestWrapper.hide();
			$("#header_extended_input_wrapper").show();
			$(window).resize();
		} else {
			$("#header_extended_input_wrapper").hide();

			if ($("#relate_keyword").html().trim() == "") {
				lastKeyword = "";
			} else {
				that.$suggestWrapper.show();
			}
		}

		if($('#vipOptionArea').length > 0) {
		if($('#vipOptionArea .fixed_btn').is(':visible') == false) {
				$('#vipOption .btn_option').trigger('click');
				$('#wrap').addClass('vip_search');
				return;
			}
		}
						
		sendKeywordOnce();
	});
	$searchInput.on("focusout", function () {
		$(this).removeClass("focus");
		$("#wrap").removeClass("search_focus");
	});

	$("#removeAllRecentKeywords").on("click", function () {
		that.recentKeywordsLayer.removeAll();
	});

	$("#header_btn_voice").on("click", function () {
		// 안드로이드일 경우 스킴처리.
		if (navigator.userAgent.match('Android') != null) {
			document.location = "intent://search?type=voice#Intent;scheme=gmarket;package=com.ebay.kr.gmarket;end";
			//gotoAppStoreMarket("G마켓 앱을 설치하시면 음성인식으로 편리한 쇼핑이 가능합니다", true);
		}
		else {
			gotoAppStoreMarket("G마켓 앱을 설치하시면 음성인식으로 편리한 쇼핑이 가능합니다", true);
			document.location = "gmarket://search?type=voice";
		}
	});

	$("#header_btn_qr").on("click", function () {
		if (navigator.userAgent.match('Android') != null) {
			document.location = "intent://search?type=qrcode#Intent;scheme=gmarket;package=com.ebay.kr.gmarket;end";
			//gotoAppStoreMarket("G마켓 앱을 설치하시면 QR코드로 편리한 쇼핑이 가능합니다", true);
		} 
		else {
			gotoAppStoreMarket("G마켓 앱을 설치하시면 QR코드로 편리한 쇼핑이 가능합니다", true);
			document.location = "gmarket://search?type=qrcode";
		}
	});

	$("#header_btn_barcode").on("click", function () {
		if (navigator.userAgent.match('Android') != null) {
			document.location = "intent://search?type=barcode#Intent;scheme=gmarket;package=com.ebay.kr.gmarket;end";
			//gotoAppStoreMarket("G마켓 앱을 설치하시면 바코드로 편리한 쇼핑이 가능합니다", true);
		}
		else {
			gotoAppStoreMarket("G마켓 앱을 설치하시면 바코드로 편리한 쇼핑이 가능합니다", true);
			document.location = "gmarket://search?type=barcode";
		}
	});

	$("#viewAllRecentKeywords").on("click", function () {
		that.recentKeywordsLayer.viewAll();
	});

	$("#closeRecent").on("click", function () {
		$('#cancel_search_button').trigger('click');
	});

	function gotoAppStoreMarket(msg, isShowAlert) {
		setTimeout(function () {
			if (!isShowAlert) return;
			if (!confirm(msg)) return;

			if (navigator.userAgent.indexOf("iPhone") > -1 || navigator.userAgent.indexOf("iPod") > -1) {
				document.location = "http://itunes.apple.com/kr/app/gmarket/id340330132?mt=8";
			} else if (navigator.userAgent.indexOf("iPad") > -1) {
				document.location = "http://itunes.apple.com/kr/app/gmarket/id404552480?mt=8";
			} else {
				document.location = "market://market.android.com/details?id=com.ebay.kr.gmarket";
			}
		}, 350);
	}

	this.render();
};

HeaderExtendedLayerController.prototype.render = function () {
	this.recentItemsLayer.render(this, function () {
		this.recentKeywordsLayer.render(this, function () {
//			this.recentKeywordsLayer.recentKeywordsController.isOn(this, function (isOn) {
//				if (false == this.recentItemsLayer.hasItems && false == isOn) {
//					$("#header_no_recent_all").show();
//					//$("#onRecentKeywordsInNoAll").show();

//					$("#header_off_recent_keyword").show();
//				} else if (false == this.recentItemsLayer.hasItems && false == this.recentKeywordsLayer.hasKeywords) {
//					$("#header_no_recent_all").show();
//					$("#header_no_recent_keyword").show();
//				} else {
//					$("#header_no_recent_all").hide();
//				}
//			})
		});	
	});
};





var HeaderExtendedLayer = function (wrapperSelector, templateSelector) {
	this.$wrapper = $();
	this.template = null;

	if (wrapperSelector) {
		this.$wrapper = $(wrapperSelector);
	}

	if (templateSelector) {
		this.template = Handlebars.compile($(templateSelector).html());
	}
};





var RecentItemsLayer = function (wrapperSelector, templateSelector, recentItemsController) {
	HeaderExtendedLayer.apply(this, [wrapperSelector, templateSelector]);

	this.recentItemsController = null;
	this.hasItems = true;
	this.scrollTab = null;

	if (recentItemsController) {
		this.recentItemsController = recentItemsController;
	}
};

RecentItemsLayer.NUMBER_OF_RECENT_ITEMS = 10;

RecentItemsLayer.prototype = new HeaderExtendedLayer();
RecentItemsLayer.prototype.constructor = RecentItemsLayer;
RecentItemsLayer.prototype.parent = HeaderExtendedLayer.prototype;

RecentItemsLayer.prototype.render = function (context, callback) {
	if (mobileLocalStorage && this.recentItemsController) {
		this.recentItemsController.getRecentItems(this, function (recentItems) {
			if (recentItems && recentItems.length > 0) {
				var someRecentItems = recentItems.slice().reverse().slice(0, RecentItemsLayer.NUMBER_OF_RECENT_ITEMS);

				if (location.protocol == "https:") {
					for (var i = 0; i < someRecentItems.length; i++) {
						someRecentItems[i].imageUrl = someRecentItems[i].imageUrl.replace("http://", "https://ssl");
					}
				}

				this.$wrapper.html(this.template({ list: someRecentItems }));

				$("#header_no_recent_item").hide();

				if (recentItems.length > RecentItemsLayer.NUMBER_OF_RECENT_ITEMS) {
					//더보기
					$("#li_view_more_recent_item").show();
					$("#header_recent_item_wrapper").append($("#li_view_more_recent_item").clone(true));
				} else {
					$("#li_view_more_recent_item").hide();
				}

				this.setFlicking();

			} else {
				// 최근본상품 없음
				$("#list_recent_right").hide();
				$("#header_no_recent_item").show();
				this.hasItems = false;
			}

			if (callback) {
				callback.apply(context, []);
			}
		});
	}
};

RecentItemsLayer.prototype.removeRecentItem = function (goodsCode) {
	var answer = confirm("해당 최근본상품을 삭제하시겠습니까?");

	if (answer) {
		if (this.recentItemsController) {
			this.recentItemsController.removeRecentItem(goodsCode);

			$("#header_item_" + goodsCode).remove();
			$("#item_" + goodsCode).remove();

			this.render();

			try {
				headerExtendedLayerController.render();
			} catch (e) {}

			try {
				recentItemsPageController.refresh();
			} catch (e) {}
		}
	}
};

RecentItemsLayer.prototype.setFlicking = function () {
	var currentX = -1;
	var $maxItemLI = $('#scroll_in li');
	var $maxItemUL = $('#scroll_in ul');
	var maxValue = ($maxItemLI.width()) * $maxItemLI.length;
	$maxItemUL.css('width', maxValue + 'px');

	RecentItemsLayer.prototype.initIscroll('scroll_in', maxValue);

	if (RecentItemsLayer.scrollTab) {
		if (currentX > 0) {
			RecentItemsLayer.scrollTab.currPageX = currentX;
			RecentItemsLayer.scrollTab.scrollToPage(currentX, 0, 0);
		}
	}
};

RecentItemsLayer.prototype.initIscroll = function (id, max) {
	var that = this;
	$.ajax({
		type: "GET",
		url: '//script.gmarket.co.kr/js/mitem/iscroll_y.js',
		dataType: "script",
		cache: true,
		success: function () {
			var targetId = id;

			if (max > document.getElementById(targetId).offsetWidth) {
				if (this.scrollTab == null) this.scrollTab = new iScroll(targetId, { hScroll: true, vScroll: false, hScrollbar: false, vScrollbar: false, useTransform: false, snap: 'li',
					onScrollEnd: function () {
						var ul = document.getElementById(targetId).querySelector('ul');
						var pos = parseInt(ul.style.left.replace('px', ''));
					},
					momentum: false,
					onBeforeScrollStart: function (e) {
						if (e.touches != undefined) {
							point = e.touches[0];
							pointStartX = point.pageX;
							pointStartY = point.pageY;
							null;
						}
					},
					onBeforeScrollMove: function (e) {
						if (e.touches != undefined) {
							deltaX = Math.abs(point.pageX - pointStartX);
							deltaY = Math.abs(point.pageY - pointStartY);
							if (deltaX >= deltaY) {
								e.preventDefault();
							} else {
								null;
							}
						}
					}
				});
				that.scrollTab = this.scrollTab;
				searchSlide = this.scrollTab;
			}
		}
	});
};

var RecentKeywordsLayer = function (wrapperSelector, templateSelector, recentKeywordsController) {
	HeaderExtendedLayer.apply(this, [wrapperSelector, templateSelector]);

	this.recentKeywordsController = null;
	this.hasKeywords = true;
	this.numberOfMaxKeywords = 6;

	if (recentKeywordsController) {
		this.recentKeywordsController = recentKeywordsController;
	}
};

//RecentKeywordsLayer.NUMBER_OF_RECENT_ITEMS = 6;

RecentKeywordsLayer.prototype = new HeaderExtendedLayer();
RecentKeywordsLayer.prototype.constructor = RecentKeywordsLayer;
RecentKeywordsLayer.prototype.parent = HeaderExtendedLayer.prototype;

RecentKeywordsLayer.prototype.render = function (context, callback) {
	if (mobileLocalStorage && this.recentKeywordsController) {
		this.recentKeywordsController.isOn(this, function (isOn) {
			if (isOn) {
				this.recentKeywordsController.getRecentKeywords(this, function (recentKeywords) {
					if (recentKeywords && recentKeywords.length > 0) {
						var someRecentKeywords = recentKeywords.slice().reverse().slice(0, this.numberOfMaxKeywords);

						this.$wrapper.html(this.template({ list: someRecentKeywords }));
						$("#header_have_recent_keyword").show();
						$("#header_off_recent_keyword").hide();

						if (recentKeywords.length > someRecentKeywords.length) {
							$("#header_view_more").show();
						} else {
							$("#header_view_more").hide();
						}
					} else {
						// 최근검색어 없음
						$("#header_have_recent_keyword").hide();
						$("#header_no_recent_keyword").show();

						this.hasKeywords = false;
					}

					if (callback) {
						callback.apply(context, []);
					}
				});
			} else {
				// 최근검색어 저장 OFF
				$("#header_have_recent_keyword").hide();
				$("#header_off_recent_keyword").show();

				if (callback) {
					callback.apply(context, []);
				}
			}
		});
	}
};

RecentKeywordsLayer.prototype.removeRecentKeyword = function (keyword, dom) {
	var answer = confirm("해당 최근검색어를 삭제하시겠습니까?");

	if (answer) {
		if (this.recentKeywordsController) {
			this.recentKeywordsController.removeRecentKeyword(keyword);

			this.render();

			try {
				headerExtendedLayerController.render();
			} catch (e) {}
		}
	}
};

RecentKeywordsLayer.prototype.removeAll = function () {
	var answer = confirm("최근검색어를 모두 삭제하시겠습니까?");

	if (answer) {
		if (this.recentKeywordsController) {
			this.recentKeywordsController.removeAll();

			this.render();

			try {
				headerExtendedLayerController.render();
			} catch (e) {}
		}
	}
};

RecentKeywordsLayer.prototype.viewAll = function () {
	if (this.recentKeywordsController) {
		this.recentKeywordsController.getRecentKeywords(this, function (recentKeywords) {
			this.numberOfMaxKeywords = recentKeywords.length;
		});
		this.render();
	}
};

var isMobileBrowser = function () {
	var isMobile = false;
	var agent = navigator.userAgent || navigator.vendor || window.opera;

	if (/android|avantgo|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od|ad)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(agent) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|e\-|e\/|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\-|2|g)|yas\-|your|zeto|zte\-/i.test(agent.substr(0, 4))) {
		isMobile = true;
	}
	return isMobile;
}


var CheckPc = function (type) {
	if (!isMobileBrowser()) {
		alert("‘카카오톡’으로의 공유 기능은\n모바일 기기에서 사용하실 수 있습니다.");
		return false;
	}
}


//3.5 버전 KakaoAPI 

var KakaoInit = function () {
	//JavaScript API Key
	Kakao.init('bb15b6fe0a91041ec32fd7b66f86a598');
}

var KakaoController = function (containerID, data, page) {

	app = '[G마켓]\n';
	if (page == "home") {
		Kakao.Link.createTalkLinkButton({
			container: containerID,
			label: app + data.msg,
			webLink: {
				text: data.url,
				url: data.url
			},
			fail: function () {
				console.log("ERROR: cannot create KakaoTalk Link on PC"); //PC 일때 발생하는 Error
			}
		});
	}

	else if (page == "vip") {
		Kakao.Link.createTalkLinkButton({
			container: containerID,
			label: data.msg,
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
		});
	}

}

var CartCountStorage = function () {
	this.key = "GMKTCartCount";
	this.storage = mobileLocalStorage;
}

CartCountStorage.prototype.getCount = function () {
	var gmktCartCount = this.storage.getItem(this.key);
	if (gmktCartCount) {
		return JSON.parse(gmktCartCount);
	} else {
		return null;
	}
}

CartCountStorage.prototype.setCount = function (count, expireTime) {
	var cartCount = {};

	if (expireTime === undefined || expireTime == null) {
		var newExpireTime = new Date();
		newExpireTime.setHours(newExpireTime.getHours() + 1);
		expireTime = newExpireTime;
	}

	cartCount.count = count;
	cartCount.expireTime = expireTime;

	this.storage.setItem(this.key, JSON.stringify(cartCount));	

	return this.getCount();
}

CartCountStorage.prototype.removeCount = function () {
	this.storage.removeItem(this.key);

	return this.getCount();
}

var CartCountController = function (option) {
	this.GET_COUNT = "getCount";
	this.SET_COUNT = "setCount";
	this.REMOVE_COUNT = "removeCount";

	this.isReady = false;
	this.isLogin = false;
	this.isApp = true;
	this.$iframe = $();
	this.$icon = $();
	this.$cart = $();

	this.q = [];

	this.setOptions(option);
}

CartCountController.prototype.setOptions = function (option) {
	if (option) {
		if (option.isLogin !== undefined && option.isLogin !== null) this.isLogin = option.isLogin;
		if (option.isApp !== undefined && option.isApp !== null) this.isApp = option.isApp;
		if (option.iframeSelector) this.$iframe = $(option.iframeSelector);
		if (option.iconSelector) this.$icon = $(option.iconSelector);
		if (option.cartSelector) this.$cart = $(option.cartSelector);
	}
}

CartCountController.prototype.ajaxSyncCount = function (data, success, error) {
	jQuery.ajax({
		type: "GET",
		url: __HEADER_FOOTER_DOMAIN_URL + "/Common/GetCartCount",
		dataType: "jsonp",
		data: data,
		context: this,
		success: success,
		error: error
	}); 
}

CartCountController.prototype.syncAfterAddCart = function (plusCount, pid) {
	var _this = this;
	pid = pid || "";

	if (!this.isLogin) {
		this.removeCount();
		return;
	}

	this.ajaxSyncCount({ pid: pid }, function (data) {
		if (data && data.success) {
			//console.log('sync-afteraddcart', data); // TODO : Remove
			this.setCount(data.count);
		}
	}, function () {
		this.plusCount(plusCount);
	});
}

CartCountController.prototype.syncAfterLogin = function (redirectUrl) {
	var _this = this;
	var isRedirect = (redirectUrl !== undefined && redirectUrl != null && redirectUrl != "");

	this.ajaxSyncCount({}, function (data) {
		if (data && data.success) {
			//console.log('sync-afterlogin', data); // TODO : Remove

			this.setCount(data.count, null, this, function() {
				if (isRedirect) location.replace(redirectUrl);
			});			
		}
	}, function () {
		if (isRedirect) location.replace(redirectUrl);
	});
}

CartCountController.prototype.needSync = function (result) {
	var now = new Date();
	var isExpired = true;

	if (result && result.expireTime) {
		isExpired = now > new Date(result.expireTime);
		//console.log('needSync(now, expireTime)', now, new Date(result.expireTime)); // TODO : Remove
	}

	return this.isLogin && isExpired;
}

CartCountController.prototype.syncCount = function () {
	var _this = this;

	if (!this.isLogin) {
		this.removeCount();
		return;
	}

	this.getCount(this, function (result) {
		if (this.needSync(result)) {
			this.ajaxSyncCount({}, function (data) {
				if (data && data.success) {
					//console.log('sync', data); // TODO : Remove
					this.setCount(data.count);
				}
			}, function () { });
		} else {
			this.render(result);
		}
	});
}

CartCountController.prototype.render = function (result) {
	//console.log("render", result); // TODO : Remove

	if (result && result.count && result.count > 0) {
		this.$cart.addClass("cart_in");
		this.$icon.text(result.count);
		this.$icon.show();
	} else {
		this.$cart.removeClass("cart_in");
		this.$icon.hide();
	}
}

CartCountController.prototype.ready = function () {
	this.isReady = true;

	while (this.q.length > 0) {
		var fromQ = this.q.shift() || {};
		switch (fromQ.method) {
			case this.GET_COUNT:
				this.getCount(fromQ.context, fromQ.callback);
				break;
			case this.SET_COUNT:
				this.setCount(fromQ.count, fromQ.expireTime, fromQ.context, fromQ.callback);
				break;;
			case this.REMOVE_COUNT:
				this.removeCount(fromQ.context, fromQ.callback);
				break;
		}
	}
}

CartCountController.prototype.getCount = function (context, callback) {
	var _this = this;

	if (!mobileLocalStorage) return;

	if (!this.isReady) {
		this.q.push({ method: this.GET_COUNT, context: context, callback: callback });
		return;
	}
	
	var listener = function (e) {
		if (e.data.method == _this.GET_COUNT) {
			if (callback && typeof callback === 'function') {
				callback.apply(context, [e.data.result]);
			}
			window.removeEventListener("message", listener);
		}
	};

	window.addEventListener("message", listener);

	this.$iframe[0].contentWindow.postMessage({
		method: this.GET_COUNT
	}, __MOBILE_WEB_URL_SECURE);
}

CartCountController.prototype.setCount = function (count, expireTime, context, callback) {	
	var _this = this;

	if (!mobileLocalStorage) return;

	if (!this.isReady) {
		this.q.push({ method: this.SET_COUNT, count: count, expireTime: expireTime, context: context, callback: callback });
		return;
	}
	
	var listener = function (e) {
		if (e.data.method == _this.SET_COUNT) {
			if (callback && typeof callback === 'function') {
				callback.apply(context, [e.data.result]);
			} else {
				// Default callback for setCount
				_this.render(e.data.result);

				if (_this.isApp) {
					location.href = "gmarket://webview?action=cartCount&value=" + e.data.result.count;
				}
			}
			window.removeEventListener("message", listener);
		}
	};

	window.addEventListener("message", listener);
	this.$iframe[0].contentWindow.postMessage({
		method: this.SET_COUNT,
		count: count,
		expireTime: expireTime
	}, __MOBILE_WEB_URL_SECURE);
}

CartCountController.prototype.removeCount = function (context, callback) {
	var _this = this;

	if (!mobileLocalStorage) return;

	if (!this.isReady) {
		this.q.push({ method: this.REMOVE_COUNT, context: context, callback: callback });
		return;
	}

	var listener = function (e) {
		if (e.data.method == _this.REMOVE_COUNT) {
			if (callback && typeof callback === 'function') {
				callback.apply(context, [e.data.result]);
			}
			window.removeEventListener("message", listener);
		}
	};

	window.addEventListener("message", listener);
	this.$iframe[0].contentWindow.postMessage({
		method: this.REMOVE_COUNT,
	}, __MOBILE_WEB_URL_SECURE);
}

CartCountController.prototype.plusCount = function (plusCount, pid) {
	var _this = this;
	var count = plusCount;

	if (pid) {
		this.syncAfterAddCart(plusCount, pid);
	} else {
		this.getCount(this, function (data) {
			if (data && data.count) count = data.count + count;
			this.setCount(count, data.expireTime);
		});
	}
}

CartCountController.prototype.minusCount = function (minusCount) {
	var _this = this;

	this.getCount(this, function (data) {
		var count = minusCount;
		if (data && data.count) count = data.count - count;
		if (count < 0) count = 0;

		this.setCount(count, data.expireTime);
	});
}

var cartCountController = new CartCountController();
