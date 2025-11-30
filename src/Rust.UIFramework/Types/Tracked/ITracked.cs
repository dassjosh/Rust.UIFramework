namespace Oxide.Ext.UiFramework.Types;

public interface ITracked
{
    bool HasChanged { get; }
    bool IsDefaultValue { get; }
    bool IsSerializationDefault { get; }
}