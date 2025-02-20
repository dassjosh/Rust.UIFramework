using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewComponent : CoreComponent
{
    private const string Type = "UnityEngine.UI.ScrollView";
    
    public readonly ScrollViewContentComponent ContentTransform = new();
    public ScrollRect.MovementType MovementType = ScrollRect.MovementType.Clamped;
    public float Elasticity = JsonDefaults.ScrollView.Elasticity;
    public bool Inertia;
    public float DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
    public float ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
    public ScrollbarComponent HorizontalScrollbar;
    public ScrollbarComponent VerticalScrollbar;
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.ScrollView.Horizontal, HorizontalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.Vertical, VerticalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.MovementType, MovementType);
        writer.AddField(JsonDefaults.ScrollView.ElasticityName, Elasticity, JsonDefaults.ScrollView.Elasticity);
        writer.AddField(JsonDefaults.ScrollView.Inertia, Inertia, false);
        writer.AddField(JsonDefaults.ScrollView.DecelerationRateName, DecelerationRate, JsonDefaults.ScrollView.DecelerationRate);
        writer.AddField(JsonDefaults.ScrollView.ScrollSensitivityName, ScrollSensitivity, JsonDefaults.ScrollView.ScrollSensitivity);
        writer.AddComponent(JsonDefaults.ScrollView.HorizontalScrollbar, HorizontalScrollbar, HorizontalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.VerticalScrollbar, VerticalScrollbar, VerticalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.ContentTransform, ContentTransform);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        ContentTransform.Reset();
        HorizontalScrollbar?.Dispose();
        HorizontalScrollbar = null;
        VerticalScrollbar?.Dispose();
        VerticalScrollbar = null;
        MovementType = ScrollRect.MovementType.Clamped;
        Elasticity = JsonDefaults.ScrollView.Elasticity;
        Inertia = false;
        DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
        ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
        HorizontalScrollbar = null;
        VerticalScrollbar = null;
    }
}