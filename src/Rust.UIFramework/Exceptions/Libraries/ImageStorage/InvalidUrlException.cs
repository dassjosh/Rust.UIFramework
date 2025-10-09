namespace Oxide.Ext.UiFramework.Exceptions;

public class InvalidUrlException : BaseUiFrameworkException
{
    internal InvalidUrlException(string url) : base($"{url} is not a valid http url. Url must start with http.")
    {
        
    }
}