using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public class UpdatableBuilder : BaseBuilder
{
    private List<UiUpdatable> _updatables;

    public static UpdatableBuilder Create(IUiFrameworkPlugin plugin)
    {
        return new UpdatableBuilder().Init(plugin.PluginPool);
    }
    
    internal void AddUpdatable(UiUpdatable updatable)
    {
        _updatables.Add(updatable);
    }

    private UpdatableBuilder Init(UiPluginPool pool)
    {
        OverridePluginPool(pool);
        _updatables = pool.GetList<UiUpdatable>();
        return this;
    }
    
    ~UpdatableBuilder()
    {
        _updatables.FreeValues();
        PluginPool.FreeList(_updatables);
        _updatables = null;
    }
    
    internal override void SendUi(SendInfo send)
    {
        JsonFrameworkWriter writer = CreateWriter();
        AddUi(send, writer);
        writer.Dispose();
    }
    
    private JsonFrameworkWriter CreateWriter()
    {
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create(PluginPool);
        writer.WriteStartArray();
        int count = _updatables.Count;
        UiUpdatable[] updatables = _updatables.GetInternalArray();
        for (int index = 0; index < count; index++)
        {
            updatables[index].Serialize(writer);
        }
        writer.WriteEndArray();
        return writer;
    }

    public override byte[] GetBytes()
    {
        throw new System.NotImplementedException();
    }
}