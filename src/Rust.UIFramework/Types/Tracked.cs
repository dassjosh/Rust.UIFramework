using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Types;

public class Tracked<T> : ITracked<T>, ITracked
{
    private T _value;
    private readonly T _frameworkDefault;
    private readonly T _serializationDefault;

    /// <summary>
    /// Gets a value indicating whether the Value property has been set
    /// since initialization or the last call to Reset().
    /// </summary>
    /// 
    public bool HasChanged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get; 
        private set;
    }

    public bool IsFrameworkDefault
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => EqualityComparer<T>.Default.Equals(_value, _frameworkDefault);
    }
    
    public bool IsSerializationDefault
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => EqualityComparer<T>.Default.Equals(_value, _serializationDefault);
    }

    /// <summary>
    /// Gets or sets the underlying value. When set, the HasChanged
    /// property is updated to true.
    /// </summary>
    public T Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _value = value;
            HasChanged = true;
        }
    }
    
    /// <summary>
    /// Initializes a new instance of the TrackedValue class with a specified value.
    /// </summary>
    /// <param name="frameworkDefault">The default value to assign on Reset().</param>
    /// <param name="serializationDefault">The default value for CUI serialization.</param>
    public Tracked(T frameworkDefault, T serializationDefault)
    {
        _frameworkDefault = frameworkDefault;
        _serializationDefault = serializationDefault;
        _value = frameworkDefault;
        HasChanged = false;
    }

    /// <summary>
    /// Initializes a new instance of the TrackedValue class with a specified value.
    /// </summary>
    /// <param name="defaultValue">The default value to assign.</param>
    public Tracked(T defaultValue)
    {
        _serializationDefault = _frameworkDefault = _value = defaultValue;
        HasChanged = false;
    }
        
    /// <summary>
    /// Initializes a new instance of the TrackedValue class with the default value for the type T.
    /// </summary>
    public Tracked()
    {
        _value = default;
        HasChanged = false;
    }

    public void UpdateIfChanged(T value)
    {
        if (!EqualityComparer<T>.Default.Equals(_value, value))
        {
            Value = value;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ShouldSerialize(SerializeMode mode)
    {
        switch (mode)
        {
            case SerializeMode.Create:
                return !IsSerializationDefault;
            case SerializeMode.Update:
                return HasChanged;
            default:
                return false;
        }
    }
    
    /// <summary>
    /// Resets the HasChanged flag back to false.
    /// </summary>
    public void ResetHasChanged()
    {
        HasChanged = false;
    }

    public void Reset()
    {
        HasChanged = false;
        _value = _frameworkDefault;
    }
}

public interface ITracked<T>
{
    T Value { get; set; }
}

public interface ITracked
{
    bool HasChanged { get; }
    bool IsFrameworkDefault { get; }
    bool IsSerializationDefault { get; }
}