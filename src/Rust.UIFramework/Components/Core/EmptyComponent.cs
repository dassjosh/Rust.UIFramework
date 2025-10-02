using System;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(EmptyComponentSerializer))]
public class EmptyComponent : CoreComponent
{
    public override Utf8String Type => throw new NotSupportedException();
    public override ComponentType ComponentType => ComponentType.Empty;
}