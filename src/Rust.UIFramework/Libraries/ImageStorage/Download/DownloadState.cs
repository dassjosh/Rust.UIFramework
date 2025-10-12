namespace Oxide.Ext.UiFramework.Libraries;

public enum DownloadState : byte
{
    Queued,
    InProgress,
    Completed,
    Failed,
    Stored
}