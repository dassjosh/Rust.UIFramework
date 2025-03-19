using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IMaterial
{
    void SetMaterial(string material);
}

public interface IMaterial<out T> : IMaterial where T : BaseUiComponent
{
    new T SetMaterial(string material);
}