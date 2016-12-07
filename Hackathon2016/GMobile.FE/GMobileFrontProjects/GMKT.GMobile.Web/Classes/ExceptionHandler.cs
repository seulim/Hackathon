using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ArcheFx;
using ArcheFx.Diagnostics;
using GMKT.GMobile.Exceptions;

namespace GMKT.GMobile.Web
{
    public class ExceptionHandler : IExceptionHandler
    {
        #region IExceptionHandler 멤버

        public bool IsLoggingEntry(ExceptionTraceEntry entry)
        {
            System.Web.HttpException httpEx = entry.Exception as System.Web.HttpException;

            // HTTP 예외가 발생했고 HttpCode가 500이 아닌 경우에는 에러로그를 남기지 않습니다.
            if (httpEx != null && httpEx.GetHttpCode() != 500)
            {
                return false;
            }

						if (entry.Exception is GMobileNotLoggingException)
						{
							return false;
						}

            return true;
        }

        #endregion
    }
}
