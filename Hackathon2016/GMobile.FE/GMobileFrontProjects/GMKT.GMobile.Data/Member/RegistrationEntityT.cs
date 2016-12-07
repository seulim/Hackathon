using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.Member
{
	public class ResultT
	{
		public RegistrationReturn ResultCode { get; set; }
		public string ResultReason { get; set; }
		public string ResultDescription { get; set; }
	}

	public class CrossResultT
	{
		public string ResultCode { get; set; }
		public string ResultDescription { get; set; }
	}
	
	public enum RegistrationReturn
	{
		NoError = 0,
		Exists = -1,
		ExistsId = -2,							//아이디가 중복
		ExistsOnwer = -3,						//가입자존재
		ExistsMinishopName = -4,		//미니샵명존재
		ExistsMinishopUrl = -5,			//미니샵주소 존재
		ExistsEmail = -6,						//이메일 존재

		InvalidIDLength = -10,				//validation 아이디 길이 
		InvalidIDCharacter = -11,			//validation 아이디 조합
		InvalidIDIncludeHpno = -12, //validation 아이디 휴대폰번호사용
		InvalidEmailAddress = -13,		//validation email
		InvalidCorpIdNo = -14,			//validation 사업자번호
		InvalidCorpRegNo = -15,			//validation 법인등록번호
		InvalidMinishopNameLength = -16,	//validation 미니샵명길이
		InvalidMinishopNameCharacter = -17,			//validation MinishopName 조합
		InvalidMinishopUrlLength = -18,					//validation Minishopurl 길이
		InvalidMinishopUrlCharacter = -19,			//validation Minishopurl 조합
		InvalidPasswdLength = -20,				//validation passwd 길이 
		InvalidPasswdCharacter = -21,			//validation passwd 조합
		InvalidGbankPasswdLength = -22,				//validation gbank passwd 길이 
		InvalidGbankPasswdCharacter = -23,			//validation gbank passwd 조합

		EqualGbankPasswod = -25,		//비번과 gbank비동 동일
		JoinInner1Week = -26,
		ReJoinLimitEternal = -27,
		ReJoinLimitSeller = -28,
		Under14 = -29,								//14세미만		

		CreateCustNoError = -30,		//custno 채번오류
		RegistrationError = -31,		//회원 등록시 오류		

		ExistsDealerCustNo = -41,		//판매자정보에 고객번호가 존재함
		TransNotExistsCustNo = -50,		//전환시 존재하지않는 고객번호
		TransNotPersonalBuyer = -51,	//EP전환시 대상 고객타입이 PP가 아닐경우
		TransNotCorpBuyer = -52,			//EC전환시 대상 고객타입이 PC가 아닐경우

		MinishopEorror = -70,				//미니샵관련 추가 오류				
		EtcError = -99,							//기타 오류

		G9Error = 10,							//G9 가입시 오류		
		SyncError = 20,						//CRMDB와 Sync중 오류
		SendJoinEmailError = 30,		//Email전송 오류	
		SendSMSError = 40					//SMS 발송 오류		
	}

    public partial class CorpAuthResultT
	{
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public bool   RegistrationStatusCode { get; set; }
        public string CorpName { get; set; }
        public string RepName { get; set; }
        public string Seq { get; set; }
	}
}
