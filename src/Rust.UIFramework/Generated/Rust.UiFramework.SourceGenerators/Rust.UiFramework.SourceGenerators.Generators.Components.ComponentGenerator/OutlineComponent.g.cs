using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class OutlineComponent : IOutlineComponent, IOutlineComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _distance = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Outline.Distance, Oxide.Ext.UiFramework.Json.JsonDefaults.Outline.FpDistance);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _useGraphicAlpha = new();

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
	public Oxide.Ext.UiFramework.Components.OutlineComponent SetDistance(in UnityEngine.Vector2 distance)
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
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_color.HasChanged || _distance.HasChanged || _useGraphicAlpha.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_color.ResetHasChanged();
		_distance.ResetHasChanged();
		_useGraphicAlpha.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_color.Reset();
		_distance.Reset();
		_useGraphicAlpha.Reset();
	}
}


