namespace Oxide.Ext.UiFramework.Exceptions;

public class InvalidStoreNameException : BaseUiFrameworkException
{
    private InvalidStoreNameException() : base("Store name cannot be null or empty") { }

    internal static void ThrowIfInvalidStoreName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidStoreNameException();
        }
    }
}