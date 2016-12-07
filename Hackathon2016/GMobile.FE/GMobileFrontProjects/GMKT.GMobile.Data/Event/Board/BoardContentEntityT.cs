using System;

namespace GMKT.GMobile.Data
{
    public class BoardContentEntityT
    {
        public int BoardID { get; set; }

        public int ID { get; set; }

        public int BoardContentID { get; set; }

        public DateTime RegistDateTime { get; set; }

        public string RegistDateTimeString
        {
            get
            {
                return RegistDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public int Depth { get; set; }

        public string Contents { get; set; }

        public string SnsKind { get; set; }

        public string SnsUserID { get; set; }

        public string SnsUserName { get; set; }

        public string SnsProfileImage { get; set; }

        public bool IsEnabledDelete { get; set; }

        public string UserInfo { get; set; }
    }
}
