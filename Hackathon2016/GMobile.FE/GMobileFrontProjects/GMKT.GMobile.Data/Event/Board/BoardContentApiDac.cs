using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.Web.Context;

namespace GMKT.GMobile.Data
{
    public class BoardContentApiDac : ApiBase
    {
        public BoardContentApiDac() : base("GMApi")
        {
        }

        public DataAndSize<IEnumerable<BoardContentEntityT>> FindBy(BoardContentSearch search)
        {
            var cookieParameter = (string.IsNullOrEmpty(search.UserInfo)) ? null :  new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, search.UserInfo);

            ApiResponse<DataAndSize<List<BoardContentEntityT>>> result = ApiHelper.CallAPI<ApiResponse<DataAndSize<List<BoardContentEntityT>>>>(
                    "GET",
                    ApiHelper.MakeUrl("api/BoardContent"),
                    new
                    {
                        boardID = search.BoardID,
                        pageIndex = search.PageIndex,
                        pageSize = search.PageSize,
                    },
                    cookieParameter
                );

            return new DataAndSize<IEnumerable<BoardContentEntityT>>() { PageTotalCount = result.Data.PageTotalCount, Data = result.Data.Data };
        }

        public BoardContentResultCode Insert(BoardContentEntityT model)
        {
            if (string.IsNullOrEmpty(model.UserInfo))
                return BoardContentResultCode.LoginFail;

			var cookieParameter = new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, model.UserInfo);

            ApiResponse<int> result = ApiHelper.CallAPI<ApiResponse<int>>(
                    "POST",
                    ApiHelper.MakeUrl("api/BoardContent/PostToAdd"),
                    model,
                    cookieParameter
                );

            return (BoardContentResultCode)result.Data;
        }

        public BoardContentResultCode Delete(BoardContentEntityT model)
        {
            if (string.IsNullOrEmpty(model.UserInfo))
                return BoardContentResultCode.LoginFail;

			var cookieParameter = new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, model.UserInfo);
            
            ApiResponse<bool> result = ApiHelper.CallAPI<ApiResponse<bool>>(
                    "POST",
                    ApiHelper.MakeUrl("api/BoardContent/PostToRemove"),
                    model.ID,
                    0,
                    cookieParameter
                );

            return result.Data ? BoardContentResultCode.Success : BoardContentResultCode.GeneralFail;
        }
    }
}

