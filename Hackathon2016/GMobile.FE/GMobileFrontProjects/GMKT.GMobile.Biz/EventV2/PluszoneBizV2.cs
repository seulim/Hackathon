using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;


namespace GMKT.GMobile.Biz.EventV2
{
    public class PluszoneBizV2
    {
        public ApiResponse<PluszoneDataT> GetPluszoneInfo()
        {
            return new PluszoneApiDac().GetPluszoneInfo();
        }
    }
}
