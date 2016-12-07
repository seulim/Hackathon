using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class GbankT
	{
		public GbankStatisticsT StatisticsT { get; set; }
		//public GMileageT MileageT { get; set; }
		public EbayPointT EbayPointT { get; set; }
		public CashBalanceStaticsT CashBalanceT { get; set; }
	}

	//Generated From UP_GMKTNet_GBankStatistics_SelectBalanceAmountByMember 
	//Generated From UP_GMKTNet_GBankStatistics_SelectBalanceAmountByNonmember 
	public partial class GbankStatisticsT
	{
		[Column("ETC_MONEY_SUM")]
		public Nullable<decimal> EtcMoneySum { get; set; }

		[Column("GCASH_SUM")]
		public Nullable<Int32> GcashSum { get; set; }

		[Column("MILEAGE_SUM")]
		public Nullable<Int32> MileageSum { get; set; }

		[Column("GSTAMP_SUM")]
		public Nullable<Int32> GstampSum { get; set; }

		[Column("GSTAMP_WAIT_SUM")]
		public Nullable<Int32> GstampWaitSum { get; set; }

		[Column("COUPON_QTY")]
		public Nullable<Int32> CouponQty { get; set; }

		[Column("TOKEN_SUM")]
		public Nullable<Int32> TokenSum { get; set; }

		[Column("ETC_MONEY_USABLE_SUM")]
		public Nullable<decimal> EtcMoneyUsableSum { get; set; }

		[Column("EVENT_WINNER_QTY")]
		public Nullable<Int32> EventWinnerQty { get; set; }

		[Column("EVENT_APPLICANT_QTY")]
		public Nullable<Int32> EventApplicantQty { get; set; }

		[Column("BCASH_SUM")]
		public Nullable<decimal> BcashSum { get; set; }

		[Column("BUYER_DEPOSIT")]
		public Nullable<decimal> BuyerDeposit { get; set; }

		[Column("SHOPPING_FUND")]
		public Nullable<decimal> ShoppingFund { get; set; }

		[Column("NGCASH_SUM")]
		public Nullable<decimal> NGcashSum { get; set; }

		public decimal AllMileage { get; set; }


		public GbankStatisticsT GetGbankStatistics(bool memberWay, string custNo, string custName, string buyerTelNo, DateTime dateTime)
		{
			throw new NotImplementedException();
		}
	}

	//Generated From UP_GMKTNet_MemberCommon_SelectEbayPoint 
	public partial class EbayPointT
	{
		[Column("PLUS_ISSUE_AMNT")]
		public decimal PlusIssueAmnt { get; set; }

		[Column("MINUS_ISSUE_AMNT")]
		public decimal MinusIssueAmnt { get; set; }

		[Column("REMAIN_ISSUE_AMNT")]
		public decimal RemainIssueAmnt { get; set; }

	}

	//Generated From dbo.UP_GMKTNet_Gbank_SelectTokenSum
	public partial class GTokenMain
	{
		[Column("TotalPrice")]
		public decimal TotalPrice { get; set; }
	}

	//Generated From dbo.UP_GMKTNet_GBankStatistics_SelectEtcMoneyByMember , dbo.UP_GMKTNet_GBankStatistics_SelectCashBalanceByNonMember 
	public partial class CashBalanceStaticsT
	{
		[Column("CASH_BALANCE_TOTAL_SUM")]
		public Nullable<decimal> CashBalanceTotalSum { get; set; }

		[Column("CASH_BALANCE_USABLE_SUM")]
		public Nullable<decimal> CashBalanceUsableSum { get; set; }

		[Column("CASH_BALANCE_NO_OUPUT_SUM")]
		public decimal CashBalanceNoOuputSum { get; set; }

		[Column("CASH_BALANCE_SUM")]
		public Nullable<decimal> CashBalanceSum { get; set; }

		[Column("CASH_BALANCE_OUTPUT_POSSIBLE_SUM")]
		public Nullable<decimal> CashBalanceOutputPossibleSum { get; set; }

		[Column("BUYER_DEPOSIT")]
		public Nullable<decimal> BuyerDeposit { get; set; }

		[Column("BUYER_DEPOSIT_OUTPUT_USABLE_SUM")]
		public Nullable<decimal> BuyerDepositOutputUsableSum { get; set; }
	}

	public partial class GTokenListT
	{
		[PetaPoco.Column("GIFT_TOKEN_SEQ")]
		public int GiftTokenSeq { get; set; }

		[PetaPoco.Column("REG_DT")]
		public DateTime RegDt { get; set; }

		[PetaPoco.Column("PRICE")]
		public decimal Price { get; set; }

		[PetaPoco.Column("TOTAL_PAGE")]
		public int TotalPage { get; set; }
	}

	public partial class GbankMileageInfoT
	{
		[Column("MILEAGE")]
		public Int32 Mileage { get; set; }

		[Column("BASIS_DATE")]
		public DateTime BasisDate { get; set; }

		[Column("USED_MILEAGE")]
		public Int32 UsedMileage { get; set; }

		[Column("TOTAL_MILEAGE")]
		public Int32 TotalMileage { get; set; }

		[Column("MONTH_TOTAL_GCASH")]
		public Int32 MonthTotalGcash { get; set; }

		[Column("REAL_MILEAGE")]
		public Int32 RealMileage { get; set; }

		[Column("JAEHU_MILEAGE")]
		public Int32 JaehuMileage { get; set; }
	}

	public partial class GTokenStatT
	{
		[Column("SEQ")]
		public Nullable<Int32> Seq { get; set; }

		[Column("PRICE")]
		public decimal Price { get; set; }

		[Column("STAT")]
		public string Stat { get; set; }

	}

	//Generated From UP_GMKTNet_GTokenExchange_SelectOfflineTokenCheck 
	public partial class GOfflineTokenCheckT
	{
		[Column("RET_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("RET_REASON")]
		public string RetReason { get; set; }

		[Column("TOKEN_MONEY")]
		public decimal TokenMoney { get; set; }
	}

	//Generated From UP_GMKTNet_GTokenStatus_SelectOfflineTokenStatus
	public partial class GOfflineTokenStatusT
	{
		[Column("CONFIRM_STAT")]
		public Int16 ConfirmStat { get; set; }

		[Column("NOT_CONFIRM_CASE")]
		public Int16 NotConfirmCase { get; set; }
	}

	//Generated From UP_GMKTNet_Gtokenexchange_InsertOfflineTokenBalance 
	public partial class GOfflineTokenInsertBalanceT
	{
		[Column("RET_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("RET_REASON")]
		public string RetReason { get; set; }

		[Column("BALANCE_NO")]
		public int BalanceNo { get; set; }

		[Column("SMILEPAY_NO")]
		public Int64 SmilePayNo { get; set; }
	}

	//Generated From UP_GMKTNet_GTokenExchange_InsertSttlBalanceExchange 
	public partial class GOfflineTokenInsertBalanceExchangeT
	{
		[Column("RET_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("RET_REASON")]
		public string RetReason { get; set; }
	}

	//Generated From UP_GMKTNet_GTokenOffExchange_InsertSttlBalanceNoOutputMoney 
	public partial class GOfflineTokenInsertBalanceNoOutputMoneyT
	{
		[Column("RET_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("RET_REASON")]
		public string RetReason { get; set; }
	}

	//Generated From UP_GMKTNet_GTokenOffExchange_UpdateOfflineTokenList 
	public partial class GOfflineTokenUpdateTokenListT
	{
		[Column("RET_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("RET_REASON")]
		public string RetReason { get; set; }
	}

	//Generated From UP_GMKTNet_GTokenOffExchange_SelectOfflineTokenInfo 
	public partial class GOfflineTokenExchangeT
	{
		[Column("RET_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("RET_REASON")]
		public string RetReason { get; set; }

		[Column("TOKEN_MONEY")]
		public decimal TokenMoney { get; set; }
	}

	//Generated From UP_GMKTNet_GTokenExchange_SelectOnlineTokenInfo 
	public partial class GOnlineTokenExchangeT
	{
		[Column("RET_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("RET_REASON")]
		public string RetReason { get; set; }

		[Column("TOKEN_MONEY")]
		public decimal TokenMoney { get; set; }
	}

	#region G마켓 오프라인 선물권 사용 내역
	public partial class GTokenUsedListT
	{
		[Column("USE_DT")]
		public string UseDt { get; set; }

		[Column("PRICE")]
		public decimal Price { get; set; }

		[Column("REF_NO")]
		public Nullable<Int64> RefNo { get; set; }

		[Column("CUR_PAGE_NO")]
		public Nullable<Int32> CurPageNo { get; set; }

		[Column("TOTAL_PAGE_CNT")]
		public int TotalPageCnt { get; set; }

	}
	#endregion
	#region G통장 비밀번호
	// G통장 비밀번호 변경이 필요한지 체크하는 entity
	//Generated From up_gmkt_front_check_gbank_pwd_request 
	public partial class GbankPasswordChangeRequestT
	{
		[Column("RET_VALUE")]
		public string RetValue { get; set; }

		[Column("GBANK_PWD")]
		public string GbankPwd { get; set; }

	}

	//G통장 비밀번호 확인
	//Generated From up_neo_get_Glogininfo 
	public partial class GBankloginT
	{
		[Column("GBANK_PWD")]
		public string GbankPwd { get; set; }

		[Column("CHG_DT")]
		public Nullable<DateTime> ChgDt { get; set; }

		[Column("SOCIAL_NO")]
		public string SocialNo { get; set; }

		[Column("IDENTIFICATION_NO")]
		public string IdentificationNo { get; set; }

	}
	#endregion

	//Generated From up_check_personal_vaccount_gbankno 
	public partial class CheckPersonalVaccountT
	{
		[Column("GBANK_NO")]
		public Nullable<Int32> GbankNo { get; set; }

		[Column("BANK_NM")]
		public string BankNm { get; set; }

		[Column("CO_ACNT_CD")]
		public string CoAcntCd { get; set; }
	}


	#region 장바구니 이력보기 결재내역
	//Generated From UP_GMKTNet_GBank_SelectOrderAccountList 
	public partial class GBankOrderAccountListT
	{
		[Column("ACPT_TYPE")]
		public string AcptType { get; set; }

		[Column("ACPT_DT")]
		public string AcptDt { get; set; }

		[Column("ACPT_MONEY")]
		public decimal AcptMoney { get; set; }

		[Column("BANK_NM")]
		public string BankNm { get; set; }

		[Column("VACCOUNT")]
		public string Vaccount { get; set; }

		[Column("DEPOSIT_NM")]
		public string DepositNm { get; set; }

		[Column("GBANK_NO")]
		public Nullable<Int32> GbankNo { get; set; }

	}
	#endregion

	#region 오프라인선물권 배송비 확인
	//Generated From UP_NEO_GET_OFFLINE_TOKEN_DELIVERY_YN 
	public partial class GBankGiftOfflineDeliveryYnT
	{
		[Column("DELIVERY_GROUP_NO")]
		public Nullable<Int32> DeliveryGroupNo { get; set; }

		[Column("DELIVERY_GROUP_NM")]
		public string DeliveryGroupNm { get; set; }

		[Column("BASIS_MONEY")]
		public decimal BasisMoney { get; set; }

		[Column("WHO_FEE")]
		public string WhoFee { get; set; }

		[Column("DELIVERY_FEE_CONDITION")]
		public string DeliveryFeeCondition { get; set; }

		[Column("DELIVERY_GROUP_TYPE")]
		public Nullable<Byte> DeliveryGroupType { get; set; }

		[Column("DELIVERY_FEE")]
		public decimal DeliveryFee { get; set; }
	}
	#endregion

	#region 고객별 사용가능 상품권 조회
	//Generated From up_gmkt_front_sttl_will_use_token_list 
	public partial class GiftUseTokenListT
	{
		[Column("GIFT_TOKEN_SEQ")]
		public Nullable<Int32> GiftTokenSeq { get; set; }

		[Column("REG_DT")]
		public Nullable<DateTime> RegDt { get; set; }

		[Column("PRICE")]
		public decimal Price { get; set; }

		[Column("OWN_WAY")]
		public string OwnWay { get; set; }

		[Column("STAT")]
		public string Stat { get; set; }

		[Column("USE_DT")]
		public string UseDt { get; set; }

		[Column("SEND_DETAIL")]
		public string SendDetail { get; set; }

		[Column("RECEIVE_DETAIL")]
		public string ReceiveDetail { get; set; }

		[Column("RETRANS_YN")]
		public string IsRetrans { get; set; }

		[Column("TOTAL_PAGE")]
		public Nullable<Int32> TotalPage { get; set; }

		[Column("TOT_COUNT")]
		public Nullable<Int32> TotalCount { get; set; }

	}
	#endregion

	#region 선물권 구매내역 페이징
	//Generated From up_neo_get_gift_token_list_sub_notfirst 
	public partial class GiftTokenListNoFirstT
	{
		[Column("REG_DT")]
		public Nullable<DateTime> RegDt { get; set; }

		[Column("CONTR_NO")]
		public Nullable<Int64> ContrNo { get; set; }

		[Column("TOKEN_MONEY")]
		public decimal TokenMoney { get; set; }

		[Column("CONTR_AMT")]
		public Nullable<Int32> ContrAmt { get; set; }

		[Column("ACNT_MONEY")]
		public decimal AcntMoney { get; set; }

		[Column("ACNT_STAT")]
		public string AcntStat { get; set; }

		[Column("ACNT_WAY")]
		public string AcntWay { get; set; }

		[Column("TOTAL_PAGE")]
		public Nullable<Int32> TotalPage { get; set; }

		[Column("TOTAL_PAGE1")]
		public Nullable<Int32> TotalPage1 { get; set; }

		[Column("IID")]
		public Nullable<Int32> Iid { get; set; }

		[Column("PACK_NO")]
		public Nullable<Int64> PackNo { get; set; }
	}
	#endregion

	#region 나의 쇼핑정보 전체 주문보기 - 결제 상세내역 불러오기
	//Generated From up_get_op_myshopping_total_order_account_list_for_gmarket 
	public partial class TotalOrderAccountListT
	{
		[Column("ACPT_TYPE")]
		public string AcptType { get; set; }

		[Column("ACPT_DT")]
		public string AcptDt { get; set; }

		[Column("ACPT_MONEY")]
		public decimal AcptMoney { get; set; }

		[Column("BANK_NM")]
		public string BankNm { get; set; }

		[Column("VACCOUNT")]
		public string Vaccount { get; set; }

		[Column("DEPOSIT_NM")]
		public string DepositNm { get; set; }

		[Column("GBANK_NO")]
		public Nullable<Int32> GbankNo { get; set; }
	}
	#endregion

	#region 결제정보 - 미결제 내역 보여주기
	//Generated From up_neo_get_gcash_charge_history_notpayh_CN 
	public partial class CashChargeHistoryNotpayhCnT
	{
		[Column("BANK_NM")]
		public string BankNm { get; set; }

		[Column("VACCOUNT")]
		public string Vaccount { get; set; }

		[Column("WILL_DEPOSIT_DT")]
		public Nullable<DateTime> WillDepositDt { get; set; }

		[Column("GBANK_NO")]
		public Nullable<Int32> GbankNo { get; set; }
	}
	#endregion

	#region [======= 2014 선물권리뉴얼 =======]

	#region 확인/재전송
	/// <summary>
	/// 확인/재전송
	/// </summary>
	public partial class GiftCardListT
	{
		//Gift card 번호
		[Column("TOKEN_SEQ")]
		public Nullable<Int32> TokenSeq { get; set; }

		//선물권 마스터 순번
		[Column("TOKEN_MASTER_SEQ")]
		public Nullable<Int64> TokenMasterSeq { get; set; }

		//구매 일자
		[Column("BUY_DT")]
		public Nullable<DateTime> BuyDT { get; set; }

		//금액
		[Column("PRICE")]
		public decimal Price { get; set; }

		//상태코드
		[Column("STAT")]
		public string Stat { get; set; }
		//상태코드명
		[Column("STAT_NM")]
		public string StatNM { get; set; }

		//전송방법코드
		[Column("TRANS_WAY")]
		public string Transway { get; set; }
		//전송방법명
		[Column("TRANS_WAY_NM")]
		public string TranswayNM { get; set; }

		//수신인
		[Column("RCV_NM")]
		public string RcvNM { get; set; }

		//발신자
		[Column("SEND_NM")]
		public string sendNM { get; set; }

		//Gift Card 전송일시
		[Column("SEND_DT")]
		public Nullable<DateTime> SendDT { get; set; }

		//Gift Card 인증일시
		[Column("AUTH_DT")]
		public Nullable<DateTime> AuthDT { get; set; }

		//전송 메시지
		[Column("SEND_MSG")]
		public string SendMsg { get; set; }

		//전송 메시지
		[Column("TOT_CNT")]
		public string TotalCount { get; set; }

		//전송 예약여부
		[Column("RSV_TRANS_YN")]
		public string RsvTransYn { get; set; }

		//전송가능한 상태
		[Column("SUCC_YN")]
		public string SuccYn { get; set; }
	}
	#endregion
	#region 확인/재전송
	/// <summary>
	/// 재전송
	/// </summary>
	public partial class GiftCardSendListT
	{
		//Gift card 번호
		[Column("TOKEN_SEQ")]
		public Nullable<Int32> TokenSeq { get; set; }

		//구매 일자
		[Column("BUY_DT")]
		public Nullable<DateTime> BuyDT { get; set; }

		//금액
		[Column("PRICE")]
		public decimal Price { get; set; }

		//상태코드명
		[Column("STAT_NM")]
		public string StatNM { get; set; }

		//전송방법명
		[Column("TRANS_WAY_NM")]
		public string TranswayNM { get; set; }

		//수신인
		[Column("RCV_NM")]
		public string RcvNM { get; set; }

		//발송횟수
		[Column("MOBILE_RETRANS_CNT")]
		public string MobileRetransCnt { get; set; }

	}
	public partial class GiftCardKakaoDataT
	{
		//금액
		[Column("PRICE")]
		public decimal Price { get; set; }

		//인증코드
		[Column("AUTH_16CODE")]
		public string Auth16Code { get; set; }

		//발송메세지
		[Column("SENDER_MEMO")]
		public string SenderMemo { get; set; }

	}
	public partial class GiftCardDetailListT
	{
		//Gift card 번호
		[Column("TOKEN_SEQ")]
		public Nullable<Int32> TokenSeq { get; set; }

		//선물권 마스터 순번
		[Column("TOKEN_MASTER_SEQ")]
		public Nullable<Int64> TokenMasterSeq { get; set; }

		//구매 일자
		[Column("BUY_DT")]
		public string BuyDT { get; set; }

		//금액
		[Column("PRICE")]
		public decimal Price { get; set; }

		//상태코드
		[Column("STAT")]
		public string Stat { get; set; }
		//상태코드명
		[Column("STAT_NM")]
		public string StatNM { get; set; }

		//전송방법코드
		[Column("TRANS_WAY")]
		public string Transway { get; set; }
		//전송방법명
		[Column("TRANS_WAY_NM")]
		public string TranswayNM { get; set; }

		//수신인
		[Column("RCV_NM")]
		public string RcvNM { get; set; }

		//발신자
		[Column("SEND_NM")]
		public string sendNM { get; set; }

		//Gift Card 전송일시
		[Column("SEND_DT")]
		public string SendDT { get; set; }

		//Gift Card 인증일시
		[Column("AUTH_DT")]
		public string AuthDT { get; set; }

		//전송 메시지
		[Column("SEND_MSG")]
		public string SendMsg { get; set; }

		//전송 메시지
		[Column("TOT_CNT")]
		public string TotalCount { get; set; }

		//전송 메시지
		[Column("RSV_TRANS_YN")]
		public string rsvTransYN { get; set; }

		//전송 메시지
		[Column("SUCC_YN")]
		public string SuccYN { get; set; }

		//수신인이름
		[Column("TRANS_REG_DT")]
		public string TransRegDT { get; set; }

		//배송지주소
		[Column("ZIP_CODE")]
		public string ZipCode { get; set; }

		//배송연락처
		[Column("DEL_RCV_NM")]
		public string DelRCVNM { get; set; }

		//배송연락처
		[Column("DEL_BACK_ADDRESS")]
		public string DelBackAddress { get; set; }

		//배송연락처
		[Column("DEL_FRONT_ADDRESS")]
		public string DelFrontAddress { get; set; }

		//배송연락처
		[Column("TEL_NO")]
		public string Telno { get; set; }

		//발송횟수
		[Column("MOBILE_RETRANS_CNT")]
		public string MobileRetransCnt { get; set; }

	}
	#endregion

	#region 아이디찾기
	/// <summary>
	/// 아이디찾기
	/// </summary>
	public partial class GiftSearchIDT
	{
		//고객번호
		[Column("cust_no")]
		public string Cust_No { get; set; }

		//고객이름
		[Column("cust_nm")]
		public string Cust_Nm { get; set; }

		//로그인아이디
		[Column("login_id")]
		public string LoginID { get; set; }

		//고객타입
		[Column("cust_type")]
		public string Cust_Type { get; set; }
		//
		[Column("tot_counts")]
		public string Tot_Counts { get; set; }

		//페이지카운트
		[Column("page_counts")]
		public string Page_Counts { get; set; }
		//검색상태코드
		[Column("ret_code")]
		public string Ret_Code { get; set; }
	}
	#endregion
	#region [선물권 인증확인]
	public partial class GiftCardAuthCheckResultT
	{
		//리턴코드
		[Column("RTNCODE")]
		public Nullable<Int32> RetCode { get; set; }

		//선물권 체널 (ON/CARD)
		[Column("GIFT_CHNL")]
		public string GiftChnl { get; set; }

		//선물권 일련번호
		[Column("TOKEN_SEQ")]
		public Nullable<Int32> TokenSeq { get; set; }

		//7자리 인증코드
		[Column("AUTH_7CODE")]
		public string AUth7Code { get; set; }

		//인증 선물권 금액
		[Column("TOKEN_MONEY")]
		public decimal TokenMoney { get; set; }
	}
	#endregion [선물권 인증확인]

	#region [온라인 선물권 인증확인]
	/*선물권(이메일/핸드폰) 인증 처리 페이지 (http://www.gmarket.co.kr/challenge/neo_token/email_hp_certification_rslt.asp) 로직과 동일*/
	public partial class GiftCardOnlineAuthT
	{
		[Column("RTN_CODE")]
		public Nullable<Int32> RetCode { get; set; }

		[Column("NEW_GIFT_TOKEN_SEQ")]
		public Int32 NewGiftTokenSeq { get; set; }
	}
	#endregion [온라인 선물권 인증확인]

	#region [LMS 재전송]
	/// <summary>
	/// LMS 재전송
	/// </summary>
	public partial class LmsGiftTokenT
	{
		//Gift card 번호
		[Column("GIFT_TOKEN_SEQ")]
		public Int32 GiftTokenSeq { get; set; }

		//수신자 전화번호
		[Column("RCVER_INFO_CP")]
		public string RcverInfoCp { get; set; }

		//수신자 이름
		[Column("rcv_nm")]
		public string RcvNm { get; set; }

		//메모
		[Column("SENDER_MEMO")]
		public string SenderMemo { get; set; }

		//발송 고객번호
		[Column("SEND_CUST_NO")]
		public string SendCustNo { get; set; }

		//인증코드
		[Column("AUTH_16CODE")]
		public string Auth16Code { get; set; }

		//보내는 사람 연락처
		[Column("SENDER_INFO_CP")]
		public string SenderInfoCp { get; set; }

		//보내는사람 이름
		[Column("SENDER_INFO_NA")]
		public string SenderInfoNa { get; set; }

		//보내는사람 이름
		[Column("PRICE")]
		public decimal Price { get; set; }
	}
	#endregion [LMS 재전송]

	#endregion [======= 2014 선물권리뉴얼 =======]
}
