namespace Oxide.Ext.UiFramework.Types;

public readonly record struct CodePoint(int Value)
{
    public CodePoint(char value) : this((int)value) { }
    public CodePoint(char high, char low) : this(char.ConvertToUtf32(high, low)) { }
    public override string ToString() => char.ConvertFromUtf32(Value);
}