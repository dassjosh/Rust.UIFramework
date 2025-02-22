using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewComponent : IComponent
{
    public readonly ScrollViewContentTransformComponent ContentTransform = new();
    public bool Horizontal;
    public bool Vertical;
    public ScrollRect.MovementType MovementType = JsonDefaults.ScrollView.MovementType;
    public float Elasticity = JsonDefaults.ScrollView.Elasticity;
    public bool Inertia = JsonDefaults.ScrollView.Inertia;
    public float DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
    public float ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
    public ScrollbarComponent HorizontalScrollbar;
    public ScrollbarComponent VerticalScrollbar;
    
    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.ScrollView.Type);
        writer.AddField(JsonDefaults.ScrollView.Horizontal, Horizontal, false);
        writer.AddField(JsonDefaults.ScrollView.Vertical, Vertical, false);
        writer.AddField(JsonDefaults.ScrollView.MovementTypeName, MovementType, JsonDefaults.ScrollView.MovementType);
        writer.AddField(JsonDefaults.ScrollView.ElasticityName, Elasticity, JsonDefaults.ScrollView.Elasticity);
        writer.AddField(JsonDefaults.ScrollView.InertiaName, Inertia, JsonDefaults.ScrollView.Inertia);
        writer.AddField(JsonDefaults.ScrollView.DecelerationRateName, DecelerationRate, JsonDefaults.ScrollView.DecelerationRate);
        writer.AddField(JsonDefaults.ScrollView.ScrollSensitivityName, ScrollSensitivity, JsonDefaults.ScrollView.ScrollSensitivity);
        writer.AddComponent(JsonDefaults.ScrollView.HorizontalScrollbar, HorizontalScrollbar, Horizontal);
        writer.AddComponent(JsonDefaults.ScrollView.VerticalScrollbar, VerticalScrollbar, Vertical);
        writer.AddComponent(JsonDefaults.ScrollView.ContentTransform, ContentTransform);
        
        writer.WriteEndObject();
    }

    public void Reset()
    {
        ContentTransform.Reset();
        Horizontal = false;
        Vertical = false;
        MovementType = JsonDefaults.ScrollView.MovementType;
        Elasticity = JsonDefaults.ScrollView.Elasticity;
        Inertia = JsonDefaults.ScrollView.Inertia;
        DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
        ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
        HorizontalScrollbar = null;
        VerticalScrollbar = null;
    }
}