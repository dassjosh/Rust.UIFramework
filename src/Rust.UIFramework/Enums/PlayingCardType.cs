using System;

namespace Oxide.Ext.UiFramework.Enums;

[Flags]
public enum PlayingCardType : byte
{
    Normal = 0,
    Small = 1 << 0,
    Transparent = 1 << 1
}