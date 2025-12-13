using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class OutlineComponent : IOutlineComponent, IOutlineComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _distance = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Outline.Distance, Oxide.Ext.UiFramework.Json.JsonDefaults.Outline.FpDistance);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _useGraphicAlpha = new();

	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => _color.Value; set => _color.Value = value; }
	public partial UnityEngine.Vector2 Distance { get => _distance.Value; set => _distance.Value = value; }
	public partial bool UseGraphicAlpha { get => _useGraphicAlpha.Value; set => _useGraphicAlpha.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IOutlineComponentTrackable.Color => _color;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> IOutlineComponentTrackable.Distance => _distance;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IOutlineComponentTrackable.UseGraphicAlpha => _useGraphicAlpha;

	public Oxide.Ext.UiFramework.Components.OutlineComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.OutlineComponent SetDistance(UnityEngine.Vector2 distance)
	{
		Distance = distance;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.OutlineComponent SetUseGraphicAlpha(bool useGraphicAlpha)
	{
		UseGraphicAlpha = useGraphicAlpha;
		return this;
	}
	public IOutlineComponentTrackable AsTrackable() => this;
	public override bool HasChanged() => false || (_color.HasChanged || _distance.HasChanged || _useGraphicAlpha.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_color.ResetHasChanged();
		_distance.ResetHasChanged();
		_useGraphicAlpha.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_color.Reset();
		_distance.Reset();
		_useGraphicAlpha.Reset();
	}
}


