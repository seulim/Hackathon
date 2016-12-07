//Entity Class for PetaPoco
using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

using PetaPoco;

namespace GMKT.GMobile.Biz
{
	#region CommonResultT
	public partial class CommonResultT
	{
		[Column("result_code")]
		public string result_code { get; set; }

		[Column("result_message")]
		public string result_message { get; set; }

		[Column("user_key")]
		public string user_key { get; set; }

		[Column("result_text")]
		public string result_text { get; set; }
	}
	#endregion

	#region KakaoTalkReceiverStatusT
	public partial class KakaoTalkReceiverStatusT
	{
		[Column("katalk_receiver_yn")]
		public string katalk_receiver_yn { get; set; }

		[Column("user_key")]
		public string user_key { get; set; }

		[Column("reg_channel_cd")]
		public string reg_channel_cd { get; set; }
	}
	#endregion


    #region CustomHPNoT
    public partial class CustomHPNoT
    {
        [Column("hp_no")]
        public string hp_no { get; set; }
    }
    #endregion
}
