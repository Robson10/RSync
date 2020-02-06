using RSync.AppResources.Localization;
using RSync.Core.Extends;

namespace RSync.Core.Enumerations
{
    public enum ServerType
    {
        [ResourceAttribute(nameof(res.enumServerGoogle))]
        Google = 0,
        [ResourceAttribute(nameof(res.AppName))]
        test,
    }
}
