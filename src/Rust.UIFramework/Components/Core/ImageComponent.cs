using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IImageComponent))]
public partial class ImageComponent : CoreComponent, IImageComponent, IGraphicalComponent
{
    public override Utf8String Type => JsonDefaults.Image.Type;
    public override ComponentType ComponentType => ComponentType.Image;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, _sprite, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, _fadeIn, mode);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, _material, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Image.ImageTypeName, _imageType, mode);
        writer.AddField(JsonDefaults.Image.FillCenterName, _fillCenter, mode);
        if (PlaceholderFor.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }
}