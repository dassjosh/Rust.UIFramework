using System;

namespace Oxide.Ext.UiFramework.Enums;

[Flags]
public enum AutoSizeDirection : byte
{
    Horizontal = 1 << 0,
    Vertical = 1 << 1,
    Both = Horizontal | Vertical
}