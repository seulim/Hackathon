using System;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Controllers
{
    public class BoardContentController : EventControllerBase
    {
        [HttpGet]
        public ActionResult GetBy(int boardID, int pageIndex = 1, int pageSize = 5)
        {
			if (pageSize == 0)
			{
				pageSize = 5;
			}

            return Json(new BoardContentApiBiz().GetBy(new BoardContentSearch()
                   {
                       BoardID = boardID,
                       PageIndex = pageIndex,
                       PageSize = pageSize,
                       UserInfo = gmktUserProfile.UserInfoString
                   }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(int boardID, int id, string contents, string snsKind, string snsUserID, string snsProfileImage, string snsUserName, int eventID = 0)
        {
            if (!PageAttr.IsLogin)
                return Json(new AddBoardResult { ResultCode = BoardContentResultCode.LoginFail });

            var result = new BoardContentApiBiz().Add(new BoardContentEntityT()
            {
                BoardID = boardID,
                ID = id,
                Contents = contents,
                RegistDateTime = DateTime.Now,
                SnsKind = snsKind,
                SnsUserID = snsUserID,
                SnsProfileImage = snsProfileImage,
                SnsUserName = snsUserName,
                UserInfo = gmktUserProfile.UserInfoString
            });

            if (result == BoardContentResultCode.Success && eventID > 0)
            {
                string[] encrytKeys = EncryptForEventPlatform(eventID);

                if (encrytKeys.Length == 2)
                {
                    return Json(new AddBoardResult()
                         {
                             ResultCode = BoardContentResultCode.Success,
                             StrValue = encrytKeys[0],
                             EncStr = encrytKeys[1]
                         });
                }
            }

            return Json(new AddBoardResult { ResultCode = result });
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            if (!PageAttr.IsLogin)
                return Json(BoardContentResultCode.LoginFail);

            return Json(new BoardContentApiBiz().Remove(new BoardContentEntityT()
            {
                ID = id,
                UserInfo = gmktUserProfile.UserInfoString
            }));
        }

        class AddBoardResult
        {
            public BoardContentResultCode ResultCode;
            public string StrValue;
            public string EncStr;
        }
    }
}