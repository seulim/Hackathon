
namespace GMKT.GMobile.Data
{
    /// <summary>
    /// Landing Banner Entity
    /// </summary>
    public interface ILandingBannerEntityT
    {
        string ImageUrl { get; }
        string Description { get; }
        LandingBannerType Type { get; }
    }
}
