using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(DirectionalLayoutComponentSerializer<HorizontalLayoutComponent>))]
public class HorizontalLayoutComponent : BaseDirectionalLayoutComponent
{
    public override Utf8String Type => JsonDefaults.DirectionalLayout.HorizontalType;
    public override ComponentType ComponentType => ComponentType.HorizontalLayout;
}