using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IBaseTypedComponent
{
	bool Enabled { get; set; }

	Oxide.Ext.UiFramework.Components.BaseTypedComponent SetEnabled(bool enabled);
}


