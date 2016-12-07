if (typeof cartCountStorage == "undefined") {
	cartCountStorage = new CartCountStorage();
}

function getReturnScheme(object) {
	var scheme = "gmarket://webstorage";
	var param = "";

	if (object) {
		for (var key in object) {
			if (param != "") {
				param += "&";
			}

			param += key + "=" + object[key];
		}

		if (param != "") {
			scheme += "?" + param;
		}
	}

	return scheme;
}

function getCartCount() {
	var result = cartCountStorage.getCount();

	if (result == undefined || result == null) {
		result = { count: 0 };
	}

	location.href = getReturnScheme({
		"function": "getCartCount",
		"result": result.count
	});
}

function setCartCount(count) {
	var result;

	if (count != undefined && count != null) {
		result = cartCountStorage.setCount(count); //cartCountStorage.getCount();
	}

	if (result == undefined || result == null) {
		result = { count: 0 };
	}

	location.href = getReturnScheme({
		"function": "setCartCount",
		"result": result.count
	});	
}