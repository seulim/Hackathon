
var xhr = null;
 var npageSize =1;
function getXMLHttpRequest() {
    if (window.ActiveXObject) {
        try {
            return new ActiveXObject("Msxml2.XMLHTTP");//IE ���� ����
        } catch (e1) {
            try {
                return new ActiveXObject("Microsoft.XMLHTTP");//IE ���� ����
            } catch (e2) {
                return null;
            }
        }
    } else if (window.XMLHttpRequest) {
        return new XMLHttpRequest();//IE �̿��� ������(FireFox ��)
    } else {
        return null;
    }
}// XMLHttpRequest ��ü ���
 


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
 	
 	URL = URL + "?PAGE=" + npageSize+ "&HowView=" + HowView + "&OrderString=" + OrderString + "&SearchKeyword=" + escape(encodeURIComponent(SearchKeyword)) + "&npageSize=" + npageSize + "&OCI=" + OCI ;//�ѱ� ó��
 	//alert(URL);		 		  
    xhr = getXMLHttpRequest();//XMLHttpRequest ��ü ���
    xhr.onreadystatechange = responseList;//�ݹ� �Լ�  ���
    xhr.open("GET", URL, true);//����		   				   
    xhr.send(null);//����
    return true;
}// ������ ��û


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
 	
 	URL = URL + "?PAGE=" + npageSize+ "&HowView=" + HowView + "&OrderString=" + OrderString + "&SearchKeyword=" + escape(encodeURIComponent(SearchKeyword)) + "&npageSize=" + npageSize + "&OCI=" + OCI + "&exc=" +  escape(encodeURIComponent(exc)) + "&ExecKeyword=" + escape(encodeURIComponent(ExecKeyword)) + "&CategoryStr=" + CategoryStr + "&prc1=" + prc1 + "&prc2=" + prc2 + "&StorePickupStr=" + StorePickupStr + "&QuickServiceStr=" + QuickServiceStr ;//�ѱ� ó��
 	//alert(URL);		 		  
    xhr = getXMLHttpRequest();//XMLHttpRequest ��ü ���
    xhr.onreadystatechange = responseList;//�ݹ� �Լ�  ���
    xhr.open("GET", URL, true);//����		   				   
    xhr.send(null);//����
    return true;
}// ������ ��û




function FeedBackrequestList(URL,PAGE,ItemID,nTotalPage) {
	var element = document.getElementById("btn_bg1");	
	npageSize =Number(npageSize)+1;

	if(npageSize > Number(nTotalPage) || Number(nTotalPage) <= 0){ //nTotalPage�� �������� üũ ���� �ʿ� ����
			element.style.display = "none";
	}else{	 	
		if(Number(npageSize) >= Number(20)){
			element.style.display = "none"; 				
			npageSize =Number(npageSize)-1;
	    }    
 	} 	
 	URL = URL + "?PAGE=" + npageSize+ "&ItemID="+ItemID ;//�ѱ� ó�� 		  
    xhr = getXMLHttpRequest();//XMLHttpRequest ��ü ���
    xhr.onreadystatechange = responseList;//�ݹ� �Լ�  ���
    xhr.open("GET", URL, true);//����		   				   
    xhr.send(null);//����
    return true;
}// ������ ��û

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
 	
 	URL = URL + "?PAGE=" + npageSize+ "&SearchID="+SearchID+"&nTotalPage="+nTotalPage;//�ѱ� ó�� 	  
    xhr = getXMLHttpRequest();//XMLHttpRequest ��ü ���
    xhr.onreadystatechange = responseList;//�ݹ� �Լ�  ���
    xhr.open("GET", URL, true);//����		   				   
    xhr.send(null);//����
    return true;
}// ������ ��û
 
function responseList() {	
    if (xhr.readyState == 4) {//�Ϸ�
        if (xhr.status == 200) {//�������� OK
            var str = xhr.responseText;//�������� ���� ���� �ޱ�            
            document.getElementById("message").innerHTML += str;//�����ֱ� 			           
        }
    }
}// ����


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
	var searchInput, sendKeywordOnceIntervalId;
	
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
		
		if (Keyword == '') {
			lastKeyword = '';
			displayLayer('sch_txtList','none');
		} else if (Keyword != lastKeyword) {
			lastKeyword = Keyword;
			
			if (Keyword != '') {
				var params = "keyword="+escape(encodeURIComponent(Keyword));
				sendRequest("http://mobile.gmarket.co.kr/Search/AutoComplete", params, displayResult, 'POST');
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

		if (Keyword == '') {
			lastKeyword = '';
			displayLayer('scb_lyr', 'none');
		} else if (Keyword != lastKeyword) {
			lastKeyword = Keyword;

			if (Keyword != '') {
				var params = "keyword=" + escape(encodeURIComponent(Keyword));
				sendRequest("http://mobile.gmarket.co.kr/Search/AutoComplete", params, displayResult, 'POST');
			} else {
				displayLayer('scb_lyr', 'none');
			}
		}
		setTimeout("sendKeyword2();", 500);
	}

	function sendKeywordOnce() {
		var keyword = searchForm.topKeyword.value;
		if (!searchInput) {
			searchInput = document.getElementById("scb_ip");
		}
		
		if (keyword != lastKeyword) {
			lastKeyword = keyword;

			if (keyword != '') {
				var params = "keyword=" + escape(encodeURIComponent(keyword));
				sendRequest("http://mobile.gmarket.co.kr/Search/AutoComplete", params, displayResult, 'POST');
			} else {
				displayLayer('scb_lyr', 'none');
				displayLayer('scb_del', 'none');
				displayLayer('search_button', 'block');
				displayLayer('list_recent', 'block');
				displayLayer('header_recent_wrapper', 'block');
				displayLayer('header_extended_input_wrapper', 'block');
			}
		}

		if (loopSendKeyword == false && searchInput.className.indexOf("focus") >= 0) {
			sendKeywordOnceIntervalId = setInterval(sendKeywordOnce, 200);
			loopSendKeyword = true;
		} else if (searchInput.className.indexOf("focus") < 0) {
			clearInterval(sendKeywordOnceIntervalId);
			loopSendKeyword = false;
		}
	}