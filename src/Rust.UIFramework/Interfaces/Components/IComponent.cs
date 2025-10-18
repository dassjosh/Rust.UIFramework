using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IComponent
{
    void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode);
    bool HasChanged();
    void ResetHasChanged();
    void Reset();
}