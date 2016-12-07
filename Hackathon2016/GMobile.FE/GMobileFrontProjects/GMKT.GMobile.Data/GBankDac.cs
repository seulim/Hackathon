using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.Data;
using GMKT.Framework.EnterpriseServices;

namespace GMKT.GMobile.Data
{
	public class GBankDac : MicroDacBase
	{
		#region All Mileage Info
		public EbayPointT SelectEbayPoint(int ebayCustNo)
		{
			return MicroDacHelper.SelectSingleEntity<EbayPointT>(
				"pointdb_read",
				"dbo.UP_GMKTNet_MemberCommon_SelectEbayPoint",
				MicroDacHelper.CreateParameter("@EBAY_CUST_NO", ebayCustNo, SqlDbType.Int)
			);
		}
		#endregion

		#region G통장 통계 data (회원)
		public GbankStatisticsT SelectGbankStatistics(string CustNo, DateTime NowDate, string LisNos)
		{
			return MicroDacHelper.SelectSingleEntity<GbankStatisticsT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GBankStatistics_SelectBalanceAmountByMember",
				MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@GETDATE", NowDate, SqlDbType.SmallDateTime),
				MicroDacHelper.CreateParameter("@LIS_NOS", LisNos, SqlDbType.VarChar, 2000)
			);
		}
		#endregion

		#region G통장 통계 data (비회원)
		public GbankStatisticsT SelectGbankStatistics(string CustNo, string CustName, string CustTelNo)
		{
			return MicroDacHelper.SelectSingleEntity<GbankStatisticsT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GBankStatistics_SelectBalanceAmountByNonmember",
				MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@CUST_NM", CustName, SqlDbType.VarChar, 20),
				MicroDacHelper.CreateParameter("@CUST_TEL", CustTelNo, SqlDbType.VarChar, 15)
			);
		}
		#endregion

		#region G마켓 선물권 합계 금액
		public GTokenMain SelectGTokenPriceSum(string CustNo)
		{
			return MicroDacHelper.SelectSingleEntity<GTokenMain>(
				"tiger_read",
				"dbo.UP_GMKTNet_Gbank_SelectTokenSum",
				MicroDacHelper.CreateParameter("@cust_no", CustNo, SqlDbType.VarChar, 10)
			);
		}
		#endregion

		#region G마켓 선물권 리스트
		public List<GTokenListT> SelectGTokenList(string custNo, int start, int end)
		{
			return MicroDacHelper.SelectMultipleEntities<GTokenListT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GTokenExchange_SelectTokenListGbankNonFisrt",
				MicroDacHelper.CreateParameter("@start", start, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@end", end, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10)
			);
		}
		#endregion

		#region G마켓 선물권 리스트
		public List<GTokenListT> SelectGTokenLists(string custNo, int page, int pagesize)
		{
			return MicroDacHelper.SelectMultipleEntities<GTokenListT>(
				"tiger_read",
				"dbo.UP_NEO_GET_BUY_TOKEN_LIST_GBANK",
				MicroDacHelper.CreateParameter("@page", page, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@pagesize", pagesize, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10)
			);
		}
		#endregion

		#region 과거 데이터 가져오기
		public string GetPastWorkHistoryDate(string TblNm)
		{
			return MicroDacHelper.SelectScalar<string>(
				"tiger_read",
				"dbo.UP_GMKTNet_GBankStatistics_SelectPastWorkHistoryDate",
				MicroDacHelper.CreateParameter("@TBL_NM", TblNm, SqlDbType.VarChar, 100)
			);
		}
		#endregion

		#region G통장 마일리지 정보
		public GbankMileageInfoT SelectGbankMileageInfo(string CustNo)
		{
			return MicroDacHelper.SelectSingleEntity<GbankMileageInfoT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GBankStatistics_SelectCustomMileageInfo",
				MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10)
			);
		}
		#endregion

		#region G마켓 선물권 사용가능 여부 조회
		public GTokenStatT SelectGTokenStatus(int iseq)
		{
			return MicroDacHelper.SelectSingleEntity<GTokenStatT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GTokenStat_SelectOfflineTokenCheck",
				MicroDacHelper.CreateParameter("@SEQ", iseq, SqlDbType.Int)
			);
		}
		#endregion

		#region G마켓 선물권 오프라인 선물권 현금잔고 전환
		public GOfflineTokenExchangeT SelectOfflineTokenInfo(int seq, string auth16codoe, string auth7code, string reg_id, string cust_no)
		{
			return MicroDacHelper.SelectSingleEntity<GOfflineTokenExchangeT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GTokenOffExchange_SelectOfflineTokenInfo",
				MicroDacHelper.CreateParameter("@SEQ", seq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@AUTH_16CODE", auth16codoe, SqlDbType.VarChar, 32),
				MicroDacHelper.CreateParameter("@AUTH_7CODE", auth7code, SqlDbType.VarChar, 32),
				MicroDacHelper.CreateParameter("@REG_ID", reg_id, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@CUST_NO", cust_no, SqlDbType.VarChar, 10)

			);
		}
		#endregion

		#region G마켓 선물권 온라인 선물권 현금잔고 전환
		public GOnlineTokenExchangeT SelectOnlineTokenInfo(int seq, string reg_id, string cust_no)
		{
			return MicroDacHelper.SelectSingleEntity<GOnlineTokenExchangeT>(
				"tiger_write",
				"dbo.UP_GMKTNet_GTokenExchange_SelectOnlineTokenInfo",
				MicroDacHelper.CreateParameter("@gift_token_seq", seq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@REG_ID", reg_id, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@CUST_NO", cust_no, SqlDbType.VarChar, 10)
			);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 해당 선물권이 유효한지 체크하고, 유효하면 가격을 가져온다.
		public GOfflineTokenCheckT SelectOfflineTokenCheck(int seq, string auth16codoe, string auth7code)
		{
			return MicroDacHelper.SelectSingleEntity<GOfflineTokenCheckT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GTokenExchange_SelectOfflineTokenCheck",
				MicroDacHelper.CreateParameter("@SEQ", seq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@AUTH_16CODE", auth16codoe, SqlDbType.VarChar, 32),
				MicroDacHelper.CreateParameter("@AUTH_7CODE", auth7code, SqlDbType.VarChar, 32)

			);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 현금잔고 유입검수.
		public GOfflineTokenStatusT SelectOfflineTokenStatus(string cust_no, int seq, decimal balance_money)
		{
			return MicroDacHelper.SelectSingleEntity<GOfflineTokenStatusT>(
				"tiger_write",
				"dbo.UP_GMKTNet_GTokenStatus_SelectOfflineTokenStatus",
				MicroDacHelper.CreateParameter("@cust_no", cust_no, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@cust_nm", string.Empty, SqlDbType.VarChar, 50),
				MicroDacHelper.CreateParameter("@tel_no", string.Empty, SqlDbType.VarChar, 20),
				MicroDacHelper.CreateParameter("@balance_type", "T5", SqlDbType.Char, 2),
				MicroDacHelper.CreateParameter("@ref_no", seq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@balance_money", balance_money, SqlDbType.Money),
				MicroDacHelper.CreateParameter("@gcash_to_balance", 0, SqlDbType.Money)
			);
		}
		#endregion   

		#region G마켓 오프라인 선물권 현금잔고 전환 - 체결번호 단위로 현금잔고 생성
		public GOfflineTokenInsertBalanceT InsertOfflineTokenBalance(int TokenSeq, string customerNo, string login_id, decimal temp_tokenmoney, Int16 ConfirmStat, Int16 NotConfirmCase)
		{
			return MicroDacHelper.SelectSingleEntity<GOfflineTokenInsertBalanceT>(
				"tiger_write",
				"dbo.UP_GMKTNet_Gtokenexchange_InsertOfflineTokenBalance",
				MicroDacHelper.CreateParameter("@contr_no", 0, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@pack_no", 0, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@ref_no", TokenSeq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@cust_no", customerNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@cust_nm", "", SqlDbType.VarChar, 50),
				MicroDacHelper.CreateParameter("@tel_no", "", SqlDbType.VarChar, 20),
				MicroDacHelper.CreateParameter("@reg_id", login_id, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@balance_amt", temp_tokenmoney, SqlDbType.Money),
				MicroDacHelper.CreateParameter("@balance_type", "T5", SqlDbType.Char, 2),
				MicroDacHelper.CreateParameter("@confirm_stat", ConfirmStat, SqlDbType.TinyInt),
				MicroDacHelper.CreateParameter("@not_confirm_case", NotConfirmCase, SqlDbType.TinyInt),
				MicroDacHelper.CreateParameter("@comments", "", SqlDbType.VarChar, 60),
				MicroDacHelper.CreateParameter("@cust_type", "BC", SqlDbType.Char, 2),
				MicroDacHelper.CreateParameter("@not_present_amt", 0, SqlDbType.Money)
			);
		}
		#endregion   

		#region G마켓 오프라인 선물권 현금잔고 전환 - BALANCE_NO OUTPUT 받아서 현금잔고 전환내역 INSERT(SETTLE.DBO.STTL_BALANCE_EXCHANGE)
		public GOfflineTokenInsertBalanceExchangeT InsertSttlBalanceExchange(int balance_no, decimal balance_amt, int tokenseq, string reg_id, Int64 smilepay_no)
		{
			return MicroDacHelper.SelectSingleEntity<GOfflineTokenInsertBalanceExchangeT>(
				"tiger_write",
				"dbo.UP_GMKTNet_GTokenExchange_InsertSttlBalanceExchange",
				MicroDacHelper.CreateParameter("@balance_no", balance_no, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@balance_amt", balance_amt, SqlDbType.Money),
				MicroDacHelper.CreateParameter("@ref_no", tokenseq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@reg_id", reg_id, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@smilepay_no", smilepay_no, SqlDbType.BigInt)

			);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 현금잔고 충전 상세내역 기록
		public GOfflineTokenInsertBalanceNoOutputMoneyT InsertSttlBalanceNoOutputMoney(int balance_no, double balance_amt, string reg_id, string cust_no)
		{
			return MicroDacHelper.SelectSingleEntity<GOfflineTokenInsertBalanceNoOutputMoneyT>(
				"tiger_write",
				"dbo.UP_GMKTNet_GTokenOffExchange_InsertSttlBalanceNoOutputMoney",
				MicroDacHelper.CreateParameter("@kind", 1, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@balance_no", balance_no, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@use_no_output_money", balance_amt, SqlDbType.Money),
				MicroDacHelper.CreateParameter("@reg_id", reg_id, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@cust_no", cust_no, SqlDbType.VarChar, 10)

			);
		}
		#endregion

		#region G마켓 오프라인 선물권 현금잔고 전환 - 전환 완료한 선물권 정보 업데이트
		public GOfflineTokenUpdateTokenListT UpdateOfflineTokenList(int seq, string auth_16code, string auth_7code, string reg_id, string cust_no)
		{
			return MicroDacHelper.SelectSingleEntity<GOfflineTokenUpdateTokenListT>(
				"tiger_write",
				"dbo.UP_GMKTNet_GTokenOffExchange_UpdateOfflineTokenList",
				MicroDacHelper.CreateParameter("@SEQ", seq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@AUTH_16CODE", auth_16code, SqlDbType.VarChar, 32),
				MicroDacHelper.CreateParameter("@AUTH_7CODE", auth_7code, SqlDbType.VarChar, 32),
				MicroDacHelper.CreateParameter("@REG_ID", reg_id, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@CUST_NO", cust_no, SqlDbType.VarChar, 10)

			);
		}
		#endregion

		#region
		public List<GTokenUsedListT> SelectOfflineGiftTokenUsedList(string cust_no, int page)
		{
			return MicroDacHelper.SelectMultipleEntities<GTokenUsedListT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GTokenUsedList_OfflineGiftTokenUsedlist",
				MicroDacHelper.CreateParameter("@cust_no", cust_no, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@show_page", page, SqlDbType.Int)
			);
		}
		#endregion

		#region G통장 비밀번호 로그인
		// G통장 비밀번호 변경이 필요한지 체크하는 entity
		public GbankPasswordChangeRequestT CheckGbankPasswordChange(string custNo, string gbankPwd, string gbankPd01)
		{
			return MicroDacHelper.SelectSingleEntity<GbankPasswordChangeRequestT>(
				"tiger_read",
				"dbo.up_gmkt_front_check_gbank_pwd_request",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@gbank_pwd", gbankPwd, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@gbank_pd01", gbankPd01, SqlDbType.VarChar, 128)
			);
		}

		// G통장 비밀번호로 로그인
		public GBankloginT GbankLogin(string custNo, string gbankPwd, string gbankPd01)
		{
			return MicroDacHelper.SelectSingleEntity<GBankloginT>(
				"tiger_read",
				"dbo.up_neo_get_Glogininfo",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@gbank_pwd", gbankPwd, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@gbank_pd01", gbankPd01, SqlDbType.VarChar, 128)
			);
		}
		#endregion

		#region 개인별 전용가상계좌의 계좌정보를 가지고온다.
		public CheckPersonalVaccountT GetCheckPersonalVaccount(string custNo, int gbankNo)
		{
			return MicroDacHelper.SelectSingleEntity<CheckPersonalVaccountT>(
					"tiger_read",
					"dbo.up_check_personal_vaccount_gbankno",
					MicroDacHelper.CreateParameter("@CUST_NO", custNo, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@GBANK_NO", gbankNo, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@SERVICE_CD", "", SqlDbType.VarChar, 12)
			);
		}
		#endregion

		//#region 개인별 전용가상계좌의 계좌정보를 가지고온다.
		// public CheckPersonalVaccountT GetCheckPersonalVaccount(string custNo, int gbankNo)
		// {
		//     return MicroDacHelper.SelectSingleEntity<CheckPersonalVaccountT>(
		//         "tiger_read",
		//         "dbo.up_check_personal_vaccount_gbankno",
		//         MicroDacHelper.CreateParameter("@CUST_NO", custNo, SqlDbType.VarChar, 10),
		//         MicroDacHelper.CreateParameter("@GBANK_NO", gbankNo, SqlDbType.Int),
		//         MicroDacHelper.CreateParameter("@SERVICE_CD", "", SqlDbType.VarChar,12)
		//     );
		// }
		// #endregion		

		#region 장바구니 이력보기 결재내역
		public List<GBankOrderAccountListT> SelectGBankOrderAccountList(string custNo, long cartNo, string AcntWay)
		{
			return MicroDacHelper.SelectMultipleEntities<GBankOrderAccountListT>(
				"tiger_read",
				"dbo.UP_GMKTNet_GBank_SelectOrderAccountList",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@pack_no", cartNo, SqlDbType.BigInt),
				MicroDacHelper.CreateParameter("@acnt_way", AcntWay, SqlDbType.Char, 2)
			);
		}
		#endregion

		#region 오프라인 선물권 갯수
		/// <summary>
		/// 구매 가능한 오프라인 선물권(종이 선물권) 수량 확인
		/// </summary>
		/// <param name="price">선물권 금액</param>
		/// <returns></returns>
		public int SelectGBankGiftCount(int price)
		{
			return MicroDacHelper.SelectScalar<int>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectOfflineGiftCount",
					MicroDacHelper.CreateParameter("@PRICE", price, SqlDbType.Money)
					);
		}

		/// <summary>
		/// 구매 가능한 오프라인 선물권 종류별(온라인/종이/플라스틱) 수량 확인
		/// </summary>
		/// <param name="goods_type">선물권 형태(online_token/offine_token/plastic_token)</param>
		/// <param name="price">선물권 금액</param>
		/// <returns></returns>
		/// <remarks>2014.01.29 에이하나 이승용</remarks>
		public int SelectGBankGiftCount(string gd_no, int price)
		{
			return MicroDacHelper.SelectScalar<int>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectOfflineGiftCount2",
					MicroDacHelper.CreateParameter("@GD_NO", gd_no, SqlDbType.VarChar, 15),
					MicroDacHelper.CreateParameter("@PRICE", price, SqlDbType.Money)
					);
		}
		#endregion

		#region 오프라인선물권 배송비 확인
		public GBankGiftOfflineDeliveryYnT SelectGBankOfflineDeliveryYn(string gdNo, int price, int ticketCnt, int deliveryGroupNo)
		{
			return MicroDacHelper.SelectSingleEntity<GBankGiftOfflineDeliveryYnT>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectOfflineGiftDeliveryYn",
					MicroDacHelper.CreateParameter("@GD_NO", gdNo, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@PRICE", price, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@TICKET_CNT", ticketCnt, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@DELIVERY_GROUP_NO", deliveryGroupNo, SqlDbType.Int)
			);
		}
		#endregion

		#region 하루 한번이라도 본인 인증을 하였을 경우 한번만 처리
		public int SelectGBankGiftSelfAuthInfo(string custNo)
		{
			return MicroDacHelper.SelectScalar<int>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_GetSelfAuthInfo",
					MicroDacHelper.CreateParameter("@CUST_NO", custNo, SqlDbType.VarChar, 10)
					);
		}
		#endregion

		#region 선물권 선물하기 가능여부 체크
		public string SelectGBankGiftCheckTokenYn(int giftTokenSeq, string custNo)
		{
			return MicroDacHelper.SelectScalar<string>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectCheckTokenYn",
					MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", giftTokenSeq, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@CUST_NO", custNo, SqlDbType.VarChar, 10)
					);
		}
		#endregion

		#region 고객별 사용가능 상품권 조회
		public List<GiftUseTokenListT> SelectGBankGiftFrontSttWillUseToken(int PageNo, int PageSize, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return MicroDacHelper.SelectMultipleEntities<GiftUseTokenListT>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectFrontSttWillUseToken",
					MicroDacHelper.CreateParameter("@PAGE", PageNo, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@PAGESIZE", PageSize, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@START_DT", StartDate, SqlDbType.DateTime),
					MicroDacHelper.CreateParameter("@END_DT", EndDate, SqlDbType.DateTime)
			);
		}
		#endregion

		#region 고객별 사용한 상품권 조회
		public List<GiftUseTokenListT> SelectGBankGiftFrontSttlUsedToken(int PageNo, int PageSize, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return MicroDacHelper.SelectMultipleEntities<GiftUseTokenListT>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectFrontSttlUsedToken",
					MicroDacHelper.CreateParameter("@PAGE", PageNo, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@PAGESIZE", PageSize, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@START_DT", StartDate, SqlDbType.DateTime),
					MicroDacHelper.CreateParameter("@END_DT", EndDate, SqlDbType.DateTime)
			);
		}
		#endregion

		#region 선물권 구매내역 페이징
		public List<GiftTokenListNoFirstT> SelectGBankGiftTokenListSubNotfirst(int Start, int End, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return MicroDacHelper.SelectMultipleEntities<GiftTokenListNoFirstT>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectTokenListSubNotfirst",
					MicroDacHelper.CreateParameter("@START", Start, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@END", End, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@START_DT", StartDate, SqlDbType.DateTime),
					MicroDacHelper.CreateParameter("@END_DT", EndDate, SqlDbType.DateTime)
			);
		}
		#endregion

		#region 나의 쇼핑정보 전체 주문보기 - 결제 상세내역 불러오기
		public List<TotalOrderAccountListT> SelectGBankGiftTotalOrderAccount(string CustNo, long Pack_no, string AcntWay)
		{
			return MicroDacHelper.SelectMultipleEntities<TotalOrderAccountListT>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectTotalOrderAccountList",
					MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@PACK_NO", Pack_no, SqlDbType.BigInt),
					MicroDacHelper.CreateParameter("@ACNT_WAY", AcntWay, SqlDbType.VarChar, 2)
			);
		}
		#endregion

		#region 결제정보 - 미결제 내역 보여주기
		public List<CashChargeHistoryNotpayhCnT> SelectCashChargeHistoryNotpayhCn(long Pack_no)
		{
			return MicroDacHelper.SelectMultipleEntities<CashChargeHistoryNotpayhCnT>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_SelectGcashChargeHistoryNotpayhCN",
					MicroDacHelper.CreateParameter("@PACK_NO", Pack_no, SqlDbType.BigInt)
			);
		}
		#endregion

		#region 선물할 선물권 액면가 반환
		public int SelectGBankGiftTokenGetPrice(int giftTokenSeq, string custNo)
		{
			return MicroDacHelper.SelectScalar<int>(
					"tiger_read",
					"dbo.UP_GMKTNet_GBankGift_TokenGetPrice",
					MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", giftTokenSeq, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@CUST_NO", custNo, SqlDbType.VarChar, 10)
					);
		}
		#endregion

		#region 선물권 선물하기 - ID
		public int InsertTokenSendDetailId(int giftTokenSeq, string rcvCustNo, string rcvLoginId, string rcvNm, string senderMemo, string sendCustNo)
		{
			return MicroDacHelper.SelectScalar<int>(
					"tiger_write",
					"dbo.UP_GMKTNet_GBankGift_InsertTokenSendDetailId",
					MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", giftTokenSeq, SqlDbType.Int),
					 MicroDacHelper.CreateParameter("@RCV_CUST_NO", rcvCustNo, SqlDbType.VarChar, 10),
					 MicroDacHelper.CreateParameter("@RCV_LOGIN_ID", rcvLoginId, SqlDbType.VarChar, 10),
					 MicroDacHelper.CreateParameter("@RCV_NM", rcvNm, SqlDbType.VarChar, 20),
					 MicroDacHelper.CreateParameter("@SENDER_MEMO", senderMemo, SqlDbType.VarChar, 600),
					 MicroDacHelper.CreateParameter("@SEND_CUST_NO", sendCustNo, SqlDbType.VarChar, 10)
			);
		}
		#endregion

		#region 자동 발송되는 쪽지
		public int InsertFrontSendMsgAuto(string rcvCustNo, string title, string content)
		{
			return MicroDacHelper.SelectScalar<int>(
					"tiger_write",
					"dbo.UP_GMKTNet_GBankGift_InsertFrontSendMsgAuto",
					MicroDacHelper.CreateParameter("@RCV_CUST_NO", rcvCustNo, SqlDbType.VarChar, 10),
					 MicroDacHelper.CreateParameter("@TITLE", title, SqlDbType.VarChar, 45),
					 MicroDacHelper.CreateParameter("@CONTENT", content, SqlDbType.VarChar, 8000),
					 MicroDacHelper.CreateParameter("@MSG_TYPE_NO", "2", SqlDbType.Int)
			);
		}
		#endregion

		#region 선물권 EMAIL로 선물하기
		public int InsertTokenSendDetailEmail(int tokenSeq, string rcvHpNo, string rcvEmail, string rcvNm
				, string senderMemo, string sendCustNo, string auth16Code, string auth7Code)
		{
			return MicroDacHelper.SelectScalar<int>(
					"tiger_write",
					"dbo.UP_GMKTNet_GBankGift_InsertTokenSendDetailEmail",
					MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", tokenSeq, SqlDbType.Int),
					 MicroDacHelper.CreateParameter("@RCV_HP_NO", rcvHpNo, SqlDbType.VarChar, 13),
					 MicroDacHelper.CreateParameter("@RCV_EMAIL", rcvEmail, SqlDbType.VarChar, 100),
					 MicroDacHelper.CreateParameter("@RCV_NM", rcvNm, SqlDbType.VarChar, 20),
					 MicroDacHelper.CreateParameter("@SENDER_MEMO", senderMemo, SqlDbType.VarChar, 600),
					 MicroDacHelper.CreateParameter("@SEND_CUST_NO", sendCustNo, SqlDbType.VarChar, 10),
					 MicroDacHelper.CreateParameter("@AUTH_16CODE", auth16Code, SqlDbType.VarChar, 150),
					 MicroDacHelper.CreateParameter("@AUTH_7CODE", auth7Code, SqlDbType.VarChar, 100)
			);
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
		public List<GiftCardSendListT> SelectGBankGiftCardMemberSendList(string CustNo, DateTime StartDate, DateTime EndDate)
		{
			//if (tab == "Send")
			//{
				return MicroDacHelper.SelectMultipleEntities<GiftCardSendListT>(
						"tiger_read",
						"dbo.UP_GMKT_GIFT_TOKEN_SEND_LIST_MOBILE",
						//MicroDacHelper.CreateParameter("@PAGE", PageNo, SqlDbType.Int),
						//MicroDacHelper.CreateParameter("@PAGESIZE", PageSize, SqlDbType.Int),
						MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
						MicroDacHelper.CreateParameter("@START_DT", StartDate, SqlDbType.DateTime),
						MicroDacHelper.CreateParameter("@END_DT", EndDate, SqlDbType.DateTime)
				);
			//}
			//else
			//{
			//  return MicroDacHelper.SelectMultipleEntities<GiftCardListT>(
			//      "tiger_read",
			//       "dbo.UP_GMKT_GIFT_TOKEN_RCV_LIST",
			//      MicroDacHelper.CreateParameter("@PAGE", PageNo, SqlDbType.Int),
			//      MicroDacHelper.CreateParameter("@PAGESIZE", PageSize, SqlDbType.Int),
			//      MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
			//      MicroDacHelper.CreateParameter("@START_DT", StartDate, SqlDbType.DateTime),
			//      MicroDacHelper.CreateParameter("@END_DT", EndDate, SqlDbType.DateTime)
			//  );
			//}
		}
		public List<GiftCardListT> GetGiftCardMemberRsvList(int PageNo, int PageSize, string CustNo, DateTime StartDate, DateTime EndDate)
		{
			return MicroDacHelper.SelectMultipleEntities<GiftCardListT>(
					"tiger_read",
					 "dbo.UP_GMKT_GIFT_TOKEN_RCV_LIST",
					MicroDacHelper.CreateParameter("@PAGE", PageNo, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@PAGESIZE", PageSize, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@CUST_NO", CustNo, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@START_DT", StartDate, SqlDbType.DateTime),
					MicroDacHelper.CreateParameter("@END_DT", EndDate, SqlDbType.DateTime)
			);
		}

		/// <summary>
		/// 선물권 - 재발송 COUNT 구하기
		/// </summary>
		/// <param name="giftTokenSeq"></param>
		/// <returns></returns>
		public int SelectMobieRetransCnt(int giftTokenSeq)
		{

			return MicroDacHelper.SelectScalar<int>(
					"tiger_read",
					"dbo.UP_GMKT_GIFT_TOKEN_SEND_COUNT",
					MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", giftTokenSeq, SqlDbType.Int)
			);
		}

		/// <summary>
		/// LMS 발송 처리
		/// </summary>
		/// <param name="tranPhone"></param>
		/// <param name="tranCallback"></param>
		/// <param name="tranDate"></param>
		/// <param name="tranTitle"></param>
		/// <param name="tranMsg"></param>
		/// <param name="mmsImgPath"></param>
		/// <param name="tranStatus"></param>
		/// <param name="regId"></param>
		/// <returns></returns>
		public string InsertGiftTokenSendLMS(string tranPhone, string tranCallback, DateTime tranDate, string tranTitle, string tranMsg, int tranStatus, string regId)
		{
			return MicroDacHelper.SelectScalar<string>(
					"ktsms_write",
					//"dbo.up_gmkt_event_lms_send_input",
                    "dbo.up_gmkt_non_event_lms_send_input",
					MicroDacHelper.CreateParameter("@tran_phone", tranPhone, SqlDbType.VarChar, 15),
				MicroDacHelper.CreateParameter("@tran_callback", tranCallback, SqlDbType.VarChar, 15),
				MicroDacHelper.CreateParameter("@tran_date", tranDate, SqlDbType.DateTime),
				MicroDacHelper.CreateParameter("@tran_title", tranTitle, SqlDbType.VarChar, 60),
				MicroDacHelper.CreateParameter("@tran_msg", tranMsg, SqlDbType.VarChar, 2000),
				MicroDacHelper.CreateParameter("@tran_status", tranStatus, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@reg_id", regId, SqlDbType.VarChar, 10)
			);
		}

		public string SetGiftTokenCancelSendMobile(int giftTokenSeq, string telNo)
		{
			return MicroDacHelper.SelectScalar<string>(
					"tiger_write",
					"dbo.UP_GMKT_GIFT_TOKEN_CANCEL_RESEND",
					MicroDacHelper.CreateParameter("@TOKEN_SEQ", giftTokenSeq, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@TEL_NO", telNo, SqlDbType.VarChar, 20)
			);
		}

		/// <summary>
		/// LMS 재발송을 위한 내용 조회
		/// </summary>
		/// <param name="giftTokenSeq"></param>
		/// <returns></returns>				
		public LmsGiftTokenT SelectGiftCardLMS(int giftTokenSeq)
		{
			return MicroDacHelper.SelectSingleEntity<LmsGiftTokenT>(
				"tiger_read",
				"dbo.UP_GMKT_GIFT_TOKEN_LMS_CONTENTS",
				MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", giftTokenSeq, SqlDbType.Int)
			);
		}

		/// <summary>
		/// 발송카운트 update 처리
		/// </summary>
		/// <returns></returns>
		public string SetGiftTokenSendCount(int giftTokenSeq)
		{
			return MicroDacHelper.SelectScalar<string>(
					"tiger_write",
					"dbo.UP_GMKT_GIFT_TOKEN_UPDATE_SEND_COUNT",
					MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", giftTokenSeq, SqlDbType.Int)
			);
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
			if (TYPE == "SEND")
			{
				return MicroDacHelper.SelectSingleEntity<GiftCardDetailListT>(
						"tiger_read",
						"dbo.UP_GMKT_GIFT_TOKEN_SEND_DETAIL",
						MicroDacHelper.CreateParameter("@TOKEN_SEQ", TOKENSEQ, SqlDbType.Int)
				);
			}
			else
			{
				return MicroDacHelper.SelectSingleEntity<GiftCardDetailListT>(
						"tiger_read",
						"dbo.UP_GMKT_GIFT_TOKEN_RECEIVE_DETAIL",
						MicroDacHelper.CreateParameter("@TOKEN_SEQ", TOKENSEQ, SqlDbType.Int)
				);
			}
		}
		#endregion 확인/재발송 디테일 조회

		#region 카카오 발송데이터 조회
		public GiftCardKakaoDataT GetKakaoSendData(string TOKENSEQ)
		{
			return MicroDacHelper.SelectSingleEntity<GiftCardKakaoDataT>(
						"tiger_write",
						"dbo.UP_GMKT_GIFT_TOKEN_KAKAO_AUTH_CODE",
						MicroDacHelper.CreateParameter("@TOKEN_SEQ", TOKENSEQ, SqlDbType.Int)
			);			
		}
		#endregion 카카오 발송데이터 조회

		#region 선물권 주문 마스터 데이터 입력
		public int SetGiftTokenOrderMst(DateTime now, string cust_no, string transway, int buyqty, string designtype, string sendmsgtype, string sendmsg, string senderinfocp, string senderinfona, string rsvtransyn, DateTime rsvdt, string bulkyn)
		{
			return MicroDacHelper.SelectScalar<int>(
					"stardb_write",
					"dbo.UP_GMKT_GIFT_TOKEN_ORDER_MASTER_INSERT",
					MicroDacHelper.CreateParameter("@ORDER_DT", now, SqlDbType.DateTime),
					MicroDacHelper.CreateParameter("@CUST_NO", cust_no, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@TRANS_WAY", transway, SqlDbType.Char, 1),
					MicroDacHelper.CreateParameter("@BUY_AMT", buyqty, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@DESIGN_TYPE", designtype, SqlDbType.Char, 2),
					MicroDacHelper.CreateParameter("@SEND_MSG_TYPE", sendmsgtype, SqlDbType.Char, 1),
					MicroDacHelper.CreateParameter("@SEND_MSG", sendmsg, SqlDbType.NVarChar, 100),
					MicroDacHelper.CreateParameter("@SENDER_INFO_CP", senderinfocp, SqlDbType.VarChar, 20),
					MicroDacHelper.CreateParameter("@SENDER_INFO_NA", senderinfona, SqlDbType.VarChar, 50),
					MicroDacHelper.CreateParameter("@RSV_TRANS_YN", rsvtransyn, SqlDbType.Char, 1),
					MicroDacHelper.CreateParameter("@RSV_DT", rsvdt, SqlDbType.SmallDateTime),
					MicroDacHelper.CreateParameter("@BULK_YN", bulkyn, SqlDbType.Char, 1)
			);
		}
		#endregion 선물권 주문 마스터 데이터 입력

		#region 선물권 주문 마스터 디테일 데이터 입력
		/// <summary>
		/// 확인/재발송 > 보낸 Gift Card 리스트 조회
		/// </summary>
		/// <param name="PageNo">페이지번호</param>
		/// <param name="PageSize">페이지사이즈</param>
		/// <param name="CustNo">고객번호</param>
		/// <param name="StartDate">조회 시작일</param>
		/// <param name="EndDate">조회 종료일</param>
		/// <returns></returns>
		public void SetGiftTokenOrderDtl(int masterseq, string RCVER_INFO_ID, string RCVER_INFO_CP, string RCVER_INFO_NA, string gd_no, int orderprice, int buyqty, int ACNT_MONEY, string SUCC_YN)
		{
			MicroDacHelper.ExecuteNonQuery(
					"stardb_write",
					"dbo.UP_GMKT_GIFT_TOKEN_ORDER_DETAIL_INSERT",
					MicroDacHelper.CreateParameter("@TOKEN_MASTER_SEQ", masterseq, SqlDbType.BigInt),
					MicroDacHelper.CreateParameter("@RCVER_INFO_ID", RCVER_INFO_ID, SqlDbType.VarChar, 30),
					MicroDacHelper.CreateParameter("@RCVER_INFO_CP", RCVER_INFO_CP, SqlDbType.VarChar, 20),
					MicroDacHelper.CreateParameter("@RCVER_INFO_NA", RCVER_INFO_NA, SqlDbType.NVarChar, 50),
					MicroDacHelper.CreateParameter("@GD_NO", gd_no, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@PRICE", orderprice, SqlDbType.Money),
					MicroDacHelper.CreateParameter("@AMT", buyqty, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@ACNT_MONEY", ACNT_MONEY, SqlDbType.Money),
					MicroDacHelper.CreateParameter("@SUCC_YN", SUCC_YN, SqlDbType.Char, 1)
			);
		}
		#endregion 선물권 주문 마스터 디테일 데이터 입력

		#region 선물권 아이디찾기
		/// <summary>
		/// 확인/재발송 > 보낸 Gift Card 리스트 조회
		/// </summary>
		/// <param name="PageNo">페이지번호</param>
		/// <param name="PageSize">페이지사이즈</param>
		/// <param name="CustNo">고객번호</param>
		/// <param name="StartDate">조회 시작일</param>
		/// <param name="EndDate">조회 종료일</param>
		/// <returns></returns>
		public GiftSearchIDT GetSearchID(string Cust_No, string ID, string NAME, DateTime now, DateTime startDate)
		{
			return MicroDacHelper.SelectSingleEntity<GiftSearchIDT>(
					"tiger_write",
					"dbo.up_gmkt_front_search_login_id_common",
					MicroDacHelper.CreateParameter("@cust_nm", NAME, SqlDbType.VarChar, 50),
					MicroDacHelper.CreateParameter("@login_id", ID, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@searcher_cust_no", Cust_No, SqlDbType.VarChar, 10),
					MicroDacHelper.CreateParameter("@page_no", 1, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@page_size", 5, SqlDbType.Int),
					MicroDacHelper.CreateParameter("@today", now, SqlDbType.DateTime),
					MicroDacHelper.CreateParameter("@start_month_day", startDate, SqlDbType.DateTime),
					MicroDacHelper.CreateParameter("@end_month_day", now, SqlDbType.DateTime)
			);
		}
		#endregion 선물권 아이디찾기

		#region 선물권 인증확인
		/// <summary>
		/// 선물권 인증확인
		/// </summary>
		/// <param name="customerNo">인증 시도 고객번호</param>
		/// <param name="encOnLineAuth16code">온라인 선물권용 암호화된 인증번호</param>
		/// <param name="encOffLineAuth16code">오프라인 선물권용 암호화된 인증번호</param>
		/// <returns></returns>
		public GiftCardAuthCheckResultT SelectGiftCardAuthCheck(string customerNo, string encOnLineAuth16code, string encOffLineAuth16code)
		{
			return MicroDacHelper.SelectSingleEntity<GiftCardAuthCheckResultT>(
				"stardb_write",
				"dbo.UP_GMKT_GIFT_TOKEN_GET_BYAUTHCODE",
				MicroDacHelper.CreateParameter("@ONLINE_AUTH_16CODE", encOnLineAuth16code, SqlDbType.VarChar, 150),
				MicroDacHelper.CreateParameter("@OFFLINE_AUTH_16CODE", encOffLineAuth16code, SqlDbType.VarChar, 32),
				MicroDacHelper.CreateParameter("@CUSTNO", customerNo, SqlDbType.VarChar, 10)
			);
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
		public GiftCardOnlineAuthT InsertGiftCardOnlineAuth(int giftTokenSeq, string customerNo, string encOnLineAuth16code, string encAUth7Code)
		{
			return MicroDacHelper.SelectSingleEntity<GiftCardOnlineAuthT>(
				"tiger_write",
				"dbo.UP_NEO_GET_TOKEN_EMAIL_AUTH_CODE7",
				MicroDacHelper.CreateParameter("@GIFT_TOKEN_SEQ", giftTokenSeq, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@AUTH_CODE16", encOnLineAuth16code, SqlDbType.VarChar, 150),
				MicroDacHelper.CreateParameter("@AUTH_CODE7", encAUth7Code, SqlDbType.VarChar, 100),
				MicroDacHelper.CreateParameter("@CUST_NO", customerNo, SqlDbType.VarChar, 10)
			);
		}
		#endregion 온라인 선물권 인증확인

		#endregion [======= 2014 선물권리뉴얼 =======]


	}
}
