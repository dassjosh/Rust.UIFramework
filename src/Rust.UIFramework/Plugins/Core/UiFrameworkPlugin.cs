using Network;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Plugins.Core;

internal class UiFrameworkPlugin : BaseUiFrameworkPlugin
{
    public static UiFrameworkPlugin Instance;

    public UiFrameworkPlugin()
    {
        Instance = this;
        Name = "UIFramework";
        Title = "UI Framework";
    }
    
    // ReSharper disable once UnusedMember.Local
    [HookMethod(nameof(OnServerInitialized))]
    private void OnServerInitialized()
    {
        Singleton<DataHandler>.Instance.LoadAll();
        BaseUiFrameworkLibrary.ProcessOnServerInitialized();
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

    [HookMethod(nameof(OnClientCommand))]
    private void OnClientCommand(Connection connection, string command)
    {
        if (command.StartsWith(UiCommands.UiCommandName))
        {
            UiCommandTokenizer tokenizer = new(command);
            tokenizer.GetNext(); // UiCommandName
            Singleton<UiCommands>.Instance.OnCommandReceived(connection, tokenizer);
        }
    }
}