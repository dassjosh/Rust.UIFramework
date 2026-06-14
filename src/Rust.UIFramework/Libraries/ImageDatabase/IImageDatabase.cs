using Oxide.Ext.UiFramework.Data;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface IImageDatabase
{
    void OnImageRegistered(ImageId id);
    bool Exists(ImageId id);
    ImageId Store(byte[] image);
    byte[] Get(ImageId id);
    SaveVersion GetSaveVersion(ICommunityEntity entity);
}