using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ImageComponent : BaseImageComponent
{
    public string Png;
    public Image.Type ImageType;
    
    public override Utf8String Type => JsonDefaults.Image.Type;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Image.PngName, Png, null);
        writer.AddField(JsonDefaults.Image.ImageType, ImageType);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        Png = JsonDefaults.Common.NullValue;
        ImageType = Image.Type.Simple;
    }
}