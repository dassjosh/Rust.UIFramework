using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class InputComponent : IInputComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _charsLimit = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Input.CharacterLimit);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _command = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.InputMode> _mode = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Input.Mode);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.InputField.LineType> _lineType = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Input.LineType);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholder = new();

	public int CharsLimit { get => _charsLimit.Value; set => _charsLimit.Value = value; }
	public string Command { get => _command.Value; set => _command.Value = value; }
	public Oxide.Ext.UiFramework.Enums.InputMode Mode { get => _mode.Value; set => _mode.Value = value; }
	public UnityEngine.UI.InputField.LineType LineType { get => _lineType.Value; set => _lineType.Value = value; }
	public Oxide.Ext.UiFramework.UiElements.UiReference Placeholder { get => _placeholder.Value; set => _placeholder.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<int> IInputComponentTrackable.CharsLimit => _charsLimit;
	Oxide.Ext.UiFramework.Types.Tracked<string> IInputComponentTrackable.Command => _command;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.InputMode> IInputComponentTrackable.Mode => _mode;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.InputField.LineType> IInputComponentTrackable.LineType => _lineType;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> IInputComponentTrackable.Placeholder => _placeholder;

	public new IInputComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_charsLimit.HasChanged || _command.HasChanged || _mode.HasChanged || _lineType.HasChanged || _placeholder.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_charsLimit.ResetHasChanged();
		_command.ResetHasChanged();
		_mode.ResetHasChanged();
		_lineType.ResetHasChanged();
		_placeholder.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_charsLimit.Reset();
		_command.Reset();
		_mode.Reset();
		_lineType.Reset();
		_placeholder.Reset();
	}
}


