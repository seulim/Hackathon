using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
    public enum BoardContentResultCode
    {
        Success = 0,
        GeneralFail,
        IsNotPeriodOfUseFail,
        AlreadyRegistedFail,
        ExcessedRegistrationCountPerDayFail,
        LoginFail,
    }
}
