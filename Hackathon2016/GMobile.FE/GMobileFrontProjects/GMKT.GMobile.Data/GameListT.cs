using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
    [TableName("GameList")]
    public partial class BaseBallGameT
    {
        [Column("GD_CD")]
        public string GdCd { get; set; }
        [Column("GD_NM")]
        public string GdNm { get; set; }
        [Column("ETC2")]
        public string Etc2 { get; set; }
        [Column("RES_START_DT")]
        public string ResStartDt { get; set; }
        [Column("PLAY_DT")]
        public string PlayDt { get; set; }
        [Column("CH_NO")]
        public string ChNo { get; set; }
    }
}
