using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public class DefaultNamingStrategy : INamingStrategy, ISingleton
{
    private DefaultNamingStrategy() { }
    
    public void SetComponentName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, string rootName, int elementNum)
    {
        switch (namingMode)
        {
            case NamingMode.Child:
                UiReferenceException.ThrowIfInValidRootName(rootName);
                component.Reference = reference.WithChild(UiNameCache.GetComponentName(rootName, elementNum));
                break;
            case NamingMode.Reference:
                component.Reference = reference;
                break;
        }
    }

    public void SetAnchorName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, string rootName, int elementNum)
    {
        switch (namingMode)
        {
            case NamingMode.Child:
                UiReferenceException.ThrowIfInValidRootName(rootName);
                component.Reference = reference.WithChild(UiNameCache.GetAnchorName(rootName, elementNum));
                break;
            case NamingMode.Reference:
                component.Reference = reference;
                break;
        }
    }
}