namespace Oxide.Ext.UiFramework.Libraries;

public enum ProcessStep : byte
{
    Init,
    Download,
    Generate,
    Process,
    Store,
    Save,
    Completed,
    Failed
}