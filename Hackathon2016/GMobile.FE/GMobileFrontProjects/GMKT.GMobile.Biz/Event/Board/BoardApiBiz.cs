using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    public class BoardApiBiz
    {
        internal BoardEntityT GetBy(int id)
        {
            return new BoardApiDac().FindBy(id);
        }
    }
}
