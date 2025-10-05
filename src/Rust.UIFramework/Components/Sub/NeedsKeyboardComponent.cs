using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(NeedsKeyboardComponentSerializer))]
public class NeedsKeyboardComponent : SubComponent
{
    public override Utf8String Type => JsonDefaults.Common.NeedsKeyboardValue;
    public override ComponentType ComponentType => ComponentType.NeedsKeyboard;
    public override bool AllowMultiple => false;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode) { }
    
    public override bool HasChanged() => false;
}