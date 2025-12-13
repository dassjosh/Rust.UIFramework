using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class BaseTypedComponent : IBaseTypedComponent, IBaseTypedComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _enabled = new(true);

	public partial bool Enabled { get => _enabled.Value; set => _enabled.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> IBaseTypedComponentTrackable.Enabled => _enabled;

	public Oxide.Ext.UiFramework.Components.BaseTypedComponent SetEnabled(bool enabled)
	{
		Enabled = enabled;
		return this;
	}
	public override bool HasChanged() => false || (_enabled.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_enabled.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_enabled.Reset();
	}
}


