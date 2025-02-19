using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Plugins;

internal static class LangKeys
{
    public const string Chat = nameof(Chat);
    public const string Version = nameof(Version);
    public const string Enabled = nameof(Enabled);
    public const string Disabled = nameof(Disabled);

    internal static class Harmony
    {
        private const string Base = nameof(Harmony) + ".";
        internal static class Patch
        {
            private const string Base = Harmony.Base + nameof(Patch) + ".";
            
            internal static class AddUi
            {
                private const string Base = Patch.Base + nameof(AddUi) + ".";
                
                internal const string Show = Base + nameof(Show);
                internal const string Set = Base + nameof(Set);
                public const string InvalidArg = Base + nameof(InvalidArg);
            }
        }
    }
}
    
internal static class Localization
{
    internal static readonly Dictionary<string, Dictionary<string, string>> Languages = new()
    {
        ["en"] = new Dictionary<string, string>
        {
            [LangKeys.Chat] = "[UiFramework] {0}",
            [LangKeys.Version] = "Server is running Ui Framework Extension v{0}",
            [LangKeys.Enabled] = "Enabled",
            [LangKeys.Disabled] = "Disabled",
            [LangKeys.Harmony.Patch.AddUi.Show] = "Harmony add Oxide CUI AddUi Patch is {0}",
            [LangKeys.Harmony.Patch.AddUi.Set] = "Harmony add Oxide CUI AddUi Patch is {0} now",
            [LangKeys.Harmony.Patch.AddUi.InvalidArg] = "Invalid argument {0}"
        }
    };
}