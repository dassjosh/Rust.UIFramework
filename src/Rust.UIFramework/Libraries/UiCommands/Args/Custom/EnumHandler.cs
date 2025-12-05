using System;
using Oxide.Ext.UiFramework.Cache;

namespace Oxide.Ext.UiFramework.Libraries;

internal class EnumHandler<T> : IArgHandler<T>
{
    private readonly bool _isUnsigned;
    private readonly Type _enumType = typeof(T);

    public EnumHandler()
    {
        Type underlyingType = Enum.GetUnderlyingType(typeof(T));
        _isUnsigned = underlyingType == typeof(byte) || underlyingType == typeof(ushort) || underlyingType == typeof(uint) || underlyingType == typeof(ulong);
    }
    
    public T Read(ReadOnlySpan<char> arg) => _isUnsigned ? (T)Enum.ToObject(_enumType, ulong.Parse(arg)) : (T)Enum.ToObject(_enumType, long.Parse(arg));

    public void Write(UiArgWriter writer, T arg) => writer.Append(InternalEnumCache<T>.ToNumber(arg));
}