using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class ItemIconComponent : IItemIconComponent, IItemIconComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<int> _itemId = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<ulong> _skinId = new();

	public partial int ItemId { get => _itemId.Value; set => _itemId.Value = value; }
	public partial ulong SkinId { get => _skinId.Value; set => _skinId.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<int> IItemIconComponentTrackable.ItemId => _itemId;
	Oxide.Ext.UiFramework.Types.Tracked<ulong> IItemIconComponentTrackable.SkinId => _skinId;

	public Oxide.Ext.UiFramework.Components.ItemIconComponent SetItemId(int itemId)
	{
		ItemId = itemId;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ItemIconComponent SetSkinId(ulong skinId)
	{
		SkinId = skinId;
		return this;
	}
	public new IItemIconComponentTrackable AsTrackable() => this;
	public override bool HasChanged() => false || (_itemId.HasChanged || _skinId.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_itemId.ResetHasChanged();
		_skinId.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_itemId.Reset();
		_skinId.Reset();
	}
}


