var AjaxHelper = AjaxHelper || {};
AjaxHelper.ContentTypeJson = "application/json; charset=utf-8";
AjaxHelper.ContentTypeXml = "text/xml; charset=utf-8";

//Sync Post Json
AjaxHelper.GetDataToPostService = function (requestUrl, argument) {
	return AjaxHelper.CallAjaxService(requestUrl, argument, "POST", "json", AjaxHelper.ContentTypeJson, "", errorHandler);
}

//Sync Get Json
AjaxHelper.GetDataToGetService = function (requestUrl, errorHandler) {
	return AjaxHelper.CallAjaxService(requestUrl, "", "GET", "json", AjaxHelper.ContentTypeJson, "", errorHandler);
}

//Async Post Json
AjaxHelper.AsyncGetDataToPostService = function (requestUrl, argument, callBackFunction, errorHandler) {
	return AjaxHelper.CallAjaxService(requestUrl, argument, "POST", "json", AjaxHelper.ContentTypeJson, callBackFunction, errorHandler);
}

//Async Get Json
AjaxHelper.AsyncGetDataToGetService = function (requestUrl, callBackFunction, errorHandler) {
	return AjaxHelper.CallAjaxService(requestUrl, "", "GET", "json", AjaxHelper.ContentTypeJson, callBackFunction, errorHandler);
}

//Async Post Soap
AjaxHelper.AsyncGetDataToGetService = function (requestUrl, argument, callBackFunction, errorHandler) {
	return AjaxHelper.CallAjaxService(requestUrl, argument, "GET", "xml", AjaxHelper.ContentTypeXml, callBackFunction, errorHandler);
}

//Common Function
AjaxHelper.CallAjaxService = function (requestUrl, argument, ajaxType, ajaxDataType, ajaxContentType, callBackFunction, errorHandler) {
	var isAsync = true;
	if (callBackFunction == "") {
		isAsync = false;
	}

	$.ajax({
		type: ajaxType,
		url: requestUrl,
		//processData: false,
		data: argument,
		//contentType: ajaxContentType,
		dataType: ajaxDataType,
		async: isAsync,
		beforeSend: function (xhr) {

		},
		success: function (msg) {
			if (msg) {
				eval(callBackFunction)(msg);
			}
		},
		error: function (err) {
			try {
				eval(errorHandler)(err);
			}
			catch (ex) {
				//alert(ex.status + ' ' + ex.statusText);	
			}
		}
	});
}

//onerror = ErrorHandler;

function ErrorHandler(pMsg, pURL, pLines) {
	//	var funcname = "";
	//	if (ErrorHandler.caller != null) {
	//		try {
	//			funcname = ErrorHandler.caller.toString();
	//			funcname = funcname.substring(0, 50);
	//		}
	//		catch (e) {
	//			// do nothing
	//		}
	//	}
	//	var domainNm = document.domain;

	//	funcname = funcname.replace(/\r\n/g, "\\n").replace(/\n/g, "\\n").replace(/\r/g, "\\n").replace(/\t/g, "\\n");

	//	AjaxHelper.WriteScriptError(pMsg, domainNm, pURL, funcname, pLines);

	return false;
};

/**
* @private
* @param {String} pMsg
* @param {String} domainNm
* @param {String} pURL
* @param {String} pLines
* @return boolean
*/
AjaxHelper.WriteScriptError = function (pMsg, domainNm, pURL, funcname, pLines) {
	//	var params = '{';
	//	params += '"message":"' + pMsg + " - " + domainNm + '"';
	//	params += ', "url":"' + pURL + '"';
	//	params += ', "funcname":"' + funcname + '"';
	//	params += ', "lines":"' + pLines + '"';
	//	params += ', "referer":"' + document.referrer + '\\nUser Agent : ' + window.navigator.userAgent + '\\nUser Language : ' + window.navigator.userLanguage + '"';
	//	params += '}';

	//	//AjaxHelper.AsyncGetDataToPostService("ArcheUser.svc", "AddScriptExceptionLog", params, "AjaxHelper.WriteScriptErrorResult");
	//	AjaxHelper.AsyncGetDataToPostService("ArcheCommon.svc", "AddScriptExceptionLog", params, "AjaxHelper.WriteScriptErrorResult");
};

AjaxHelper.WriteScriptErrorResult = function (result) {
}