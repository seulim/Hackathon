var url, NeverdieEventUrl, NeverdieItemUrl, NeverdiePromotionUrl, eventNetUrl;
if (location.hostname.indexOf("dev") >= 0 || location.hostname.indexOf("local-mobile") >= 0){
    url = 'http://dev.gmarket.co.kr/';
    NeverdieEventUrl = 'http://eventdev.gmarket.co.kr/';
    NeverdieItemUrl = 'http://itemdev.gmarket.co.kr/';
    NeverdiePromotionUrl = 'http://promotiondev.gmarket.co.kr/';
    eventNetUrl = 'http://devevent2.gmarket.co.kr/';
}
else {
    url = 'http://www.gmarket.co.kr/';
    NeverdieEventUrl = 'http://event.gmarket.co.kr/';
    NeverdieItemUrl = 'http://item.gmarket.co.kr/';
    NeverdiePromotionUrl = 'http://promotion.gmarket.co.kr/';
    eventNetUrl = 'http://eventnet.gmarket.co.kr/';
}



function appGetCookie() {

    var c_name = "pcid";
    var appYn = "N";

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

function CommonApplyEventPlatform(Str, encStr, reloadYn, password, groupYn) {

    document.cookie = "ECif" + "=" + escape(encStr) + "; path=/;domain=gmarket.co.kr";

    if (typeof (reloadYn) == "undefined") {
        reloadYn = "N";
    }

    if (typeof (password) == "undefined") {
        password = "";
    }

    if (typeof (groupYn) == "undefined") {
        groupYn = "N";
    }

    var openerURL = document.URL;

    location.href = eventNetUrl + "eventplatform/Apply?epif=" + Str + "&reload=" + reloadYn + "&groupYn=" + groupYn + "&openerURL=" + openerURL + "&isMobile=Y";
    
    return;
}
