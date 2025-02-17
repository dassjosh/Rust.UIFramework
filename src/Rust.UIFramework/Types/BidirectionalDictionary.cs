using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Oxide.Ext.UiFramework.Types;

/// <summary>
/// A Dictionary that stores the keys -> values and values -> keys
/// </summary>
/// <typeparam name="TKey">Type of the key</typeparam>
/// <typeparam name="TValue">Type of the value</typeparam>
internal class BidirectionalDictionary<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> _keyToValue = new();
    private readonly Dictionary<TValue, TKey> _valueToKey = new();

    /// <summary>
    /// Count of the 
    /// </summary>
    public int Count => Math.Min(_keyToValue.Count, _valueToKey.Count);

    /// <summary>
    /// Returns true of the dictionary contains the given key
    /// </summary>
    /// <param name="key">Key to check for</param>
    /// <returns></returns>
    public bool ContainsKey(TKey key) => _keyToValue.ContainsKey(key);

    /// <summary>
    /// Returns true of the dictionary contains the given key
    /// </summary>
    /// <param name="key">Key to check for</param>
    /// <returns></returns>
    public bool ContainsKey(TValue key) => _valueToKey.ContainsKey(key);
        
    public bool TryGetValue(TKey key, out TValue value) => _keyToValue.TryGetValue(key, out value);

    public bool TryGetValue(TValue key, out TKey value) => _valueToKey.TryGetValue(key, out value);

    public TValue this[TKey key]
    {
        get => _keyToValue[key];
        set
        {
            _keyToValue[key] = value;
            _valueToKey[value] = key;
        }
    }
        
    public TKey this[TValue key]
    {
        get => _valueToKey[key];
        set
        {
            _valueToKey[key] = value;
            _keyToValue[value] = key;
        }
    }

    public void Add(TKey key, TValue value)
    {
        _valueToKey.Add(value, key);
        _keyToValue.Add(key, value);
    }
        
    public void Add(TValue key, TKey value)
    {
        _keyToValue.Add(value, key);
        _valueToKey.Add(key, value);
    }

    public void AddKey(TKey key, TValue value)
    {
        _keyToValue[key] = value;
    }
        
    public void AddValue(TValue key, TKey value)
    {
        _valueToKey[key] = value;
    }

    public bool Remove(TKey key)
    {
        if (!_keyToValue.Remove(key, out TValue valueKey))
        {
            return false;
        }

        _valueToKey.Remove(valueKey, out TKey _);

        return true;
    }
        
    public bool Remove(TValue key)
    {
        if (!_valueToKey.Remove(key, out TKey valueKey))
        {
            return false;
        }

        _keyToValue.Remove(valueKey, out TValue _);

        return true;
    }

    public void Clear()
    {
        _keyToValue.Clear();
        _valueToKey.Clear();
    }

    public ReadOnlyDictionary<TKey, TValue> AsReadOnlyKeyToValue() => new(_keyToValue);
    public ReadOnlyDictionary<TValue, TKey> AsReadOnlyValueToKey() => new(_valueToKey);

    public ICollection<TKey> AsKeyCollection() => _keyToValue.Keys;
    public ICollection<TValue> AsValueCollection() => _valueToKey.Keys;
}