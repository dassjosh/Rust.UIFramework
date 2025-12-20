using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public delegate Tracked<TField> ElementFieldSelector<TField, in T>(T element) where T : BaseUiComponent;
public delegate Tracked<TField> ComponentFieldSelector<TField, in T>(T component) where T : BaseComponent;
public delegate TComponent ComponentSelector<out TComponent, in TElement>(TElement element) where TElement : BaseUiComponent where TComponent : BaseComponent;
public delegate TChild ChildComponentSelector<out TChild, in TParent>(TParent element) where TParent : BaseComponent where TChild : BaseComponent;