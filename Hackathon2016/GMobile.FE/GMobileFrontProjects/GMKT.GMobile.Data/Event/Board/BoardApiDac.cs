using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
    public class BoardApiDac: ApiBase
    {
        public BoardApiDac() : base("GMApi")
        {
        }

        public BoardEntityT FindBy(int id)
        {
            var result = ApiHelper.CallAPI<ApiResponse<BoardEntityT>>(
                    "GET",
                    ApiHelper.MakeUrl("api/Board/GetBy"),
                    new
                    {
                        id = id,
                    }
                );

            return result.ResultCode == 0 ? result.Data : new BoardEntityT();
        }
    }
}