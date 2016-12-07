using System.Collections.Generic;
using System;

namespace GMKT.GMobile.Data
{
    public class BoardEntityT
    {
        public int EventID { get; set; }

        public int PlanID { get; set; }

        public string PlanTitle { get; set; }

        public string BoardType { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime EndedDate { get; set; }

        public bool IsEnableRegist
        {
            get
            {
                return StartedDate <= DateTime.Now && EndedDate >= DateTime.Now;
            }
        }

        public int CountPerPage { get; set; }

        public bool IsEnabledReply { get; set; }

        public bool IsEnabledDelete { get; set; }

        public string DuplicationRegistrationType { get; set; }

        public int DuplicationRegistrationCountPerDay { get; set; }

        public List<string> SnsKinds { get; set; }

        public string FacebookThumbnailImage { get; set; }

        public bool IsPreventEnglish { get; set; }

        public bool IsPreventRepeatedWord { get; set; }

        public bool IsPreventCopy { get; set; }

        public int TextMinLength { get; set; }

        public int TextMaxLength { get; set; }

        public string LanguageType { get; set; }

        public string TopLineColor { get; set; }

        public string BottomLineColor { get; set; }

        public string ButtonColor { get; set; }

        public string ButtonTextColor { get; set; }
    }
}
