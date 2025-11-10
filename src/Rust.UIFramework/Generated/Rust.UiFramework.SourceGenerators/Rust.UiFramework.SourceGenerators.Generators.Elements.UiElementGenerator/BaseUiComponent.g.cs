using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;
public partial class BaseUiComponent : IBaseUiComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _reference = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeOut = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Common.FadeOut);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.UpdateMode> _update = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _active = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Common.Active);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _enabled = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> _position = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> _offset = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> _positionPadding = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> _offsetPadding = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> _positionScale = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> _offsetScale = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiRotation> _rotation = new();

	public Oxide.Ext.UiFramework.UiElements.UiReference Reference { get; set; }
	public float FadeOut { get => _fadeOut.Value; set => _fadeOut.Value = value; }
	public Oxide.Ext.UiFramework.Enums.UpdateMode Update { get; set; }
	public bool Active { get => _active.Value; set => _active.Value = value; }
	public bool Enabled { get => Component.Enabled; set => Component.Enabled = value; }
	public Oxide.Ext.UiFramework.Positions.UiPosition Position { get => RectTransform.Position; set => RectTransform.Position = value; }
	public Oxide.Ext.UiFramework.Offsets.UiOffset Offset { get => RectTransform.Offset; set => RectTransform.Offset = value; }
	public Oxide.Ext.UiFramework.Types.UiPadding PositionPadding { get => RectTransform.PositionPadding; set => RectTransform.PositionPadding = value; }
	public Oxide.Ext.UiFramework.Types.UiPadding OffsetPadding { get => RectTransform.OffsetPadding; set => RectTransform.OffsetPadding = value; }
	public Oxide.Ext.UiFramework.Types.UiScale PositionScale { get => RectTransform.PositionScale; set => RectTransform.PositionScale = value; }
	public Oxide.Ext.UiFramework.Types.UiScale OffsetScale { get => RectTransform.OffsetScale; set => RectTransform.OffsetScale = value; }
	public Oxide.Ext.UiFramework.Types.UiRotation Rotation { get => RectTransform.Rotation; set => RectTransform.Rotation = value; }
	Oxide.Ext.UiFramework.Types.Tracked<float> IBaseUiComponentTrackable.FadeOut => _fadeOut;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IBaseUiComponentTrackable.Active => _active;
}


