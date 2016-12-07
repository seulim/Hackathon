function doSubmit() {
	//뒤공백 제거 후 비교
	if( (document.searchForm.topKeyword.value).replace(/\s*$/,"") == ""){
		if (isMain == true){
			location.href = "http://sna.gmarket.co.kr/?fcd=703100005&url=" + encodeURIComponent("@Urls.MobileWebUrl");
		}
		alert("검색어를 입력해 주세요");
	}else{
		if (isMain == true){
			location.href = "http://sna.gmarket.co.kr/?fcd=703100005&url=" + encodeURIComponent("@Urls.MobileWebUrl/Search/Search?topKeyword=" + encodeURIComponent(searchForm.topKeyword.value));
		}else{
			document.searchForm.submit();
		}
	}
}

function displayResult() {
	if (httpRequest.readyState == 4) {
		if (httpRequest.status == 200) {
			var html = httpRequest.responseText;
			var listView = document.getElementById('recommand_keyword');
			if (html.length > 100) {
				listView.innerHTML = html;
				hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'Y');
			} else {
				listView.innerHTML = '';
				hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'N');
			}
		} else {
			hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'N');
		}
	}
}

function clearTopSearch() {
	var obj = document.getElementById('scb_ip');
	obj.value = ''
	obj.focus();
	hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'N');
}

function goSearch(keyword) {
	searchForm.topKeyword.value = keyword;
	doSubmit();
}


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

function checkAndroid( homeURL )
{
	try
	{
		var app_pcid = getAppCookieValue("pcid");
		var app_navimode = getAppCookieValue("navigation_mode");

		// 안드로이드 어플일 경우 스킴처리.
		if ( app_pcid.length > 0 && app_pcid.substring(0,1) == "3"){
			document.location.href = "gmarket://home";
		} else if ( app_pcid.length > 0 && app_pcid.substring(0,1) == "4" && app_navimode == "pc"){
			document.location.href = "http://www.gmarket.co.kr";
		} else {
			document.location.href = homeURL;
		}
	}
	catch( ex )
	{
	}
}

function checkMmygLink( Home )
{
	var c_name = "pcid";
	if (document.cookie.length > 0) {
		c_start = document.cookie.indexOf(c_name + "=");
		if (c_start != -1) {
			c_start = c_start + c_name.length + 1;
			c_end = document.cookie.indexOf(";", c_start);
			if (c_end == -1) c_end = document.cookie.length;

			if (unescape(document.cookie.substring(c_start, c_end)) == "") {
					document.location.href = Home;
			} else {
				var app_mmyglink = getAppCookieValue("mmyglink");

				if (app_mmyglink.length > 0 && app_mmyglink == "Y") {
					document.location.href = Home;
				} else {
					document.location.href = mwUrl;
				}
			}
		}else {
			document.location.href = Home;
		}
	} 
}

function clearBoxBtn()
{ 
	if ($("#scb_ip").val() == "") {
		$("#scb_del").hide();
	}
}

function showCategory_slim(subNum, ctgyNum){
	var nCtgyNum = -1;
	var nSubNum = -1;
	var ctgrGrpLength = $("#hd_slim .ctgr").length; //Category Count	
	var ctgrSubLength = $("#hd_slim .ctgr_sub").length;	//CategorySub Count

	for(var i=0;i<=ctgrGrpLength;i++){
		var ctgrGrp = $("#ctgr" + i);
		var ctgrGrpCnm = ctgrGrp.attr("class");

		if(i == ctgyNum){
			if(ctgrGrpCnm.indexOf("on") >= 0){
				if(nCtgyNum !== ctgyNum){
					ctgrGrp.removeClass("on");
				}
				if(nSubNum == subNum){
					ctgrGrp.removeClass("on");
				}
			}else{
				ctgrGrp.addClass("on");
			}
		}
		else{
			ctgrGrp.removeClass("on");
		}
	}
	nCtgyNum = ctgyNum;
	nSubNum = subNum;

	for(var i=0;i<=ctgrSubLength;i++){
		var ctgrSub = $("#ctgr_sub" + i);
		var ctgr = $(".ctgr" + (i+1));

		if(i == subNum && ctgrSub.css("display") != "block"){
			ctgr.addClass("on");
			ctgrSub.css("display", "block");
		}
		else{
			ctgr.removeClass("on");
			ctgrSub.css("display", "none");
		}
	}
}

 function getUrl(defaultUrl) {
	var currentUrlpath = window.location.pathname.toLowerCase();
	var laststr = currentUrlpath.substr(-1, 1)
	if (laststr == "/") {
		currentUrlpath = currentUrlpath.substr(0, currentUrlpath.length - 1);
	}
	//alert("currentUrlpath :" + currentUrlpath);

	switch (currentUrlpath) {
		case "/eventzone/mybenefithome":
		case "/eventzone/mybenefit":
		case "/eventzone/mybenefitcoupon":
		case "/eventzone/couponcategory":
		case "/pluszone":
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