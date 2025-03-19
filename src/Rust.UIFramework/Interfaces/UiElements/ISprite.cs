using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ISprite
{
    void SetSprite(string sprite);
}

public interface ISprite<out T> : ISprite where T : BaseUiComponent
{
    new T SetSprite(string sprite);
}