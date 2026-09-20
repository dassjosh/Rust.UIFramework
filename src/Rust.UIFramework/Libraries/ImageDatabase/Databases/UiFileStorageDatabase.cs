using System.Collections.Generic;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiFileStorageDatabase : BaseUiFrameworkLibrary, IImageDatabase
{
    public ImageDatabaseType Type => ImageDatabaseType.FileStorage;
    private NetworkableId _id;

    private UiFileStorageDatabase() { }

    public bool Exists(ImageId id) => Get(id) != null;
    public ImageId Store(byte[] image) => new(FileStorage.server.Store(image, FileStorage.Type.png, _id));
    public void Remove(ImageId id) => FileStorage.server.Remove(id.Id, FileStorage.Type.png, _id);

    public byte[] Get(ImageId id) => FileStorage.server.Get(id.Id, FileStorage.Type.png, _id);
    public List<ImageId> GetExistingImages()
    {
        AssociatedFiles.AssociatedFile[] storedImages = [.. FileStorage.server.QueryAllByEntity(_id)];
        List<ImageId> existing = [];
        for (int index = 0; index < storedImages.Length; index++)
        {
            using AssociatedFiles.AssociatedFile image = storedImages[index];
            existing.Add(new ImageId(image.numID));
        }

        return existing;
    }

    protected override void OnCommunityEntitySpawned(ICommunityEntity entity)
    {
        _id = new NetworkableId(entity.Id);
    }
}