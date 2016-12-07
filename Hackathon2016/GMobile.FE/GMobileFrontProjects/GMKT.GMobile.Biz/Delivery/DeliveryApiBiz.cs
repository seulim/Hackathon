using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class DeliveryApiBiz
	{
		public DaumAddressToCoordChannel GetAddressSearch(string keyword)
		{
			var response = new DeliveryApiDac().GetAddressSearch(keyword);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new DaumAddressToCoordChannel();
			}
		}

		public DaumCoordToAddress GetCoordToAddress(double longitude, double latitude)
		{
			var response = new DeliveryApiDac().GetCoordToAddress(longitude, latitude);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new DaumCoordToAddress();
			}
		}

		public DeliveryBannerCategory GetDeliveryBannerCategory(string userInfo, double longitude, double latitude, string zipCode)
		{
			var response = new DeliveryApiDac().GetDeliveryBannerCategory( userInfo, longitude, latitude, zipCode );
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new DeliveryBannerCategory();
			}
		}

		public DeliveryMain GetDeliveryMain( string userInfo )
		{
			var response = new DeliveryApiDac().GetDeliveryMain( userInfo );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new DeliveryMain();
			}
		}

		public DeliveryMain GetDeliveryShop(string userInfo, double longitude, double latitude, string zipCode, int myShopPageCount)
		{
			var response = new DeliveryApiDac().GetDeliveryShop(userInfo, longitude, latitude, zipCode, myShopPageCount);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new DeliveryMain();
			}
		}

		public DeliveryAgree GetDeliveryAgreeInfo(string userInfo)
		{
			var response = new DeliveryApiDac().GetDeliveryAgreeInfo(userInfo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new DeliveryAgree();
			}
		}

		public int SetDeliveryAgreeInfo( DeliveryPosityionAddType type, string yn, string userInfo )
		{
			var response = new DeliveryApiDac().GetSetDeliveryAgreeInfo( type, yn, userInfo );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return -1;
			}
		}
	}
}
