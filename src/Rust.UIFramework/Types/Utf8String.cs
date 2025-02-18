using System;
using System.Text;

namespace Oxide.Ext.UiFramework.Types;

public readonly struct Utf8String(byte[] str)
{
    internal readonly byte[] String = str;

    public override string ToString() => Encoding.UTF8.GetString(String);

    //While this looks stupid. It's faster to take in the u8 strings and convert them to an array and store them in this struct than to use a ref struct using a ReadOnlySpan<byte>
    public static implicit operator Utf8String(ReadOnlySpan<byte> utf8String) => new(utf8String.ToArray());
    public static implicit operator Utf8String(string str) => new(Encoding.UTF8.GetBytes(str));
}