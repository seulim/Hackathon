(function (GMobile, $, iScroll, Handlebars, undefined) {
	GMobile.Const = GMobile.Const || {};
	GMobile.Const.mobileWebUrl = "http://mobile.gmarket.co.kr";
	GMobile.Const.mmygUrl = "http://mmyg.gmarket.co.kr/delivery";

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

	(function (Delivery, undefined) {
		var leftDrawerUrl, homeUrl;
		if (GMobile.isApp) {
			leftDrawerUrl = "gmarket://openmenu";
			homeUrl = "gmarket://home";
		} else {
			leftDrawerUrl = GMobile.Const.mobileWebUrl + "/Home/LeftDrawer";
			homeUrl = GMobile.Const.mobileWebUrl;
		}

		Delivery.goLeftDrawer = function () {
			location.href = leftDrawerUrl;
		};

		Delivery.goHome = function () {
			location.href = homeUrl;
		};

		Delivery.goMmyg = function () {
			location.href = GMobile.Const.mmygUrl;
		};

		Delivery.setCookie = function (cname, cvalue) {
			var d = new Date();
			d.setTime(d.getTime() + (Delivery.COOKIE_EXPIRE_MINUTE * 60 * 1000));
			var expires = "expires=" + d.toUTCString();
			document.cookie = cname + "=" + cvalue + "; " + expires + ";domain=.gmarket.co.kr;path=/";
		}

		Delivery.getCookie = function (cname) {
			var name = cname + "=";
			var ca = document.cookie.split(';');
			for (var i = 0; i < ca.length; i++) {
				var c = ca[i];
				while (c.charAt(0) == ' ') c = c.substring(1);
				if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
			}
			return "";
		}

		Delivery.COOKIE_LOCATION_KEY = "deliveryLocation";
		Delivery.COOKIE_EXPIRE_MINUTE = 60;

		Delivery.Geolocation = function () {
			if (Delivery.Geolocation.prototype._singletonInstance) {
				return Delivery.Geolocation.prototype._singletonInstance;
			}

			Delivery.Geolocation.prototype._singletonInstance = this;

			var that = this;

			this.isSupport = false;
			this.hasCurrentPosition = false;
			this.lock = null;

			this.latitude = null;
			this.longitude = null;
			this.zipcode = null;
			this.address = null;

			if (navigator.geolocation) {
				this.isSupport = true;
			}

			var successCallbacks = [];
			var errorCallbacks = [];

			this.addListener = function (callbacks) {
				if (callbacks) {
					if (callbacks.success) {
						successCallbacks.push(callbacks.success);
					}

					if (callbacks.error) {
						errorCallbacks.push(callbacks.error);
					}
				}
			};

			this.setCurrentData = function (latitude, longitude, zipcode, address) {
				that.latitude = latitude;
				that.longitude = longitude;
				that.zipcode = zipcode;
				that.address = address;

				// TODO: 값을 체크할까?
				that.hasCurrentPosition = true;
			}

			this.applySuccessCallbacks = function () {
				for (var i in successCallbacks) {
					successCallbacks[i].apply(this, [this.latitude, this.longitude, this.zipcode, this.address]);
				}
			};

			this.applyErrorCallbacks = function (data) {
				for (var i in errorCallbacks) {
					errorCallbacks[i].apply(this, [data]);
				}
			};
		};

		Delivery.Geolocation.getInstance = function () {
			return new Delivery.Geolocation();
		}

		Delivery.Geolocation.prototype.get = function (isForceGet) {
			var that = this;

			if (this.isSupport) {
				if ((false == this.hasCurrentPosition || isForceGet) && this.lock == null) {
					this.lock = {};
					var cookieString = Delivery.getCookie(Delivery.COOKIE_LOCATION_KEY);
					if (cookieString && !isForceGet) {
						var splitedArray = cookieString.split('|');
						this.setCurrentData(splitedArray[0], splitedArray[1], splitedArray[2], decodeURIComponent(splitedArray[3]));
						this.applySuccessCallbacks();
						this.lock = null;
					}
					else {
						navigator.geolocation.getCurrentPosition(
						function (data) {
							$.ajax({
								url: GMobile.Const.mobileWebUrl + "/Delivery/GetCoordToAddress",
								type: "POST",
								data: {
									longitude: data.coords.longitude,
									latitude: data.coords.latitude
								},
								success: function (addressData) {
									if (addressData != null) {
										that.setCurrentData(data.coords.latitude, data.coords.longitude, addressData.zipcode, addressData.fullName);
										var coockieValueString = data.coords.latitude + "|" + data.coords.longitude + "|" + addressData.zipcode + "|" + encodeURIComponent(addressData.fullName);
										Delivery.setCookie(Delivery.COOKIE_LOCATION_KEY, coockieValueString);
										that.applySuccessCallbacks();
									}

									that.lock = null;
								},
								error: function () {
									that.setCurrentData(data.coords.latitude, data.coords.longitude);
									that.applyErrorCallbacks();

									that.lock = null;
								}
							});
						},
						function (data) {
							that.setCurrentData(0, 0);
							that.applyErrorCallbacks(data);

							that.lock = null;
						},
						{
							timeout: 5000
						}
					);
					}
				} else if (this.lock == null) {
					this.applySuccessCallbacks();
				}
			}
		};





		Delivery.LocationController = function () {
			if (Delivery.LocationController.prototype._singletonInstance) {
				return Delivery.LocationController.prototype._singletonInstance;
			}

			Delivery.LocationController.prototype._singletonInstance = this;

			var that = this;

			var isFirstRender = true;

			this.latitude = null;
			this.longitude = null;
			this.zipcode = null;
			this.address = null;

			var successCallbacks = [];
			var errorCallbacks = [];

			this.addListener = function (callbacks) {
				if (callbacks) {
					if (callbacks.success) {
						successCallbacks.push(callbacks.success);
					}

					if (callbacks.error) {
						errorCallbacks.push(callbacks.error);
					}
				}
			};

			this.setCurrentData = function (latitude, longitude, zipcode, address) {
				that.latitude = latitude;
				that.longitude = longitude;
				that.zipcode = zipcode;
				that.address = address;
			}

			this.applySuccessCallbacks = function () {
				for (var i in successCallbacks) {
					successCallbacks[i].apply(this, [this.latitude, this.longitude, this.zipcode, this.address]);
				}
			};

			this.applyErrorCallbacks = function (data) {
				for (var i in errorCallbacks) {
					errorCallbacks[i].apply(this, [data]);
				}
			};

			var geolocation = new Delivery.Geolocation();

			var locationTemplate = new Delivery.LocationTemplate("#layer_local");

			locationTemplate.renderMainLayer();

			var $addressInput = $("#srch1");
			var $currentAddress = $("#current_address");
			var $currentAddressOuter = $("#current_address_outer");
			var $refreshButton = $("#address_refresh");

			var setOuterAddress = function (address) {
				var splitedAddress = address.split(" ");
				if (splitedAddress.length >= 3) {
					$currentAddressOuter.html(splitedAddress[splitedAddress.length - 1]);
				} else {
					$currentAddressOuter.html(address);
				}
			};

			if (false == geolocation.isSupport) {
				$currentAddress.html("해당 기기에서는 현재 위치를 사용할 수 없습니다.");

				$refreshButton.hide();
			} else {
				var rotateInterval;
				$refreshButton.on("click", function () {
					var $thisButton = $(this);

					var rotateButton = function () {
						$thisButton.animate({ borderSpacing: -180 }, {
							step: function (now, fx) {
								$(this).css('-webkit-transform', 'rotate(' + now + 'deg)');
								$(this).css('-moz-transform', 'rotate(' + now + 'deg)');
								$(this).css('transform', 'rotate(' + now + 'deg)');
							},
							complete: function () {
								$(this).css('-webkit-transform', '');
								$(this).css('-moz-transform', '');
								$(this).css('transform', '');
								$(this).css('border-spacing', '');
							},
							duration: 600
						}, 'swing');
					};

					rotateButton();
					rotateInterval = setInterval(rotateButton, 900);

					geolocation.get(true);
				});

				geolocation.addListener({
					success: function (latitude, longitude, zipcode, address) {
						that.setCurrentData(latitude, longitude, zipcode, address);

						if (isFirstRender) {
							that.setCurrentData(geolocation.latitude, geolocation.longitude, geolocation.zipcode, geolocation.address);
							that.applySuccessCallbacks();
							setOuterAddress(that.address);

							isFirstRender = false;
						}

						$currentAddress.html(address);

						clearInterval(rotateInterval);
					},
					error: function (data) {
						if (data.code == data.PERMISSION_DENIED) {
							alert("위치정보 사용을 실행하시면 현재 위치를 검색할 수 있습니다.");
						} else {
							alert("위치정보를 수신하지 못하였습니다. 다시 시도해 주세요.");
						}

						that.setCurrentData(0, 0, "empty", "");
						that.applyErrorCallbacks(data);

						$currentAddress.html("위치를 설정해주세요.");
						if (isFirstRender) {
							$currentAddressOuter.html("위치를 설정해주세요.");

							isFirstRender = false;
						}

						clearInterval(rotateInterval);
					}
				});

				this.getGeoLocation = function () {
					return geolocation;
				}
				//				geolocation.get();
			}

			var $addressWrapper = $("#address_wrapper");
			$addressWrapper.on("click", "a", function (e) {
				e.preventDefault();

				$addressWrapper.find("li").removeClass("selected");
				$(this).parent().addClass("selected");

				var coockieValueString = $(this).data("latitude") + "|" + $(this).data("longitude") + "|" + $(this).data("zipcode") + "|" + encodeURIComponent($(this).html());
				Delivery.setCookie(Delivery.COOKIE_LOCATION_KEY, coockieValueString);

				that.setCurrentData($(this).data("latitude"), $(this).data("longitude"), $(this).data("zipcode"), $(this).html());
				that.applySuccessCallbacks();
				setOuterAddress($(this).html());

				locationTemplate.closeLayer();
			})

			$("#go_get_address_search").on("click", function () {
				that.getAddressSearch($addressInput.val());
			});

			$("#select_current").on("click", function () {
				if (geolocation.isSupport && geolocation.hasCurrentPosition && geolocation.latitude != 0 && geolocation.longitude != 0) {
					var coockieValueString = geolocation.latitude + "|" + geolocation.longitude + "|" + geolocation.zipcode + "|" + encodeURIComponent(geolocation.address);
					Delivery.setCookie(Delivery.COOKIE_LOCATION_KEY, coockieValueString);

					that.setCurrentData(geolocation.latitude, geolocation.longitude, geolocation.zipcode, geolocation.address);
					that.applySuccessCallbacks();
					setOuterAddress(that.address);

					locationTemplate.closeLayer();
				} else {
					alert("현재 위치는 사용할 수 없습니다.");
				}
			});

			this.getAddressSearch = function (keyword) {
				$.ajax({
					url: GMobile.Const.mobileWebUrl + "/Delivery/GetAddressSearch",
					type: "POST",
					data: {
						keyword: keyword
					},
					success: function (data) {
						if (data != null && data.item != null) {
							locationTemplate.renderAddress(data.item);
						}
					},
					error: function () {
						locationTemplate.renderAddress("검색결과가 없습니다. 다른 주소를 검색해주세요");
					}
				});
			};

			this.openLocationLayer = function () {
				if (GMobile.isApp) {
					location.href = "gmarket://find_location?callback=GMobile.Delivery.LocationController.findLocationCallback";
				} else {
					popBlockUI3('layer_local');
				}
			};

		};

		Delivery.LocationController.getInstance = function () {
			return new Delivery.LocationController();
		};

		Delivery.LocationController.findLocationCallback = function (latitude, longitude, zipcode, address) {
			var that = Delivery.LocationController.getInstance();

			that.setCurrentData(latitude, longitude, zipcode, address);
			that.applySuccessCallbacks();
		};




		Delivery.LocationTemplate = function (wrapperSelector) {
			var mainLayer = "";
			mainLayer += '<h4 class="ly_tit">위치 설정</h4>';
			mainLayer += '<div class="local_area">';
			mainLayer += '	<span class="now_txt">현재위치</span>';
			mainLayer += '	<p class="local" id="current_address">탐색 중</p>';
			mainLayer += '	<button class="btn_refresh" id="address_refresh"><span class="sp_del">새로고침</span></button>';
			mainLayer += '	<button class="btn_chk" id="select_current">선택</button>';
			mainLayer += '</div>';
			mainLayer += '<div class="find_area">';
			mainLayer += '	<p class="ex_txt">동(읍/면/가)명으로 검색</p>';
			mainLayer += '	<div class="srch_bx">';
			mainLayer += '		<label for="srch1" class="blind"></label>';
			mainLayer += '		<input type="text" id="srch1" placeholder="동(읍/면/가)명으로 검색" />';
			mainLayer += '		<button type="button" id="go_get_address_search" class="btn_srch">검색</button>';
			mainLayer += '	</div>';
			mainLayer += '	<div class="adr_resultbx">';
			mainLayer += '		<div id="adr_wrapper" class="adr_result">';
			mainLayer += '			<div class="scroll_box">';
			mainLayer += '				<ul class="adr_lst" id="address_wrapper"></ul>';
			mainLayer += '			</div>';
			mainLayer += '		</div>';
			mainLayer += '	</div>';
			mainLayer += '</div>';
			mainLayer += '<a href="#" class="btn_close"><span class="sp_del">닫기</span></a>';

			var addressTemplate = '<li><a href="#" data-latitude="{{lat}}" data-longitude="{{lng}}" data-zipcode="{{zipcode}}">{{title}}</a></li>';

			var locationIScroll;
			var $layerWrapper = $(wrapperSelector);
			var $addressWrapper = $();
			var $addressResultWrapper = $();

			this.renderMainLayer = function () {
				var that = this;

				$layerWrapper
					.append(mainLayer)
					.find(".btn_close").on("click", function (e) {
						e.preventDefault();

						that.closeLayer();
					});

				$addressWrapper = $layerWrapper.find("#address_wrapper");
				$addressResultWrapper = $layerWrapper.find(".adr_resultbx");

				locationIScroll = new iScroll('adr_wrapper', { hScrollbar: false, vScrollbar: true, checkDOMChanges: true });
			};

			this.renderAddress = function (data) {
				if (typeof data !== "undefined" && data != null) {
					$addressWrapper.html("");

					if (typeof data.length !== "undefined" && data.length > 0) {
						for (var i = 0; i < data.length; i++) {
							$addressWrapper.append(addressTemplate
								.replace("{{lat}}", data[i].lat)
								.replace("{{lng}}", data[i].lng)
								.replace("{{zipcode}}", data[i].zipcode)
								.replace("{{title}}", data[i].title)
							);
						}
					} else {
						$addressWrapper.append("<li class='adr_none'>검색결과가 없습니다. 다른 주소를 검색해주세요</li>");
					}

					$addressResultWrapper.show();
					locationIScroll.refresh();
				}
			};

			this.closeLayer = function () {
				popBlockUI3('layer_local');
			};
		};





		Delivery.HomeController = function () {
			if (Handlebars) {
				var that = this;

				var locationController = new GMobile.Delivery.LocationController();

				Handlebars.registerHelper("addComma", function (str) {
					var num = str.toString();
					var pattern = /(-?[0-9]+)([0-9]{3})/;

					while (pattern.test(num)) {
						num = num.replace(pattern, "$1,$2");
					}

					return num;
				});

				Handlebars.registerHelper("ifBuyNowShowTypeIsShow", function (buyNowShowType, opts) {
					if (buyNowShowType == "Show") {
						return opts.fn(this);
					} else {
						return opts.inverse(this);
					}
				});

				Handlebars.registerHelper("ifBuyNowShowTypeIsDimmed", function (buyNowShowType, opts) {
					if (buyNowShowType == "Dimmed") {
						return opts.fn(this);
					} else {
						return opts.inverse(this);
					}
				});

				Handlebars.registerHelper("ifShopTagTypeIsFavorite", function (shopTagType, opts) {
					if (shopTagType == "Favorite") {
						return opts.fn(this);
					} else {
						return opts.inverse(this);
					}
				});

				Handlebars.registerHelper("ifShopTagTypeIsOrdered", function (shopTagType, opts) {
					if (shopTagType == "Ordered") {
						return opts.fn(this);
					} else {
						return opts.inverse(this);
					}
				});

				Handlebars.registerHelper("getStarPercentage", function (count) {
					return count / 5 * 100;
				});

				var $bannerWrapper = $("#banner_wrapper");
				var bannerTemplate = Handlebars.compile($("#tmpl_delivery_banner").html());

				var $categoryWrapper = $("#category_wrapper");
				var categoryTemplate = Handlebars.compile($("#tmpl_delivery_category").html());

				var $myShopWrapper = $("#bnGrp2");
				var myShopTemplate = Handlebars.compile($("#tmpl_delivery_myshop_list").html());

				var $myPositionShopWrapper = $("#mypositionshop_wrapper");
				var myPositionShopTemplate = Handlebars.compile($("#tmpl_delivery_mypositionshop_list").html());

				var bannerSwipeView;

				$("#prev").on("click", function (e) {
					e.preventDefault();

					bannerSwipeView.prev();
				});

				$("#next").on("click", function (e) {
					e.preventDefault();

					bannerSwipeView.next();
				});

				var renderBanner = function (data) {
					if (data && data.length > 0) {
						var bannerHtmls = [];
						for (var i = 0; i < data.length; i++) {
							bannerHtmls.push(bannerTemplate(data[i]).trim());
						}

						if (bannerHtmls.length > 1) {
							var $wrapper = $("#bnGrp");
							var $buttons = $("#bnTurn button");
							var $currentPageIndex = $("#banner_current_index");
							var $maxPageIndex = $("#banner_max_index");

							$currentPageIndex.text("1");
							$maxPageIndex.text(data.length);

							if (bannerSwipeView) {
								bannerSwipeView.destroy();

								$wrapper.html("");
							}

							bannerSwipeView = new SwipeView("#bnGrp", {
								numberOfPages: bannerHtmls.length,
								hastyPageFlip: true,
								flickTime: 0
							});

							for (var i = 0; i < 3; i++) {
								var page = i == 0 ? bannerHtmls.length - 1 : i - 1;

								bannerSwipeView.masterPages[i].innerHTML = bannerHtmls[page];
							}

							bannerSwipeView.onFlip(function () {
								for (var i = 0; i < 3; i++) {
									var upcoming = bannerSwipeView.masterPages[i].dataset.upcomingPageIndex;

									if (upcoming != bannerSwipeView.masterPages[i].dataset.pageIndex) {
										bannerSwipeView.masterPages[i].innerHTML = bannerHtmls[upcoming];
									}
								}

								$currentPageIndex.text(bannerSwipeView.pageIndex + 1);
							});

							var $swipeViewSlider = $wrapper.find("#swipeview-slider");

							$(window).load(function () {
								changeWrapperHeight();
							});

							$(window).on("resize", function () {
								changeWrapperHeight();
							});

							function changeWrapperHeight() {
								var $bannerImage = $wrapper.find("img");
								var height = $bannerImage.height(); //이미지높이값
								$wrapper.height(height); //래퍼높이값 조절
								$buttons.height(height);
								$buttons.css("top", -height);

								//ios7에서 높이값 잡지 못하는 버그	
								$swipeViewSlider.children().height(height);
							}

							changeWrapperHeight();
						}
					}
				};

				var renderCategory = function (data) {
					$categoryWrapper.html(categoryTemplate(data));
				};

				var renderLinks = function (data) {
					var $keywordDeliverySearch = $("#keyword_delivery_search");
					$("#go_delivery_search").bind("click", function (event) {
						if ($keywordDeliverySearch.val()) {
							var encode = encodeURIComponent($keywordDeliverySearch.val());
							location.href = data.SrpUrl + encode;
						}
						else {
							alert('매장명을 입력해주세요');
						}
					});

					$keywordDeliverySearch.focus(function () {
						$(this).siblings().hide();
					}).blur(function () {
						$(this).siblings().show();
						if (!$(this).val() == "") {
							$(this).siblings().hide();
						}
					});

					$keywordDeliverySearch.trigger("blur");

					$('#aUseDelivery').prop('href', data.UseDeliveryUrl);
					$('#aFaq').prop('href', data.FAQUrl);
					$('#aFaq').find('.num').text(data.FAQTel);
					$('#aCart').prop('href', data.CartLinkUrl);
					$('#aDeliveryServiceRequest').prop('href', data.DeliveryServiceRequestUrl);
				}

				var myShopSwipeView;
				var $myShopSection = $("#myshop_title, #myshop_body");
				var $myShopPaging = $("#myshop_paging");
				var $pageIndicatorWrapper = $("#bnIndex2");

				$("#prev2").on("click", function (e) {
					e.preventDefault();

					myShopSwipeView.prev();
				});

				$("#next2").on("click", function (e) {
					e.preventDefault();

					myShopSwipeView.next();
				});

				var renderMyShop = function (data) {
					var myShopHtmls = [];

					if (data && data.length > 0) {
						$myShopSection.show();

						$pageIndicatorWrapper.html("");

						for (var i = 0; i < data.length; i += Delivery.HomeController.MY_SHOP_PAGE_COUNT) {
							myShopHtmls.push(myShopTemplate(data.slice(i, i + Delivery.HomeController.MY_SHOP_PAGE_COUNT)).trim());

							$pageIndicatorWrapper.append("<p></p>");
						}

						var $wrapper = $("#bnGrp2");

						if (myShopHtmls.length > 1) {
							if (myShopSwipeView) {
								myShopSwipeView.destroy();
								$wrapper.html("");
							}

							myShopSwipeView = new SwipeView('#bnGrp2', {
								numberOfPages: myShopHtmls.length,
								hastyPageFlip: true,
								flickTime: 0
							});

							$pageIndicatorWrapper.find(":eq(0)").addClass("selected");

							myShopSwipeView.onFlip(function () {
								for (var i = 0; i < 3; i++) {
									var upcoming = myShopSwipeView.masterPages[i].dataset.upcomingPageIndex;

									if (upcoming != myShopSwipeView.masterPages[i].dataset.pageIndex) {
										myShopSwipeView.masterPages[i].innerHTML = myShopHtmls[upcoming];
									}
								}

								$pageIndicatorWrapper.find(".selected").removeClass("selected");
								$pageIndicatorWrapper.find(":eq(" + myShopSwipeView.pageIndex + ")").addClass("selected");
							});

							for (var i = 0; i < 3; i++) {
								var page = i == 0 ? myShopHtmls.length - 1 : i - 1;

								myShopSwipeView.masterPages[i].innerHTML = myShopHtmls[page];
							}


							var $swipeViewSlider = $wrapper.find("#swipeview-slider");

							$(window).load(function () {
								changeWrapperHeight();
							});

							$(window).on("resize", function () {
								changeWrapperHeight();
							});

							function changeWrapperHeight() {
								var $ul = $wrapper.find("ul");
								var height = $ul.height();
								$wrapper.height(height);

								//ios7에서 높이값 잡지 못하는 버그	
								$swipeViewSlider.children().height(height);
							}

							changeWrapperHeight();

							$myShopPaging.show();
						} else {
							$wrapper.html(myShopHtmls.join());
							$myShopPaging.hide();
						}
					} else {
						$myShopSection.hide();
						$myShopPaging.hide();
					}
				}

				var $myPositionShopSection = $("#mypositionshop_title, #mypositionshop_body");

				var renderMyPositionShop = function (data) {
					if (data && data.length > 0) {
						$myPositionShopSection.show();
						$myPositionShopWrapper.html(myPositionShopTemplate(data));
					} else {
						$myPositionShopSection.hide();
					}
				}



				var getDeliveryMain = function () {
					$.ajax({
						url: GMobile.Const.mobileWebUrl + "/Delivery/GetDeliveryMain",
						type: "POST",
						data: {},
						success: function (data) {
							if (data != null) {
								renderCategory(data);
								renderBanner(data.Banner);
								renderLinks(data);
							}
						},
						error: function (data) {
							console.log(data);
						}
					});
				};

				var getDeliveryShop = function (latitude, longitude, zipCode) {
					$.ajax({
						url: GMobile.Const.mobileWebUrl + "/Delivery/GetDeliveryShop",
						type: "POST",
						data: {
							latitude: latitude,
							longitude: longitude,
							zipCode: zipCode,
							myShopPageCount: Delivery.HomeController.MY_SHOP_PAGE_COUNT
						},
						success: function (data) {
							if (data != null) {
								renderMyShop(data.MyShop);
								renderMyPositionShop(data.MyPositionShop);
								$("#go_my_position_shop_more").prop("href", data.MyPositionBestUrl);
							}
						},
						error: function (data) {
							$myShopSection.hide();
							$myShopPaging.hide();
							$myPositionShopSection.hide();
						}
					});
				};

				locationController.addListener({
					success: function (latitude, longitude, zipcode, address) {
						getDeliveryMain();
						getDeliveryShop(latitude, longitude, zipcode);
					},
					error: function (data) {
						getDeliveryMain();
						getDeliveryShop(0, 0, "empty");
					}
				});

				locationController.getGeoLocation().get();
			} else {
				throw "Need Handlebars.";
			}
		};

		Delivery.HomeController.MY_SHOP_PAGE_COUNT = 2;
	})(GMobile.Delivery = GMobile.Delivery || {});
})(window.GMobile = window.GMobile || {}, jQuery, iScroll, window.Handlebars);