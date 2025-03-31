using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ButtonComponent : ImageComponent
{
    public string Command;
    public ButtonType ButtonType;
    
    public ColorBlockComponent ColorBlock { get; internal set; }
    public override Utf8String Type => JsonDefaults.Button.Type;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        switch (ButtonType)
        {
            case ButtonType.Command:
                writer.AddCommand(JsonDefaults.Common.CommandName, Command);
                break;
            case ButtonType.Close:
                writer.AddField(JsonDefaults.Button.CloseName, Command, JsonDefaults.Common.NullValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        ColorBlock?.WriteComponent(writer);
        base.WriteComponentFields(writer);
    }
    
    public override void Reset()
    {
        base.Reset();
        ColorBlock?.Dispose();
        ColorBlock = null;
        Command = null;
        ButtonType = default;
        ImageType = Image.Type.Simple;
    }
}