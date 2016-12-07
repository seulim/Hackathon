
/***** Facebook Login �� ��ǰ ���� ***************/

function fb_login() {
    FB.login(function (response) {
        if (response.authResponse) {
        } else {
            FB.login();
        }
    }, { scope: 'publish_actions' });
}


function doFbImagePost(msg, imgUrl) {
    FB.login(function (response) {
        if (response.authResponse) {
            var access_token = FB.getAuthResponse()['accessToken'];
            console.log('Access Token = ' + access_token);
            FB.api('me/photos', 'post', {
                message: msg,
                status: 'success',
                access_token: access_token,
                url: imgUrl
            }, function (response) {

                if (!response || response.error) {
                    alert('Error occured:' + response);
                } else {
                    alert('Post ID:[' + response.id + ']���̽��Ͽ� �����õǾ����ϴ�.');
                }
            });
        } else {
            log('User cancelled login or did not fully authorize.');
        }
    }, { scope: 'user_photos,photo_upload,publish_stream,offline_access' }
   );
}

/* facebook post */
 function doFbPost(_postObj) {
 	FB.getLoginStatus(function (response) {
 		if (response.status === 'connected') {
 			//profile_id = response.authResponse.userID;
 			//profileImg = 'https://graph.facebook.com/' + profile_id + '/picture';

 			FB.api('/me/feed', 'post', {
 				message: _postObj.MSG,
 				link: _postObj.LINK,
 				picture: _postObj.IMG,
 				name: _postObj.NAME,
 				description: _postObj.DESC
 			}, function (data) {
 				if (data.error != undefined) {
 					if (data.error.type == "OAuthException") {
 						alert("facebook�� �α��� �� �ٽ� �õ��� ������.");
 					} else {
 						alert(data.error.code + ":" + data.error.message + "\n" + data.error.type);
 					}
 					fb_mobile_login();  //fb_login();
 				} else {
 					//log(data);
 					if (typeof _postObj.CALLBACK == 'function') _postObj.CALLBACK(response.authResponse.userID);
 				}
 			});
 		} else {
 			log('login with doFbPost()');
 			fb_mobile_login();  //fb_login();
 		}
 	}, true);
}

function fb_mobile_login() {
	var loginUrl = "https://m.facebook.com/dialog/oauth/?client_id=230223200507806&redirect_uri=" + document.URL + "&scope=publish_actions,publish_stream,photo_upload";
	window.location = loginUrl;
}

/* facebook login check */
function fb_try_login(callback) {
	_user_uid = "";
	var _o = this;
	FB.getLoginStatus(function (response) {
		//�۰� ���� �Ǿ� ������.
		if (response.status === 'connected') {
			var accessToken = response.authResponse.accessToken;
			_o.user_uid = response.authResponse.userID;
			if (callback !== undefined) callback(response);
		}
		//�۰� ����Ǿ� ���� ������ app�α���.
		else {
			fb_mobile_login();  //fb_login(callback);
		}
	}, true);
};