using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.Member;
using GMKT.Framework.EnterpriseServices;
using ArcheFx.EnterpriseServices;

namespace GMKT.GMobile.Biz.Member
{
    public class PolicyBiz : BizBase
    {
        [Transaction(TransactionOption.NotSupported)]
        public string GetMemberPolicy(long termsCode)
        {   
            PolicyEntityT policy = new PolicyDac().SelectMemberPolicyCache(termsCode);
            return policy.Content;
        }

        public string EncodedHtml(string htmlString)
        {
            string newString = htmlString.Replace("\n", "");
            newString = newString.Replace("\t", "");
            newString = newString.Replace("\"", "'");
            return newString;
        }
    }
}
