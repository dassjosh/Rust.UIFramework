using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Libraries;

public class RegisteredFailedException(RegisterImageErrorCode errorCode) : BaseImageStorageException($"Register failed with error code: {errorCode}")
{
    public override FailureType FailureType => FailureType.RegisterFailed;
    public RegisterImageErrorCode ErrorCode { get; init; } = errorCode;
}