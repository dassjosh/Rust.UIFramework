using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

public static class UiFrameworkPool
{
    internal static readonly UiPluginPool Pool = Singleton<UiPool>.Instance.CreateObsoletePool();
    
    /// <summary>
    /// Returns a pooled object of type T
    /// Must inherit from <see cref="BasePoolable"/> and have an empty default constructor
    /// </summary>
    /// <typeparam name="T">Type to be returned</typeparam>
    /// <returns>Pooled object of type T</returns>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    public static T Get<T>() where T : BasePoolable, new() => Pool.Get<T>();

    /// <summary>
    /// Returns a <see cref="BasePoolable"/> back into the pool
    /// </summary>
    /// <param name="value">Object to free</param>
    /// <typeparam name="T">Type of object being freed</typeparam>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    internal static void Free<T>(T value) where T : BasePoolable, new() => Pool.Free(value);

    /// <summary>
    /// Returns a pooled <see cref="List{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the list</typeparam>
    /// <returns>Pooled List</returns>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    public static List<T> GetList<T>() => Pool.GetList<T>();

    /// <summary>
    /// Free's a pooled <see cref="List{T}"/>
    /// </summary>
    /// <param name="list">List to be freed</param>
    /// <typeparam name="T">Type of the list</typeparam>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    public static void FreeList<T>(List<T> list) => Pool.FreeList(list);

    /// <summary>
    /// Returns a pooled <see cref="Hash{TKey, TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled Hash</returns>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    public static Hash<TKey, TValue> GetHash<TKey, TValue>() => Pool.GetHash<TKey, TValue>();

    /// <summary>
    /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
    /// </summary>
    /// <param name="hash">Hash to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    public static void FreeHash<TKey, TValue>(Hash<TKey, TValue> hash) => Pool.FreeHash(hash);

    /// <summary>
    /// Returns a pooled <see cref="StringBuilder"/>
    /// </summary>
    /// <returns>Pooled <see cref="StringBuilder"/></returns>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    public static StringBuilder GetStringBuilder() => Pool.GetStringBuilder();

    /// <summary>
    /// Frees a <see cref="StringBuilder"/> back to the pool
    /// </summary>
    /// <param name="sb">StringBuilder being freed</param>
    [Obsolete($"Implement {nameof(IUiFrameworkPlugin)} interface on your plugin and use the provided pool from there.")]
    public static void FreeStringBuilder(StringBuilder sb) => Pool.FreeStringBuilder(sb);
}