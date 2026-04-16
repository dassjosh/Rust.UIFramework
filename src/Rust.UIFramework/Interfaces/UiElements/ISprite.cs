using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ISprite
{
    string Sprite { get; set; }
}

public interface ISprite<out T> : ISprite where T : BaseUiComponent
{
    T SetSprite(string sprite);
}