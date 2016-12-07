using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data.Member
{
    public class PolicyEntityT
    {
        [Column("KEY_SEQ")]
        public long KeySeq { get; set; }

        [Column("CONTENT")]
        public string Content { get; set; }

        [Column("START_DT")]
        public string StartDate { get; set; }   
    }
}
