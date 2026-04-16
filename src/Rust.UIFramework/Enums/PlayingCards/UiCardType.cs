using System;

namespace Oxide.Ext.UiFramework.Enums;

[Flags]
public enum UiCardType : byte
{
    Normal = 0,
    Small = 1 << 0,
    Transparent = 1 << 1,
    SmallTransparent = Small | Transparent
}