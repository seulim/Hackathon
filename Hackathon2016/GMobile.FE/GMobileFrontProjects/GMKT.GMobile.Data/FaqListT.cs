using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public class FaqListT
	{
		[Column("seqno")]
		public Int32 seqNo { get; set; }

		[Column("title")]
		public string title { get; set; }

		[Column("contents")]
		public string contents { get; set; }

		[Column("viewcnt")]
		public Int32 viewCnt { get; set; }

		[Column("faq_lclass_group_cd")]
		public string faqLclassGroupCd { get; set; }

		[Column("faq_lclass_cd")]
		public string faqLclassCd { get; set; }

		[Column("faq_lclass_Nm")]
		public string faqLclassNm { get; set; }

		[Column("faq_mclass_cd")]
		public string faqMclassCd { get; set; }

		[Column("faq_sclass_cd")]
		public string faqSclassCd { get; set; }

		[Column("rnum")]
		public string rnum { get; set; }

		[Column("dummy_num")]
		public string dummyNum { get; set; }

		[Column("total_cnt")]
		public Int32 totalCnt { get; set; }

		[Column("page_cnt")]
		public Int32 pageCnt { get; set; }
	}

	public class FaqCategoryT
	{
		[Column("cd")]
		public string cd { get; set; }

		[Column("cd_nm")]
		public string cdNm { get; set; }

		[Column("etc1_cd")]
		public string etcCd { get; set; }

		[Column("order_goods_yn")]
		public string orderGoodsYn { get; set; }

		[Column("file_upload_yn")]
		public string fileUploadYn { get; set; }
	}

	public class FaqDetailT
	{
		[Column("seqno")]
		public Int32 seqNo { get; set; }

		[Column("contents")]
		public string contents { get; set; }

		[Column("title")]
		public string title { get; set; }
	}


	public class FaqDetailCntT
	{
		[Column("ret_code")]
		public Int32 retCode { get; set; }
	}
}