//document.domain = "gmarket.co.kr";

// AddAntiForgeryToken
AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};

var INDEX_CSSROOT = 0;
var INDEX_IMAGEROOT = 1;
var INDEX_SCRIPTROOT = 2;

var INDEX_SECURE_CSSROOT = 3;
var INDEX_SECURE_IMAGEROOT = 4;
var INDEX_SECURE_SCRIPTROOT = 5;
var _IsHttps = null;

function CssUrl(relativePath) {
    if (_IsHttps == null) {
        _IsHttps = document.location.protocol == "https:" ? true : false;
    }

    var rootPath = _ContentUrls[INDEX_CSSROOT];
    if (_IsHttps == true) {
        rootPath = _ContentUrls[INDEX_SECURE_CSSROOT];
    }

    return rootPath.concat(relativePath);
}

function ImageUrl(relativePath) {
    if (_IsHttps == null) {
        _IsHttps = document.location.protocol == "https:" ? true : false;
    }

    var rootPath = _ContentUrls[INDEX_IMAGEROOT];
    if (_IsHttps == true) {
        rootPath = _ContentUrls[INDEX_SECURE_IMAGEROOT];
    }

    return rootPath.concat(relativePath);
}

function ScriptUrl(relativePath) {
    if (_IsHttps == null) {
        _IsHttps = document.location.protocol == "https:" ? true : false;
    }

    var rootPath = _ContentUrls[INDEX_SCRIPTROOT];
    if (_IsHttps == true) {
        rootPath = _ContentUrls[INDEX_SECURE_SCRIPTROOT];
    }

    return rootPath.concat(relativePath);
}

function URLDecode(psEncodeString) {
    var lsRegExp = /\+/g;
    return decodeURIComponent(String(psEncodeString).replace(lsRegExp, " "));
}

// commonClose
function ClosePopup() {
    window.opener = null;
    window.open('', '_parent', '');
    window.close();
}

// resize popup
function AutoResizePopup(objname, w, h) {
    if (objname == undefined) {
        objname = "popWrap";  // 기본 팝업레이아웃
    }
    var thisX;
    var thisY;

    if (w == undefined) {
        thisX = document.getElementById(objname).scrollWidth;
    } else {
        thisX = w;
    }

    if (h == undefined) {
        thisY = document.getElementById(objname).scrollHeight;
    } else {
        thisY = h;
    }


    var maxThisX = screen.width - 50;
    var maxThisY = screen.height - 50;
    var marginY = 0;
    var marginX = 10;

    //alert(!"임시 브라우저 확인 : " + navigator.userAgent);
    // 브라우저별 높이 조절. 
    if (navigator.userAgent.indexOf("MSIE 6") > 0) marginY = 45;        // IE 6.x
    else if (navigator.userAgent.indexOf("MSIE 7") > 0) marginY = 75;    // IE 7.x
    else if (navigator.userAgent.indexOf("MSIE 8") > 0) marginY = 80;    // IE 8.x
    else if (navigator.userAgent.indexOf("MSIE 9") > 0) marginY = 90;    // IE 9.x
    else if (navigator.userAgent.indexOf("Firefox") > 0) marginY = 80;   // FF
    else if (navigator.userAgent.indexOf("Opera") > 0) marginY = 30;     // Opera
    else if (navigator.userAgent.indexOf("Netscape") > 0) marginY = -2;  // Netscape
    else if (navigator.userAgent.indexOf("Chrome") > 0) marginY = 64;    // Chrome
    if (navigator.userAgent.indexOf("Chrome") > 0) marginX = 16;

    window.resizeTo(thisX + marginX, thisY + marginY);
}

function safeWinOpen(url, x, y) {
    var centeredY, centeredX;
    var features = 'height=' + y + ',width=' + x + ',toolbar=0,scrollbars=1,status=0,resizable=1,location=0,menuBar=0';
    centeredY = (screen.height - y) / 2;
    centeredX = (screen.width - x) / 2;
    window.open(url, "chdWin", features + ',left=' + centeredX + ',top=' + centeredY).focus();
}

function safePopWinOpen(url, x, y, opt) {
    var centeredY, centeredX;
    var features = 'height=' + y + ',width=' + x + ',' + opt;
    centeredY = (screen.height - y) / 2;
    centeredX = (screen.width - x) / 2;
    window.open(url, "chdWin", features + ',left=' + centeredX + ',top=' + centeredY).focus();
}


/**
* jQuery-UI 를 이용하여 자바스크립트  alert 을 오버라이드
*/
function tAlert(message, options) {

    var defaults =
		{
		    modal: true,
		    resizable: true,
		    title: "TSP - TotalSellingPlatform",
		    buttons: {
		        "Ok": function () {
		            $(this).dialog("close");
		        }
		    }
			, show: 'fade', hide: 'fade'
			, width: 300, height: 'auto', autoResize: true

		}

    $alert = $.createDialog('alert');
    // set message
    $("p", $alert).html(message.replace(/\n/, "<br />"));
    // init dialog
    $alert.dialog($.extend({}
		, defaults, options));
}

/**
* jQuery-UI 를 이용하여 자바스크립트  confirm 을 오버라이드
*/

function tConfirm(message, callback, options) {

    var defaults =
		{
		    modal: true,
		    resizable: true,
		    title: "TSP - TotalSellingPlatform",
		    buttons:
			{
			    확인: function () {
			        $(this).dialog('close');
			        return (typeof callback == 'string') ? window.location.href =
						callback : callback();
			    }
				, 취소: function () {
				    $(this).dialog('close');
				    return false;
				}
			}
			, show: 'fade', hide: 'fade'
			, width: 300, height: 'auto', autoResize: true
		}

    $confirm = $.createDialog('confirm');
    // set message
    $("p", $confirm).html(message.replace(/\n/, "<br />"));
    // init dialog
    $confirm.dialog($.extend({}
		, defaults, options));
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Dimmed Layer Control Script
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function GmktIframeReload(sIdStr) {
    var IframeLayer = document.getElementById(sIdStr);
    if (IframeLayer) IframeLayer.contentWindow.location.reload();
}
var GMKTpopLayerIndex = 0;
var GMKTpopLayerParentReload = "N";
function GmktTopLeft() {
    var T, L;
    if (window.pageYOffset) { T = window.pageYOffset }
    else if (document.documentElement && document.documentElement.scrollTop) { T = document.documentElement.scrollTop; }
    else if (document.body) { T = document.body.scrollTop; }
    if (window.pageXOffset) { L = window.pageXOffset }
    else if (document.documentElement && document.documentElement.scrollLeft) { L = document.documentElement.scrollLeft; }
    else if (document.body) { L = document.body.scrollLeft; }
    arrTopLeft = new Array(T, L);
    return arrTopLeft;
}
function GmktPageSize() {
    var W1, W2, H1, H2;
    var pageWidth, pageHeight;
    if (window.innerHeight && window.scrollMaxY) {
        W2 = document.body.scrollWidth;
        H1 = window.innerHeight + window.scrollMaxY
    } else {
        if (document.body.scrollHeight > document.body.offsetHeight) {
            H1 = document.body.scrollHeight
        } else {
            H1 = document.body.offsetHeight
        }
        if (document.body.scrollWidth > document.body.offsetWidth) {
            W2 = document.body.scrollWidth;
        } else {
            W2 = document.body.offsetWidth;
        }
    }
    if (self.innerHeight) {
        W1 = self.innerWidth;
        H2 = self.innerHeight
    } else {
        if (document.documentElement && document.documentElement.clientHeight) {
            W1 = document.documentElement.clientWidth;
            H2 = document.documentElement.clientHeight
        } else {
            if (document.body) {
                W1 = document.body.clientWidth;
                H2 = document.body.clientHeight
            }
        }
    }
    if (H1 < H2) {
        pageHeight = H2
    } else {
        pageHeight = H1
    }
    if (W2 < W1) {
        pageWidth = W1
    } else {
        pageWidth = W2
    }
    arrPageSize = new Array(pageWidth, pageHeight, W1, H2);
    return arrPageSize;
}
function GmktPopLayerSetParentReload(str) {
    GMKTpopLayerParentReload = str;
}
function GmktPopLayerInit(callback, sUrl, sWidth, sHeight, sClickYn) {
    var fileref = document.createElement('link');
    var tmpUrl = location.href;
    var tmpCss;

    if (tmpUrl.indexOf("https://") < 0)
        tmpCss = "http://script.gmarket.co.kr/css/common/dimmed.css";
    else
        tmpCss = "https://script.gmarket.co.kr/css/common/dimmed.css";

    // add poplayer css
    fileref.setAttribute("rel", "stylesheet")
    fileref.setAttribute("type", "text/css")
    fileref.setAttribute("href", tmpCss);
    if (navigator.appVersion.indexOf("MSIE") > -1) {
        var loaded = false;
        fileref.onreadystatechange = function () {
            if (this.readyState == 'loaded' || this.readyState == 'complete') {
                if (loaded) {
                    return;
                }
                loaded = true;
                callback(sUrl, sWidth, sHeight, sClickYn);
            }
        }
    }
    else {
        callback(sUrl, sWidth, sHeight, sClickYn);
    }
    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref);
}
function GmktPopLayerAddOrigin(sUrl, sWidth, sHeight, sClickYn) {
    var popLayer = document.getElementById("GmktPopLayer");
    if (!popLayer) {
        // create poplayer
        var newPopLayer = document.createElement("div");
        newPopLayer.setAttribute('id', 'GmktPopLayer');
        newPopLayer.className = "poplayer";
        document.body.appendChild(newPopLayer);
        popLayer = newPopLayer;
    }
    var arrPageSize, arrTopLeft;
    arrPageSize = GmktPageSize();
    arrTopLeft = GmktTopLeft();

    popLayer.style.height = arrPageSize[1] + "px";
    popLayer.style.width = arrPageSize[0] + "px";

    GMKTpopLayerIndex++;
    var element = document.createElement("div");
    element.setAttribute('id', 'popLayer' + GMKTpopLayerIndex);

    element.className = "poplayer";
    element.style.height = arrPageSize[1] + "px";
    element.style.width = arrPageSize[0] + "px";
    element.style.zIndex = 99998 + GMKTpopLayerIndex;

    var dimmed = document.createElement("div");
    dimmed.setAttribute('id', 'popLayerDimmed' + GMKTpopLayerIndex);
    dimmed.className = "dimmed";
    if (sClickYn == "Y") dimmed.onclick = GmktPopLayerDelete;
    else dimmed.onclick = GmktPopLayerNull;

    dimmed.style.width = arrPageSize[0] + "px";
    if (navigator.appVersion.indexOf("MSIE") > -1 && arrPageSize[1] > 4096) {
        var dTop = document.body.scrollTop || document.documentElement.scrollTop;
        dimmed.style.height = 4096 + "px";
        if ((dTop + 4096) < arrPageSize[1]) dimmed.style.top = dTop - 1500;
        else dimmed.style.top = (arrPageSize[1] - 4096) + "px";
    } else
        dimmed.style.height = arrPageSize[1] + "px";

    if ((/MSIE (6)/).test(navigator.userAgent)) {
        var dIframe = document.createElement('iframe');
        dIframe.setAttribute('id', 'popLayerDimmedIframe' + GMKTpopLayerIndex);
        dIframe.className = "blocker";
        if (sClickYn == "Y")
            dIframe.src = "~/Dimmed/GmktDimmedLayerEvent";
        else
            dIframe.src = 'about:blank';
        dimmed.appendChild(dIframe);

    }
    element.appendChild(dimmed);

    var contents = document.createElement("div");
    contents.setAttribute('id', 'popLayerContents' + GMKTpopLayerIndex);
    contents.className = "frame_setting";
    if (sClickYn == "Y") contents.onclick = GmktPopLayerDelete;
    else contents.onclick = GmktPopLayerNull;
    var iTempTop = (arrPageSize[3] / 2) - (sHeight / 2) + (arrTopLeft[0])
    iTempTop = iTempTop < 0 ? 0 : iTempTop;
    contents.style.top = iTempTop + "px";
    contents.style.left = (arrPageSize[2] / 2) - (sWidth / 2) + (arrTopLeft[1]) + "px";
    contents.style.width = sWidth + "px";

    var cIframe = (/MSIE (6|7|8)/).test(navigator.userAgent) ? document.createElement('<iframe name="' + 'popLayerIframe' + GMKTpopLayerIndex + '">') : document.createElement('iframe');
    cIframe.setAttribute('name', 'popLayerIframe' + GMKTpopLayerIndex);
    cIframe.setAttribute('id', 'popLayerIframe' + GMKTpopLayerIndex);
    cIframe.src = sUrl;
    cIframe.width = sWidth + 'px';
    cIframe.height = sHeight + 'px';
    cIframe.frameBorder = 'no';
    cIframe.scrolling = 'no';

    contents.appendChild(cIframe);

    element.appendChild(contents);

    for (var i = 0; i < popLayer.childNodes.length; i++) popLayer.childNodes[i].childNodes[0].style.display = "none";
    popLayer.appendChild(element);
}
function GmktPopScrollLayerAddOrigin(sUrl, sWidth, sHeight, sClickYn) {
    var popLayer = document.getElementById("GmktPopLayer");
    if (!popLayer) {
        // create poplayer
        var newPopLayer = document.createElement("div");
        newPopLayer.setAttribute('id', 'GmktPopLayer');
        newPopLayer.className = "poplayer";
        document.body.appendChild(newPopLayer);
        popLayer = newPopLayer;
    }
    var arrPageSize, arrTopLeft;
    arrPageSize = GmktPageSize();
    arrTopLeft = GmktTopLeft();

    popLayer.style.height = arrPageSize[1] + "px";
    popLayer.style.width = arrPageSize[0] + "px";

    GMKTpopLayerIndex++;
    var element = document.createElement("div");
    element.setAttribute('id', 'popLayer' + GMKTpopLayerIndex);

    element.className = "poplayer";
    element.style.height = arrPageSize[1] + "px";
    element.style.width = arrPageSize[0] + "px";
    element.style.zIndex = 99998 + GMKTpopLayerIndex;

    var dimmed = document.createElement("div");
    dimmed.setAttribute('id', 'popLayerDimmed' + GMKTpopLayerIndex);
    dimmed.className = "dimmed";
    if (sClickYn == "Y") dimmed.onclick = GmktPopLayerDelete;
    else dimmed.onclick = GmktPopLayerNull;

    dimmed.style.width = arrPageSize[0] + "px";
    if (navigator.appVersion.indexOf("MSIE") > -1 && arrPageSize[1] > 4096) {
        var dTop = document.body.scrollTop || document.documentElement.scrollTop;
        dimmed.style.height = 4096 + "px";
        if ((dTop + 4096) < arrPageSize[1]) dimmed.style.top = dTop - 1500;
        else dimmed.style.top = (arrPageSize[1] - 4096) + "px";
    } else
        dimmed.style.height = arrPageSize[1] + "px";

    if ((/MSIE (6)/).test(navigator.userAgent)) {
        var dIframe = document.createElement('iframe');
        dIframe.setAttribute('id', 'popLayerDimmedIframe' + GMKTpopLayerIndex);
        dIframe.className = "blocker";
        if (sClickYn == "Y")
            dIframe.src = "~/Dimmed/GmktDimmedLayerEvent";
        else
            dIframe.src = 'about:blank';
        dimmed.appendChild(dIframe);

    }
    element.appendChild(dimmed);

    var contents = document.createElement("div");
    contents.setAttribute('id', 'popLayerContents' + GMKTpopLayerIndex);
    contents.className = "frame_setting";
    if (sClickYn == "Y") contents.onclick = GmktPopLayerDelete;
    else contents.onclick = GmktPopLayerNull;
    var iTempTop = (arrPageSize[3] / 2) - (sHeight / 2) + (arrTopLeft[0])
    iTempTop = iTempTop < 0 ? 0 : iTempTop;
    contents.style.top = iTempTop + "px";
    contents.style.left = (arrPageSize[2] / 2) - (sWidth / 2) + (arrTopLeft[1]) + "px";
    contents.style.width = sWidth + "px";

    var cIframe = (/MSIE (6|7|8)/).test(navigator.userAgent) ? document.createElement('<iframe name="' + 'popLayerIframe' + GMKTpopLayerIndex + '">') : document.createElement('iframe');
    cIframe.setAttribute('name', 'popLayerIframe' + GMKTpopLayerIndex);
    cIframe.setAttribute('id', 'popLayerIframe' + GMKTpopLayerIndex);
    cIframe.src = sUrl;
    cIframe.width = sWidth + 'px';
    cIframe.height = sHeight + 'px';
    cIframe.frameBorder = 'no';
    cIframe.scrolling = 'yes';

    contents.appendChild(cIframe);

    element.appendChild(contents);

    for (var i = 0; i < popLayer.childNodes.length; i++) popLayer.childNodes[i].childNodes[0].style.display = "none";
    popLayer.appendChild(element);
}
function GmktPopLayerAdd(sUrl, sWidth, sHeight, sClickYn) {
    GmktPopLayerInit(GmktPopLayerAddOrigin, sUrl, sWidth, sHeight, sClickYn);
}
function GmktPopScrollLayerAdd(sUrl, sWidth, sHeight, sClickYn) {
    GmktPopLayerInit(GmktPopScrollLayerAddOrigin, sUrl, sWidth, sHeight, sClickYn);
}
function GmktPopLayerDelete() {
    var popLayer = document.getElementById("GmktPopLayer");
    if (popLayer) {
        if (popLayer.lastChild) {
            popLayer.removeChild(popLayer.lastChild);
            GMKTpopLayerIndex--;
            if (GMKTpopLayerIndex == 0)
                popLayer.style.height = "0px";
        }
        if (popLayer.lastChild)
            popLayer.lastChild.childNodes[0].style.display = "";
    }
    if (GMKTpopLayerParentReload == "Y")
        location.reload();
}
function GmktPopLayerDeleteAll() {
    var popLayer = document.getElementById("GmktPopLayer");
    if (popLayer) {
        while (popLayer.lastChild)
            popLayer.removeChild(popLayer.lastChild);
        GMKTpopLayerIndex = 0;
        popLayer.style.height = "0px";
    }
    if (GMKTpopLayerParentReload == "Y")
        location.reload();
}
function GmktPopLayerNull() { }
function GmktPopLayerModify(sUrl, sWidth, sHeight, sClickYn) {
    var IframeLayer = document.getElementById("popLayerIframe" + GMKTpopLayerIndex);
    var contents = document.getElementById("popLayerContents" + GMKTpopLayerIndex);
    if (contents) {
        var dimmed = document.getElementById("popLayerDimmed" + GMKTpopLayerIndex);
        var dIframe = document.getElementById("popLayerDimmedIframe" + GMKTpopLayerIndex);
        if (sClickYn == "Y") {
            dimmed.onclick = GmktPopLayerDelete;
            contents.onclick = GmktPopLayerDelete;
            if (dIframe) dIframe.src = "~/Dimmed/GmktDimmedLayerEvent";
        } else {
            dimmed.onclick = GmktPopLayerNull;
            contents.onclick = GmktPopLayerNull;
            if (dIframe) dIframe.src = 'about:blank';
        }
    }
    if (IframeLayer) {
        if (sUrl.length > 0) IframeLayer.src = sUrl;
        if (sWidth.length > 0) IframeLayer.width = sWidth + "px";
        if (sHeight.length > 0) IframeLayer.height = sHeight + "px";
        GmktPopLayerResize("modify");
    }
}
function GmktPopLayerReload() {
    var IframeLayer = document.getElementById("popLayerIframe" + GMKTpopLayerIndex);
    if (IframeLayer) IframeLayer.contentWindow.location.reload();
}
function GmktPopLayerReloadAll() {
    var IframeLayer;
    for (var i = 1; i <= GMKTpopLayerIndex; i++) {
        IframeLayer = document.getElementById("popLayerIframe" + i);
        if (IframeLayer)
            IframeLayer.contentWindow.location.reload();
    }
}
function GmktPopLayerSetInnerIframe(sUrl) {
    var iframe = document.getElementById('GmktPopupLayerInnerIframe');
    if (!iframe) {
        iframe = document.createElement("IFRAME");
        iframe.setAttribute('id', 'GmktPopupLayerInnerIframe');
        iframe.width = '0px';
        iframe.height = '0px';
        document.body.appendChild(iframe);
    }
    iframe.src = sUrl;
}
function GmktPopLayerAddInner(sUrl, sWidth, sHeight, sClickYn) {
    var iframeDomain = "";
    if (document.location.href.split("#").length == 2) iframeDomain = 'http://' + document.location.href.split("#")[1];
    var iframeUrl = iframeDomain + '/Dimmed/GmktDimmedLayerGate?mode=add&c=' + sClickYn + '&h=' + sHeight + '&w=' + sWidth + '&url=' + escape(sUrl);
    GmktPopLayerSetInnerIframe(iframeUrl);
}
function GmktPopScrollLayerAddInner(sUrl, sWidth, sHeight, sClickYn) {
    var iframeDomain = "";
    if (document.location.href.split("#").length == 2) iframeDomain = 'http://' + document.location.href.split("#")[1];
    var iframeUrl = iframeDomain + '/Dimmed/GmktDimmedLayerGate?mode=addscroll&c=' + sClickYn + '&h=' + sHeight + '&w=' + sWidth + '&url=' + escape(sUrl);
    GmktPopLayerSetInnerIframe(iframeUrl);
}
function GmktPopLayerDeleteInner() {
    var iframeDomain = "";
    if (document.location.href.split("#").length == 2) iframeDomain = 'http://' + document.location.href.split("#")[1];
    var iframeUrl = iframeDomain + '/Dimmed/GmktDimmedLayerGate?mode=delete';
    GmktPopLayerSetInnerIframe(iframeUrl);
}
function GmktPopLayerModifyInner(sUrl, sWidth, sHeight, sClickYn) {
    var iframeDomain = "";
    if (document.location.href.split("#").length == 2) iframeDomain = 'http://' + document.location.href.split("#")[1];
    var iframeUrl = iframeDomain + '/Dimmed/GmktDimmedLayerGate?mode=modify&c=' + sClickYn + '&h=' + sHeight + "&w=" + sWidth + "&url=" + escape(sUrl);
    GmktPopLayerSetInnerIframe(iframeUrl);
}
function GmktPopLayerReplaceParentInner(sUrl) {
    var iframeDomain = "";
    if (document.location.href.split("#").length == 2) iframeDomain = 'http://' + document.location.href.split("#")[1];
    var iframeUrl = iframeDomain + '/Dimmed/GmktDimmedLayerGate?mode=replaceparent&url=' + escape(sUrl);
    GmktPopLayerSetInnerIframe(iframeUrl);
}
function GmktPopLayerReloadParentInner() {
    var iframeDomain = "";
    if (document.location.href.split("#").length == 2) iframeDomain = 'http://' + document.location.href.split("#")[1];
    var iframeUrl = iframeDomain + '/Dimmed/GmktDimmedLayerGate?mode=reloadparent';
    GmktPopLayerSetInnerIframe(iframeUrl);
}
function GmktPopLayerEtcInner(fname, fparam, fscript) {
    var iframeDomain = "";
    if (document.location.href.split("#").length == 2) iframeDomain = 'http://' + document.location.href.split("#")[1];
    var iframeUrl = iframeDomain + '/Dimmed/GmktDimmedLayerGate?mode=etc&fname=' + fname + '&fscript=' + escape(fscript) + '&fparam=' + fparam;
    GmktPopLayerSetInnerIframe(iframeUrl);
}
function GmktPopLayerScroll() {
    var popLayer = document.getElementById("GmktPopLayer");
    if (popLayer && GMKTpopLayerIndex > 0) {
        var arrPageSize, element, dimmed, contents;

        arrPageSize = GmktPageSize();
        popLayer.style.height = arrPageSize[1] + "px";
        popLayer.style.width = arrPageSize[0] + "px";

        for (var i = 1; i <= GMKTpopLayerIndex; i++) {
            element = document.getElementById("popLayer" + i);
            if (element) {
                element.style.height = arrPageSize[1] + "px";
                element.style.width = arrPageSize[0] + "px";
            }
            dimmed = document.getElementById("popLayerDimmed" + i);
            if (dimmed) {
                dimmed.style.width = arrPageSize[0] + "px";
                if (navigator.appVersion.indexOf("MSIE") > -1 && arrPageSize[1] > 4096) {
                    var dTop = document.body.scrollTop || document.documentElement.scrollTop;
                    dimmed.style.height = 4096 + "px";
                    if ((dTop + 4096) < arrPageSize[1]) dimmed.style.top = dTop - 1500;
                    else dimmed.style.top = (arrPageSize[1] - 4096) + "px";
                } else
                    dimmed.style.height = arrPageSize[1] + "px";
            }
        }
    }
}
function GmktPopLayerResize(sMode) {
    var popLayer = document.getElementById("GmktPopLayer");
    if (popLayer && GMKTpopLayerIndex > 0) {
        var arrPageSize, element, dimmed, contents, ciframe;
        arrPageSize = GmktPageSize();
        arrTopLeft = GmktTopLeft();
        popLayer.style.height = arrPageSize[1] + "px";
        popLayer.style.width = arrPageSize[0] + "px";

        for (var i = 1; i <= GMKTpopLayerIndex; i++) {
            element = document.getElementById("popLayer" + i);
            if (element) {
                element.style.height = arrPageSize[1] + "px";
                element.style.width = arrPageSize[0] + "px";
            }
            dimmed = document.getElementById("popLayerDimmed" + i);
            if (dimmed) {
                dimmed.style.width = arrPageSize[0] + "px";
                if (navigator.appVersion.indexOf("MSIE") > -1 && arrPageSize[1] > 4096) {
                    var dTop = document.body.scrollTop || document.documentElement.scrollTop;
                    dimmed.style.height = 4096 + "px";
                    if ((dTop + 4096) < arrPageSize[1]) dimmed.style.top = dTop - 1500;
                    else dimmed.style.top = (arrPageSize[1] - 4096) + "px";
                } else
                    dimmed.style.height = arrPageSize[1] + "px";
            }
            contents = document.getElementById('popLayerContents' + i);
            ciframe = document.getElementById("popLayerIframe" + i);
            if (contents && ciframe) {
                if (sMode == "modify") {
                    var iTempTop = (arrPageSize[3] / 2) - (parseInt(ciframe.height) / 2) + (document.documentElement.scrollTop || document.body.scrollTop);
                    iTempTop = iTempTop < 0 ? 0 : iTempTop;
                    contents.style.top = iTempTop + "px";
                }
                contents.style.left = (arrPageSize[2] / 2) - (parseInt(ciframe.width) / 2) + (arrTopLeft[1]) + "px";
                contents.style.width = parseInt(ciframe.width) + "px";
            }
        }
    }
}
function GmktAddEvent(o, evtName, fun) {
    var oldFun = o[evtName];
    if (typeof oldFun != "function") {
        o[evtName] = fun;
    } else {
        o[evtName] = function () {
            oldFun.call(this);
            fun();
        }
    }
}
GmktAddEvent(window, 'onresize', GmktPopLayerResize);
GmktAddEvent(window, 'onscroll', GmktPopLayerScroll);

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Common Javascript Area
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var jjjj_ie6 = navigator.appVersion.indexOf("MSIE 6.0") > -1; //ie6 2012-05-14 추가 

// ie6 png24 
function setPng24(obj) {
    obj.width = obj.height = 1;
    obj.className = obj.className.replace(/\bpng24\b/i, '');
    obj.style.filter = 'progid:DXImageTransform.Microsoft.AlphaImageLoader(src="' + obj.src + '",sizingMethod="image");';
    obj.src = ImageUrl("images/lotte/blank.gif");
    return '';
}

//레이어 오버시 layer
function overLayer(self, idLayer) {
    var $self = $(self),
		evType = 'inline',
		idLayer = idLayer;

    if (!(idLayer.split("#")[1])) {
        idLayer = '#' + idLayer;
    }
    var $shopDiv = $(idLayer);

    $shopDiv.css('display', evType);
    $self.addClass('on');
    $self.bind('mouseout', function () {
        $shopDiv.hide();
        $self.removeClass('on');
    });
    $shopDiv
		.bind('mouseover', function () {
		    $shopDiv.css('display', evType);
		    $self.addClass('on');
		})
		.bind('mouseout', function () {
		    $shopDiv.hide();
		    $self.removeClass('on');
		});
}
//layer
function openLay(id) {
    var layer = document.getElementById(id);
    layer.style.display = 'block';
}
function closeLay(id) {
    var layer = document.getElementById(id);
    layer.style.display = 'none';
}

// 전체 카테고리 보기 
function openCate(self, cateDiv) {
    var $self = $(self),
		$cateLayer = $(document).find(cateDiv);

    if ($cateLayer.css('display') == 'none') {
        $self.addClass('selected');
        $cateLayer.slideDown('500', function () {
            $cateLayer.css('overflow', 'visible');
        });
    } else {
        $self.removeClass('selected');
        $cateLayer.slideUp('500');
    }
}
/*
function openCate(self, cateDiv) {
var $self = $(self),
$cateLayer = $(document).find(cateDiv);

if($cateLayer.css('display') == 'none') {
$self.addClass('selected');
$cateLayer.css('display','block');
} else {
$self.removeClass('selected');
$cateLayer.css('display','none');
}
}*/

//quick 레이어
function overQu(self) {
    var $self = $(self),
		evType = 'block',
		addClass = 'on',
		$shopDiv = $self.next();

    if ($shopDiv.attr('class') == 'overlyr') {
        $shopDiv.css('display', evType);
        $self.bind('mouseout', function () {
            $shopDiv.hide();
        });
        $shopDiv
			.bind('mouseover', function () {
			    $shopDiv.css('display', evType);
			    $self.addClass(addClass);
			})
			.bind('mouseout', function () {
			    $shopDiv.hide();
			    $self.removeClass(addClass);
			});
    }
}

function initMoving(target, position, topLimit, btmLimit) {
    if (!target)
        return false;
    var obj = target;
    obj.initTop = position;
    obj.topLimit = topLimit;
    obj.bottomLimit = document.documentElement.scrollHeight - btmLimit;
    obj.style.position = "absolute";
    obj.top = obj.initTop;
    obj.left = obj.initLeft;
    if (typeof (window.pageYOffset) == "number") {
        obj.getTop = function () {
            return window.pageYOffset;
        }
    } else if (typeof (document.documentElement.scrollTop) == "number") {
        obj.getTop = function () {
            return document.documentElement.scrollTop;
        }
    } else {
        obj.getTop = function () {
            return 0;
        }
    }
    if (self.innerHeight) {
        obj.getHeight = function () {
            return self.innerHeight;
        }
    } else if (document.documentElement.clientHeight) {
        obj.getHeight = function () {
            return document.documentElement.clientHeight;
        }
    } else {
        obj.getHeight = function () {
            return 500;
        }
    }
    obj.move = setInterval(function () {
        if (obj.initTop > 0) {
            pos = obj.getTop() + obj.initTop;
        } else {
            pos = obj.getTop() + obj.getHeight() + obj.initTop;
            //pos = obj.getTop() + obj.getHeight() / 2 - 15;
        }
        if (pos > obj.bottomLimit)
            pos = obj.bottomLimit;
        if (pos < obj.topLimit)
            pos = obj.topLimit;
        interval = obj.top - pos;
        obj.top = obj.top - interval / 3;
        obj.style.top = obj.top + "px";
    }, 30)
}

/* 2012-05-07 수정 및 추가 */
function quickWing() {
    var rvistart = $('#wing');
    var header = $('#subHeader');

    var footerHeight = $('#footer').height();

    var wrap = rvistart.parent().parent(); ;
    var innerWidth = wrap.innerWidth();
    var width = wrap.width();
    var margin = ((width - (innerWidth - width)) / 2) + 5 + 5 + 90;
    var margin2 = ((width - (innerWidth - width)) / 2) + 5 + 5 + 85;

    var sctT = (document.body.scrollTop) ? document.body.scrollTop : document.documentElement.scrollTop;
    var sctL = (document.body.scrollLeft) ? document.body.scrollLeft : document.documentElement.scrollLeft;


    var a = header.width()//$(document).width()

    var marginRight;

    var quickTop;
    if (sctT <= 144) {
        quickTop = header.height() - 5;
    } else {

        if (rvistart.parent().parent().height() + footerHeight < sctT + rvistart.height()) {
            quickTop = -((sctT + rvistart.height() + header.height()) - (rvistart.parent().parent().height() + footerHeight));
        } else {
            quickTop = 5;
        }
    }

    if (innerWidth > document.body.clientWidth) {
        marginRight = -(innerWidth - document.body.clientWidth)
        rvistart.css({
            'position': 'fixed',
            'top': quickTop,
            'right': marginRight + sctL,
            'margin-right': 0
        })
        if (jjjj_ie6) rvistart.css("position", "absolute"); //2012-05-14 추가
    } else {				// 스크롤이 없을때


        marginRight = (a - innerWidth) / 2 + 5;

        if ($('#wingLeft').width() > 0) {
            marginRight -= ($('#wingLeft').width() + 20);
        }
        rvistart.css({
            'position': 'fixed',
            'top': quickTop,
            'right': 0,
            'margin-right': marginRight
        })
        if (jjjj_ie6) rvistart.css("position", "absolute"); //2012-05-14 추가 
    }
}

$(window).resize(function () {
    quickWing();
})

window.onload = function () {

    //rvi 기본 포지션 지정.
    if (document.getElementById("main")) {
        var rviTopPosition = 583;
    } else {
        var rviTopPosition = -5;
    }

    quickWing();

    var rvistart = $('#wing');
    var lvistart = document.getElementById('wingLeft');

    if (rvistart) {

        if (lvistart) {
            lvistart.style.top = rviTopPosition + 'px';
        }

        if ($.browser.msie && parseInt($.browser.version, 10) < 7) {
            rviscroll6();
        }
        function rviscroll6() {
            var maxtop = rviTopPosition;
            var body = document.getElementById('wrap');
            var newtop = (body) ? -(body.offsetTop - 5) : -137; //ie6 스크롤시 top 여백조절
            var sct = (document.body.scrollTop) ? document.body.scrollTop : document.documentElement.scrollTop;
            if (sct <= 0) rv = maxtop;
            else {
                var top = maxtop - sct;
                rv = (top > newtop) ? top : newtop;
            }
            var botm = 330;
            var sch = (document.body.scrollHeight) ? document.body.scrollHeight - botm : document.documentElement.scrollHeight - botm;
            if (body) sch = sch;
            var returnv = ((sct + rv) > sch) ? sch : sct + rv;
            //			rvistart.style.top=(returnv < (rviTopPosition - 0)) ? (rviTopPosition - 0) + 'px' : returnv + 'px';
            rvistart.css({
                'top': (returnv < (rviTopPosition - 0)) ? (rviTopPosition - 0) + 'px' : returnv + 'px'
            })
            if (lvistart) {
                lvistart.style.top = (returnv < (rviTopPosition - 0)) ? (rviTopPosition - 0) + 'px' : returnv + 'px';
            }
        }
        // rvi 관련 top위치 변경.
        window.onscroll = function () {
            if ($.browser.msie && parseInt($.browser.version, 10) < 7) {
                rviscroll6();
            } else {
                var rvi = $('#wing');
                var lvi = $('#wingLeft');
                if (rvi) {
                    var body = document.getElementById('wrap');
                    var maxtop = rviTopPosition;
                    //var newtop=(body)? -(body.offsetTop-50) : 50;
                    var newtop = (body) ? -(body.offsetTop - 5) : 5;

                    rvi.css('background', 'none');

                    if (lvistart) {
                        lvi.css('background', 'none');
                    }
                    var sct = (document.body.scrollTop) ? document.body.scrollTop : document.documentElement.scrollTop;
                    var sct1 = (document.body.scrollLeft) ? document.body.scrollLeft : document.documentElement.scrollLeft;
                    if (sct <= 144) {

                        quickWing();

                        if (lvistart) {
                            lvi.css({
                                'position': 'absolute',
                                'top': maxtop,
                                'left': -95,
                                'margin': 0
                            })
                        }
                    } else {

                        var top = 5;

                        var wrap = $('#wing').parent().parent();
                        var innerWidth = wrap.innerWidth();
                        var width = wrap.width();
                        var margin = ((width - (innerWidth - width)) / 2) + 5 + 5 + 90;
                        var margin2 = ((width - (innerWidth - width)) / 2) + 5 + 5 + 85;

                        quickWing();

                        if (lvistart) {
                            lvi.css({
                                'position': 'fixed',
                                'top': 5,
                                'left': '50%',
                                'margin-left': -margin2
                            })

                        }
                    }
                }
            }
        }
    }
}
/* //2012-05-07 수정 및 추가 */




