using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ItemIconComponent : IItemIconComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _itemId = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<ulong> _skinId = new();

	public int ItemId { get => _itemId.Value; set => _itemId.Value = value; }
	public ulong SkinId { get => _skinId.Value; set => _skinId.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<int> IItemIconComponentTrackable.ItemId => _itemId;
	Oxide.Ext.UiFramework.Types.Tracked<ulong> IItemIconComponentTrackable.SkinId => _skinId;

	public new IItemIconComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_itemId.HasChanged || _skinId.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_itemId.ResetHasChanged();
		_skinId.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_itemId.Reset();
		_skinId.Reset();
	}
}


