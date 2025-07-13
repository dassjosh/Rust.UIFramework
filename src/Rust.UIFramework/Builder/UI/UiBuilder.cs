using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.UI;

public partial class UiBuilder : BaseUiBuilder, IAnimationBuilder
{
    public BaseUiComponent Root;

    private BaseUiComponent _actualRoot;
    private readonly List<BaseAnimation> _animations = [];

    #region Decontructor
    ~UiBuilder()
    {
        Dispose();
        //Need this because there is a global GC class that causes issues
        //ReSharper disable once RedundantNameQualifier
        System.GC.SuppressFinalize(this);
    }
    #endregion
        
    #region Setup
    public UiBuilder SetRoot<T>(out T element) where T : BaseUiComponent, new()
    {
        element = PluginPool.Get<T>();
        Components.Add(element);
        return this;
    }
    
    public UiBuilder SetRoot<T>(in UiReference reference, out T element) where T : BaseUiComponent, new()
    {
        element = PluginPool.Get<T>();
        return SetRoot(element, reference.Name, reference.Parent);
    }
    
    public UiBuilder SetRoot(BaseUiComponent element, string name, string parent)
    {
        Root = _actualRoot = element;
        element.Update = UpdateMode.AutoDestroy;
        element.Reference = new UiReference(parent, name);
        Components.Add(element);
        RootName = name;
        return this;
    }

    public UiBuilder OverrideRoot(BaseUiComponent component)
    {
        Root = component;
        return this;
    }

    public UiBuilder NeedsMouse(bool enabled = true)
    {
        _actualRoot.NeedsMouse(enabled);
        return this;
    }

    public UiBuilder NeedsKeyboard(bool enabled = true)
    {
        _actualRoot.NeedsKeyboard(enabled);
        return this;
    }

    public UiBuilder EnableAutoDestroy(bool enabled = true)
    {
        _actualRoot.Update = enabled ? UpdateMode.AutoDestroy : UpdateMode.None;
        return this;
    }

    public BaseUiComponent GetActualRoot() => _actualRoot;
    #endregion

    #region Builder
    public UiBuilder SetName(string name)
    {
        RootName = name;
        _actualRoot.Name = name;
        return this;
    }
    
    public UiBuilder SetParent(string parent)
    {
        _actualRoot.Parent = parent;
        return this;
    }

    public UiBuilder SetParent(UiLayer parent)
    {
        _actualRoot.SetParent(parent);
        return this;
    }

    public UiBuilder SetReference(in UiReference reference)
    {
        _actualRoot.Reference = reference;
        return this;
    }
    
    public UiBuilder SetPosition(in UiPosition pos)
    {
        _actualRoot.SetPosition(pos);
        return this;
    }
    
    public UiBuilder SetPosition(in UiPosition pos, in UiOffset offset)
    {
        _actualRoot.SetPosition(pos, offset);
        return this;
    }
    #endregion
    
    #region JSON
    public int WriteBuffer(byte[] buffer)
    {
        JsonFrameworkWriter writer = CreateWriter();
        int bytes = writer.WriteTo(buffer);
        writer.Dispose();
        return bytes;
    }

    public CachedUiBuilder ToCachedBuilder(bool dispose = true)
    {
        CachedUiBuilder cached = CachedUiBuilder.CreateCachedBuilder(this);
        if (dispose && CanPool)
        {
            Dispose();
        }
        return cached;
    }
    #endregion
        
    #region Add Components
    public override void AddComponent(BaseUiComponent component, in UiReference parent)
    {
        UiReferenceException.ThrowIfInvalidParent(parent);
        component.Reference = parent.WithChild(UiNameCache.GetComponentName(RootName, Components.Count));
        Components.Add(component);
    }
        
    protected override void AddAnchor(BaseUiComponent component, in UiReference parent)
    {
        UiReferenceException.ThrowIfInvalidParent(parent);
        component.Reference = parent.WithChild(UiNameCache.GetAnchorName(RootName, Anchors.Count));
        Anchors.Add(component);
    }

    void IAnimationBuilder.AddAnimation(BaseAnimation animation) => _animations.Add(animation);

    #endregion

    #region Animations
    protected override void OnUiSent(SendInfo send)
    {
        Singleton<AnimationTracker>.Instance.RemoveUiForSend(send, RootName);
        for (int index = 0; index < _animations.Count; index++)
        {
            BaseAnimation animation = _animations[index];
            Singleton<AnimationHandler>.Instance.EnqueueAnimation(animation, SendInfoBuilder.GetForAnimations(send));
            Singleton<AnimationTracker>.Instance.OnAnimatedPanelCreated(send, RootName, animation.Reference.Name, animation.Id);
        }
    }
    #endregion

    #region Pooling

    protected override void FreeComponents()
    {
        base.FreeComponents();
        ClearAnimationList(_animations);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Root = null;
        _actualRoot = null;
    }
    #endregion
}