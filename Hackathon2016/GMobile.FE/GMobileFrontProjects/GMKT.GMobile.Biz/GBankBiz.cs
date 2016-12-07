using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ArcheFx;
using ArcheFx.EnterpriseServices;

using GMKT.Framework;
using GMKT.Framework.EnterpriseServices;

using GMKT.Framework.Security;
//using GMKT.Component.Member.Util;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	[Transaction(TransactionOption.NotSupported)]
	public class GBankBiz : BizBase
	{
		public GBankBiz()
		{
		}

		#region G마켓 선물권 합계 금액
		public GTokenMain GetGTokenMain(string CustNo)
		{
			return new GBankDac().SelectGTokenPriceSum(CustNo);
		}
		#endregion

		#region G마켓 선물권 리스트
		public List<GTokenListT> GetGTokenList(string custNo, int start, int end)
		{
			return new GBankDac().SelectGTokenList(custNo, start, end);
		}
		#endregion

		#region G마켓 선물권 리스트
		public List<GTokenListT> GetGTokenLists(string custNo, int page, int pagesize)
		{
			return new GBankDac().SelectGTokenLists(custNo, page, pagesize);
		}
		#endregion

		#region 과거 데이터 가져오기
		public string GetPastWorkHistoryDate(string TblNm)
		{
			return new GBankDac().GetPastWorkHistoryDate(TblNm);
		}

		#endregion

		#region G마켓 선물권 사용가능 여부 조회
		public GTokenStatT GetGTokenStat(int iseq)
		{
			return new GBankDac().SelectGTokenStatus(iseq);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환
		public GOfflineTokenExchangeT GetOfflineTokenInfo(int seq, string auth16code, string auth7code, string reg_id, string cust_no)
		{
			return new GBankDac().SelectOfflineTokenInfo(seq, auth16code, auth7code, reg_id, cust_no);
		}
		#endregion

		#region G마켓 온라인 선물권 현금잔고 전환
		public GOnlineTokenExchangeT GetOnlineTokenInfo(int seq, string reg_id, string cust_no)
		{
			return new GBankDac().SelectOnlineTokenInfo(seq, reg_id, cust_no);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 해당 선물권이 유효한지 체크하고, 유효하면 가격을 가져온다.
		public GOfflineTokenCheckT GetOfflineTokenCheck(int seq, string auth16code, string auth7code)
		{
			return new GBankDac().SelectOfflineTokenCheck(seq, auth16code, auth7code);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 현금잔고 유입검수.
		public GOfflineTokenStatusT GetOfflineTokenStatus(string cust_no, int seq, decimal balance_money)
		{
			return new GBankDac().SelectOfflineTokenStatus(cust_no, seq, balance_money);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 체결번호 단위로 현금잔고 생성
		public GOfflineTokenInsertBalanceT SetOfflineTokenBalance(int TokenSeq, string customerNo, string login_id, decimal temp_tokenmoney, Int16 ConfirmStat, Int16 NotConfirmCase)
		{
			return new GBankDac().InsertOfflineTokenBalance(TokenSeq, customerNo, login_id, temp_tokenmoney, ConfirmStat, NotConfirmCase);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - BALANCE_NO OUTPUT 받아서 현금잔고 전환내역 INSERT(SETTLE.DBO.STTL_BALANCE_EXCHANGE)
		public GOfflineTokenInsertBalanceExchangeT SetSttlBalanceExchange(int balance_no, decimal balance_amt, int tokenseq, string reg_id, Int64 smilepay_no)
		{
			return new GBankDac().InsertSttlBalanceExchange(balance_no, balance_amt, tokenseq, reg_id, smilepay_no);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 현금잔고 충전 상세내역 기록
		public GOfflineTokenInsertBalanceNoOutputMoneyT SetSttlBalanceNoOutputMoney(int balance_no, double balance_amt, string reg_id, string cust_no)
		{
			return new GBankDac().InsertSttlBalanceNoOutputMoney(balance_no, balance_amt, reg_id, cust_no);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 전환 완료한 선물권 정보 업데이트
		public GOfflineTokenUpdateTokenListT SetOfflineTokenList(int seq, string auth_16code, string auth_7code, string reg_id, string cust_no)
		{
			return new GBankDac().UpdateOfflineTokenList(seq, auth_16code, auth_7code, reg_id, cust_no);
		}
		#endregion

		#region G마켓 종이 선물권의 사용 내역 획득
		public List<GTokenUsedListT> GetOfflineGiftTokenUsedList(string cust_no, int page)
		{
			return new GBankDac().SelectOfflineGiftTokenUsedList(cust_no, page);
		}
		#endregion

		#region

		public CheckPersonalVaccountT CheckPersonalVaccount(string custNo, int gbankNo)
		{
			return new GBankDac().GetCheckPersonalVaccount(custNo, gbankNo);
		}

		#endregion

		#region 장바구니 이력보기
		public List<GBankOrderAccountListT> GetGBankOrderAccountList(string custNo, long cartNo, string AcntWay)
		{
			return new GBankDac().SelectGBankOrderAccountList(custNo, cartNo, AcntWay);
		}
		#endregion

		#region 오프라인 선물권 갯수
		/// <summary>
		/// 구매 가능한 오프라인 선물권(종이 선물권) 수량 확인
		/// </summary>
		/// <param name="price">선물권 금액</param>
		/// <returns></returns>
		public int GetGBankGiftCount(int price)
		{
			return new GBankDac().SelectGBankGiftCount(price);
		}

		/// <summary>
		/// 구매 가능한 오프라인 선물권 종류별(온라인/종이/플라스틱) 수량 확인
		/// </summary>
		/// <param name="goods_type">선물권 형태(online_token/offine_token/plastic_token)</param>
		/// <param name="price">선물권 금액</param>
		/// <returns></returns>
		/// <remarks>2014.01.29 에이하나 이승용</remarks>
		public int GetGBankGiftCount(string gd_no, int price)
		{
			int GiftCount = 0;

			//if (!goods_type.Equals("online_token"))
			//{
			GiftCount = new GBankDac().SelectGBankGiftCount(gd_no, price);
			//}

			return GiftCount;
		}
		#endregion


		#region 오프라인선물권 배송비 확인
		public GBankGiftOfflineDeliveryYnT GetGBankOfflineDeliveryYn(string gdNo, int price, int ticketCnt, int deliveryGroupNo)
		{
			return new GBankDac().SelectGBankOfflineDeliveryYn(gdNo, price, ticketCnt, deliveryGroupNo);
		}
		#endregion

		#region 하루 한번이라도 본인 인증을 하였을 경우 한번만 처리
		public int GetGBankGiftSelfAuthInfo(string custNo)
		{
			return new GBankDac().SelectGBankGiftSelfAuthInfo(custNo);
		}
		#endregion

		#region 선물권 선물하기 가능여부 체크
		public string GetGBankGiftCheckTokenYn(int giftTokenSeq, string custNo)
		{
			return new GBankDac().SelectGBankGiftCheckTokenYn(giftTokenSeq, custNo);
		}
		#endregion

		#region 고객별 사용가능 상품권 조회
		public List<GiftUseTokenListT> GetUseGiftList(int PageNo, int PageSize, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return new GBankDac().SelectGBankGiftFrontSttWillUseToken(PageNo, PageSize, CustNo, StartDate, EndDate);
		}
		#endregion

		#region 고객별 사용한 상품권 조회
		public List<GiftUseTokenListT> GetUsedGiftList(int PageNo, int PageSize, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return new GBankDac().SelectGBankGiftFrontSttlUsedToken(PageNo, PageSize, CustNo, StartDate, EndDate);
		}
		#endregion

		#region 선물권 구매내역 페이징
		public List<GiftTokenListNoFirstT> GetGiftTokenSubNotfirstList(int Start, int End, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return new GBankDac().SelectGBankGiftTokenListSubNotfirst(Start, End, CustNo, StartDate, EndDate);
		}
		#endregion

		#region 나의 쇼핑정보 전체 주문보기 - 결제 상세내역 불러오기
		public List<TotalOrderAccountListT> GetGiftTotalOrderAccount(string CustNo, long Pack_no, string AcntWay)
		{
			return new GBankDac().SelectGBankGiftTotalOrderAccount(CustNo, Pack_no, AcntWay);
		}
		#endregion

		#region 나의 쇼핑정보 전체 주문보기 - 결제 상세내역 불러오기
		public List<CashChargeHistoryNotpayhCnT> GetCashChargeHistoryNotpayhCn(long Pack_no)
		{
			return new GBankDac().SelectCashChargeHistoryNotpayhCn(Pack_no);
		}
		#endregion

		#region 선물할 선물권 액면가 반환
		public int GetGBankGiftTokenGetPrice(int giftTokenSeq, string custNo)
		{
			return new GBankDac().SelectGBankGiftTokenGetPrice(giftTokenSeq, custNo);
		}
		#endregion

		#region 선물권 선물하기 - ID
		public int GetGBankGiftTokenSendDetailId(int giftTokenSeq, string rcvCustNo, string rcvLoginId, string rcvNm, string senderMemo, string sendCustNo)
		{
			return new GBankDac().InsertTokenSendDetailId(giftTokenSeq, rcvCustNo, rcvLoginId, rcvNm, senderMemo, sendCustNo);
		}
		#endregion

		#region 자동 발송되는 쪽지
		public int GetGBankGiftFrontSendMsgAuto(string rcvCustNo, string title, string content)
		{
			return new GBankDac().InsertFrontSendMsgAuto(rcvCustNo, title, content);
		}
		#endregion

		#region 자동 발송되는 쪽지
		public int GetGBankTokenSendDetailEmail(int tokenSeq, string rcvHpNo, string rcvEmail, string rcvNm
				, string senderMemo, string sendCustNo, string auth16Code, string auth7Code)
		{
			return new GBankDac().InsertTokenSendDetailEmail(tokenSeq, rcvHpNo, rcvEmail, rcvNm
					, senderMemo, sendCustNo, auth16Code, auth7Code);
		}
		#endregion



		#region [======= 2014 선물권리뉴얼 =======]

		#region 확인/재발송 > 보낸 Gift Card 리스트 조회
		/// <summary>
		/// 확인/재발송
		/// </summary>
		/// <param name="tab">탭명</param>
		/// <param name="PageNo">페이지번호</param>
		/// <param name="PageSize">페이지사이즈</param>
		/// <param name="CustNo">고객번호</param>
		/// <param name="StartDate">조회 시작일</param>
		/// <param name="EndDate">조회 종료일</param>
		/// <returns></returns>
		public List<GiftCardSendListT> GetGiftCardMemberSendList(string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return new GBankDac().SelectGBankGiftCardMemberSendList(CustNo, StartDate, EndDate);
		}
		public List<GiftCardListT> GetGiftCardMemberRsvList(int PageNo, int PageSize, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return new GBankDac().GetGiftCardMemberRsvList(PageNo, PageSize, CustNo, StartDate, EndDate);
		}
		#endregion 확인/재발송
		#region 확인/재발송 디테일 조회
		/// <summary>
		/// 확인/재발송
		/// </summary>
		/// <param name="tab">탭명</param>
		/// <param name="PageNo">페이지번호</param>
		/// <param name="PageSize">페이지사이즈</param>
		/// <param name="CustNo">고객번호</param>
		/// <param name="StartDate">조회 시작일</param>
		/// <param name="EndDate">조회 종료일</param>
		/// <returns></returns>
		public GiftCardDetailListT GetGiftCardItemDetail(string TOKENSEQ, string TYPE)
		{
			return new GBankDac().GetGiftCardItemDetail(TOKENSEQ, TYPE);
		}
		#endregion 확인/재발송 디테일 조회

		#region 카카오 발송데이터 조회
		public GiftCardKakaoDataT GetKakaoSendData(string TOKENSEQ)
		{
			return new GBankDac().GetKakaoSendData(TOKENSEQ);
		}
		#endregion 카카오 발송데이터 조회


		#region 선물권 주문 마스터 데이터 입력
		/// <summary>
		/// 
		/// </summary>
		/// <param name="now"></param>
		/// <param name="cust_no"></param>
		/// <param name="transway"></param>
		/// <param name="buyqty"></param>
		/// <param name="designtype"></param>
		/// <param name="sendmsgtype"></param>
		/// <param name="sendmsg"></param>
		/// <param name="senderinfocp"></param>
		/// <param name="senderinfona"></param>
		/// <param name="rsvtransyn"></param>
		/// <param name="rsvdt"></param>
		/// <param name="bulkyn"></param>
		public int SetGiftTokenOrderMst(DateTime now, string cust_no, string transway, int buyqty, string designtype, string sendmsgtype, string sendmsg, string senderinfocp, string senderinfona, string rsvtransyn, DateTime rsvdt, string bulkyn)
		{
			return new GBankDac().SetGiftTokenOrderMst(now, cust_no, transway, buyqty, designtype, sendmsgtype, sendmsg, senderinfocp, senderinfona, rsvtransyn, rsvdt, bulkyn);
		}
		#endregion 선물권 주문 마스터 데이터 입력
		#region 선물권 아이디 검색
		/// <summary>
		/// 
		/// </summary>
		/// <param name="now"></param>
		/// <param name="cust_no"></param>
		/// <param name="transway"></param>
		/// <param name="buyqty"></param>
		/// <param name="designtype"></param>
		/// <param name="sendmsgtype"></param>
		/// <param name="sendmsg"></param>
		/// <param name="senderinfocp"></param>
		/// <param name="senderinfona"></param>
		/// <param name="rsvtransyn"></param>
		/// <param name="rsvdt"></param>
		/// <param name="bulkyn"></param>
		public GiftSearchIDT GetSearchID(string Cust_No, string ID, string NAME, DateTime now, DateTime startDate)
		{
			return new GBankDac().GetSearchID(Cust_No, ID, NAME, now, startDate);
		}
		#endregion 선물권 주문 마스터 데이터 입력

		#region 선물권 주문 마스터 디테일 데이터 입력
		public void SetGiftTokenOrderDtl(int masterseq, string RCVER_INFO_ID, string RCVER_INFO_CP, string RCVER_INFO_NA, string gd_no, int orderprice, int buyqty, int ACNT_MONEY, string SUCC_YN)
		{
			new GBankDac().SetGiftTokenOrderDtl(masterseq, RCVER_INFO_ID, RCVER_INFO_CP, RCVER_INFO_NA, gd_no, orderprice, buyqty, ACNT_MONEY, SUCC_YN);
		}
		#endregion 선물권 주문 마스터 디테일 데이터 입력

		#region 선물권 인증확인
		/// <summary>
		/// 선물권 인증확인
		/// </summary>
		/// <param name="customerNo">인증 시도 고객번호</param>
		/// <param name="encOnLineAuth16code">온라인 선물권용 암호화된 인증번호</param>
		/// <param name="encOffLineAuth16code">오프라인 선물권용 암호화된 인증번호</param>
		/// <returns></returns>
		public GiftCardAuthCheckResultT GetGiftCardAuthCheck(string customerNo, string encOnLineAuth16code, string encOffLineAuth16code)
		{
			return new GBankDac().SelectGiftCardAuthCheck(customerNo, encOnLineAuth16code, encOffLineAuth16code);
		}
		#endregion 선물권 인증확인

		#region 온라인 선물권 인증확인
		/// <summary>
		/// 온라인 선물권 인증확인
		/// </summary>
		/// <param name="customerNo">인증 시도 고객번호</param>
		/// <param name="encOnLineAuth16code">온라인 선물권용 암호화된 16자리 인증번호</param>
		/// <param name="encAUth7Code">온라인 선물권용 암호화된 7자리 인증번호</param>
		/// <returns></returns>
		public GiftCardOnlineAuthT SetGiftCardOnlineAuth(int giftTokenSeq, string customerNo, string encOnLineAuth16code, string encAUth7Code)
		{
			return new GBankDac().InsertGiftCardOnlineAuth(giftTokenSeq, customerNo, encOnLineAuth16code, encAUth7Code);
		}
		#endregion 온라인 선물권 인증확인
		/// <summary>
		/// 재발송 COUNT 구하기
		/// </summary>
		/// <param name="giftTokenSeq"></param>
		/// <returns></returns>
		public int CountMobileRetrans(int giftTokenSeq)
		{
			return new GBankDac().SelectMobieRetransCnt(giftTokenSeq);
		}

		// LMS 재발송용 데이터 조회
		public LmsGiftTokenT GetGiftCardLMS(int giftTokenSeq)
		{
			return new GBankDac().SelectGiftCardLMS(giftTokenSeq);
		}

		// LMS 보내기
		public string SetGiftTokenSendMobileMessage(int giftTokenSeq, string callbackUrl)
		{
			LmsGiftTokenT lmsGift = new LmsGiftTokenT();
			string retCode = string.Empty;
			string msgType = string.Empty;
			msgType = "LMS";

			string tranPhone = string.Empty;
			string tranCallback = string.Empty;
			DateTime tranDate = DateTime.Now;
			string tranTitle = "G마켓 Gift Card가 도착했어요!";
			string tranMsg = string.Empty;
			int tranStatus = 1;
			string regId = string.Empty;
			string designType = string.Empty;
			decimal tokenPrice = 0;
			string au16Code = string.Empty;

			lmsGift = GetGiftCardLMS(giftTokenSeq);

			au16Code = lmsGift.Auth16Code;
			tokenPrice = lmsGift.Price;
			tranPhone = lmsGift.RcverInfoCp;
			tranCallback = lmsGift.SenderInfoCp;
			tranMsg = lmsGift.SenderMemo;
			tranMsg = makeTokenMobileMsg(tranMsg, au16Code, tokenPrice, callbackUrl);
			regId = lmsGift.SendCustNo;

			retCode = new GBankDac().InsertGiftTokenSendLMS(tranPhone, tranCallback, tranDate, tranTitle, tranMsg, tranStatus, regId);

			// 
			if (retCode == "0")
				return new GBankDac().SetGiftTokenSendCount(giftTokenSeq);
			else
				return retCode;
		}
		public string SetGiftTokenCancelSendMobile(int giftTokenSeq, string telNo)
		{
			string retCode = string.Empty;
			retCode = new GBankDac().SetGiftTokenCancelSendMobile(giftTokenSeq, telNo);

			// 
			return retCode;
		}
		/// <summary>
		/// mobile 문자내용 만들기
		/// </summary>
		/// <param name="auth16identification"></param>
		/// <param name="sTokenPrice"></param>
		/// <returns></returns>
		public string makeTokenMobileMsg(string msg, string auth16identification, decimal sTokenPrice, string callbackUrl)
		{
			string mobileMsg = string.Empty;
			string decriptAuth16 = string.Empty;

			decriptAuth16 = GMKT.Framework.Security.GMKTCryptoLibraryOption.DecodeFrom64(auth16identification);

			string money = String.Format("{0:#,###}", Convert.ToInt32(sTokenPrice));
			mobileMsg = mobileMsg + System.Environment.NewLine;
			mobileMsg = mobileMsg + "G마켓에서 현금처럼 사용할 수 있는 Gift Card입니다" + System.Environment.NewLine;
			mobileMsg = mobileMsg + "-인증번호 : " + decriptAuth16 + System.Environment.NewLine;
			mobileMsg = mobileMsg + System.Environment.NewLine;
			mobileMsg = mobileMsg + msg + System.Environment.NewLine;
			mobileMsg = mobileMsg + System.Environment.NewLine;
			mobileMsg = mobileMsg + "-상품명: G마켓 Gift Card " + money + "원" + System.Environment.NewLine;
			mobileMsg = mobileMsg + "-이용안내: " + System.Environment.NewLine;
			mobileMsg = mobileMsg + "1. 아래 링크를 누르시면 G마켓 APP이 실행되고 인증화면으로 이동합니다." + System.Environment.NewLine;
			mobileMsg = mobileMsg + callbackUrl + "?RegCode=" + decriptAuth16 + System.Environment.NewLine;
			mobileMsg = mobileMsg + "2. 선물받은 금액이 현금잔고로 전환 되었는지 확인" + System.Environment.NewLine;
			mobileMsg = mobileMsg + "3. 현금잔고로 쇼핑하기!" + System.Environment.NewLine;
			mobileMsg = mobileMsg + "-유효기간: 구매일로부터 5년" + System.Environment.NewLine;
			mobileMsg = mobileMsg + "-이용문의: 1566-5701" + System.Environment.NewLine;

			//mobileMsg = msg + System.Environment.NewLine;
			//mobileMsg = mobileMsg + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "G마켓 사이트에서 현금처럼 사용할 수 있는 Gift Card입니다" + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-인증번호 : " + decriptAuth16 + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-상품명: G마켓 Gift Card " + Convert.ToInt32(sTokenPrice).ToString() + "원" + System.Environment.NewLine;
			////mobileMsg = mobileMsg + "-상품명: G마켓 Gift Card " + sTokenPrice.ToString() + "원" + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-유효기간: 구매일로부터 5년 " + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-이용문의: 1566-5701" + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-이용안내: " + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "1. G마켓 홈페이지> G마켓 Gift Card 페이지 >등록하기 에서 인증번호를 입력" + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "(아래 URL 누르면 바로 이동 가능합니다)"+System.Environment.NewLine;
			//mobileMsg = mobileMsg + "2. 선물받은 금액이 현금잔고로 전환 되었는지 확인"+System.Environment.NewLine;
			//mobileMsg = mobileMsg + "3. 현금잔고로 쇼핑하기!" + System.Environment.NewLine;
			//mobileMsg = mobileMsg + callbackUrl + "?RegCode="+ decriptAuth16 + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "(위 URL은 G마켓 APP으로 연결 됩니다.)"+System.Environment.NewLine;

			//mobileMsg = msg + System.Environment.NewLine;
			//mobileMsg = mobileMsg + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-인증번호 : " + decriptAuth16;
			//mobileMsg = mobileMsg + "-상품명: G마켓 Gift Card " + sTokenPrice.ToString() + "원"+System.Environment.NewLine + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-유효기간: 구매일로부터 5년 " + System.Environment.NewLine+System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-이용문의: 1566-5701" + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "-이용안내: 1. G마켓 홈페이지< G마켓 Gift Card 페이지< 등록하기 에서 인증번호를 입력 (아래 URL 누르면 바로 이동 가능합니다) " + System.Environment.NewLine;
			//mobileMsg = mobileMsg + "2. 선물받은 금액이 현금잔고로 전환 되었는지 확인 3. 현금잔고로 쇼핑하기! " + System.Environment.NewLine;
			//mobileMsg = mobileMsg + callbackUrl + System.Environment.NewLine;

			return mobileMsg;
		}

		#endregion [======= 2014 선물권리뉴얼 =======]


	}
}
