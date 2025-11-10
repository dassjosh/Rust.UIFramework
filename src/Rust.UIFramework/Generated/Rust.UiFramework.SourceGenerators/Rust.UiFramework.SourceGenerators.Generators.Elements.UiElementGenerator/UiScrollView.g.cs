using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;
public partial class UiScrollView : IUiScrollViewTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ScrollRect.MovementType> _movementType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _elasticity = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _inertia = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _decelerationRate = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _scrollSensitivity = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _horizontalScrollProgress = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _verticalScrollProgress = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> _contentPosition = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> _contentOffset = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _contentPivot = new();

	public UnityEngine.UI.ScrollRect.MovementType MovementType { get => ScrollView.MovementType; set => ScrollView.MovementType = value; }
	public float Elasticity { get => ScrollView.Elasticity; set => ScrollView.Elasticity = value; }
	public bool Inertia { get => ScrollView.Inertia; set => ScrollView.Inertia = value; }
	public float DecelerationRate { get => ScrollView.DecelerationRate; set => ScrollView.DecelerationRate = value; }
	public float ScrollSensitivity { get => ScrollView.ScrollSensitivity; set => ScrollView.ScrollSensitivity = value; }
	public float HorizontalScrollProgress { get => ScrollView.HorizontalScrollProgress; set => ScrollView.HorizontalScrollProgress = value; }
	public float VerticalScrollProgress { get => ScrollView.VerticalScrollProgress; set => ScrollView.VerticalScrollProgress = value; }
	public Oxide.Ext.UiFramework.Positions.UiPosition ContentPosition { get => GetOrCreateContentTransform().Position; set => GetOrCreateContentTransform().Position = value; }
	public Oxide.Ext.UiFramework.Offsets.UiOffset ContentOffset { get => GetOrCreateContentTransform().Offset; set => GetOrCreateContentTransform().Offset = value; }
	public UnityEngine.Vector2 ContentPivot { get => GetOrCreateContentTransform().Pivot; set => GetOrCreateContentTransform().Pivot = value; }
	IScrollViewComponentTrackable IUiScrollViewTrackable.ScrollView => ScrollView.AsTrackable();

	public IUiScrollViewTrackable AsTrackable() => this;
	public UiScrollView SetMovementType(UnityEngine.UI.ScrollRect.MovementType movementType)
	{
		MovementType = movementType;
		return this;
	}
	public UiScrollView SetElasticity(float elasticity)
	{
		Elasticity = elasticity;
		return this;
	}
	public UiScrollView SetInertia(bool inertia)
	{
		Inertia = inertia;
		return this;
	}
	public UiScrollView SetDecelerationRate(float decelerationRate)
	{
		DecelerationRate = decelerationRate;
		return this;
	}
	public UiScrollView SetScrollSensitivity(float scrollSensitivity)
	{
		ScrollSensitivity = scrollSensitivity;
		return this;
	}
	public UiScrollView SetHorizontalScrollProgress(float horizontalScrollProgress)
	{
		HorizontalScrollProgress = horizontalScrollProgress;
		return this;
	}
	public UiScrollView SetVerticalScrollProgress(float verticalScrollProgress)
	{
		VerticalScrollProgress = verticalScrollProgress;
		return this;
	}
	public UiScrollView SetContentPosition(in Oxide.Ext.UiFramework.Positions.UiPosition contentPosition)
	{
		ContentPosition = contentPosition;
		return this;
	}
	public UiScrollView SetContentOffset(in Oxide.Ext.UiFramework.Offsets.UiOffset contentOffset)
	{
		ContentOffset = contentOffset;
		return this;
	}
	public UiScrollView SetContentPivot(in UnityEngine.Vector2 contentPivot)
	{
		ContentPivot = contentPivot;
		return this;
	}
}


