using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public class DefaultNamingStrategy : INamingStrategy, ISingleton
{
    private DefaultNamingStrategy() { }
    
    public void SetComponentName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, INamingCache cache, int elementNum)
    {
        switch (namingMode)
        {
            case NamingMode.Child:
                component.Reference = reference.WithChild(cache.GetComponentName(elementNum));
                break;
            case NamingMode.Reference:
                component.Reference = reference;
                break;
        }
    }

    public void SetAnchorName(BaseUiComponent component, in UiReference reference, NamingMode namingMode, INamingCache cache, int elementNum)
    {
        switch (namingMode)
        {
            case NamingMode.Child:
                component.Reference = reference.WithChild(cache.GetAnchorName(elementNum));
                break;
            case NamingMode.Reference:
                component.Reference = reference;
                break;
        }
    }
}