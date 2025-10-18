using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class BaseLayoutComponent : IBaseLayoutComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> _childAlignment = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Layout.ChildAlignment);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> _padding = new();

	public UnityEngine.TextAnchor ChildAlignment { get => _childAlignment.Value; set => _childAlignment.Value = value; }
	public Oxide.Ext.UiFramework.Types.UiPadding Padding { get => _padding.Value; set => _padding.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> IBaseLayoutComponentTrackable.ChildAlignment => _childAlignment;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> IBaseLayoutComponentTrackable.Padding => _padding;

	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_childAlignment.HasChanged || _padding.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_childAlignment.ResetHasChanged();
		_padding.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_childAlignment.Reset();
		_padding.Reset();
	}
}


