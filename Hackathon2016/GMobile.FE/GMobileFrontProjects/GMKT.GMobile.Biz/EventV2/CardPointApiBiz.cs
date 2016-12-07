using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;
using GMKT.Framework.EnterpriseServices;
using GMKT.Component.Member;

namespace GMKT.GMobile.Biz.EventV2
{
	public class CardPointApiBiz
	{
		public CardBenefitJsonEntityT GetCardBenefitEntity()
		{
			ApiResponse<CardBenefitJsonEntityT> response = new CardPointDac().GetCardBenefitEntity();
			if( response != null )
			{
				return response.Data;
			}
			else
			{
				return new CardBenefitJsonEntityT();
			}
		}

		public PointBenefitJsonEntityT GetPointBenefitEntity()
		{
			ApiResponse<PointBenefitJsonEntityT> response = new CardPointDac().GetPointBenefitEntity();
			if( response != null )
			{
				return response.Data;
			}
			else
			{
				return new PointBenefitJsonEntityT();
			}
		}

		public bool IsReregOCB(string custNo)
		{
			return this.IsReregPartnerCard(custNo, "O");
		}

		public bool IsReregAsiana(string custNo)
		{
			return this.IsReregPartnerCard(custNo, "A");
		}

		protected bool IsReregPartnerCard(string custNo, string cardType)
		{
			if (false == string.IsNullOrEmpty(custNo) && false == string.IsNullOrEmpty(cardType))
			{
				string reregYN = string.Empty;
				try
				{
					reregYN = new MyInfoBiz().GetJaehuCardDuplicateInfo(custNo, cardType);
				}
				catch { } //Ignore

				if (!string.IsNullOrEmpty(reregYN))
				{
					if (reregYN == "Y")
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
	}
}
