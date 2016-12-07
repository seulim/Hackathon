using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
	public class ApiResponse<T>
	{
		public int ResultCode { get; set; }

		public string Message { get; set; }

		public T Data { get; set; }
	}
}
