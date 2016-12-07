using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.Search
{
	public class SmartFinderKeywordInfoByKeywordResponse
	{
		public string Keyword { get; set; }
		public string Gubun { get; set; }
		public string BuyGdlcCd { get; set; }
		public string ClkGdmcCd { get; set; }

		public SmartFinderInfo SmartFinderKeywordInfo { get; set; }
	}

	public class SmartFinderKeywordInfoByCategoryResponse
	{
		public string CategoryCd { get; set; }
		public string GdlcCd { get; set; }
		public string GdlcNm { get; set; }
		public string GdmcCd { get; set; }
		public string GdmcNm { get; set; }
		public string GdscCd { get; set; }
		public string GdscNm { get; set; }
		public string MFrontType { get; set; }
		public string SFrontType { get; set; }
		public string MMobileType { get; set; }

		public SmartFinderInfo SmartFinderCategoryInfo { get; set; }
	}

	public class SmartFinderInfo
	{
		public string Keyword { get; set; }

		public string ClassCd { get; set; }	// TODO: 그냥 상속받고 분리할까...
		public string ClassType { get; set; }

		public string SmartFinderYn { get; set; }
		public string DirectMatchingYn { get; set; }
		public string ExposeType { get; set; }
		public string LClassSeq { get; set; }
		public string LClassNm { get; set; }
		public string MClassExposeNm { get; set; }
		public string SClassExposeNm { get; set; }
		public string PcExposeYn { get; set; }
		public string MobileExposeYn { get; set; }
		public string MobileImageUrl { get; set; }
	}

	public class SmartFinderMClass
	{
		public string MClassSeq { get; set; }
		public string MClassNm { get; set; }
		public string MExposePriority { get; set; }
		public string MClassExposeNm { get; set; }
	}

	public class SmartFinderSClass
	{
		public string SClassSeq { get; set; }
		public string SClassNm { get; set; }
		public string SExposePriority { get; set; }
		public string SClassExposeNm { get; set; }
	}

	public class SmartFinderValue
	{
		public string ValueID { get; set; }
		public string TypeValue { get; set; }
		public string VExposePriority { get; set; }
		public string ImageUrl { get; set; }
	}
}
