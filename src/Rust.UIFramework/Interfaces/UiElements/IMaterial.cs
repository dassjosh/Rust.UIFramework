using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IMaterial
{
    string Material { get; set; }
}

public interface IMaterial<out T> : IMaterial where T : BaseUiComponent
{
    T SetMaterial(string material);
}