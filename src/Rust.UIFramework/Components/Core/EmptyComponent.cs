using System;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class EmptyComponent : CoreComponent
{
#if UNIT_TESTS
    public override Utf8String Type => default;
#else
    public override Utf8String Type => throw new NotSupportedException();
#endif
    protected override void WriteComponentFields(JsonFrameworkWriter writer) => throw new NotSupportedException();
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        //We don't send any UI for an empty component
    }
}