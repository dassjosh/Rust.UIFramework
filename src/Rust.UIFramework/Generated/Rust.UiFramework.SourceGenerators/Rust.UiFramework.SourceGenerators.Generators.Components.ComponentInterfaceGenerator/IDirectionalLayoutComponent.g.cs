using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IDirectionalLayoutComponent : IBaseLayoutComponent
{
	float Spacing { get; set; }
	bool ChildForceExpandWidth { get; set; }
	bool ChildForceExpandHeight { get; set; }
	bool ChildControlWidth { get; set; }
	bool ChildControlHeight { get; set; }
	bool ChildScaleWidth { get; set; }
	bool ChildScaleHeight { get; set; }
	Oxide.Ext.UiFramework.Enums.LayoutDirection Direction { get; set; }
}


