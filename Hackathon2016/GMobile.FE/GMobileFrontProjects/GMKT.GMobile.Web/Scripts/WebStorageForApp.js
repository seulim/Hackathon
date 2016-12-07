if (typeof recentItemsController == "undefined") {
	recentItemsController = new recentItemsController(true);
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

function getRecentlyItems() {
	var items = recentItemsController.getRecentItems();

	if (!items) {
		items = [];
	}

	location.href = getReturnScheme({
		"function": "getRecentlyItems",
		"result": JSON.stringify(items.reverse())
	});
}

function deleteRecentlyItem(goodsCode) {
	if (goodsCode) {
		recentItemsController.removeRecentItem(goodsCode);

		var items = recentItemsController.getRecentItems();

		if (!items) {
			items = [];
		}

		location.href = getReturnScheme({
			"function": "deleteRecentlyItem",
			"result": JSON.stringify(items.reverse())
		});		
	}
}