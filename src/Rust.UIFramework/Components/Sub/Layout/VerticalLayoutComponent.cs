using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(DirectionalLayoutComponentSerializer<VerticalLayoutComponent>))]
public class VerticalLayoutComponent : BaseDirectionalLayoutComponent
{
    public override Utf8String Type => JsonDefaults.DirectionalLayout.VerticalType;
    public override ComponentType ComponentType => ComponentType.VerticalLayout;
}