// 레이어 Show/Hidden 처리
function displayLayer(id, disp, inn) {
    var obj = document.getElementById(id);
    obj.style.display = disp;

    if (inn) {
        var obj = document.getElementById(id);
        var temp_value = obj.innerHTML;
        obj.innerHTML = temp_value;
    }
}

function visibilityLayer(id, disp) {
    var obj = document.getElementById(id);
    obj.style.visibility = disp;
}

function switchLayer(hiddenId, showId) {
    displayLayer(hiddenId, "none");
    displayLayer(showId, "block");
}

function switchLayer2(hiddenId, showId) {
    displayLayer(hiddenId, "none");
    displayLayer(showId, "");
}

function simpleToggleLayer(id) {
    var obj = document.getElementById(id);
    if (obj == undefined) return;
    obj.style.display = (obj.style.display == "block") ? "none" : "block";
}

//----------------------------------------
// TOGGLE ID CLASS
//----------------------------------------
function fn_combo(tabId, targetId) {
    var stat = $("#" + targetId).css('display');

    if (stat == 'none') {
        $("#" + targetId).slideDown("fast");
        $("#" + tabId)[0].className = $("#" + tabId)[0].className.replace("on", "off");
    } else {
        $("#" + targetId).slideUp("fast");
        $("#" + tabId)[0].className = $("#" + tabId)[0].className.replace("off", "on");
    }
}

/**
* 탭 버튼 + 컨텐츠 스위칭 구조
* tabSize(*) : 탭 버튼 갯수
* tabId(*)   : 탭 아이디
* idx(*)     : 탭 순번(0부터 시작)
* ctsId      : 선택된 탭의 컨텐츠1
* cts2Id     : 선택된 탭의 컨텐츠2
*/
function tab_combo(tabSize, tabId, idx, ctsId, cts2Id) {
    var objTab, objCts, objCts2;
    var className;

    for (var i = 0; i < tabSize; i++) {
        objTab = document.getElementById(tabId + i);
        if (i == idx) { objTab.className = objTab.className.replace('off', 'on'); }
        else { objTab.className = objTab.className.replace('on', 'off'); }
    }

    if (ctsId != 'undefined') {
        for (var i = 0; i < tabSize; i++) {
            objCts = document.getElementById(ctsId + i);
            if (objCts == null) continue;
            if (i == idx) { objCts.style.display = 'block'; }
            else { objCts.style.display = 'none'; }
        }
    }

    if (cts2Id != 'undefined') {
        for (var i = 0; i < tabSize; i++) {
            objCts2 = document.getElementById(cts2Id + i);
            if (objCts2 == null) continue;
            if (i == idx) { objCts2.style.display = 'block'; }
            else { objCts2.style.display = 'none'; }
        }
    }
}

/**
* ON/OFF Toggle Button
*/
function toggle_button(tBtnId) {
    var objBtn = document.getElementById(tBtnId);

    if (objBtn.className == "on") {
        objBtn.className = objBtn.className.replace('on', 'off');
        setValueByElementId(tBtnId + '_val', 'N'); //[2012.07.12] '0' -> 'N' 으로 수정
    }
    else if (objBtn.className == "off") {
        objBtn.className = objBtn.className.replace('off', 'on');
        setValueByElementId(tBtnId + '_val', 'Y'); //[2012.07.12] '1' -> 'Y' 으로 수정
    }
    else {
        objBtn.className = objBtn.addClass("off");
        setValueByElementId(tBtnId + '_val', 'N'); //[2012.07.12] '0' -> 'N' 으로 수정
    }
}
function setValueByElementId(id, val) {
    var obj = document.getElementById(id);
    if (obj != undefined) {
        obj.value = val;
    }
}

//----------------------------------------
// TOGGLE
//----------------------------------------
function toggle(id) {
    var element = document.getElementById(id);
    element.style.display = (element.style.display == 'block') ? "none" : "block";
}

function setPlus(sid) {
    var value = $("#" + sid).val();
    value++;
    if (value > 100) { return false; }
    $("#" + sid).val(value);
}

function setMinus(sid) {
    var value = $("#" + sid).val();
    value--;
    if (value < 1) { return false; }
    $("#" + sid).val(value);
}

//----------------------------------------
// 이미지 Rolling
//----------------------------------------
function ImageRotation() {
    var scroll = { time: 1, start: 0, change: 0, duration: 25, timer: null };
    var originaltime = scroll.time;
    var objWidth = 0;
    var currentNum = 0;
    var tmpName, tmpWrap, tmpListId, tmpNext, tmpPrev, tmpNum, tmpTime, moveEvent, restNum, objNum, nowNum, totNum, cntTmp, nowTmp, totTmp, cntNum, tmpTit, tmpDir, tmpPage, cntRoll, tmpMg;
    this.GoodsSetTime = null;
    var cloneElement = [];

    this.setScrollType = function (obj) { }


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
        if (objNum > 0) {
            liWidth = tmpListId.getElementsByTagName('li')[0].offsetWidth + tmpMg; // + parseInt(li.style.marginLeft) + parseInt(li.style.marginRight);
        }

        //	if (liWidth == 0){liWidth = tmpWdth;}

        tmpListId.style.width = (objNum * liWidth) + 'px';

        objWidth = liWidth * tmpNum;

        tmpListId.style.overflow = 'hidden';
        tmpWrap.style.overflow = 'hidden';

        tmpNext.onclick = setPrev;
        tmpPrev.onclick = setNext;

        if (this.autoScroll == 'none') {
            // do nothing.
        }
        else {
            clearInterval(tmpTime);
            /* 0525
            if(tmpDir == 'direction'){moveEvent = 'setPrev';}
            else{moveEvent = 'setNext';}
            tmpTime = setInterval(function () { eval(moveEvent + '();'); }, this.scrollGap);
            */
            this.startTimer();
        }

        // count
        if (totNum) { totNum.innerHTML = objNum / tmpNum; }

        //img name
        //if(tmpTit){tmpTit.innerHTML = tmpListId.getElementsByTagName("img")[0].alt;}

        // PAGES
        if (tmpPage) { setPages(1); }

    }

    var setPages = function (pNum) {
        var pBtn;

        totNum = parseInt(objNum / tmpNum);


        if (totNum) {
            for (var i = 1; i < totNum + 1; i++) {
                if (pBtn == undefined) { pBtn = ""; }
                if (i == pNum) { pBtn += "<strong  id='" + cntRoll + "_sBtn" + i + "' class='on'></strong>"; }
                else { pBtn += "<strong id='" + cntRoll + "_sBtn" + i + "' class='off'></strong>"; }
            }
            tmpPage.innerHTML = pBtn;
        }
        nowNum = pNum;
    }


    var setNext = function () {
        if (objNum <= tmpNum) return false;
        // count
        cntNum = cntNum - tmpNum;
        if (cntNum < 1) { cntNum = objNum; }
        if (nowNum) { nowNum.innerHTML = parseInt(cntNum / tmpNum); }

        // PAGES
        if (tmpPage) { setPages(parseInt(cntNum / tmpNum)); }

        //moveEvent = 'setNext';
        for (var i = 0; i < tmpNum; i++) {
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
        if (objNum < cntNum) { cntNum = tmpNum; }
        if (nowNum) { nowNum.innerHTML = parseInt(cntNum / tmpNum); }

        // PAGES
        if (tmpPage) { setPages(parseInt(cntNum / tmpNum)); }

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
                for (var i = 0; i < tmpNum; i++) {
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

        var position = { start: 0, end: 0 };
        position.start = start;
        position.end = end;

        return position;
    }

    var sineInOut = function (t, b, c, d) { return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b; }

    var findElementPos = function (elemFind) {
        var elemX = 0;
        elemX = tmpWidth * (elemFind / tmpNum);
        return elemX;
    }

    /*0525*/
    this.stopTimer = function () {
        if (tmpTime != 0) {
            clearInterval(tmpTime);
            tmpTime = 0;
        }
    }

    this.startTimer = function () {
        if (tmpTime == 0 || tmpTime == null) {
            tmpTime = setInterval(function () { setPrev() }, this.scrollGap);
        }
    }

}


//----------------------------------------
// BLOCK UI
//----------------------------------------

/* z-index 값의 이하 레이어를 가려주는 딤드 창 처리 */
function showDimmedLayer() {
    $("<div id='mask' selected='true'></div>").appendTo("body");
    $('#mask').css({
        width: "100%",
        height: ($(document).height()),
        position: 'absolute',
        left: 0,
        top: 0,
        background: '#000',
        opacity: 0.5,
        'z-index': 10
    });
}
function closeDimmedLayer() {
    $('#mask').remove();
}

//----------------------------------------
// CHECHBOX
//----------------------------------------
function selectChkBox(chk, id) {
    var sId = document.getElementById(id);
    var obj = sId.getElementsByTagName('input');
    for (i = 0; i < obj.length; i++) {
        obj[i].checked = false;
        chk.checked = true;
    }
}


//wide filp
jQuery.fn.horizentalSlide = function (plus_W, options) {
    var settings = { li: $(this).find("li"), prevBtn: $("#leftBtn a"), nextBtn: $("#rightBtn a") }
    settings = $.extend(settings, options || {});
    $(this).each(function () {
        var ul = $(this);
        var li = settings.li;
        var win_W;
        var li_W = $(li).width() + plus_W; //img width
        var li_length = $(li).length;
        var li_posX;
        var curr = 0;
        var prevBtn = settings.prevBtn;
        var nextBtn = settings.nextBtn;

        function moving() {
            win_W = $(window).width();
            li_posX = parseInt((win_W - li_W) / 2) - (curr * li_W);
            ul.stop().animate({ marginLeft: li_posX });
            li.eq(curr).addClass("current");
        }

        function first() {
            win_W = $(window).width();
            li_posX = parseInt((win_W - li_W) / 2) - (curr * li_W);
            ul.css({ marginLeft: li_posX });
            ul.css({ visibility: "visible" });
            li.eq(curr).addClass("current");
        }

        function removeCurrClass() {
            li.eq(curr).removeClass("current");
        }

        function layoutFixed() {
            ul.width(li_W * li_length, function () { });
        }

        $(prevBtn).click(function () {
            if (curr > 0) {
                removeCurrClass();
                curr--;
                moving();
            }
            return false;
        });

        $(nextBtn).click(function () {
            if (li_length - 1 > curr) {
                removeCurrClass();
                curr++;
                moving();
            }
            return false;
        });

        $(".goods_filp").touchwipe({
            wipeLeft: function () {
                $(nextBtn).click();
            },
            wipeRight: function () {
                $(prevBtn).click();
            },
            min_move_x: 20
        });

        layoutFixed();
        first();

        if (typeof orientationchange != "undefined") {
            $(window).bind("orientationchange", function () {
                moving();
            });
        } else {
            $(window).bind("resize", function () {
                moving();
            });
        }
    });
}

//quick left menu
jQuery.fn.scrollMenu = function (top) {
    var el = $(this);
    var speed = 400;
    var targetY;
    function init() {
        targetY = $(window).scrollTop() + top;
        el.stop().animate({ top: targetY }, speed);
    }
    init();
    $(window).scroll(function () {
        init();
    });
}
jQuery.fn.scrollMenuByPercent = function (topPercent) {
    var el = $(this);
    var speed = 300;
    var targetY;
    var windowHeight;
    function init() {
        windowHeight = window.innerHeight ? window.innerHeight : $(window).height();
        targetY = $(window).scrollTop() + (windowHeight * topPercent / 100);
        el.stop().animate({ top: targetY }, speed);
    }
    init();
    $(window).scroll(function () {
        init();
    });
}

//페이지 로딩 아이콘
jQuery.fn.progressBar = function () {
    var el = $(this);
    var speed = 300;
    var targetY;
    var windowHeight, windowWidth, boxHeight;
    var halfWindowHeight, halfProgressHeight;
    var scrollTop;
    var checkPos;

    function init() {
        if (el.css('display') == 'block') {
            windowHeight = window.innerHeight ? window.innerHeight : $(window).height();
            scrollTop = $(window).scrollTop();

            halfWindowHeight = windowHeight / 2;
            //alert(halfWindowHeight);
            //halfProgressHeight = $("#progress_icon").height() / 2;

            //targetY = scrollTop + (halfWindowHeight - halfProgressHeight) + 100;
            targetY = scrollTop + halfWindowHeight;

            //	el.stop().animate({top:targetY},speed);
        }
    }
    init();
    el.css({ top: targetY });
    //$(window).scroll(function(){
    //	init();
    //});
}

//팝업(아래에서위로)
function LayerToggle(lyrId, a, type) {//레이어 ID

    var stat = $("#" + lyrId).css('display'); 	//레이어 상태
    var scn_ht = $(window).height(); 		//화면 높이

    var lyr_ht = $("#" + lyrId).height(); 		//레이어 높이
    var scrollTop = $(window).scrollTop(); 	//스크롤 위치
    var sumTop = scn_ht + lyr_ht + scrollTop;

    var lyr_pos = scrollTop; 				//레이어 위치 변수

    if (scn_ht > lyr_ht) { lyr_pos = parseInt((scn_ht - lyr_ht) / 2) + scrollTop + 40 } //모바일 주소창(40px)

    if (stat == 'none') {
        /*
        // BLOCK UI
        $("<div id='mask' selected='true'></div>").appendTo("body");
        $('#mask').css({
        width:"100%",
        height:$(document).height(),
        position:'absolute',
        left:0,
        top:0,
        background:'#000',
        opacity:0.5,
        'z-index':40
        });
        */
        if (type == undefined) {
            $("#" + lyrId).css('top', sumTop); 	//최초레이어 위치
            $("#" + lyrId).show().animate({ top: lyr_pos }, 500).css('z-index', 100);
        } else {
            var scn_wd = $(window).width(); 	//화면 넓이
            var defLeft = parseInt($("#" + lyrId).css("left"));

            defLeft = defLeft + 10; //[2012-06-11] 팝업 좌측 여백 처리(너비의 10%)

            $("#" + lyrId).css({ top: lyr_pos, left: (defLeft + scn_wd) + "%" }); 	//최초레이어 위치
            $("#" + lyrId).show()
				        .animate({ left: defLeft + "%" }, 500, null, function () {
				            //[2012-06-12] 쿠폰함의 SELECT 선택이 잘 안되는 현상 처리
				            if (lyrId == "ctCpn") {
				                var objFocus = document.getElementById('selector_coupon2');
				                if (objFocus != undefined) {
				                    objFocus.focus();
				                }
				            }
				        }).css('z-index', 100);
        }

        // BLOCK UI
        $("<div id='mask' selected='true'></div>").appendTo("body");
        resizeMaskLayer(40);
    } else {


        if (type == undefined) {

            // BLOCK UI
            $('#mask').remove();

            $("#" + lyrId).css('top', lyr_pos).animate({ top: sumTop }, 500).fadeOut(100);
        }
        else {

            var scn_wd = $(window).width(); 	//화면 넓이
            var defLeft = parseInt($("#" + lyrId).css("left"));

            defLeft = defLeft - 10; //[2012-06-11] 팝업 좌측 여백 처리(너비의 10%)

            ///*
            $("#" + lyrId)
				.css('top', lyr_pos)
				.animate({ left: (defLeft * -1) + "%" }, 300)
				.fadeOut({
				    dutation: "fast",
				    complete: function () {
				        $(this).css({ left: defLeft + "%" });
				    }
				});
            //*/
            /*
            $("#"+lyrId)
            .css('top',lyr_pos)
            .animate({left: (defLeft - 100) + "%"}, 300)
            .fadeOut({
            dutation : "fast",
            complete : function(){
            $(this).css({left: defLeft + "%"});
            }
            });
            */

            // BLOCK UI
            $('#mask').remove();
        }
    }
}
function resizeMaskLayer(zIndexNum) {
    $('#mask').css({
        width: "100%",
        height: $(document).height(),
        position: 'absolute',
        left: 0,
        top: 0,
        background: '#000',
        opacity: 0.5,
        'z-index': zIndexNum	//[2012.06.25] 카테고리 상세검색 레이어를 덮는 문제가 있어 z-index 값을 40->10으로 변경
    });
}

//팝업2(아래에서위로) : LyrToggle 레이어 위에 중첩레이어를 올림
function LayerToggle2(lyrId) {//레이어 ID

    var stat = $("#" + lyrId).css('display'); 	//레이어 상태
    var scn_ht = $(window).height(); 		//화면 높이

    var lyr_ht = $("#" + lyrId).height(); 		//레이어 높이
    var scrollTop = $(window).scrollTop(); 	//스크롤 위치
    var sumTop = scn_ht + lyr_ht + scrollTop;

    var lyr_pos = scrollTop; 				//레이어 위치 변수

    if (scn_ht > lyr_ht) { lyr_pos = parseInt((scn_ht - lyr_ht) / 2) + scrollTop + 40 } //모바일 주소창(40px)

    if (stat == 'none') {
        // BLOCK UI
        $("<div id='mask2' selected='true'></div>").appendTo("body");
        $('#mask2').css({
            width: "100%",
            height: $(document).height(),
            position: 'absolute',
            left: 0,
            top: 0,
            background: '#000',
            opacity: 0.5,
            'z-index': 140
        });

        $("#" + lyrId).css('top', sumTop); 	//최초레이어 위치
        $("#" + lyrId).show().animate({ top: lyr_pos }, 500).css('z-index', 200);

    } else {
        // BLOCK UI
        $('#mask2').remove();
        $("#" + lyrId).css('top', lyr_pos).animate({ top: sumTop }, 500).fadeOut(100);
    }
}

function LayerToggleAndRefreshScroll(layerId, scrollId) {
    LayerToggle(layerId);
    refreshFixedScroll(scrollId);
}
function LayerToggle2AndRefreshScroll(layerId, scrollId) {
    LayerToggle2(layerId);
    refreshFixedScroll(scrollId);
}

$.fn.switchImg = function (on) {
    $(this).each(function (i) {
        var img = $(this);
        var type = "." + img.attr("src").match(/gif$|jpg$|png$/);
        if (on) {
            if (img.attr("src").indexOf("_on" + type) != -1) {
                return;
            }
            img.attr("src", img.attr("src").replace(type, "_on" + type));
        } else {
            img.attr("src", img.attr("src").replace("_on" + type, type));
        }
    });
}

$.fn.textPlaceholder = function () {

    return this.each(function () {

        var that = this;

        if (that.placeholder && 'placeholder' in document.createElement(that.tagName)) return;

        var placeholder = that.getAttribute('placeholder');
        var input = jQuery(that);

        if (that.value === '' || that.value == placeholder) {
            input.addClass('text-placeholder');
            that.value = placeholder;
        }

        input.focus(function () {
            if (input.hasClass('text-placeholder')) {
                this.value = '';
                input.removeClass('text-placeholder');
            }
        });

        input.blur(function () {
            if (this.value === '') {
                input.addClass('text-placeholder');
                this.value = placeholder;
            } else {
                input.removeClass('text-placeholder');
            }
        });

        that.form && jQuery(that.form).submit(function () {
            if (input.hasClass('text-placeholder')) {
                that.value = '';
            }
        });

    });
};

$.fn.layerSlideToggle = function (options) {
    var opt = {
        group: ".slideToggleBtn",
        btn: ".slideToggleBtn",
        layer: ".slideToggleObj",
        speed: 300,
        imgOn: false,
        fnOpen: function () { },
        fnClose: function () { }
    };
    opt = $.extend(opt, options || {});
    $(this).each(function () {
        var self = $(this);
        var btn = self.find(opt.btn);
        var obj = self.find(opt.layer);
        var speed = opt.speed;
        var group = $(opt.group);
        var imgOn = opt.imgOn;
        var fnOpen = opt.fnOpen;
        var fnClose = opt.fnClose;
        btn.on("click", function () {
            var btn = $(this);
            obj.each(function () {
                var self = $(this);
                if (self.is(":hidden")) {
                    fnOpen(op = { el: btn });
                    group.filter(".unfold").click();
                    self.slideDown(speed, function () {
                        var objMask = document.getElementById("mask");
                        resizeMaskLayer((objMask == undefined) ? 40 : objMask.style.zIndex);
                    });
                    btn.addClass("unfold");
                    if (imgOn) {
                        btn.find("img").switchImg(1);
                    }

                    //Category 상세검색 버튼 액션 처리(Dimmed Layer)
                    if (btn.attr("id") == 'a_srp') {
                        showDimmedLayer();
                    }

                } else {
                    fnClose(op = { el: btn });
                    self.slideUp(speed);
                    btn.removeClass("unfold");

                    if (imgOn) {
                        btn.find("img").switchImg(0);
                    }

                    //Category 상세검색 버튼 액션 처리(Dimmed Layer)
                    if (btn.attr("id") == 'a_srp') {
                        closeDimmedLayer();
                    }

                }
            });
            return false;
        });
    });
}

$.fn.layerSlideToggle2 = function (options) {
    var opt = {
        group: ".slideToggleBtn",
        btn: ".slideToggleBtn",
        layer: ".slideToggleObj",
        speed: 300,
        imgOn: false,
        fnOpen: function () { },
        fnClose: function () { }
    };
    opt = $.extend(opt, options || {});
    $(this).each(function () {
        var self = $(this);
        var btn = self.find(opt.btn);
        var obj = self.find(opt.layer);
        var speed = opt.speed;
        var group = $(opt.group);
        var imgOn = opt.imgOn;
        var fnOpen = opt.fnOpen;
        var fnClose = opt.fnClose;
        btn.on("click", function () {
            var btn = $("#a_srp");
            obj.each(function () {
                var self = $(this);
                if (self.is(":hidden")) {
                    fnOpen(op = { el: btn });
                    group.filter(".unfold").click();
                    self.slideDown(speed, function () {
                        var objMask = document.getElementById("mask");
                        resizeMaskLayer((objMask == undefined) ? 40 : objMask.style.zIndex);
                    });
                    btn.addClass("unfold");
                    if (imgOn) {
                        btn.find("img").switchImg(1);
                    }

                    //Category 상세검색 버튼 액션 처리(Dimmed Layer)
                    if (btn.attr("id") == 'a_srp') {
                        showDimmedLayer();
                    }

                } else {
                    fnClose(op = { el: btn });
                    self.slideUp(speed);
                    btn.removeClass("unfold");

                    if (imgOn) {
                        btn.find("img").switchImg(0);
                    }

                    //Category 상세검색 버튼 액션 처리(Dimmed Layer)
                    closeDimmedLayer();

                }
            });
            return false;
        });
    });
}

/* [쇼핑기획전] 버튼 이벤트 */
function addBestAllBtnToggleListener() {
    var btnBestAll = $("#btn_all a");
    var cateBtn = $("#gmenu a");

    function init_buttons() {

        btnBestAll.attr("class", "off");

        cateBtn.each(function () {
            var btn2 = $(this);
            if (btn2.attr("class") == "on") {
                btn2.removeClass("on");

                var btnImg2 = btn2.find("img");
                btnImg2.attr("src", btnImg2.attr("src").replace("_on.png", ".png"));
            }
        });

    }

    btnBestAll.click(function () {
        init_buttons();
        btnBestAll.attr("class", "on");
        //return false;
    });

    cateBtn.each(function () {
        var btn = $(this);
        btn.click(function () {
            init_buttons();
            var btnImg = btn.find("img");
            btnImg.attr("src", btnImg.attr("src").replace(".png", "_on.png"));
            btn.addClass("on");
            //return false;
        });
    });
}

function gMktBestTab() {
    var obj_gmenuAll = $("#gmtt .gmenuAll");
    function gmenuAll(on) {
        if (on) {
            obj_gmenuAll.parent().attr("class", "curr").find("img").switchImg(1);
        } else {
            obj_gmenuAll.parent().removeClass("curr").find("img").switchImg(0);
        }
    }
    var gmenuBtn = $("#gmtt .gmenuBtn");
    gmenuBtn.each(function () {
        var self = $(this);
        var layer = $(self.attr("href"));
        $(".g_category").layerSlideToggle({ group: "#gmtt .gmenuBtn", btn: self, layer: layer, speed: 0, imgOn: true,
            fnOpen: function () {
                self.parent().addClass("curr");
                gmenuAll(0);
            },
            fnClose: function () {
                self.parent().removeClass("curr");
                $("#gmenu2 .btn_close").click();
                setTimeout(function () {
                    if (!gmenuBtn.hasClass("unfold")) {
                        gmenuAll(1);
                    }
                }, 50);
            }
        });
    });

    obj_gmenuAll.click(function () {
        gmenuAll(1);
        gmenuBtn.filter(".unfold").click();
        $("#gmenu2 .btn_close").click();
        return false;
    });

    moreCategory();
}

// G베스트
function moreCategory() {
    var obj = $("#gmenu2")
    var obj_more = obj.find(".btn_more");
    var obj_close = obj.find(".btn_close");
    var obj_li = obj.find("li");
    var showNum = 10;

    function init() {
        obj_li.hide();
        obj_li.filter(":lt(" + showNum + ")").show();
        obj_more.css({ display: "block" });
        obj_close.css({ display: "none" });
    }

    obj_more.click(function () {
        obj_li.show();
        $(this).hide();
        obj_close.css({ display: "block" });
        return false;
    });

    obj_close.click(function () {
        init();
        return false;
    });

    init();
}

// 카테고리 131029수정
$.fn.moreCategory2 = function (moreBtn, defaultCount) {
    var self = $(this);
    var obj = self.find("li");
    var btn = $(moreBtn);
    var btnInner = btn.find("span");
    var isBtn = (btn.html() == null) ? false : true;
    var showNum = 0;
    var objCnt = obj.length;
    function init(type) {
        if (type == 'show') {
            if (showNum == 0) {
                showNum = defaultCount;
            }
            else {
                showNum = objCnt;
                btn.addClass('on');
                btnInner.text('닫기');
            }
            obj.filter(":lt(" + showNum + ")").show();

            if (objCnt <= defaultCount) {
                if (isBtn) {
                    btn.hide();
                }
            }
        }
        else {
            obj.hide();
            showNum = defaultCount;
            obj.filter(":lt(" + showNum + ")").show();
            if (isBtn) {
                btn.removeClass('on');
                btnInner.text('더보기');
            }
        }
    }
    if (isBtn) {
        btn.on('click', function () {
            if (showNum != objCnt) {
                init('show');
            } else {
                init('hide');
            }
            return false;
        });
    }
    init('show');
}


function UL_layoutFix(el, isNewLine, addMargine) {
    var obj = $(el).find("li");
    var win_w, obj_w, n, x, nmg;
    var tempWidth = 0;
    function init() {
        obj = $(el).find("li");

        win_w = $(window).width();

        //if (tempWidth == win_w) return;
        tempWidth = win_w;

        obj_w = $(obj).outerWidth();
        n = Math.floor(win_w / obj_w); //라인당 li 갯수

        //개행되는 목록과 개행되지 않는 목록을 분기
        if (isNewLine) {
            x = win_w % obj_w; //남는 영역
            x = (x / n) / 2;
            obj.css({ marginLeft: x, marginRight: x });

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
            x = (n > 1) ? (nmg / (n - 1)) : nmg;
            obj.css({ marginLeft: 0, marginRight: x });
        }
    }

    if (typeof orientationchange != "undefined") {
        $(window).bind("orientationchange", function () {
            init();
        });
    }
    else {
        $(window).bind("resize", function () {
            init();
        });
    }
    init();
}

function UL_layoutUnFix(el, isNewLine) {

    var obj = $(el).find("li");
    var win_w, obj_w, n, x, nmg;
    var tempWidth = 0;

    win_w = $(window).width();
    //alert(win_w+" ::"+ tempWidth);
    //if (tempWidth == win_w) return;
    tempWidth = win_w;

    obj_w = $(obj).outerWidth();
    n = Math.floor(win_w / obj_w); //라인당 li 갯수

    //개행되는 목록과 개행되지 않는 목록을 분기
    if (isNewLine) {
        x = win_w % obj_w; //남는 영역
        x = (x / n) / 2;
        obj.css({ marginLeft: 0, marginRight: 0 });

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
        x = (n > 1) ? (nmg / (n - 1)) : nmg;
        obj.css({ marginLeft: 0, marginRight: x });
    }
}

function Premium_layoutFix(el) {

    function init() {
        var obj = $(el).find("ul"); 				//항목 객체
        var obj_service = $(el).find(".service"); //서비스 항목 객체
        var outer_w = $(el).find(".right").width(); //전체 항목을 감싸고 있는 너비
        var obj_w = obj.width(); 				//항목 1개의 너비
        var remain_w = obj_w + (outer_w - (obj_w * 5) + 5); //잔여 너비(각 항목의 marginLeft:-1px 보정값 5)
        obj_service.css({ width: remain_w });
    }

    if (typeof orientationchange != "undefined") {
        $(window).bind("load orientationchange", function () {
            init();
        });
    }
    else {
        $(window).bind("load resize", function () {
            init();
        });
    }
}

function GNB_layoutFix(el) {
    var obj = $(el).find("li");
    var statRotate = 0;
    function init() {

        win_w = $(window).width();
        win_h = $(window).height();

        if (win_w < win_h || win_w <= 480) {
            //portrait : 1

            if (statRotate == 1)
                return;

            obj.css("width", "2.5%");
            statRotate = 1;
        }
        else {
            //landscape : 2

            if (statRotate == 2)
                return;

            obj.css("width", "1.66%");
            statRotate = 2;
        }

    }

    if (typeof orientationchange != "undefined") {
        $(window).bind("load orientationchange", function () {
            init();
        });
    }
    else {
        $(window).bind("load resize", function () {
            init();
        });
    }
}

//----------------------------------------
// 글자제한
//----------------------------------------
function limitCharacters(textid, limit, limitid) {

    // 입력 값 저장
    var text = $('#' + textid).val();

    // 입력값 길이 저장
    var textlength = text.length;
    if (textlength > limit) {
        $('#' + limitid).html(limit + '자 이상 쓸수 없습니다!');

        // 제한 글자 길이만큼 값 재 저장
        $('#' + textid).val(text.substr(0, limit));
        return false;
    } else {
        $('#' + limitid).html(limit - textlength);
        return true;
    }
}

function Characters(textid, limitid) {

    // 입력 값 저장
    var text = $('#' + textid).val();

    // 입력값 길이 저장
    var textlength = text.length;

    $('#' + limitid).html(textlength);
    return true;

}

function clearInput(id) {
    var obj = document.getElementById(id);
    obj.value = '';
    obj.focus();
    addTopSearchKeyEvent();
}

/**
* GNB 영역의 대 카테고리 레이어를 보여준다.
*/
function showLCategoryLayer() {

    showMCategoryLayer(-1);

    var objLayer = $("#menu");
    if (objLayer.css("display") == "block") {
        //objLayer.slideUp("fast");
        objLayer.css("display", "none");
    }
    else {
        //objLayer.slideDown("fast");
        objLayer.css("display", "block");
    }
}

/**
* GNB 영역의 중 카테고리 레이어를 보여준다.
*/
function showMCategoryLayer(selectedNo) {

    var totalBtnCount = 8; //Large Category Count
    var objLayer;
    var objBtn;
    var objSpan;
    var objImg;
    var objLCategory;

    for (var i = 1; i <= totalBtnCount; i++) {

        objLayer = $("#m_category" + i);
        objLCategory = $("#l_category" + i);
        objBtn = objLCategory.children("a");
        objSpan = objBtn.children("span");
        objImg = objSpan.children("img");

        if (selectedNo == i && objLayer.css("display") != "block") {

            objSpan.addClass("selected");
            objLayer.css("display", "block");
            objImg.attr("src", objImg.attr("src").replace(".png", "_on.png"));

        }
        else {

            objSpan.removeClass("selected");
            objImg.attr("src", objImg.attr("src").replace("_on.png", ".png"));
            objLayer.css("display", "none");

        }
    }
}

/**
* HEADER SEARCH 의 검색어 삭제버튼 유무에 따라 INPUT 영역을 변경한다.
*/
function addTopSearchKeyEvent() {
    var objDelete = document.getElementById("top_search_del");
    var objInput = document.getElementById("top_search");
    if (objInput.value.length > 0) {
        objDelete.style.display = "block";
        objInput.style.paddingRight = "45px";
    }
    else if (objInput.value.length == 0) {
        objDelete.style.display = "none";
        objInput.style.paddingRight = "0px";
    }
}

/**
* HEADER SEARCH 의 검색어 삭제버튼 유무에 따라 INPUT 영역을 변경한다.
*/
function addTopSearchKeyEvent2() {
    var objDelete = document.getElementById("top_search_del");
    var objInput = document.getElementById("scb_lyr");
    if (objInput.value.length > 0) {
        objDelete.style.display = "block";
        objInput.style.paddingRight = "45px";
    }
    else if (objInput.value.length == 0) {
        objDelete.style.display = "none";
        objInput.style.paddingRight = "0px";
    }
}

/**
* 0.5초 주기로 KEY EVENT를 발생시킨다.
* 안드로이드 디바이스에서 키 이벤트가 잘 발생하지 않는 문제가 있어
* 해당 코드를 삽입함
*/
var TopSearchObserve = function (oEl) {
    this._o = oEl;
    this._value = oEl.value;
    this._bindEvents();
};
TopSearchObserve.prototype._bindEvents = function () {
    var self = this;
    var bind = function (oEl, sEvent, pHandler) {
        if (oEl.attachEvent) oEl.attachEvent('on' + sEvent, pHandler);
        else oEl.addEventListener(sEvent, pHandler, false);
    };

    bind(this._o, 'focus', function () {
        if (self._timer) clearInterval(self._timer);
        self._timer = setInterval(function () {
            if (self._value != self._o.value) {
                self._value = self._o.value;
                self._fireEvent();
            }
        }, 50);
    });

    bind(this._o, 'blur', function () {
        if (self._timer) clearInterval(self._timer);
        self._timer = null;
    });
};
TopSearchObserve.prototype._fireEvent = function () {
    if (document.createEvent) {
        var e;
        if (window.KeyEvent) {
            e = document.createEvent('KeyEvents');
            e.initKeyEvent('keyup', true, true, window, false, false, false, false, 65, 0);
        }
        else {
            e = document.createEvent('UIEvents');
            e.initUIEvent('keyup', true, true, window, 1);
            e.keyCode = 65;
        }
        this._o.dispatchEvent(e);
    }
    else {
        var e = document.createEventObject();
        e.keyCode = 65;
        this._o._fireEvent('onkeyup', e);
    }
};

/**
* iscroll.js library 적용한 스크롤 영역 새로고침
*/
function refreshFixedScroll(scrollId) {
    new iScroll(scrollId, { hScrollbar: false, vScrollbar: true, hScroll: false });
}

/**
* UL, LI 태그 구조에서 특정 LI 를 삭제하는 함수
* - groupId : UL TAG ID
* - itemId  : LI TAG ID
*/
function deleteItem(groupId, itemId) {
    var objGroup = document.getElementById(groupId);
    var objItem = document.getElementById(itemId);
    if (objGroup == undefined || objItem == undefined) return;
    objGroup.removeChild(objItem);
}

/**
* 특정 INPUT 값을 증감 또는 가감하는 함수
* - inputId : INPUT TAG ID
* - gapNum  : 증감 또는 가감할 수
*/
function changeVol(inputId, gapNum) {
    var obj = document.getElementById(inputId);
    if (obj == undefined) return;

    var result = Number(obj.value) + Number(gapNum);
    obj.value = (result > 0) ? result : 1;
}

/*
$(window).load(function ()
{

//HEADER SEARCH
new TopSearchObserve(document.getElementById('top_search'));
$('#top_search').bind('keyup', function (event, ui)
{
});
});
*/

function deleteTag(listId) {
    var obj = document.getElementById(listId);
    $(obj).remove();
}

//layer display 131127 수정
function displayLayer_posit(ele, pos) {
	var $element = $('#' + ele);
	if ($element.is(':visible')) {
		$element.hide();
		$element.css('z-index', '0');
		closeDimmedLayer();
		$element = null;
	} else {
		$element.css('z-index', '30');
		$element.show();
		showDimmedLayer();

	}
	if (pos) {
		// POSITION
		var $objPosit = $(pos);
		var tpos = $objPosit.offset().top;
		$('#' + ele).css({ "top": tpos - 65 });
	}
}

