using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsValid(PluginId id, [CallerArgumentExpression(nameof(id))] string name = null)
    {
        if (!id.IsValid) throw new ArgumentException(Message($"'{name}' is not a valid {nameof(PluginId)}."), name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsValid(ImageId id, [CallerArgumentExpression(nameof(id))] string name = null)
    {
        if (!id.IsValid) throw new ArgumentException(Message($"'{name}' is not a valid {nameof(ImageId)}."), name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsValidUrl(string url, [CallerArgumentExpression(nameof(url))] string name = null)
    {
        if (!url.IsValidUrl()) throw new ArgumentException(Message($"'{name}' is not a valid url '{url}'."), name);
    }
}