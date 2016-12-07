function ListParameter() {
	this.category = category;
	this.isshop = isshop;
	this.keyword = keyword;
	this.pageNo = currentPageNo;
	this.pageSize = pageSize;
	this.sort = sort;
	this.alias = shopAlias;
	this.lastPage = Math.ceil(totalCount / pageSize);

	this.Init = function () {
		this.category = category;
		this.isshop = isshop;
		this.keyword = "";
		this.pageNo = currentPageNo;
		this.pageSize = pageSize;
		this.sort = 2;
		this.alias = shopAlias;
		this.lastPage = Math.ceil(totalCount / pageSize);
	}
}

$(document).ready(function () {
	$("#selListSort").val(sort);
	SetMorePageInfo();

	$("#selListSort").bind('change', function () {
		InitSearchInfo();
		listParams.sort = parseInt($("#selListSort").val());
		ViewMorePage();
	});

});

function change_listtype(type) {
	console.log($('.viewType li'));
	$('.viewType li').removeClass('on');

	if (type == 'img') {
		$('#liViewTypeImage').addClass('on');
		$('#list_result').attr('class', 'type_img');
	} else if (type == 'big') {
		$('#liViewTypeBigImage').addClass('on');
		$('#list_result').attr('class', 'type_img_big');
	} else {
		$('#liViewTypeList').addClass('on');
		$('#list_result').attr('class', 'type_default');
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

function InitSearchInfo() {
	currentPageNo = 0; // 처음페이지 뿌려줘야 하므로 1 이 아니고 0
	listParams.Init();
	$("#list_result").empty();
	$("#btnShopListViewMore").show();
	$("#ShopListNodata").hide();
}

function SetMorePageInfo() {
	$("#btnShopListViewMore em").text(pageSize);
	if (currentPageNo == listParams.lastPage) {
		$("#btnShopListViewMore").hide();
		$("#ShopListNodata").show();
	}

	if (currentPageNo == listParams.lastPage - 1) {
		var remainCount;
		remainCount = totalCount - (currentPageNo * pageSize);
		$("#btnShopListViewMore em").text(remainCount);
	}
}

function ViewMorePage() {
	if (currentPageNo == listParams.lastPage) {
		SetMorePageInfo();
		return false;
	}

	var listTemplate = Handlebars.compile($("#list_result_template").html());

	$.ajax({
		type: "POST",
		url: "/Shop/" + shopAlias + "/ListPost",
		dataType: "json",
		data: {
			category: listParams.category,
			isshop: listParams.isshop,
			keyword: listParams.key,
			pageNo: currentPageNo + 1,
			pageSize: listParams.pageSize,
			sort: listParams.sort,
			alias: listParams.alias
		},
		error: function (request, status, error) {
			alert('오류가 발생 했습니다. 잠시후 다시 시도해 주세요');
		},
		success: function (result) {
			console.log(result);
			currentPageNo = currentPageNo + 1;
			listParams.pageNo = currentPageNo;
			$("#list_result").append(listTemplate(result));
			$(".lazy_page_" + currentPageNo + " img").lazyload({
				effect: "fadeIn",
				threshold: 350
			});
			SetMorePageInfo();
		}
	});
}

function GoPrev(prev_category) {
	if (prev_category != null && prev_category != 0 && prev_category != "") {
		window.location.href = '/Shop/' + shopAlias + '/List?category=' + prev_category + '&prevcate=' + prev_category;
	}
	else {
		history.back();
	}
}