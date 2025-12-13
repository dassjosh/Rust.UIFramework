using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class InputComponent : IInputComponent, IInputComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<int> _charsLimit = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Input.CharacterLimit);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<string> _command = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.InputMode> _mode = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Input.Mode);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.InputField.LineType> _lineType = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Input.LineType);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholder = new();

	public partial int CharsLimit { get => _charsLimit.Value; set => _charsLimit.Value = value; }
	public partial string Command { get => _command.Value; set => _command.Value = value; }
	public partial Oxide.Ext.UiFramework.Enums.InputMode Mode { get => _mode.Value; set => _mode.Value = value; }
	public partial UnityEngine.UI.InputField.LineType LineType { get => _lineType.Value; set => _lineType.Value = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference Placeholder { get => _placeholder.Value; set => _placeholder.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<int> IInputComponentTrackable.CharsLimit => _charsLimit;
	Oxide.Ext.UiFramework.Types.Tracked<string> IInputComponentTrackable.Command => _command;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.InputMode> IInputComponentTrackable.Mode => _mode;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.InputField.LineType> IInputComponentTrackable.LineType => _lineType;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> IInputComponentTrackable.Placeholder => _placeholder;

	public Oxide.Ext.UiFramework.Components.InputComponent SetCharsLimit(int charsLimit)
	{
		CharsLimit = charsLimit;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetCommand(string command)
	{
		Command = command;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetMode(Oxide.Ext.UiFramework.Enums.InputMode mode)
	{
		Mode = mode;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetLineType(UnityEngine.UI.InputField.LineType lineType)
	{
		LineType = lineType;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetPlaceholder(in Oxide.Ext.UiFramework.UiElements.UiReference placeholder)
	{
		Placeholder = placeholder;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetIsPassword(bool isPassword)
	{
		IsPassword = isPassword;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetNeedsKeyboard(bool needsKeyboard)
	{
		NeedsKeyboard = needsKeyboard;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetHudNeedsKeyboard(bool hudNeedsKeyboard)
	{
		HudNeedsKeyboard = hudNeedsKeyboard;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetAutoFocus(bool autoFocus)
	{
		AutoFocus = autoFocus;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.InputComponent SetReadOnly(bool readOnly)
	{
		ReadOnly = readOnly;
		return this;
	}
	public new IInputComponentTrackable AsTrackable() => this;
	public override bool HasChanged() => false || (_charsLimit.HasChanged || _command.HasChanged || _mode.HasChanged || _lineType.HasChanged || _placeholder.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_charsLimit.ResetHasChanged();
		_command.ResetHasChanged();
		_mode.ResetHasChanged();
		_lineType.ResetHasChanged();
		_placeholder.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_charsLimit.Reset();
		_command.Reset();
		_mode.Reset();
		_lineType.Reset();
		_placeholder.Reset();
	}
}


