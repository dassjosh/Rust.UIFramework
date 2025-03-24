using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder : BaseBuilder
{
    protected readonly List<BaseUiComponent> Components = [];
    protected readonly List<BaseUiControl> Controls = [];
    protected readonly List<BaseUiComponent> Anchors = [];
    protected readonly List<BaseLayout> Layouts = [];
        
    protected string Font;
    protected static string GlobalFont = UiFontCache.GetUiFont(UiFont.RobotoCondensedRegular);
        
    public void EnsureCapacity(int capacity)
    {
        if (Components.Capacity < capacity)
        {
            Components.Capacity = capacity;
        }
    }
        
    public void SetCurrentFont(UiFont font) => SetCurrentFont(UiFontCache.GetUiFont(font));
    public void SetCurrentFont(string font) => Font = font;

    public override byte[] GetBytes()
    {
        JsonFrameworkWriter writer = CreateWriter();
        byte[] bytes = writer.ToArray();
        writer.Dispose();
        return bytes;
    }
        
    internal override void SendUi(SendInfo send)
    {
        JsonFrameworkWriter writer = CreateWriter();
        AddUi(send, writer);
        writer.Dispose();
        OnUiSent(send);
    }
        
    public JsonFrameworkWriter CreateWriter()
    {
        PreprocessElements();
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
        writer.WriteStartArray();
        WriteComponentsInternal(writer);
        writer.WriteEndArray();
        return writer;
    }

    protected abstract void WriteComponentsInternal(JsonFrameworkWriter writer);
    protected virtual void OnUiSent(SendInfo send) {}

    private void PreprocessElements()
    {
        ProcessLayouts();
    }

    private void ProcessLayouts()
    {
        int count = Layouts.Count;
        for (int index = 0; index < count; index++)
        {
            Layouts[index].CalculateElementPositions();
        }
    }
    
    protected virtual void FreeComponents()
    {
        ClearComponentList(Components);
        ClearComponentList(Controls);
        ClearComponentList(Anchors);
        ClearComponentList(Layouts);
    }
    
    private static void ClearComponentList<T>(List<T> components) where T : BasePoolable
    {
        int count = components.Count;
        for (int index = 0; index < count; index++)
        {
            components[index].Dispose();
        }

        components.Clear();
    }
    
    protected static void ClearAnimationList(List<BaseAnimation> animations)
    {
        int count = animations.Count;
        for (int index = 0; index < count; index++)
        {
            BaseAnimation animation = animations[index];
            if (!animation.WasQueued)
            {
                animation.Dispose();
            }
        }
        
        animations.Clear();
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        FreeComponents();
        Font = null;
    }

    protected override void LeavePool()
    {
        base.LeavePool();
        Font = GlobalFont;
    }
}