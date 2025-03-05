using System;
using Oxide.Ext.UiFramework.Cache;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class EnumHandler<T> : IArgHandler<T>
{
    private readonly bool _isUnsigned;

    public EnumHandler()
    {
        Type underlyingType = Enum.GetUnderlyingType(typeof(T));
        _isUnsigned = underlyingType == typeof(sbyte) || underlyingType == typeof(ushort) || underlyingType == typeof(uint) || underlyingType == typeof(ulong);
    }
    
    public T Read(ReadOnlySpan<char> arg) => _isUnsigned ? (T)Enum.ToObject(typeof(T), ulong.Parse(arg)) : (T)Enum.ToObject(typeof(T), long.Parse(arg));

    public void Write(UiArgWriter writer, T arg) => writer.Append(InternalEnumCache<T>.ToNumber(arg));
}