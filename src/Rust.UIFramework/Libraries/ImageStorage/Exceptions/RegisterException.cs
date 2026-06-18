using System;

namespace Oxide.Ext.UiFramework.Libraries;

public class RegisterException(Exception exception) : BaseImageStorageException($"An exception occurred: {exception}", exception)
{
    public override FailureType FailureType => FailureType.Exception;
}