using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class OptionApplyT
	{
		[Column("cnt")]
		public Int32 Count { get; set; }
	}
}
