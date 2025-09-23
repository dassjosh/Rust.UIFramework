using System;
using System.Collections.Generic;
using Network;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Harmony;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Plugins;

internal class UiFrameworkPlugin : BaseUiFrameworkPlugin, IUiFrameworkPlugin
{
    public static UiFrameworkPlugin Instance;
    public readonly PluginId PluginId;
    private readonly object _true = true;
    public UiPluginPool PluginPool { get; set; } = UiPool.Internal;
    
    public UiFrameworkPlugin()
    {
        Instance = this;
        Name = UiFrameworkExtension.Instance.Name;
        Title = "UI Framework";
        Author = UiFrameworkExtension.Instance.Author;
        PluginId = new PluginId(this);
    }

    [HookMethod(nameof(Init))]
    internal void Init()
    {
        AddCovalenceCommand(["uif.version"], nameof(VersionCommand), "uif.version");
        AddCovalenceCommand(["uif.harmony.oxide.addui"], nameof(HarmonyAddUiPatch), "uif.harmony.oxide.addui");
        
        foreach (KeyValuePair<string, Dictionary<string, string>> language in Localization.Languages)
        {
            Lang.RegisterMessages(language.Value, this, language.Key);
        }
        
        IconsLib.RegisterIcons<Icons>(this, @enum => string.Format(DefaultIconFormats.RustIconsFormat, (int)@enum), icon => icon.SetMaterial(UiMaterials.Icons.IconMaterial));
        IconsLib.RegisterIcons<FontAwesomeRegularIcons>(this, @enum => string.Format(DefaultIconFormats.FontAwesomeRegularFormat, @enum), icon => icon.SetMaterial(UiMaterials.Icons.IconMaterial));
        IconsLib.RegisterIcons<FontAwesomeSolidIcons>(this, @enum => string.Format(DefaultIconFormats.FontAwesomeSolidFormat, @enum), icon => icon.SetMaterial(UiMaterials.Icons.IconMaterial));
    }

    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnServerInitialized))]
    internal void OnServerInitialized()
    {
        BaseUiFrameworkLibrary.ProcessOnServerInitialized();
        UiHarmony.Initialize();
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnServerSave))]
    private void OnServerSave()
    {
        Singleton<DataHandler>.Instance.OnServerSave();
        BaseMemoryCache.ExpireCaches();
        Singleton<ImageUpdateAnimations>.Instance.CleanupOldUpdates();
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnPluginLoaded))]
    private void OnPluginLoaded(Plugin plugin)
    {
        BaseUiFrameworkLibrary.ProcessOnPluginLoaded(plugin);
    }

    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnPluginUnloaded))]
    private void OnPluginUnloaded(Plugin plugin)
    {
        BaseUiFrameworkLibrary.ProcessOnPluginUnloaded(plugin);
        PluginIdExt.OnPluginUnloaded(plugin);
        if (plugin is IUiFrameworkPlugin uiPlugin)
        {
            Singleton<AnimationHandler>.Instance.OnPluginUnloaded(uiPlugin);
        }
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnServerShutdown))]
    private void OnServerShutdown()
    {
        BaseUiFrameworkLibrary.ProcessOnServerShutdown();
    }
    
    #region Hooks
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnEntitySpawned))]
    private void OnEntitySpawned(CommunityEntity entity)
    {
        ImageStorageData.Instance.OnCommunityEntityLoaded();
        Singleton<UiImageStorage>.Instance.OnCommunityEntitySpawned();
        Unsubscribe(nameof(OnEntitySpawned));
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnPlayerConnected))]
    private void OnPlayerConnected(BasePlayer player)
    {
        BaseUiFrameworkLibrary.ProcessOnPlayerConnected(player);
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnPlayerDisconnected))]
    private void OnPlayerDisconnected(BasePlayer player)
    {
        BaseUiFrameworkLibrary.ProcessOnPlayerDisconnected(player);
        Singleton<AnimationHandler>.Instance.OnPlayerDisconnected(player.userID.Get());
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnClientCommand))]
    private object OnClientCommand(Connection connection, string command)
    {
        BasePlayer player = connection.player as BasePlayer;
        if (player && command.StartsWith(UiCommands.UiCommandName))
        {
            try
            {
                Singleton<UiCommands>.Instance.OnCommandReceived(player, new UiCommandTokenizer(command));
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
        
        if (state == UiFrameworkConfig.Instance.Harmony.PatchAddUiMethod)
        {
            Chat(player, LangKeys.Harmony.Patch.AddUi.Show, GetLang(state ? LangKeys.Enabled : LangKeys.Disabled));
            return;
        }
            
        UiFrameworkConfig.Instance.Harmony.PatchAddUiMethod = state;
        CuiHelper_AddUi_Patch.ToggleState(state);
        Chat(player, LangKeys.Harmony.Patch.AddUi.Set, GetLang(state ? LangKeys.Enabled : LangKeys.Disabled));
        UiFrameworkConfig.Instance.Save();
    }
    
    [HookMethod(nameof(SetAnimationEnabled))]
    private void SetAnimationEnabled(IPlayer player, string cmd, string[] args)
    {
        if (args.Length == 0)
        {
            Chat(player, LangKeys.Animations.Enabled.Show, GetLang(GetBoolLang(UiFrameworkConfig.Instance.Animations.Enabled)));
            return;
        }

        if (!args[0].TryParseBool(out bool state))
        {
            Chat(player, LangKeys.Animations.Enabled.InvalidArg, args[0]);
            return;
        }
        
        if (state == UiFrameworkConfig.Instance.Animations.Enabled)
        {
            Chat(player, LangKeys.Animations.Enabled.Show, GetBoolLang(state));
            return;
        }
            
        UiFrameworkConfig.Instance.Animations.Enabled = state;
        Chat(player, LangKeys.Animations.Enabled.Set, GetBoolLang(state));
        UiFrameworkConfig.Instance.Save();
    }
    
    [HookMethod(nameof(SetAnimationUpdateRate))]
    private void SetAnimationUpdateRate(IPlayer player, string cmd, string[] args)
    {
        if (args.Length == 0)
        {
            Chat(player, LangKeys.Animations.UpdateRate.Show, UiFrameworkConfig.Instance.Animations.UpdateRate);
            return;
        }

        if (!args[0].TryParseInt(out int updateRate) || updateRate <= 0)
        {
            Chat(player, LangKeys.Animations.UpdateRate.InvalidArg, args[0]);
            return;
        }
        
        if (updateRate == UiFrameworkConfig.Instance.Animations.UpdateRate)
        {
            Chat(player, LangKeys.Animations.UpdateRate.Show, updateRate);
            return;
        }
            
        UiFrameworkConfig.Instance.Animations.UpdateRate = updateRate;
        Chat(player, LangKeys.Animations.UpdateRate.Set, updateRate);
        UiFrameworkConfig.Instance.Save();
    }

    private string GetBoolLang(bool state) => GetLang(state ? LangKeys.Enabled : LangKeys.Disabled);

    #endregion
}