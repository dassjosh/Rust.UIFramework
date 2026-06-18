using System.Net;

namespace Oxide.Ext.UiFramework.Libraries;

public class DownloadFailedException(HttpStatusCode code, string responseMessage) : BaseImageStorageException($"HTTP Error {code}: {responseMessage}"), IRegisterImageException
{
    public override FailureType FailureType => FailureType.DownloadFailed;
    public HttpStatusCode Code { get; init; } = code;
    public string ResponseMessage { get; init; } = responseMessage;
}