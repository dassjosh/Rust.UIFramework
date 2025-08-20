using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public class DefaultNamingStrategy : INamingStrategy, ISingleton
{
    private DefaultNamingStrategy() { }
    
    public UiReference GetComponentName(in UiReference reference, UpdateMode updateMode, string baseName, int index)
    {
        switch (updateMode)
        {
            case UpdateMode.None:
                return reference.WithChild(UiNameCache.GetComponentName(baseName, index));
            case UpdateMode.AutoDestroy:
            case UpdateMode.Update:
                return reference;
            default:
                throw new ArgumentOutOfRangeException(nameof(updateMode), updateMode, null);
        }
    }

    public UiReference GetAnchorName(in UiReference reference, UpdateMode updateMode, string baseName, int index)
    {
        switch (updateMode)
        {
            case UpdateMode.None:
                return reference.WithChild(UiNameCache.GetAnchorName(baseName, index));
            case UpdateMode.AutoDestroy:
            case UpdateMode.Update:
                return reference;
            default:
                throw new ArgumentOutOfRangeException(nameof(updateMode), updateMode, null);
        }
    }
}