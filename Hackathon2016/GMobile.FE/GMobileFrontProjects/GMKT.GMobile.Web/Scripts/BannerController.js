var BannerController = function (wrapperSelector, indicatorWrapperSelector, prevButtonSelector, nextButtonSelector) {
	var $wrapper = $();
	var $items = $();
	var $indicators = $();

	var currentIndex = 0;
	var maxIndex = -1;

	if (wrapperSelector) {
		$wrapper = $(wrapperSelector);
		$items = $wrapper.children();

		if (indicatorWrapperSelector) {
			$indicators = $(indicatorWrapperSelector).children();
		}

		maxIndex = $items.length - 1;
	}

	$(prevButtonSelector).on("click", function () {
		if (--currentIndex < 0) {
			currentIndex = maxIndex;
		}

		showBanner();
	});

	$(nextButtonSelector).on("click", function () {
		if (++currentIndex > maxIndex) {
			currentIndex = 0;
		}

		showBanner();
	});

	function showBanner() {
		$items.hide();
		$indicators.removeClass("selected");

		$($items[currentIndex]).show();
		$($indicators[currentIndex]).addClass("selected");
	}
};