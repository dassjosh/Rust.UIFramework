using System;
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

    public static UpdatableBuilder Create(IUiFrameworkPlugin plugin) => new UpdatableBuilder().Init(plugin.PluginPool);

    public UiUpdatable<T> AddUpdatable<T>(T element) where T : BaseUiComponent, new() => UiUpdatable<T>.Create(this, element);
    public UiUpdatable<T> AddUpdatable<T>(in UiReference reference, Action<T> init = null) where T : BaseUiComponent, new() => UiUpdatable<T>.Create(this, reference, init);

    internal void AddUpdatable(UiUpdatable updatable) => _updatables.Add(updatable);

    private UpdatableBuilder Init(UiPluginPool pool)
    {
        OverridePluginPool(pool);
        _updatables = pool.GetList<UiUpdatable>();
        return this;
    }
    
    ~UpdatableBuilder() => Cleanup();

    internal override void SendUi(SendInfo send, in UiDebugOptions? options)
    {
        JsonFrameworkWriter writer = CreateWriter(true);
        AddUi(send, writer, options);
        writer.Dispose();
    }
    
    private JsonFrameworkWriter CreateWriter(bool swap)
    {
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create(PluginPool);
        writer.WriteStartArray();
        int count = _updatables.Count;
        UiUpdatable[] updatables = _updatables.GetInternalArray();
        for (int index = 0; index < count; index++)
        {
            UiUpdatable updatable = updatables[index];
            updatable.Serialize(writer);
            if (swap)
            {
                updatable.Swap();
            }
        }
        writer.WriteEndArray();
        return writer;
    }

    public override byte[] GetBytes()
    {
        JsonFrameworkWriter writer = CreateWriter(false);
        byte[] bytes = writer.ToArray();
        writer.Dispose();
        return bytes;
    }
    
    public override void Dispose()
    {
        Cleanup();
        GC.SuppressFinalize(this);
    }

    private void Cleanup()
    {
        if (_updatables != null)
        {
            _updatables.FreeValues();
            PluginPool.FreeList(_updatables);
            _updatables = null;
        }
    }
}