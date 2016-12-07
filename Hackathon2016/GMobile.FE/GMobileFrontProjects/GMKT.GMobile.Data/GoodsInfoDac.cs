using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;
using GMKT.Framework.Data;

namespace GMKT.GMobile.Data
{
	public class GoodsInfoDac : MicroDacBase
	{
		/// <summary>
		/// 판매자 정보 조회
		/// </summary>
		/// <param name="rows">상품번호</param>
		/// <returns></returns>
		public GoodsInfoT SelectGoodsSellerInfo(string goodsNo)
		{
			return MicroDacHelper.SelectSingleEntity<GoodsInfoT>(
				"tiger_read"
				, "dbo.up_gmkt_mobile_front_get_goods_seller_info"
				, MicroDacHelper.CreateParameter("@goodscode", goodsNo, SqlDbType.VarChar, 10)
			);
		}


		/// <summary>
		/// 티켓 상품 기본 정보 조회
		/// </summary>
		/// <param name="rows">공연코드번호</param>
		/// <returns></returns>
		public GoodsTicketInfoT SelectGoodsTicketInfo(string goodsCd, string goodsNo)
		{
			return MicroDacHelper.SelectSingleEntity<GoodsTicketInfoT>(
				"ticket_read"
				, "dbo.up_get_baseball_ticket_info"
				, MicroDacHelper.CreateParameter("@gd_cd", goodsCd, SqlDbType.VarChar, 12)
				, MicroDacHelper.CreateParameter("@gd_no", goodsNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 쿠폰 정보 조회
		/// </summary>
		/// <param name="rows">상품번호</param>
		/// <returns></returns>
		public GoodsCouponInfoT SelectGoodsCouponInfo(string goodsNo)
		{
			return MicroDacHelper.SelectSingleEntity<GoodsCouponInfoT>(
				"tiger_read"
				, "dbo.Up_New_GDBack_B_Goods_GetCouponMarketGoodsInfo"
				, MicroDacHelper.CreateParameter("@goodscode", goodsNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 상품 상세 정보 조회
		/// </summary>
		/// <param name="rows">상품번호</param>
		/// <returns></returns>
		public GoodsDetailInfoT SelectGoodsDetailInfo(string goodsNo)
		{
			return MicroDacHelper.SelectSingleEntity<GoodsDetailInfoT>(
				"item_read"
				, "dbo.up_gmkt_front_get_ticket_goods_info_detail_div1_item"
				, MicroDacHelper.CreateParameter("@goodscode", goodsNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 상품 기본 정보 조회
		/// </summary>
		/// <param name="rows">상품번호</param>
		/// <returns></returns>
		public GoodsBasicInfoT SelectGoodsBasicInfo(string goodsNo)
		{
			return MicroDacHelper.SelectSingleEntity<GoodsBasicInfoT>(
				"tiger_read"
				, "dbo.up_gmkt_mobile_front_get_goods_detail_info"
				, MicroDacHelper.CreateParameter("@goodscode", goodsNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 좌석 등급 조회
		/// </summary>
		/// <param name="rows">공연코드번호</param>
		/// <returns></returns>
		public List<TicketSeatClassT> SelectSeatClassInfo(string goodsCd)
		{
			// backend 실행 권한
			return MicroDacHelper.SelectMultipleEntities<TicketSeatClassT>(
				"ticket_read"
				, "dbo.up_get_goods_seatclass_info"
				, MicroDacHelper.CreateParameter("@gd_cd", goodsCd, SqlDbType.VarChar, 12)
			);
		}
	}
}
