using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IBorderRadiusRequest : IRegisterImageRequest
{
    UiSize2D Size { get; }
    UiBorderRadius Radius { get; }
    bool AntiAlias { get; }
    float EdgeWidth { get; }
}