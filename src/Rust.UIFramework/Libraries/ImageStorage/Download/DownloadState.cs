namespace Oxide.Ext.UiFramework.Libraries;

public enum DownloadState : byte
{
    Init,
    Queued,
    InProgress,
    Completed,
    Failed,
    Errored,
    Stored
}