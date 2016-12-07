/* 초기화 함수 */
$(document).ready(function () {
    KakaoTalkPinNumber.onInit();

});

// --------------------------------------------------
// Page 초기화
// --------------------------------------------------
function KakaoTalkPinNumber() { }

KakaoTalkPinNumber.onInit = function () {
    // 초기화가 필요한 코드는 여기에

    var custHPNo = "";
    custHPNo = KakaoTalkApplyUser.UserHPNoArr[0] + "-" + KakaoTalkApplyUser.UserHPNoArr[1] + "-" + KakaoTalkApplyUser.UserHPNoArr[2];
    //var custHPNo2 = "82" + KakaoTalkApplyUser.UserHPNoArr[0].substring(1) + KakaoTalkApplyUser.UserHPNoArr[1] + KakaoTalkApplyUser.UserHPNoArr[2];


    $("#btnPinNumAgain").click(function () {
        //api 호출 (cert_code/send)
        //alert("custHPNo:" + custHPNo);

        KakaoTalkPinNumberEvents.GetCertCode(custHPNo);

    });

    $("#btnPinNumSubmit").click(function () {

        var certCode = $("#txtInputCode").val();

        if (certCode == "" || certCode == null) {
            alert("카카오톡 메시지로 받으신 인증번호를 입력해 주세요.");
            return false;
        }

        KakaoTalkPinNumberEvents.GetAddWithCertCode(custHPNo, certCode, String(KakaoTalkApplyUser.AccessChannel));

    });
}

// --------------------------------------------------
// Events 
// --------------------------------------------------
function KakaoTalkPinNumberEvents() { }

KakaoTalkPinNumberEvents.Confirm = function () {

    $("form").attr("action", "/KakaoTalk/KakaoTalkApplyComplete");
    $("form").submit();

}

KakaoTalkPinNumberEvents.GetCertCode = function (custHPNo) {

    var nowstamp = new Date().getTime();
    var randomstamp = nowstamp + Math.random().toString();
    //alert(randomstamp)

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
            }
            else if (result.result_code == "3009") {
                alert("최근 3분 이내에 발급된 (유효한) 인증번호가 있습니다.\n카카오톡 메시지를 확인 후 다시 입력해주세요.");
            }
            else if (result.result_code == "-1001") {
                alert("휴대폰 번호를 확인하여 주세요.");
            }
            else{
                alert("일시적인 오류가 발생했습니다. \n다시 시도해 주세요. (" + result.result_code + ")");
            }
        }
    });
}


KakaoTalkPinNumberEvents.GetAddWithCertCode = function (custHPNo, certCode, accessChannel) {

    var nowstamp = new Date().getTime();
    var randomstamp = nowstamp + Math.random().toString();
    //alert(randomstamp)

    $.ajax({
        type: "POST",
        url: "/KakaoTalk/GetAddWithCertCode",
        dataType: "json",
        //jsonp: 'callback',
        //contentType: "application/json",
        data: {
            "custHPNo": custHPNo,
            "certCode": certCode,
            "accessChannel": accessChannel,
            "randomStamp": randomstamp
        },
        async: false,
        error: function (request, status, error) {
            alert(error);
        },
        success: function (result) {
            //alert("result_text : " + result.result_text);
            if (result.result_code == "1000") {
                KakaoTalkPinNumberEvents.Confirm();  //alert("성공");
            }
            else if (result.result_code == "3009") {
                alert("최근 3분 이내에 발급된 (유효한) 인증번호가 있습니다.\n카카오톡 메시지를 확인 후 다시 입력해주세요.");
            }
            else if (result.result_code == "3002") {
                alert("유효하지 않은 인증코드입니다.\n카카오톡 메시지를 확인 후 다시 입력해주세요.");
            }
            else if (result.result_code == "-1001") {
                alert("휴대폰 번호를 확인하여 주세요.");
            }
            else if (result.result_code == "3008") {
                alert("인증번호가 만료되었습니다.");
            }
            else {
                alert("일시적인 오류가 발생했습니다. \n다시 시도해 주세요. (" + result.result_code + ")");
            }
        }
    });
}

// --------------------------------------------------
// Data
// --------------------------------------------------
function KakaoTalkPinNumberData() { }

// --------------------------------------------------
// Bind
// --------------------------------------------------
function KakaoTalkPinNumberBind() { }

// --------------------------------------------------
// Util
// --------------------------------------------------
function KakaoTalkPinNumberUtil() { }
