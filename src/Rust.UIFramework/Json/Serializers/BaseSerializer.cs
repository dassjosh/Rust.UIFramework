using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public abstract class BaseSerializer<T> : IUiFrameworkSerializer<T> where T : class, new()
{
    private readonly T _defaults = new();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(JsonFrameworkWriter writer, object component, object defaults, SerializeMode mode) => Serialize(writer, (T)component, (T)defaults ?? _defaults, mode);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void Serialize(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode);
}