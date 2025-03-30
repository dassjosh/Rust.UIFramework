using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ButtonComponent : ImageComponent
{
    public string Command;
    public string Close;
    public ColorBlockComponent ColorBlock { get; internal set; }
    public override Utf8String Type => JsonDefaults.Button.Type;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddCommand(JsonDefaults.Common.CommandName, Command);
        writer.AddField(JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
        ColorBlock?.WriteComponent(writer);
        base.WriteComponentFields(writer);
    }
    
    public override void Reset()
    {
        base.Reset();
        ColorBlock?.Dispose();
        ColorBlock = null;
        Command = null;
        Close = null;
        ImageType = Image.Type.Simple;
    }
}