using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(InputComponentSerializer))]
public class InputComponent : TextComponent
{
    public int CharsLimit;
    public string Command;
    public InputMode Mode;
    public InputField.LineType LineType;
    public UiReference Placeholder;

    public override Utf8String Type => JsonDefaults.Input.Type;
    public override ComponentType ComponentType => ComponentType.Input;
    
    public bool HasMode(InputMode mode) => (Mode & mode) == mode;

    [Obsolete("Use SetMode on UiInput instead")]
    public void SetMode(InputMode mode, bool enabled)
    {
        if (enabled)
        {
            Mode |= mode;
        }
        else
        {
            Mode &= ~mode;
        }
    }

    public override void Reset()
    {
        base.Reset();
        CharsLimit = JsonDefaults.Input.CharacterLimit;
        Command = null;
        Mode = JsonDefaults.Input.Mode;
        LineType = JsonDefaults.Input.LineType;
        Placeholder = default;
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        InputComponent typedOther = (InputComponent)other!;
        return CharsLimit == typedOther.CharsLimit
               && Command == typedOther.Command 
               && Mode == typedOther.Mode 
               && LineType == typedOther.LineType 
               && Placeholder == typedOther.Placeholder;
    }
}