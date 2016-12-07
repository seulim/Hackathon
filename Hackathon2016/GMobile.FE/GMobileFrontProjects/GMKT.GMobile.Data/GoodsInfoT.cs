using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class GoodsInfoT
	{
		// 판매자 번호
		[Column("seller_cust_no")]
		public string SellerNo { get; set; }

		// 미니샵 이름
		[Column("nickname")]
		public string MinishopName { get; set; }

		// 판매자 등급
		[Column("cust_gr")]
		public string SellerGrade { get; set; }

		// 평가
		[Column("seller_estimation")]
		public Nullable<Int32> SellerPoint { get; set; }

		// 상호
		[Column("ccust_cmpnm")]
		public string ShopName { get; set; }

		// 대표자명
		[Column("top_mgr")]
		public string Represent { get; set; }

		// 연락처
		[Column("helpdesk_tel_no")]
		public string TelNo { get; set; }

		// FAX 번호
		[Column("fax_no")]
		public string FaxNo { get; set; }

		// 이메일주소
		[Column("e_Mail")]
		public string Email { get; set; }

		// 사업자등록번호
		[Column("corp_no")]
		public string License { get; set; }

		// 통신사업자등록번호
		[Column("ecomerce_no")]
		public string RegCoNo { get; set; }

		// 영업소재지
		[Column("sellerAddress")]
		public string Location { get; set; }

		// 응대 시작 시간
		[Column("helpdesk_sdt")]
		public string HelpdeskStart { get; set; }

		// 응대 종료 시간
		[Column("helpdesk_edt")]
		public string HelpdeskEnd { get; set; }

	}

	// 티켓 상품 정보
	public partial class GoodsTicketInfoT
	{
		// 공연명
		[Column("gd_nm")]
		public string GoodsName { get; set; }

		// 공연장명
		[Column("pl_nm")]
		public string PlaceName { get; set; }

		// 공연일
		[Column("play_dt")]
		public string PlayDate { get; set; }

		// 공연시간
		[Column("play_tm")]
		public string PlayTime { get; set; }

		// 예매 시작 일시
		[Column("res_start_dt")]
		public string ReservationStartDate { get; set; }

		// 취소 가능 일시
		[Column("cancel_able_tm")]
		public string CancelAbleDate { get; set; }

		// 수수료
		[Column("comm_price")]
		public string CommissionPrice { get; set; }

		// 티켓번호
		[Column("gd_cd")]
		public string GoodsCode { get; set; }

		// 상품번호
		[Column("gd_no")]
		public string GoodsNo { get; set; }

		// 경기장이미지
		[Column("stadium_img_path")]
		public string StadiumImagePath { get; set; }

		// 회차번호
		[Column("seq_cd")]
		public string SequenceCode { get; set; }

		// ext_gd_cd == outer_gd_no ??
		[Column("ext_gd_cd")]
		public string ExtGoodsCode { get; set; }

		// 상품 이미지 URL
		public string GoodsSImageURL { get; set; }

		// 공연일
		public DateString PlayDateTime { get; set; }

		// 예매오픈
		public DateString ReservationDateTime { get; set; } 

		// 취소마감
		public DateString CancelAbleDateTime { get; set; }

		// 현재 시간이 예매 가능한 시간인지 여부
		public bool IsCanReservation { get; set; }
	}

	public partial class DateString
	{
		// 년
		public string Year { get; set; }
		// 월
		public string Month { get; set; }
		// 일
		public string Day { get; set; }
		// 시
		public string Hour { get; set; }
		// 분
		public string Minute { get; set; }
		// 초
		public string Second { get; set; }
	}

	public partial class GoodsDetailInfoT
	{
		// 여행지URL (상세정보 URL)
		[Column("rsv_det_trns_url")]
		public string RsvDetTrnsUrl { get; set; }
	}

	public partial class GoodsBasicInfoT
	{
		// 상품번호
		[Column("gd_no")]
		public string GoodsNo { get; set; }

		// 상품명
		[Column("gd_nm")]
		public string GoodsName { get; set; }

		//// 대분류 코드
		//[Column("gdlc_cd")]
		//public string GdlcCode { get; set; }

		//// 중분류 코드
		//[Column("gdmc_cd")]
		//public string GdmcCode { get; set; }

		//// 소분류 코드
		//[Column("gdsc_cd")]
		//public string GdscCode { get; set; }

		//// 성인상품 여부
		//[Column("adult_yn")]
		//public string AdultYN { get; set; }

		// gd_shopkind
		[Column("gd_shopkind")]
		public string ShopKind { get; set; }

		// gd_shopkind2
		[Column("gd_shopkind2")]
		public string ShopKind2 { get; set; }

		// gd_shopkind3
		[Column("gd_shopkind3")]
		public string ShopKind3 { get; set; }

		//// 제조사
		//[Column("maker_nm")]
		//public string ShopKind3 { get; set; }

		[Column("trad_way")]
		public string TradWay { get; set; }

		public string TradWayName { get; set; }

		public string AdditionalInfo { get; set; }

		[Column("tax_issue_yn")]
		public string TaxIssueYN { get; set; }

		[Column("cash_issue_yn")]
		public string CashIssueYN { get; set; }

		[Column("delivery_group_no")]
		public string DeliveryGroupNo { get; set; }

		[Column("delivery_group_nm")]
		public string DeliveryGroupName { get; set; }

		[Column("basis_money")]
		public int BasisMoney { get; set; }

		[Column("delivery_fee")]
		public string DeliveryFee { get; set; }

		[Column("delivery_fee_yn")]
		public string DeliveryFeeYN { get; set; }

		[Column("delivery_fee_condition")]
		public string DeliveryFeeCondition { get; set; }

		[Column("delivery_group_type")]
		public int DeliveryGroupType { get; set; }

		[Column("who_fee")]
		public string WhoFee { get; set; }

		[Column("basis_type")]
		public string BasisType { get; set; }

		[Column("delivery_bundle_no")]
		public string DeliveryBundleNo { get; set; }

		[Column("delivery_fee_condition_no")]
		public string DeliveryFeeConditionNo { get; set; }

		[Column("delivery_bundle_policy")]
		public string BundlePolicy { get; set; }

		public string DeliverySZYN { get; set; }

		public string OverSeaGoodsYN { get; set; }

		public string OverSeaNation { get; set; }

	}

	public partial class GoodsCouponInfoT
	{
		// 쿠폰 URL(취소, 환불 정보)
		[Column("web_url")]
		public string WebUrl { get; set; }

		// 상품명
		[Column("gd_nm")]
		public string GoodsName { get; set; }
	}

	public partial class TicketSeatClassT
	{
		// 티켓코드 + SEAT_CL
		[Column("seat_cl")]
		public string TicketCodeSeatClass { get; set; }

		//  좌석명
		[Column("seat_cl_nm")]
		public string SeatClassName { get; set; }

		// 공연코드
		public string TicketCode { get; set; }

		// 좌석등급번호
		public string SeatClass { get; set; }

		// 잔여 좌석수 
		public string SeatCount { get; set; }

		// 좌석 위치명
		public string StadiumZoneName { get; set; }

		// 가격
		public string Price { get; set; }

	}

	public partial class PriceInfo
	{
		// 상품 코드
		public string Goodscode { get; set; }

		// 좌석등급번호
		public string SequenceCode { get; set; }

		// 좌석 등급
		public string SeatClass { get; set; }

		// 좌석 이름
		public string SeatClassName { get; set; }

		// 가격 등급
		public string PriceClass { get; set; }

		// 가격 코드 이름
		public string PriceClassName { get; set; }

		// 가격 
		public string Price { get; set; }

		// 가격 코드
		public string PriceCode { get; set; }

		// CardDiscCode
		public string CardDiscCode { get; set; }

		// VoucherGubun
		public string VoucherGubun { get; set; }

	}

	public partial class SelectedPriceInfo
	{
		// index
		public string idx { get; set; }

		// 가격
		public int price { get; set; }

		// 이름
		public string name { get; set; }

		// 갯수
		public int count { get; set; }

		// 가격 등급
		public string priceClass { get; set; }

		// 가격 코드
		public string priceCode { get; set; }

		// VoucherGubun
		public string voucherGubun { get; set; }

		// CardDiscCode
		public string cardDiscCode { get; set; }

		// 바우쳐 여부
		public bool isVoucher { get; set; }

	}

	public partial class RandomSelectResult
	{
		// 에러코드 
		public int errorCode { get; set; }

		// 에러 메시지
		public string errorMessage { get; set; }

		// encrypted 된 lockNo
		public string lockNo { get; set; }

		// VoucherGubun
		public string voucherGubun { get; set; }

		// CardDiscCode
		public string cardDiscCode { get; set; }
	}

	public partial class VipResult
	{
		// 에러코드 
		public int errorCode { get; set; }

		// 에러 메시지
		public string errorMessage { get; set; }
	}
}
