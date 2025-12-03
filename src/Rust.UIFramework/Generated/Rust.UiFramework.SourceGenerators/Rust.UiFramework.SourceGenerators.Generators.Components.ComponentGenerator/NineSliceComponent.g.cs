using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class NineSliceComponent : INineSliceComponent, INineSliceComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _png = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiBorderWidth> _slice = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Image.Slice);

	public partial string Png { get => _png.Value; set => _png.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiBorderWidth Slice { get => _slice.Value; set => _slice.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<string> INineSliceComponentTrackable.Png => _png;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiBorderWidth> INineSliceComponentTrackable.Slice => _slice;

	public Oxide.Ext.UiFramework.Components.NineSliceComponent SetPng(string png)
	{
		Png = png;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.NineSliceComponent SetSlice(in Oxide.Ext.UiFramework.Types.UiBorderWidth slice)
	{
		Slice = slice;
		return this;
	}
	public new INineSliceComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_png.HasChanged || _slice.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_png.ResetHasChanged();
		_slice.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_png.Reset();
		_slice.Reset();
	}
}


