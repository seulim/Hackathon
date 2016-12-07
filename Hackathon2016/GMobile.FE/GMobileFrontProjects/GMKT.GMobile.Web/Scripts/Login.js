// 페이지 사용 스크립트 정의
function isSpace(inChar) {
    return (inChar == ' ' || inChar == '\t' || inChar == '\n');
}

function trim(tmpStr) {
    var atChar;
    if (tmpStr.length > 0)
        atChar = tmpStr.charAt(0);
    while (isSpace(atChar)) {
        tmpStr = tmpStr.substring(1, tmpStr.length);
        atChar = tmpStr.charAt(0);
    }
    if (tmpStr.length > 0)
        atChar = tmpStr.charAt(tmpStr.length - 1);
    while (isSpace(atChar)) {
        tmpStr = tmpStr.substring(0, (tmpStr.length - 1));
        atChar = tmpStr.charAt(tmpStr.length - 1);
    }
    return tmpStr;
}

function getByteLength(obj) {
    var byteLength = 0;
    for (var inx = 0; inx < obj.length; inx++) {
        var oneChar = escape(obj.charAt(inx));
        if (oneChar.length == 1) {
            byteLength++;
        } else if (oneChar.indexOf("%u") != -1) {
            byteLength += 2;
        } else if (oneChar.indexOf("%") != -1) {
            byteLength += oneChar.length / 3;
        }
    }
    return byteLength;
}

function autoNextFocus(sObj1, sLength, sObj2) {
    if (sObj1.value.length == sLength) {
        document.getElementsByName(sObj2)[0].focus();
    }
}

function checkValidID(id) {
    for (i = 0; i < id.length; i++) {
        if (id.charAt(i) >= '0' && id.charAt(i) <= '9') {
            continue;
        }
        else if (id.charAt(i) >= 'a' && id.charAt(i) <= 'z') {
            continue;
        }
        else if (id.charAt(i) >= 'A' && id.charAt(i) <= 'Z') {
            continue;
        }
        else {
            alert("아이디(ID)는 영문자와 숫자 조합만 가능합니다.");
            document.defaultForm.UID.focus();
            return false;
        }
    }

    if (id.length < 4) {
        alert("회원 아이디(ID)는 4~10자리의 영문자와 숫자 조합만 가능합니다.");
        document.defaultForm.UID.focus();
        return false;
    }

    return true;
}

function getAppCookie(name) {
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

var c_name = "pcid";
var appYn = "N";
function appGetCookie() {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;

            if (unescape(document.cookie.substring(c_start, c_end)) == "") {
                appYn = "N";
            } else {
                appYn = "Y";
            }

        }
    }
    return appYn;
}

function appGetViewLayoutAllCookie() {
    var cName = "viewLayoutAll";
    var i, x, y, ARRcookies = document.cookie.split(";");

    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == cName) {
            return unescape(y);
        }
    }

    return "N";
}

function getUrl(defaultUrl) {
    var currentUrlpath = window.location.pathname.toLowerCase();
    if (currentUrlpath == "/event/2013/01/0102_campaign/twenty_main.asp") {
        location.href = "http://promotion.gmarket.co.kr/eventzone/Info.asp";
    } else {
        location.href = defaultUrl;
    }
}