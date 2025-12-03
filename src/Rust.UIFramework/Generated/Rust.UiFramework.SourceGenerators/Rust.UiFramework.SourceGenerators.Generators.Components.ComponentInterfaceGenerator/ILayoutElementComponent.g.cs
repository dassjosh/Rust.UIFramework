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

	Oxide.Ext.UiFramework.Components.LayoutElementComponent SetPreferredWidth(float preferredWidth);
	Oxide.Ext.UiFramework.Components.LayoutElementComponent SetPreferredHeight(float preferredHeight);
	Oxide.Ext.UiFramework.Components.LayoutElementComponent SetMinWidth(float minWidth);
	Oxide.Ext.UiFramework.Components.LayoutElementComponent SetMinHeight(float minHeight);
	Oxide.Ext.UiFramework.Components.LayoutElementComponent SetFlexibleWidth(float flexibleWidth);
	Oxide.Ext.UiFramework.Components.LayoutElementComponent SetFlexibleHeight(float flexibleHeight);
	Oxide.Ext.UiFramework.Components.LayoutElementComponent SetIgnoreLayout(bool ignoreLayout);
}


