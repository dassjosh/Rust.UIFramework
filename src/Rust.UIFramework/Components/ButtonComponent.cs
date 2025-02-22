using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ButtonComponent : BaseImageComponent
{
    public string Command;
    public string Close;
    public Image.Type ImageType;
    public ColorBlockComponent ColorBlock { get; internal set; }

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Button.Type);
        writer.AddTextField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
        writer.AddField(JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
        writer.AddField(JsonDefaults.Image.ImageType, ImageType);
        ColorBlock?.WriteComponent(writer);
        base.WriteComponent(writer);
        writer.WriteEndObject();
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