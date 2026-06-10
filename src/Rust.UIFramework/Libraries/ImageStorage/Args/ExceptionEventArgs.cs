using System;

namespace Oxide.Ext.UiFramework.Libraries;

public record ExceptionEventArgs(Exception Exception) : IRegisterImageFailureResult
{
    public FailureType FailureType => FailureType.Exception;
    public string Message => field ??= $"An exception occurred: {Exception}";
}