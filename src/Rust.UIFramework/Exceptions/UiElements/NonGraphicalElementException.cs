using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Exceptions.UiElements;

public class NonGraphicalElementException : BaseUiFrameworkException
{
    private NonGraphicalElementException(string message) : base(message)
    {

    }

    public static void ThrowIfNonGraphicalElement<T>(T component) where T : BaseUiComponent
    {
        if (component is not UiImage and not UiRawImage and not UiLabel)
        {
            throw new NonGraphicalElementException($"{component.GetType().Name} is not a graphical element. Graphical Elements are {nameof(UiImage)}, {nameof(UiRawImage)}, and {nameof(UiLabel)}");
        }
    }
}