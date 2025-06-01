using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

public static class UiFrameworkPool
{
    private static UiFrameworkPluginPool Pool = Singleton<UiFrameworkPoolLib>.Instance.Obsolete;
    
    /// <summary>
    /// Returns a pooled object of type T
    /// Must inherit from <see cref="BasePoolable"/> and have an empty default constructor
    /// </summary>
    /// <typeparam name="T">Type to be returned</typeparam>
    /// <returns>Pooled object of type T</returns>
    [Obsolete]
    public static T Get<T>() where T : BasePoolable, new()
    {
        return Pool.Get<T>();
    }

    /// <summary>
    /// Returns a <see cref="BasePoolable"/> back into the pool
    /// </summary>
    /// <param name="value">Object to free</param>
    /// <typeparam name="T">Type of object being freed</typeparam>
    [Obsolete]
    internal static void Free<T>(T value) where T : BasePoolable, new()
    {
        Pool.Free(value);
    }

    /// <summary>
    /// Returns a pooled <see cref="List{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the list</typeparam>
    /// <returns>Pooled List</returns>
    [Obsolete]
    public static List<T> GetList<T>()
    {
        return Pool.GetList<T>();
    }

    /// <summary>
    /// Returns a pooled <see cref="Hash{TKey, TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled Hash</returns>
    [Obsolete]
    public static Hash<TKey, TValue> GetHash<TKey, TValue>()
    {
        return Pool.GetHash<TKey, TValue>();
    }

    /// <summary>
    /// Returns a pooled <see cref="StringBuilder"/>
    /// </summary>
    /// <returns>Pooled <see cref="StringBuilder"/></returns>
    [Obsolete]
    public static StringBuilder GetStringBuilder()
    {
        return Pool.GetStringBuilder();
    }

    /// <summary>
    /// Free's a pooled <see cref="List{T}"/>
    /// </summary>
    /// <param name="list">List to be freed</param>
    /// <typeparam name="T">Type of the list</typeparam>
    [Obsolete]
    public static void FreeList<T>(List<T> list)
    {
        Pool.FreeList(list);
    }

    /// <summary>
    /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
    /// </summary>
    /// <param name="hash">Hash to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    [Obsolete]
    public static void FreeHash<TKey, TValue>(Hash<TKey, TValue> hash)
    {
        Pool.FreeHash(hash);
    }

    /// <summary>
    /// Frees a <see cref="StringBuilder"/> back to the pool
    /// </summary>
    /// <param name="sb">StringBuilder being freed</param>
    [Obsolete]
    public static void FreeStringBuilder(StringBuilder sb)
    {
        Pool.FreeStringBuilder(sb);
    }
}