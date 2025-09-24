using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder : BaseBuilder
{
    public UpdateMode UpdateMode;
    public NamingMode NamingMode;
    
    protected readonly List<BaseUiComponent> Components = [];
    protected readonly List<BaseUiControl> Controls = [];
    protected readonly List<BaseUiComponent> Anchors = [];
    protected readonly List<BaseUiLayout> Layouts = [];
        
    protected string Font;
    
    protected INamingStrategy Naming = Singleton<DefaultNamingStrategy>.Instance;
    protected INamingCache NamingCache = Singleton<UiNamingCache>.Instance.Default;
    
    private static readonly string GlobalFont = UiFrameworkConfig.Instance.Font.DefaultFont;
    
    public IUiFrameworkPlugin Plugin { get; protected set; }
        
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
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create(PluginPool);
        writer.WriteStartArray();
        WriteComponentsInternal(writer);
        writer.WriteEndArray();
        return writer;
    }

    private void WriteComponentsInternal(JsonFrameworkWriter writer)
    {
        PreprocessElements();
        WriteComponents(writer, Components);
        WriteComponents(writer, Anchors);
    }

    private static void WriteComponents<T>(JsonFrameworkWriter writer, List<T> components) where T : BaseUiComponent
    {
        int count = components.Count;
        ReadOnlySpan<T> span = components.GetAsReadOnlySpan();
        for (int index = 0; index < count; index++)
        {
            span[index].WriteComponent(writer);
        }
    }
    
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
        Components.FreeValues();
        Controls.FreeValues();
        Anchors.FreeValues();
        Layouts.FreeValues();
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        FreeComponents();
        Naming = Singleton<DefaultNamingStrategy>.Instance;
        NamingCache = Singleton<UiNamingCache>.Instance.Default;
    }
    
    protected override void LeavePool()
    {
        base.LeavePool();
        Font = GlobalFont;
    }
}