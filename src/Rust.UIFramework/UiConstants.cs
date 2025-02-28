using System;

namespace Oxide.Ext.UiFramework;

[Obsolete]
public class UiConstants
{
    [Obsolete]
    public static class RpcFunctions
    {
        [Obsolete] public const string AddUiFunc = "AddUI";
        [Obsolete] public const string DestroyUiFunc = "DestroyUI";
    }

    [Obsolete("Use UiMaterials instead")]
    public static class Materials
    {
        [Obsolete] public const string InGameBlur = "assets/content/ui/uibackgroundblur-ingamemenu.mat";
        [Obsolete] public const string NoticeBlur = "assets/content/ui/uibackgroundblur-notice.mat";
        [Obsolete] public const string BackgroundBlur = "assets/content/ui/uibackgroundblur.mat";
        [Obsolete] public const string Icon = "assets/icons/iconmaterial.mat";
    }

    [Obsolete("Use UiSprites instead")]
    public static class Sprites
    {
        [Obsolete] public const string Default = "Assets/Content/UI/UI.Background.Tile.psd";
        [Obsolete] public const string Transparent = "Assets/Content/Textures/Generic/fulltransparent.tga";
        [Obsolete] public const string RoundedBackground1 = "Assets/Content/UI/UI.Rounded.tga";
        [Obsolete] public const string RoundedBackground2 = "Assets/Content/UI/UI.Background.Rounded.png";
        [Obsolete] public const string GradientUp = "Assets/Content/UI/UI.Gradient.Up.psd";
        [Obsolete] public const string BackgroundTransparentLinear = "Assets/Content/UI/UI.Background.Transparent.Linear.png";
        [Obsolete] public const string BackgroundTransparentLinearLtr = "Assets/Content/UI/UI.Background.Transparent.LinearLTR.png";
        [Obsolete] public const string White = "Assets/Content/UI/UI.White.tga";
        [Obsolete] public const string Circle = "Assets/Icons/circle_closed_white.png";
        [Obsolete] public const string CircleToEdge = "Assets/Icons/circle_closed_white_toEdge.png";
        [Obsolete] public const string Box = "Assets/Content/UI/UI.Box.tga";
        [Obsolete] public const string BoxSharp = "Assets/Content/UI/UI.Box.Sharp.tga";
    }
}