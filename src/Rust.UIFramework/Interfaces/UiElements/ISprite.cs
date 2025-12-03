using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ISprite
{
    string Sprite { get; set; }
}

public interface ISprite<out T> : ISprite where T : BaseUiComponent
{
    T SetSprite(string sprite);
}