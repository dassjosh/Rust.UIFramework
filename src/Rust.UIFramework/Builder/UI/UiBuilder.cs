using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.UI;

public partial class UiBuilder : BaseUiBuilder
{
    public BaseUiComponent Root;

    private BaseUiComponent _actualRoot;
    protected readonly List<BaseAnimation> Animations = [];
    
    private bool _autoDestroy = true;

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
    public void SetRoot(BaseUiComponent component, string name, string parent)
    {
        Root = component;
        _actualRoot = component;
        component.Reference = new UiReference(parent, name);
        Components.Add(component);
        RootName = name;
    }

    public void OverrideRoot(BaseUiComponent component)
    {
        Root = component;
    }

    public void NeedsMouse(bool enabled = true)
    {
        if (enabled)
        {
            _actualRoot.Component.AddSubComponent<NeedsMouseComponent>(true);
        }
        else
        {
            _actualRoot.Component.RemoveComponent<NeedsMouseComponent>();
        }
    }

    public void NeedsKeyboard(bool enabled = true)
    {
        if (enabled)
        {
            _actualRoot.Component.AddSubComponent<NeedsKeyboardComponent>(true);
        }
        else
        {
            _actualRoot.Component.RemoveComponent<NeedsKeyboardComponent>();
        }
    }

    public void EnableAutoDestroy(bool enabled = true)
    {
        _autoDestroy = enabled;
    }

    public BaseUiComponent GetActualRoot() => _actualRoot;
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
        if (dispose && !Disposed)
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

    private void AddAnimation(BaseAnimation animation)
    {
        Animations.Add(animation);
    }
    #endregion

    #region Write Components
    protected override void WriteComponentsInternal(JsonFrameworkWriter writer)
    {
        _actualRoot.WriteRootComponent(writer, _autoDestroy);

        WriteComponents(writer, Components, 1);
        WriteComponents(writer, Anchors, 0);
    }

    private static void WriteComponents<T>(JsonFrameworkWriter writer, List<T> components, int startIndex) where T : BaseUiComponent
    {
        int count = components.Count;
        for (int index = startIndex; index < count; index++)
        {
            components[index].WriteComponent(writer);
        }
    }
    #endregion

    #region Animations
    protected override void OnUiSent(SendInfo send)
    {
        Singleton<AnimationTracker>.Instance.RemoveUiForSend(send, RootName);
        for (int index = 0; index < Animations.Count; index++)
        {
            BaseAnimation animation = Animations[index];
            Singleton<AnimationHandler>.Instance.EnqueueAnimation(animation, SendInfoBuilder.GetForAnimations(send));
            Singleton<AnimationTracker>.Instance.OnAnimatedPanelCreated(send, RootName, animation.Reference.Name, animation.Id);
        }
    }
    #endregion

    #region Pooling

    protected override void FreeComponents()
    {
        base.FreeComponents();
        ClearAnimationList(Animations);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Root = null;
        _actualRoot = null;
        _autoDestroy = true;
    }
    #endregion
}