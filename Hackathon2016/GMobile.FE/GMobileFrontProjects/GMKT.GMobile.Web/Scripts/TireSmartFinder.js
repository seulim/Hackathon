$("#go_back, #size_search_cancel, #car_search_cancel").on("click", function (e) {
	e.preventDefault();

	if (isApp) {
		location.href = "gmarket://webview?action=goback";
	} else {
		history.back();
	}
});

var $manufacturer = $("#tire_opt5");
var $model = $("#tire_opt6");
var modelTemplate = Handlebars.compile($("#model_template").html());

var $mask = $("#tire_opt6_mask");

var mClassSeq, sClassSeq;

$manufacturer.val(0);
$model.val(0);

$manufacturer.on("change", function () {
	$mask.show();

	$model.val(0);

	mClassSeq = $(this).val();

	if (mClassSeq > 0) {
		$.ajax({
			url: "/Search/GetSmartFinderSClassList",
			type: "POST",
			data: {
				mClassSeq: mClassSeq
			},
			success: function (data) {
				if (data != null) {
					$model.html(modelTemplate(data));

					$mask.hide();
				}
			},
			error: function (data) {
				alert("모델 정보를 가져오는 중 오류가 발생했습니다. 잠시 후 다시 시도해주세요.")

				$manufacturer.val(0);
			}
		});	
	}
});

$model.on("change", function () {
	sClassSeq = $(this).val();
});

$mask.on("click", function (e) {
	if ($manufacturer.val() == 0) {
		alert("제조사를 선택해주세요.");
	} else {
		alert("잠시만 기다려주세요.");	
	}
});

var $width = $("#tire_inp");
var $ratio = $("#tire_inp2");
var $diameter = $("#tire_inp3");

$("#size_search_go").on("click", function () {
	var width = $width.val();
	var ratio = $ratio.val();
	var diameter = $diameter.val();

	if (width == "") {
		alert("단면폭을 입력해주세요.");
		$width.focus();

		return false;
	} else if (ratio == "") {
		alert("편평비를 입력해주세요.");
		$ratio.focus();

		return false;
	} else if (diameter == "") {
		alert("지름을 입력해주세요.");
		$diameter.focus();

		return false;
	}

	var url;

	if (isApp) {
		url = "gmarket://search";
	} else {
		url = "/Search/Search";
	}

	if (location.search != "") {
		url += location.search + "&";
	} else {
		url += "?";
	}

	url += "valueIdName=" + encodeURIComponent(width + "/" + ratio + "R " + diameter);

	if (isApp) {
		url += "&type=smartfinder";
	}
	
	location.href = url;
});

$("#car_search_go").on("click", function () {
	if (mClassSeq <= 0) {
		alert("제조사를 선택해주세요.");
	} else if (sClassSeq <= 0) {
		alert("모델을 선택해주세요.");
	} else {
		var url;

		if (isApp) {
			url = "gmarket://search";
		} else {
			url = "/Search/Search";
		}

		if (location.search != "") {
			url += location.search + "&";
		} else {
			url += "?";
		}

		url += "mClassSeq=" + mClassSeq + "&sClassSeq=" + sClassSeq;

		if (isApp) {
			url += "&type=smartfinder";
		}

		location.href = url;	
	}
});