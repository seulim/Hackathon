var userAgent = navigator.userAgent;

// 갤럭시S3 
if (userAgent.indexOf("SHV-E210") > -1 || userAgent.indexOf("SHW-M440") > -1) {
	$('meta.#viewport').attr('content', 'user-scalable=no, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, width=480, target-densitydpi=high-dpi');
	//$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=0.66, minimum-scale=0.66, maximum-scale=0.66, width=480');

	//아이스크림 겔럭시넥서스 일경우(갤럭시S와 S2에 대해 ANDROID 4)
} else if (userAgent.indexOf("ANDROID 4") > -1 || userAgent.indexOf("SHW-M110") > -1 || userAgent.indexOf("SHW-M250") > -1) {
	$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=480, target-densitydpi=device-dpi');

	//허니콤 갤럭시탭 일경우
} else if (userAgent.indexOf("ANDROID 3") > -1) {
	$('meta.#viewport').attr('content', 'user-scalable=no, initial-scale=1.0, maximum-scale=1.0,  width=device-width');

	//HTC 폰일경우
} else if (userAgent.indexOf("HTC") > -1) {
	$('meta.#viewport').attr('content', 'user-scalable=no, initial-scale=1.0, maximum-scale=1.0, width=480, target-densitydpi=device-dpi');

	//갤럭시 탭(9.8, 10.1) 노트(10.1)일경우
} else if (userAgent.indexOf("SHV-E140") > -1 || userAgent.indexOf("SHW-M380") > -1 || userAgent.indexOf("SHW-M480") > -1) {
	$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=0.66, minimum-scale=0.66, maximum-scale=0.66, width=device-width, target-densitydpi=device-dpi');

} else {
	$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=0.66, minimum-scale=0.66, maximum-scale=0.66, width=device-width, target-densitydpi=high-dpi');
}


//갤럭시S3 경우만 Landscape/Portrait(가로/세로) 변경시 스크립트를 이용하여 사이즈를 조절한다.
if (userAgent.indexOf("SHV-E210") > -1 || userAgent.indexOf("SHW-M440") > -1) {
	$(window).bind('orientationchange', function (e) {
		if (window.orientation == 90 || window.orientation == -90 || window.orientation == 270) {
			$('meta.#viewport').attr('content', 'user-scalable=no, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, width=801, target-densitydpi=high-dpi');
			//$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=0.66, minimum-scale=0.66, maximum-scale=0.66, width=800');
		}
		else {
			$('meta.#viewport').attr('content', 'user-scalable=no, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, width=480, target-densitydpi=high-dpi');
			//$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=0.66, minimum-scale=0.66, maximum-scale=0.66, width=480');
		}

	}).trigger('orientationchange');

	//갤럭시s2
} else if (userAgent.indexOf("ANDROID 4") > -1 || userAgent.indexOf("SHW-M110") > -1 || userAgent.indexOf("SHW-M250") > -1) {
	$(window).bind('orientationchange', function (e) {
		if (window.orientation == 90 || window.orientation == -90 || window.orientation == 270) {
			$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=800, target-densitydpi=device-dpi');
		}
		else {
			$('meta.#viewport').attr('content', 'user-scalable=yes, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, width=480, target-densitydpi=device-dpi');
		}

	}).trigger('orientationchange');
}