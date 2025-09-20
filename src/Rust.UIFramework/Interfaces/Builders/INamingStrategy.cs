using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface INamingStrategy
{
    void SetComponentName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, INamingCache cache, int elementNum);
    void SetAnchorName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, INamingCache cache, int elementNum);
}