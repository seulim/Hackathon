using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;
using ArcheFx.EnterpriseServices;

namespace GMKT.GMobile.Biz
{
	public class BaseBallBiz : BizBase
	{
		[Transaction(TransactionOption.NotSupported)]
		public GoodsInfoT GetGoodsSellerInfo(string goodsNo)
		{
			return new GoodsInfoDac().SelectGoodsSellerInfo(goodsNo);
		}

		[Transaction(TransactionOption.NotSupported)]
		public GoodsTicketInfoT GetGoodsTicketInfo(string goodsCd, string goodsNo)
		{
			return new GoodsInfoDac().SelectGoodsTicketInfo(goodsCd, goodsNo);
		}

		[Transaction(TransactionOption.NotSupported)]
		public GoodsCouponInfoT GetGoodsCouponInfo(string goodsNo)
		{
			return new GoodsInfoDac().SelectGoodsCouponInfo(goodsNo);
		}

		[Transaction(TransactionOption.NotSupported)]
		public GoodsDetailInfoT GetGoodsDetailInfo(string goodsNo)
		{
			return new GoodsInfoDac().SelectGoodsDetailInfo(goodsNo);
		}

		[Transaction(TransactionOption.NotSupported)]
		public List<TicketSeatClassT> GetSeatClassInfo(string goodsCd)
		{
			return new GoodsInfoDac().SelectSeatClassInfo(goodsCd);
		}

		[Transaction(TransactionOption.NotSupported)]
		public GoodsBasicInfoT GetGoodsBasicInfo(string goodsNo)
		{
			return new GoodsInfoDac().SelectGoodsBasicInfo(goodsNo);
		}
		
        public List<BaseBallGameT> GetBaseBallList( string playdt, string teamcd, int pagesize)
        {
            return new GameListDac().SelectBaseBallList(playdt, teamcd, pagesize);
        }
	}
}
