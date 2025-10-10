using System;

namespace Oxide.Ext.UiFramework;

[Obsolete("Check the sub class to determine what to replace this with.")]
public class UiConstants
{
    [Obsolete($"Use {nameof(RpcFunctions)} without UiConstants prefix.")]
    public static class RpcFunctions
    {
        [Obsolete("Use RpcFunctions.AddUi instead.")] public const string AddUiFunc = "AddUI";
        [Obsolete("Use RpcFunctions.DestroyUi instead.")] public const string DestroyUiFunc = "DestroyUI";
    }

    [Obsolete("Use UiMaterials instead")]
    public static class Materials
    {
        [Obsolete("Use UiMaterials.Content.Ui.UiBackgroundBlurInGameMenu Instead")] 
        public const string InGameBlur = "assets/content/ui/uibackgroundblur-ingamemenu.mat";
        [Obsolete("Use UiMaterials.Content.Ui.UiBackgroundBlurNotice Instead")] 
        public const string NoticeBlur = "assets/content/ui/uibackgroundblur-notice.mat";
        [Obsolete("Use UiMaterials.Content.Ui.UiBackgroundBlur Instead")] 
        public const string BackgroundBlur = "assets/content/ui/uibackgroundblur.mat";
        [Obsolete("Use UiMaterials.Icons.IconMaterial Instead")] 
        public const string Icon = "assets/icons/iconmaterial.mat";
    }

    [Obsolete("Use UiSprites instead")]
    public static class Sprites
    {
        [Obsolete("Use UiSprites.Content.Ui.UiBackgroundTile Instead")] 
        public const string Default = "Assets/Content/UI/UI.Background.Tile.psd";
        [Obsolete("Use UiSprites.Content.Texures.Generic.FullTransparent Instead")] 
        public const string Transparent = "Assets/Content/Textures/Generic/fulltransparent.tga";
        [Obsolete("Use UiSprites.Content.Ui.UiRounded Instead")] 
        public const string RoundedBackground1 = "Assets/Content/UI/UI.Rounded.tga";
        [Obsolete("Use UiSprites.Content.Ui.UiBackgroundRounded Instead")] 
        public const string RoundedBackground2 = "Assets/Content/UI/UI.Background.Rounded.png";
        [Obsolete("Use UiSprites.Content.Ui.UiGradientUp Instead")] 
        public const string GradientUp = "Assets/Content/UI/UI.Gradient.Up.psd";
        [Obsolete("Use UiSprites.Content.Ui.UiBackgroundTransparentLinear Instead")] 
        public const string BackgroundTransparentLinear = "Assets/Content/UI/UI.Background.Transparent.Linear.png";
        [Obsolete("Use UiSprites.Content.Ui.UiBackgroundTransparentLinearLtr Instead")] 
        public const string BackgroundTransparentLinearLtr = "Assets/Content/UI/UI.Background.Transparent.LinearLTR.png";
        [Obsolete("Use UiSprites.Content.Ui.UiWhite Instead")] 
        public const string White = "Assets/Content/UI/UI.White.tga";
        [Obsolete("Use UiSprites.Icons.CircleClosedWhite Instead")] 
        public const string Circle = "Assets/Icons/circle_closed_white.png";
        [Obsolete("Use UiSprites.Icons.CircleClosedWhiteToEdge Instead")] 
        public const string CircleToEdge = "Assets/Icons/circle_closed_white_toEdge.png";
        [Obsolete("Use UiSprites.Icons.UiBox Instead")] 
        public const string Box = "Assets/Content/UI/UI.Box.tga";
        [Obsolete("Use UiSprites.Icons.UiBoxSharp Instead")] 
        public const string BoxSharp = "Assets/Content/UI/UI.Box.Sharp.tga";
    }
}