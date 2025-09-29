using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.UI;

public partial class UiBuilder : BaseUiBuilder, IAnimationBuilder
{
    public BaseUiComponent Root;
    
    private BaseUiComponent _actualRoot;
    private readonly List<BaseAnimation> _animations = [];
        
    #region Setup
    public UiBuilder Init(IUiFrameworkPlugin plugin)
    {
        Plugin = plugin;
        return this;
    }
    
    public UiBuilder SetRoot<T>(in UiReference reference, out T element) where T : BaseUiComponent, new()
    {
        element = PluginPool.Get<T>();
        return SetRoot(element, reference.Name, reference.Parent);
    }
    
    public UiBuilder SetRoot(BaseUiComponent element, string name, string parent)
    {
        Root = _actualRoot = element.SetUpdate(UpdateMode.Replace).SetReference(new UiReference(parent, name));
        Components.Add(element);
        RootName = name;
        SetNamingCache(name);
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

    public UiBuilder SetRootReplace(bool enabled = true)
    {
        _actualRoot.Update = enabled ? UpdateMode.Replace : UpdateMode.None;
        return this;
    }

    public BaseUiComponent GetActualRoot() => _actualRoot;
    #endregion

    #region Builder
    public UiBuilder SetName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            RootName = name;
            SetNamingCache(name);
            if (_actualRoot != null)
            {
                _actualRoot.Name = name;
            }
        }

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
    
    public UiBuilder SetNamingCache(string name)
    {
        SetNamingCache(Singleton<UiNamingCache>.Instance.GetNamingCache(name));
        return this;
    }

    public UiBuilder SetNamingCache(INamingCache cache)
    {
        NamingCache = cache;
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
    
    public UiBuilder SetUpdateMode(UpdateMode mode)
    {
        UpdateMode = mode;
        return this;
    }
    
    public UiBuilder SetNamingMode(NamingMode mode)
    {
        NamingMode = mode;
        return this;
    }

    public UiBuilder SetNamingStrategy(INamingStrategy naming)
    {
        Naming = naming;
        return this;
    }
    
    public UiBuilder UseDefaultNamingStrategy() => SetNamingStrategy(Singleton<DefaultNamingStrategy>.Instance);

    public UiBuilder SwitchToDefaultMode(string uiRootName = null) => SwitchTo(UpdateMode.None, NamingMode.Child).SetName(uiRootName);
    public UiBuilder SwitchToUpdateExistingMode() => SwitchTo(UpdateMode.Update, NamingMode.Reference);
    public UiBuilder SwitchToReplaceExistingMode() => SwitchTo(UpdateMode.Replace, NamingMode.Reference);
    public UiBuilder SwitchTo(UpdateMode update, NamingMode naming) => SetUpdateMode(update).SetNamingMode(naming);
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

    #region Animations
    void IAnimationBuilder.AddAnimation(BaseAnimation animation) => _animations.Add(animation);
    
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
    
    protected override void LeavePool()
    {
        base.LeavePool();
        UpdateMode = UpdateMode.None;
        NamingMode = NamingMode.Child;
    }

    #endregion
}