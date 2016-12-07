var mobileWebUrl = "";
//CONST_MOBILE_WEB_URL 최초정의는 mobile, mitem 에서 각각
if (typeof(CONST_MOBILE_WEB_URL) === "undefined") {
	mobileWebUrl = "http://mobile.gmarket.co.kr";
} else {
	if (CONST_MOBILE_WEB_URL == null || CONST_MOBILE_WEB_URL == "") {
		mobileWebUrl = "http://mobile.gmarket.co.kr";
	} else {
		mobileWebUrl = CONST_MOBILE_WEB_URL;
	}
}
var mobileWebUrlSecure = "";
//CONST_MOBILE_WEB_URL_SECURE 최초정의는 mobile, mitem 에서 각각
if (typeof (CONST_MOBILE_WEB_URL_SECURE) === "undefined") {
	mobileWebUrlSecure = "https://mobile.gmarket.co.kr";
} else {
	if (CONST_MOBILE_WEB_URL_SECURE == null || CONST_MOBILE_WEB_URL_SECURE == "") {
		mobileWebUrlSecure = "https://mobile.gmarket.co.kr";
	} else {
		mobileWebUrlSecure = CONST_MOBILE_WEB_URL_SECURE;
	}
}

var setAddrListClickEvent = function () {
	var h_inpt = $(".plus_addr .scroll .chk_f");
	$(h_inpt).on("click", function (e) {
		e.stopPropagation(); //e.preventDefault(); 
		$(this).siblings('.chk_f input').prop('checked', false).blur();
		$(this).children('input').prop('checked', true).focus();
		$(this).siblings('.chk_f').removeClass("on");
		$(this).addClass("on");
	});
}

var getMyAddrList = function (isLayer) {
	$.ajax({
		url: mobileWebUrlSecure + "/HomePlus/MyAddrList",
		type: "GET",
		//data: ,
		contentType: "application/javascript; charset=utf-8",
		scriptCharset: "utf-8",
		dataType: "jsonp",
		crossdomain: true,
		success: function (result) {
			if (result) {
				var template = Handlebars.compile($('#tmpl_setting_delivery_address').html());
				if (result.addrList.length > 0) {
					$("#hplus_address_ul").html(template({ addrList: result.addrList }));
					$(".addr_tab").find("li a").first().trigger('click'); // 항상 '배송지선택' 탭 열리게.
					var ipt_sec = $(".new_addr_bx .ipt_sec"), ipt_zip = $(".ser_zip");
					$(ipt_sec).removeClass("none");
					$(ipt_zip).removeClass("on");
				}
				else {
					$("#hplus_address_ul").html("배송주소가 설정되어 있지 않습니다.</br>배송주소를 등록해 주세요.");
					setClickEventInit();
				}

				if (isLayer == undefined || isLayer == true) {
					popBlockUI('hplus_address');
				}
				//setClickEventInit();    // 항상 '새로운 주소' 탭 열리게.bc_9944
				/*
				$(".addr_tab").find("li a").first().trigger('click'); // 항상 '배송지선택' 탭 열리게.
				var ipt_sec = $(".new_addr_bx .ipt_sec"), ipt_zip = $(".ser_zip");
				$(ipt_sec).removeClass("none");
				$(ipt_zip).removeClass("on");
				*/
				$('#zip_lst_ul').html("");
				$(".noti").hide();
				$('.input_area input').val("");
				iFrameScroll('fixed_scroll_addr');
				setClickEvent();
				setAddrListClickEvent();
				if (result.selectedAddrNo > 0) {
					var idStr = 'li #' + result.selectedAddrNo;
					if ($(idStr) != undefined) {
						//$(idStr).click()
						// 상위 li 태그에 대한 Object
						var $parentObj = $(idStr).parent();
						$parentObj.addClass('on');
						// 상위 radio check : 이건 안해도 될 듯..
						$(idStr).attr('checked', true);

						// 이동시킬 Y 값을 계산한다.
						var scrollY = $(idStr).parent().offset().top;
						var $addrTab = $('.addr_tab');
						if ($addrTab != undefined) {
							if (scrollY > ($addrTab.offset().top + $addrTab.height())) {
								scrollY -= ($addrTab.offset().top + $addrTab.height());
							}
						}
						// iScroll 이 적용 된 경우 iScroll 의 ScrollTo 로 이동시켜 주지 않으면 iScroll 뻑남
						if (myScroll["fixed_scroll_addr"] != undefined) {
							if (scrollY > ($addrTab.offset().top + $addrTab.height())) {
								myScroll["fixed_scroll_addr"].scrollTo(0, -1 * scrollY, 0);
							}
						}
					}
				}
				return false;
			} else {
				alert("배송주소록을 가져오던 중에 오류가 발생했습니다.");
			}
		},
		error: function (result) {
			alert("배송주소록을 가져오던 중에 오류가 발생했습니다.");
		}
	});
}

var setClickEventInit = function () {
    var tab_bx = $(".addr_tab")
        , addr_tab = $(tab_bx).find("li a")
        , tab_view = $(".layer_hplus .htab_view")
        , btn_zip = $(tab_view).find(".btn_zip")
        , ipt_sec = $(".new_addr_bx .ipt_sec")
        , ipt_input = $(ipt_sec).find('input')
        , ipt_zip = $(".ser_zip")
        , zip_ok = $(ipt_zip).find(".chk");

    addr_tab.eq(1).trigger('click'); // 항상 '새로운 주소' 탭 열리게.
    $(".addr_tab li").removeClass("on");
    addr_tab.parent('li.new_addr').addClass("on");
    $(tab_view).css({ 'display': 'none' });
    $(".new_addr_bx").css({ 'display': 'block' });
}

var getHomePlusBranchName = function (fromWhere, zipCd, zipAddr, isSecure) {
	$("#btnSetBranch").addClass("disabled");
	$("#btnSetBranch").prop("disabled", true);
	var prefixUrl = mobileWebUrl;
	if (isSecure != undefined && isSecure == true) {
		prefixUrl = mobileWebUrlSecure;
	}
	$.ajax({
		url: prefixUrl + "/HomePlus/HomePlusBranchName",
		type: "GET",
		data: { "zipCd": zipCd },
		contentType: "application/javascript; charset=utf-8",
		scriptCharset: "utf-8",
		dataType: "jsonp",
		crossdomain: true,
		success: function (result) {
			if (result.success) {
				if (fromWhere == "list") {
					if (result.tryBranchChangeButFail) {
						var branchNameHtml = "장바구니에 다른 점포의 상품이 담겨 있습니다. 당일배송 주소를 변경하시려면 다른 점포의 상품을 장바구니에서 삭제해주세요.";
						$(".market_txt").html(branchNameHtml);
						$("#btnSetBranch").removeClass("disabled");
						$("#btnSetBranch").prop("disabled", false);
						$("#btnSetBranch").html("닫기");
						if (isSecure != undefined && isSecure == true) {
							$("#btnSetBranch").attr("onclick", "javascript:refreshVip(); return false;");
						} else {
							$("#btnSetBranch").attr("onclick", "popBlockUI('hplus_address');return false;")
						}
					} else {
						var branchNameHtml = "<strong>" + result.branch.BranchNm + "</strong>에서 배송됩니다."
						$(".market_txt").html(branchNameHtml);
						$("#btnSetBranch").removeClass("disabled");
						$("#btnSetBranch").prop("disabled", false);
						$("#btnSetBranch").html("확인");
						if (isSecure != undefined && isSecure == true) {
							$("#btnSetBranch").attr("onclick", "javascript:setMyHomePlusBranch(true);return false;")
						} else {
							$("#btnSetBranch").attr("onclick", "javascript:setMyHomePlusBranch();return false;")
						}
					}
				}
				else {
					if (result.tryBranchChangeButFail) {
						alert("장바구니에 다른 점포의 상품이 담겨 있습니다. 당일배송 주소를 변경하시려면 다른 점포의 상품을 장바구니에서 삭제해주세요.");
					} else {
						selectZipCode(zipCd, zipAddr);
					}
				}
			} else {
				var branchNameHtml = "선택하신 주소지 인근에 당일 배송 가능한 홈플러스 점포가 없습니다."
				if (fromWhere == "list") {
					$(".market_txt").html(branchNameHtml);
					$("#btnSetBranch").removeClass("disabled");
					$("#btnSetBranch").prop("disabled", false);
					$("#btnSetBranch").html("닫기");
					if (isSecure != undefined && isSecure == true) {
						$("#btnSetBranch").attr("onclick", "javascript:refreshVip(); return false;");
					} else {
						$("#btnSetBranch").attr("onclick", "popBlockUI('hplus_address');return false;")
					}
				} else {
					alert(branchNameHtml);
				}
			}
		},
		error: function (result) {
			alert("홈플러스 지점이름을 가져오던 중에 오류가 발생했습니다.");
		}
	});
}

var refreshVip = function () {
	if (typeof(CONST_VIP_URL) != "undefined" && CONST_VIP_URL != null && CONST_VIP_URL != "") {
		window.location.href = CONST_VIP_URL;
	} else {
		window.location.reload(true);
	}
}

var getMyHomePlusBranchTimeTable = function (isLayer) {
	$.ajax({
		url: mobileWebUrl + "/HomePlus/GetMyHomePlusBranchTimeTable",
		type: "GET",
		//data: { "zipCd": zipCd },
		contentType: "application/javascript; charset=utf-8",
		scriptCharset: "utf-8",
		dataType: "jsonp",
		crossdomain: true,
		success: function (result) {
			if (result.success) {
				var template = Handlebars.compile($('#tmpl_branch_timetable').html());
				$("#hplus_dtime").html(template({ branchInfo: result }));
				if (isLayer == undefined || isLayer == true) {
					popBlockUI('hplus_dtime'); return false;
				}
			} else {
				alert("홈플러스 지점정보를 가져오는 중에 오류가 발생했습니다.");
			}
		},
		error: function (result) {
			alert("홈플러스 지점정보를 가져오는 중에 오류가 발생했습니다.");
		}
	});
}

var gotoAddressBook = function () {
	if (CONST_ADDRESS_URL != undefined && CONST_ADDRESS_URL != null && CONST_ADDRESS_URL != "") {
		window.location.href = CONST_ADDRESS_URL;
	}
}

var setMyHomePlusBranch = function (isSecure) {
	if ($(".h_lst_addr .chk_f.on input").length <= 0) {
		alert("배송지를 선택해 주세요.");
		return false;
	}

	var addrNo = $(".h_lst_addr .chk_f.on input").prop("id");

	var prefixUrl = mobileWebUrl;
	if (isSecure != undefined && isSecure == true) {
		prefixUrl = mobileWebUrlSecure;
	}

	$.ajax({
		url: prefixUrl + "/HomePlus/SetMyHomePlusBranch",
		type: "GET",
		data: { "addrNo": addrNo },
		contentType: "application/javascript; charset=utf-8",
		scriptCharset: "utf-8",
		dataType: "jsonp",
		crossdomain: true,
		success: function (result) {
			if (result.success) {
				if (typeof(CONST_VIP_URL) != "undefined" && CONST_VIP_URL != null && CONST_VIP_URL != "") {
					window.location.href = CONST_VIP_URL;
				} else {
					window.location.reload(true);
				}
			} else {
				if (result.message != undefined && result.message != null) {
					alert(result.message);
				} else {
					alert("홈플러스 지점을 저장하는 중에 오류가 발생했습니다.");
				}
			}
		},
		error: function (result) {
			alert("홈플러스 지점을 저장하는 중에 오류가 발생했습니다.");
		}
	});
}

var findZipCode = function (isSecure) {
	var locationKeyword = $("#adr_name").val();
	if (locationKeyword == "") {
		alert("지역명을 입력해 주세요");
		return false;
	}

	var prefixUrl = mobileWebUrl;
	if (isSecure != undefined && isSecure == true) {
		prefixUrl = mobileWebUrlSecure;
	}

	$.ajax({
		url: prefixUrl + "/HomePlus/JsonReturnZipList",
		type: "GET",
		data: { "locationKeyword": locationKeyword },
		contentType: "application/javascript; charset=utf-8",
		scriptCharset: "utf-8",
		dataType: "jsonp",
		crossdomain: true,
		success: function (result) {
			if (result) {
				if (result.length > 0) {
					var template = Handlebars.compile($('#tmpl_zipcode_address').html());
					$("#zip_lst_ul").html(template({ zipList: result }));
					$(".noti").show();
					iFrameScroll('fixed_scroll_addr2'); return false;
				}
				else {
					$(".noti").hide();
					$("#zip_lst_ul").html("");
				}
			} else {
				alert("주소를 검색하는 중에 오류가 발생했습니다.");
			}
		},
		error: function (result) {
			alert("주소를 검색하는 중에 오류가 발생했습니다.");
		}
	});

}

var selectZipCode = function (zipCd, zipAddr) {
    if (zipCd != null && zipAddr != null) {
        var ipt_sec = $(".new_addr_bx .ipt_sec")
		, ipt_zip = $(".ser_zip");
        $(ipt_sec).removeClass("none");
        $(ipt_zip).removeClass("on"); //addClass("none");
        $("#txtZipCodeFront").val(zipCd.substring(0, 3));
        $("#txtZipCodeBack").val(zipCd.substring(4, 7));
        $("#txtBasicAddr").val(zipAddr);
    }
}

var setMyHomePlusBranchWithNewAddr = function () {
	var inputList = $(".new_addr_bx .ipt_sec").find('input');
	var param;
	if (inputList != null && inputList.length > 0 && inputList.length >= 8) {
		for (var i in inputList) {
			if (inputList[i].value == "") {
				alert("새로운 주소의 모든 입력란을 채워 주세요.");
				return false;
				break;
			}
		}

		if (inputList[1].value != inputList[1].value.replace(/[^0-9]/gi, "") ||
	inputList[2].value != inputList[2].value.replace(/[^0-9]/gi, "")) {
			alert("우편번호는 숫자만 입력 가능합니다.");
			return false;
		}

		if (inputList[5].value != inputList[5].value.replace(/[^0-9]/gi, "") ||
	inputList[6].value != inputList[6].value.replace(/[^0-9]/gi, "") ||
	inputList[7].value != inputList[7].value.replace(/[^0-9]/gi, "")) {
			alert("휴대폰번호는 숫자만 입력 가능합니다.");
			return false;
		}

		param = { "addrName": inputList[0].value,
			"addrZipCode": inputList[1].value + "-" + inputList[2].value,
			"addrFront": inputList[3].value,
			"addrBack": inputList[4].value,
			"addrHpNo": inputList[5].value + "-" + inputList[6].value + "-" + inputList[7].value
		};
	}

	$.ajax({
		url: mobileWebUrlSecure + "/HomePlus/SetMyHomePlusBranchWithNewAddr",
		type: "GET",
		data: param,
		contentType: "application/javascript; charset=utf-8",
		scriptCharset: "utf-8",
		dataType: "jsonp",
		crossdomain: true,
		success: function (result) {
			if (result.success) {
				window.location.reload(true);
			} else {
				if (result.message != undefined && result.message != null) {
					alert(result.message);
				} else {
					alert("홈플러스 지점을 저장하는 중에 오류가 발생했습니다.");
				}
			}
		},
		error: function (result) {
			alert("홈플러스 지점을 저장하는 중에 오류가 발생했습니다.");
		}
	});
}

var editMyHomePlusBranch = function () {
	//장바구니 홈플러스 상품 chk
	popBlockUI('hplus_dtime');
	getMyAddrList();
}

var addCart = function () {

    var itemNo = $("#hidItemNoToCart").val();
    var orderQty = $("#count").val();
    var isInstantOrder = false;

    if (itemNo == "") {
        alert("상품번호가 잘못되었습니다.");
        return false;
    } else if (orderQty <= 0) {
        alert("상품 수량이 잘못되었습니다.");
        return false;
    } else if (orderQty > HomeplusAddCart.orderLimitCnt) {
        alert("선택하신 상품의 구매 가능한 수량은" + HomeplusAddCart.orderLimitCnt + "개 입니다.\n수량을 변경해 주세요.");
        return false;
    }

    var param = { itemNo: itemNo,
        orderQty: orderQty,
        isInstantOrder: isInstantOrder
    };

    $.ajax({
        url: mobileWebUrl + "/HomePlus/GetAddCartResult"
		, type: 'POST'
		, async: false
		, data: param
		, success: function (result) {
		    if (result != null) {
		        if (result.ReturnCode == "000") {  // 장바구니 저장성공
		            popBlockUI('hplus_cart');
		            //getCartList();
		            //alert("장바구니에 담겼습니다.");
		            hplusCartEnd('hplus_cart_end');

		        } else if (result.ReturnCode == "816") {
		            alert("장바구니에 다른 배송주소의 상품이 담겨있어서 함께 담을 수 없습니다. 배송주소를 확인해주세요.");
		        }
		    }
		    else {
		        alert("홈플러스 상품을 장바구니 저장하는 중에 오류가 발생했습니다.");
		    }
		}
		, error: function (result) {
		    alert("홈플러스 상품을 장바구니 저장하는 중에 오류가 발생했습니다.");
		}
    });
}

var getCartList = function () {
	$.ajax({
		url: mobileWebUrl + "/HomePlus/GetCartResult"
		, type: 'POST'
		, async: false
		//, data: { "branchCd": branchCd }
		, success: function (result) {
			if (result.success) {  // 성공
				var template = Handlebars.compile($('#tmpl_homeplus_cartlist').html());
				if (result.cartlist.length > 0) {
					$("#cal_lst_ul").html(template({ cartList: result.cartlist }));
				} else {
					var emptyString = '<p style="margin-top:100px;font-weight:normal;font-size:12px;text-align:center;">홈플러스 카트에 담긴 상품이 없습니다.</p>';
					$("#scroll_cal").html(emptyString);
				}

				//합계, 배송비 계산
				var totalPrice = 0;
				var deliveryPrice = 0;
				for (var i in result.cartlist) {
					totalPrice += result.cartlist[i].dblPrice;
					if (deliveryPrice == 0 && result.cartlist[i] != null && result.cartlist[i].dblShippingCost > 0) {
						deliveryPrice = result.cartlist[i].dblShippingCost;
					}
				}
				$("#hplus_cal .total_bx .sum").html(format(totalPrice, 'abc', 3));
				$("#hplus_cal .total_bx .add_tex em").html(format(deliveryPrice, 'abc', 3));


				if (totalPrice == 0) {
					$("#hplus_cal .total_bx .sum").remove();
					$("#hplus_cal .total_bx .add_tex").remove();
					$("#hplus_cal .total_bx em").remove()
				}

				if (result.cartlist.length > 0) {
					$(".sp_hplus.ico_cal").addClass("on");
				} else {
					$(".sp_hplus.ico_cal").removeClass("on");
				}

				popBlockUI('hplus_cal'); iFrameScroll('scroll_cal'); return false;
			}
			else {
				alert("장바구니 목록을 가져오는 중에 오류가 발생했습니다.");
			}
		}
		, error: function (result) {
			alert("장바구니 목록을 가져오는 중에 오류가 발생했습니다.");
		}
	});
}

var removeCartItem = function (orderIdx, ItemLinkUrl) {
	$.ajax({
		url: mobileWebUrl + "/HomePlus/GetRemoveCartResult"
		, type: 'POST'
		, async: false
		, data: { "orderIdx": orderIdx }
		, success: function (result) {
			if (result.success) {  // 성공

				if (result.cartlist.length > 0) {
					$(".sp_hplus.ico_cal").addClass("on");
					//$("#scroll_cal").html(template({ cartList: result.cartlist }));
					var isremoved = true;
					for (var i = 0; i < result.cartlist.length; i++) {
						if (orderIdx == result.cartlist[i].OrderIdx) {
							isremoved = false;
							break;
						}
					}

					if (isremoved) {
						var removeItemId = "li#" + orderIdx;
						if ($(removeItemId).length > 0) $(removeItemId).remove();
					}

				} else {
					$(".sp_hplus.ico_cal").removeClass("on");
					var emptyString = '<p style="margin-top:100px;font-weight:normal;font-size:12px;text-align:center;">홈플러스 카트에 담긴 상품이 없습니다.</p>';
					$("#scroll_cal").html(emptyString);
				}


				//합계, 배송비 계산
				var totalPrice = 0;
				var deliveryPrice = "3,000";
				for (var i in result.cartlist) {
					totalPrice += result.cartlist[i].dblPrice;
				}
				if (totalPrice >= 30000 || totalPrice == 0) deliveryPrice = 0;
				$("#hplus_cal .total_bx .sum").html(format(totalPrice, 'abc', 3));
				$("#hplus_cal .total_bx .add_tex em").html(deliveryPrice);

				if (totalPrice == 0) {
					$("#hplus_cal .total_bx .sum").remove();
					$("#hplus_cal .total_bx .add_tex").remove();
					$("#hplus_cal .total_bx em").remove()
				}

				if (ItemLinkUrl != undefined && ItemLinkUrl != null) {
					window.location(ItemLinkUrl);
				}
			}
			else {
				alert("홈플러스 상품을 삭제하는 중에 오류가 발생했습니다.");
			}
		}
		, error: function (result) {
			alert("홈플러스 상품을 삭제하는 중에 오류가 발생했습니다.");
		}
	});
}

this.HomeplusAddCart = {};
HomeplusAddCart.minBuyCnt = 1;
HomeplusAddCart.buyUnitCnt = 1;
HomeplusAddCart.calculatedMinCnt = 1;
HomeplusAddCart.salePrice = 0;
HomeplusAddCart.orderLimitCnt = 0;

var getMinBuyCnt = function (itemNo, strPrice) {

    $.ajax({
        url: mobileWebUrl + "/HomePlus/GetItemMinUnitBuyCount"
		, type: 'GET'
		, async: false
		, data: { itemNo: itemNo }
		, success: function (result) {
		    if (result.success) {
		        HomeplusAddCart.minBuyCnt = result.itemcntdata.MinBuyCnt;
		        HomeplusAddCart.buyUnitCnt = result.itemcntdata.BuyUnitCnt;
		        HomeplusAddCart.orderLimitCnt = result.itemcntdata.ItemOrderLimitInfo.OrderLimitCnt;

		        if (HomeplusAddCart.minBuyCnt <= HomeplusAddCart.buyUnitCnt)
		            HomeplusAddCart.calculatedMinCnt = HomeplusAddCart.buyUnitCnt;
		        else
		            if (HomeplusAddCart.buyUnitCnt == 1) {
		                HomeplusAddCart.calculatedMinCnt = HomeplusAddCart.minBuyCnt;
		            }
		            else {
		                HomeplusAddCart.calculatedMinCnt = HomeplusAddCart.buyUnitCnt * 2;
		            }

		        HomeplusAddCart.salePrice = parseFloat(strPrice.replace(/[^0-9-.]/g, ''));
		        HomeplusAddCart.render();
		    }
		    else {
		        HomeplusAddCart.salePrice = parseFloat(strPrice.replace(/[^0-9-.]/g, ''));
		        HomeplusAddCart.render();
		    }
		}
		, error: function (result) {
		    HomeplusAddCart.salePrice = parseFloat(strPrice.replace(/[^0-9-.]/g, ''));
		    HomeplusAddCart.render();
		}
    });

    if (HomeplusAddCart.buyUnitCnt > 1) {
        $("#count").prop("readonly", true);
    }
    else {
        $("#count").prop("readonly", false);
    }
}

HomeplusAddCart.render = function () {
	$("#hplus_cart").find("#count").val(HomeplusAddCart.calculatedMinCnt);
	HomeplusAddCart.addCartTotalSum();
}

HomeplusAddCart.addOrderQty = function (type) {
    var qty = parseInt($("#hplus_cart").find("#count").val()) + ((type == "up") ? HomeplusAddCart.buyUnitCnt : -HomeplusAddCart.buyUnitCnt);

    if (qty <= HomeplusAddCart.calculatedMinCnt)
        qty = HomeplusAddCart.calculatedMinCnt;

    if (qty > HomeplusAddCart.orderLimitCnt) {
        alert("선택하신 상품의 구매 가능한 수량은" + HomeplusAddCart.orderLimitCnt + "개 입니다.\n수량을 변경해 주세요.");
        qty = HomeplusAddCart.orderLimitCnt;
    }

    $("#hplus_cart").find("#count").val(qty);
    HomeplusAddCart.addCartTotalSum();
}

HomeplusAddCart.numberCheck = function () {
	var bRet = true;
	var qty = $("#hplus_cart").find("#count").val();
	
	if (isNaN(qty)) {
		alert("숫자만 입력이 가능합니다.");
		$("#hplus_cart").find("#count").val(HomeplusAddCart.calculatedMinCnt);
		bRet = false;
	}
	else {
		if (qty <= HomeplusAddCart.calculatedMinCnt) {
			qty = HomeplusAddCart.calculatedMinCnt;
		}
		else {
			var left = qty % HomeplusAddCart.buyUnitCnt;
			if (left != 0) qty = qty - left;
		}
		$("#hplus_cart").find("#count").val(qty);
		bRet = true;
	}

	HomeplusAddCart.addCartTotalSum();
	return bRet;
}

HomeplusAddCart.addCartTotalSum = function () {
	var qty = $("#hplus_cart").find("#count").val();
	$("#hplus_cart").find('.sum').html(comma(HomeplusAddCart.salePrice * qty));
}

var comma = function (num) {
	var len, point, str;

	num = num + "";
	point = num.length % 3;
	len = num.length;

	str = num.substring(0, point);
	while (point < len) {
		if (str != "") str += ",";
		str += num.substring(point, point + 3);
		point += 3;
	}

	return str;
}


Handlebars.registerHelper('renderTimeTable', renderTimeTable);

function renderTimeTable(timetable, timeslot) {
	var timeTableHtml = "";
	var theadHtml = "<th>배송시간</th>";
	var tbodyHtmlArr = new Array(timeslot.length);
	for (var t = 0; t < timeslot.length; t++) {
		tbodyHtmlArr[t] = [];
		tbodyHtmlArr[t].push("<th><span>" + timeslot[t].StrStartHour + "</span> ~ <span>" + timeslot[t].StrEndHour  + "</span></th>");
	}

	if (timetable != null && timetable.length > 0) {
		if (timetable.length >= 4) timetable = timetable.slice(0, 4);//4일치
		for (var i = 0; i<timetable.length; i++) {

			theadHtml += "<th><span>" + timetable[i].StrDate + "</span> (" + timetable[i].StrDay + ")</th>"

			if (timetable[i].HolidayYn == 'Y') {
				//첫번째타임(8시) 에 휴무일추가
				tbodyHtmlArr[0][i + 1] = '<td class="out break" rowspan="13">휴무일</td>';			}
			else {
				if (timetable[i].DeliveryYnDict != null) {
					for (var t = 0; t < timeslot.length; t++) {
					    if (timetable[i].DeliveryYnDict[timeslot[t].StrStartHour] != undefined) {
					        var now = new Date();
					        
					        var deliApplyEndMillisec = timetable[i].DeliveryYnDict[timeslot[t].StrStartHour].DeliveryAplnEndDt; // /Date(1291548407008)/
					        var substringedDate = deliApplyEndMillisec.substring(6); //substringedDate= 1291548407008)/
					        var parsedIntDate = parseInt(substringedDate); //parsedIntDate= 1291548407008
					        var deliveryApplyEndDt = new Date(parsedIntDate);
					        var enableYn = timetable[i].DeliveryYnDict[timeslot[t].StrStartHour].DelvieryEnableYn;
					        var longManageYn = timetable[i].DeliveryYnDict[timeslot[t].StrStartHour].LongManageYn;
					        var longDeliveryEnableYn = timetable[i].DeliveryYnDict[timeslot[t].StrStartHour].LongDeliveryEnableYn;

					        if (enableYn == 'Y' && (now < deliveryApplyEndDt)) {
					            if (longManageYn == 'Y' && longDeliveryEnableYn == "N") {
					                tbodyHtmlArr[t][i + 1] = '<td class="out">마감</td>';
					            }
					            else {
					                tbodyHtmlArr[t][i + 1] = '<td>예약가능</td>';
					            }
					        } else {
					            tbodyHtmlArr[t][i + 1] = '<td class="out">마감</td>';
					        }
					    }
					    else {
					        tbodyHtmlArr[t][i + 1] = '<td class="out">마감</td>';
					    }
					}
				}
			}
		}
	}
	timeTableHtml += "<thead><tr>";
	timeTableHtml += theadHtml;
	timeTableHtml += "</tr></thead><tbody>";
	for (var i in tbodyHtmlArr) {
		timeTableHtml += "<tr>";
		for (var j in tbodyHtmlArr[i]) {
			timeTableHtml += tbodyHtmlArr[i][j];
		}
		timeTableHtml += "</tr>";
	}
	timeTableHtml += "</tbody>";

	return new Handlebars.SafeString(timeTableHtml);
}

var format = function (a, n, x) {
	var re = '(\\d)(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
	return a.toFixed(Math.max(0, ~ ~n)).replace(new RegExp(re, 'g'), '$1,');
};