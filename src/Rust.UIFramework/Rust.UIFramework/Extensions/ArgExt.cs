using System;

namespace Oxide.Ext.UiFramework.Extensions
{
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
    }
}