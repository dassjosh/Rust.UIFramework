using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces.Builders;

public interface INamingStrategy
{
    void SetComponentName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, string rootName, int elementNum);
    void SetAnchorName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, string rootName, int elementNum);
}