using System;

namespace GMKT.GMobile.Data
{
    public interface ILandingBannerSearch
    {
        DateTime Date { get; }
        LandingBannerType Type { get; }
    }
}
