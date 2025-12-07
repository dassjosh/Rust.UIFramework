using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiScrollView : IUiScrollView, IUiScrollViewTrackable
{
	public partial UnityEngine.UI.ScrollRect.MovementType MovementType { get => ScrollView.MovementType; set => ScrollView.MovementType = value; }
	public partial float Elasticity { get => ScrollView.Elasticity; set => ScrollView.Elasticity = value; }
	public partial bool Inertia { get => ScrollView.Inertia; set => ScrollView.Inertia = value; }
	public partial float DecelerationRate { get => ScrollView.DecelerationRate; set => ScrollView.DecelerationRate = value; }
	public partial float ScrollSensitivity { get => ScrollView.ScrollSensitivity; set => ScrollView.ScrollSensitivity = value; }
	public partial float HorizontalScrollProgress { get => ScrollView.HorizontalScrollProgress; set => ScrollView.HorizontalScrollProgress = value; }
	public partial float VerticalScrollProgress { get => ScrollView.VerticalScrollProgress; set => ScrollView.VerticalScrollProgress = value; }
	public partial Oxide.Ext.UiFramework.Positions.UiPosition ContentPosition { get => GetOrCreateContentTransform().Position; set => GetOrCreateContentTransform().Position = value; }
	public partial Oxide.Ext.UiFramework.Offsets.UiOffset ContentOffset { get => GetOrCreateContentTransform().Offset; set => GetOrCreateContentTransform().Offset = value; }
	public partial UnityEngine.Vector2 ContentPivot { get => GetOrCreateContentTransform().Pivot; set => GetOrCreateContentTransform().Pivot = value; }
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


