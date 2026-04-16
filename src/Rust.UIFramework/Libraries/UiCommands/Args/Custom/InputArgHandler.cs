using System;

namespace Oxide.Ext.UiFramework.Libraries;

public readonly struct InputArg(string value)
{
    public readonly string Value = value;
    public bool IsValid => !string.IsNullOrEmpty(Value);
    public static InputArg Empty => new(null);
    
    public bool TryGetValue(out byte value) => byte.TryParse(Value, out value);
    public bool TryGetValue(out sbyte value) => sbyte.TryParse(Value, out value);
    public bool TryGetValue(out short value) => short.TryParse(Value, out value);
    public bool TryGetValue(out ushort value) => ushort.TryParse(Value, out value);
    public bool TryGetValue(out int value) => int.TryParse(Value, out value);
    public bool TryGetValue(out uint value) => uint.TryParse(Value, out value);
    public bool TryGetValue(out long value) => long.TryParse(Value, out value);
    public bool TryGetValue(out ulong value) => ulong.TryParse(Value, out value);
    public bool TryGetValue(out float value) => float.TryParse(Value, out value);
    public bool TryGetValue(out double value) => double.TryParse(Value, out value);
    public bool TryGetValue(out decimal value) => decimal.TryParse(Value, out value);
    public bool TryGetValue(out bool value) => bool.TryParse(Value, out value);
    public bool TryGetValue(out char value) => char.TryParse(Value, out value);
}

internal class InputArgHandler : IArgHandler<InputArg>
{
    public InputArg Read(ReadOnlySpan<char> arg) => new(arg.ToString());
    public void Write(UiArgWriter writer, InputArg arg) => throw new NotSupportedException();
    public bool IsInputArg() => true;
}