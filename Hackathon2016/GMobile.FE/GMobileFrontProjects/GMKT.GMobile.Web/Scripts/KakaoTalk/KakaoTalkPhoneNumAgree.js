/* 초기화 함수 */
$(document).ready(function () {
    KakaoTalkPhoneNumAgree.onInit();

});

// --------------------------------------------------
// Page 초기화
// --------------------------------------------------
function KakaoTalkPhoneNumAgree() { }

KakaoTalkPhoneNumAgree.onInit = function () {
    // 초기화가 필요한 코드는 여기에

    $("#btnGetCertCode").click(function () {
        KakaoTalkPhoneNumAgreeEvents.Apply();

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
function KakaoTalkPhoneNumAgreeEvents() { }

KakaoTalkPhoneNumAgreeEvents.Apply = function () {

    //사용동의 여부 확인
    if ($("#chkboxUseAgree").length > 0 && $("#AccessChannel")[0].value == "GmarketApp") {
        var isChecked = $("#chkboxUseAgree").is(':checked');
        //alert(isChecked);

        if (!isChecked) {
            alert("사용 동의하셔야 카카오톡 맞춤정보 서비스 신청이 가능합니다.");
            return;
        }
    } else if ($("#AccessChannel")[0].value == "KakaoTalkApp") {
        if ($("UseAgree").value == false) {
            alert("KakaoTalkApp useagree fail");
            return;
        }
    };

    for (var i = 0; i < $(".ip_num").length; i++) {

        var txtvalue = $(".ip_num")[i].value;
        if (isNaN(txtvalue)) {
            alert("숫자 입력하세요.");
            return;
        }
        else if (txtvalue == "" || txtvalue == null) {
            alert("휴대폰 번호 입력하세요.");
            return;
        }
        else if (i == 0 && txtvalue.length != 3) {
            alert("입력하신 휴대폰 번호의 확인이 필요합니다.");
            return;
        }
        else if (i == 1) {
            if (!(txtvalue.length == 3 || txtvalue.length == 4)) {
                alert("입력하신 휴대폰 번호의 확인이 필요합니다.");
                return;
            }
        }
        else if (i == 2 && txtvalue.length != 4) {
            alert("입력하신 휴대폰 번호의 확인이 필요합니다.");
            return;
        }


    };

    var custHPNo = $(".ip_num")[0].value + "-" + $(".ip_num")[1].value + "-" + $(".ip_num")[2].value;

    //alert("custHPNo:" + custHPNo);

    KakaoTalkPhoneNumAgreeEvents.GetCertCode(custHPNo);

}

KakaoTalkPhoneNumAgreeEvents.GetCertCode = function (custHPNo) {

    var nowstamp = new Date().getTime();
    var randomstamp = nowstamp + Math.random().toString();
    //alert (randomstamp)


    $.ajax({
        type: "POST",
        url: "/KakaoTalk/GetCertCode",
        dataType: "json",
        //jsonp: 'callback',
        //contentType: "application/json",
        data: {
            "custHPNo": custHPNo,
            "randomStamp": randomstamp
        },
        async: false,
        error: function (request, status, error) {
            alert(error);
        },
        success: function (result) {

            //alert("result_text : " + result.result_text);
            if (result.result_code == "1000") {
                alert("인증번호가 발송되었습니다.");
                KakaoTalkPhoneNumAgreeEvents.NextStep();
            }
            else if (result.result_code == "3009") {
                alert("최근 3분 이내에 발급된 (유효한) 인증번호가 있습니다.\n카카오톡 메시지를 확인 후 다시 입력해주세요.");
                KakaoTalkPhoneNumAgreeEvents.NextStep();
            }
            else if (result.result_code == "-1001") {
                alert("휴대폰 번호를 확인하여 주세요.");
            }
            else {
                alert("일시적인 오류가 발생했습니다. \n다시 시도해 주세요. (" + result.result_code + ")");
            }
        }
    });
}

KakaoTalkPhoneNumAgreeEvents.NextStep = function () {

    $("form").attr("action", "/KakaoTalk/KakaoTalkPinNumber");
    $("form").submit();
}

// --------------------------------------------------
// Data
// --------------------------------------------------
function KakaoTalkPhoneNumAgreeData() { }

// --------------------------------------------------
// Bind
// --------------------------------------------------
function KakaoTalkPhoneNumAgreeBind() { }

// --------------------------------------------------
// Util
// --------------------------------------------------
function KakaoTalkPhoneNumAgreeUtil() { }

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