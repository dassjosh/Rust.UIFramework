using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(NeedsMouseComponentSerializer))]
public class NeedsMouseComponent : SubComponent
{
    public override Utf8String Type => JsonDefaults.Common.NeedsCursorValue;
    public override ComponentType ComponentType => ComponentType.NeedsMouse;
    public override bool AllowMultiple => false;
}