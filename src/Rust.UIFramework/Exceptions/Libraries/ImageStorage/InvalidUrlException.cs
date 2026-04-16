using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Exceptions;

public class InvalidUrlException : BaseUiFrameworkException
{
    internal InvalidUrlException(string url) : base($"{url} is not a valid http url. Url must start with http or www.")
    {
        
    }
    
    internal static void ThrowIfInvalidUrl(string url)
    {
        if (!url.IsValidUrl()) throw new InvalidUrlException(url);
    }
}