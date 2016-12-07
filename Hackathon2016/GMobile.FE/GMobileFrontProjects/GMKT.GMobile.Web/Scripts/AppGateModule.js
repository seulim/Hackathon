//Todo by sue : V2 배포 이전에 외부에 게시된 배너 링크를 위해 남겨둠
(function (undefined) {
	var IS_IPHONE = (navigator.userAgent.match('iPhone') != null || navigator.userAgent.match('iPod') != null);
	var IS_IPAD = (navigator.userAgent.match('iPad') != null);
	var IS_IOS = (navigator.userAgent.match(/(iPad|iPhone|iPod)/g) ? true : false);
	var IOS_IPHONE_APP_URL = 'https://itunes.apple.com/app/id340330132';
	var IOS_IPAD_APP_URL = 'https://itunes.apple.com/app/id404552480';
	var IS_ANDROID = (navigator.userAgent.match('Android') != null);
	var IS_CHROME = (navigator.userAgent.match('Chrome') != null);

	var variables = {};
	var exportObject = {};

	var PACKAGE_NAME = exportObject.PACKAGE_NAME = 'com.ebay.kr.gmarket';

	var MARKET_BASE_SCHEME = exportObject.MARKET_BASE_SCHEME = 'market://';
	variables.marketAction = 'details';
	variables.marketParameters = 'id=' + exportObject.PACKAGE_NAME;

	var GMARKET_BASE_SCHEME = exportObject.GMARKET_BASE_SCHEME = 'gmarket://';
	variables.gmarketAction = 'main';
	variables.gmarketParameters = '';

	with (variables) {
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
			if (!scheme) {
				scheme = this.getScheme();
			}

			if (IS_ANDROID) {
				if (needClose) {
					setTimeout(function () {
						window.close();
					}, 500);
				}
				document.location = scheme;
			}
			else if (IS_IOS) {
				setTimeout(function () {
					if (IS_IPAD) {
						document.location = IOS_IPAD_APP_URL;
					}
					else {
						document.location = IOS_IPHONE_APP_URL;
					}
				}, 350);
				document.location = scheme;
			}
			else {
				needAlertWhenNothing && alert('iPhone 과 Android 에서만 가능합니다');
				return null;
			}
		}
	}
	window.AppGateModule = exportObject;
} ());