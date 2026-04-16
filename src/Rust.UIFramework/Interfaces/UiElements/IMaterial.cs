using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IMaterial
{
    string Material { get; set; }
}

public interface IMaterial<out T> : IMaterial where T : BaseUiComponent
{
    T SetMaterial(string material);
}