using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ScrollViewComponent : IScrollViewComponent, IScrollViewComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ScrollRect.MovementType> _movementType = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.MovementType);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _elasticity = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.Elasticity);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _inertia = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.Inertia);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _decelerationRate = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.DecelerationRate);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _scrollSensitivity = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.ScrollSensitivity);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _horizontalScrollProgress = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.HorizontalScrollProgress);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _verticalScrollProgress = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.VerticalScrollProgress);

	public partial UnityEngine.UI.ScrollRect.MovementType MovementType { get => _movementType.Value; set => _movementType.Value = value; }
	public partial float Elasticity { get => _elasticity.Value; set => _elasticity.Value = value; }
	public partial bool Inertia { get => _inertia.Value; set => _inertia.Value = value; }
	public partial float DecelerationRate { get => _decelerationRate.Value; set => _decelerationRate.Value = value; }
	public partial float ScrollSensitivity { get => _scrollSensitivity.Value; set => _scrollSensitivity.Value = value; }
	public partial float HorizontalScrollProgress { get => _horizontalScrollProgress.Value; set => _horizontalScrollProgress.Value = value; }
	public partial float VerticalScrollProgress { get => _verticalScrollProgress.Value; set => _verticalScrollProgress.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ScrollRect.MovementType> IScrollViewComponentTrackable.MovementType => _movementType;
	Oxide.Ext.UiFramework.Types.Tracked<float> IScrollViewComponentTrackable.Elasticity => _elasticity;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IScrollViewComponentTrackable.Inertia => _inertia;
	Oxide.Ext.UiFramework.Types.Tracked<float> IScrollViewComponentTrackable.DecelerationRate => _decelerationRate;
	Oxide.Ext.UiFramework.Types.Tracked<float> IScrollViewComponentTrackable.ScrollSensitivity => _scrollSensitivity;
	Oxide.Ext.UiFramework.Types.Tracked<float> IScrollViewComponentTrackable.HorizontalScrollProgress => _horizontalScrollProgress;
	Oxide.Ext.UiFramework.Types.Tracked<float> IScrollViewComponentTrackable.VerticalScrollProgress => _verticalScrollProgress;

	public Oxide.Ext.UiFramework.Components.ScrollViewComponent SetMovementType(UnityEngine.UI.ScrollRect.MovementType movementType)
	{
		MovementType = movementType;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewComponent SetElasticity(float elasticity)
	{
		Elasticity = elasticity;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewComponent SetInertia(bool inertia)
	{
		Inertia = inertia;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewComponent SetDecelerationRate(float decelerationRate)
	{
		DecelerationRate = decelerationRate;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewComponent SetScrollSensitivity(float scrollSensitivity)
	{
		ScrollSensitivity = scrollSensitivity;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewComponent SetHorizontalScrollProgress(float horizontalScrollProgress)
	{
		HorizontalScrollProgress = horizontalScrollProgress;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewComponent SetVerticalScrollProgress(float verticalScrollProgress)
	{
		VerticalScrollProgress = verticalScrollProgress;
		return this;
	}
	public IScrollViewComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_movementType.HasChanged || _elasticity.HasChanged || _inertia.HasChanged || _decelerationRate.HasChanged || _scrollSensitivity.HasChanged || _horizontalScrollProgress.HasChanged || _verticalScrollProgress.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_movementType.ResetHasChanged();
		_elasticity.ResetHasChanged();
		_inertia.ResetHasChanged();
		_decelerationRate.ResetHasChanged();
		_scrollSensitivity.ResetHasChanged();
		_horizontalScrollProgress.ResetHasChanged();
		_verticalScrollProgress.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_movementType.Reset();
		_elasticity.Reset();
		_inertia.Reset();
		_decelerationRate.Reset();
		_scrollSensitivity.Reset();
		_horizontalScrollProgress.Reset();
		_verticalScrollProgress.Reset();
	}
}


