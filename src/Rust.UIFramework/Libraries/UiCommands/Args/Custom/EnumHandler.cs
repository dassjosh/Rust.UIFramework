using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class EnumHandler<T> : IArgHandler<T>
{
    private readonly bool _isUnsigned;
    private readonly Type _enumType = typeof(T);

    public EnumHandler()
    {
        Type underlyingType = Enum.GetUnderlyingType(typeof(T));
        _isUnsigned = underlyingType.IsUnsigned();
    }
    
    public T Read(in UiStringView view) => _isUnsigned ? (T)Enum.ToObject(_enumType, ulong.Parse(view)) : (T)Enum.ToObject(_enumType, long.Parse(view));

    public void Write(UiArgWriter writer, T arg)
    {
        switch (TypeCache<T>.TypeCode)
        {
            case TypeCode.SByte:
                writer.Append(Unsafe.As<T, sbyte>(ref arg));
                return;
            case TypeCode.Byte:
                writer.Append(Unsafe.As<T, byte>(ref arg));
                return;
            case TypeCode.Int16:
                writer.Append(Unsafe.As<T, short>(ref arg));
                return;
            case TypeCode.UInt16:
                writer.Append(Unsafe.As<T, ushort>(ref arg));
                return;
            case TypeCode.Int32:
                writer.Append(Unsafe.As<T, int>(ref arg));
                return;
            case TypeCode.UInt32:
                writer.Append(Unsafe.As<T, uint>(ref arg));
                return;
            case TypeCode.Int64:
                writer.Append(Unsafe.As<T, long>(ref arg));
                return;
            case TypeCode.UInt64:
                writer.Append(Unsafe.As<T, ulong>(ref arg));
                return;
        }
        
        writer.Append(0);
    }
}