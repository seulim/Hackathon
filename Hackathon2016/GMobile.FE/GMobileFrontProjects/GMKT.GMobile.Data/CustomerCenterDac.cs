using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;

namespace GMKT.GMobile.Data
{
	public class CustomerCenterDac : MicroDacBase
	{
		/// <summary>
		/// FAQ 카테고리 조회
		/// </summary>
		/// <param name="faqlclasscd">대분류코드( 대분류 조회시는 빈값)</param>
		public List<FaqCategoryT> SelectFaqCategory(string faqClassType, string faqClassCd )
		{
			return MicroDacHelper.SelectMultipleEntities<FaqCategoryT>(
				"tiger_read",
				"dbo.up_gmkt_mobile_get_customcenter_faq_class_list"
				,MicroDacHelper.CreateParameter("@faq_class_type", faqClassType, SqlDbType.Char, 1)
				,MicroDacHelper.CreateParameter("@faq_class_cd", faqClassCd, SqlDbType.VarChar)
			);
		}

		/// <summary>
		/// BEST FAQ 리스트 조회
		/// </summary>
		/// <returns></returns>
		public List<FaqListT> SelectBestFaqList()
		{
			return MicroDacHelper.SelectMultipleEntities<FaqListT>(
				"stardb_read",
				"dbo.up_gmkt_mobile_get_mobile_qabank_best_faq_list",
				MicroDacHelper.CreateParameter("@top_cnt", 5, SqlDbType.Int)
			);
		}

		/// <summary>
		/// FAQ 리스트 조회
		/// </summary>
		/// <param name="keyword">검색키워드(카테고리별일경우 빈값)</param>
		/// <param name="faqlclasscd">대분류 코드(검색어별일 경우 빈값)</param>
		/// <param name="pageno">페이지번호</param>
		/// <param name="pagesize">페이지 사이즈</param>
		/// <returns></returns>
		public List<FaqListT> SelectCustomerCenter(string keyword, string faqlclasscd, int pageno, int pagesize)
		{
			return MicroDacHelper.SelectMultipleEntities<FaqListT>(
				"tiger_read",
				"dbo.up_gmkt_mobile_get_faq_search_list",
				MicroDacHelper.CreateParameter("@keyword", keyword, SqlDbType.VarChar, 50),
				MicroDacHelper.CreateParameter("@faq_lclass_cd", faqlclasscd, SqlDbType.VarChar, 3),
				MicroDacHelper.CreateParameter("@page_no", pageno, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@page_size", pagesize, SqlDbType.Int)
			);
		}

		/// <summary>
		/// BEST, FAQ 상세 조회
		/// </summary>
		/// <param name="seqNo">순번</param>
		/// <returns></returns>
		public FaqDetailT SelectFaqDetail(int seqNo)
		{
			return MicroDacHelper.SelectSingleEntity<FaqDetailT>(
				"tiger_read",
				"dbo.up_gmkt_admin_get_mobile_qabank_search_detail",
				MicroDacHelper.CreateParameter("@seqno", seqNo, SqlDbType.Int)
			);
		}

		/// <summary>
		/// BEST, FAQ 상세 조회 조회수 처리
		/// </summary>
		/// <param name="seqNo">순번</param>
		/// <returns></returns>
		public FaqDetailCntT AddFaqDetailReadCnt(int seqNo)
		{
			return MicroDacHelper.SelectSingleEntity<FaqDetailCntT>(
				"tiger_write",
				"dbo.up_gmkt_mobile_set_qabank_read_cnt",
				MicroDacHelper.CreateParameter("@seqno", seqNo, SqlDbType.Int)
			);
		}
		
		/// <summary>
		/// 연관FAQ 리스트 조회
		/// </summary>
		/// <param name="faqseq">FAQ 번호</param>
		/// <returns></returns>
		public List<FaqDetailT> SelectRelationFAQList(int faqseq)
		{
			return MicroDacHelper.SelectMultipleEntities<FaqDetailT>(
				"tiger_read",
				"dbo.up_gmkt_mobile_get_rel_faq_list",
				MicroDacHelper.CreateParameter("@faq_seq", faqseq, SqlDbType.Int)
			);
		}
		
	}
}
