

    var tempDecSearchKeyword = ""; // 비교를 위해 사용
    var timer;	// 시간 함수
    var checkFirst = false; // 검색 박스에 처음 들어왔는지


    //자동 완성 기능 ajax  호출 ,
    function autoComplete() {
        //document.getElementById('test1').innerHTML = document.getElementById('test1').innerHTML +timer+"=";
    	//0.3초마다 호출 하나 입력 값에 따라 예외처리
    	if(document.searchForm.decSearchKeyword.value ==""){
    		showAutoComplete(null);
    		document.getElementById("clrt").style.display = "none";
    	}else{
	        if ( document.searchForm.decSearchKeyword.value == document.searchForm.searchKeyword.value && tempDecSearchKeyword !=  document.searchForm.searchKeyword.value ) {

	            	tabToggle(3, 'stab', 0, 'stabc'); //자동 완성 탭을 활성화 한다
	                try {
	                    var param = "";
	                    var url = JAVASPT_SK_CONTEXT_NM+"/common/getAutoCompleteAjax.tmall";
	                    param = "getAutoCompleteKey="+document.searchForm.decSearchKeyword.value;
	                    tempDecSearchKeyword =document.searchForm.decSearchKeyword.value;
	                    fn_getValueByAjax(url,
	                         "POST",
	                         param,
	                         true,
	                         function(jsonData){
	                             var data = eval(jsonData);
	                             showAutoComplete(myJSONObject , myJSONObject2);//}, myJSONObject2, myJSONObject3, myJSONObject4);
	                             if(document.getElementById('decSearchKeyword').value.length > 0){
	                                document.getElementById("clrt").style.display = "";
	                             }
	                         },
	                         "text");
	                    return;

	                } catch ( err ) {
	                    alert(err);
	                }

	        } else {
	            document.searchForm.searchKeyword.value = document.searchForm.decSearchKeyword.value;
	        }
    	}
        if ( window.navigator.appName.indexOf("Explorer") < 0 ){
            clearTimeout(timer);
            clearInterval(timer);
            timer = this.setTimeout('autoComplete();', 250);
        }

    }
    // 자동완성 검색어 UI 만듬
    function showAutoComplete(obj1 ,obj2){//, obj2, obj3, obj4) {
        var divObject    = document.getElementById("autoComplete");
        var htmlStr        = "";
        var htmlKeywordStr = "";
        var keyword = document.searchForm.decSearchKeyword.value;

        var display = "";

        var obj1ListCount = 0;
        if(obj1 == null){
            // 앞 검색 키원드
            for ( var i = 0; i <5 ; i++ ) {
                 htmlStr += "<li> </li>";
            }
        }else{
        	obj1ListCount =obj1.LIST.length;
            // 앞 검색 키원드
            for ( var i = 0; i <5 ; i++ ) {
                if(i <  obj1ListCount){
                    var htmlKeyword = obj1.LIST[i].KEYWORD;
                    if (  obj1.LIST[i].KEYWORD.indexOf(keyword) >= 0 ) {
                        htmlKeyword = obj1.LIST[i].KEYWORD.replace(keyword, "<strong>" + keyword + "</strong>");//prefix + "<strong>" + keyword +"</strong>" +  surfix;
                    }
                    htmlStr += "<li><a href=\"javascript:autoCompleteSearch('"+obj2.LIST[i].TAG+"');\">"+htmlKeyword+"</a></li>";
                }else{
                    htmlStr += "<li> </li>";
                }
            }
        }
        display = "block";
        divObject.innerHTML = htmlStr;
        disLayerHeader( 'hy_new' ,'none');
        disLayerHeader( 'kwd_new' ,display);

        //0.3 초마다 호출 한다
        if ( window.navigator.appName.indexOf("Explorer") < 0 ){
            clearTimeout(timer);
            clearInterval(timer);
            checkFirst = false;
             timer = this.setTimeout('autoComplete();', 250);
        }
    }
    // 자동완성 검색어를 누룰 경우 action (상품상세로 간다)
    function autoCompleteSearch(tag) {
    	location.href=JAVASPT_SK_HTTP_CONTEXT_URL+"/product/productDetail.tmall?gdsNo="+tag+"&dispCtgNo=";
    }
    // 앤터 누루면 검색 리스트로 감
    function autoSearch(keyword) {
        document.searchForm.decSearchKeyword.value = keyword;
        doSubmit();
    }
    // onFocus 시 처음 focus 외 자동 완성 0.3초 마다 호출 (input 박스 들어올때)
    function getInput() {
        if (checkFirst == false) {
            timer = this.setTimeout("autoComplete()", 250);
        }
        checkFirst = true;
    }
    // 	onclick 실행    최근 검색어
    function fncStop()
    {
	     var ele = document.getElementById('decSearchKeyword');
	     ele.value="";

	     if (ele.value==""){
	    	 autoComplete_new();	// 최근 검색어 호출
	    	 tabToggle(3, 'stab', 1, 'stabc'); // 최근 검색어 UI 보여줌
	    	 autoBest(); // 인기 검색어 호출
	     }
	     ele.focus();
    }
    // onBlur 시간을 clear 한다 (input 박스 나갈때)
    function releaseInput() {
        if ( timer ) clearTimeout(timer);
    }
    // onclick 초기화
    function clearInputValue(elem){
         var ele = document.getElementById(elem);
         ele.value="";
         ele.focus();
    }

    //인기 검색어
    function autoBest() {
    	try {
			var param = "";
			var url = JAVASPT_SK_CONTEXT_NM +"/common/getBestSearchAjax.tmall";

            fn_getValueByAjax(url,
	            "POST",
	            param,
	            true,
	            function(jsonData){
					var data = eval('(' + jsonData + ')');
					showAutoBest(data.popularKeywordList);
	            },
            "text");
            return;
        } catch ( err ) {
              alert(err);
        }
    }
    // 인기 검색어 UI 만듬
    function showAutoBest(keywordList){
        var autoBestStart    = document.getElementById("autoBestStart");
        var autoBestEnd    = document.getElementById("autoBestEnd");
        var autoBestStarthtmlStr        = "";
        var autoBestEndhtmlStr = "";
        var display = "";
		var keywordListArray = keywordList.split(',');
        var obj1ListCount = keywordListArray.length;
            // 앞 인기검색어
            for ( var i = 0; i <5 ; i++ ) {
            	if(obj1ListCount > i){
            		autoBestStarthtmlStr +="<li><a href=\"javascript:autoSearch('"+keywordListArray[i]+"');\"><em>"+(i+1)+"</em>"+keywordListArray[i]+"</a></li>";
                }else{
                	autoBestStarthtmlStr += "<li> </li>";
                }
            }
            // 뒤 인기 검색어
            for ( var i = 5; i <10 ; i++ ) {
            	if(obj1ListCount > i){
            		autoBestEndhtmlStr +="<li><a href=\"javascript:autoSearch('"+keywordListArray[i]+"');\"><em>"+(i+1)+"</em>"+keywordListArray[i]+"</a></li>";
                }else{
                	autoBestEndhtmlStr += "<li> </li>";
                }
            }

        display = "block";
        autoBestStart.innerHTML = autoBestStarthtmlStr;
        autoBestEnd.innerHTML = autoBestEndhtmlStr;
    }
	String.prototype.hreplaceAll = function(str1, str2) {
	     var temp_str = "";
	     if (this.trim() != "" && str1 != str2) {
	         temp_str = this.trim();
	         while (temp_str.indexOf(str1) > -1){
	             temp_str = temp_str.replace(str1, str2);
	         }
	     }
	     return temp_str;
	 }
    /**
    * 최근 검색 어 리스트
    */

    function autoComplete_new()
    {
        var strSearchName = "";
        var strSearchNameArray = "";
        var index = 0;
        var endstr = 0;
        var strAutoComplete = "";
        var strAutoCompleteKeyWord = "";
        var divObject    = document.getElementById("autoComplete_new");
        //var keyDivObject = document.getElementById("autoCompleteKeyWord");
        disLayerHeader( 'kwd_new' ,'block');
//        index = document.cookie.indexOf("SEARCH_NAME" + "=");
//        if (index == -1){
//            disLayerHeader( 'hy_new' ,'none');
//            return null;
//        }
//
//        index = document.cookie.indexOf("=", index) + 1;
//        endstr = document.cookie.indexOf(";", index);
//        if (endstr == -1) endstr = document.cookie.length;
//        strSearchName = document.cookie.substring(index, endstr);
        strSearchName = BKCookieUtil.getCookie("BOOK_SEARCH_NAME");
            strSearchName =decodeURIComponent(strSearchName);
            strSearchNameArray = strSearchName.split('_');
            var searchNameArrayCount =  strSearchNameArray.length-1;

            for ( var i = 0;   i < 5 ; i++ ) {
            	if(i <= searchNameArrayCount){
            		strAutoComplete += "<li><a href='javascript:autoSearch(\""+ strSearchNameArray[i].split("#")[0].hreplaceAll("∏", " ") +"\");' class=\"ico_x\">"+strSearchNameArray[i].split("#")[0].hreplaceAll("∏", " ")+"</a></li>";
            	}else{
            		strAutoComplete += "<li><a href='' class=\"ico_x\"> </a></li>";
            	}
            }
            divObject.innerHTML = strAutoComplete;
//        if ( window.navigator.appName.indexOf("Explorer") < 0 ){
//            clearTimeout(timer);
//            clearInterval(timer);
//            checkFirst = false;
//             timer = this.setTimeout('autoComplete();', 250);
//        }
    }

    /**
    * 최근 검색 어 삭제
    */

    function fncSearchDelete()
    {
    	BKCookieUtil.removeCookie("BOOK_SEARCH_NAME");
        autoComplete_new();
    }


    // 공통 헤더 레이어 열린 창 닫기고 열기
    function disLayerHeader( id, disp) {

        // 배열 선언
        var objName = new Array();
        objName[0] = "kwd_new"; //검색
        objName[1] = "gmn"; //카테고리
        objName[2] = 'categoy';  //기획전
        for ( var i = 0; i< objName.length  ; i++ ) {

            //모두 닫는다
            if ( eval("document.all."+objName[i]) != undefined  ){
                if( objName[i] == 'm_plan_all' &&  id == 'm_plan_bt'){
                    var stat = $("#"+disp).css('display');

                    if (stat != 'none'){
                        $("#"+disp).slideDown("fast");
                        $("#"+id)[0].className =  $("#"+id)[0].className.replace("on", "off");
                    }else{
                        $("#"+disp).slideDown("fast");
                        $("#"+id)[0].className =  $("#"+id)[0].className.replace("off", "on");
                    }
                }
                eval("document.all."+objName[i] ).style.display   = "none";

            }
        }
        // 연다
        if ( eval("document.all."+id ) != undefined &&  id != 'm_plan_bt'){
            eval("document.all."+id ).style.display   = disp;
        }
    }
    //로그인 여부 체크
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


