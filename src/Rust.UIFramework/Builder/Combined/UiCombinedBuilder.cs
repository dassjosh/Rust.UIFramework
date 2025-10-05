using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Builder.Combined;

public class UiCombinedBuilder : BaseBuilder
{
    private readonly List<BaseBuilder> _builders = [];
    
    public static UiCombinedBuilder Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<UiCombinedBuilder>().Init(plugin);
    public static UiCombinedBuilder Create(IUiFrameworkPlugin plugin, BaseBuilder builder) => Create(plugin).Add(builder);
    public static UiCombinedBuilder Create(IUiFrameworkPlugin plugin, BaseBuilder b1, BaseBuilder b2) => Create(plugin, b1).Add(b2);
    public static UiCombinedBuilder Create(IUiFrameworkPlugin plugin, BaseBuilder b1, BaseBuilder b2, BaseBuilder b3) => Create(plugin, b1, b2).Add(b3);
    public static UiCombinedBuilder Create(IUiFrameworkPlugin plugin, IEnumerable<BaseBuilder> builders) => Create(plugin).AddRange(builders);
    
    private new UiCombinedBuilder Init(IUiFrameworkPlugin plugin)
    {
        base.Init(plugin);
        return this;
    }
    
    public UiCombinedBuilder Add(BaseBuilder builder)
    {
        _builders.Add(builder);
        return this;
    }
    
    public UiCombinedBuilder AddRange(IEnumerable<BaseBuilder> builders)
    {
        _builders.AddRange(builders);
        return this;
    }
    
    internal override void SendUi(SendInfo send, in UiDebugOptions? options)
    {
        
    }

    public override byte[] GetBytes()
    {
        throw new System.NotImplementedException();
    }

    public override void Combine(SendInfo send, JsonFrameworkWriter writer)
    {
        for (int index = 0; index < _builders.Count; index++)
        {
            BaseBuilder builder = _builders[index];
            builder.Combine(send, writer);
        }
    }
}