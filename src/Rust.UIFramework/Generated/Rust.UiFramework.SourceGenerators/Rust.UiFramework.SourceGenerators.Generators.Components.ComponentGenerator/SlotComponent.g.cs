using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class SlotComponent : ISlotComponent, ISlotComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<string> _filter = new();

	public partial string Filter { get => _filter.Value; set => _filter.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<string> ISlotComponentTrackable.Filter => _filter;

	public Oxide.Ext.UiFramework.Components.SlotComponent SetFilter(string filter)
	{
		Filter = filter;
		return this;
	}
	public ISlotComponentTrackable AsTrackable() => this;
	public override bool HasChanged() => false || (_filter.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_filter.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_filter.Reset();
	}
}


