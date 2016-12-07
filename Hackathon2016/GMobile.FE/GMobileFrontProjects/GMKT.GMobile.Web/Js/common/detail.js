/*
//허니콤 (갤럭시 탭 10)
if (navigator.userAgent.indexOf("Android 3") > -1 ){
	var index = document.cookie.indexOf("appEmbweb=");
	if (index > -1 && document.cookie.substring(index+10, index+11) == "Y") {
		$('meta.#viewport').attr('content', 'user-scalable=yes,  initial-scale=0.66, maximum-scale=0.66, minimum-scale=0.66, width=device-width, target-densitydpi=device-dpi');
	} else {
		$('meta.#viewport').attr('content', 'user-scalable=no,  initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, width=medium-dpi');
	}
	//아이스크림 (갤럭시 넥서스 )
}else if(  navigator.userAgent.indexOf("Android 4") > -1  || navigator.userAgent.indexOf("SHW-M110S") > -1  || navigator.userAgent.indexOf("SHW-M250S") > -1	){
	  if (   (document.URL.indexOf("/Order/ccOrderDetail") >  -1 ||  document.URL.indexOf("/Order/viewOrderDetailInfo") >  -1 ||	 document.URL.indexOf("/Product/productBasicInfo") >  -1)){
		  $('meta.#viewport').attr('content', 'user-scalable=yes,  initial-scale=1.0, minimum-scale=1.0,  width=480,  target-densitydpi=device-dpi');
	  }else{
		  $('meta.#viewport').attr('content', 'user-scalable=no,  initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=480 , target-densitydpi=device-dpi');
	  }
// HTC
}else if(  navigator.userAgent.indexOf("HTC") > -1 ){
	$('meta.#viewport').attr('content', 'user-scalable=no,  initial-scale=1.0, maximum-scale=1.0,  width=480 , target-densitydpi=device-dpi');
}
*/
function keyPress() {
	alert("keyPress");
	if ( window.event.keyCode == 13 ) {
		doSubmit();
	} else {//if ( ( window.event.keyCode >= 65 && window.event.keyCode <= 90 )
		   //	 || ( window.event.keyCode >= 48 && window.event.keyCode <= 57 ) ) {
		if ( window.navigator.appName.indexOf("Explorer") > 0 )
			autoComplete();
		else
			timer = this.setTimeout('autoComplete();', 250);
	}
	return true;
}
var tempDecSearchKeyword = "";

function autoComplete() {
	//document.getElementById('test1').innerHTML = document.getElementById('test1').innerHTML +timer+"=";
		   //alert();
	if ( document.searchForm.decSearchKeyword.value == document.searchForm.searchKeyword.value && tempDecSearchKeyword !=  document.searchForm.searchKeyword.value ) {
	  // alert(document.searchForm.decSearchKeyword.value +"==="+ document.searchForm.searchKeyword.value);

		if ( document.searchForm.decSearchKeyword.value == "" ) {
			fncSearchList();
		  //  document.getElementById('kwd_new').style.display="none";

		}else{
			try {
				var param = "";
				var url = "/MW/Common/getAutoCompleteAjax.tmall";
				param = "getAutoCompleteKey="+document.searchForm.decSearchKeyword.value;
				tempDecSearchKeyword =document.searchForm.decSearchKeyword.value;
				fn_getValueByAjax(url,
					 "POST",
					 param,
					 true,
					 function(jsonData){
						 var data = eval(jsonData);
						 showAutoComplete(myJSONObject, myJSONObject2, myJSONObject3, myJSONObject4);

						 if(document.getElementById('decSearchKeyword').value.length > 0){
							document.getElementById("clrt").style.display = "";
						 }
					 },
					 "text");
				return;

			} catch ( err ) {
				alert(err);
			}
		}
	} else {
		document.searchForm.searchKeyword.value = document.searchForm.decSearchKeyword.value;

	}
	if ( window.navigator.appName.indexOf("Explorer") < 0 ){
		clearTimeout(timer);
		clearInterval(timer);
		timer = this.setTimeout('autoComplete();', 250);
	}

}
function showAutoComplete(obj1, obj2, obj3, obj4) {
	var divObject	= document.getElementById("autoComplete");
	var keyDivObject = document.getElementById("autoCompleteKeyWord");
	var htmlStr		= "";
	var htmlKeywordStr = "";
	var keyword = document.searchForm.decSearchKeyword.value;

	var display = "";
	var obj1ListCount = obj1.LIST.length;
	var obj3ListCount = obj3.LIST.length;
	var objListCountMax = 0;
	if ( obj1ListCount > 0 || obj3ListCount > 0 ) {
		if( obj1ListCount > 5) {
			obj1ListCount = 5;
		}
		if( obj3ListCount > 5) {
			obj3ListCount = 5;
		}
		if (obj1ListCount  >  obj3ListCount ){
			objListCountMax =obj1ListCount;
		}else{
			objListCountMax =obj3ListCount;
		}
		// 앞 검색 키원드
		for ( var i = 0; objListCountMax > i ; i++ ) {
			if(i <  obj1ListCount){
				var htmlKeyword = obj1.LIST[i].KEYWORD;
				if (  obj1.LIST[i].KEYWORD.indexOf(keyword) >= 0 ) {
					//var prefix = obj1.LIST[i].KEYWORD.substring(0, obj1.LIST[i].KEYWORD.indexOf(keyword)) ;
					//var surfix = obj1.LIST[i].KEYWORD.substring(obj1.LIST[i].KEYWORD.indexOf(keyword)+keyword.length);
					htmlKeyword = obj1.LIST[i].KEYWORD.replace(keyword, "<strong>" + keyword + "</strong>");//prefix + "<strong>" + keyword +"</strong>" +  surfix;
				}
				htmlStr += "<li><a href='javascript:autoSearch(\""+obj1.LIST[i].KEYWORD+"\");'>"+htmlKeyword+"</a></li>";
			}else{
				htmlStr += "<li> </li>";
			}
		}
		// 뒤 검색 키원드
		for ( var i = 0; objListCountMax > i ; i++ ) {
			if(i <  obj3ListCount){
				htmlKeyword = obj3.LIST[i].KEYWORD.replace(keyword, "<strong>" + keyword + "</strong>");
				htmlKeywordStr += "<li><a href='javascript:autoSearch(\""+obj3.LIST[i].KEYWORD+"\");'>"+htmlKeyword+"</a></li>";
			}else{
				htmlKeywordStr += "<li> </li>";
			}
		}
		 display = "block";
	} else {
		display = "none";
	}
	divObject.innerHTML = htmlStr;
	keyDivObject.innerHTML = htmlKeywordStr;
	//document.getElementById('kwd_new').style.display=display;
	disLayerHeader( 'hy_new' ,'none');
	disLayerHeader( 'kwd_new' ,display);

	if ( window.navigator.appName.indexOf("Explorer") < 0 ){
		clearTimeout(timer);
		clearInterval(timer);
		checkFirst = false;
		 timer = this.setTimeout('autoComplete();', 250);
	}
}
function autoSearch(keyword) {
	document.searchForm.decSearchKeyword.value = keyword;
	document.searchForm.isSearchInSearch.value = "N";
	document.searchForm.isCategoryInSearch.value = "N";
	document.searchForm.isBrandInSearch.value = "N";
	doSubmit();
}
var timer;
var checkFirst = false;
function getInput() {
	if (checkFirst == false) {
		timer = this.setTimeout("autoComplete()", 250);
	}
	checkFirst = true;
}
function releaseInput() {
	if ( timer ) clearTimeout(timer);
}
function clearInputValue(elem){
	 var ele = document.getElementById(elem);
	 ele.value="";
	 ele.focus();

	 document.getElementById("clrt").style.display = "none";
	 document.getElementById("kwd_new").style.display = "none";
}


function funcCheckIsLogin() {
	var arg = "TMALL_AUTH=";
	var alen = arg.length;
	var clen = document.cookie.length;
	var i = 0;

	while(i < clen){
			var j = i + alen;
			if(document.cookie.substring(i, j) == arg)
					return true;
					i = document.cookie.indexOf(" ", i) + 1;
			if(i == 0) break;
	}
	return false;
}