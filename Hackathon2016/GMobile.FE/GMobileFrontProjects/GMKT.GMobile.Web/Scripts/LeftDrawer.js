var NUMBER_OF_RECENT_ITEMS_FOR_LEFT_DRAWER = 3;
var $leftDrawerGetRecentItemsIframe = $("#leftDrawerGetRecentItems");
var recentItemsControllerForLeftDrawer;

$leftDrawerGetRecentItemsIframe.on("load", function () {
	recentItemsControllerForLeftDrawer = new RecentItemsController(false, "#leftDrawerGetRecentItems");

	recentItemsControllerForLeftDrawer.getRecentItems(this, function (result) {
		$("#recent_loading").hide();

		if (result) {
			if (result.length > 0) {
				result = result.slice().reverse();

				if (result.length > NUMBER_OF_RECENT_ITEMS_FOR_LEFT_DRAWER) {
					result = result.slice(0, NUMBER_OF_RECENT_ITEMS_FOR_LEFT_DRAWER);
				}
				var template = Handlebars.compile($("#recent_item_template").html());

				$("#recent_item_list_wrapper").html(template({ list: result }));

				$("#recent_item_title").show();
				$("#recent_item_div").show();
			} else {
				$("#no_recent_item").show();
			}
		} else {
			$("#no_recent_item").show();
		}
	});
});

Handlebars.registerHelper("getSnaUrlForItem", function (fcd, domain, goodsCode) {
    return "http://sna.gmarket.co.kr/?fcd=" + fcd + "&url=" + encodeURIComponent(domain + "/Item?goodscode=" + goodsCode);
});