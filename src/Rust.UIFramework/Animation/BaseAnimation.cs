using System;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimation : BasePoolable
{
    public AnimationId Id;
    private UiReference _reference;
    public float UpdateRate;
    public float Delay;
    public float Duration;
    public float Elapsed;
    public float ElapsedPercentage => Elapsed < Delay ? 0 : Math.Min((Elapsed - Delay) / Duration, 1f);
    public int Repeats;
    public float RepeatDelay;
    public SendInfo Send;

    public DateTime StartTime;

    internal bool WasQueued;

    protected void Init(BaseUiComponent component, float updateRate, float delay, float duration, int repeats, float repeatDelay)
    {
        Id = AnimationId.GetNextId();
        _reference = component;
        UpdateRate = updateRate;
        Delay = delay;
        Duration = duration;
        Repeats = repeats;
        RepeatDelay = repeatDelay;
    }
    
    public void SendAnimation(float elapsedPercentage)
    {
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
        WriteAnimationComponent(writer, elapsedPercentage);
        BaseBuilder.AddUi(Send, writer);
        writer.Dispose();
    }

    private void WriteAnimationComponent(JsonFrameworkWriter writer, float elapsedPercentage)
    {
        writer.WriteStartArray();
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, _reference.Parent);
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, _reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.Update, true);
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        WriteAnimation(writer, elapsedPercentage);    
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.WriteEndArray();
    }
    
    protected abstract void WriteAnimation(JsonFrameworkWriter writer, float value);

    protected override void EnterPool()
    {
        Id = default;
        _reference = default;
        UpdateRate = default;
        Delay = default;
        Duration = default;
        Elapsed = default;
        Repeats = default;
        RepeatDelay = default;
        if (Send.connections != null)
        {
            UiFrameworkPool.FreeList(Send.connections);
        }
        Send = default;
        WasQueued = false;
    }
}