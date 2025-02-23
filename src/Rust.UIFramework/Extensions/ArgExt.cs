using System;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

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
    
    public static T GetEnum<T>(this ConsoleSystem.Arg arg, int iArg, T def = default) where T : struct, Enum
    {
        string enumString = arg.GetString(iArg, null);
        if (string.IsNullOrEmpty(enumString))
        {
            return def;
        }
            
        return Enum.Parse<T>(enumString);
    }
    
    public static bool TryGetEnum<T>(this ConsoleSystem.Arg arg, int iArg, out T value) where T : struct, Enum
    {
        string enumString = arg.GetString(iArg, null);
        return Enum.TryParse(enumString, out value);
    }
}