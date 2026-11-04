using System.Collections.Generic;
using Oxide.Ext.UiFramework.Guards;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiFileStorageDatabase : BaseUiFrameworkLibrary, IImageDatabase
{
    public ImageDatabaseType Type => ImageDatabaseType.FileStorage;
    private NetworkableId _id;

    private UiFileStorageDatabase() { }

    public bool Exists(ImageId id) => Get(id) != null;

    public ImageId Store(byte[] image)
    {
        Guard.IsMainThread();
        return new ImageId(FileStorage.server.StoreByCrc(_id, FileStorage.Type.png, image));
    }

    public void Remove(ImageId id)
    {
        Guard.IsMainThread();
        FileStorage.server.Remove(_id, id.Id);
    }

    public byte[] Get(ImageId id)
    {
        Guard.IsMainThread();
        return FileStorage.server.Get(_id, id.Id);
    }

    public List<ImageId> GetExistingImages()
    {
        AssociatedFiles.AssociatedFile[] storedImages = [.. FileStorage.server.QueryAllByEntity(_id)];
        List<ImageId> existing = [];
        for (int index = 0; index < storedImages.Length; index++)
        {
            using AssociatedFiles.AssociatedFile image = storedImages[index];
            existing.Add(new ImageId((uint)image.part));
        }

        return existing;
    }

    protected override void OnCommunityEntitySpawned(ICommunityEntity entity)
    {
        _id = new NetworkableId(entity.Id);
    }
}