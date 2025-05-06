﻿using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiItemIcon : BaseUiOutline
{
    public readonly ItemIconComponent Icon = new();

    public static UiItemIcon Create(in UiPosition pos, in UiOffset offset, UiColor color, int itemId, ulong skinId = 0)
    {
        UiItemIcon icon = CreateBase<UiItemIcon>(pos, offset);
        icon.Icon.Color = color;
        icon.Icon.ItemId = itemId;
        icon.Icon.SkinId = skinId;
        return icon;
    }
        
    public void SetFadeIn(float duration)
    {
        Icon.FadeIn = duration;
    }

    protected override void WriteComponents(JsonFrameworkWriter writer)
    {
        Icon.WriteComponent(writer);
        base.WriteComponents(writer);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Icon.Reset();
    }
}