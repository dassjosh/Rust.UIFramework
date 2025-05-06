using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public interface IComponent
{
    void WriteComponent(JsonFrameworkWriter writer);
    void Reset();
}