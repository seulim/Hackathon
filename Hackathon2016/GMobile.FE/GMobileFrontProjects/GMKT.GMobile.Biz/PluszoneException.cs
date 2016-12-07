using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Exceptions;

namespace GMKT.GMobile.Biz
{
	public class PluszoneNotLoggingException : GMobileNotLoggingException
	{
		public PluszoneNotLoggingException(string message) : base(message) { }
	}

	public class PluszoneException : GMobileException
	{
		public PluszoneException(string message) : base(message) { }
	}
}
