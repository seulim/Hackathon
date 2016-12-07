var NoticeListController = function () {
	this.noticeListMoreController = new NoticeListMoreController(Const.NOTICE_COUNT, Const.MORE_NOTICE_LIST_URL, Const.MORE_NOTICE_LIST_PARAM, 10, "#show_more", "#more_count", "#no_more_notice", "#notice_wrapper", "#noticelist_template");
};



var NoticeListMoreController = function (totalCount, url, param, pageSize, moreButtonSelector, moreCountSelector, noMoreMessageSelector, wrapperSelector, templateSelector) {
	this.totalCount = -1;
	this.pageNo = 1;
	this.pageSize = 10;
	this.$moreButton = $();
	this.$wrapper = $();
	this.$moreCount = $();
	this.$noMoreMessage = $();
	this.template = null;
	this.isInProgress = false;

	if (totalCount) {
		this.totalCount = totalCount;
	}

	if (pageSize) {
		this.pageSize = pageSize;
	}

	if (moreButtonSelector) {
		this.$moreButton = $(moreButtonSelector);
	}

	if (wrapperSelector) {
		this.$wrapper = $(wrapperSelector);
	}

	if (templateSelector) {
		if ($(templateSelector).length <= 0) {
			return null;
		} else {
			this.template = Handlebars.compile($(templateSelector).html());	
		}
	}

	if (moreCountSelector) {
		this.$moreCount = $(moreCountSelector);
	}

	if (noMoreMessageSelector) {
		this.$noMoreMessage = $(noMoreMessageSelector);
	}

	var that = this;
	this.renderMore = function () {
		if (false == this.isInProgress) {
			if (this.pageNo < this.totalCount) {
				param.pageNo = ++this.pageNo;
				param.pageSize = this.pageSize;

				this.isInProgress = true;

				$.ajax({
					url: url,
					type: "POST",
					data: param,
					success: function (result) {
						if (result.result) {
							if (result.noticeList.length > 0) {
								that.$wrapper.append(that.template({ list: result.noticeList }));

								// count 업데이트
								var leftCount = that.totalCount - (that.pageNo * that.pageSize);
								if (leftCount > that.pageSize) {
									leftCount = that.pageSize;
								} else if (leftCount <= 0) {
									that.$moreButton.hide();
									that.$noMoreMessage.show();
								}
								that.$moreCount.html(leftCount);
							} else {
								that.$moreButton.hide();
								that.$noMoreMessage.show();
							}
						} else {
							alert("공지사항을 가져오던 중에 오류가 발생했습니다.");	
							
						}

						that.isInProgress = false;
					},
					error: function (result) {
						alert("공지사항을 가져오던 중에 오류가 발생했습니다.");
						that.isInProgress = false;
					}
				});
			}
		} else {
			alert("공지사항을 불러오는 중입니다. 잠시만 기다려주세요.");
		}
	};

	this.$moreButton.on("click", function () { that.renderMore(); });
};