using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    class LandingBannerApiBiz
    {
        public ILandingBannerEntityT GetBy(ILandingBannerSearch search)
        {
            return new LandingBannerApiDac().FindBy(search);
        }
    }
}
