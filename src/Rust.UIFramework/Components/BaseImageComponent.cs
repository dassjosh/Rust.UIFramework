using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseImageComponent : BaseComponent
{
    public UiColor Color;
    public float FadeIn;
    public string Sprite;
    public string Material;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
        base.WriteComponent(writer);
    }

    public override void Reset()
    {
        base.Reset();
        Color = default;
        FadeIn = 0;
        Sprite = null;
        Material = null;
    }
}