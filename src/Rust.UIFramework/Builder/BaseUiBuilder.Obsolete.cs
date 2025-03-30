using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    public UiPanel Panel(in UiReference parent, in UiPosition pos, UiColor color) => Panel(parent, pos, default, color);
    public UiButton CommandButton(in UiReference parent, in UiPosition pos, UiColor color, string command) => CommandButton(parent, pos, default, color, command);
    public UiButton CloseButton(in UiReference parent, in UiPosition pos, UiColor color, string close) => CloseButton(parent, pos, default, color, close);
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, in UiOffset offset, string sprite) => ImageSprite(parent, pos, offset, sprite, UiColor.White);
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, string sprite, UiColor color) => ImageSprite(parent, pos, default, sprite, color);
    public UiImage ImageSprite(in UiReference parent, in UiPosition pos, string sprite) => ImageSprite(parent, pos, sprite, UiColor.White);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, ulong skinId) => ItemIcon(parent, pos, offset, itemId, skinId, UiColor.White);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, in UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColor.White);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default, itemId, skinId);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default, itemId, color);
    public UiItemIcon ItemIcon(in UiReference parent, in UiPosition pos, int itemId) => ItemIcon(parent, pos, default, itemId);
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, in UiOffset offset, ulong steamId) => PlayerAvatar(parent, pos, offset, steamId, AvatarType.Medium, UiColor.White);
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId) => PlayerAvatar(parent, pos, default, steamId);
    public UiPlayerAvatar PlayerAvatar(in UiReference parent, in UiPosition pos, ulong steamId, UiColor color) => PlayerAvatar(parent, pos, default, steamId, AvatarType.Medium, color);
    public UiLabel Label(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default, text, fontSize, textColor, align);
    // public UiLabelBackground LabelBackground(in UiReference parent, in UiPosition pos, in UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
    // {
    //     UiLabelBackground control = UiLabelBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, align);
    //     AddControl(control);
    //     return control;
    // }
    //
    // public UiLabelBackground LabelBackground(in UiReference parent, in UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default, text, fontSize, textColor, backgroundColor, align);
    //
    public UiRawImage ImageFileStorage(in UiReference parent, in UiPosition pos, in UiOffset offset, string png, UiColor? color = null) => RawImage(parent, pos, offset, png, color);
}