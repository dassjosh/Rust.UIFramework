using System;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Cache;

public class TypeCache<T>
{
    public static readonly TypeCode TypeCode = typeof(T).GetTypeCode();
}