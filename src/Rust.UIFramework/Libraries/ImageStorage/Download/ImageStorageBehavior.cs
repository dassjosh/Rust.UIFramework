using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;
//TODO: Switch to UniTask when available
#if SERVER
using System.Collections.Concurrent;
/// <summary>
/// Stores downloaded images on the main thread
/// </summary>
internal class ImageStorageBehavior : FacepunchBehaviour, IImageStorageBehavior
{
    private readonly ConcurrentQueue<DownloadImageRequest> _completed = new();

    private void Awake()
    {
        enabled = false;
    }
    
    public void OnDownloadCompleted(DownloadImageRequest download)
    {
        _completed.Enqueue(download);
        enabled = true;
    }
    
    private void Update()
    {
        if (_completed.TryDequeue(out DownloadImageRequest download))
        {
            Singleton<UiImageStorage>.Instance.StoreDownloadedImage(download);
            return;
        }

        enabled = false;
    }
}
#else
internal class ImageStorageBehavior : ISingleton, IImageStorageBehavior
{
    private ImageStorageBehavior() { }
    
    public void OnDownloadCompleted(DownloadImageRequest download)
    {
        Singleton<UiImageStorage>.Instance.StoreDownloadedImage(download);
    }
}
#endif

internal interface IImageStorageBehavior
{
    void OnDownloadCompleted(DownloadImageRequest download);
}