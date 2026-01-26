using Oxide.Ext.UiFramework.Cache;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;

[ProtoContract]
public readonly record struct ImageId([property: ProtoMember(1)] uint Id)
{
    public bool IsValid => Id > 0;
    public override string ToString() => IsValid ? StringCache<uint>.ToString(Id) : "Invalid ImageId";
    public static implicit operator string(ImageId id) => id.ToString();
}