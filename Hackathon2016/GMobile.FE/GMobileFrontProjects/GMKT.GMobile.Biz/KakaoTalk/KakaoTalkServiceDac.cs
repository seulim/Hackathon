using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;

using GMKT.Framework.Data;
using GMKT.Framework.EnterpriseServices;

namespace GMKT.GMobile.Biz
{
	internal class KakaoTalkServiceDac : MicroDacBase
	{
		#region 맞춤정보 수신여부 및 user_key 조회
		public KakaoTalkReceiverStatusT SelectReceiverStatusInfo(string custNo)
		{
			return MicroDacHelper.SelectSingleEntity<KakaoTalkReceiverStatusT>(
				"stardb_read",
				"dbo.up_gmkt_get_kakaotalk_receiver_status_info",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10)
			);
		}
		#endregion

		#region 맞춤정보 수신 등록
		public string InsertReceiver(string custNo, string loginId, string userKey, string phoneNumber, string regChannelCd)
		{
			return MicroDacHelper.SelectScalar<string>(
			 "stardb_read",
			 "dbo.up_gmkt_set_kakaotalk_receiver_insert",
			 MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
			 MicroDacHelper.CreateParameter("@login_id", loginId, SqlDbType.VarChar, 10),
			 MicroDacHelper.CreateParameter("@user_key", userKey, SqlDbType.VarChar, 20),
			 MicroDacHelper.CreateParameter("@info_cp", phoneNumber, SqlDbType.VarChar,20),
			 MicroDacHelper.CreateParameter("@reg_channel_cd", regChannelCd, SqlDbType.Char, 1)
			);
		}
		#endregion

        public CustomHPNoT SelectUserHPNo(string custNo)
        {
            return MicroDacHelper.SelectSingleEntity<CustomHPNoT>(
                "tiger_read",
                "dbo.UP_NEO_GET_CUSTOM_HP_NO",
                MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10)
            );
        }
    }
}
