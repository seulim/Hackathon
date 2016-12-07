using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.Exceptions;
using GMKT.GMobile.Exceptions;

namespace GMKT.GMobile.Biz
{
	#region [ Base Exception ]
	public class ShopNotLoggingException : IgnoreBizException
	{
		public ShopNotLoggingException(string message) : base(message) { }
	}

	public class ShopException : GMobileException
	{
		public ShopException(string message) : base(message) { }
	}
	#endregion

	public class NotExistShopException : ShopNotLoggingException
	{
		public NotExistShopException() : base("해당 미니샵이 존재하지 않습니다.") { }
	}

	public class NotExistSellerInfoException : ShopNotLoggingException
	{
		public NotExistSellerInfoException() : base("해당 미니샵의 판매자 정보가 존재하지 않습니다.") { }
	}
}
