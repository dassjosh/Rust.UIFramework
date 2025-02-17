using System.Collections.Generic;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Data;

internal class ImageStorageData : BaseDataFile<ImageStorageData>
{
    internal SaveVersion SaveVersion;
    internal readonly List<UrlImage> URLImages = [];
    internal readonly List<NamedImage> PluginImages = [];

    public void AddUrlImage(string url, ImageId imageId)
    {
        URLImages.Add(new UrlImage(url, imageId));
        OnDataChanged();
    }
    
    public void AddPluginImage(in PluginImage pluginImage, ImageId imageId)
    {
        PluginImages.Add(new NamedImage(pluginImage.PluginId, pluginImage.Name, imageId));
        OnDataChanged();
    }

    internal override void OnDataLoaded(DataFileInfo info)
    {
        base.OnDataLoaded(info);
        SaveVersion saveVersion = new(Rust.Protocol.save, CommunityEntity.ServerInstance.net.ID.Value);
        if (saveVersion != SaveVersion)
        {
            Wipe();
            SaveVersion = saveVersion;
        }
    }

    public void Wipe()
    {
        URLImages.Clear();
        PluginImages.Clear();
        OnDataChanged();
    }
}