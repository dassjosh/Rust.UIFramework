using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IInputComponentTrackable : ITextComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<int> CharsLimit { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Command { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.InputMode> Mode { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.InputField.LineType> LineType { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> Placeholder { get; }
}


