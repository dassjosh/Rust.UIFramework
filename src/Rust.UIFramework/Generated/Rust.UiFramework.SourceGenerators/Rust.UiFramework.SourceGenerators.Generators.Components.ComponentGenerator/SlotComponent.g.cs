using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class SlotComponent : ISlotComponent, ISlotComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _filter = new();

	public partial string Filter { get => _filter.Value; set => _filter.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<string> ISlotComponentTrackable.Filter => _filter;

	public Oxide.Ext.UiFramework.Components.SlotComponent SetFilter(string filter)
	{
		Filter = filter;
		return this;
	}
	public ISlotComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_filter.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_filter.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_filter.Reset();
	}
}


