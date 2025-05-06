using System;

namespace Oxide.Ext.UiFramework.Exceptions;

public class UiFrameworkException : Exception
{
    public UiFrameworkException(string message) : base(message) { }
}