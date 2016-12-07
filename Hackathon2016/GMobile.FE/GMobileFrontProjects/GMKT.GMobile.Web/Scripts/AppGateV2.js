(function ($, undefined) {
	var queryDictionary = (function () {
		var dictionary = {};
		var query = window.location.search.substring(1);
		var vars = query.split("&");
		for (var i = 0; i < vars.length; i++) {
			var pair = vars[i].split("=");
			// last one will be applied.
			dictionary[pair[0]] = decodeURIComponent(pair[1]);
		}
		return dictionary;
	})();
	//var IS_IOS = (navigator.userAgent.match(/(iPad|iPhone|iPod)/g) ? true : false);

	var variables = {};

	queryDictionary.marketAction && (variables.marketAction = queryDictionary.marketAction);
	if (queryDictionary.marketParameters) {
		if (!queryDictionary.marketAction || queryDictionary.marketAction === 'details') {
			variables.marketParameters = 'id=' + AppGateModule.PACKAGE_NAME + '&' + queryDictionary.marketParameters;
		}
		else {
			variables.marketParameters = queryDictionary.marketParameters;
		}
	}

	queryDictionary.useGmarket && (variables.useGmarket = (queryDictionary.useGmarket === 'N' ? false : true));
	queryDictionary.gmarketAction && (variables.gmarketAction = queryDictionary.gmarketAction);
	queryDictionary.gmarketParameters && (variables.gmarketParameters = queryDictionary.gmarketParameters);
	//IS_IOS && (variables.gmarketParameters = decodeURIComponent(variables.gmarketParameters));
	queryDictionary.webTargetUrl && (variables.webTargetUrl = decodeURIComponent(queryDictionary.webTargetUrl));	

	var INSTANT = queryDictionary.instant === 'Y' ? true : false;
	var IMAGE = queryDictionary.image || '';

	$('#image').prop('src', IMAGE);

	if (INSTANT) {
		AppGateModule.setVariables(variables);
		AppGateModule.executeScheme(true, true);
	}
	else {
		$('#anchor').bind('click', function (event) {
			AppGateModule.setVariables(variables);
			AppGateModule.executeScheme(true);
			return false;
		});
	}
} (jQuery));