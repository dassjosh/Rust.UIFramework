using System;
using Oxide.Ext.UiFramework.Exceptions;

namespace Oxide.Ext.UiFramework.Libraries;

public abstract class BaseImageStorageException(string message, Exception inner = null) : BaseUiFrameworkException(message, inner), IRegisterImageException
{
    public abstract FailureType FailureType { get; }
}