using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewComponent : IComponent
{
    private const string Type = "UnityEngine.UI.ScrollView";
    
    public ScrollViewContentTransformComponent ContentTransform = new();
    public bool Horizontal;
    public bool Vertical;
    public ScrollRect.MovementType MovementType = ScrollRect.MovementType.Clamped;
    public float Elasticity = JsonDefaults.ScrollView.Elasticity;
    public bool Inertia;
    public float DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
    public float ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
    public ScrollbarComponent HorizontalScrollbar;
    public ScrollbarComponent VerticalScrollbar;
    
    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.ScrollView.Horizontal, Horizontal, false);
        writer.AddField(JsonDefaults.ScrollView.Vertical, Vertical, false);
        writer.AddField(JsonDefaults.ScrollView.MovementType, MovementType);
        writer.AddField(JsonDefaults.ScrollView.ElasticityName, Elasticity, JsonDefaults.ScrollView.Elasticity);
        writer.AddField(JsonDefaults.ScrollView.Inertia, Inertia, false);
        writer.AddField(JsonDefaults.ScrollView.DecelerationRateName, DecelerationRate, JsonDefaults.ScrollView.DecelerationRate);
        writer.AddField(JsonDefaults.ScrollView.ScrollSensitivityName, ScrollSensitivity, JsonDefaults.ScrollView.ScrollSensitivity);
        
        if (Horizontal)
        {
            writer.AddComponent(JsonDefaults.ScrollView.HorizontalScrollbar, HorizontalScrollbar);
        }

        if (Vertical)
        {
            writer.AddComponent(JsonDefaults.ScrollView.VerticalScrollbar, VerticalScrollbar);
        }
        
        if (ContentTransform != null)
        {
            writer.AddComponent(JsonDefaults.ScrollView.ContentTransform, ContentTransform);
        }
        
        writer.WriteEndObject();
    }

    public void Reset()
    {
        ContentTransform.Reset();
        Horizontal = false;
        Vertical = false;
        MovementType = ScrollRect.MovementType.Clamped;
        Elasticity = JsonDefaults.ScrollView.Elasticity;
        Inertia = false;
        DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
        ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
        HorizontalScrollbar = null;
        VerticalScrollbar = null;
    }
}