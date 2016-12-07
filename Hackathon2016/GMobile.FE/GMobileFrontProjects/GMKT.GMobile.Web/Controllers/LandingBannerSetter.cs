using System;
using System.Web;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Web.Util;
using GMKT.Web;

namespace GMKT.GMobile.Web.Controllers
{
    class LandingBannerSetter
    {
        public LandingBannerSetter(HttpRequestBase request)
        {
            _request = request;
        }

        readonly HttpRequestBase _request;

        public ILandingBannerModel Set(ILandingBannerModel model, bool isApp)
        {
            if (!isApp && GetLandingDisplay())
            {
                LandingBannerType landingType = GetLandingType();
                var landingBannerResult = new LandingBannerApiBiz_Cache().GetFromCacheBy(landingType);

                if (landingBannerResult != null)
                {
                    var bannerModel = new LandingBannerModel()
                    {
                        ImageUrl = landingBannerResult.ImageUrl,
                        Description = landingBannerResult.Description,
                        Type = landingType,
                        Name = _request.QueryString["utm_campaign"] ?? string.Empty,
                        Medium = _request.QueryString["utm_medium"] ?? string.Empty,
                        MediumSource = _request.QueryString["utm_source"] ?? string.Empty,
                        MediumContent = _request.QueryString["utm_content"] ?? string.Empty,
                        Keyword = _request.QueryString["utm_term"] ?? string.Empty,
                    };

                    model.LandingBanner = bannerModel;
                    model.Campaign = bannerModel;

                    return model;
                }
            }

            model.LandingBanner = new LandingBannerModel();
            model.Campaign = new LandingBannerModel();

            return model;
        }

        public T ConvertStringToEnum<T>(String enumString) where T : struct
        {
            var lowerString = enumString.ToLower();
            var enumValues = typeof(T).GetEnumValues();
            foreach (var item in enumValues)
            {
                if (item.ToString().ToLower().Equals(lowerString))
                    return (T)item;
            }
            return default(T);
        }

        private LandingBannerType GetLandingType()
        {   
            string landingType = _request.QueryString["landingtype"];

            if (string.IsNullOrEmpty(landingType) == true)
                return LandingBannerType.B;

            return ConvertStringToEnum<LandingBannerType>(landingType);
        }

        private bool GetLandingDisplay()
        {
            string landingdisplay = _request.QueryString["landingdisplay"];

            if (string.IsNullOrEmpty(landingdisplay) == true)
                landingdisplay = "1";

            switch (landingdisplay)
            {
                case "0":
                    break;
                case "1":
                    {
                        var cookieHelper = new CookieHelper();

                        if (cookieHelper.GetCookieValue("landing", "banner") != null)
                            return false;

                        cookieHelper.SetCookieValue("landing", "banner", DateTime.Now.ToString(), GMKTEnvironment.Instance.BaseDomainUrl, DateTime.Now.AddDays(1));
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }

        class LandingBannerSearch : ILandingBannerSearch
        {
            public DateTime Date { get { return DateTime.Now; } }
            public LandingBannerType Type { get; set; }
        }

        class LandingBannerModel : ILandingBannerEntityT, ICampaign
        {
            /// <summary>
            /// Image Url
            /// </summary>
            public string ImageUrl { get; internal set; }

            /// <summary>
            /// Description
            /// </summary>
            public string Description { get; internal set; }

            /// <summary>
            /// Landing Banner Type
            /// </summary>
            public LandingBannerType Type { get; internal set; }

            public string Name { get; internal set; }

            public string Medium { get; internal set; }

            public string MediumSource { get; internal set; }

            public string MediumContent { get; internal set; }

            public string Keyword { get; internal set; }
        }
    }
}