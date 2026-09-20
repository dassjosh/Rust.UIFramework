using System.Collections.Generic;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface IImageDatabase : ISingleton
{
    ImageDatabaseType Type { get; }

    bool Exists(ImageId id);
    ImageId Store(byte[] image);
    void Remove(ImageId id);
    byte[] Get(ImageId id);
    List<ImageId> GetExistingImages();
}