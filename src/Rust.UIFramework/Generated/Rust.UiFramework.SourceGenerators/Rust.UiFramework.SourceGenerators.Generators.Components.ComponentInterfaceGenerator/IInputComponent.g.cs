using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IInputComponent : ITextComponent
{
	int CharsLimit { get; set; }
	string Command { get; set; }
	Oxide.Ext.UiFramework.Enums.InputMode Mode { get; set; }
	UnityEngine.UI.InputField.LineType LineType { get; set; }
	Oxide.Ext.UiFramework.UiElements.UiReference Placeholder { get; set; }
}


