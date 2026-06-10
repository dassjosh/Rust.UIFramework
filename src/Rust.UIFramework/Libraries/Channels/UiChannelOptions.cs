namespace Oxide.Ext.UiFramework.Libraries;

public record UiChannelOptions(bool EnableMultithreading, int MaxConcurrency)
{
    public static readonly UiChannelOptions MainThread = new(false, 1);
}