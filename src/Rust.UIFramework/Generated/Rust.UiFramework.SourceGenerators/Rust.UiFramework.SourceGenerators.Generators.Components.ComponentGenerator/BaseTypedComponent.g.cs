using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class BaseTypedComponent : IBaseTypedComponent, IBaseTypedComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _enabled = new(true);

	public partial bool Enabled { get => _enabled.Value; set => _enabled.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> IBaseTypedComponentTrackable.Enabled => _enabled;

	public Oxide.Ext.UiFramework.Components.BaseTypedComponent SetEnabled(bool enabled)
	{
		Enabled = enabled;
		return this;
	}
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_enabled.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_enabled.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_enabled.Reset();
	}
}


