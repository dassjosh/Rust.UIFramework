using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ISlotComponent : IBaseTypedComponent
{
	string Filter { get; set; }

	Oxide.Ext.UiFramework.Components.SlotComponent SetFilter(string filter);
}


