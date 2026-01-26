using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiFileStorageDatabase : BaseUiFrameworkLibrary, ISingleton, IImageDatabase
{
    private UiFileStorageDatabase() { }

    public void OnImageRegistered(ImageId id)
    {
        // Do nothing for FP FileStorage
    }

    public bool Exists(ImageId id) => FileStorage.server.Get(id.Id, FileStorage.Type.png, CommunityEntity.ServerInstance.net.ID) != null;

    public ImageId Store(byte[] image)
    {
        return new ImageId(FileStorage.server.Store(image, FileStorage.Type.png, CommunityEntity.ServerInstance.net.ID));
    }

    public SaveVersion GetSaveVersion(CommunityEntity entity) => new(entity.net.ID.Value);
}