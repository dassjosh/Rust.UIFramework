using System.Collections.Concurrent;
using Oxide.Core.Libraries;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries;

namespace Rust.UiFramework.UnitTests.Mocks.Libraries.ImageDb;

internal class ImageDatabaseMock : Library, IImageDatabase
{
    private readonly ConcurrentDictionary<ImageId, byte[]> _images = new();

    public void OnImageRegistered(ImageId id) { }

    public bool Exists(ImageId id) => _images.ContainsKey(id);

    public ImageId Store(byte[] image)
    {
        ImageId id = new(Crc.GetCRC(image));
        _images.TryAdd(id, image);
        return id;
    }

    public byte[] Get(ImageId id)
    {
        return _images.GetValueOrDefault(id);
    }

    public SaveVersion GetSaveVersion(ICommunityEntity entity)
    {
        return new SaveVersion(entity.Id);
    }
}