using System;
using System.Runtime.CompilerServices;
using Facepunch;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public readonly struct InputArg(UiStringView view)
{
    public UiStringView Value { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; } = view;
    public bool HasValue => Value.Length != 0;
    public static InputArg Empty => new(UiStringView.Empty);
    
    public bool TryGetValue(out byte value) => byte.TryParse(AsSpan(), out value);
    public bool TryGetValue(out sbyte value) => sbyte.TryParse(AsSpan(), out value);
    public bool TryGetValue(out short value) => short.TryParse(AsSpan(), out value);
    public bool TryGetValue(out ushort value) => ushort.TryParse(AsSpan(), out value);
    public bool TryGetValue(out int value) => int.TryParse(AsSpan(), out value);
    public bool TryGetValue(out uint value) => uint.TryParse(AsSpan(), out value);
    public bool TryGetValue(out long value) => long.TryParse(AsSpan(), out value);
    public bool TryGetValue(out ulong value) => ulong.TryParse(AsSpan(), out value);
    public bool TryGetValue(out float value) => float.TryParse(AsSpan(), out value);
    public bool TryGetValue(out double value) => double.TryParse(AsSpan(), out value);
    public bool TryGetValue(out decimal value) => decimal.TryParse(AsSpan(), out value);
    public bool TryGetValue(out bool value) => bool.TryParse(AsSpan(), out value);
    public bool TryGetValue(out char value) => char.TryParse(AsSpan(), out value);
    public bool TryGetValue(out UiColor value) => UiColor.TryParse(AsSpan(), out value);

    public ReadOnlySpan<char> AsSpan() => Value.AsSpan();
    public StringView AsView() => Value;
    public override string ToString() => Value.ToString();
}

internal class InputArgHandler : IArgHandler<InputArg>
{
    public InputArg Read(in UiStringView view) => new(view);
    public void Write(UiArgWriter writer, InputArg arg) => throw new NotSupportedException();
    public bool IsInputArg() => true;
}