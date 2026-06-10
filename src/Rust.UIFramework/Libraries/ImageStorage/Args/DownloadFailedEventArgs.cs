using System.Net;

namespace Oxide.Ext.UiFramework.Libraries;

public record DownloadFailedEventArgs(HttpStatusCode Code, string ResponseMessage) : IRegisterImageFailureResult
{
    public FailureType FailureType => FailureType.DownloadFailed;

    public string Message => field ??= $"HTTP Error {Code}: {ResponseMessage}";
}