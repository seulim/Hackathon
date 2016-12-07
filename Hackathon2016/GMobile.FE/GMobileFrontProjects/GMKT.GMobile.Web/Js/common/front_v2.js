// SHOW/HIDDEN 
function displayLayer(id, disp) {
	var obj = document.getElementById(id);
	obj.style.display = disp;
}

function displayLayer2(targetId, tabId, disp) {
	var target = $("#" + targetId);
	var tab = $("#" + tabId);
	var tabCnm = tab.attr("class");

	if(tabCnm.indexOf("on") >= 0){
		target.css("display", "none");
		tab.removeClass("on");
	}else{
		target.css("display", disp);
		tab.addClass("on");
	}
}


// TAB TOGGLE
function tabView(tabSize, tabId, idx, targetId) {
	for ( var i = 0; i < tabSize; i++ ) {
		if ( i == idx ) {
			$("#"+tabId+i)[0].className = $("#"+tabId+i)[0].className.replace("off", "on");
			if ( $("#"+targetId+i) ) { $("#"+targetId+i).show(); }
		}else{
			$("#"+tabId+i)[0].className = $("#"+tabId+i)[0].className.replace("on", "off");
			if ( $("#"+targetId+i) ) {$("#"+targetId+i).hide();	}
		}
	}
}

function tabView2(tabSize, tabId, idx, targetId) {
	for ( var i = 0; i < tabSize; i++ ) {
		if ( i == idx ) {
			$("#"+tabId+i)[0].className = $("#"+tabId+i)[0].className.replace("off", "on");
			if ( $("#"+targetId+i) ) { $("#"+targetId+i).css("height", "100%"); }
		}else{
			$("#"+tabId+i)[0].className = $("#"+tabId+i)[0].className.replace("on", "off");
			if ( $("#"+targetId+i) ) {$("#"+targetId+i).css("height", "0")	}
		}
	}
}


// HEADER SEARCH BAR 
// ipId : 입력창, delId : 삭제버튼, lyrId : 자동완성레이어, displayYN : Y=보여줌, N=감춤)
function hdScbKeyEvent(ipId, delId, lyrId, displayYN){
	var ipId = $("#" + ipId);
	var delId = $("#" + delId);
	var lyrId = $("#" + lyrId);

	if(0 < ipId.val().length){ // 입력창에 글자를 입력한 경우
		delId.css('display', "block");

		// '닫기'버튼을 선택시 자동완성 레이어만 감춤
		if("N" == displayYN){ 
			lyrId.css('display', "none");
		}else if("Y" == displayYN){
			lyrId.css('display', "block");
		}//end else if
	}else if(0 == ipId.val().length){ // 입력창에 글자가 없을 경우

		// 삭제버튼, 자동완성레이어 감춤
		delId.css('display', "none"); 
		lyrId.css('display', "none");
	}//end else if
}//end function


// HEADER CATEGORY
var nCtgyNum = -1;
var nSubNum = -1;
function showCategory(subNum, ctgyNum){

	var ctgrGrpLength = $("#hd .ctgr").length; //Category Count	
	var ctgrSubLength = $("#hd .ctgr_sub").length;	//CategorySub Count

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


// ROLLING(FOOTER NOTICE)
function rolling(rollGrp){
	setInterval(function(){
		var rollList = $("#" + rollGrp + " li");
		var nRollList = $(rollList[0]).clone();

		$(rollList[0]).remove();	
		$("#" + rollGrp).append(nRollList);
	}, 3000);
}


// FLICKING MAIN
function flicking(bnGrp, prev, next){
	$(window).bind("load", function(){
		imgPos();
	});
	var userAgent = navigator.userAgent.toLowerCase();

	if(userAgent.search("iphone|ipod|ipad") > -1){
		$(window).bind("orientationchange", function(){
			imgPos();
		});
	}else{
		$(window).bind("resize", function(){
			imgPos();
		});
	}

	function imgPos(){ // RESIZE, LOAD 이미지의 넓이와 양쪽 화살표의 위치를 계산한다.
		var imgHeight = $("#" + bnGrp + " span img").css('height'); // 계산되어진 이미지의 높이값을 가져온다.
		var imgHeightNum = parseInt(imgHeight);	
		var btnHeightHalfSize = parseInt($("#" + prev).css('height'))/2; // 좌우 플리킹 버튼의 높이의 반.

		$("#" + bnGrp).css('height', imgHeight); // 가져온 높이값을 이미지를 감싸고 있는 곳(div id="bnWrap")에 넣어준다.
		
		$("#" + prev).css('height', imgHeight);
		$("#" + next).css('height', imgHeight);
		$("#" + prev).css('top', -imgHeightNum + "px"); // 좌우 플리킹 버튼의 TOP위치, 이미지의 높이의 중간에서 버튼높이사이즈의 반을 빼준값을 넣어준다.
		$("#" + next).css('top', -imgHeightNum + "px"); // 좌우 플리킹 버튼의 TOP위치, 이미지의 높이의 중간에서 버튼높이사이즈의 반을 빼준값을 넣어준다.
	}

	$("#" + bnGrp + " > ul").not(".bn_sub4").remove(); // 기존 데이터 삭제, MAIN(반응형웹을 위한 소스부분빼고 모두 제거(메인, 서브))
	$("#" + bnGrp + " > ol").remove(); // 기존 데이터 삭제 
	
}

// FLICKING SUB
function flicking2(bnGrp){
	$("#" + bnGrp + " > ul").remove(); // 기존 데이터 삭제
	$("#" + bnGrp + " > ol").remove(); // 기존 데이터 삭제 
}

// 반응형웹 
function responseWeb(){
	var windowWidth = $(window).width(); //기기 가로사이즈
	var bnSub4 = $(".bn_sub4"); //기존 모바일 기획전 소스
	var bnGrp4Div = $("#bnGrp4 > div"); //플리킹 모바일 기획전 소스

	if(800 < windowWidth){ //기준픽셀보다 기기의 가로사이즈가 크면
		$("#bnGrp4 > div").remove(); //플리킹 모바일 기획전 소스 제거
		$("#bnGrp4").append(bnSub4); //기존 모바일 기획전 소스 추가			
	}else{ //기준픽셀보다 기기의 가로사이즈가 작으면
		$(".bn_sub4").remove(); //기존 모바일 기획전 소스 제거
		$("#bnGrp4").append(bnGrp4Div); //플리킹 모바일 기획전 소스 추가
	}

	$(window).bind("resize",function(){
	//리사이즈될때도 위와 같은 방식으로 한다.
		var windowWidth = $(window).width(); //기기 가로사이즈

		if(800 < windowWidth){
			$("#bnGrp4 > div").remove();
			$("#bnGrp4").append(bnSub4);
		}else{
			$(".bn_sub4").remove();
			$("#bnGrp4").append(bnGrp4Div);
		}
	});
}


// 기존 스크립트 기획전_상품가로정렬 
function UL_layoutFix(el, isNewLine, addMargine){
	var obj = $(el).find("li");
	var win_w,obj_w,n,x, nmg;
	var tempWidth = 0;
	function init(){
		obj = $(el).find("li");

		win_w = $(window).width();
		
		//if (tempWidth == win_w) return;
		tempWidth = win_w;

		obj_w = $(obj).outerWidth();
		n = Math.floor(win_w / obj_w); //라인당 li 갯수

		//개행되는 목록과 개행되지 않는 목록을 분기
		if (isNewLine){
			x = win_w % obj_w; //남는 영역
			x = (x/n)/2;
			obj.css({marginLeft:x,marginRight:x});

//			alert(obj_w + " / " + n + " / " + x);
		}
		else {
			win_w = win_w - 30; //화면너비 - 좌우기본마진(15px/15px)

			//기본 마진 외에 추가로 공간이 발생하는 경우 보정해 주어야 한다.
			if (addMargine != undefined) {
				win_w = win_w - addMargine;
			}

			n = Math.floor(win_w / obj_w); //라인당 li 갯수
			nmg = win_w - (obj_w * n);
			x = (n > 1) ? (nmg /(n - 1)) : nmg;
			obj.css({marginLeft:0,marginRight:x});
		}
	}

	if (typeof orientationchange != "undefined"){
		$(window).bind("orientationchange", function(){
			init();
		});
	}
	else{
		$(window).bind("resize",function(){
			init();
		});
	}
	init();
}


// 이미지 Rolling
function ImageRotation() {
    var scroll = {time:1, start:0, change:0, duration:25, timer:null};
    var originaltime = scroll.time;
    var objWidth = 0;
    var currentNum = 0;
    var tmpName, tmpWrap, tmpListId, tmpNext, tmpPrev, tmpNum, tmpTime, moveEvent, restNum, objNum, nowNum, totNum, cntTmp, nowTmp, totTmp, cntNum, tmpTit, tmpDir, tmpPage, cntRoll, tmpMg;
    this.GoodsSetTime = null;
    var cloneElement = [];

    this.setScrollType = function (obj) {}


	this.initialize = function () {
        tmpNum = this.listNum;
        tmpTime = this.GoodsSetTime;
        tmpDir = this.scrollDirection;
        tmpMg = this.mg;
        tmpWrap = document.getElementById(this.wrapId);
        tmpListId = document.getElementById(this.listId);
        tmpNext = document.getElementById(this.btnNext);
        tmpPrev = document.getElementById(this.btnPrev);
        nowNum = document.getElementById(this.nowCnt);
        totNum = document.getElementById(this.totCnt);
        //tmpTit = document.getElementById(this.titName);
        tmpPage = document.getElementById(this.cntPage);
        cntRoll = this.sRoll;


        objNum = tmpListId.getElementsByTagName('li').length;
        cntNum = tmpNum;

		liWidth = tmpListId.getElementsByTagName('li')[0].offsetWidth + tmpMg; // + parseInt(li.style.marginLeft) + parseInt(li.style.marginRight);

	//	if (liWidth == 0){liWidth = tmpWdth;}
		
        tmpListId.style.width = (objNum * liWidth) + 'px';

		objWidth = liWidth * tmpNum;
		
        tmpListId.style.overflow = 'hidden';
        tmpWrap.style.overflow = 'hidden';

        tmpNext.onclick = setPrev;
        tmpPrev.onclick = setNext;

        if (this.autoScroll == 'none') {
                // do nothing.
        } else {
            clearInterval(tmpTime);
            /* 0525
            if(tmpDir == 'direction'){moveEvent = 'setPrev';}
            else{moveEvent = 'setNext';}
            tmpTime = setInterval(function () { eval(moveEvent + '();'); }, this.scrollGap);
			*/
			this.startTimer();
        }

        // count
        if(totNum){totNum.innerHTML= objNum/tmpNum;}

        //img name
        //if(tmpTit){tmpTit.innerHTML = tmpListId.getElementsByTagName("img")[0].alt;}

        // PAGES
        if(tmpPage){setPages(1);}

    }

    var setPages = function (pNum) {
        var pBtn;

        totNum = parseInt(objNum/tmpNum);


        if(totNum){
            for (var i=1; i<totNum+1; i++){
                if(pBtn == undefined){pBtn = "";}
                if(i == pNum){pBtn += "<strong  id='"+cntRoll+"_sBtn"+i+"' class='on'></strong>";}
                else{pBtn += "<strong id='"+cntRoll+"_sBtn"+i+"' class='off'></strong>";}
            }
            tmpPage.innerHTML = pBtn;
        }
        nowNum = pNum;
    }


    var setNext = function () {
        if (objNum <= tmpNum) return false;
        // count
        cntNum = cntNum - tmpNum;
        if(cntNum < 1){cntNum = objNum;}
        if(nowNum){nowNum.innerHTML = parseInt(cntNum/tmpNum);}

        // PAGES
        if(tmpPage){setPages(parseInt(cntNum/tmpNum));}

        //moveEvent = 'setNext';
        for (var i=0; i<tmpNum; i++) {
            var objLastNode = tmpListId.removeChild(tmpListId.getElementsByTagName('li')[objNum - 1]);
            tmpListId.insertBefore(objLastNode, tmpListId.getElementsByTagName('li')[0]);

            //img name
            //if(tmpTit){tmpTit.innerHTML = tmpListId.getElementsByTagName("img")[tmpNum-1].alt;}
        }

        tmpWrap.scrollLeft = objWidth;
        var position = getActionPoint('indirect');
        startScroll(position.start, position.end, 'next');
        return false;
    }

    var setPrev = function () {
        if (objNum <= tmpNum) return false;
        // count
        cntNum = cntNum + tmpNum;
        if(objNum < cntNum){cntNum = tmpNum;}
        if(nowNum){nowNum.innerHTML = parseInt(cntNum/tmpNum);}

        // PAGES
        if(tmpPage){setPages(parseInt(cntNum/tmpNum));}

        //moveEvent = 'setPrev';
        var position = getActionPoint('direct');
        startScroll(position.start, position.end, 'prev');
        return false;
    }

    var startScroll = function (start, end, location) {
        if (scroll.timer != null) {
            clearInterval(scroll.timer);
            scroll.timer = null;
        }

        scroll.start = start;
        scroll.change = end - start;
        scroll.timer = setInterval(function () {
            scrollHorizontal(location);
        }, 15);
    }

    var scrollHorizontal = function (location) {
        if (scroll.time > scroll.duration) {
            clearInterval(scroll.timer);
            scroll.time = originaltime;
            scroll.timer = null;
            if (location == 'prev') {
                for (var i=0; i<tmpNum; i++) {
                    var objFirstNode = tmpListId.removeChild(tmpListId.getElementsByTagName('li')[0]);
                    tmpListId.appendChild(objFirstNode);
                    //img name
                    //if(tmpTit){tmpTit.innerHTML = tmpListId.getElementsByTagName("img")[tmpNum-1].alt;}
                }

            }
            tmpWrap.scrollLeft = 0;
        } else {
            tmpWrap.scrollLeft = sineInOut(scroll.time, scroll.start, scroll.change, scroll.duration);
            scroll.time++;
        }
    }

    var getActionPoint = function (dir) {
        var end;

        if (dir == 'direct') end = tmpWrap.scrollLeft + objWidth;
        else end = tmpWrap.scrollLeft - objWidth;

        var start = tmpWrap.scrollLeft;

        var position = {start:0, end:0};
        position.start = start;
        position.end = end;

        return position;
    }

    var sineInOut = function (t, b, c, d) { return -c/2 * (Math.cos(Math.PI*t/d) - 1) + b; }

    var findElementPos = function (elemFind) {
        var elemX = 0;
        elemX = tmpWidth*(elemFind/tmpNum);
        return elemX;
 }

    /*0525*/
	this.stopTimer=function(){
		if(tmpTime!=0)
		{
			clearInterval(tmpTime);
			tmpTime=0;
		}
	}

	this.startTimer=function(){
			if(tmpTime==0 || tmpTime==null)
			{
       		     tmpTime = setInterval(function () { setPrev() }, this.scrollGap);
			}
      }

}