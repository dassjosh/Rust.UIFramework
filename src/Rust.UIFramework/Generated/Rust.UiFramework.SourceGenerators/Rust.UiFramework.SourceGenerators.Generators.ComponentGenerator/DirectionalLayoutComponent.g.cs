using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class DirectionalLayoutComponent : IDirectionalLayoutComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _spacing = new(Oxide.Ext.UiFramework.Json.JsonDefaults.DirectionalLayout.Spacing);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _childForceExpandWidth = new(Oxide.Ext.UiFramework.Json.JsonDefaults.DirectionalLayout.ChildForceExpandWidth);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _childForceExpandHeight = new(Oxide.Ext.UiFramework.Json.JsonDefaults.DirectionalLayout.ChildForceExpandHeight);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _childControlWidth = new(Oxide.Ext.UiFramework.Json.JsonDefaults.DirectionalLayout.ChildControlWidth);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _childControlHeight = new(Oxide.Ext.UiFramework.Json.JsonDefaults.DirectionalLayout.ChildControlHeight);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _childScaleWidth = new(Oxide.Ext.UiFramework.Json.JsonDefaults.DirectionalLayout.ChildScaleWidth);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _childScaleHeight = new(Oxide.Ext.UiFramework.Json.JsonDefaults.DirectionalLayout.ChildScaleHeight);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.LayoutDirection> _direction = new();

	public float Spacing { get => _spacing.Value; set => _spacing.Value = value; }
	public bool ChildForceExpandWidth { get => _childForceExpandWidth.Value; set => _childForceExpandWidth.Value = value; }
	public bool ChildForceExpandHeight { get => _childForceExpandHeight.Value; set => _childForceExpandHeight.Value = value; }
	public bool ChildControlWidth { get => _childControlWidth.Value; set => _childControlWidth.Value = value; }
	public bool ChildControlHeight { get => _childControlHeight.Value; set => _childControlHeight.Value = value; }
	public bool ChildScaleWidth { get => _childScaleWidth.Value; set => _childScaleWidth.Value = value; }
	public bool ChildScaleHeight { get => _childScaleHeight.Value; set => _childScaleHeight.Value = value; }
	public Oxide.Ext.UiFramework.Enums.LayoutDirection Direction { get => _direction.Value; set => _direction.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<float> IDirectionalLayoutComponentTrackable.Spacing => _spacing;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDirectionalLayoutComponentTrackable.ChildForceExpandWidth => _childForceExpandWidth;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDirectionalLayoutComponentTrackable.ChildForceExpandHeight => _childForceExpandHeight;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDirectionalLayoutComponentTrackable.ChildControlWidth => _childControlWidth;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDirectionalLayoutComponentTrackable.ChildControlHeight => _childControlHeight;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDirectionalLayoutComponentTrackable.ChildScaleWidth => _childScaleWidth;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDirectionalLayoutComponentTrackable.ChildScaleHeight => _childScaleHeight;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.LayoutDirection> IDirectionalLayoutComponentTrackable.Direction => _direction;

	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetSpacing(float spacing)
	{
		Spacing = spacing;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetChildForceExpandWidth(bool childForceExpandWidth)
	{
		ChildForceExpandWidth = childForceExpandWidth;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetChildForceExpandHeight(bool childForceExpandHeight)
	{
		ChildForceExpandHeight = childForceExpandHeight;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetChildControlWidth(bool childControlWidth)
	{
		ChildControlWidth = childControlWidth;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetChildControlHeight(bool childControlHeight)
	{
		ChildControlHeight = childControlHeight;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetChildScaleWidth(bool childScaleWidth)
	{
		ChildScaleWidth = childScaleWidth;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetChildScaleHeight(bool childScaleHeight)
	{
		ChildScaleHeight = childScaleHeight;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DirectionalLayoutComponent SetDirection(Oxide.Ext.UiFramework.Enums.LayoutDirection direction)
	{
		Direction = direction;
		return this;
	}
	public IDirectionalLayoutComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_spacing.HasChanged || _childForceExpandWidth.HasChanged || _childForceExpandHeight.HasChanged || _childControlWidth.HasChanged || _childControlHeight.HasChanged || _childScaleWidth.HasChanged || _childScaleHeight.HasChanged || _direction.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_spacing.ResetHasChanged();
		_childForceExpandWidth.ResetHasChanged();
		_childForceExpandHeight.ResetHasChanged();
		_childControlWidth.ResetHasChanged();
		_childControlHeight.ResetHasChanged();
		_childScaleWidth.ResetHasChanged();
		_childScaleHeight.ResetHasChanged();
		_direction.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_spacing.Reset();
		_childForceExpandWidth.Reset();
		_childForceExpandHeight.Reset();
		_childControlWidth.Reset();
		_childControlHeight.Reset();
		_childScaleWidth.Reset();
		_childScaleHeight.Reset();
		_direction.Reset();
	}
}


