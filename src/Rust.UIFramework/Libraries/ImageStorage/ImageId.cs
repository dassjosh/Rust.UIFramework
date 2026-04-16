using Oxide.Ext.UiFramework.Cache;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;

[ProtoContract]
internal readonly record struct ImageId([property: ProtoMember(1)] uint Id)
{
    public bool IsValid => Id > 0;
    public override string ToString() => StringCache<uint>.ToString(Id);
}