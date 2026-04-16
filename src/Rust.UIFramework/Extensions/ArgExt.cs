using System;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class ArgExt
{
    extension(ConsoleSystem.Arg arg)
    {
        public DateTime GetDateTime(int iArg, DateTime def)
        {
            string s = arg.GetString(iArg, null);
            if (string.IsNullOrEmpty(s))
            {
                return def;
            }
            
            DateTime date = DateTime.Parse(s);
            return date;
        }

        public UiReference GetParentReference(int iArg)
        {
            string s = arg.GetString(iArg);
            return new UiReference(s, null);
        }

        public T GetEnum<T>(int iArg, T def = default) where T : struct, Enum
        {
            string enumString = arg.GetString(iArg, null);
            if (string.IsNullOrEmpty(enumString))
            {
                return def;
            }
            
            return Enum.Parse<T>(enumString);
        }

        public bool TryGetEnum<T>(int iArg, out T value) where T : struct, Enum
        {
            string enumString = arg.GetString(iArg, null);
            return Enum.TryParse(enumString, out value);
        }
    }
}