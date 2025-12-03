using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiInputTrackable : IBaseUiComponentTrackable
{
	IInputComponentTrackable Input { get; }
}


