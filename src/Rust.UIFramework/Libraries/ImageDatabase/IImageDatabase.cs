using Oxide.Ext.UiFramework.Data;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface IImageDatabase
{
    void OnImageRegistered(ImageId id);
    bool Exists(ImageId id);
    ImageId Store(byte[] image);
    SaveVersion GetSaveVersion(CommunityEntity entity);
}