//JSON 객체가 없을 경우 비슷한 동작을 수행하는 코드
if (!window.JSON) {
    window.JSON = {
        parse: function (sJSON) { return eval("(" + sJSON + ")"); },
        stringify: function (vContent) {
            if (vContent instanceof Object) {
                var sOutput = "";
                if (vContent.constructor === Array) {
                    for (var nId = 0; nId < vContent.length; sOutput += this.stringify(vContent[nId]) + ",", nId++);
                    return "[" + sOutput.substr(0, sOutput.length - 1) + "]";
                }
                if (vContent.toString !== Object.prototype.toString) { return "\"" + vContent.toString().replace(/"/g, "\\$&") + "\""; }
                for (var sProp in vContent) { sOutput += "\"" + sProp.replace(/"/g, "\\$&") + "\":" + this.stringify(vContent[sProp]) + ","; }
                return "{" + sOutput.substr(0, sOutput.length - 1) + "}";
            }
            return typeof vContent === "string" ? "\"" + vContent.replace(/"/g, "\\$&") + "\"" : String(vContent);
        }
    };
}

//////////////////////////////////////////////////////////////////
// 모바일웹에서 SNS 웹페이지로 상품정보 공유하는 Sender
//////////////////////////////////////////////////////////////////
var _MobileSnsSender = function() {

    

    return {
        send: function(snsType, params) {
            window.open(MobileSnsUtil.sns(snsType).baseUrl + this.serialize(params));    
        },
        serialize: function(params) {
            var paramArr = [];
            for (var p in params) {
                paramArr.push(p + "=" + encodeURIComponent(params[p]));
            }
            return paramArr.join("&");
        }
    }
}

//////////////////////////////////////////////////////////////////
// 모바일웹에서 SNS 앱 호출하여 상품정보 공유하는 Sender
//////////////////////////////////////////////////////////////////
var _MobileSnsAppSender = function() {
    return {
        getMobileOS: function() {
            var agent = navigator.userAgent.toLocaleLowerCase();
            var os = "";
            if (agent.search("android") > -1) {
                os = "android";
            } else if (agent.search("iphone") > -1 || agent.search("ipod") > -1 || agent.search("ipad") > -1) {
                os = "ios";
            }

            return os;    
        },
        isMobileBrowser: function() {
            var isMobile = false;
            var agent = navigator.userAgent || navigator.vendor || window.opera;

            if (/android|avantgo|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od|ad)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(agent) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|e\-|e\/|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\-|2|g)|yas\-|your|zeto|zte\-/i.test(agent.substr(0, 4)))
                isMobile = true;

            return isMobile;    
        },
        send: function(snsType, params) {
            var targetSns = MobileSnsUtil.sns(snsType);
            var mobileOS = this.getMobileOS();

            var install = (function (os) {
                return function () {
                    window.location = targetSns.store[os];
                };
            })(mobileOS);

            if (!this.isMobileBrowser()) {
                alert(targetSns.message["pc"]);
                return false;
            }

            var fullUrl = targetSns.baseUrl + this.serialize(params);

            if (mobileOS === "ios") {
                setTimeout(install, 35);
                window.location = fullUrl;
            } else if (mobileOS === "android") {
                if (MobileSnsUtil.getAgentType() === MobileSnsUtil.agentType().ANDROIDAPP) {
                    document.location.href = fullUrl;
                } else {
                    window.open(targetSns.store[mobileOS].intentPrefix + this.serialize(params) + targetSns.store[mobileOS].intentSuffix);
                }
            }
        },
//        SnsParam: function(snsType, goodsInfo) {
//            switch (snsType) {
//                case MobileSnsUtil.snsType.TWITTER:
//                    return {
//                        source: "webclient",
//                        text: "이거 괜찮은 듯 " + goodsInfo.brand + " " + goodsInfo.category + " " + goodsInfo.price + " " + goodsInfo.shortUrl + "&200004730"
//                    };
//                case MobileSnsUtil.snsType.FACEBOOK:
//                    return {
//                        u: goodsInfo.url + "&jaehuid=200004729",
//                        t: goodsInfo.brand + " " + goodsInfo.category + " " + goodsInfo.price
//                    };
//                case MobileSnsUtil.snsType.KAKAOTALK:
//                    return {
//                        msg: goodsInfo.brand + " " + goodsInfo.category + " " + goodsInfo.price,
//                        url: goodsInfo.shortUrl + "&200004731",
//                        appid: "gmarket",
//                        appver: "0.1",
//                        type: "link",
//                        appname: "G마켓"
//                    };
//                case MobileSnsUtil.snsType.KAKAOSTORY:
//                    return {
//                        post: goodsInfo.shortUrl + "&200004732",
//                        appid: "gmarket",
//                        appver: "0.1",
//                        apiver: "1.0",
//                        appname: "G마켓",
//                        urlinfo: JSON.stringify({
//                            title: goodsInfo.brand + " " + goodsInfo.category + " " + goodsInfo.price,
//                            desc: goodsInfo.name,
//                            imageurl: [goodsInfo.image],
//                            type: "website"
//                        })
//                    };
//                
//            }

//        },
        serialize: function (params) {
            var paramArr = [];
            for (var p in params) {
                paramArr.push(p + "=" + encodeURIComponent(params[p]));
            }
            return paramArr.join("&");
        }
    }
}


var _MobileSnsUtil = function () {
	var _snsType = {
		TWITTER: "twitter",
		FACEBOOK: "facebook",
		KAKAOTALK: "kakaotalk",
		KAKAOSTORY: "kakaostory",
		LINE: "line"
	}

	var _agentType = {
		ANDROIDAPP: "3"
	}

	var _sns = {
		twitter: {
			baseUrl: 'https://twitter.com/intent/tweet?',
			sender: new _MobileSnsSender()
		},
		facebook: {
			baseUrl: 'https://www.facebook.com/sharer.php?',
			sender: new _MobileSnsSender()
		},
		kakaotalk: {
			baseUrl: "kakaolink://sendurl?",
			apiver: "2.0.1",
			store: {
				android: {
					intentPrefix: "intent://sendurl?",
					intentSuffix: "#Intent;scheme=kakaolink;action=android.intent.action.VIEW;category=android.intent.category.BROWSABLE;package=com.kakao.talk;end"
				},
				ios: "http://itunes.apple.com/app/id362057947"
			},
			sender: new _MobileSnsAppSender(),
			message: {
				pc: "‘카카오톡’으로의 공유 기능은\n모바일 기기에서 사용하실 수 있습니다."
			}
		},
		kakaostory: {
			baseUrl: "storylink://posting?",
			apiver: "1.0",
			store: {
				android: {
					intentPrefix: "intent://posting?",
					intentSuffix: "#Intent;scheme=storylink;action=android.intent.action.VIEW;category=android.intent.category.BROWSABLE;package=com.kakao.story;end"
				},
				ios: "http://itunes.apple.com/app/id486244601"
			},
			sender: new _MobileSnsAppSender(),
			message: {
				pc: "‘카카오스토리’로의 공유 기능은\n모바일 기기에서 사용하실 수 있습니다."
			}
		}
	}

	return {
		getCookie: function (cookieName) {
			var parts = document.cookie.split(cookieName + "=");
			if (parts.length === 2) {
				return parts.pop().split(";").shift();
			}
		},
		getAgentType: function () {
			if (MobileSnsUtil.getCookie("pcid") === undefined) {
				return "";
			} else {
				return MobileSnsUtil.getCookie("pcid").substr(0, 1);
			}
		},
		agentType: function () {
			return _agentType;
		},
		snsType: function () {
			return _snsType;
		},
		sns: function (type) {
			if (type === this.snsType().TWITTER) {
				return {
					baseUrl: 'https://twitter.com/intent/tweet?',
					sender: new _MobileSnsSender()
				}
			} else if (type === this.snsType().FACEBOOK) {
				return {
					baseUrl: 'https://www.facebook.com/sharer.php?',
					sender: new _MobileSnsSender()
				}
			} else if (type === this.snsType().KAKAOTALK) {
				return {
					baseUrl: "kakaolink://sendurl?",
					apiver: "2.0.1",
					store: {
						android: {
							intentPrefix: "intent://sendurl?",
							intentSuffix: "#Intent;scheme=kakaolink;action=android.intent.action.VIEW;category=android.intent.category.BROWSABLE;package=com.kakao.talk;end"
						},
						ios: "http://itunes.apple.com/app/id362057947"
					},
					sender: new _MobileSnsAppSender(),
					message: {
						pc: "‘카카오톡’으로의 공유 기능은\n모바일 기기에서 사용하실 수 있습니다."
					}
				}
			} else if (type === this.snsType().KAKAOSTORY) {
				return {
					baseUrl: "storylink://posting?",
					apiver: "1.0",
					store: {
						android: {
							intentPrefix: "intent://posting?",
							intentSuffix: "#Intent;scheme=storylink;action=android.intent.action.VIEW;category=android.intent.category.BROWSABLE;package=com.kakao.story;end"
						},
						ios: "http://itunes.apple.com/app/id486244601"
					},
					sender: new _MobileSnsAppSender(),
					message: {
						pc: "‘카카오스토리’로의 공유 기능은\n모바일 기기에서 사용하실 수 있습니다."
					}
				}
			} else if (type === this.snsType().LINE) {
				return {
					baseUrl: 'line://msg/text/',
					store: {
						android: {
							intentPrefix: "intent://msg/text/",
							intentSuffix: "#Intent;scheme=line;action=android.intent.action.VIEW;category=android.intent.category.BROWSABLE;package=jp.naver.line.android;end"
						},
						ios: "http://itunes.apple.com/jp/app/line/id443904275"
					},
					sender: new _LineAppSender(),
					message: {
						pc: "‘라인’으로의 공유 기능은\n모바일 기기에서 사용하실 수 있습니다."
					}
				};
			}
		},
		agentType: function () {
			return _agentType;
		}
	}
}

var SnsParam = function (snsType, data) {
    switch (snsType) {
        case MobileSnsUtil.snsType().TWITTER:
            return {
                source: "webclient",
                text: "이거 괜찮은 듯 G마켓 " + data.name + " " + data.url
            };
        case MobileSnsUtil.snsType().FACEBOOK:
            return {
                u: data.url,
                t: data.name
            };
        case MobileSnsUtil.snsType().KAKAOTALK:
            return {
                msg: data.name,
                url: data.url,
                appid: "gmarket",
                appver: "0.1",
                type: "link",
                appname: "G마켓"
            };
        case MobileSnsUtil.snsType().KAKAOSTORY:
            return {
                post: data.url,
                appid: "gmarket",
                appver: "0.1",
                apiver: "1.0",
                appname: data.title,
                urlinfo: JSON.stringify({
                	title: "G마켓" + data.name,
                    type: "website"
                })
			};
        case MobileSnsUtil.snsType().LINE:
			return {
				message: "이거 어때? " + data.shortUrl + "&200005198"
			};
    }
}

var MobileSnsUtil = new _MobileSnsUtil();
//var MobileSnsSender = new _MobileSnsSender();
//var MobileSnsAppSender = new _MobileSnsAppSender();

