namespace Oxide.Ext.UiFramework.Libraries;

public interface IRegisterImageException
{
    FailureType FailureType { get; }
    string Message { get; }
}