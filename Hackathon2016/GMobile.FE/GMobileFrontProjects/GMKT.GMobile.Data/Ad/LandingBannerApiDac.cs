using ConnApi.Client;
using System;

namespace GMKT.GMobile.Data
{
    public class LandingBannerApiDac : ApiBase
    {
        public LandingBannerApiDac() : base ("GMApi")
        {
        }

        /// <summary>
        /// Api Call
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ILandingBannerEntityT FindBy(ILandingBannerSearch search)
        {
            //ToDO : Api Make Url 알아보기
            ApiResponse<Model> result = ApiHelper.CallAPI<ApiResponse<Model>>(
                    "GET",
                    ApiHelper.MakeUrl("api/LandingBanner/GetLandingBanner"),
                    new
                    {
                        bannerType = search.Type.ToString(),
                    }
                );

            Model2 ret = null;
            if (result.ResultCode == 0)
            {
                ret = new Model2();
                LandingBannerType t;

                ret.Type = Enum.TryParse<LandingBannerType>(result.Data.Type, out t) ? t : LandingBannerType.None;
                ret.ImageUrl = result.Data.ImageUrl;
                ret.Description = result.Data.Description;                 
            }

            return result.ResultCode != 0 ? null : ret;
        }
        
        /// <summary>
        /// Inner Class Model
        /// </summary>
        class Model
        {
            public string ImageUrl
            {
                get;
                set;
            }

            public string Description
            {
                get;
                set;
            }

            public string Type
            {
                get;
                set;
            }
        }

        class Model2 : ILandingBannerEntityT
        {
            public string ImageUrl
            {
                get;
                set;
            }

            public string Description
            {
                get;
                set;
            }

            public LandingBannerType Type
            {
                get;
                set;
            }
        }
    }
}
