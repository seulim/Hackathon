$(document).ready(function () {
	var router = new Grapnel();
	router.get('/:sort/:listType/:pageNo', function (req) {
		if (params.needPageListAjax) {
			params.sort = Number(req.params.sort);
			params.currentPageNo = Number(req.params.pageNo);

			getPageList(params.currentPageNo);
			$("#selListSort").val(params.sort);
		}
		params.currentListType = req.params.listType;
		change_listtype(req.params.listType);
	});

	updateHashTag(params.currentPageNo);

	$("#selListSort").bind('change', function () {
		params.sort = Number($("#selListSort").val());
		updateHashTag(params.currentPageNo);
		$(this).blur();
	});
});

function change_listtypeandupdate(type) {
	change_listtype(type);
	updateHashTag();
}

function change_listtype(type) {
	$('.viewType li').removeClass('on');

	var $listResult = $('#list_result');
	var $img = $listResult.find("img");

	var $imgSmall = $listResult.find(".small");
	var $imgBig = $listResult.find(".big");

	if (type == 'img') {
		$('#liViewTypeImage').addClass('on');
		$listResult.attr('class', 'type_img');

		$imgSmall.show();
		$imgBig.hide();
	}
	else if (type == 'big') {
		$('#liViewTypeBigImage').addClass('on');
		$listResult.attr('class', 'type_img_big');

		$imgBig.show();
		$imgSmall.hide();
	}
	else {
		$('#liViewTypeList').addClass('on');
		$listResult.attr('class', 'type_default');

		$imgSmall.show();
		$imgBig.hide();
	}

	$img.lazy({
		effect: "fadeIn",
		effectTime: 500,
		threshold: 350,
		attribute: "data-original",
		bind: "event",
		onError: imageOnErrorHandler
	});
	params.currentListType = type;

	$(window).trigger("scroll");
}

function imageOnErrorHandler($this) {
	if (params.currentListType == "big" && $this.attr("src") != $this.data("onerror")) {
		$this.attr("src", $this.data("onerror"));
	}
}

function fold_shopcatelist() {
	if ($('#link_shopcatelists').is('.on')) {
		$('#link_shopcatelists').removeClass('on');
		$('#shopcatelists').hide();
	} else {
		$('#link_shopcatelists').addClass('on');
		$('#shopcatelists').show();
	}
}

function GoPrev(prev_category) {
	if (prev_category != null && prev_category != 0 && prev_category != "") {
		window.location.href = '/Shop/' + params.shopAlias + '/List?category=' + prev_category + '&prevcate=' + prev_category;
	}
	else {
		history.back();
	}
}

function OpenCloseMoreCategory(isOpen) {
	if (isOpen == true) {
		$(".cate_list > li").show();
		$("#srpViewMoreCategory").hide();
		$("#srpViewCloseCategory").show();
	}
	else {
		$(".cate_list > li:gt(4)").hide();
		$("#srpViewMoreCategory").show();
		$("#srpViewCloseCategory").hide();
	}
}

var getPageList = function (pageNo) {
	if (pageNo < 1 || pageNo > params.lastPage) {
		return false;
	}
	$.ajax({
		type: "POST",
		url: "/Shop/" + params.shopAlias + "/ListPost",
		dataType: "json",
		data: {
			category: params.category,
			isshop: params.isshop,
			keyword: params.keyword,
			pageNo: pageNo,
			pageSize: params.pageSize,
			sort: params.sort,
			alias: params.alias
		},
		error: function (request, status, error) {
			alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
		},
		success: function (result) {
			var listTemplate = Handlebars.compile($("#list_result_template").html());
			$("#list_result").empty().append(listTemplate(result));

			$("#list_result").find("._favoriteFunc").on("click", function (e) {
				e.stopPropagation(); e.preventDefault();
				var goodscode = $(this).attr("data-goodscode");
				$.ajax({
					type: "GET",
					url: "/Shop/" + Const.ALIAS + "/AddFavoriteItem",
					dataType: "json",
					data: {
						itemNo: goodscode
					},
					error: function (request, status, error) {
						alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
					},
					success: function (result) {
						if (result != null && result != undefined) {
							alert(result.msg);

							if (result.result != null && result.result == -100) {
								location.href = (Const && Const.LOGIN_URL) ? Const.LOGIN_URL : "http://m.gmarket.co.kr/Login/login_mw.asp";
							}
						}
					}
				});
			});

			$(".lazy_page_" + pageNo + " img").lazy({
				effect: "fadeIn",
				effectTime: 500,
				threshold: 350,
				attribute: "data-original",
				bind: "event",
				onError: imageOnErrorHandler
			});

			params.currentPageNo = pageNo;

			paging();
		}
	});
}

var paging = function () {
	$('div.paging').find('span.pg_pages').find('a').remove();
	var start, last, halfSize = Math.floor(PAGENATION_SIZE / 2);
	if (params.lastPage < PAGENATION_SIZE) {
		start = 1;
		last = params.lastPage;
	}
	else if (params.currentPageNo - halfSize < 1) {
		start = 1;
		last = PAGENATION_SIZE;
	}
	else if (params.currentPageNo + halfSize > params.lastPage) {
		start = params.lastPage - PAGENATION_SIZE + 1;
		last = params.lastPage;
	}
	else {
		start = params.currentPageNo - halfSize;
		last = params.currentPageNo + halfSize;
	}
	for (var i = start; i <= last; i++) {
		var $a = $('<a/>', {
			href: 'javascript:;'
			, text: i
		});
		(function (pageNo) {
			$a.bind('click', function (event) {
				return getPageListAndGoTop(pageNo);
			}).appendTo('div.paging span.pg_pages');
		})(i);
		if (i === params.currentPageNo) {
			$a.addClass('current');
		}
	}
}

function updateHashTag(pageNo) {
	if (pageNo) {
		params.needPageListAjax = true;
	}
	else {
		params.needPageListAjax = false;
	}
	location.hash = "#/" + params.sort + "/" + params.currentListType + "/" + (pageNo || params.currentPageNo);
}

var getPageListAndGoTop = function (pageNo) {
	if (pageNo !== params.currentPageNo
		&& pageNo >= 1
		&& pageNo <= params.lastPage
		) {
		updateHashTag(pageNo);
		$('html, body').animate({ scrollTop: $('div.prod_list_view_type').offset().top }, "fast");
	};
	return false;
}