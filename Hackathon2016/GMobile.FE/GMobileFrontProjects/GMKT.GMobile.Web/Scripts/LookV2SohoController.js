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
		Handlebars.registerHelper("getSnaUrl", function (landingUrl, fcd) {
			return "http://sna.gmarket.co.kr/fcd=" + fcd + "&url=" + encodeURIComponent(landingUrl);
		});

		Look.SohoPageType = "UnKnown";
		Look.SohoCategoryCode = "ALL";
		Look.SohoBrandNo = 0;
		Look.SohoPageNo = 1;
		Look.SohoPageSize = 100;

		Look.SohoController = function () {
			this.$wrapper = $("#SohoDetail");
			this.$template = Handlebars.compile($("#soho_gallery_template").html());
		};

		Look.SohoController.prototype.render = function (data, isByMoreBtn) {
			var that = this;
			$("#btnSohoMore").hide();
			if (!isByMoreBtn) {
				that.$wrapper.html("");
			}

			$.ajax({
				url: "/Look/GetLookV2BrandBest",
				data: data,
				type: "GET",
				success: function (data) {
					that.$wrapper.append(that.$template(data));
					Look.SohoPageNo = data.Paging.CurrentPageNo;
					if (data.Paging.HasNext) {
						$("#btnSohoMore").show();
					}
				},
				error: function (e) {

				}
			});


		};

		Look.SohoCategoryController = function () {
			this.$wrapper = $("#SohoCategory");
			this.$template = Handlebars.compile($("#soho_category_template").html());
		}

		Look.SohoCategoryController.prototype.render = function (data, callback) {
			var that = this;
			that.$wrapper.html("");
			Look.SohoCategoryCode = "ALL";
			$.ajax({
				url: "/Look/GetLookV2Category",
				data: data,
				type: "GET",
				success: function (data) {
					var result = {};
					result.Categories = data;
					that.$wrapper.append(that.$template(result));
					//$('.look_cate li a[value="' + Look.SohoCategoryCode + '"]').attr("class", "on");
					if (typeof callback === "function") {
						callback();
					}
				},
				error: function (e) {

				}
			});
		}

		Look.sohoBestController = new Look.SohoController();
		Look.sohoCategoryController = new Look.SohoCategoryController();

		$(".look_cate li a").live("click", function (e) {
			e.preventDefault();
			Look.SohoCategoryCode = $(e.currentTarget).attr("value");
			$('.look_cate li a').removeAttr("class");
			$('.look_cate li a[value="' + Look.SohoCategoryCode + '"]').attr("class", "on");
			Look.sohoBestController.render({
				pageType: Look.SohoPageType,
				categoryType: "Soho",
				lCategoryCode: Look.SohoCategoryCode,
				brandNo: Look.SohoBrandNo,
				pageNo: 1,
				pageSize: Look.SohoPageSize
			}, false);
		});

		$("#btnSohoMore .btn_look_more").on("click", function (e) {
			e.preventDefault();
			Look.sohoBestController.render({
				pageType:Look.SohoPageType,
				categoryType: "Soho",
				lCategoryCode: Look.SohoCategoryCode,
				brandNo: Look.SohoBrandNo,
				pageNo: Look.SohoPageNo + 1,
				pageSize: Look.SohoPageSize
			},true);
		});


		$(document).ready(function () {
			Look.sohoCategoryController.render({
				categoryType: "Soho"
			}, function () {
				$('.look_cate li a[value="' + Look.SohoCategoryCode + '"]').trigger("click");
			});

		});






	})(GMobile.Look = GMobile.Look || {});
})(window.GMobile = window.GMobile || {}, jQuery, Handlebars);