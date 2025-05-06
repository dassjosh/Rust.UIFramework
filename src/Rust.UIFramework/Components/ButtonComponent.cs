using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ButtonComponent : BaseImageComponent
{
    private const string Type = "UnityEngine.UI.Button";

    public string Command;
    public string Close;
    public Image.Type ImageType;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
        writer.AddField(JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
        writer.AddField(JsonDefaults.Image.ImageType, ImageType);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        Command = null;
        Close = null;
        ImageType = Image.Type.Simple;
    }
}