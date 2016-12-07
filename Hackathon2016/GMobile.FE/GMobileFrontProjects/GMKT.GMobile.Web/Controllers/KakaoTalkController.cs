using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.Web.Mvc;
using GMKT.Web.Membership;
using GMKT.WebComponents.Common.Filters;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Biz;
using GMKT.Framework;
using GMKT.Framework.Security;

namespace GMKT.GMobile.Web.Controllers
{
    public class KakaoTalkController : MemberBaseController
    {

        public ActionResult IndexGet()
        {
            return View();
        }
        
        [GMKTAuthorize(RedirectionURLEnum.REDIRECTION_KAKAOTALK_LOGIN, UnauthorizedPolicyEnum.RedirectURL, RoleEnum.BuyerMember, RoleEnum.SellerMember)]
        [HttpPost]        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(string temp_user_key, string access_channel)
        {
            //if (!ModelState.IsValid)
            //{
            //    string sHtml = "<script type='text/javascript'>alert('!ModelState.IsValid');</script>";
            //    return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
            //}

            try
            {
                KakaoTalkApplyUserM kakaoTalkApplyUser = SetApplyUserInfo();

                kakaoTalkApplyUser.TempUserKey = temp_user_key;
                kakaoTalkApplyUser.AccessChannel = GetOriginChannelValid(temp_user_key, access_channel);

                if (GetCustTypeValid(gmktUserProfile.CustType))
                {
                    return View(kakaoTalkApplyUser);
                }
                else
                {
                    string sHtml = "<script type='text/javascript'>alert('회원종류가 개인구매자 또는 개인이딜러가 아닙니다.');</script>";
                    return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
                }
            }
            catch (Exception e)
            {
                ArcheFx.Diagnostics.Trace.WriteError(e);
                string sHtml = "<script type='text/javascript'>alert('오류가 발생하였습니다."+e.Message+"');</script>";
                return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
            }

            
        }

        // 카카오톡 휴대폰번호 업데이트동의

        [GMKTAuthorize(RedirectionURLEnum.REDIRECTION_KAKAOTALK_LOGIN, UnauthorizedPolicyEnum.RedirectURL, RoleEnum.BuyerMember, RoleEnum.SellerMember)]
        [HttpPost]//[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult KakaoTalkPhoneNumAgree(KakaoTalkApplyUserM model)
        {

            KakaoTalkApplyUserM kakaoTalkApplyUser = SetApplyUserInfo();
            kakaoTalkApplyUser.UseAgree = model.UseAgree;
            kakaoTalkApplyUser.AccessChannel = model.AccessChannel;

            KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
            CustomHPNoT customT = biz.SelectUserHPNo(gmktUserProfile.CustNo);
            string UserHPNo = "";

            if (customT != null && customT.hp_no != null && (customT.hp_no.Length == 13 || customT.hp_no.Length == 12))
            {
                UserHPNo = customT.hp_no;
                kakaoTalkApplyUser.UserHPNoArr = UserHPNo.Split(new char[] { '-' });
            }
            else
            {
                kakaoTalkApplyUser.UserHPNoArr = new string[3];
                for (int i = 0; i < 3; i++)
                {
                    kakaoTalkApplyUser.UserHPNoArr[i] = "";
                }
            }
                        
            ViewBag.UserHPNo = UserHPNo;
            

            //if (kakaoTalkApplyUser.UserHPNoArr.Length != 3)
            //{
            //    kakaoTalkApplyUser.UserHPNoArr = new string[3];
            //    for (int i = 0; i < 3; i++)
            //    {
            //        kakaoTalkApplyUser.UserHPNoArr[i] = "";
            //    }
            //}

            if (model.AccessChannel == EnumOriginChannelType.GmarketApp)
            {
                return View(kakaoTalkApplyUser);
            }
            else if (model.AccessChannel == EnumOriginChannelType.KakaoTalkApp)
            {
                if (model.UseAgree)
                {
                      return View(kakaoTalkApplyUser);
                }
                else
                {
                    string sHtml = "<script type='text/javascript'>alert('사용동의 fail, 잘못된접근');</script>";
                    return Content(sHtml, "text/html", System.Text.Encoding.UTF8);

                }
            }
            else
            {
                string sHtml = "<script type='text/javascript'>alert('접근확인');</script>";
                return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
            }
        }

        // 카카오톡 인증번호
        [GMKTAuthorize(RedirectionURLEnum.REDIRECTION_KAKAOTALK_LOGIN, UnauthorizedPolicyEnum.RedirectURL, RoleEnum.BuyerMember, RoleEnum.SellerMember)]
        [HttpPost]
        public ActionResult KakaoTalkPinNumber(KakaoTalkApplyUserM model)
        {
            //System.Web.Helpers.Json
            return View(model);
        }

        // 카카오톡 맞춤정보 신청 완료
        [GMKTAuthorize(RedirectionURLEnum.REDIRECTION_KAKAOTALK_LOGIN, UnauthorizedPolicyEnum.RedirectURL, RoleEnum.BuyerMember, RoleEnum.SellerMember)]
        [HttpPost]
        public ActionResult KakaoTalkApplyComplete()
        {
            return View();

        }

        private KakaoTalkApplyUserM SetApplyUserInfo()
        {
            return new KakaoTalkApplyUserM
            {
                UseAgree = false,
                AccessChannel = EnumOriginChannelType.UnsuitChannel,
                TempUserKey = null,
                UserHPNoArr = new string[3]
            };
        }

        private EnumOriginChannelType GetOriginChannelValid(string tempUserKey, string accessChannel)
        {
            if (accessChannel == "KakaoTalkApp" && (tempUserKey != null && tempUserKey != ""))
            {
                return EnumOriginChannelType.KakaoTalkApp;
            }
            else if (accessChannel == "GmarketApp")
            {
                return EnumOriginChannelType.GmarketApp;
            }
            else 
            {
                return EnumOriginChannelType.UnsuitChannel;
            }
        }

        private bool GetCustTypeValid(EnumMemberType? custType)
        {
            if (custType == EnumMemberType.PersonalBuyer || custType == EnumMemberType.PersonalSeller)
                return true;
            else
                return false;
        }

        [HttpPost]
        public ActionResult GetAddWithTempUserKey(string tempUserKey, string accessChannel)
        {
            KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
            CommonResultT result = new CommonResultT();
            string regChannelCd = "";

            CustomHPNoT customT = biz.SelectUserHPNo(gmktUserProfile.CustNo);
            string UserHPNo = "";
            if (customT != null && customT.hp_no != null && (customT.hp_no.Length == 13 || customT.hp_no.Length == 12))
            {
                UserHPNo = customT.hp_no;
            }
            else
            {
                result.result_code = "-1005";
                result.result_message = "핸드폰번호가 고객정보에 제대로 저장되어 있지 않습니다.";
                return Json(result);
            }

            if (Convert.ToInt32(accessChannel) == (int)EnumOriginChannelType.GmarketApp)
            {
                regChannelCd = "M";
            }
            else if (Convert.ToInt32(accessChannel) == (int)EnumOriginChannelType.KakaoTalkApp)
            {
                regChannelCd = "K";
            }
            else
            {
                result.result_code = "-1006";
                result.result_message = "접근 경로 오류";
                return Json(result);
            }

            result = biz.AddUserTemp(gmktUserProfile.CustNo, UserHPNo, tempUserKey, gmktUserProfile.LoginID, regChannelCd);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetCertCode(string custHPNo)
        {
            KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
            CommonResultT result = new CommonResultT();

            result = biz.SendCertCode(custHPNo);

            return Json(result);
            
        }

        [HttpPost]
        public ActionResult GetAddWithCertCode(string custHPNo, string certCode, string accessChannel)
        {
            KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
            CommonResultT result = new CommonResultT();

            string regChannelCd = ""; 
            //stardb.dbo.kakaotalk_receiver 
            //reg_channel_cd/ cncc_channel_cd
            //F: pc회원가입
            //M: 모바일 G마켓
            //K: 모바일 카카오톡
            //A: 어드민(해지채널코드만 해당)

            if (Convert.ToInt32(accessChannel) == (int)EnumOriginChannelType.GmarketApp)
            {
                regChannelCd = "M";
            }
            else if (Convert.ToInt32(accessChannel) == (int)EnumOriginChannelType.KakaoTalkApp)
            {
                regChannelCd = "K";
            }
            else
            {
                result.result_code = "-1005";
                result.result_message = "접근 경로 오류";
                return Json(result);
            }

            result = biz.AddUser(gmktUserProfile.CustNo, custHPNo, certCode, gmktUserProfile.LoginID, regChannelCd);
            return Json(result);
        }

    }
}
