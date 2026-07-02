using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Facepunch;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;

namespace Oxide.Ext.UiFramework.Types;

[DebuggerDisplay("{ToString()}")]
public readonly struct UiStringView(string value, int start, int length) : IEquatable<UiStringView>
{
    public readonly string Value = value;
    public readonly int Start = start;
    public readonly int Length = length;

    public UiStringView(string value) : this(value, 0, value.Length) { }
    public UiStringView(string value, int start) : this(value, start, value.Length - start) { }
    public UiStringView() : this(string.Empty) { }

    public static UiStringView Empty => new(string.Empty);

    public char this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Guard.InRange(index, 0, Length);
            return Value[Start + index];
        }
    }

    public char this[Index index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this[index.GetOffset(Length)];
    }

    public UiStringView this[Range range]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            int length = Length;
            int start = range.Start.GetOffset(length);
            int end = range.End.GetOffset(length);
            Guard.InRange(start, 0, length);
            Guard.InRange(end, 0, length);
            return new UiStringView(Value, Start + start, end - start);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan() => Value.AsSpan(Start, Length);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlyMemory<char> AsMemory() => Value.AsMemory(Start, Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlySpan<char>(UiStringView view) => view.AsSpan();
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlyMemory<char>(UiStringView view) => view.AsMemory();
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator StringView(UiStringView view) => new(view.Value, view.Start, view.Length);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(UiStringView view) => view.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UiStringView left, UiStringView right) => left.Equals(right);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UiStringView left, UiStringView right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UiStringView other) => AsSpan().SequenceEqual(other.AsSpan());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj) => obj is UiStringView other && Equals(other);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => AsSpan().GetSpanHashCode();

    public override string ToString() => Value.Substring(Start, Length);
}