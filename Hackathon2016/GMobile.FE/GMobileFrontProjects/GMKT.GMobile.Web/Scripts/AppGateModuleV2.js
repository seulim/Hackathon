(function (undefined) {
	var uaUtil = {
		getInfo: function () {
			var ua = navigator.userAgent;
			var versionDetect = [];
			var wvDetect = [];
			var info = {
				os: { type: '' },
				browser: { type: '', version: 0 }
			}

			if (ua.match(/(iPad|iPhone|iPod)/g) != null) {
				info.os.type = 'ios';
			} else if (ua.match('Android') != null) {
				info.os.type = 'android';
			}

			if (ua.match('Chrome') != null) {
				info.browser.type = 'chrome';
				versionDetect = (ua.match(/Chrome\/(\d+)\./) || []);
				info.browser.version = (versionDetect.length > 1) ? parseFloat(versionDetect[1] || 0) : 0;

				wvDetect = ua.match(/(Mozilla\/5.0 \(([^)]+)\))/);
				wvDetect = wvDetect != null && wvDetect.length > 2 ? wvDetect[2].split("; ") : [];
				if (wvDetect.indexOf("wv") > -1) {
					info.browser.type = 'chromewv';
				}
			} else if (ua.match('Safari') != null) {
				info.browser.type = 'safari';
				versionDetect = (ua.match(/Version\/([\d\.]+)\sSafari\/(\d+)/) || []);
				info.browser.version = (versionDetect.length > 1) ? parseFloat(versionDetect[1] || 0) : 0;
			}

			return info;
		}
	};

	window.uaInfo = uaUtil.getInfo();
} (undefined));

(function (undefined, uaInfo) {
	//var IS_IPHONE = (navigator.userAgent.match('iPhone') != null || navigator.userAgent.match('iPod') != null);
	//var IS_IPAD = (navigator.userAgent.match('iPad') != null);
	//var IS_IOS = (navigator.userAgent.match(/(iPad|iPhone|iPod)/g) ? true : false);
	var IS_IOS = uaInfo.os.type === 'ios';
	//var IOS_IPHONE_APP_URL = 'https://itunes.apple.com/app/id340330132';
	//var IOS_IPAD_APP_URL = 'https://itunes.apple.com/app/id404552480';
	//var IS_ANDROID = (navigator.userAgent.match('Android') != null);
	var IS_ANDROID = uaInfo.os.type === 'android';
	//var IS_CHROME = (navigator.userAgent.match('Chrome') != null);

	var variables = {};
	var exportObject = {};

	var PACKAGE_NAME = exportObject.PACKAGE_NAME = 'com.ebay.kr.gmarket';

	var MARKET_BASE_SCHEME = exportObject.MARKET_BASE_SCHEME = 'market://';
	variables.marketAction = 'details';
	variables.marketParameters = 'id=' + exportObject.PACKAGE_NAME;

	var GMARKET_BASE_SCHEME = exportObject.GMARKET_BASE_SCHEME = 'gmarket://';
	variables.gmarketAction = 'main';
	variables.gmarketParameters = '';

	var WEB_BASE_URL = "http://mobile.gmarket.co.kr";

	with (variables) {
		var getIntentUrl = function(webTargeturl) {
			var intentUrl = [
                'intent://' + gmarketAction + (gmarketParameters && '?' + gmarketParameters) + "#Intent",
                'scheme=gmarket',
                'package=' + PACKAGE_NAME,
                'S.browser_fallback_url=' + encodeURIComponent(webTargetUrl),
                'end'
			].join(';');
			return intentUrl;
		}

		exportObject.setVariables = function (inputObject) {
			for (var key in inputObject) {
				if (inputObject.hasOwnProperty(key)) {
					variables[key] = inputObject[key];
				}
			}
		}

		exportObject.getScheme = function () {
			var result;

			if (IS_IOS) {
				result =
					GMARKET_BASE_SCHEME
					+ gmarketAction
					+ (gmarketParameters && '?' + gmarketParameters);
			}
			else if (IS_ANDROID) {
				result =
						MARKET_BASE_SCHEME
						+ marketAction
						+ (marketParameters && '?' + marketParameters);

				var urlScheme =
					encodeURIComponent(GMARKET_BASE_SCHEME
					+ gmarketAction
					+ (gmarketParameters && '?' + gmarketParameters));

				result += (result.indexOf('?') > 0 ? '&' : '?') + 'url=' + urlScheme;
			}
			else {
				result = null;
			}
			return result;
		}

		exportObject.executeScheme = function (needAlertWhenNothing, needClose, scheme) {
			var url = webTargetUrl || WEB_BASE_URL;

			if (!scheme) {
				scheme = this.getScheme();
			}

			if (IS_ANDROID) {
				if(uaInfo.browser.type === 'chrome' && uaInfo.browser.version >= 40) {
					//location.replace(url);
					document.location = getIntentUrl(url);
				} else {
					setTimeout(function () {						
						location.replace(url);
					}, 4000);
					document.location = scheme;
				}				
			}
			else if (IS_IOS) {				
				location.replace(url);
			}
			else {
				needAlertWhenNothing && alert('iPhone 과 Android 에서만 가능합니다');
				return null;
			}
		}
	}
	window.AppGateModule = exportObject;
} (undefined, uaInfo));