using System;

namespace Oxide.Ext.UiFramework.Enums;

[Flags]
public enum InputMode : byte
{
    Default = 0,
    ReadOnly = 1 << 0,
    NeedsKeyboard = 1 << 1,
    HudNeedsKeyboard = 1 << 2,
    Password = 1 << 3,
    AutoFocus = 1 << 4
}