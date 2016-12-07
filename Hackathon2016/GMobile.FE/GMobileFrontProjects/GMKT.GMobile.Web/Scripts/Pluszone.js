function fnApplyAlertLogin(loginUrl) {
	alert("이벤트에 응모하시려면 로그인을 하셔야 합니다.");
	document.location.href = loginUrl;
	return;
}

function fnApplyAlertAttendance(idx) {
	alert("10회 이상 출석 체크후 응모해주세요.");
	return;
}

var NUMBER_OF_ROULETTE_ITEMS = 12;
var ANGLE_BETWEEN_ITEMS = 360 / NUMBER_OF_ROULETTE_ITEMS;