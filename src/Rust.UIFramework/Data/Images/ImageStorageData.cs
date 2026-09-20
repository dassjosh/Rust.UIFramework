using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Extensions;
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
    public SaveVersion SaveVersion { get; private set; }
    [ProtoMember(2)]
    public ImageDatabaseType Type { get; private set; }
    [ProtoMember(3)]
    private readonly ConcurrentDictionary<ImageId, DateTime> _imageLastAccessed = [];
    [ProtoMember(4)]
    private readonly ConcurrentDictionary<string, ImageId> _urlImages = [];
    [ProtoMember(5)]
    private readonly ConcurrentDictionary<PluginImage, ImageId> _pluginImages = [];
    [ProtoMember(6)]
    private readonly ConcurrentDictionary<string, ImageId> _borderRadiusImages = [];

    public void AddUrlImage(string url, ImageId imageId)
    {
        if (!_urlImages.TryGetValue(url, out ImageId existingId) || existingId != imageId)
        {
            _urlImages[url] = imageId;
            Touch(imageId);
        }
    }
    
    public void AddPluginImage(PluginId pluginId, string name, ImageId imageId)
    {
        PluginImage pluginImage = new(pluginId, name);
        if (!_pluginImages.TryGetValue(pluginImage, out ImageId existingId) || existingId != imageId)
        {
            _pluginImages[pluginImage] = imageId;
            Touch(imageId);
        }
    }

    public void AddBorderRadiusImage(string name, ImageId imageId)
    {
        if (!_borderRadiusImages.TryGetValue(name, out ImageId existingId) || existingId != imageId)
        {
            _borderRadiusImages[name] = imageId;
            Touch(imageId);
        }
    }

    public ImageId Get(PluginId pluginId, string name)
    {
        if (_pluginImages.TryGetValue(new PluginImage(pluginId, name), out ImageId png) && png.IsValid)
        {
            Touch(png);
            return png;
        }
        
        if (_urlImages.TryGetValue(name, out ImageId url) && url.IsValid)
        {
            Touch(url);
            return url;
        }

        return default;
    }

    public ImageId GetByUrl(string url)
    {
        if (_urlImages.TryGetValue(url, out ImageId image) && image.IsValid)
        {
            Touch(image);
            return image;
        }

        return default;
    }

    public ImageId GetBorderRadius(string name)
    {
        if (_borderRadiusImages.TryGetValue(name, out ImageId image))
        {
            Touch(image);
            return image;
        }

        return default;
    }

    public void Touch(ImageId imageId)
    {
        _imageLastAccessed[imageId] = DateTime.UtcNow;
        OnDataChanged();
    }

    public IEnumerable<ImageId> GetExpiredImages() => _imageLastAccessed
        .Where(x => x.Value + TimeSpan.FromDays(UiFrameworkConfig.Instance.ImageDatabase.UnusedImageMaxDays) < DateTime.UtcNow)
        .Select(x => x.Key);

    public void RemoveImage(ImageId id)
    {
        _imageLastAccessed.TryRemove(id, out _);
        _pluginImages.TryRemoveByValue(id);
        _urlImages.TryRemoveByValue(id);
        _borderRadiusImages.TryRemoveByValue(id);
        OnDataChanged();
    }

    public void OnCommunityEntityLoaded(ICommunityEntity entity, ImageDatabaseType type)
    {
        SaveVersion saveVersion = new(entity.Id);
        if(Type != type)
        {
            Type = type;
            Wipe(saveVersion);
            return;
        }

        if (SaveVersion != saveVersion)
        {
            if (Type == ImageDatabaseType.FileStorage)
            {
                Wipe(saveVersion);
            }
            else
            {
                SaveVersion = saveVersion;
                OnDataChanged();
            }
        }
    }

    private void Wipe(SaveVersion saveVersion)
    {
        SaveVersion = saveVersion;
        _imageLastAccessed.Clear();
        _urlImages.Clear();
        _pluginImages.Clear();
        _borderRadiusImages.Clear();
        OnDataChanged();
    }
}