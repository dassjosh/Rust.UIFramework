namespace Oxide.Ext.UiFramework.Libraries;

public readonly struct BulkByteImageRequest(string name, byte[] image)
{
    public readonly string Name = name;
    public readonly byte[] Image = image;
}