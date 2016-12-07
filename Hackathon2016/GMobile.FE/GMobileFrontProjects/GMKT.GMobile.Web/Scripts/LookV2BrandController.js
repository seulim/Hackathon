(function (GMobile, $, Handlebars, undefined) {
	GMobile.FromWhereType = {
		AndroidApp: 1,
		iOSApp: 2,
		Web: 3
	};

	GMobile.UserAgent = function () {
		if (GMobile.UserAgent.prototype._singletonInstance) {
			return GMobile.UserAgent.prototype._singletonInstance;
		}

		GMobile.UserAgent.prototype._singletonInstance = this;

		var regex = new RegExp("MobileApp/(.+)\s*\\((.+)\s*;\s*(.+)\s*;\s*(.+)\s*\\)");

		var indexOfSchemaVersion = 1;
		var indexOfOS = 2;
		var indexOfVersion = 3;
		var indexOfIdentifier = 4;

		this.getFromWhere = function () {
			if (typeof navigator !== "undefined" && navigator != null && typeof navigator.userAgent === "string") {
				var matches = regex.exec(navigator.userAgent);

				if (matches != null && matches.length > indexOfOS) {
					var os = matches[indexOfOS];

					if (os !== undefined) {
						os = os.trim().toLowerCase();

						if (os === "android") {
							return GMobile.FromWhereType.AndroidApp;
						} else if (os === "ios") {
							return GMobile.FromWhereType.iOSApp;
						}
					}
				}
			}
			return GMobile.FromWhereType.Web;
		};
	};

	GMobile.UserAgent.getInstance = function () {
		return new GMobile.UserAgent();
	};

	GMobile.fromWhere = GMobile.UserAgent.getInstance().getFromWhere();
	GMobile.isApp = (GMobile.fromWhere == GMobile.FromWhereType.AndroidApp || GMobile.fromWhere == GMobile.FromWhereType.iOSApp);
	if (GMobile.isApp) {
		$("a.btn_top_main").remove();
	}


	(function (Look, undefined) {
		Handlebars.registerHelper("ifBrandGalleryExposeTypeBanner", function (exposeType, opts) {
			if (exposeType == "Banner") {
				return opts.fn(this);
			}
			else {
				return opts.inverse(this);
			}
		});


		Handlebars.registerHelper("getSnaUrl", function (landingUrl, fcd) {
			return "http://sna.gmarket.co.kr/fcd=" + fcd + "&url=" + encodeURIComponent(landingUrl);
		});

		Look.BrandCategoryCode = "ALL";
		Look.BrandBrandNo = 0;
		Look.BrandPageNo = 1;

		//테스트
		Look.BrandPageSize = 10;
		//

		Look.BrandController = function (wrapperId, templateId, hideWrapperId) {
			this.$wrapper = $(wrapperId);
			this.$hideWrapper = $(hideWrapperId);
			this.$template = Handlebars.compile($(templateId).html());
		};

		Look.BrandController.prototype.render = function (data) {
			var that = this;
			$("#btnBrandBestMore").hide();
			that.$hideWrapper.hide();
			that.$wrapper.show();
			that.$wrapper.html("");

			if (this == Look.brandGalleryController) {
				$("#btnFashionBrandBest").removeAttr("class");
				$("#btnFashionBrandGallery").attr("class", "on");
				$("#BrandCategory").hide();
			}
			else if (this == Look.brandBestController) {
				$("#btnFashionBrandGallery").removeAttr("class");
				$("#btnFashionBrandBest").attr("class", "on");
			}

			$.ajax({
				url: "/Look/GetLookV2BrandGallery",
				data: data,
				type: "GET",
				success: function (data) {
					that.$wrapper.append(that.$template(data));
					Look.BrandPageNo = data.BestPaging.CurrentPageNo;
					if (that == Look.brandBestController && data.BestPaging.HasNext) {
						$("#btnBrandBestMore").show();
					}
				},
				error: function (e) {

				}
			});
		};

		Look.BrandCategoryController = function () {
			this.$wrapper = $("#BrandCategory");
			this.$template = Handlebars.compile($("#brand_category_template").html());
		};

		Look.BrandCategoryController.prototype.render = function (data) {
			var that = this;
			that.$wrapper.show();
			that.$wrapper.html("");
			Look.BrandCategoryCode = "ALL";
			$.ajax({
				url: "/Look/GetLookV2BrandGallery",
				data: data,
				type: "GET",
				success: function (data) {
					that.$wrapper.append(that.$template(data));
					$('.look_cate.brand li a').removeAttr("class");
					$('.look_cate.brand li a[value="' + Look.BrandCategoryCode + '"]').attr("class", "on");
				},
				error: function (e) {

				}
			});
		}

		Look.BrandBestDetailController = function () {
			this.$wrapper = $("#BrandBestDetail");
			this.$template = Handlebars.compile($("#brand_best_template").html());
		}

		Look.BrandBestDetailController.prototype.render = function (data, isByMoreBtn) {
			var that = this;
			if (!isByMoreBtn) {
				that.$wrapper.html("");
			}
			$("#btnBrandBestMore").hide();
			$.ajax({
				url: "/Look/GetLookV2BrandBest",
				data: data,
				type: "GET",
				success: function (data) {
					that.$wrapper.append(that.$template(data));
					Look.BrandPageNo = data.Paging.CurrentPageNo;
					if (data.Paging.HasNext) {
						$("#btnBrandBestMore").show();
					}
					else {
						$("#btnBrandBestMore").hide();
					}
				},
				error: function (e) {

				}
			});
		}



		Look.brandGalleryController = new Look.BrandController("#BrandGalleryDetail", "#brand_gallery_template", "#BrandBestDetail");
		Look.brandBestController = new Look.BrandController("#BrandBestDetail", "#brand_best_template", "#BrandGalleryDetail");
		Look.brandCategoryController = new Look.BrandCategoryController();
		Look.brandBestDetailController = new Look.BrandBestDetailController();

		Look.brandCategoryController.IsAlreadyRendered = false;

		$("#btnFashionBrandGallery").on("click", function (e) {
			e.preventDefault();
			Look.brandGalleryController.render({ pageType: Look.BrandPageType });
		});

		$("#btnFashionBrandBest").on("click", function (e) {
			e.preventDefault();
			if (!Look.brandCategoryController.IsAlreadyRendered) {
				Look.brandCategoryController.render({ pageType: Look.BrandPageType });
				Look.brandCategoryController.IsAlreadyRendered = true;
			}
			else {
				Look.BrandCategoryCode = "ALL";
				Look.brandCategoryController.$wrapper.show();
				$('.look_cate.brand li a').removeAttr("class");
				$('.look_cate.brand li a[value="' + Look.BrandCategoryCode + '"]').attr("class", "on");
			}
			Look.brandBestController.render({ pageType: Look.BrandPageType });
		});

		$(".look_cate.brand li a").live("click", function (e) {
			e.preventDefault();
			Look.BrandCategoryCode = $(e.currentTarget).attr("value");
			$('.look_cate.brand li a').removeAttr("class");
			$('.look_cate.brand li a[value="' + Look.BrandCategoryCode + '"]').attr("class", "on");
			Look.brandBestDetailController.render({
				pageType: Look.BrandPageType,
				categoryType: "Brand",
				lCategoryCode: Look.BrandCategoryCode,
				brandNo: Look.BrandBrandNo,
				pageNo: 1,
				pageSize: Look.BrandPageSize
			}, false);
		});

		$("#btnBrandBestMore .btn_look_more").on("click", function (e) {
			e.preventDefault();
			Look.brandBestDetailController.render({
				pageType: Look.BrandPageType,
				categoryType: "Brand",
				lCategoryCode: Look.BrandCategoryCode,
				brandNo: Look.BrandBrandNo,
				pageNo: Look.BrandPageNo + 1,
				pageSize: Look.BrandPageSize
			}, true);
		});


		$(document).ready(function () {
			if (Look.BrandMenuType == "Gallery") {
				$("#btnFashionBrandGallery").trigger("click");
			}
			else if (Look.BrandMenuType == "Best") {
				$("#btnFashionBrandBest").trigger("click");
			}
		});
	})(GMobile.Look = GMobile.Look || {});
})(window.GMobile = window.GMobile || {}, jQuery, Handlebars);