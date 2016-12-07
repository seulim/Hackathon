using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	[TableName("PLUSZONE_ATTANCE")]
	public partial class PluszoneAttendanceT
	{
		[Column("cust_no")]
		public string CustNo { get; set; }

		[Column("attance_date")]
		public DateTime AttendanceDate { get; set; }

		[Column("login_id")]
		public string LoginID { get; set; }
	}

    [TableName("PLUSZONE_ATTENDANCE")]
    public partial class PluszoneNewAttendanceT
    {
        [Column("REG_DT")]
        public DateTime AttendanceDate { get; set; }
    }
}
