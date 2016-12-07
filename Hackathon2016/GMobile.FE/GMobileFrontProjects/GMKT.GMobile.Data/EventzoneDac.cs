using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;
using GMKT.Framework.Data;

namespace GMKT.GMobile.Data
{
	public class EventzoneDac : MicroDacBase
	{
        public List<PluszoneNewAttendanceT> SelectPluszoneAttendance(string custNo, DateTime startDate, DateTime endDate)
		{
            return MicroDacHelper.SelectMultipleEntities<PluszoneNewAttendanceT>(
				"proevent_read",
                "dbo.UP_GMKT_FRONT_GET_PLUSZONE_ATTENDANCE",
                MicroDacHelper.CreateParameter("@CUST_NO", custNo, SqlDbType.VarChar, 10),
                MicroDacHelper.CreateParameter("@ATTENDANCE_SDATE", startDate, SqlDbType.DateTime),
                MicroDacHelper.CreateParameter("@ATTENDANCE_EDATE", endDate, SqlDbType.DateTime)
			);   
		}

		public OptionApplyT GetCountTodayEid(string custNo, int eid, DateTime startDate, DateTime endDate)
		{
			return MicroDacHelper.SelectSingleEntity<OptionApplyT>(
				"gevent_read",
				"dbo.up_gmkt_event_optionapply_getcnttoday_eid",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@eid", eid, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@start_date", startDate, SqlDbType.DateTime),
				MicroDacHelper.CreateParameter("@end_date", endDate, SqlDbType.DateTime)
			);
		}

		public int? SetOptionApply(int geid, int eid, string custNo, string jaehuCustNo, int optionNo, DateTime reg_dt, string optionInfo)
		{
			return MicroDacHelper.SelectScalar<int>(
				"gevent_write",
				"dbo.up_gmkt_event_optionapply_set",
				MicroDacHelper.CreateParameter("@geid", geid, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@eid", eid, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@jaehu_cust_no", jaehuCustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@option_no", optionNo, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@reg_dt", reg_dt, SqlDbType.SmallDateTime),
				MicroDacHelper.CreateParameter("@option_info", optionInfo, SqlDbType.VarChar, 50)
			);
		}

		public List<EventzoneBannerT> GetEventzoneBannerList(string bannerType1, string bannerType2, DateTime nowDate, int rowCount)
		{
			return MicroDacHelper.SelectMultipleEntities<EventzoneBannerT>(
				"gevent_read",
				"dbo.up_gmkt_front_get_eventzone_banner_list",
				MicroDacHelper.CreateParameter("@banner_type_1", bannerType1, SqlDbType.VarChar, 2),
				MicroDacHelper.CreateParameter("@banner_type_2", bannerType2, SqlDbType.VarChar, 6),
				MicroDacHelper.CreateParameter("@nowdate", nowDate, SqlDbType.SmallDateTime),
				MicroDacHelper.CreateParameter("@rowcount", rowCount, SqlDbType.Int)
			);
		}
        /*
		public int? InsertPluszoneAttance(string custNo, string loginID)
		{
			return MicroDacHelper.SelectScalar<int>(
				"gevent_write",
				"dbo.up_gmkt_front_pluszone_attance_insert",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@login_id", loginID, SqlDbType.VarChar, 10)
			);
		}*/

		public GStampIssueT GetStampIssueCount(string custNo)
		{
			return MicroDacHelper.SelectSingleEntity<GStampIssueT>
			(
				"tiger_read",
				"dbo.up_neo_get_stamp_issue_cnt",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10)
			);
		}

		public EventStickerT GetEventStickerMinusCount(int eid)
		{
			return MicroDacHelper.SelectSingleEntity<EventStickerT>
			(
				"neoevent_read",
				"dbo.up_gmkt_front_event_detail",
				MicroDacHelper.CreateParameter("@eid", eid, SqlDbType.Int)
			);
		}

		public EventWinnerT GetEventRemainCount(int eid, DateTime today)
		{
			return MicroDacHelper.SelectSingleEntity<EventWinnerT>
			(
				"neoevent_read",
				"dbo.up_gmkt_front_event_get_winner_count",
				MicroDacHelper.CreateParameter("@eid", eid, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@today", today.ToString("yyyy-MM-dd 00:00"), SqlDbType.SmallDateTime)
			);
		}

		public List<EventzoneGiftT> GetEventzoneGiftList(string gtype, DateTime nowDate, int rowCount)
		{
			return MicroDacHelper.SelectMultipleEntities<EventzoneGiftT>(
				"gevent_read",
				"dbo.up_gmkt_front_get_eventzone_gift_list",
				MicroDacHelper.CreateParameter("@gtype", gtype, SqlDbType.Char, 2),
				MicroDacHelper.CreateParameter("@nowdate", nowDate, SqlDbType.SmallDateTime),
				MicroDacHelper.CreateParameter("@rowcount", rowCount, SqlDbType.Int)
			);
		}
	}
}
