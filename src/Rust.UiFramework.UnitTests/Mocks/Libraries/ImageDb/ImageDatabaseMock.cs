using System.Collections.Concurrent;
using Oxide.Core.Libraries;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries;

namespace Rust.UiFramework.UnitTests.Mocks.Libraries.ImageDb;

internal class ImageDatabaseMock : Library, IImageDatabase
{
    private readonly ConcurrentDictionary<ImageId, byte[]> _images = new();

    public ImageDatabaseType Type { get; set; }
    public bool Exists(ImageId id) => _images.ContainsKey(id);

    public ImageId Store(byte[] image)
    {
        ImageId id = new(Crc.GetCRC(image));
        _images.TryAdd(id, image);
        return id;
    }

    public void Remove(ImageId id) => _images.TryRemove(id, out _);
    public byte[] Get(ImageId id) => _images.GetValueOrDefault(id);
    public List<ImageId> GetExistingImages() => [.. _images.Keys];
}