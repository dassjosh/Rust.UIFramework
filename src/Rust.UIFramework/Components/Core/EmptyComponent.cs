using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class EmptyComponent : CoreComponent
{
    public override Utf8String Type => throw new NotSupportedException();
}