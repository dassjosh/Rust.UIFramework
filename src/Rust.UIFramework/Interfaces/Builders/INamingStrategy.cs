using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces.Builders;

public interface INamingStrategy
{
    UiReference GetComponentName(in UiReference reference, UpdateMode updateMode, string baseName, int index);
    UiReference GetAnchorName(in UiReference reference, UpdateMode updateMode, string baseName, int index);
}