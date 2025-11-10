using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class ExecutionData : BasePoolable
{
    private IUiFrameworkPlugin _plugin;
    public BasePlayer Player { get; private set; }
    public bool IsOnCooldown { get; internal set; }
    public float RemainingCooldown { get; internal set; }
    public bool HasPermission { get; internal set; }
    public bool IsValid => !IsOnCooldown && HasPermission;

    public static ExecutionData Create(IUiFrameworkPlugin plugin, BasePlayer player) => plugin.PluginPool.Get<ExecutionData>().Init(plugin, player);

    private ExecutionData Init(IUiFrameworkPlugin plugin, BasePlayer player)
    {
        _plugin = plugin;
        Player = player;
        return this;
    }
    
    public T GetStore<T>() where T : class, IPlayerStore => GetStore<T>(Player);
    public T GetStore<T>(ulong playerId) where T : class, IPlayerStore => Singleton<UiPlayerStore>.Instance.GetStore<T>(_plugin, playerId);
    public T GetStore<T>(BasePlayer player) where T : class, IPlayerStore => Singleton<UiPlayerStore>.Instance.GetStore<T>(_plugin, player);
    public T GetStore<T>(string name) where T : class, INamedStore => Singleton<UiNameStore>.Instance.GetStore<T>(_plugin, name);

    protected override void EnterPool()
    {
        _plugin = null;
        Player = null;
        IsOnCooldown = true;
        HasPermission = false;
    }
}