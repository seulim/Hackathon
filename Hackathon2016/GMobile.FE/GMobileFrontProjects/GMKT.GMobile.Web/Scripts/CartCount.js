var cartCountStorage = new CartCountStorage();

window.addEventListener("message", function (e) {
	var method = e.data.method;
	var result = null;

	if (method) {
		switch (method) {
			case "getCount":
				result = cartCountStorage.getCount();
				break;
			case "setCount":
				result = cartCountStorage.setCount(e.data.count, e.data.expireTime);
				break;
			case "removeCount":
				result = cartCountStorage.removeCount();
				break;
		}
	}

	e.source.postMessage({
		method: method,
		result: result
	}, e.origin);
});