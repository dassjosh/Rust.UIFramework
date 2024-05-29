using System;

namespace Oxide.Ext.UiFramework.Enums;

[Flags]
public enum BorderMode : byte
{
    Top = 1 << 0,
    Left = 1 << 1,
    Bottom = 1 << 2,
    Right = 1 << 3,
    All = Top | Left | Bottom | Right
}