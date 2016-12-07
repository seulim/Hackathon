(function (GMobile, $, SwipeView, Handlebars, undefined) {
	(function (Look, undefined) {
		$("#content").on("click", "a, button", function () {
			var fcd = $(this).data("fcd");
			
			if (fcd) {
				SnaUrlFooter(fcd);
			}
		});

		function typeHelper(type1, type2, type3) {
			return function (item, opts) {
				if (typeof item === "object" && typeof item.Type === "string" &&
					(item.Type.toUpperCase() == type1 || item.Type.toUpperCase() == type2 || item.Type.toUpperCase() == type3)) {
					return opts.fn(this);
				} else {
					return opts.inverse(this);
				}
			};
		};

		Handlebars.registerHelper("ifTypeA", typeHelper("A","A2","A3"));
		Handlebars.registerHelper("ifTypeB", typeHelper("B1", "B2"));

		function eachHelper(startIndex, endIndex) {
			return function (array, opts) {
				if (typeof array === "object" && array.length > endIndex) {
					var ret = "";

					for (var i = startIndex; i <= endIndex; i++) {
						ret = ret + opts.fn(array[i]);
					}

					return ret;
				}
			};
		};

		Handlebars.registerHelper("eachBTop", eachHelper(0, 2));
		Handlebars.registerHelper("eachBBottom", eachHelper(3, 4));

		Handlebars.registerHelper("lookItemType", function (type) {
			if (type == "B1") {
				return "type1";
			} else if (type == "B2") {
				return "type2";
			} else if (type == "C") {
				return "type3";
			} else if (type == "D") {
				return "type4";
			}
		});

		Handlebars.registerPartial("PartialLookItem", $("#tmpl_partial_look_item").html());

		Handlebars.registerHelper("getSnaUrl", function (landingUrl, fcd) {
			return "http://sna.gmarket.co.kr/?fcd=" + fcd + "&url=" + encodeURIComponent(landingUrl);
		});

		Handlebars.registerHelper("getSnaUrlForItem", function (landingUrl, logicType, fcdForManual, fcdForAuto) {
			var fcd;

			if (logicType == "Manual") {
				fcd = fcdForManual;
			} else if (logicType == "Auto") {
				fcd = fcdForAuto;
			} else {
				return landingUrl;
			}

			return "http://sna.gmarket.co.kr/?fcd=" + fcd + "&url=" + encodeURIComponent(landingUrl);
		});




		Look.ItemRenderController = function (wrapperSelector, templateSelector, url) {
			this.$wrapper = $(wrapperSelector);
			this.template = Handlebars.compile($(templateSelector).html());
			this.url = url;

			this.$moreButton = $("#btn_more");
			this.$loadingIcon = $("#ico_loading");

			this.isRendering = false;
		};

		Look.ItemRenderController.prototype.render = function (data, append, callback) {
			var that = this;

			if (!this.isRendering) {
				if (!append) {
					this.$wrapper.html("");
					that.$moreButton.hide();
				}

				this.$loadingIcon.show();
				this.isRendering = true;

				$.ajax({
					url: this.url,
					data: data,
					type: "POST",
					success: function (data) {
						that.$wrapper.append(that.template(data));
						
						if (data.Paging.HasNext) {
							that.$moreButton.show();
						} else {
							that.$moreButton.hide();
						}

						that.$loadingIcon.hide();
						that.isRendering = false;

						if (typeof callback === "function") {
							callback.apply();
						}
					},
					error: function (e) {
						that.$loadingIcon.hide();
						that.isRendering = false;

						if (typeof callback === "function") {
							callback.apply();
						}
					}
				});
			}
		};





		Look.CategoryRenderController = function (wrapperSelector, templateSelector) {
			var that = this;

			this.$wrapper = $(wrapperSelector);
			this.template = Handlebars.compile($(templateSelector).html());

			this.iScroll = null;

			this.$scrollWrapper = $("#supdTabCnt");

			this.$prevButton = $("#category_prev");
			this.$nextButton = $("#category_next");

			this.$prevButton.on("click", function (e) {
				if (that.iScroll && false == $(this).hasClass("off")) {
					that.iScroll.scrollToPage("prev", 0);
				}
			});

			this.$nextButton.on("click", function (e) {
				if (that.iScroll && false == $(this).hasClass("off")) {
					that.iScroll.scrollToPage("next", 0);
				}
			});

			this.refresh = function () {
				var $li = this.$wrapper.find("li");
				var ulWidth = $li.width() * $li.length;

				this.$wrapper.width(ulWidth);

				if (ulWidth <= this.$scrollWrapper.width()) {
					this.$prevButton.addClass("off");
					this.$nextButton.addClass("off");
				}

				if (this.iScroll) {
					this.iScroll.refresh();
				}
			};

			this.$wrapper.on("click", "a", function (e) {
				e.preventDefault();

				SnaUrlFooter("705500001");

				if (currentGdLcCd != $(this).data("category")) {
					that.$scrollWrapper.find("li").removeClass("on");
					$(this).parent().addClass("on");

					currentPageNo = 1;
					currentGdLcCd = $(this).data("category");

					renderItem(false);
				}
			});

			$(window).on("resize", function () {
				if (that.iScroll) {
					that.refresh();
				
					var selectedIndex = that.$wrapper.find(".on").index();
					if (selectedIndex >= 0) {
						that.iScroll.scrollToPage(selectedIndex, 0, 0);
					} else {
						that.iScroll.scrollTo(0, 0, 0);
					}
				}
			});
		};

		Look.CategoryRenderController.prototype.render = function (categoryList) {
			var that = this;

			this.$wrapper.html(this.template(categoryList));

			if (!this.iScroll) {
				this.iScroll = new iScroll(this.$scrollWrapper[0], {
					hScrollbar: false,
					vScrollbar: false,
					useTransform: false,
					snap: 'li',
					onScrollEnd: function () {
						if (that.$wrapper.position().left < 0) {
							that.$prevButton.removeClass("off");
						} else {
							that.$prevButton.addClass("off");
						}

						if (that.$scrollWrapper.width() + Math.abs(that.$wrapper.position().left) < that.$wrapper.width()) {
							that.$nextButton.removeClass("off");
						} else {
							that.$nextButton.addClass("off");					
						}
					}
				});
			}

			this.refresh();
			this.iScroll.scrollTo(0, 0, 0);
		}




		Look.SwipeBannerController = function () {
			var that = this;

			this.data = Look.Data.ContentsSection.ContentsList;

			if (this.data.length > 0) {
				this.swipeView = new SwipeView("#bnrList", {
					numberOfPages: this.data.length
				});

				$("#hotPrev").on("click", function (e) {
					e.preventDefault();

					that.swipeView.prev();
				});

				$("#hotNext").on("click", function (e) {
					e.preventDefault();

					that.swipeView.next();
				});

				var template = Handlebars.compile($("#tmpl_contents").html());

				//var $title = $("#contents_title");
				//var $groupName = $("#contents_groupname");
				var $pageNo = $("#contents_pageno");

				function setTitleAndPageNo(index) {
					var data = that.data[index];

					//$title.html(data.Title);
					//$groupName.html(data.GroupName);
					$pageNo.html(index + 1);
				}

				var $wrapper = $("#bnrList");
				var $swipeViewSlider = $("#swipeview-slider");

				$(window).load(function () {
					changeWrapperHeight();
				});

				$(window).on("resize", function () {
					changeWrapperHeight();
				});

				function changeWrapperHeight() {
					var $bannerImage = $wrapper.find("img");
					var height = $bannerImage.height();//이미지높이값
					$wrapper.height(height);//래퍼높이값 조절

					//ios7에서 높이값 잡지 못하는 버그	
					$swipeViewSlider.children().height(height);
				}

				for (var i = 0; i < 3; i++) {
					var page = i == 0 ? this.data.length - 1 : this.data.length == 1 ? 0 : i - 1;

					this.swipeView.masterPages[i].innerHTML = template(this.data[page]);
				}

				this.swipeView.onFlip(function () {
					for (var i = 0; i < 3; i++) {
						var upcoming = that.swipeView.masterPages[i].dataset.upcomingPageIndex;

						if (upcoming != that.swipeView.masterPages[i].dataset.pageIndex) {
							that.swipeView.masterPages[i].innerHTML = template(that.data[upcoming]);
						}
					}

					setTitleAndPageNo(that.swipeView.pageIndex);
				});

				var randomIndex = Math.floor(Math.random() * this.data.length) % this.data.length;
				this.swipeView.goToPage(randomIndex);
				setTitleAndPageNo(randomIndex);
			}
		};





		var $tabWrapper = $("#tab_wrapper");
		var $categorySection = $("#category_section");

		$tabWrapper.on("click", "a", function () {
			$tabWrapper.find("li").removeClass("selected");
			$(this).parent().addClass("selected");
		});

		Look.itemRenderController = new Look.ItemRenderController("#item_list_wrapper", "#tmpl_look_item", "/Look/GetLookSectionItem");
		Look.categoryRenderController = new Look.CategoryRenderController("#category_list_wrapper", "#tmpl_look_category");
		Look.swipeBannerController = new Look.SwipeBannerController();

		var currentGroupNo;
		var currentPageNo;
		var currentGdLcCd;

		function renderItem(append) {
			var data = {
				groupNo: currentGroupNo,
				pageNo: currentPageNo
			}

			if (currentGdLcCd) {
				data.gdLcCd = currentGdLcCd;
			}

			Look.itemRenderController.render(data, append, function () {
				currentPageNo++;
				$moreButton.on("click", moreButtonClickCallback);
			});
		}

		function renderGroup(groupIndex) {
			var thisGroup = Look.Data.LookSection.GroupList[groupIndex];

			if (thisGroup.CategoryList.length > 0) {
				$categorySection.show();

				Look.categoryRenderController.render(thisGroup.CategoryList);
			} else {
				$categorySection.hide();				
			}

			if (currentGroupNo != thisGroup.GroupNo || currentGdLcCd != "") {
				currentGroupNo = thisGroup.GroupNo;
				currentPageNo = 1;
				currentGdLcCd = "";

				renderItem(false);
			}
		}

		var $moreButton = $("#btn_more");
		function moreButtonClickCallback() {
			$moreButton.off("click", moreButtonClickCallback);

			renderItem(true);
		}
		$moreButton.on("click", moreButtonClickCallback);

		$("#tab_the_look").on("click", function (e) {
			e.preventDefault();

			renderGroup(0);
		});

		$("#tab_trendy_daily").on("click", function (e) {
			e.preventDefault();

			renderGroup(1);
		});

		$("#tab_basic_missy").on("click", function (e) {
			e.preventDefault();

			renderGroup(2);
		});

		$("#tab_spa").on("click", function (e) {
			e.preventDefault();

			renderGroup(3);
		});





		$(function () {
			renderGroup(0);
		});
	})(GMobile.Look = GMobile.Look || {});
})(window.GMobile = window.GMobile || {}, jQuery, SwipeView, Handlebars);