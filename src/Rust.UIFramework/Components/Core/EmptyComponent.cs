using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class EmptyComponent : CoreComponent
{
    public override Utf8String Type => throw new NotSupportedException();
    public override ComponentType ComponentType => ComponentType.Empty;

    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        // Do not serialize Empty component
    }

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode) => throw new NotSupportedException();
}