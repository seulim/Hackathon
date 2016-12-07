using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx.EnterpriseServices;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class CustomerCenterBiz : BizBase
	{
		/// <summary>
		/// FAQ 카테고리 조회
		/// </summary>
		/// <param name="faqlclasscd">대분류코드( 대분류 조회시는 빈값)</param>
		[Transaction(TransactionOption.NotSupported)]
		public List<FaqCategoryT> GetFaqCategoryListFromDB(string faqClassType, string faqClassCd )
		{
			return new CustomerCenterDac().SelectFaqCategory(faqClassType, faqClassCd);
		}

		/// <summary>
		/// Best FAQ 리스트 조회
		/// </summary>
		[Transaction(TransactionOption.NotSupported)]
		public List<FaqListT> GetBestFaqListFromDB()
		{
			return new CustomerCenterDac().SelectBestFaqList();
		}

		/// <summary>
		/// FAQ 리스트 조회
		/// </summary>
		/// <param name="keyword">검색키워드(카테고리별일경우 빈값)</param>
		/// <param name="faqlclasscd">대분류 코드(검색어별일 경우 빈값)</param>
		/// <param name="pageno">페이지번호</param>
		/// <param name="pagesize">페이지 사이즈</param>
		/// <returns></returns>
		[Transaction(TransactionOption.NotSupported)]
		public List<FaqListT> GetFaqListFromDB(string keyword, string faqlclasscd, int pageno, int pagesize)
		{
			return new CustomerCenterDac().SelectCustomerCenter(keyword, faqlclasscd, pageno, pagesize);
		}

		/// <summary>
		/// FAQ 상세 조회
		/// </summary>
		/// <param name="seqNo">순번</param>
		/// <returns></returns>
		[Transaction(TransactionOption.NotSupported)]
		public FaqDetailT GetFaqDetailFromDB(int seqNo)
		{
			return new CustomerCenterDac().SelectFaqDetail(seqNo);
		}

		/// <summary>
		/// FAQ 상세 조회수 처리
		/// </summary>
		/// <param name="seqNo">순번</param>
		/// <returns></returns>
		[Transaction(TransactionOption.NotSupported)]
		public FaqDetailCntT SetFaqDetailReadCntFromDB(int seqNo)
		{
			return new CustomerCenterDac().AddFaqDetailReadCnt(seqNo);
		}

		/// <summary>
		/// 연관FAQ 리스트
		/// </summary>
		/// <param name="seqNo">순번</param>
		/// <returns></returns>
		[Transaction(TransactionOption.NotSupported)]
		public List<FaqDetailT> GetRelFaqDetailFromDB(int seqNo)
		{
			return new CustomerCenterDac().SelectRelationFAQList(seqNo);
		}
	}
}
