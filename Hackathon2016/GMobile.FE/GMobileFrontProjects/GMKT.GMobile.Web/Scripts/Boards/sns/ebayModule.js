/*! ebayModules - v0.1.8 - 2014-09-17 */window.eModule = window.eModule || {};

(function (window, em) {
    //strict 모드 선언
    //http://msdn.microsoft.com/ko-kr/library/br230269(v=vs.94).aspx
    "use strict";
    function _defined(_this, name, modules, callback) {
        _this[name] = null;
        if (Array.isArray(modules) && typeof callback === 'function') {
            _this[name] = callback.apply(this, modules);
        } else {
            _this[name] = callback;
        }
    };
    /**
    * # global consts in eMobule  
    *
    * @namespace eModule
    * @class globlas
    **/
    em.globals = {
        /**
        * globals init or add globals-value  
        * @method defined
        * @param name {string} required The value name in globals.
        * @param options {object} The value in globals[name].
        * @chainable
        **/
        defined : function( name, options ){
            var _this = this;

            if( _this[name] ){
                //update
                for( var prop in _this[name] ){
                    _this[name][prop] = options[prop];
                }
            } else {
                _this[name] = options;
            }
            return this;
        },
        regExp : {
          username: '^[a-z0-9_-]{4,12}$',
					password: '^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\\s).{6,12}$',
					hex: '^#?([a-f0-9]{6}|[a-f0-9]{3})$',
					email: '^([a-z0-9_\\.-]+)@([\\da-z\\.-]+)\\.([a-z\\.]{2,6})$',
					url: '^(https?:\\/\\/)?([\\da-z\\.-]+)\.([a-z\\.]{2,6})([\\/\\w \\.-]*)*\\/?$',
					ip: '^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$',
					letter: '[A-Za-z]+',
					number: '^[0-9]+$',
					number3: '^[0-9]{3}$',
					number4: '^[0-9]{4}$',
					plusOrMinus: '^[-+]?[0-9]+$',
					floating: '^[-+]?[0-9]+\\.[0-9]+$',
					dateformat: '^\\d{4}[\\/\\-](0?[1-9]|1[012])[\\/\\-](0?[1-9]|[12][0-9]|3[01])$',
					phone: '^\\(?([0-9]{3})\\)?\\-?([0-9]{3,4})\\-?([0-9]{4})$',
					cardAmericanExpress: '^(?:3[47][0-9]{13})$',
					cardVisa: '^(?:4[0-9]{12}(?:[0-9]{3})?)$',
					cardMaster: '^(?:5[1-5][0-9]{14})$',
					cardDiscover: '^(?:6(?:011|5[0-9][0-9])[0-9]{12})$',
					cardDinersClub: '^(?:3(?:0[0-5]|[68][0-9])[0-9]{11})$',
					cardJCB: '^(?:(?:2131|1800|35\\d{3})\\d{11})$',
          continue4letter : '/(\\w)\\1\\1\\1/'
        }
    };
    /**
    * # global functions in eModule  
    *
    * @namespace eModule
    * @class utils
    */
    em.utils = {
        /**
        * utils init or add util-application.  
        * @method defined
        * @param name {string} The application name in utils.
        * @param callback {function} The application value in utils[name].
        * @chainable
        **/
        defined : function( name, callback ) {
            _defined( this, name, [], callback );
            return this;
        }
        /**
        * Get parameters of document url  
        * @method params
        * @param str {string} search of url params
        * @return returnValue {array} params type array
        **/
        , params : function(str){
            if(str){
                var strArray = str.split('&')
                    , returnValue = {};
                for(var i = 0, len = strArray.length ; i < len ; i++){
                    var tmp = strArray[i].split('=');
                    returnValue[tmp[0].toLowerCase()] = tmp[1];
                };
            } else {
                console.error('please checked str');
            }
            return returnValue;
        }
        /**
        * Update property and value in Target  
        * @method extend
        * @param target {object} DataSets
        * @param obj {object} options
        * @return target {object} update target
        * @since 0.1.0
        **/
        , extend : function(target, obj){
            for(var prop in obj){
                if(obj[prop] !== 'undefined' && obj[prop] !== null){
                    target[prop] = obj[prop] !== 'undefined' && obj[prop] !== null ? obj[prop] : '';
                }
            }
            return target;
        }
        /**
        * Set defaults jQuery Ajax  
        * @uses jQuery
        **/
        , ajax : function(url, options){
          if(!window.jQuery){
            console.log('[#utils.ajax - error #00] ');
            return;
          }
          if(!url){
            console.log('[#utils.ajax - error #01] url is not defined. please check url');
            return;
          }
          return {
            /**
            * @method ajax.get
            * @param {object} options - ajax setting options url, data
            * @param {string} options.url - url
            * @param {object} [options.data] - params
            * @return {void}
            * @example  
                eModule.utils.ajax({ 
                   url : config.get('contextPath') + '/books' ,  
                   data : {  
                     offset : 0  
                   } 
                }).get ();
            **/
            get: function () {
              var _this = this;
              // if (!options.loading) {
              // 	options.loading = _this.loading();
              // 	options.loading.show();
              // }
              $.getJSON(url, options.data)
                .done(function (result) {
                  // options.loading && options.loading.hide();
                  //if (!result.hasErrors) {
                    options.successHandler && options.successHandler(result);
                  //} else {
                    //options.errorHandler ? options.errorHandler(result) : null;
                  //}
                }).fail(function (jqxhr, textStatus, error) {
                  // options.loading && options.loading.hide();
                  console.log('[#response jqxhr] : ' + jqxhr);
                  console.log('[#response textStatus] : ' + textStatus);
                  console.log('[#response error] : ' + error);
                });
            }
            /**
      	    * @method ajsx.getp
      	    * @param {object} options - ajax setting options url, data
      		  * @param {string} options.url - url
      		  * @param {object} [options.data] - params
      		  * @return {void}
      		  * @example 
                utils.ajax({
                 url : config.get('contextPath') + '/books/data.jsp>callback=?' 
                }).get();
      	    **/
      			, getp: function () {
      				var _this = this;
        				$.ajax({
        					url: url,
        					data: options.data,
        					dataType: 'jsonp'
        				})
      					.done(function (result) {
      						// options.loading && options.loading.hide();
      						//if (!result.hasErrors) {
      							options.successHandler && options.successHandler(result);
      			//			}
      					}).fail(function (jqxhr, textStatus, error) {
      						// options.loading && options.loading.hide();
                  console.log('[#response error] : ' + error);
      					});
      			}
            /**
        	  * @method ajax.set
        	  * @param {object} options - ajax setting options url, data
        		* @param {string} options.url - url
        		* @param {object} [options.data] - params
        		* @return {void}
        		* @example 
                utils.ajax({ 
                  url : config.get('contextPath') + '/books/add' ,
                  data : { id : 1 , name : 'javascript 완벽 가이드' }
                }).set();
	           **/
      			, set: function () {
      				var _this = this;
      				// if (!options.loading) {
      				// 	options.loading = _this.loading();
      				// 	options.loading.show();
      				// }
      				$.post(url, options.data)
      					.done(function (result) {
      						// options.loading && options.loading.hide();
      						//if (!result.hasErrors) {
      							options.successHandler && options.successHandler(result);
      			//			} else {
      			//				options.errorHandler ? options.errorHandler(result) : null;
      			//			}
      					}).fail(function (jqxhr, textStatus, error) {
      						// options.loading && options.loading.hide();
                  console.log('[#response error] : ' + error);
      					});
      			}
          };
        }
        , errorHandler : function(code){
          var messages = {
            '1' : {
              'K' : '저장하지 못했습니다. 다시 시도해 주세요. ',
              'E' : 'This message was not saved. Please try again.'
            },
            '2' : {
              'K' : '게시판 사용 기간이 아닙니다.',
              'E' : ': It is not the period to be able to use this board.'
            },
            '3' : {
              'K' : '이미 글을 등록하셨습니다. ',
              'E' : "You've already written the same message."
            },
            '4' : {
              'K' : '하루 중복 등록 가능 회수를 넘었습니다.',
              'E' : 'You can leave only one message in a day.'
            },
            '5' : {
                'K' : '로그인 후 이용하실 수 있습니다. ',
                'E' : 'Please, sign-in first.'
            },
            'delete' : {
              'K' : '삭제하지 못했습니다. 다시 시도해 주세요. ',
              'E' : 'This message was not deleted. Please try again.'
            },
            'submit' : {
              'K' : '글을 저장할 수 없습니다.',
              'E' : "The message can't be saved."
            }
          };
          
          return messages[code];
        }
    };
    /**
    * # applications in globals  
    *
    * @namespace eModule
    * @class apps
    *
    **/
    em.apps = {
        /**
        * application init  
        * @method defined
        * @param name {string} application name
        * @param module {object} include modules
        * @param callback {function} callback function
        * @chainable
        **/
        defined : function( name, modules, callback ){
            _defined( this, name, modules, callback );
            return this;
        }
    };
})(window, eModule)
;; (function (window, em) {
    var globals = em.globals;
    globals.defined('boards', {
        sns : {
          table : '<div id="sns" class="sns_board">' +
                    '<div class="sns_write"></div>'+
                    '<ul class="lst_sns"></ul>'+
                    '<div id="paging" class="paginate"></div>'+
                  '</div>',
          list :  '<li data-bcid="{{BoardContentID}}" data-id="{{ID}}" class="{{isReply}}">'+
                    '<div class="lst_view">' +
                      '<div class="thmb" data-snsid="{{SnsUserName}}" data-snskind="{{snsKlass}}">'+
                        '<img src="{{SnsProfileImage}}" class="js-goto-sns" style="width:38px;height:38px;border:0"/>' + 
                      '</div>'+
                      '<div class="tx">'+
                        '<em class="tx_uid">'+
                          '<span class="sns_{{snsKlass}}">{{snsName}}</span>'+
                          '{{SnsUserID}}&nbsp;'+
                        '</em>'+
                        '<span class="tx_time">&nbsp;{{RegistDateTimeString}}</span>'+
                        '<p class="tx_sns">'+
                          '{{Contents}}&nbsp;'+
                          '{{IsEnabledReply}}&nbsp;'+
                          '{{IsEnabledDelete}}&nbsp;'+
                        '</p>'+
                      '</div>'+
                    '</div>'+
                  '</li>',
          reply : '<form action="/" method="post" name="reply">'+
                    '<div class="reply_area">'+
                      '<div class="sel_sns">'+
                        '{{snsBox}}'+
                      '</div>'+
                      '<div class="thmb">'+
                        '{{profileImage}}'+
                      '</div>'+
                      '<div class="write">'+
                        '<div class="is-visible"></div>'+
                        '<textarea cols="30" rows="5" placeholder="{{placeholder}}"></textarea>'+
                        '<p class="tx"><strong>0</strong>/{{maxLength}}</p>'+
                        '<button class="btn_cfm js-submit-post">{{submit}}</button>'+
                      '</div>'+
                    '</div>' +
                  '</form>',
          confirm : '<form action="/" method="post" name="post">'+
                        '{{isLogin}}'+
                        '<div class="write_area">'+
                          '<div class="sel_sns">'+
                            '{{snsBox}}'+
                          '</div>'+
                          '<div class="thmb">'+
                            '{{profileImage}}'+
                          '</div>'+
                          '<div class="write">'+
                            '<div class="is-visible"></div>'+
                            '<textarea cols="30" rows="5" name="contents" placeholder="{{placeholder}}"></textarea>'+
                            '<p class="tx"><strong>0</strong>/{{maxLength}}</p>'+
                            '<button class="btn_cfm js-submit-post">{{submit}}</button>'+
                          '</div>'+
                        '</div>'+
                      '</form>',
          emptyList : '<p class="sns_default">등록된 글이 없습니다.</p>',
          snsBox : '<input type="checkbox" class="input_chk js-selected-sns" data-snstype="{{kindType}}" name="sns" value="{{kindValue}}" {{checked}}/>' + 
          '<label for="sns_{{kindId}}" class="sp_evt sns_{{kindId}} {{kindLogOn}}">{{kindName}}</label>',
          profileImage : '<a href="#" class="btn prev {{disable}} js-change-profileimage"><span class="sp_evt">이전 이미지</span></a><img src="{{profileImageUrl}}" class="js-move-usersns" style="{{width}};{{height}};border:0"/><a href="#" class="btn next {{disable}} js-change-profileimage" ><span class="sp_evt">다음 이미지</span></a>',
          profileImageReply : '<img src="{{profileImageUrl}}" class="js-move-usersns" style="{{width}};{{height}};border:0"/>',
          snsDatas :  '<input type="hidden" name="snsKind" value="{{kind}}"/><input type="hidden" name="snsUserID" value="{{userId}}"/><input type="hidden" name="snsProfileImage" value="{{perfileImage}}"/><input type="hidden" name="snsUserName" value="{{userName}}"/>',
          buttons : {
            reply : '<button class="btn_reply js-open-reply"><span>{{replyButton}}</span></button>', 
            del : '<button class="btn_del js-delete-post"><span>{{deleteButton}}</span></button>'
          },
          icons : {
            errorThumnailImageUrl : 'http://image.gmarket.co.kr/challenge/neo_total_board/sns/img_45.gif',
            profileImage : {
              post : 'http://image.gmarket.co.kr/event/bbs/68x68_img.gif',
              reply : 'http://image.gmarket.co.kr/event/bbs/43x43_none_img.gif'
            }
          }
        }
    });
})(window, eModule);
;;(function(window, em){
  var globals = em.globals;
  globals.defined('snsAttributes', {
    'FB' : {
      kindId : 'f',
      kindClass : 'f',
      kindType : 'FB',
      kindValue : 'facebook',
      kindName : '페이스북',
      kindUrl : 'http://m.facebook.com'
    },
    'TW' : {
      kindId : 't',
      kindClass : 't',
      kindType : 'TW',
      kindValue : 'twitter',
      kindName : '트위터',
      kindUrl : 'http://m.twitter.com'
    }
  });
})(window, eModule);
;; (function (window, em) {
    var globals = em.globals;
    /**
    * # 전역 네임스페이스  
    * ---  
    * 상수 변수를 관리하는 전역 네임스페이스  
    *  
    * ```javascript  
    * eModule.globals.fcd = {  
    *  'A' : 706200001 // landing type === 'A'  
    *  , 'BApp' : 706200002 // landing type === 'B' and click ['go to app']  
    *  , 'BNext' : 706200003 // landing type === 'B' and click ['next']  
    * };  
    * ```    
    *  
    * @namespace eModule.globals
    * @attribute fcd
    * @type object
    * @since 0.0.5
    **/
    globals.defined('fcd', {
        /**
        * @property A
        * @type Number
        * @final
        * @default 706200001
        **/
        'A' : 706200001
        /**
        * @property BApp
        * @type Number
        * @final
        * @default 706200002
        **/
        , 'BApp' : 706200002
        /**
        * @property BNext
        * @type Number
        * @final
        * @default 706200003
        **/
        , 'BNext' : 706200003
    });
})(window, eModule)
;;(function(window, em){
  var utils = em.utils;
  
  utils.defined('api', function(){
    var _snsBoard = {
        all : function(options){
          utils.ajax('/Board/GetBy', options).get();
        },
        list : function(options){
          utils.ajax('/BoardContent/GetBy' ,options).get();
        },
        del : function(options){
          utils.ajax('/BoardContent/Remove', options).set();
        },
        postSubmit : function(options){
          utils.ajax('/BoardContent/Add', options).set();
        },
        logout : function(options){
        }
      };
    
    return {
      snsBoard : {
        all : function(opt){
          _snsBoard.all(opt);
        },
        list : function(opt){
          _snsBoard.list(opt);
        },
        del : function(opt){
          _snsBoard.del(opt);
        },
        postSubmit : function(opt){
          _snsBoard.postSubmit(opt);
        },
        logout : function(opt){
          _snsBoard.logout(opt);
        }
      }
    };
  });
})(window, eModule);
;; (function (window, em) {
    "use strict";
    var utils = em.utils;
    /**
    * # 쿠키  
    * ---  
    * 쿠키를 만들고, 특정 쿠키의 값을 가져온다.  
    *  
    * ```javascript    
    * eModule.utils.cookie.get([name]);  
    * eModule.utils.cookie.set([name, value]);  
    * ```  
    *  
    * @namespace eModule.utils  
    * @class cookie  
    * @since 0.0.5  
    **/
    utils.defined('cookie' , function(){
        return {
            /**
            * ### gmarket 도메인에 쿠키를 생성한다.  
            *  
            * @method set
            * @param {String} name cookie name
            * @param {String} value cookie value
            * @param {String} [exdays=1]
            * @example
            *  eModule.utils.cookie.set('test' , 'true', 1);
            **/
            set : function(name, value, exdays){
                var date = new Date()
                    , expires = 'expires=';

                date.setTime(date.getTime() + ( ( exdays ?  exdays : 1 ) * 24 * 60 * 60 * 1000 ));
                expires = expires + date.toGMTString();
                document.cookie = name + '=' + value + '; path=/;domain=gmarket.co.kr; ' + expires;
            }
            /**
            * ### 특정 쿠키의 값을 가져온다.  
            *  
            * @method get
            * @param {String} [name]
            * @return {String} cookie value
            * @example
            *  eModule.utils.cookie.get('test');
            **/
            , get : function(name){
                if(name){
                    var nameOfCookie = name + "="
                        , cookies = document.cookie.split(';');
                    for(var i = 0,  len = cookies.length ; i < len ; i++ ) {
                        var cookieValue = cookies[i];
                        while( cookieValue.charAt(0) == ' ' )  {
                            cookieValue = cookieValue.substring(1);
                            if( cookieValue.indexOf(nameOfCookie) != -1 ){
                                return cookieValue.substring(nameOfCookie.length, cookieValue.length);
                            }
                        };
                    };
                }

                return '';
            }
        }
    });

})(window, eModule)
;; (function(window, em){
  'use strict';
  var utils = em.utils;
  
  utils.defined('observableArray', function(){
    function ObservableArray(items) {
        var _self = this,
            _array = [];
    
        _self.onItemAdded = null;
        _self.onItemSet = null;
        _self.onItemRemoved = null;
    
        function defineIndexProperty(index) {
            if (!(index in _self)) {
                Object.defineProperty(_self, index, {
                    configurable: true,
                    enumerable: true,
                    get: function () {
                        return _array[index];
                    },
                    set: function (v) {
                        _array[index] = v;
                        if (typeof _self.onItemSet === "function") {
                            _self.onItemSet(index, v);
                        }
                    }
                });
            }
        }
        
        _self.push = function () {
            var index;
            for (var i = 0, ln = arguments.length; i < ln; i++) {
                index = _array.length;
                _array.push(arguments[i]);
                defineIndexProperty(index);
                if (typeof _self.onItemAdded === "function") {
                    _self.onItemAdded(index, arguments[i]);
                }
            }
            return _array.length;
        };
    
        _self.pop = function () {
            if (~_array.length) {
                var index = _array.length - 1,
                    item = _array.pop();
                delete _self[index];
                if (typeof _self.onItemRemoved === "function") {
                    _self.onItemRemoved(index, item);
                }
                return item;
            }
        };
    
        _self.unshift = function () {
            for (var i = 0, ln = arguments.length; i < ln; i++) {
                _array.splice(i, 0, arguments[i]);
                defineIndexProperty(_array.length - 1);
                if (typeof _self.onItemAdded === "function") {
                    _self.onItemAdded(i, arguments[i]);
                }
            }
            return _array.length;
        };
    
        _self.shift = function () {
            if (~_array.length) {
                var item = _array.shift();
                // _array.length === 0 && delete _self[index];
                if (typeof _self.onItemRemoved === "function") {
                    _self.onItemRemoved(0, item);
                }
                return item;
            }
        };
    
        _self.splice = function (index, howMany /*, element1, element2, ... */ ) {
            var removed = [],
                item,
                pos;
    
            index = !~index ? _array.length - index : index;
    
            howMany = (howMany == null ? _array.length - index : howMany) || 0;
    
            while (howMany--) {
                item = _array.splice(index, 1)[0];
                removed.push(item);
                delete _self[_array.length];
                if (typeof _self.onItemRemoved === "function") {
                    _self.onItemRemoved(index + removed.length - 1, item);
                }
            }
    
            for (var i = 2, ln = arguments.length; i < ln; i++) {
                _array.splice(index, 0, arguments[i]);
                defineIndexProperty(_array.length - 1);
                if (typeof _self.onItemAdded === "function") {
                    _self.onItemAdded(index, arguments[i]);
                }
                index++;
            }
    
            return removed;
        };
    
        Object.defineProperty(_self, "length", {
            configurable: false,
            enumerable: true,
            get: function () {
                return _array.length;
            },
            set: function (value) {
                var n = Number(value);
                if (n % 1 === 0 && n >= 0) {
                    if (n < _array.length) {
                        _self.splice(n);
                    } else if (n > _array.length) {
                        _self.push.apply(_self, new Array(n - _array.length));
                    }
                } else {
                    throw new RangeError("Invalid array length");
                }
                return value;
            }
        });
    
        Object.getOwnPropertyNames(Array.prototype).forEach(function (name) {
            if (!(name in _self)) {
                Object.defineProperty(_self, name, {
                    configurable: false,
                    enumerable: true,
                    writeable: false,
                    value: Array.prototype[name]
                });
            }
        });
    
        if (items instanceof Array) {
            _self.push.apply(_self, items);
        }
    }
    return function(items){
       return new ObservableArray(items);
    };
  });
})(window, eModule)
// (function testing() {
// 
//     var x = new ObservableArray(["a", "b", "c", "d"]);
// 
//     console.log(x.slice());
// 
//     x.onItemAdded = function (index, item) {
//         console.log("Added %o at index %d.", item, index);
//     };
//     x.onItemSet = function (index, item) {
//         console.log("Set index %d to %o.", index, item);
//     };
//     x.onItemRemoved = function (index, item) {
//         console.log("Removed %o at index %d.", item, index);
//     };
//     
//     console.log(x.reverse().slice());
//     
//     x.splice(1, 2, "x");
//     x[2] = "foo";
// 
//     x.length = 10;    
//     console.log(x.slice());
//     
//     x.length = 2;
//     console.log(x.slice());
//     
//     console.dir(x);
// 
// })();
;; (function(window, em){
  'use strict';
  var utils = em.utils;
  
  if(!window.jQuery){
    console.log('paging need jQuery');
    return;
  }
  /**
   * # paging  
   * --- 
   *  
   * paging 함수  
   * > 기존 cj 에서 개발하였던 paging module 수정
   *   
   * ```javascript  
   * $(target).paging({  
   *  totalCount : 100, // 총 리스트 수  
   *  currentPage : 1, // 현재 페이지  
   *  limit : 10, // 한 페이지에 표시할 리스트 수  
   *  offest : 0, // 데이터 시작점  
   *  block : 10, // 한번에 표시하는 페이지의 수  
   *  autoSlide : false, // 현재 페이지를 가운데에 표시하는 기법  
   *  callBack : '' // 페이지 클릭시 callBack 함수  
   * });  
   * ```
   *  
   * @namespace eModule.utils
   * @class paging
   * @uses jQuery
   * @since 2.0
   **/
  utils.defined('paging', function(){
    var _defaults = {
      root : 'body',
      el : '#paging',
      totalCount : 0,
      currentPage : 1,
      //offset : 0,
      limit : 10,
      block : 10,
      autoSlide : false,
      callback:''
    },
    _self = this,
    _pageData = {};
    
    var _event = function(){
      var $el = _defaults.$el || $(_defaults.el);
          
      $el.find('a').not('[class^="selected"]').on( 'click', function( e ){
        e.preventDefault();
        e.stopPropagation();

        var _this = $(this),
            _page = _this.data('page'),
            //_offset = ( _page - 1 ) * _defaults.limit,
            _callback = _defaults.callback;

        //_defaults.currentPage = ( _page / _defaults.limit ) + 1;
        //_defaults.offset = _offset;
        _defaults.currentPage = _page;

        if ( !_this.hasClass('disabled')){
          _this.addClass( 'selected' ).siblings().removeClass( 'selected' );
          $.isFunction(_callback) && _callback(_defaults);
        }
      });
    };
    
    var _format = function(){
      var $el = _defaults.$el = $(_defaults.el),
          cRl = 0,
          tRl = 0,
          max = 0,
          min = 0,
          $data = $.data($el),
          first = 0,
          prev = 0,
          next = 0,
          last = 0,
          std = 0,
          to = 0,
          from = 0,
          total = _defaults.totalCount,
          itemLimit = _defaults.limit,
          pageLimit = _defaults.block,
          isSlide = _defaults.autoSlide,
          //current = ( _defaults.offset / itemLimit ) + 1,
          current = _defaults.currentPage,
          reg = /disabled/gi;
        
        _pageData = {
          first : {
            title : '처음',
            "class" : 'sp_evt prev_end'
          },
          prev : {
            title: '이전',
            "class" : 'sp_evt prev'
          },
          number : {
            title : '페이지',
            "class" : 'sp_evt num'
          },
          next : {
            title : '다음',
            "class" : 'sp_evt next'
          },
          last : {
            title : '마지막',
            "class" : 'sp_evt next_end'
          }
        };
          
      //_defaults.currentPage = current;
      
      if( total !== 0 ){
        if( isSlide ) {
          cRl = ( max ) ? Math.ceil( max / 2 ) : Math.ceil( pageLimit / 2 );
          std = cRl - 1;
          tRl = Math.ceil( total / itemLimit );
          max = ( tRl >= pageLimit ) ? ( ( current <= cRl ) ? cRl + std : current + std ) : tRl;
          min = ( tRl >= pageLimit ) ? ( ( current <= cRl ) ? cRl - std : current - std ) : 1;
          
          if( tRl >= pageLimit && max >= tRl ){
            max = tRl;
            min = max - pageLimit + 1;
          }

        } else {
          cRl = Math.ceil( current / pageLimit );
          tRl = Math.ceil( total / itemLimit );
          max = ( tRl > cRl * pageLimit ) ? cRl * pageLimit : tRl;
          min = ( cRl * pageLimit ) - pageLimit +  1;
        }

        first = 1;
        // next = ( current + pageLimit < tRl ) ? current + pageLimit : tRl;
        // prev = ( current - pageLimit > 0) ? current - pageLimit : 1;
        next = ( current + pageLimit < tRl ) ? max + 1 : tRl;
        prev = ( current - pageLimit > 0) ? min - 1 : 1;
        last = tRl;
        to = min;
        from = max;

        $.data( $el, 'to', to);
        $.data( $el, 'from', from);

        _pageData.first['data-page'] = first;
        _pageData.prev['data-page'] = prev;
        _pageData.next['data-page'] = next;
        _pageData.last['data-page'] = last;

        // _pageData.first['class'] = ( current === tRl ) ? _pageData.first['class'] + ' disabled' : _pageData.first['class'].replace(reg,'').replace(/\s/gi,'');
        _pageData.first['class'] = current === 1  ? _pageData.first['class'] + ' disabled' : _pageData.first['class'].replace(reg,'').replace(/^\s*|\s*$/,'');
        _pageData.prev['class'] = ( min === 1 ) ? _pageData.prev['class'] + ' disabled' : _pageData.prev['class'].replace(reg,'').replace(/^\s*|\s*$/,'');
        _pageData.next['class'] = ( max === tRl ) ? _pageData.next['class'] + ' disabled' : _pageData.next['class'].replace(reg,'').replace(/^\s*|\s*$/,'');
        // _pageData.last['class'] = ( max === tRl && current > 1 ) ? _pageData.last['class'] + ' disabled' : _pageData.last['class'].replace(reg,'').replace(/\s/gi,'');
        _pageData.last['class'] = ( max === tRl && current === max) ? _pageData.last['class'] + ' disabled' : _pageData.last['class'].replace(reg,'').replace(/^\s*|\s*$/,'');
      } else {
        $el.empty();
      }
    };
    
    var _render = function(){
      var $el = _defaults.$el || $(_defaults.el),
        $data = $.data($el),
        current = _defaults.currentPage,
        to = $data.to,
        from = $data.from,
        // nav = '<span class="page">{{contents}}</span>',
        contents = '',
        numbers = '';
      $.each(_pageData, function(index, value){
        if(index !== 'number'){
          var klass = _pageData[index]['class'];
          if(klass.indexOf('disabled') !== -1){
            contents = contents + '<span class="'+ klass +'" data-page="'+ _pageData[index]['data-page']+'"><span class="blind">'+ _pageData[index].title+'</span></span>';
          } else {
            contents = contents + '<a href="#" class="'+ klass + '" data-page="'+ _pageData[index]['data-page']+'"><span class="blind">' + _pageData[index].title + '</span></a>';
          }
        } else {
          contents = contents + '{{numbers}}';
          
          for(var i = to , len = from; i <= len ; i++){
            if(i === current){
              numbers = numbers + '<a href="#" data-page="'+ i +'" class="num selected">'+ i +'</a>';
            } else {
              numbers = numbers + '<a href="#" data-page="'+ i +'" class="num">'+ i +'</a>';
            }
          }
        }
      });
      contents = contents.replace(/\{\{numbers\}\}/, numbers);
      // nav = nav.replace(/\{\{contents\}\}/, contents);
      $el.html(contents);
    };
    
    return {
      init : function(options){
          _defaults = utils.extend(_defaults, options);
          _format();
          return this;
      },
      render : function(){
          _render();
          _event();
      },
      setOptions : function(options){
          _defaults = utils.extend(_defaults, options);
          _format();
          this.render();
      }
    };
  });
})(window, eModule)
;;(function(window, em){
  'use strict';
  var utils = em.utils,
      globals = em.globals;
  
  utils.defined('render',function(){
    var _templates = {
        sns : globals.boards.sns
    };
    var _regExp = {
      
    };
    var _getTemplate = function(templateName){
      var templates = '';
      for(var i = 0, names = templateName.split('.'), len = names.length; i < len ;i++){
        templates = templates && typeof templates === 'object' ? templates[names[i]] : globals[names[i]];
    }
      return templates === '' ? console.log('not existed template.') : templates;
    };
    
    var _getExp = function(templateName){
      var name = templateName.replace('.','_');
      return _regExp[name] ? _regExp[name] : _regExp[name] = {};
    };
    
    return {
      // board : {
      //   snsTable : function(){
      //     return _templates.sns ? _templates.sns.table : '';
      //   },
      //   snsList : function(){
      //     return _templates.sns ? _templates.sns.list : '';
      //   },
      //   snsConfirm : function(){
      //     return _templates.sns ? _templates.sns.confirm : '';
      //   },
      //   snsReply : function(){
      //     return _templates.sns ? _templates.sns.reply : '';
      //   },
      //   snsBox : function(){
      //     return _templates.sns ? _templates.sns.snsBox : '';
      //   },
      //   profileImage : function(){
      //     return _templates.sns ? _templates.sns.profileImage : '';
      //   },
      //   profileImageReply : function(){
      //     return _templates.sns ? _templates.sns.profileImageReply : '';
      //   },
      //   icons : function(){
      //     return _templates.sns ? _templates.sns.icons : '';
      //   },
      //   buttons : function(){
      //     return _templates.sns ? _templates.sns.buttons : '';
      //   },
      //   snsDatas : function(){
      //     return _templates.sns ? _templates.sns.snsDatas : '';
      //   },
      //   emptyList : function(){
      //     return _templates.sns ? _templates.sns.emptyList : '';
      //   }
      // }
      get : function(templateName , datas){
        //console.log(templateName);
        var template = _getTemplate(templateName),
            exp = _getExp(templateName),
            returnValue = '',
            name = '';
        if(datas){
          for(var i =0, len = datas.length ; i < len; i++){
            var tmpl = '';
            if( typeof datas[i] === 'object'){
                for(var prop in datas[i]){
                    if(exp[prop]){
                      tmpl = tmpl !== '' ? tmpl.replace(exp[prop], datas[i][prop]) : template.replace(exp[prop], datas[i][prop]);
                    } else {
                      var reg = '\\{\\{' + prop + '\\}\\}';
                      exp[prop] = new RegExp(reg, 'gi');
                      tmpl = tmpl !== '' ? tmpl.replace(exp[prop], datas[i][prop]) : template.replace(exp[prop], datas[i][prop]);
                    }
                }
            }
            returnValue = returnValue + tmpl;
          }
        } else {
          returnValue = template;
        }
        return returnValue;
      }
    };
  });
})(window, eModule);
;; (function (window, em) {
    "use strict";
    /**
    * # App 또는 market 으로 이동    
    * ---    
    *
    * App 이 설치되어 있다면 앱을 실행하고, 해당 페이지로 이동, App 이 설치되어 있지 않다면 마켓으로 이동한다.  
    * IOS 와 Android 모두 적용되며,  
    * Android 의 경우 Android 기본 브라우저와 Chrome 브라우저일 경우와 그 이외의 경우로 나뉜다.  
    *  
    *  
    * ```javascript  
    * eModule.utils.scheme.link([name]);  
    * ```  
    *
    * @namespace eModule.utils
    * @class scheme
    * @since 0.0.5
    **/
    /**
    * ### Android 버전  
    *  
    * @private
    * @return {String} Android Veersion
    **/
    function getAndroidVersion(){
        var androidVersion = -1
            , ua = navigator.userAgent.toLowerCase()
            , androidIndex = ua.indexOf('android');

        if (androidIndex != -1) {
            var versionStartIndex = androidIndex + 8;
            var versionEndIndex = ua.indexOf(';', versionStartIndex);

            androidVersion = parseFloat(ua.slice(versionStartIndex, versionEndIndex));
        }

        return androidVersion;
    };
    
    var utils = em.utils
        , scheme = {};
    
    utils.scheme = utils.scheme || scheme;

    var uagent = navigator.userAgent.toLocaleLowerCase();
    if (uagent.search("android") > -1) {
        scheme.os = "android";
        scheme.androidVersion = getAndroidVersion && getAndroidVersion();
        if (uagent.search("chrome") > -1) {
            if(uagent.search("opr") > -1){
                scheme.browser = "android+opera";
            } else if(uagent.search("opera") > -1){
                scheme.browser = "android+opera";
            } else {
                scheme.browser = "android+chrome";
            }
        } else if(uagent.search("naver") > -1){
            scheme.browser = "android+naver";
        } else if(uagent.search("Firefox") > -1 ){
            scheme.browser = "android+firefox";
        }
    } else if (uagent.search("iphone") > -1 || uagent.search("ipod") > -1 || uagent.search("ipad") > -1) {
        scheme.os = "ios";
    }
    var app = {
        gmarket: { // 앱이름을 정의(ex:카카오톡)
            base_url: "gmarket://",//앱 콜백주소를 적용
            store: {
                android: "market://details?id=com.ebay.kr.gmarket", //안드로이드일경우설치주소
                ios: "http://itunes.apple.com/kr/app/gmarket/id340330132?mt=8"//아이폰일경우설치주소
            },
            package: "com.ebay.kr.gmarket"
        }
    };
    /**
    * @constructor link
    * @param {String} name init scheme info
    * @return this.send()
    * @example
    *  eModule.utils.scheme.link('gmarket');
    **/
    scheme.link = function (name) {
        var link_app = app[name];
        if (!link_app) return {
            send: function () {throw "No App exists";}
        };
        return {
            /**
            * ### 만약 사용자 기기에 App 이 설치 되어 있다면, App 으로 이동, 설치되어 있지 않다면 마켓으로 이동하는 scheme 을 만든다.  
            *  
            * @method send
            * @param {Object} [params] link option set
            * @example
            *  eModule.utils.scheme.link('gmarket').link({  
            *    schemeUrl : [app scheme url]
                , referrerUrl : [referrer url]
            *  });
            **/
            send: function (params) {
                var _app = this.app;
                var full_url = _app.base_url + params.schemeUrl;
                var install_block = (function (os) {                    
                    return function () {
                        window.location = _app.store[os] + '&' + params.referrerUrl;
//                        window.location = _app.store[os];
                    };
                })(this.os);

                if (this.os == "ios") {
                    setTimeout(install_block, 25);
                    window.location = full_url;
                } else if (this.os == "android") {
                    if (this.browser == 'android+naver' || (this.browser == "android+chrome" && this.androidVersion >= 4.3) ) {
                        window.location = "intent:" + full_url + "#Intent;action=android.intent.action.VIEW;category=android.intent.category.BROWSABLE;package=" + _app.package + ";end;";
                    }
                    else
                    {
                        var iframe = document.createElement('iframe');
                        iframe.style.visibility = 'hidden';
                        iframe.src = full_url;
                        iframe.onload = install_block;
                        document.body.appendChild(iframe);
                    }
                }
            },
            /**
            * @property app
            * @type String
            **/
            app: link_app,
            /**
            * ### 현재 사용자 모바일 기기의 정보를 보여준다.  
            *  
            * @property os
            * @type String
            **/
            os: scheme.os,
            /**
            * ### 현재 사용자의 모바일 브라우저의 정보를 보여준다.  
            *  
            * @property browser
            * @type String
            **/
            browser : scheme.browser,
            /**
            * ### Android Version  
            *  
            * @property androidVersion
            * @type String
            **/
            androidVersion : scheme.androidVersion
        };
        /**
        * ### object 형을 나열형(string)으로 변경  
        *  
        * @private
        * @function serialized
        * @param {Object} [params]
        **/
        function serialized(params) {
            var stripped = [];
            for (var k in params) {
                stripped.push(k + "=" + encodeURIComponent(params[k]));
            }
            return stripped.join("&");
        }
    };
})(window, eModule)
;;(function(window, em){
  var utils = em.utils;
  
  utils.defined('snsUtils', function(){
    function Facebook(options){
      var self = this;
      this._defaults = {
        appkey : '',
        scretkey : '',
        callback : '',
        kind : 'FB',
        profile_image_url : '',
        link : '',
        screen_name : '',
        name : '',
        id : '',
        accessToken : ''
      };
      
      this._defaults = utils.extend(self._defaults, options || {});
      
      this._complete = function(){
        var value = $.param(self._defaults);
        utils.cookie.set('FB', value, '1');
        location.reload();
      };
      this._getProfileImage = function(){
        FB.api("/me/picture", {
          width : '68',
          height : '68'
        }, function(response){
          self._defaults.profile_image_url = response.data.url;
          self._complete();
        });
      };
      this._user = function(){
        FB.api("/me", function(response){
          self._defaults.screen_name = response.name;
          self._getProfileImage();
        });
      };
      
      this._statusChangeCallback = function(response) {
        if (response.status === 'connected') {
          self._facebookLogin();
        } else {
          self._facebookLogin();
        }
      };
      
      this._facebookLogin = function(){
        
          if(parent){
            parent.location.href = 'https://graph.facebook.com/oauth/authorize?client_id=1403722356549612&scope=publish_stream&redirect_uri=' + encodeURIComponent('http://md9.gmarket.co.kr/Sns/FacebookCallBack?returnUrl=' + parent.location.href);
        }
        else{
            location.href = 'https://graph.facebook.com/oauth/authorize?client_id=1403722356549612&scope=publish_stream&redirect_uri=' + encodeURIComponent('http://md9.gmarket.co.kr/Sns/FacebookCallBack?returnUrl=' + location.href);
        }
      };

    }
    Facebook.prototype.init = function(){
      var self = this;
      (function(d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "http://connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
      }(document, 'script', 'facebook-jssdk'));
      
      window.fbAsyncInit = function() {
        FB.init({
          appId      : self._defaults.appkey ? self._defaults.appkey : self._defaults.scretkey,
          cookie     : true,  // enable cookies to allow the server to access 
                              // the session
          xfbml      : true,  // parse social plugins on this page
          version    : 'v2.1' // use version 2.1
        });
        
         FB.getLoginStatus(function(response) {
           if( response.status === 'connected'){
             self._defaults.accessToken = response.authResponse.accessToken;
           }
         }, true);
      };
      
      $.ajax({
        url : '/sns/getshortenlink',
        data : {
          planId : self._defaults.planId
        },
        success : function(result){
          self._defaults.shortenlink = result;
        }
      });
    };
    Facebook.prototype.post = function(options){
      var self = this;
      
      FB.api('/me/feed', 'POST', {
        'message' : options.message,
        'link' : self._defaults.shortenlink ? self._defaults.shortenlink : '',
        'link_url' : self._defaults.shortenlink ? self._defaults.shortenlink : '',
        'access_token' : self._defaults.accessToken,
        'picture' : options.picture,
        'name' : self._defaults.planTitle
      });
    };
    Facebook.prototype.checkLoginState = function(){
      var self = this;
      FB.getLoginStatus(function(response) {
        self._statusChangeCallback(response);
      });
    };
    Facebook.prototype.logout = function(){
      utils.cookie.set('FB','','-1');
      location.reload();
    };
    
    function Twitter(){}
    
    Twitter.prototype.init = function(){
      (function (d, s, id) {
        var t, js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id; js.src= "https://platform.twitter.com/widgets.js";
        fjs.parentNode.insertBefore(js, fjs);
        return window.twttr || (t = { _e: [], ready: function (f) { t._e.push(f) } });
      }(document, "script", "twitter-wjs"));
    };
    
    Twitter.prototype.checkLoginState = function(){
        if(parent) {
              parent.location.href = '/sns/twitter?returnurl=' + parent.document.location.href;
          }
          else {
            location.href = '/sns/twitter?returnurl=' + document.location.href;
          }  
      //location.href = '/sns/twitter?returnurl=' + parent.document.location.href;
      //window.open('/sns/twitter?returnurl=' + parent.document.location.href, 'twitter-login');
    };
    Twitter.prototype.post = function(options){
      $.ajax({
        url : '/sns/update',
        data : {
          status : options.message,
          planId : options.planId
        },
        success : function(result){
          //console.log('success tw post');
        }
      });
    };
    Twitter.prototype.logout = function(){
      utils.cookie.set('TW','','-1');
      location.reload();
    };
    return {
      facebook : function(opt){
        return new Facebook(opt);
      },
      twitter : function(){
        return new Twitter();
      }
    };
  });
})(window, eModule);
;; (function (window, em) {
    "use strict";
    var apps = em.apps
        , globals = em.globals
        , utils = em.utils;
    /**
    * # 랜딩 페이지 버튼 클릭 트래킹 관리 함수  
    * ---  
    * 임의의 iframe 을 호출하여 트래킹을 한다.  
    *  
    * ```javascript  
    * eModule.utils.wiselog.send([type, callback]);  
    * ```  
    *  
    * @namespace eModule.utils
    * @class wiselog
    * @extend globals
    * @uses fcd
    * @since 0.0.5
    **/
    utils.defined('wiselog', function(){
        var _fcd = globals.fcd
            , _el = 'wiseLog'
            , $iframe = null;

        var _send = function(type){
            if(!$iframe){
                $("#wrap").append('<iframe src="" style="display:none" id="wiseLog"></iframe>');
                $iframe = $("#wiseLog");
            };
            $iframe.attr('src' , 'http://sna.gmarket.co.kr?fcd=' + _fcd[type]);
        };
        /**
        * ### 트래킹 페이지에 해당 fcd 값을 전달한다.  
        *  
        * @method send
        * @param {String} type click type for landing banner button.
        * @param {function} [callback] callback function
        **/
        return {
          send : function(type, callback){
            typeof callback === 'function' && callback();
            _send(type);
          }
        };
    });
})(window, eModule)
;; (function (window, em) {
    "use strict";
    var apps = em.apps
        , utils = em.utils;
    /**
    * # Gmarket App 설치 유도 베너  
    * ---  
    *  
    *  사용자가 모바일 웹으로 gmarket 에 접속시  
    * landing Banner 레이어가 화면에 표시되어  
    * 사용자가 G마켓 App 을 설치하거나  
    * G마켓 App 을 통해 상품을 검색하도록 유도한다.  
    *  
    * ```javascript  
    * $(document).ready(function(){  
    *  var em = eModule  
    *   , apps = em.apps  
    *   , utils = em.utils  
    *   , globals = em.globals;  
    *  
    *  apps.landing.init({  
    *   landingType : 'A'  // ['A','B','None']  
    *   , ladningDisplay : '[ladning Image Description]'  
    *   , imageUrl : '[landing Image Url]'  
    *  });  
    * });  
    * ```  
    *  
    * @namespace eModule.apps
    * @class lading
    * @extends utils
    * @uses wiselog
    * @uses scheme
    * @since 0.0.5
    **/
    apps.defined('landing', [utils.wiselog, utils.scheme], function (wiselog, scheme) {
        var _defaults = {
            root: '#wrap'
            , content : '#content'
            , el: '#lay_notice'
            , pageName : 'home'
            , landingType : 'B' //type : A, B, none ( 노출하지 않음 )
            , imageUrl: ''
            , imageDomain: 'http://image.gmarket.co.kr'
            , description: ''
            , messages: {
                btn: {
                    goToApp: 'G마켓 APP으로 보기'
                    , close: '다음에 받기'
                }
            }
            , installAppUrl: 'gmarket://'
            , templates: {
                'wrapper': '<div id="lay_notice" class="lay_notice"><div class="lay_area">{{inner}}</div></div>'
                , 'A': '<span class="img_bx">{{image}}</span>'+
				        '<div class="btn_bx">' +
					        '<a href="#" id="goToApp"><img src="{{imageDomain}}/mobile/banner/btn_large_go_01.png" alt="{{goToApp}}"/></a>' +
				        '</div>'
                , 'B': '<span class="img_bx">{{image}}</span>' +
				        '<div class="btn_bx">' +
					        '<a href="#" id="goToApp"><img src="{{imageDomain}}/mobile/banner/btn_small_go_01.png" alt="{{goToApp}}"/></a>' +
                            '<a href="#" id="landingClose"><img src="{{imageDomain}}/mobile/banner/btn_small_next_01.png" alt="{{close}}"/></a>' +
				        '</div>'
                , 'ETC': ''
                , 'image' : '<img src="{{imageUrl}}" alt="{{description}}">'
                , 'mask': '<div id="mask" selected="true"></div>'
            }
            , campaign : ''
            , medium : ''
            , mediumSource : ''
            , mediumContent : ''
            , term : ''
            , isIos : false
        };
        //set defaults
        var _set = function () {
            for (var prop in options) {
                _defaults[prop] = options[prop];
            };
        };
        var _referr = function(){
             var _ref = {
                    'utm_campaign' : _defaults.campaign
                    , 'utm_medium' : _defaults.medium
                    , 'utm_source' : _defaults.mediumSource
                    , 'utm_content' : _defaults.mediumContent
                    , 'utm_term' : _defaults.term
                }
                , _url = '';

            for(var item in _ref){
                if( _url === ''){
                    _url = item + '=' + _ref[item];
                } else {
                    _url = _url + '&' + item + '=' + encodeURIComponent(_ref[item]);
                }
            }
            return _url;
        };
        var _schemeUrl = function(path , search){
            var returnValue = ''
                , params = '';

            if( search !== '' ) {
                params = utils.params(search);

                for(var prop in params){
                    if(prop !== 'landingtype' && prop !== 'landingdisplay'){
                        returnValue = ( returnValue == '' ? '?' : '&' )+ prop + '=' + params[prop];
                    }
                };

            }
            return path + returnValue;
        };
        var _setInstallAppUrl = function () {
            var _location = location
                , regExp = new RegExp('\\/')
                , _domain = _location.origin
                , _pathname = _location.pathname.replace('/','')
                , _search = _location.search.replace('?', '')
                , _parentPage = regExp.test(_pathname) ? _pathname.split(regExp)[0].toLowerCase() : _pathname.toLowerCase()
                , _childrenPage = regExp.test(_pathname) ? _pathname.split(regExp)[1].toLowerCase() : _pathname.toLowerCase()
                , _installAppUrl = _defaults.installAppUrl
                , _appUrl = ''
                , _isIos = _defaults.isIos
                , schemeUrl = _domain + '/' + _schemeUrl(_pathname, _search)
                , shopInfo = {
                    '1' : '브랜드온'
                    , '2' : ''
                    , '3' : '해외직구'
                    , '4' : '장전G'
                    , '5' : 'G맘클럽'
                    , '6' : '소호'
                    , '7' : ''
                    , '8' : '마트ON'
                };
            switch (_parentPage) {
                case 'item':
                    var _goodscode = utils.params(_search).goodscode || '';
                    _appUrl = 'item?itemid=' + _goodscode + '&jaehuid=';
                    break;
                case 'best':
                    _appUrl = 'main?best';
                    break;
                case 'superdeal':
                    _appUrl = 'main?superdeal';
                    break;
                case 'pluszone':
                    _appUrl = 'main?pluszone';
                    break;
                case 'ecoupon':
                    _appUrl = 'main?ecoupon';
                    break;
                case 'look' :
                    _appUrl = 'main?look';
                    break;
                case 'mark' :
                    _appUrl = 'main?mark';
                    break;
                case 'giftcard' :
                    _appUrl = 'openwindow?title=' + encodeURIComponent('선물권') + '&targetUrl=' +  schemeUrl;
                    break;
                case 'search':
                    var _topkeyword = utils.params(_search).topkeyword || ''
                        , _params = 'keyword=' + _topkeyword;
                    _appUrl ='search?type=srp&' + _params;
                    break;
                case 'category':
                    var type = ''
                        , lcId = utils.params(_search).lcid
                        , mcId = utils.params(_search).mcid;
                    if(_childrenPage == 'list'){
                        type = 'lp';
                        _appUrl = 'search?type=lp&lcId=' + lcId + '&mcId=' + mcId;
                    } else {
                        type = 'cpp';
                        _appUrl = 'search?type=cpp&lcId=' + lcId;
                    }
                    break;
                case 'main' :
                    _appUrl = 'main';
                    break;
                case 'home' :
                    if(location.host === 'mmyg.gmarket.co.kr'){ //나의 쇼핑정보
                        _appUrl = 'openwindow?title=' + encodeURIComponent('나의쇼핑정보메인') + '&targetUrl=' +  schemeUrl;
                    } else {
                        _appUrl = 'main';
                    }
                    break;
                case 'contractlist' :
                    _appUrl = 'openwindow?title=' + encodeURIComponent('주문내역') + '&targetUrl=' +  schemeUrl;
                    break;
                case 'event':
                    _appUrl = 'openwindow?title=' + encodeURIComponent('이벤트') + '&targetUrl=' +  schemeUrl;
                    break;
                case 'display':
                    _appUrl = 'openwindow?title=' + encodeURIComponent('기획전') + '&targetUrl=' +  schemeUrl;
                    break;
                case 'vertical':
                    var shopname = shopInfo[utils.params(_search).shopseq];
                    _appUrl = 'openwindow?title=' + encodeURIComponent(shopname) + '&targetUrl=' +  schemeUrl;
                    break;
                case 'shop' :
                    _appUrl = 'openwindow?title=' + encodeURIComponent('롯데백화점') + '&targetUrl=' +  schemeUrl;
                    break;
                default:
                    _appUrl = 'main';
                    break;
            };
            _defaults.installAppUrl = _appUrl;
        };

        //render landing banner
        var _landingBanner = function () {
            var $root = _defaults.$root = $(_defaults.root)
                , $content = _defaults.$content = $(_defaults.content).length > 0 ? $(_defaults.content) : $root
                , type = _defaults.landingType === '' ? 'A' : _defaults.landingType
                , wrapperTemplate = _defaults.templates.wrapper
                , innerTemplate = _defaults.templates[type]
                , maskTemplate = _defaults.templates.mask
                , imageTemplate = _defaults.templates.image
                , datas = {
                    imageUrl: _defaults.imageUrl
                    , imageDomain: _defaults.imageDomain
                    , description: _defaults.description
                    , goToApp: _defaults.messages.btn.goToApp
                    , close: _defaults.messages.btn.close
                }
                ,landingTemplate = '';
            for (var prop in datas) {
                var regexp = new RegExp('\{\{' + prop + '\}\}', 'g');
                innerTemplate = innerTemplate.replace(regexp, datas[prop]);
            };
            if( datas.imageUrl !== '' ){
                   imageTemplate = imageTemplate.replace(/\{\{imageUrl\}\}/, datas.imageUrl).replace(/\{\{description\}\}/, datas.description);
            } else{
                imageTemplate = '';
            }
            innerTemplate = innerTemplate.replace(/\{\{image\}\}/, imageTemplate);
            landingTemplate = wrapperTemplate.replace(/\{\{inner\}\}/, innerTemplate);

            $root.append(maskTemplate);
            $content.append(landingTemplate);
        };

        return {
            /**
            * ### 랜딩 베너 데이터 초기화  
            * > - 예외 ) 아이폰, 아이패드로 접속 시 VIP 페이지에서만 랜딩베너 초기화  
            *  
            * @method init
            * @param options {object} landingbanner data init
            * @example
            * ```javascript
            *   eModule.apps.landing.init([options]);
            * ```
            *
            **/
            init: function (options) {
                var landingType = ''
                    , pageName = '';
                _defaults = utils.extend(_defaults, options);
                _defaults.isIos = (/iphone|ipod|ipad/).test(navigator.userAgent.toLowerCase());
                landingType = _defaults.landingType.toLowerCase();
                pageName = _defaults.pageName.toLowerCase();
                if(_defaults.isIos){
                    if(pageName == 'vip' && landingType !== 'none'){
                        this.render();
                    }
                } else {
                    if(landingType !== 'none' && pageName !== 'srp' && pageName !== 'lp' && pageName !== 'cpp' && pageName !== 'contractlist' && pageName !== 'mmyghome' && pageName !== 'specialshopping'){
                        this.render();
                    }
                }
//                 if(landingType !== 'none'){
//                    this.render();
//                }
            }
            /**
            * ### 배너 마크업 생성  
            *  
            * @method render
            **/
            , render: function () {
                //make install app url
                _setInstallAppUrl();
                //open landingBanner
                _landingBanner();
            }
            /**
            * ### Gmarket App 으로 이동 이벤트  
            *  
            * @event installApp
            * @example
            * ```javascript
            *   eModule.apps.landing.installApp();
            * ```
            *
            **/
            , installApp: function () {
                var type = _defaults.landingType.toUpperCase() === 'A' ? 'A' : 'BApp'
                    , refUrl = 'referrer=' + _referr();
                wiselog.send(type, function(){
                    scheme.link('gmarket').send({
                        schemeUrl : _defaults.installAppUrl
                        , referrerUrl : refUrl
                    });
                });
            }
            //close landing page
            /**
            * ### 배너 닫기 이벤트  
            *  
            * @event close
            * @example
            * ```javascript
            *   eModule.apps.landing.close();
            * ```
            **/
            , close: function () {
                _defaults.$content.find(_defaults.el).hide();
                _defaults.$root.find('#mask').remove();
                wiselog.send('BNext');

            }
        }
    });

})(window, eModule)
;; (function(window, em){
  'use strict';
  var apps = em.apps,
      utils = em.utils, //TODO if don't useed, then remove please.
      globals = em.globals; //TODO if don't useed, then remove please.
  
  apps.defined('snsBoard',[globals.snsAttributes, utils.observableArray, utils.paging, utils.api, utils.render, utils.cookie, utils.snsUtils], function(snsAttributes, observableArray, paging, api, render, cookie, snsUtils){
    var _defaults = {
      root : 'body',
      el : '',
      boardID : '',
      gmarketLogin : 'False',
      elPaging :'',
      sharedSNSKinds : [],
      selectedSNS : '',
      userAuthorizeSNSInfo : {},
      BoardType: 'Sns',
      BottomLineColor: '#FF3333',
      ButtonColor: '#FF3333',
      ButtonTextColor: '#FFFFFF',
      CountPerPage: 0,
      DuplicationRegistrationCountPerDay: 0,
      DuplicationRegistrationType: 'N',
      EndedDate: '',
      EventID: 0,
      FacebookThumbnailImage: '',
      IsEnableRegist : true,
      IsEnabledDelete: true,
      IsEnabledReply: true,
      IsPreventCopy: true,
      IsPreventEnglish: true,
      IsPreventRepeatedWord: true,
      LanguageType: 'K',
      PlanID: 0,
      PlanTitle: '',
      SnsKinds: [],
      StartedDate: '',
      TextMaxLength: 140,
      TextMinLength: 0,
      TopLineColor: '#FF3333',
      logoutMessages : {
        'K' : 'G마켓에서 로그인된 각 서비스를 로그아웃합니다.',
        'E' : 'You are logged in log out.'
      },
      listMessages : {
        'K' : {
          noLists : '등록된 글이 없습니다.'
        },
        'E' : {
          noLists : 'There is no records.'
        }
      },
      validateMesaages : {
        'K' : {
          empty : '글을 입력해 주세요.',
          gmarketLogin : '로그인 후 이용하실 수 있습니다.',
          snsLogin : '글을 보내실 SNS에 로그인해 주세요.',
          minLength : '글자수가 부족합니다.(최소 {{min}}자)\n수정후 등록해주세요.',
          maxLength : '최대 {{max}}자 까지만 입력 가능합니다.',
          repeatWord : '동일한 문자가 5회 이상 반복되었습니다.\n수정후 등록해주세요.',
          enabledEnglish : '영어사용이 금지되었습니다.\n수정후 등록해주세요.',
          selectedSNS : '글을 보내실 SNS를 선택해 주세요.',
          delete : '삭제하시겠습니까?',
          regist :'게시판 사용 기간이 아닙니다.',
          copypaset : '복사하기, 붙여넣기는 사용하실 수 없습니다.'
        },
        'E' : {
          empty : 'Please write a message.',
          gmarketLogin : 'Please, sign-in first.',
          snsLogin : 'Please sign-in SNS site to share your message.',
          minLength : 'You are allowed a minimum of {{min}} letters.',
          maxLength : 'You are allowed a maximum of {{max}} letters.',
          repeatWord : 'You are not allowed to write same word patterns more than 5 times.\nPlease check again.',
          enabledEnglish : '영어사용이 금지되었습니다.\n수정후 등록해주세요.',
          selectedSNS : 'Please select SNS site to share your message.',
          delete : '삭제하시겠습니까?',
          regist :'It is not the period to be able to use this board.',
          copypaset : 'Copy, paste can not be used.'
        }
      },
      formMessages : {
        'K' : {
          login : 'SNS에 로그인 후 작성해주세요.',
          logout : '로그아웃',
          placeholder : '작성하신 글은 로그인 하신 SNS에 함께 게시됩니다.',
          submit : '확인'
        }, 
        'E' : {
          login : 'Leave a message after signing-in the SNS.',
          logout : 'Sign-out',
          placeholder : 'Your comment will post not only here but also on your timeline.',
          submit : 'submit'
        }
      },
      buttonMessages : {
        'K' : {
          reply : '답글',
          delete : '삭제'
        },
        'E' : {
          reply : 'Reply',
          delete : 'Delete'
        }
      },
      pageIndex : 1,
      templates : {
      },
      regExpGroup : {
        table : {
          
        },
        list : {
          
        },
        reply : {
          
        },
        snsBox : {
          
        },
        profileImage : {
          
        },
        snsDatas : {
          
        },
        buttons : {
          
        },
        confirm : {
          
        }
      }
    };
    var _listData = observableArray();
    var _renderLists ='';
    function _loginUrl(){
      var location = parent.document.location,
          protocol = location.protocol + '//',
          host = location.host,
          pathname = location.pathname,
          search = location.search,
          currentUrl = protocol + host + pathname + search,
          //returnUrl = host.indexOf('md9') > -1 ? 'http://mdev.gmarket.co.kr/Login/login_mw.asp?URL=' + currentUrl : 'http://m.gmarket.co.kr/Login/login_mw.asp?URL=' + currentUrl;
          returnUrl = 'http://mdev.gmarket.co.kr/Login/login_mw.asp?URL=' + currentUrl;
      return returnUrl;
    }
    function _getSNSInfo(){
      var cookieValue = '',
          snsKinds = _defaults.SnsKinds,          
          type = '';
      
      for(var i = 0 , len = snsKinds.length ; i < len ; i++){
        type = snsKinds[i];
        cookieValue = cookie.get(type) ? cookie.get(type) : null;
        var json = {};
        
        if( cookieValue ){
          for(var h = 0 , index = cookieValue.split('&'), hlen = index.length ; h < hlen ; h++){
            for(var j = 0 , data = index[h].split('='), jlen = data.length; j < jlen ; j++){
               var key = data[0],
                   value = decodeURIComponent(data[1]);
               
               json[key] = value;
            }
          }
        } else {
          json = cookieValue;
        }
        _defaults.userAuthorizeSNSInfo[type] = json;
      }       
    }
    function _setSelectedSNS(){
      var snsKinds = _defaults.SnsKinds,
          snsKindsLength = snsKinds.length,
          snsType = '',
          snsSelected = [],
          authorizeSNS = _defaults.userAuthorizeSNSInfo;
      
      for(var i = 0 ; i < snsKindsLength; i ++){
        snsType = snsKinds[i];
        
        if( authorizeSNS && authorizeSNS[snsType] ){
            snsSelected.push(snsType);
        }      
      }
      
      _defaults.selectedSNS = snsSelected.length > 0 ? snsSelected[0] : '';      
    }
    function _initSNS(){
      for(var i = 0, len = _defaults.SnsKinds.length ; i < len ; i++){
        if(_defaults.SnsKinds[i] === 'FB'){
          _defaults.$FB = snsUtils.facebook({
              appkey: '1403722356549612',
              scretkey: '533c349c22929cde37a5ae15117175a6',
              callback: 'http://mobiledev.gmarket.co.kr/Sns/FacebookCallBack',
              planId : _defaults.PlanID ? _defaults.PlanID : 0,
              planTitle : _defaults.PlanTitle ? _defaults.PlanTitle :'',
          });
          _defaults.$FB.init();
        } else if(_defaults.SnsKinds[i] === 'TW'){
          _defaults.$TW = snsUtils.twitter();
          _defaults.$TW.init();
        }
      }
    }
    function _eventBinding($el){
      var pasttime = null;
      var ua = navigator.userAgent.toLowerCase();
      var isAndroid = ua.indexOf('android') > -1;
      if(isAndroid && _defaults.IsPreventCopy){
        $el.on('contextmenu','textarea', function(e){
          e.preventDefault();
          alert(_defaults.validateMesaages[_defaults.LanguageType].copypaset);
        });
      }
      $el.on('copy', 'textarea', function(e){
        if( _defaults.IsPreventCopy ){
            e.preventDefault();
            alert(_defaults.validateMesaages[_defaults.LanguageType].copypaset);
        }
      }).on('paste', 'textarea', function(e){
        //alert('paste');
        if( _defaults.IsPreventCopy ){
            e.preventDefault();
            alert(_defaults.validateMesaages[_defaults.LanguageType].copypaset);
        } else {
          var _this = $(this);
          pasttime = setTimeout(function(){
              var text = _this.val();
              _this.next().find('strong').text(text.length);
              clearTimeout(pasttime);
          }, 100);
        }
      }).on('cut', 'textarea', function(e){
        if( _defaults.IsPreventCopy ){
            e.preventDefault();
            alert(_defaults.validateMesaages[_defaults.LanguageType].copypaset);
        }
      }).on('keyup','textarea', function(e){
        var text = $(this).val();
        $(this).next().find('strong').text(text.length);
      }).on('change','textarea', function(e){
        var text = $(this).val();
        $(this).next().find('strong').text(text.length);
      });
      $el.on('click','.js-move-usersns', function(e){
        e.preventDefault();
        _loginCheck();
        if(_defaults.selectedSNS === 'FB'){
          //window.open('http://m.facebook.com/me', '_blank');
          parent.location.href = 'http://m.facebook.com/me';
        } else if(_defaults.selectedSNS === 'TW'){
          //window.open('http://m.twitter.com/' + _defaults.userAuthorizeSNSInfo.TW.screen_name, '_blank');
          parent.location.href = 'http://m.twitter.com/' + _defaults.userAuthorizeSNSInfo.TW.screen_name;
        }
      });
      $el.on('click','.js-change-profileimage', function(e){
        e.preventDefault();
        $el.find(".js-close-reply").trigger('click');
        for(var prop in _defaults.userAuthorizeSNSInfo){
          if(prop !== _defaults.selectedSNS){
            _defaults.selectedSNS = prop;
            _changeProfile($(this));
            return false;
          }
        }
      });
      $el.on('click', '.js-goto-sns', function(e){
        e.preventDefault();
        var $img = $(this),
            $parent = $img.parent(),
            snsid = $parent.data('snsid'),
            snskind = $parent.data('snskind');
        
        if(snskind === 'f'){
          //window.open('http://www.facebook.com/' + snsid, '_blank');
          parent.location.href = 'http://www.facebook.com/' + snsid;
        } else if(snskind === 't'){
          //window.open('http://www.twitter.com/'+ snsid, '_blank');
          parent.location.href = 'http://www.twitter.com/'+ snsid;
        }
      });
      //example
      //$('.mainPrd iframe', window.parent.document).attr('width', $('body').outerWidth());
      //$('.mainPrd iframe', window.parent.document).attr('height', $('body').outerHeight());
    }  
    function _changeProfile($this){
      var $el = _defaults.$el,
          $img = $this.closest('form').find('.thmb img'),
          imageUrl = _defaults.userAuthorizeSNSInfo[_defaults.selectedSNS] ? _defaults.userAuthorizeSNSInfo[_defaults.selectedSNS].profile_image_url : render.get('boards.sns.icons.profileImage.icon.post');
      
      $img.attr('src', imageUrl);
    }
    function _loginCheck(){
      if( _defaults.gmarketLogin === 'False'){
        alert(_defaults.validateMesaages[_defaults.LanguageType].gmarketLogin);
        parent.document.location.href = _loginUrl();
        //window.open(_loginUrl(),'_blank');
        return false;
      } else if( _defaults.selectedSNS === ''){
        alert(_defaults.validateMesaages[_defaults.LanguageType].snsLogin);
        return false;
      } else if(!_defaults.IsEnableRegist){
        alert(_defaults.validateMesaages[_defaults.LanguageType].regist);
        return false;
      } else if(!_defaults.userAuthorizeSNSInfo[_defaults.selectedSNS]){
        alert(_defaults.validateMesaages[_defaults.LanguageType].snsLogin);
        return false;
      }
      return true;
    }
    function _loginCheckOnlyGmarket(){
      if( _defaults.gmarketLogin === 'False'){
        alert(_defaults.validateMesaages[_defaults.LanguageType].gmarketLogin);
        parent.document.location.href = _loginUrl();
        // window.open(_loginUrl(),'_blank');
        return false;
      } else if(!_defaults.IsEnableRegist){
        alert(_defaults.validateMesaages[_defaults.LanguageType].regist);
        return false;
      }
      return true;
    }
    function _formValidate(inputData){
      var min = _defaults.TextMinLength,
          max = _defaults.TextMaxLength,
          enabledEn = _defaults.IsPreventEnglish,
          repeatWord = _defaults.IsPreventRepeatedWord,
          exp = null,
          tmp = '';
    
     if(repeatWord){
       exp = _defaults.regExpGroup.confirm.repeatWord ? _defaults.regExpGroup.confirm.repeatWord : new RegExp('([가-힣a-zA-Zㄱ-ㅎ])\\1\\1\\1\\1$');
       if( exp.test(inputData)){
           alert(_defaults.validateMesaages[_defaults.LanguageType].repeatWord);
          return false;
       }
     } 
     
     if(enabledEn){
       exp = _defaults.regExpGroup.confirm.enabledEn ? _defaults.regExpGroup.confirm.enabledEn : new RegExp(globals.regExp.letter,'gi');
       if( exp.test(inputData) ){
           alert(_defaults.validateMesaages[_defaults.LanguageType].enabledEnglish);
           return false;
       }
     }
     if( inputData.length === 0 ){
        alert(_defaults.validateMesaages[_defaults.LanguageType].empty);
        returnValue = false;  
     }
     if( inputData.length < min){
        tmp = _defaults.validateMesaages[_defaults.LanguageType].minLength.replace(/\{\{min\}\}/gi, min);
        alert(tmp);
        return false;
        
     }
     if( inputData.length > max ){
        tmp = _defaults.validateMesaages[_defaults.LanguageType].maxLength.replace(/\{\{max\}\}/gi, max);
        alert(tmp);
        return inputData.substring(0, max);
        
     }
     if( _defaults.$el.find('.sel_sns :checkbox:checked').length === 0){
        alert(_defaults.validateMesaages[_defaults.LanguageType].selectedSNS);
        return false;
     }
     return true;
    }
    function _renderList(datas){
      _listData.shift();
      _listData.push(datas);
      _defaults.$el.find('.lst_sns').html(_renderLists);
    }
    function _snsBox(snsKinds){
      var snsBox = '',
          shared = _defaults.sharedSNSKinds.length === 0 ? [] : _defaults.sharedSNSKinds,
          len = snsKinds.length;
          // tmpl = _defaults.templates.snsBox,
          
          //returnValue = '';
          
      if( len > 0 ){
        if( shared.length === 0){//초기화
          for(var i = 0 ; i < len ; i++){
              var key = snsKinds[i],
                  IsEmptyUserAuthorizeSNSInfo = $.isEmptyObject(_defaults.userAuthorizeSNSInfo[key]),
                  snsAttr = snsAttributes[key];
              
              snsAttr.checked =  IsEmptyUserAuthorizeSNSInfo ?  '' : 'checked';
              snsAttr.kindLogOn = IsEmptyUserAuthorizeSNSInfo ? '' : 'on';
              snsAttr.profileImageUrl = IsEmptyUserAuthorizeSNSInfo ? '' :  _defaults.userAuthorizeSNSInfo[key].profile_image_url;
              shared.push(snsAttr);
          }
          _defaults.sharedSNSKinds = shared;  
        }
        
        snsBox = render.get('boards.sns.snsBox', _defaults.sharedSNSKinds);
        return snsBox;
        // for(var j = 0, length = shared.length ; j < length ; j++){
        //     for(var prop in shared[j]){
        //         var reg = '\\{\\{' + prop + '\\}\\}',
        //             exp = _defaults.regExpGroup.snsBox[prop];
        //             
        //         if( !exp ){
        //           exp = _defaults.regExpGroup.snsBox[prop] = new RegExp(reg, 'gi');
        //         }
        //         
        //         if( snsBox === ''){
        //           snsBox = tmpl.replace(exp, shared[j][prop]);
        //         } else {
        //           snsBox = snsBox.replace(exp, shared[j][prop]);
        //         }
        //     }
        //     returnValue = returnValue + snsBox;
        //     snsBox = '';
        // }
      }
      // return returnValue;
    }
    function _profileImage(type){
      // var tmplProfile = type === 'post' ? _defaults.templates.profileImage : _defaults.templates.profileImageReply,
      //     tmplIcon = _defaults.templates.icons.profileImage[type],
      //     info = _defaults.userAuthorizeSNSInfo,
      //     loginSNSList = [],
      //     selectedSNS = _defaults.selectedSNS,
      //     regImage = '\\{\\{profileImageUrl\\}\\}',
      //     regW = '\\{\\{width\\}\\}',
      //     regH = '\\{\\{height\\}\\}',
      //     disable = '\\{\\{disable\\}\\}',
      //     expImage = _defaults.regExpGroup.profileImage.url,
      //     expW = _defaults.regExpGroup.profileImage.width,
      //     expH = _defaults.regExpGroup.profileImage.height,
      //     expDisable = _defaults.regExpGroup.profileImage.disable,
      //     profileImage = '';
      
      var userAuthorizeSNSInfoLength = 0;
      for(var prop in _defaults.userAuthorizeSNSInfo){
        if( _defaults.userAuthorizeSNSInfo[prop] ){
          userAuthorizeSNSInfoLength++;
        }
      }
      
      var profileImageDatas = [{
        profileImageUrl : _defaults.selectedSNS !== '' && _defaults.userAuthorizeSNSInfo[_defaults.selectedSNS] ? _defaults.userAuthorizeSNSInfo[_defaults.selectedSNS].profile_image_url : render.get('boards.sns.icons.profileImage.' + type),
        width : type === 'post' ? 'width:68px' :  'width:48px',
        height : type === 'post' ? 'height:68px' :  'height:48px',
        disable :  type === 'post' && userAuthorizeSNSInfoLength > 1 ? '' : 'disabled'
      }];
      
      return render.get('boards.sns.profileImage', profileImageDatas);
      
      // profileImage = selectedSNS !== '' && info[selectedSNS] ? info[selectedSNS].profile_image_url : tmplIcon;
      // var width = type === 'post' ? 'width:68px' :  'width:48px';
      // var height = type === 'post' ? 'height:68px' :  'height:48px';
      // 
      // if(!expImage){
      //   expImage = _defaults.regExpGroup.profileImage.url = new RegExp(regImage, 'gi');
      // }
      // if(!expW){
      //   expW = _defaults.regExpGroup.profileImage.width = new RegExp(regW, 'gi');
      // }
      // if(!expH){
      //   expH = _defaults.regExpGroup.profileImage.height = new RegExp(regH, 'gi');
      // }
      // if(!expDisable){
      //   expDisable = _defaults.regExpGroup.profileImage.disable = new RegExp(disable, 'gi');
      // }
      // 
      // if(type === 'post'){
      //   var userAuthorizeSNSInfoLength = 0;
      //   for(var prop in _defaults.userAuthorizeSNSInfo){
      //     if( _defaults.userAuthorizeSNSInfo[prop] ){
      //       userAuthorizeSNSInfoLength++;
      //     }
      //   }
      //   if( userAuthorizeSNSInfoLength > 1){
      //     tmplProfile = tmplProfile.replace(expDisable, ' ');
      //   } else {
      //     tmplProfile = tmplProfile.replace(expDisable, 'disabled');
      //   }
      // }
      
      // return tmplProfile.replace(expImage, profileImage).replace(expW, width).replace(expH, width);
    }
    function _init(options){
      var el = '',
          $el = null,
          table = '',
          form = '',
          boardID = '0';
          // tmpl = null;
          
      //get templates
      // tmpl = _defaults.templates = {
      //   snsTable : render.board.snsTable(),
      //   snsList : render.board.snsList(),
      //   snsConfirm : render.board.snsConfirm(),
      //   snsReply : render.board.snsReply(),
      //   snsBox : render.board.snsBox(),
      //   profileImage : render.board.profileImage(),
      //   profileImageReply : render.board.profileImageReply(),
      //   snsDatas : render.board.snsDatas(),
      //   icons : render.board.icons(),
      //   buttons : render.board.buttons(),
      //   empty : render.board.emptyList()
      // };
      
      el = _defaults.el = options.el !== '' ? options.el : _defaults.root;
      $el = _defaults.$el = _defaults.$el ? _defaults.$el : $(el);
      boardID = _defaults.boardID = options.boardID !== '' ? options.boardID : '0';
      _defaults.gmarketLogin = options.gmarketLogin? options.gmarketLogin : 'False';
      
      api.snsBoard.all({
        data : {
          "id" : boardID
        },
        successHandler : function(results){
          _defaults = utils.extend(_defaults, results);
          _initSNS();
          _getSNSInfo();
          _setSelectedSNS();
          
          var replyDatas = [{
            submit : _defaults.formMessages[_defaults.LanguageType].submit,
            placeholder : _defaults.formMessages[_defaults.LanguageType].placeholder,
            maxLength : _defaults.TextMaxLength,
            snsBox : _snsBox(_defaults.SnsKinds),
            profileImage : _profileImage('post'),
            isLogin :  _defaults.selectedSNS === '' ? '<h2>' + _defaults.formMessages[_defaults.LanguageType].login + '</h2>' : '<p class="u_log"><a href="#" class="js-log-out">' + _defaults.formMessages[_defaults.LanguageType].logout +'</a></p>'
          }];
          
          form = render.get('boards.sns.confirm', replyDatas);
          
          // form = _defaults.templates.snsConfirm
          //           .replace(/\{\{submit\}\}/gi, _defaults.formMessages[_defaults.LanguageType].submit)
          //           .replace(/\{\{placeholder\}\}/gi, _defaults.formMessages[_defaults.LanguageType].placeholder)
          //           .replace(/\{\{maxLength\}\}/gi, results.TextMaxLength)
          //           .replace(/\{\{snsBox\}\}/gi, _snsBox(results.SnsKinds))
          //           .replace(/\{\{profileImage\}\}/gi, _profileImage('post'))
          //           .replace(/\{\{isLogin\}\}/gi, _defaults.selectedSNS === '' ? _defaults.formMessages[_defaults.LanguageType].login : '<a href="#" class="js-log-out">' + _defaults.formMessages[_defaults.LanguageType].logout +'</a>');
                    
          $el.html(render.get('boards.sns.table')).find('.sns_write').html(form);
          
          if($el.find('.sns_write :checkbox:checked').length === 1){
            $el.find('.sns_write :checkbox:checked').attr('disabled','disabled');
          }
          api.snsBoard.list({
            data : {
              boardID : _defaults.boardID,
              pageIndex : _defaults.pageIndex,
              pageSize : _defaults.CountPerPage
            },
            successHandler : function(results){
              if(results.Data.length > 0 ){
                _renderList(results.Data);
                paging.init({
                  totalCount : results.PageTotalCount,
                  currentPage : _defaults.pageIndex,
                  offset : 0,
                  limit : _defaults.CountPerPage,
                  block : 5, 
                  callback : function(options){
                    _defaults.pageIndex = options.currentPage;
                    
                    api.snsBoard.list({
                      data : {
                        boardID : _defaults.boardID,
                        pageIndex : _defaults.pageIndex,
                        pageSize : _defaults.CountPerPage
                      },
                      successHandler : function(results){              
                        if(results.Data.length > 0 ){
                          $('.sns_default').remove();
                          _renderList(results.Data);
                          paging.setOptions({
                            totalCount : results.PageTotalCount,
                            currentPage : _defaults.pageIndex
                          });
                        } else {
                          _defaults.$el.find('#sns').append(render.get('boards.sns.emptyList'));
                          var errorMessage = _defaults.listMessages[_defaults.LanguageType].noLists;
                          alert(errorMessage);
                        }
                      }
                    });
                  }
                }).render();
              } else {
                _defaults.$el.find('#sns').append(render.get('boards.sns.emptyList'));
              }
            }
          });
          _eventBinding($el);
        }
      });
    }
    function _del(index){
      api.snsBoard.del({
        url : '/BoardContent/Remove',
        data : {
          id : index
        },
        successHandler : function(results){
          if(results === 0 ){
            _defaults.pageIndex = 1;
            
            api.snsBoard.list({
              data : {
                boardID : _defaults.boardID,
                pageIndex : _defaults.pageIndex,
                pageSize : _defaults.CountPerPage
              },
              successHandler : function(results){
                if(results.Data.length > 0 ){
                  $('.sns_default').remove();
                  _renderList(results.Data);
                  paging.setOptions({
                    totalCount : results.PageTotalCount,
                    currentPage : _defaults.pageIndex
                  });
                  
                } else {
                    _defaults.$el.find('#sns').append(render.get('boards.sns.emptyList'));
                    _defaults.$el.find('.lst_sns').empty();
                    _defaults.$el.find('#paging').empty();
                    var errorMessage = _defaults.listMessages[_defaults.LanguageType].noLists;
                    alert(errorMessage);
                }
              }
            });
          } else {
            var errorMessage = utils.errorHandler('delete')[_defaults.LanguageType];
            alert(errorMessage);
          }
        }
      });
    }
    function _postSNS(opt){
      var $form = opt.form,
          message = opt.message,
          picture = opt.picture,
          planId = opt.planId,
          $checkbox = $form.find(':checkbox');
        
      $checkbox.each(function(){
        if($(this).is(':checked')){
          var snsType = $(this).data('snstype');
          var snsInstance = '$' + snsType;
          _defaults[snsInstance].post({
              message : message,
              picture : picture,
              planId : planId
          });
        }
      });
      // for(var prop in _defaults.userAuthorizeSNSInfo){
      //   var snsInstance = '$' + prop;
      //   if(_defaults.userAuthorizeSNSInfo[prop] && _defaults[snsInstance]){
      //     _defaults[snsInstance].post({
      //         message : opt.message,
      //         picture : opt.picture,
      //         planId : opt.planId
      //     });
      //   }
      // }
    }
    function _submit($form){
      var selectedSNS = _defaults.selectedSNS,
          datas = '',
          name = $form.attr('name'),
          id = $form.closest('li').data('id'),
          contents = $form.find('textarea').val(),
          isFormTrue = _formValidate(contents);
      
      datas = _defaults.userAuthorizeSNSInfo[selectedSNS];
      
      if(isFormTrue === true){
          api.snsBoard.postSubmit({
            data : {
              boardID : _defaults.boardID,
              id : name === 'post' ? 0 : id,
              eventID : _defaults.EventID ? _defaults.EventID : 0,
              contents : contents,
              snsKind : datas.kind,
              snsUserID: datas.screen_name,
              snsProfileImage : datas.profile_image_url,
              snsUserName : datas.id && datas.id !== '' ? datas.id : datas.screen_name
            },
            successHandler : function(results){
              $form.find('textarea').val('');
              $form.find('.write strong').text(0);
              if(name === 'post'){
                _defaults.pageIndex = 1;
              }
              if(results.ResultCode === 0 ){
                
                //$form[0].reset();
                
                _postSNS({
                  form : $form,
                  message : contents,
                  picture : _defaults.FacebookThumbnailImage,
                  planId : _defaults.PlanID ? _defaults.PlanID : 0
                });
                
                if(results.EncStr && results.StrValue){
                  window.CommonApplyEventPlatformMobile(results.StrValue ,results.EncStr);
                }
                
                api.snsBoard.list({
                  data : {
                    boardID : _defaults.boardID,
                    pageIndex : _defaults.pageIndex,
                    pageSize : _defaults.CountPerPage
                  },
                  successHandler : function(results){
                    if(results.Data.length > 0 ){
                      $('.sns_default').remove();
                      _renderList(results.Data);
                      paging.setOptions({
                        totalCount : results.PageTotalCount,
                        currentPage : _defaults.pageIndex
                      });
                      
                    } else {
                      _defaults.$el.find('#sns').append(render.get('boards.sns.emptyList'));
                      var errorMessage = _defaults.listMessages[_defaults.LanguageType].noLists;
                      alert(errorMessage);
                    }
                  }
                });
              } else {
                var errorMessage = utils.errorHandler(results.ResultCode)[_defaults.LanguageType];
                alert(errorMessage);
                switch(results){
                  case '5' :
                    parent.document.location.href = _loginUrl();
                    // window.open(_loginUrl(),'_blank');
                    break;  
                }
              }
            }
          });
      } else {
        $form.find('textarea').focus();
        if(typeof isFormTrue !== 'boolean'){
          $form.find('textarea').val(isFormTrue);
          $form.find('.write strong').text(isFormTrue.length);
        }
      }
    }
    
    function _reply($this){
      var $li = $this.closest('li'),
          $reply = $li.find('.reply_area'),
          //tmpl = _defaults.templates,
          form = '';
          
      var replyDatas = [{
        submit : _defaults.formMessages[_defaults.LanguageType].submit,
        placeholder : _defaults.formMessages[_defaults.LanguageType].placeholder,
        maxLength : _defaults.TextMaxLength,
        snsBox : _snsBox(_defaults.SnsKinds),
        profileImage : _profileImage('reply')
      }];
      
      form = render.get('boards.sns.reply', replyDatas); 
      $reply.parent().empty();
      
        // form = tmpl.snsReply
        //     .replace(/\{\{submit\}\}/gi, _defaults.formMessages[_defaults.LanguageType].submit)
        //     .replace(/\{\{placeholder\}\}/gi, _defaults.formMessages[_defaults.LanguageType].placeholder)
        //     .replace(/\{\{maxLength\}\}/gi, _defaults.TextMaxLength)
        //     .replace(/\{\{snsBox\}\}/gi, _snsBox(_defaults.SnsKinds))
        //     .replace(/\{\{profileImage\}\}/gi, _profileImage('reply'))
        //     .replace(/\{\{textareaBox\}\}/gi, tmpl.textareaBox);
        $li.append(form);
        
        if($li.find(':checkbox:checked').length === 1){
          $li.find(':checkbox:checked').attr('disabled', 'disabled');
        }
        
        //_eventBinding($li);
    }
    function _cancel($this){
      var $li = $this.closest('li'),
          $form = $li.find('form'),
          $reply = $li.find('.reply_area');
          
        $reply.parent().empty();
    }
    function _snsselect($this, type){
      var snsAttr = snsAttributes[type],
          snsInstance = '$' + type,
          kindType = snsAttr.kindType;
    
      _defaults.selectedSNS = type;
       if(_defaults[snsInstance] && snsAttr.kindLogOn === ''){
         _defaults[snsInstance].checkLoginState();
       }
       
       _changeProfile($this);
    }
    function _logout(){
      alert(_defaults.logoutMessages[_defaults.LanguageType]);
      for(var prop in _defaults.userAuthorizeSNSInfo){
        var snsInstance = '$' + prop;
        
        if(_defaults[snsInstance]){
          _defaults[snsInstance].logout();
        }
      }
    }
    _listData.onItemAdded = function(index, item){
     _renderLists = '';
    //  var list = _defaults.templates.snsList,
    //     replyWrapper = _defaults.templates.replyWrapper,
    //     replyList = _defaults.templates.replyList,
    //     regisEnabledReply = '\\{\\{IsEnabledReply\\}\\}',
    //     regisReply = '\\{\\{isReply\\}\\}',
    //     regButtonReply = '\\{\\{replyButton\\}\\}',
    //     regButtonDelete = '\\{\\{deleteButton\\}\\}',
    //     expisEnabledReply = null,
    //     expisReply = null,
    //     expButtonReply = null,
    //     expButtonDelete = null;
    // 
    //   if( _defaults.regExpGroup.list.IsEnabledReply ){
    //     expisEnabledReply = _defaults.regExpGroup.list.IsEnabledReply;
    //   } else {
    //     expisEnabledReply = _defaults.regExpGroup.list.IsEnabledReply = new RegExp(regisEnabledReply, 'gi');
    //   }
    //   
    //   if( _defaults.regExpGroup.list.isReply ){
    //     expisReply = _defaults.regExpGroup.list.isReply;
    //   } else {
    //     expisReply = _defaults.regExpGroup.list.isReply = new RegExp(regisReply, 'gi');
    //   }
    //   
    //   if( _defaults.regExpGroup.list.buttonReply ){
    //     expButtonReply = _defaults.regExpGroup.list.buttonReply;
    //   } else {
    //     expButtonReply = _defaults.regExpGroup.list.buttonReply = new RegExp(regButtonReply, 'gi');
    //   }
    //   
    //   if( _defaults.regExpGroup.list.buttonDelete ){
    //     expButtonDelete = _defaults.regExpGroup.list.buttonDelete;
    //   } else {
    //     expButtonDelete = _defaults.regExpGroup.list.buttonDelete = new RegExp(regButtonDelete, 'gi');
    //   }
      //test code
      //console.log(item, typeof item);
      //console.log(item.length);
      for(var i = 0, len = item.length; i < len ; i++){
          item[i].snsKlass = snsAttributes[item[i].SnsKind].kindClass;
          item[i].snsName = snsAttributes[item[i].SnsKind].kindName;
          item[i].isReply = _defaults.IsEnabledReply && item[i].Depth > 1 ? 'lst_reply' : '';
          item[i].IsEnabledReply = _defaults.IsEnabledReply && item[i].Depth === 1 ? render.get('boards.sns.buttons.reply') : '';
          item[i].IsEnabledDelete = item[i].IsEnabledDelete && _defaults.IsEnabledDelete ? render.get('boards.sns.buttons.del') : '';
          item[i].Contents = item[i].Contents ? item[i].Contents.replace(/(\r\n|\n|\r)/gm, "<br>") : '';
          item[i].replyButton = _defaults.buttonMessages[_defaults.LanguageType].reply;
          item[i].deleteButton = _defaults.buttonMessages[_defaults.LanguageType].delete;
      }
      var renderTemplate = render.get('boards.sns.list', item);
      _renderLists = _renderLists + renderTemplate;
    //  for(var i = 0, len = item.length; i < len ; i++){
    //      var renderTemplate = list;
    //      
    //      for(var prop in item[i]){
    //          var reg = '\\{\\{' + prop + '\\}\\}',
    //              exp = null,
    //              expClass = null,
    //              expName = null;
    //              
    //          if( _defaults.regExpGroup.list[prop] ){
    //            exp = _defaults.regExpGroup.list[prop];
    //          } else {
    //            exp = _defaults.regExpGroup.list[prop] = new RegExp(reg, 'gi');
    //          }
    //          
    //          if(_defaults.IsEnabledReply && item[i].Depth > 1){
    //            renderTemplate = renderTemplate.replace(expisReply, 'lst_reply');
    //          } else {
    //            renderTemplate = renderTemplate.replace(expisReply, '');
    //          }
    //          
    //          if( prop === 'SnsKind'){
    //             if( _defaults.regExpGroup.list.snsKlass ){
    //               expClass = _defaults.regExpGroup.list.snsKlass;
    //             } else {
    //               expClass = _defaults.regExpGroup.list.snsKlass = new RegExp('\\{\\{snsKlass\\}\\}', 'gi');
    //             }
    //             if( _defaults.regExpGroup.list.snsName ){
    //               expName = _defaults.regExpGroup.list.snsName;
    //             } else {
    //               expName = _defaults.regExpGroup.list.snsName = new RegExp('\\{\\{snsName\\}\\}', 'gi');
    //             }
    //             
    //             renderTemplate = renderTemplate
    //                               .replace(expClass,snsAttributes[item[i][prop]].kindClass)
    //                               .replace(expName, snsAttributes[item[i][prop]].kindName);
    //          }
    //          renderTemplate = renderTemplate.replace(exp, function(){
    //            if(prop === 'IsEnabledDelete' ){
    //              if( item[i].IsEnabledDelete && _defaults.IsEnabledDelete){
    //                var tmp = _defaults.templates.buttons.del.replace(expButtonDelete, _defaults.buttonMessages[_defaults.LanguageType].delete);
    //                return tmp;
    //              }
    //              else {
    //                return '';
    //              }
    //            } else if(prop === 'Contents'){
    //              return item[i][prop].replace(/(\r\n|\n|\r)/gm, "<br>");
    //            } else {
    //              return item[i][prop];
    //            }
    //          })
    //          
    //          if( _defaults.IsEnabledReply && item[i].Depth === 1){
    //            var tmp = _defaults.templates.buttons.reply.replace(expButtonReply, _defaults.buttonMessages[_defaults.LanguageType].reply);
    //            renderTemplate = renderTemplate.replace(expisEnabledReply, tmp);
    //          } else {
    //            renderTemplate = renderTemplate.replace(expisEnabledReply, '');
    //          }
    //      }
    //      _renderLists = _renderLists + renderTemplate;
    //  }
   };
    return {
      init : function(options){
        _init(options);
        return this;
      },      
      del : function(index){
        if(confirm(_defaults.validateMesaages[_defaults.LanguageType].delete)){
          _del(index);
        } else {
          return false;
        }
      },
      submit : function($form){
        _submit($form);
      },
      cancel : function($this){
        _cancel($this);
      },
      reply : function($this){
        _reply($this);
      },
      login : function(){
        _login();
      },
      logout : function(){
        _logout();
      },
      loginCheck : function(){
        return _loginCheck();
      },
      loginCheckOnlyGmarket : function(){
        return _loginCheckOnlyGmarket();
      },
      snsselect : function($this){
        var type = $this.data('snstype'),
            selectedType = '',
            $el = _defaults.$el,
            $form = $this.closest('form'),
            formName = $form.attr('name'),
            $parent = $this.parent(),
            $checkBoxs = $parent.find(':checkbox'),
            $checkedCheckBoxs = $parent.find(':checkbox:checked');
        
        if($this.is(':checked')){
          if($checkedCheckBoxs.length > 1){
            $checkBoxs.removeAttr('disabled', 'disabled');
            if(formName==='post'){
              $form.find('.js-change-profileimage').removeClass('disabled');
            }
          } else {
            $checkedCheckBoxs.attr('disabled', 'disabled');
            if(formName === 'post'){
              $form.find('.js-change-profileimage').addClass('disabled');
            }
          }
          selectedType = type;
          for(var i = 0 , len = _defaults.sharedSNSKinds.length; i < len ; i++){
            if( _defaults.sharedSNSKinds[i].kindType === type ) {
              _defaults.sharedSNSKinds[i].checked = 'checked';
            }
          }
        } else{
          if($checkedCheckBoxs.length === 1){  
            $checkedCheckBoxs.attr('disabled', 'disabled');
            if(formName === 'post'){
              $form.find('.js-change-profileimage').addClass('disabled');
            }
          }
          selectedType = $this.siblings(':checkbox:checked')[0].getAttribute('data-snstype');
          for(var j = 0 , jlen = _defaults.sharedSNSKinds.length; j < jlen ; j++){
            if( _defaults.sharedSNSKinds[j].kindType === type ) {
              _defaults.sharedSNSKinds[j].checked = '';
            }
          }
        }
        
        if(formName === 'post'){
          $el.find(".js-close-reply").trigger('click');
        }
        _snsselect($this, selectedType);
      }
    };
  });
})(window, eModule);
