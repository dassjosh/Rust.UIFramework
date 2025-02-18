using System;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Exceptions.UiCommands;

public class NoArgHandlerException(Type type) : BaseUiFrameworkException($"No ArgHandler was found for type: {type.GetRealTypeName()}. Did you forget to register it?");