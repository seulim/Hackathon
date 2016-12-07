using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
    public interface ILandingBannerModel
    {
        ILandingBannerEntityT LandingBanner { get; set; }
        ICampaign Campaign { get; set; }
    }

    public interface ICampaign
    {
        /// <summary>
        /// 마케팅 캠페인 이름
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 마케팅 캠페인 채널명
        /// </summary>
        string Medium { get; }

        /// <summary>
        /// 매체 소스
        /// </summary>
        string MediumSource { get; }

        /// <summary>
        /// 매체 컨텐츠
        /// </summary>
        string MediumContent { get; }

        /// <summary>
        /// 키워드
        /// </summary>
        string Keyword { get; }
    }
}