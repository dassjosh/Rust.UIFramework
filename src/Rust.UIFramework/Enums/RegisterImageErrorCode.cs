namespace Oxide.Ext.UiFramework.Enums;

public enum RegisterImageErrorCode : byte
{
    None,
    AlreadyRegistered,
    EmptyImage,
    InvalidImageType,
    DbStorageFailed
}