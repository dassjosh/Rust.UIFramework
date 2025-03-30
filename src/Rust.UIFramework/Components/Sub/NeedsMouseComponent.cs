using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class NeedsMouseComponent : SubComponent
{
    public override Utf8String Type => JsonDefaults.Common.NeedsCursorValue;
    public override bool AllowMultiple => false;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer) { }
}