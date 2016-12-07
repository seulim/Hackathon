﻿
var xhr = null;
 var npageSize =1;
function getXMLHttpRequest() {
    if (window.ActiveXObject) {
        try {
            return new ActiveXObject("Msxml2.XMLHTTP");//IE 상위 버젼
        } catch (e1) {
            try {
                return new ActiveXObject("Microsoft.XMLHTTP");//IE 하위 버젼
            } catch (e2) {
                return null;
            }
        }
    } else if (window.XMLHttpRequest) {
        return new XMLHttpRequest();//IE 이외의 브라우저(FireFox 등)
    } else {
        return null;
    }
}// XMLHttpRequest 객체 얻기
 


function requestList1(URL,PAGE,HowView,OrderString,SearchKeyword,OCI,nTotalPage) {
	

	
	var element = document.getElementById("btn_bg1");	
	npageSize =Number(npageSize)+1;	 
	if(npageSize > Number(nTotalPage) || Number(nTotalPage) <= 0){
			element.style.display = "none";
	}else{	 	
		if(Number(npageSize) >= Number(11)){
			element.style.display = "none"; 				
			npageSize =Number(npageSize)-1;
	    }    
 	} 	
 	
 	URL = URL + "?PAGE=" + npageSize+ "&HowView=" + HowView + "&OrderString=" + OrderString + "&SearchKeyword=" + escape(encodeURIComponent(SearchKeyword)) + "&npageSize=" + npageSize + "&OCI=" + OCI ;//한글 처리
 	//alert(URL);		 		  
    xhr = getXMLHttpRequest();//XMLHttpRequest 객체 얻기
    xhr.onreadystatechange = responseList;//콜백 함수  등록
    xhr.open("GET", URL, true);//연결		   				   
    xhr.send(null);//전송
    return true;
}// 서버에 요청


function requestList(URL,PAGE,HowView,OrderString,SearchKeyword,OCI,nTotalPage,exc,ExecKeyword,CategoryStr,prc1,prc2,StorePickupStr,QuickServiceStr) {
	

	
	var element = document.getElementById("btn_bg1");	
	npageSize =Number(npageSize)+1;	 
	if(npageSize > Number(nTotalPage) || Number(nTotalPage) <= 0){
			element.style.display = "none";
	}else{	 	
		if(Number(npageSize) >= Number(11)){
			element.style.display = "none"; 				
			npageSize =Number(npageSize)-1;
	    }    
 	} 	
 	
 	URL = URL + "?PAGE=" + npageSize+ "&HowView=" + HowView + "&OrderString=" + OrderString + "&SearchKeyword=" + escape(encodeURIComponent(SearchKeyword)) + "&npageSize=" + npageSize + "&OCI=" + OCI + "&exc=" +  escape(encodeURIComponent(exc)) + "&ExecKeyword=" + escape(encodeURIComponent(ExecKeyword)) + "&CategoryStr=" + CategoryStr + "&prc1=" + prc1 + "&prc2=" + prc2 + "&StorePickupStr=" + StorePickupStr + "&QuickServiceStr=" + QuickServiceStr ;//한글 처리
 	//alert(URL);		 		  
    xhr = getXMLHttpRequest();//XMLHttpRequest 객체 얻기
    xhr.onreadystatechange = responseList;//콜백 함수  등록
    xhr.open("GET", URL, true);//연결		   				   
    xhr.send(null);//전송
    return true;
}// 서버에 요청




function FeedBackrequestList(URL,PAGE,ItemID,nTotalPage) {
	var element = document.getElementById("btn_bg1");	
	npageSize =Number(npageSize)+1;

	if(npageSize > Number(nTotalPage) || Number(nTotalPage) <= 0){ //nTotalPage가 없음으로 체크 해줄 필요 없음
			element.style.display = "none";
	}else{	 	
		if(Number(npageSize) >= Number(20)){
			element.style.display = "none"; 				
			npageSize =Number(npageSize)-1;
	    }    
 	} 	
 	URL = URL + "?PAGE=" + npageSize+ "&ItemID="+ItemID ;//한글 처리 		  
    xhr = getXMLHttpRequest();//XMLHttpRequest 객체 얻기
    xhr.onreadystatechange = responseList;//콜백 함수  등록
    xhr.open("GET", URL, true);//연결		   				   
    xhr.send(null);//전송
    return true;
}// 서버에 요청

function Search100requestList(URL,PAGE,SearchID,nTotalPage) {
	
	var element = document.getElementById("btn_bg1");	
	npageSize =Number(npageSize)+1;	 
	if(npageSize > Number(nTotalPage-1) || Number(nTotalPage) < 0){
			element.style.display = "none";
	}else{	 	
		if(Number(npageSize) > Number(5)){
			element.style.display = "none"; 				
			npageSize =Number(npageSize)-1;
	    }    
 	} 	
 	
 	URL = URL + "?PAGE=" + npageSize+ "&SearchID="+SearchID+"&nTotalPage="+nTotalPage;//한글 처리 	  
    xhr = getXMLHttpRequest();//XMLHttpRequest 객체 얻기
    xhr.onreadystatechange = responseList;//콜백 함수  등록
    xhr.open("GET", URL, true);//연결		   				   
    xhr.send(null);//전송
    return true;
}// 서버에 요청
 
function responseList() {	
    if (xhr.readyState == 4) {//완료
        if (xhr.status == 200) {//오류없이 OK
            var str = xhr.responseText;//서버에서 보낸 내용 받기            
            document.getElementById("message").innerHTML += str;//보여주기 			           
        }
    }
}// 응답


/////////////////////////////suggest///////////////////////////////////////
var httpRequest = null;

function sendRequest(url, params, callback, method) {
	httpRequest = getXMLHttpRequest();
	var httpMethod = method ? method : 'GET';
	if (httpMethod != 'GET' && httpMethod != 'POST') {
		httpMethod = 'GET';
	}
	var httpParams = (params == null || params == '') ? null : params;
	var httpUrl = url;
	if (httpMethod == 'GET' && httpParams != null) {
		httpUrl = httpUrl + "?" + httpParams;
	}
	httpRequest.open(httpMethod, httpUrl, true);
	httpRequest.setRequestHeader(
		'Content-Type', 'application/x-www-form-urlencoded');
	httpRequest.onreadystatechange = callback;
	httpRequest.send(httpMethod == 'POST' ? httpParams : null);
}



	var checkFirst = false;
	var lastKeyword = '';
	var loopSendKeyword = false;
	
	function startSuggest() {		
		
		if (checkFirst == false) {
			setTimeout("sendKeyword();", 500);
			loopSendKeyword = true;
		}
		checkFirst = true;
	}
	function sendKeyword() {
	
		var Keyword="";
		if (loopSendKeyword == false) return;
		
		Keyword = searchForm.topKeyword.value;
		//alert(Keyword);  
		if (Keyword == '') {
			lastKeyword = '';
			displayLayer('sch_txtList','none');
		} else if (Keyword != lastKeyword) {
			lastKeyword = Keyword;
			
			if (Keyword != '') {
				var params = "keyword="+escape(encodeURIComponent(Keyword));				
				sendRequest("/Search/AutoComplete", params, displayResult, 'POST');
			} else {
				displayLayer('sch_txtList','none');
			}
		}
		setTimeout("sendKeyword();", 500);
	}

	function startSuggest2() {

		if (checkFirst == false) {
			setTimeout("sendKeyword2();", 500);
			loopSendKeyword = true;
		}
		checkFirst = true;
	}
	function sendKeyword2() {

		var Keyword = "";
		if (loopSendKeyword == false) return;

		Keyword = searchForm.topKeyword.value;
		//alert(Keyword);  
		if (Keyword == '') {
			lastKeyword = '';
			displayLayer('scb_lyr', 'none');
		} else if (Keyword != lastKeyword) {
			lastKeyword = Keyword;

			if (Keyword != '') {
				var params = "keyword=" + escape(encodeURIComponent(Keyword));
				sendRequest("/Search/AutoComplete", params, displayResult, 'POST');
			} else {
				displayLayer('scb_lyr', 'none');
			}
		}
		setTimeout("sendKeyword2();", 500);
	}
	
	

	
