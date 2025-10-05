using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseLayoutComponent : SubComponent
{
    public TextAnchor ChildAlignment;
    public UiPadding Padding;
    public BaseUiComponent Owner { get; internal set; }
    public UiReference Reference => Owner.Reference;
    
    public override bool AllowMultiple => false;

    public override void Reset()
    {
        base.Reset();
        ChildAlignment = JsonDefaults.Layout.ChildAlignment;
        Padding = default;
        Owner = null;
    }
}