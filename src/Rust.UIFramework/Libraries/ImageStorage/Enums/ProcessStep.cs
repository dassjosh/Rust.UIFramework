namespace Oxide.Ext.UiFramework.Libraries;

public enum ProcessStep : byte
{
    Init,
    Download,
    Process,
    Store,
    Save,
    Completed,
    Failed
}