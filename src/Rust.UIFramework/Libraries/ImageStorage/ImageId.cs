using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;


[ProtoContract]
internal readonly record struct ImageId([property: ProtoMember(1)] string Id)
{
    public bool IsValid => !string.IsNullOrEmpty(Id);
    public bool TryGetCrc(out uint crc) => uint.TryParse(Id, out crc);
}