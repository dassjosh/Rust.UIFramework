using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Types;

public class TrackedValue<T> : ITrackedValue
{
    private T _value;
    private T _defaultValue;

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

    public bool IsDefault
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => EqualityComparer<T>.Default.Equals(_value, _defaultValue);
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
    /// <param name="defaultValue">The default value to assign.</param>
    public TrackedValue(T defaultValue)
    {
        _defaultValue = _value = defaultValue;
        HasChanged = false;
    }
        
    /// <summary>
    /// Initializes a new instance of the TrackedValue class with the default value for the type T.
    /// </summary>
    public TrackedValue()
    {
        _value = default;
        HasChanged = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ShouldSerialize(SerializeMode mode)
    {
        switch (mode)
        {
            case SerializeMode.Create:
                return !IsDefault;
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

    public void OverrideDefault(T @default)
    {
        _defaultValue = @default;
    }

    public void Reset()
    {
        HasChanged = false;
        _value = _defaultValue;
    }
}

public interface ITrackedValue
{
    bool HasChanged { get; }
    bool IsDefault { get; }
}