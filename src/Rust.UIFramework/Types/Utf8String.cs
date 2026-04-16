using System;
using System.Buffers.Text;
using System.Diagnostics;
using System.Text;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Types;

//While this looks stupid. It's faster to take in the strings and convert them to an array and store them in this struct than to use a ref struct using a ReadOnlySpan<byte>
[DebuggerDisplay("{ToString()}")]
public readonly struct Utf8String(byte[] str) : IEquatable<Utf8String>
{
    internal static readonly Utf8String Empty = new([]); 
    
    internal readonly byte[] String = str;

    public override string ToString() => Encoding.UTF8.GetString(String);
    public static implicit operator Utf8String(string str) => new(Encoding.UTF8.GetBytes(str));

    public bool Equals(Utf8String other) => String != null && other.String != null && String.SequenceEqual(other.String);
    public override bool Equals(object obj) => obj is Utf8String other && Equals(other);
    public override int GetHashCode() => String != null ? String.GetHashCode() : 0;
    
    public static bool operator ==(Utf8String left, Utf8String right) => left.Equals(right);
    public static bool operator !=(Utf8String left, Utf8String right) => !(left == right);

    public static Utf8String FromChar(char value)
    {
        int size = Utf8Formatter.GetCharSize(value);
        byte[] str = new byte[size];
        Utf8Formatter.FormatChar(value, str);
        return new Utf8String(str);
    }
}