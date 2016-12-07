using System.Collections.Generic;

namespace GMKT.GMobile.Data
{
    public class DataAndSize<T>
    {
        public long PageTotalCount { get; set; }

        public T Data { get; set; }
    }
}
