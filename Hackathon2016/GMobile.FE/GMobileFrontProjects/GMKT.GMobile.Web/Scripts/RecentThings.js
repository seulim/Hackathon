var recentItemsController = new RecentItemsController(true);
var recentKeywordsController = new RecentKeywordsController(true);

window.addEventListener("message", function (e) {
	var method = e.data.method;
	var result = null;

	if (method) {
		switch (method) {
			case "getRecentItems":
				result = recentItemsController.getRecentItems();
				break;
			case "removeRecentItem":
				result = true;
				recentItemsController.removeRecentItem(e.data.goodsCode);
				break;
			case "removeRecentItemsAll":
				result = true;
				recentItemsController.removeAll();
				break;
			case "refreshItems":
				result = true;
				recentItemsController.refreshItems();
				break;
			case "getRecentKeywords":
				result = recentKeywordsController.getRecentKeywords();
				break;
			case "setRecentKeyword":
				result = true;
				recentKeywordsController.setRecentKeyword(e.data.keyword);
				break;
			case "removeRecentKeyword":
				result = true;
				recentKeywordsController.removeRecentKeyword(e.data.keyword);
				break;
			case "removeRecentKeywordsAll":
				result = true;
				recentKeywordsController.removeAll();
				break;
			case "on":
				result = true;
				recentKeywordsController.on();
				break;
			case "off":
				result = true;
				recentKeywordsController.off();
				break;
			case "isOn":
				result = recentKeywordsController.isOn();
				break;
		}
	}

	e.source.postMessage({
		method: method,
		result: result
	}, e.origin);
});