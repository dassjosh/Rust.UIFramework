using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public readonly record struct RequestId()
{
    public readonly int Id = IdGen<RequestId>.GetNextId();
    public bool IsValid => Id != 0;
    public override string ToString() => Id.ToString();
}