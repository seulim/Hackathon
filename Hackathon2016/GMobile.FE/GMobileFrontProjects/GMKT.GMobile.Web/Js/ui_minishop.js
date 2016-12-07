/*
* 미니샵 카테고리 확장 *
	G마켓 헤더가 확장상태(G마켓카테고리, 추천검색)중인 상태면 G마켓 헤더는 닫힘 상태로 변경해야합니다.
	그래서 front_v2.js에 있는 hdScbKeyEvent, displayLayer2를 실행시켜주었는데, 
	두함수가 해당 객체의 ID값을 참조하고 있기때문에 ID(scb_ip, scb_del, scb_lyr, all_btn, all_lyr) 변경시 같이 변경해 주어야 합니다.
*/

function ms_nav_open(){
	$("html").addClass("ms_nav_extend");
	hdScbKeyEvent('scb_ip', 'scb_del', 'scb_lyr', 'N');
	if($("#all_btn").hasClass("on")){
		displayLayer2('all_lyr', 'all_btn', 'none');
	}
}
/* 미니샵 카테고리 닫기 */
function ms_nav_close(){
	$("html").removeClass("ms_nav_extend");
}

/* 미니샵 카테고리 확장,닫기 토글 */
function ms_nav_toggle() {
	if ($("html").hasClass( 'ms_nav_extend' ) ) {
		ms_nav_close();
	} else {
		ms_nav_open();
	}
}

$(function(){

	/* 
	* 미니샵 카테고리확장/닫기 이벤트 등록 & 미니샵 카테고리위치값 보정 *
		미니샵 카테고리 확장된 상태에서 카테고리이외의 영역(G마켓 헤더 , 미니샵 컨텐츠) 터치시 미니샵 카테고리 닫기
		G마켓 Header의 ID를 참조하고 있기때문에 ID(hd_slim)의 변경시 같이 변경해 주어야 합니다.
	*/
	$(document)
	.on( 'click touchstart', '.ms_nav_extend #ms_cont, .ms_nav_extend #hd_slim, .ms_nav_extend #ft', function ( e ) {
		e.preventDefault();
		ms_nav_close();
	})
	.on( 'click', '#ms_nav_extend', function ( e ) {
		e.preventDefault();
		ms_nav_toggle();
	})
	$("#ms_side").css("top",$("#hd_slim").height());

	/* 관심상품 이벤트 삭제 2013-11-21
	$("._favoriteFunc").bind("click",function(e){
		e.stopPropagation(); e.preventDefault();
		if($(this).hasClass("favoriteOn")){
			alert("이미 관심상품으로 등록되어 있습니다.")
		}else {
			$(this).addClass("favoriteOn");
			alert("Hello World!")
		}
	})
	 */
});