using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

#if !UNIT_TESTS
using System.Collections.Concurrent;
/// <summary>
/// Stores downloaded images on the main thread
/// </summary>
internal class ImageStorageBehavior : FacepunchBehaviour, IImageStorageBehavior
{
    private readonly ConcurrentQueue<CompletedDownload> _completed = new();

    private void Awake()
    {
        enabled = false;
    }
    
    public void OnDownloadComplete(in CompletedDownload download)
    {
        _completed.Enqueue(download);
        enabled = true;
    }
    
    private void Update()
    {
        if (_completed.TryDequeue(out CompletedDownload download))
        {
            Singleton<UiImageStorage>.Instance.OnImageDownloaded(download);
            return;
        }

        enabled = false;
    }
}
#else
internal class ImageStorageBehavior : ISingleton, IImageStorageBehavior
{
    private ImageStorageBehavior() { }
    
    public void OnDownloadComplete(in CompletedDownload download)
    {
        Singleton<UiImageStorage>.Instance.OnImageDownloaded(download);
    }
}
#endif

internal interface IImageStorageBehavior
{
    void OnDownloadComplete(in CompletedDownload download);
}