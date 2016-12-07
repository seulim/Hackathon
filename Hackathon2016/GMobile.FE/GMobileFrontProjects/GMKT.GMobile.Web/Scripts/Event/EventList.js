var EVENT_LIST_PAGE_SIZE = 10;

var pageNo = 1;
var orderKind = 'S';

jQuery(document).ready(function () {
    displayLayer("progress_icon", "none");

    getEventList(1, EVENT_LIST_PAGE_SIZE);
});

function getEventList(pageNo, pageSize) {
    var params = { pageNo: pageNo, pageSize: pageSize, OrderKind: orderKind };

    if (pageNo == 1)
        $('#event_list').empty();

    if (orderKind == 'S') {
        $('#showWin').attr('class', 'on');
        $('#showAll').attr('class', '');
    }
    else {     //전체보기
        $('#showAll').attr('class', 'on');
        $('#showWin').attr('class', '');
    }

    if (pageNo == "" || pageNo == undefined) pageNo = 1;
    if (pageSize == "" || pageSize == undefined) pageSize = 10;

    $("#NoticePageNo").val(pageNo)
    $("#NoticePageSize").val(pageSize)

    jQuery.ajax({
        type: "POST",
        url: "/Event/EventListJson",
        dataType: "json",
        data: params,
        crossDomain: true,
        success: function (data) {

            if (data == null || data == 'undefined') return;
            if (data.myApplicantEventListM == null || data.myApplicantEventListM == 'undefined') return;

            displayEventList(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

// 상태값이 html 로 내려오기 때문에 p 태그 안의 값을 사용.
function toWinString(str) {
    var div = $(str);
    return div.find('p').eq(0).html();
}

function displayEventList(data) {
    var totalCnt = 0;
    totalCnt = data.TotRowCount;
    var pageNo = $("#NoticePageNo").val();
    var pageSize = $("#NoticePageSize").val();

    var ulElement = $('#event_list');
    ulElement.empty();

    if (data.myApplicantEventListM.length > 0) {
        for (var i = 0; i < data.myApplicantEventListM.length; i++) {

            var cls = 'class="win"';
            if (data.myApplicantEventListM[i].EventIsWin) {
                cls = 'class="win"';
            }
            else {
                if (data.myApplicantEventListM[i].EventResultHtml.indexOf("대기") > 1) {
                    cls = 'class=""';
                    data.myApplicantEventListM[i].EventResultHtml = data.myApplicantEventListM[i].EventResultHtml.replace("대기","응모중");
                } else {
                    cls = 'class="fail"';
                }
            }

            ulElement.append('<li ' + cls + '><span class="t1">' + jsDate(data.myApplicantEventListM[i].EventApplicantDate) +
		'</span><span class="t2">' + data.myApplicantEventListM[i].EventName +
			'</span><span class="reslt">' + toWinString(data.myApplicantEventListM[i].EventResultHtml) +
			'</span></li>');
        }
    } else {
        ulElement.append('<li class="nodata">최근 3개월간 응모한 내역이 없습니다.</li>');
    }
    MyEventApplicantMakePaging("EventApplicantListPageing", totalCnt, pageNo, pageSize);
}

function MyEventApplicantMakePaging(obj, totalCnt, pageNo, pageSize) {
    var target = $('#' + obj);
    target.empty();
    var NaviSize = 10;
    var TotalPageCnt = Math.floor((totalCnt - 1) / pageSize) + 1;

    var beforePage = Math.floor((pageNo - 1) / NaviSize) * NaviSize;
    var nextPage = beforePage + NaviSize + 1;
    var PrevPageNo = eval(pageNo) - 1;
    var NextPageNo = eval(pageNo) + 1;
    if (PrevPageNo < 1) { PrevPageNo = 1; }
    if (NextPageNo > TotalPageCnt) { NextPageNo = TotalPageCnt; }
    if (TotalPageCnt < nextPage) nextPage = TotalPageCnt + 1;

    var html = "";
    if (TotalPageCnt != 0) {
        if (TotalPageCnt > 0) {
           // html += "<a href='javascript:getEventList(1," + pageSize + ")' class='sp_cart prev'><span class='blind'>처음페이지</span></a>&nbsp;"; //기능제거
            if (PrevPageNo < pageNo) {                
                html += "<a href='javascript:getEventList(" + PrevPageNo + "," + pageSize + ")' class='sp_cart prev' ><span class='blind'>이전</span></a>";
            } else {
                html += "<a ><span class='sp_cart prev'><span class='blind'></span></span></a>"; //비활성
            }
        }
        html += "";
        for (var page = beforePage + 1; page < nextPage; page++) {
            if (page == pageNo)
                html += "<a href='javascript:getEventList(" + page + "," + pageSize + ")' class='num selected' >" + page + "</a>";
            else
                html += "<a href='javascript:getEventList(" + page + "," + pageSize + ")' class='num' >" + page + "</a>";
        }
        html += "";
        //if (NextPageNo <= TotalPageCnt)
        if (TotalPageCnt > 0) {
            if (pageNo != TotalPageCnt) {
                html += "<a href='javascript:getEventList(" + NextPageNo + "," + pageSize + ")' class='sp_cart next'><span class='blind'>다음</span></a>&nbsp;"; //활성
            } else {
                html += "<a><span class='sp_cart next'><span class='blind'>다음</span></span></a>&nbsp;"; //비활성
            }            
           // html += "<a href='javascript:getEventList(" + TotalPageCnt + "," + pageSize + ")' class='sp_cart next'><span class='blind'>마지막페이지</span></a>";//기능제거
        }
    }
    target.append(html);
}

//날짜 변환
function jsDate(sdate){
    var convert_date = sdate.substring(5, 10).replace(".", "/");
    return convert_date;
}
