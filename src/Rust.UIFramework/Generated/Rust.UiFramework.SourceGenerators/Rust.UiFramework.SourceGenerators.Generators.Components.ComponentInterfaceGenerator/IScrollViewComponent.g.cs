using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IScrollViewComponent : IBaseTypedComponent
{
	UnityEngine.UI.ScrollRect.MovementType MovementType { get; set; }
	float Elasticity { get; set; }
	bool Inertia { get; set; }
	float DecelerationRate { get; set; }
	float ScrollSensitivity { get; set; }
	float HorizontalScrollProgress { get; set; }
	float VerticalScrollProgress { get; set; }
}


