using System;
using System.Collections.Generic;
using Network;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.HarmonyPatches;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Plugins.Core;

internal class UiFrameworkPlugin : BaseUiFrameworkPlugin
{
    public static UiFrameworkPlugin Instance;
    private readonly object _true = true;

    public UiFrameworkPlugin()
    {
        Instance = this;
        Name = UiFrameworkExtension.Instance.Name;
        Title = "UI Framework";
        Author = UiFrameworkExtension.Instance.Author;
    }

    [HookMethod(nameof(Init))]
    private void Init()
    {
        AddCovalenceCommand(["uif.version"], nameof(VersionCommand), "uif.version");
        AddCovalenceCommand(["uif.harmony.oxide.addui"], nameof(HarmonyAddUiPatch), "uif.harmony.oxide.addui");
        
        foreach (KeyValuePair<string, Dictionary<string, string>> language in Localization.Languages)
        {
            Lang.RegisterMessages(language.Value, this, language.Key);
        }
    }

    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnServerInitialized))]
    private void OnServerInitialized()
    {
        Singleton<DataHandler>.Instance.LoadAll();
        BaseUiFrameworkLibrary.ProcessOnServerInitialized();
        UiHarmony.Initialize();
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnServerSave))]
    private void OnServerSave()
    {
        Singleton<DataHandler>.Instance.OnServerSave();
    }

    [HookMethod(nameof(OnPluginUnloaded))]
    private void OnPluginUnloaded(Plugin plugin)
    {
        BaseUiFrameworkLibrary.ProcessOnPluginUnloaded(plugin);
    }

    #region Hooks
    [HookMethod(nameof(OnPlayerDisconnected))]
    private void OnPlayerDisconnected(BasePlayer player)
    {
        BaseUiFrameworkLibrary.ProcessOnPlayerDisconnected(player);
    }
    
    [HookMethod(nameof(OnClientCommand))]
    private object OnClientCommand(Connection connection, string command)
    {
        BasePlayer player = connection.player as BasePlayer;
        if (player && command.StartsWith(UiCommands.UiCommandName))
        {
            try
            {
                UiCommandTokenizer tokenizer = new(command);
                tokenizer.GetNext(); // UiCommandName
                Singleton<UiCommands>.Instance.OnCommandReceived(player, tokenizer);
                return _true;
            }
            catch (Exception ex)
            {
                PrintError($"Failed to process command '{command}':\n{ex}");
                return _true;
            }
        }

        return null;
    }
    #endregion

    #region Commands
    [HookMethod(nameof(VersionCommand))]
    private void VersionCommand(IPlayer player)
    {
        Chat(player, LangKeys.Version, UiFrameworkExtension.Instance.Version);
    }
    
    [HookMethod(nameof(HarmonyAddUiPatch))]
    private void HarmonyAddUiPatch(IPlayer player, string cmd, string[] args)
    {
        if (args.Length == 0)
        {
            Chat(player, LangKeys.Harmony.Patch.AddUi.Show, GetLang(UiFrameworkConfig.Instance.Harmony.PatchAddUiMethod ? LangKeys.Enabled : LangKeys.Disabled));
            return;
        }

        if (!args[0].TryParseBool(out bool state))
        {
            Chat(player, LangKeys.Harmony.Patch.AddUi.InvalidArg, args[0]);
            return;
        }
            
        UiFrameworkConfig.Instance.Harmony.PatchAddUiMethod = state;
        CuiHelper_AddUi_Patch.ToggleState(state);
        Chat(player, LangKeys.Harmony.Patch.AddUi.Set, GetLang(state ? LangKeys.Enabled : LangKeys.Disabled));
        UiFrameworkConfig.Instance.Save();
    }
    #endregion
}