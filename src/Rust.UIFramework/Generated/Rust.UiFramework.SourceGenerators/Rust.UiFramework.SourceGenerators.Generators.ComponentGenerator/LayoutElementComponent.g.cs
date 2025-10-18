using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class LayoutElementComponent : ILayoutElementComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _preferredWidth = new(Oxide.Ext.UiFramework.Json.JsonDefaults.LayoutElement.PreferredWidth);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _preferredHeight = new(Oxide.Ext.UiFramework.Json.JsonDefaults.LayoutElement.PreferredHeight);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _minWidth = new(Oxide.Ext.UiFramework.Json.JsonDefaults.LayoutElement.MinWidth);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _minHeight = new(Oxide.Ext.UiFramework.Json.JsonDefaults.LayoutElement.MinHeight);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _flexibleWidth = new(Oxide.Ext.UiFramework.Json.JsonDefaults.LayoutElement.FlexibleWidth);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _flexibleHeight = new(Oxide.Ext.UiFramework.Json.JsonDefaults.LayoutElement.PreferredWidth);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _ignoreLayout = new(Oxide.Ext.UiFramework.Json.JsonDefaults.LayoutElement.IgnoreLayout);

	public float PreferredWidth { get => _preferredWidth.Value; set => _preferredWidth.Value = value; }
	public float PreferredHeight { get => _preferredHeight.Value; set => _preferredHeight.Value = value; }
	public float MinWidth { get => _minWidth.Value; set => _minWidth.Value = value; }
	public float MinHeight { get => _minHeight.Value; set => _minHeight.Value = value; }
	public float FlexibleWidth { get => _flexibleWidth.Value; set => _flexibleWidth.Value = value; }
	public float FlexibleHeight { get => _flexibleHeight.Value; set => _flexibleHeight.Value = value; }
	public bool IgnoreLayout { get => _ignoreLayout.Value; set => _ignoreLayout.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<float> ILayoutElementComponentTrackable.PreferredWidth => _preferredWidth;
	Oxide.Ext.UiFramework.Types.Tracked<float> ILayoutElementComponentTrackable.PreferredHeight => _preferredHeight;
	Oxide.Ext.UiFramework.Types.Tracked<float> ILayoutElementComponentTrackable.MinWidth => _minWidth;
	Oxide.Ext.UiFramework.Types.Tracked<float> ILayoutElementComponentTrackable.MinHeight => _minHeight;
	Oxide.Ext.UiFramework.Types.Tracked<float> ILayoutElementComponentTrackable.FlexibleWidth => _flexibleWidth;
	Oxide.Ext.UiFramework.Types.Tracked<float> ILayoutElementComponentTrackable.FlexibleHeight => _flexibleHeight;
	Oxide.Ext.UiFramework.Types.Tracked<bool> ILayoutElementComponentTrackable.IgnoreLayout => _ignoreLayout;

	public Oxide.Ext.UiFramework.Components.LayoutElementComponent SetPreferredWidth(float preferredWidth)
	{
		PreferredWidth = preferredWidth;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.LayoutElementComponent SetPreferredHeight(float preferredHeight)
	{
		PreferredHeight = preferredHeight;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.LayoutElementComponent SetMinWidth(float minWidth)
	{
		MinWidth = minWidth;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.LayoutElementComponent SetMinHeight(float minHeight)
	{
		MinHeight = minHeight;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.LayoutElementComponent SetFlexibleWidth(float flexibleWidth)
	{
		FlexibleWidth = flexibleWidth;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.LayoutElementComponent SetFlexibleHeight(float flexibleHeight)
	{
		FlexibleHeight = flexibleHeight;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.LayoutElementComponent SetIgnoreLayout(bool ignoreLayout)
	{
		IgnoreLayout = ignoreLayout;
		return this;
	}
	public ILayoutElementComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_preferredWidth.HasChanged || _preferredHeight.HasChanged || _minWidth.HasChanged || _minHeight.HasChanged || _flexibleWidth.HasChanged || _flexibleHeight.HasChanged || _ignoreLayout.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_preferredWidth.ResetHasChanged();
		_preferredHeight.ResetHasChanged();
		_minWidth.ResetHasChanged();
		_minHeight.ResetHasChanged();
		_flexibleWidth.ResetHasChanged();
		_flexibleHeight.ResetHasChanged();
		_ignoreLayout.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_preferredWidth.Reset();
		_preferredHeight.Reset();
		_minWidth.Reset();
		_minHeight.Reset();
		_flexibleWidth.Reset();
		_flexibleHeight.Reset();
		_ignoreLayout.Reset();
	}
}


