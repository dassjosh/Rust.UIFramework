using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Libraries;

public record RegisterFailedEventArgs(RegisterImageErrorCode ErrorCode) : IRegisterImageFailureResult
{
    public FailureType FailureType => FailureType.RegisterFailed;
    public string Message => field ??= $"Register failed with error code: {ErrorCode}";
}