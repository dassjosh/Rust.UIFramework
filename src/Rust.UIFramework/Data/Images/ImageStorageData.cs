using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Data;

[ProtoContract]
internal readonly record struct SaveVersion([property: ProtoMember(1)] ulong EntityId);

[ProtoContract]
internal class ImageStorageData : BaseDataFile<ImageStorageData>
{
    [ProtoMember(1)]
    private SaveVersion _saveVersion;
    [ProtoMember(2)]
    private readonly ConcurrentDictionary<string, ImageId> _urlImages = [];
    [ProtoMember(3)]
    private readonly ConcurrentDictionary<PluginImage, ImageId> _pluginImages = [];

    public void AddUrlImage(string url, ImageId imageId)
    {
        if (!_urlImages.TryGetValue(url, out ImageId existingId) || existingId != imageId)
        {
            _urlImages[url] = imageId;
            OnDataChanged();
        }
    }
    
    public void AddPluginImage(PluginId pluginId, string name, ImageId imageId)
    {
        PluginImage pluginImage = new(pluginId, name);
        if (!_pluginImages.TryGetValue(pluginImage, out ImageId existingId) || existingId != imageId)
        {
            _pluginImages[pluginImage] = imageId;
            OnDataChanged();
        }
    }

    public ImageId Get(PluginId pluginId, string name)
    {
        if (_pluginImages.TryGetValue(new PluginImage(pluginId, name), out ImageId png) && png.IsValid)
        {
            return png;
        }
        
        if (_urlImages.TryGetValue(name, out ImageId url) && url.IsValid)
        {
            return url;
        }

        return default;
    }

    public ImageId GetByUrl(string url)
    {
        if (_urlImages.TryGetValue(url, out ImageId image) && image.IsValid)
        {
            return image;
        }

        return default;
    }

    internal void OnCommunityEntityLoaded()
    {
        SaveVersion saveVersion = new(CommunityEntity.ServerInstance.net.ID.Value);
        if (saveVersion != _saveVersion)
        {
            Wipe();
            _saveVersion = saveVersion;
        }
    }

    public void Wipe()
    {
        _urlImages.Clear();
        _pluginImages.Clear();
        OnDataChanged();
    }
}