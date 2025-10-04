using System;

namespace Oxide.Ext.UiFramework.Builder;

[Flags]
public enum UiDebugModes : byte
{
    Console = 1 << 0,
    File = 1 << 1,
    Append = 1 << 2,
}