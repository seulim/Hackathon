using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class EventStickerT
	{
		[Column("app_minus_cnt")]
		public int Count { get; set; }
	}
}
