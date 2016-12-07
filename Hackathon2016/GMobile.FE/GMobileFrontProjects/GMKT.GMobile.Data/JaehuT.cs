using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
    public partial class JaehuT
    {
        [Column("co_nm")] //컨텐츠 이름
        public string CoNm { get; set; }
        [Column("contract_cd")] //제휴코드
        public string ContractCd { get; set; }
    }
}
