using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class GStampIssueT
	{
		[Column("tot_issue_cnt")]
		public Int32 TotalIssue { get; set;}

		[Column("used_issue_cnt")]
		public Int32 UsedIssue { get; set;}

		[Column("possible_issue_cnt")]
		public Int32 PossibleIssue { get; set;}

		[Column("wait_issue_cnt")]
		public Int32 WaitIssue { get; set; }
	}
}
