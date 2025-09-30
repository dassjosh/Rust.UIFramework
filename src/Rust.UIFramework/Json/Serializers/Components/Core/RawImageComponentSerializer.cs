using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Json;

public class RawImageComponentSerializer : RawImageComponentSerializer<RawImageComponent>;

public abstract class RawImageComponentSerializer<T> : CoreComponentSerializer<T> where T : RawImageComponent, new()
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.MaterialName, component.Material, JsonDefaults.BaseImage.Material);
        writer.AddField(JsonDefaults.Color.ColorName, component.Color);
        
        if (component.PlaceholderFor.IsValidReference())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, component.PlaceholderFor.Name);
        }

        string image = component.Image;
        if (!string.IsNullOrEmpty(image) && image != defaults.Image)
        {
            if (image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                writer.AddFieldRaw(JsonDefaults.Image.UrlName, image);
            } 
            else if (uint.TryParse(image, out uint _))
            {
                writer.AddFieldRaw(JsonDefaults.Image.PngName, image);
            }
            else if(image.StartsWith("assets/", StringComparison.OrdinalIgnoreCase))
            {
                writer.AddField(JsonDefaults.BaseImage.SpriteName, image, JsonDefaults.RawImage.TextureValue);
            }
            else
            {
                UiFrameworkExtension.GlobalLogger.Warning("[UiFramework] RawImage.Image '{0}' is not a valid image. Should be a URL, PNG ID, or Texture.", image);
            }
        }
    }
}