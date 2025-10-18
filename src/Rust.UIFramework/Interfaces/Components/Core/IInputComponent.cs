using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IInputComponent : ITextComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Input), nameof(JsonDefaults.Input.CharacterLimit))]
    int CharsLimit { get; set; }
    string Command { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Input), nameof(JsonDefaults.Input.Mode))]
    InputMode Mode { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Input), nameof(JsonDefaults.Input.LineType))]
    InputField.LineType LineType { get; set; }
    UiReference Placeholder { get; set; }
}