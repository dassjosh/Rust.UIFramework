using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class BaseLayoutComponent : IBaseLayoutComponent, IBaseLayoutComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> _childAlignment = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Layout.ChildAlignment);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> _padding = new();

	public partial UnityEngine.TextAnchor ChildAlignment { get => _childAlignment.Value; set => _childAlignment.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiPadding Padding { get => _padding.Value; set => _padding.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> IBaseLayoutComponentTrackable.ChildAlignment => _childAlignment;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> IBaseLayoutComponentTrackable.Padding => _padding;

	public override bool HasChanged() => false || (_childAlignment.HasChanged || _padding.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_childAlignment.ResetHasChanged();
		_padding.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_childAlignment.Reset();
		_padding.Reset();
	}
}


