$(window).bind( 'orientationchange', function(e){

	if (window.orientation == 90 || window.orientation == -90 || window.orientation == 270) {
		$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=0.66, minimum-scale=0.66, maximum-scale=0.66, width=800');
	}
	else {
		$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=0.66, minimum-scale=0.66, maximum-scale=0.66, width=480');
	}

}).trigger('orientationchange');