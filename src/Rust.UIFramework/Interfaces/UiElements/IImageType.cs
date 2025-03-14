using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IImageType
{
    void SetImageType(Image.Type type);
}

public interface IImageType<out T> : IImageType where T : BaseUiComponent
{
    T SetImageType(Image.Type type);
}