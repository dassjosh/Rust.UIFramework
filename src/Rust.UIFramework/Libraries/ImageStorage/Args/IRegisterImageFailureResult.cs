namespace Oxide.Ext.UiFramework.Libraries;

public interface IRegisterImageFailureResult
{
    FailureType FailureType { get; }
    string Message { get; }
}