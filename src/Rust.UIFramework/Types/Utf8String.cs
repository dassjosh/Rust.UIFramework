using System.Diagnostics;
using System.Text;

namespace Oxide.Ext.UiFramework.Types;

//While this looks stupid. It's faster to take in the strings and convert them to an array and store them in this struct than to use a ref struct using a ReadOnlySpan<byte>
[DebuggerDisplay("{ToString()}")]
public readonly struct Utf8String(byte[] str)
{
    internal readonly byte[] String = str;

    public override string ToString() => Encoding.UTF8.GetString(String);
    public static implicit operator Utf8String(string str) => new(Encoding.UTF8.GetBytes(str));
    public static implicit operator Utf8String(char value) => value.ToString();
}