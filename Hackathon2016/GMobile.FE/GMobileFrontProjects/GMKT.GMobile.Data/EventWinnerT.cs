using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class EventWinnerT
	{
		[Column("winner_cnt")]
		public Int32 WinnerCount { get; set; }

		[Column("win_cnt")]
		public Int32 WinCount { get; set; }
	}
}
