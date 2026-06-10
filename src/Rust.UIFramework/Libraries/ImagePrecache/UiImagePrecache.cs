using System.Collections;
using System.Collections.Concurrent;
using Network;
using Oxide.Core;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.ImagePrecache;

public class UiImagePrecache : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ConcurrentList<PrecacheImage> _images = [];
    private readonly ConcurrentDictionary<PluginId, ConcurrentList<ImageId>> _pluginImages = new();
    private readonly ConcurrentDictionary<ImageId, ConcurrentList<PluginId>> _imagePlugins = new();
    
    private UiImagePrecache() { }

    public void AddPrecachedImage(IUiFrameworkPlugin plugin, string imageId, byte[] image) => AddPrecachedImage(plugin, uint.Parse(imageId), image);
    public void AddPrecachedImage(IUiFrameworkPlugin plugin, uint imageId, byte[] image) => AddPrecachedImage(plugin.Id(), new ImageId(imageId), image);

    internal void AddPrecachedImage(PluginId pluginId, ImageId imageId, byte[] image)
    {
        ConcurrentList<ImageId> images = _pluginImages.GetOrAdd(pluginId, _ => []);
        if (!images.Contains(imageId))
        {
            images.Add(imageId);
        }
        
        ConcurrentList<PluginId> plugins = _imagePlugins.GetOrAdd(imageId, _ => []);
        if (!plugins.Contains(pluginId))
        {
            plugins.Add(pluginId);
        }

        foreach (PrecacheImage precache in _images.GetPooledEnumerator())
        {
            if (precache.ImageId == imageId)
            {
                return;
            }
        }
        
        _images.Add(new PrecacheImage(imageId, image));
    }
    
    protected override void OnPlayerConnected(BasePlayer player)
    {
        if (_images.Count != 0)
        {
            SingletonBehavior<UiFrameworkBehavior>.Instance.StartCoroutine(PrecacheImages(player));
        }
    }

    private IEnumerator PrecacheImages(BasePlayer player)
    {
        SendInfo send = SendInfoBuilder.GetForPrecache(player);
        foreach (PrecacheImage image in _images.GetPooledEnumerator())
        {
            if (player && player.IsConnected)
            {
                RpcFunctions.SendFilePng(send, image.ImageId.Id, image.Image);
                yield return null;
            }
        }

        if (player && player.IsConnected)
        {
            Interface.Oxide.CallHook(UiFrameworkHooks.OnPlayerUiImagesPrecached, player);
        }
    }

    protected override void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        PluginId pluginId = plugin.Id();
        if (!_pluginImages.TryRemove(pluginId, out ConcurrentList<ImageId> images))
        {
            return;
        }
        
        foreach (ImageId image in images.GetPooledEnumerator())
        {
            if (!_imagePlugins.TryGetValue(image, out ConcurrentList<PluginId> plugins))
            {
                continue;
            }
            
            plugins.Remove(pluginId);
            if (plugins.Count == 0)
            {
                _imagePlugins.TryRemove(image, out _);
                _images.RemoveAll(i => i.ImageId == image);
            }
        }
    }

    private readonly record struct PrecacheImage(ImageId ImageId, byte[] Image);
}