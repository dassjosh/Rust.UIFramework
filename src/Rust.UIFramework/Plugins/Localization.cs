using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Plugins;

internal static class LangKeys
{
    public const string Chat = nameof(Chat);
}
    
internal static class Localization
{
    internal static readonly Dictionary<string, Dictionary<string, string>> Languages = new()
    {
        ["en"] = new Dictionary<string, string>
        {
            [LangKeys.Chat] = "[UiFramework] {0}",
        }
    };
}