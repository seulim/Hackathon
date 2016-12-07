using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using System.Text;
using GMKT.Framework.Security;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Controllers
{
	public class CashBalanceController : GMobileControllerBase
    {
        //
        // GET: /CashBalance/

        public ActionResult Index()
        {
            return View();
        }

				#region [======= 2014 선물권리뉴얼 =======]
				#region 받은 G마켓 Gift Card 등록하기
		public ActionResult GiftCardAuthentication(string Auth16code)
		{
			string customerNo = gmktUserProfile.CustNo;
			string login_id = gmktUserProfile.LoginID;
			string GiftChnl = string.Empty;
			int TokenSeq;
			string encAUth7Code = string.Empty;
			decimal temp_tokenmoney = 0;
			int balance_no = 0;
			int newTokenSeq;		// 새로 발급된 gift_token_seq
			Int64 smilepay_no = 0;

			GiftCardAuthCheckResultM AjaxModel = new GiftCardAuthCheckResultM();

			string encOnLineAuth16code = System.Convert.ToBase64String(Encoding.ASCII.GetBytes(Auth16code.ToUpper()));
			string encOffLineAuth16code = GMKTCryptoLibrary.AesCryptoGMKTEncrypt(Auth16code.ToUpper());

			//선물권 인증확인
			GiftCardAuthCheckResultT result = new GBankBiz().GetGiftCardAuthCheck(customerNo, encOnLineAuth16code, encOffLineAuth16code);

			if (result.RetCode != 0)
			{
				AjaxModel = new GiftCardAuthCheckResultM
				{
					RetCode = (int)result.RetCode,
					GiftChnl = result.GiftChnl,
					TokenMoney = result.TokenMoney
				};
			}
			else
			{
				GiftChnl = result.GiftChnl;
				TokenSeq = (int)result.TokenSeq;
				encAUth7Code = result.AUth7Code;
				temp_tokenmoney = result.TokenMoney;

				if (GiftChnl == "ON")
				{
					//온라인 선물권
					//* 선물권(이메일/핸드폰) 인증 처리 페이지 (http://www.gmarket.co.kr/challenge/neo_token/email_hp_certification_rslt.asp) 로직과 동일 *//
					//온라인 선물권 인증 처리
					GiftCardOnlineAuthT OnLineResult = new GBankBiz().SetGiftCardOnlineAuth(TokenSeq, customerNo, encOnLineAuth16code, encAUth7Code);

					if (OnLineResult.RetCode != 100)
					{
						AjaxModel = new GiftCardAuthCheckResultM
						{
							RetCode = -3,
							RetReason = OnLineResult.RetCode == null ? "이미 Gift Card를 수령하였거나 취소된 선물권입니다." : "사용할 수 없는 인증번호 입니다\n확인 후 다시 입력해 주세요."
						};
					}
					else
					{
						newTokenSeq = OnLineResult.NewGiftTokenSeq;

						//인증이 정상적으로 처리되면 현금 전환
						//*ExchangeOnlineToken()와 동일*//
						//GOnlineTokenExchangeT OnLineExchangeResult = new GBankBiz().GetOnlineTokenInfo(TokenSeq, login_id, customerNo);

						//smile cash
						//상위 '온라인 선물권 사용하기'(ExchangeOnlineToken) 에서 신규로 추가된 biz 를 호출하도록 한다.
						GOnlineTokenExchangeT OnLineExchangeResult = new GBankBiz().GetOnlineTokenInfo(newTokenSeq, login_id, customerNo);

						if (OnLineExchangeResult.RetCode < 0)
						{
							//현금전환 오류
							AjaxModel = new GiftCardAuthCheckResultM
							{
								RetCode = -4,
								RetReason = OnLineExchangeResult.RetReason,
								TokenMoney = result.TokenMoney
							};
						}
						else
						{
							//현금 전환 성공
							AjaxModel = new GiftCardAuthCheckResultM
							{
								RetCode = 0,
								RetReason = OnLineExchangeResult.RetReason,
								TokenMoney = result.TokenMoney
							};
						}
					}
				}
				else
				{
					//카드(오프라인) 선물권
					//* 아래 부터 ExchangeOfflineToken() 부분과 동일 *//

					// G마켓 오프라인 선물권 현금잔고 전환 - 현금잔고 유입검수.
					//GOfflineTokenStatusT Stat = new GBankBiz().GetOfflineTokenStatus(customerNo, TokenSeq, temp_tokenmoney);

					Int16 ConfirmStat;
					Int16 NotConfirmCase;
					ConfirmStat = 0;
					NotConfirmCase = 0;

					//if (Stat == null) //왠지 찜찜..
					//{
					//    ConfirmStat = 0;
					//    NotConfirmCase = 0;
					//}
					//else
					//{
					//    ConfirmStat = Stat.ConfirmStat;
					//    NotConfirmCase = Stat.NotConfirmCase;
					//}

					//마켓 오프라인 선물권 현금잔고 전환 - 체결번호 단위로 현금잔고 생성
					GOfflineTokenInsertBalanceT Balance = new GBankBiz().SetOfflineTokenBalance(TokenSeq, customerNo, login_id, temp_tokenmoney, ConfirmStat, NotConfirmCase);

					//if (Balance.RetCode == 0 && Balance.BalanceNo != 0)
					if (Balance.RetCode == 0 && Balance.SmilePayNo != 0)
					{
						balance_no = Balance.BalanceNo;
						smilepay_no = Balance.SmilePayNo;

						//G마켓 오프라인 선물권 현금잔고 전환 - BALANCE_NO OUTPUT 받아서 현금잔고 전환내역 INSERT(SETTLE.DBO.STTL_BALANCE_EXCHANGE)
						GOfflineTokenInsertBalanceExchangeT result2 = new GBankBiz().SetSttlBalanceExchange(balance_no, temp_tokenmoney, TokenSeq, login_id, smilepay_no);

						if (result2.RetCode == 0)
						{
							double tmp = Convert.ToDouble(temp_tokenmoney);

							if (tmp > 10000)
							{
								tmp = tmp * 0.6;
							}
							else
							{
								tmp = tmp * 0.8;
							}

							//G마켓 오프라인 선물권 현금잔고 전환 - 현금잔고 충전 상세내역 기록
							//GOfflineTokenInsertBalanceNoOutputMoneyT result3 = new GBankBiz().SetSttlBalanceNoOutputMoney(balance_no, tmp, login_id, customerNo);

							//if (result3.RetCode == 0)
							if (1 == 1)
							{
								//G마켓 오프라인 선물권 현금잔고 전환 - 전환 완료한 선물권 정보 업데이트
								GOfflineTokenUpdateTokenListT final_result = new GBankBiz().SetOfflineTokenList(TokenSeq, encOffLineAuth16code, encAUth7Code, login_id, customerNo);

								if (final_result.RetCode == 0)
								{
									AjaxModel = new GiftCardAuthCheckResultM
									{
										RetCode = (int)final_result.RetCode,
										RetReason = final_result.RetReason,
										TokenMoney = result.TokenMoney
									};
								}
								else
								{
									AjaxModel = new GiftCardAuthCheckResultM
									{
										RetCode = (int)final_result.RetCode,
										RetReason = final_result.RetReason,
										TokenMoney = result.TokenMoney
									};
								}
							}
							else
							{
								AjaxModel = new GiftCardAuthCheckResultM
								{
									RetCode = 0,//(int)result3.RetCode,
									RetReason = "",//result3.RetReason,
									TokenMoney = 0
								};
							}
						}
						else
						{
							AjaxModel = new GiftCardAuthCheckResultM
							{
								RetCode = (int)result2.RetCode,
								RetReason = result2.RetReason,
								TokenMoney = 0
							};
						}
					}
					else
					{
						AjaxModel = new GiftCardAuthCheckResultM
						{
							RetCode = (int)Balance.RetCode,
							RetReason = Balance.RetReason,
							TokenMoney = 0
						};
					}
				}
			}

			return Json(AjaxModel);
		}
				#endregion 종이 선물권 사용하기 (전환하기)
				#endregion [======= 2014 선물권리뉴얼 =======]

    }
}
