using System;

namespace Oxide.Ext.UiFramework.Enums;

[Flags]
public enum ScrollbarTypes : byte
{
    None = 0,
    Horizontal = 1 << 0,
    Vertical = 1 << 1,
    Both = Horizontal | Vertical
}