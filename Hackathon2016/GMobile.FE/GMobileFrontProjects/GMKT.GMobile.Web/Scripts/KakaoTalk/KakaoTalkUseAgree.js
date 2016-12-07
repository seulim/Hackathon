/* 초기화 함수 */
$(document).ready(function () {
    KakaoTalkUseAgree.onInit();

});

// --------------------------------------------------
// Page 초기화
// --------------------------------------------------
function KakaoTalkUseAgree() { }

KakaoTalkUseAgree.onInit = function () {
    // 초기화가 필요한 코드는 여기에

    $("#btnUseAgree").click(function () {
        KakaoTalkUseAgreeEvents.Apply();
    });

    var app_pcid = "";
    app_pcid = checkAppCookie("pcid");

    $("#btnCancle").click(function () {
        var win = window.open('', '_self');
        if (app_pcid.substr(0, 1) == "4") //Gmarket Iphone App??
        {
            win.location.href = "jscall://calliPhoneForCancel";
        } else {
            win.close();
        }
    });
}

// --------------------------------------------------
// Events 
// --------------------------------------------------
function KakaoTalkUseAgreeEvents() { }

KakaoTalkUseAgreeEvents.Apply = function () {

    //사용동의 체크박스 확인
    var isChecked = $("#chkboxUseAgree").is(':checked');
    //alert(isChecked);

    if (!isChecked) {
        alert("사용 동의하셔야 카카오톡 맞춤정보 서비스 신청이 가능합니다.");
        return;
    }

    KakaoTalkUseAgreeEvents.GetAddWithTempUserKey(KakaoTalkApplyUser.TempUserKey, String(KakaoTalkApplyUser.AccessChannel));

}

KakaoTalkUseAgreeEvents.GetAddWithTempUserKey = function (tempUserKey, accessChannel) {

    var nowstamp = new Date().getTime();
    var randomstamp = nowstamp + Math.random().toString();
    //alert(randomstamp)

    $.ajax({
        type: "POST",
        url: "/KakaoTalk/GetAddWithTempUserKey",
        dataType: "json",
        //jsonp: 'callback',
        //contentType: "application/json",
        data: {
            "tempUserKey": tempUserKey,
            "accessChannel": accessChannel,
            "randomStamp": randomstamp
        },
        async: false,
        error: function (request, status, error) {
            alert(error);
        },
        success: function (result) {
            //alert("result_code : " + result.result_code);
            if (result.result_code == "1000") {
                //alert("성공");
                KakaoTalkUseAgreeEvents.Complete();
            }
            else if (result.result_code == "3005") {
                alert("유효하지 않은 임시회원으로 확인됩니다.");
                KakaoTalkUseAgreeEvents.Retry();
            }
            else if (result.result_code == "-1001") {
                alert("휴대폰 번호를 확인하여 주세요.");
                KakaoTalkUseAgreeEvents.Retry();
            }
            else if (result.result_code == "-1005") {
                alert("휴대폰 번호가 고객정보에 저장되어 있지 않습니다.");
                KakaoTalkUseAgreeEvents.Retry();
            }
            else {
                //alert("일시적인 오류가 발생했습니다. \n다시 시도해 주세요. (" + result.result_code + ")");
                KakaoTalkUseAgreeEvents.Retry();
            }
        }
    });
}

KakaoTalkUseAgreeEvents.Complete = function () {

    $("form").attr("action", "/KakaoTalk/KakaoTalkApplyComplete");
    $("form").submit();

}

KakaoTalkUseAgreeEvents.Retry = function () {

    //$("form").append($("<input></input>").attr("id", "hiddenUserM").attr("type", "hidden").attr("value", custHPNo));
    $("form").attr("action", "/KakaoTalk/KakaoTalkPhoneNumAgree");
    $("form").submit();

}

function checkAppCookie(name) {
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