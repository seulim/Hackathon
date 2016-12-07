using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;

namespace GMKT.GMobile.Data.Member
{
    public class PolicyDac : MicroDacBase
    {
        public PolicyEntityT SelectMemberPolicyCache(long termsCode)
        {
            return MicroDacHelper.SelectSingleEntity<PolicyEntityT>(
                "display_read",
                "dbo.UP_GMKT_Admin_GetTermsPolicy",
                MicroDacHelper.CreateParameter("@TERMS_CD", termsCode, SqlDbType.BigInt)
            );
            
        }
    }
}
