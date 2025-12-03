using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface ILayoutElementComponent : IBaseTypedComponent
{
	float PreferredWidth { get; set; }
	float PreferredHeight { get; set; }
	float MinWidth { get; set; }
	float MinHeight { get; set; }
	float FlexibleWidth { get; set; }
	float FlexibleHeight { get; set; }
	bool IgnoreLayout { get; set; }
}


