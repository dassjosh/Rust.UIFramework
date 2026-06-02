// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a policy for managing pooled objects.
/// </summary>
/// <typeparam name="T">The type of object which is being pooled.</typeparam>
public interface IPooledObjectPolicy<T> where T : notnull
{
    /// <summary>
    /// Returns the max amount of items to be pooled at one time
    /// </summary>
    /// <param name="settings">Settings that contains pool settings for the pool</param>
    /// <returns></returns>
    int GetPoolSize(PoolSettings settings);

    /// <summary>
    /// Create a <typeparamref name="T"/>.
    /// </summary>
    /// <returns>The <typeparamref name="T"/> which was created.</returns>
    T Create();

    /// <summary>
    /// Runs some precessing when an object is retrieved from the pool.
    /// </summary>
    /// <param name="obj">The object to retrieved from the pool.</param>
    void Get(T obj);

    /// <summary>
    /// Runs some processing when an object was returned to the pool. Can be used to reset the state of an object and indicate if the object should be returned to the pool.
    /// </summary>
    /// <param name="obj">The object to return to the pool.</param>
    /// <returns><see langword="true" /> if the object should be returned to the pool. <see langword="false" /> if it's not possible/desirable for the pool to keep the object.</returns>
    bool Return(T obj);
}
