using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewComponent : CoreComponent
{
    public readonly ScrollViewContentComponent ContentTransform = new();
    public ScrollRect.MovementType MovementType;
    public float Elasticity;
    public bool Inertia;
    public float DecelerationRate;
    public float ScrollSensitivity;
    public ScrollbarComponent HorizontalScrollbar { get; internal set;  }
    public ScrollbarComponent VerticalScrollbar { get; internal set; }
    
    public override Utf8String Type => JsonDefaults.ScrollView.Type;

    public void UpdateContentTransform(in UiPosition? position = null, in UiOffset? offset = null) => ContentTransform.UpdateContentTransform(position, offset);
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.ScrollView.Horizontal, HorizontalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.Vertical, VerticalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.MovementTypeName, MovementType);
        writer.AddField(JsonDefaults.ScrollView.ElasticityName, Elasticity, JsonDefaults.ScrollView.Elasticity);
        writer.AddField(JsonDefaults.ScrollView.InertiaName, Inertia, JsonDefaults.ScrollView.Inertia);
        writer.AddField(JsonDefaults.ScrollView.DecelerationRateName, DecelerationRate, JsonDefaults.ScrollView.DecelerationRate);
        writer.AddField(JsonDefaults.ScrollView.ScrollSensitivityName, ScrollSensitivity, JsonDefaults.ScrollView.ScrollSensitivity);
        writer.AddComponent(JsonDefaults.ScrollView.HorizontalScrollbar, HorizontalScrollbar, HorizontalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.VerticalScrollbar, VerticalScrollbar, VerticalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.ContentTransform, ContentTransform);
    }

    public override void Reset()
    {
        ContentTransform.Reset();
        HorizontalScrollbar?.Dispose();
        HorizontalScrollbar = null;
        VerticalScrollbar?.Dispose();
        VerticalScrollbar = null;
        MovementType = JsonDefaults.ScrollView.MovementType;
        Elasticity = JsonDefaults.ScrollView.Elasticity;
        Inertia = JsonDefaults.ScrollView.Inertia;
        DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
        ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
        HorizontalScrollbar = null;
        VerticalScrollbar = null;
    }
}