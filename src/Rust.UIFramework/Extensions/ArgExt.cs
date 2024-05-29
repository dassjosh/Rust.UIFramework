using System;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

//Define:ExtensionMethods
public static class ArgExt
{
    public static DateTime GetDateTime(this ConsoleSystem.Arg arg, int iArg, DateTime def)
    {
        string s = arg.GetString(iArg, null);
        if (string.IsNullOrEmpty(s))
        {
            return def;
        }
            
        DateTime date = DateTime.Parse(s);
        return date;
    }

    public static UiReference GetParentReference(this ConsoleSystem.Arg arg, int iArg)
    {
        string s = arg.GetString(iArg);
        return new UiReference(s, null);
    }
}