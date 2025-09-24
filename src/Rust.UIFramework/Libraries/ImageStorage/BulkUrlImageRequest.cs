namespace Oxide.Ext.UiFramework.Libraries;

public readonly struct BulkUrlImageRequest(string name, string url)
{
    public readonly string Name = name;
    public readonly string Url = url;

    public BulkUrlImageRequest(string url) : this(url, url) { }
}