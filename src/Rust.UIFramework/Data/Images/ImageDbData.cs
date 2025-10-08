using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Libraries;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Data;

[ProtoContract]
internal class ImageDbData : BaseDataFile<ImageDbData>
{
    [ProtoMember(1)]
    private readonly ConcurrentDictionary<ImageId, DateTime> _storedImages = [];
    
    public bool IsStored(ImageId id) => _storedImages.ContainsKey(id);
    public void Touch(ImageId id)
    {
        _storedImages[id] = DateTime.UtcNow;
        OnDataChanged();
    }

    public void Remove(ImageId id)
    {
        _storedImages.TryRemove(id, out DateTime _);
        OnDataChanged();
    }

    public IEnumerable<ImageId> GetExpiredImages() => _storedImages
        .Where(x => x.Value + TimeSpan.FromDays(UiFrameworkConfig.Instance.ImageDb.UnusedImageMaxDays) < DateTime.UtcNow)
        .Select(x => x.Key);
}