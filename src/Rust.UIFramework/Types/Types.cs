namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiTexture(string Texture)
{
    public bool IsValid => !string.IsNullOrEmpty(Texture);
    
    public static explicit operator UiTexture(string texture) => new(texture);
}

public readonly record struct UiSprite(string Sprite)
{
    public bool IsValid => !string.IsNullOrEmpty(Sprite);
    public static explicit operator UiSprite(string sprite) => new(sprite);
}