using System.Collections;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Harmony;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiImageDatabaseHandler : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ImageStorageData _data = ImageStorageData.Instance;
    private readonly IImageDatabase _db = OxideLibrary.GetLibrary<IImageDatabase>(nameof(IImageDatabase));
    private readonly ConcurrentHashSet<ImageId> _storedImages = [];
    private readonly IUiLogger _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiImageDatabaseHandler>();

    private UiImageDatabaseHandler() { }

    public bool TryGetImageId(PluginId pluginId, string image, out ImageId id)
    {
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);

        if(!ImageId.TryParse(image, out id))
        {
            id = _data.Get(pluginId, image);
        }

        if (!ValidateImageId(id))
        {
            _logger.Verbose($"{nameof(TryGetImageId)} Failed to Validate ID Plugin: {{0}} Image: {{1}}", pluginId.Id, image);
            return false;
        }

        return true;
    }

    public bool TryGetByUrl(string url, out ImageId id)
    {
        Guard.IsValidUrl(url);
        id = _data.GetByUrl(url);
        if (!ValidateImageId(id))
        {
            _logger.Verbose($"{nameof(TryGetByUrl)} Failed to Validate Id Url: {{0}}", url);
            return false;
        }

        return true;
    }

    public bool TryGetBytes(PluginId pluginId, string image, out byte[] bytes)
    {
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(image);
        if (!TryGetImageId(pluginId, image, out ImageId id))
        {
            bytes = [];
            _logger.Verbose($"{nameof(TryGetBytes)} Failed for Plugin: {{0}} Image: {{1}}", pluginId.Id, image);
            return false;
        }

        return TryGetBytes(id, out bytes);
    }

    public bool TryGetBytes(ImageId id, out byte[] image)
    {
        image = _db.Get(id);
        if (image == null || image.Length == 0)
        {
            image = [];
            _logger.Verbose($"{nameof(TryGetBytes)} Failed for ID: {{0}}", id.Id);
            return false;
        }

        return true;
    }

    public bool TryGetBorderRadiusImageId(BorderRadiusData radius, out ImageId id)
    {
        Guard.IsNotNull(radius);
        id = _data.GetBorderRadius(BorderRadiusKeyCache.GetKey(radius));
        if (!ValidateImageId(id))
        {
            _logger.Verbose($"{nameof(TryGetBorderRadiusImageId)} Failed to Validate Id Radius: {{0}}", BorderRadiusKeyCache.GetKey(radius));
            return false;
        }

        return true;
    }

    private bool ValidateImageId(ImageId id)
    {
        if (!id.IsValid)
        {
            _logger.Verbose($"{nameof(ValidateImageId)} Invalid ID: {{0}}", id.Id);
            return false;
        }

        if (!_storedImages.Contains(id))
        {
            _logger.Verbose($"{nameof(ValidateImageId)} Stored Images does not contain ID: {{0}}", id.Id);
            return false;
        }

        Touch(id);
        return true;
    }

    public void AddPluginImage(PluginId pluginId, string name, ImageId id)
    {
        _data.AddPluginImage(pluginId, name, id);
        OnImageAdded(id);
        _logger.Verbose($"{nameof(AddPluginImage)} Plugin: {{0}} Name: {{1}} ID: {{2}}", pluginId.Id, name, id.Id);
    }

    public void AddUrlImage(string url, ImageId id)
    {
        _data.AddUrlImage(url, id);
        OnImageAdded(id);
        _logger.Verbose($"{nameof(AddUrlImage)} Url: {{0}} ID: {{1}}", url, id.Id);
    }

    public void AddBorderRadiusImage(string name, ImageId id)
    {
        _data.AddBorderRadiusImage(name, id);
        OnImageAdded(id);
        _logger.Verbose($"{nameof(AddBorderRadiusImage)} Name: {{0}} ID: {{1}}", name, id.Id);
    }

    public ImageId Store(byte[] image)
    {
        ImageId id = _db.Store(image);
        _logger.Verbose($"{nameof(Store)} ID: {{0}} Valid: {{1}}", id.Id, id.IsValid);
        if (id.IsValid)
        {
            OnImageAdded(id);
        }
        return id;
    }

    private void OnImageAdded(ImageId id)
    {
        _storedImages.Add(id);
        Touch(id);
    }

    private void Touch(ImageId id)
    {
        _data.Touch(id);
    }

    protected override void OnCommunityEntitySpawned(ICommunityEntity entity)
    {
        _data.OnCommunityEntityLoaded(entity, _db.Type);
        LoadExistingImages();
        PatchFileStorageGetPrefix(entity);
    }

    private void PatchFileStorageGetPrefix(ICommunityEntity entity)
    {
        if (_db.Type == ImageDatabaseType.ImageDatabase)
        {
            FileStorage_Get_Prefix_Patch.Patch(entity);
        }
    }

    protected override void OnServerInitialized()
    {
        if (_db.Type == ImageDatabaseType.ImageDatabase)
        {
            IEnumerable<ImageId> expiredImages = _data.GetExpiredImages();
            SingletonBehavior<UiFrameworkBehavior>.Instance.StartCoroutine(ClearExpiredImages(expiredImages));
        }
    }

    private IEnumerator ClearExpiredImages(IEnumerable<ImageId> expiredImages)
    {
        foreach (ImageId id in expiredImages)
        {
            _data.RemoveImage(id);
            _db.Remove(id);
            _logger.Verbose($"{nameof(ClearExpiredImages)} ID: {{0}}", id.Id);
            yield return null;
        }
    }

    private void LoadExistingImages()
    {
        List<ImageId> images = _db.GetExistingImages();
        _storedImages.UnionWith(images);
    }
}