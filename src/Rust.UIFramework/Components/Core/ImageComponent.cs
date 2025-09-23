using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ImageComponent : CoreComponent
{
    public UiColor Color;
    public float FadeIn;
    public string Sprite;
    public string Material;
    public Image.Type ImageType;
    public UiReference PlaceholderFor;
    
    public override Utf8String Type => JsonDefaults.Image.Type;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
        writer.AddField(JsonDefaults.Image.ImageTypeName, ImageType, JsonDefaults.Image.ImageType);
        if (PlaceholderFor.IsValidReference())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }

    public override void Reset()
    {
        base.Reset();
        Color = JsonDefaults.Color.ColorValue;
        FadeIn = 0;
        Sprite = null;
        Material = null;
        ImageType = Image.Type.Simple;
        PlaceholderFor = default;
    }
}