using Oxide.Ext.UiFramework.Cache;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Libraries;

[ProtoContract]
public readonly record struct ImageId([property: ProtoMember(1)] uint Id)
{
    public bool IsValid => Id > 0;
    public override string ToString() => IsValid ? StringCache<uint>.ToString(Id) : "Invalid ImageId";
    public static implicit operator string(ImageId id) => id.ToString();
    public static implicit operator ImageId(string id) => uint.TryParse(id, out uint imageId) ? new ImageId(imageId) : default;

    public static bool TryParse(string image, out ImageId id)
    {
        if (uint.TryParse(image, out uint imageId) && imageId != 0)
        {
            id = new ImageId(imageId);
            return true;
        }

        id = default;
        return false;
    }
}