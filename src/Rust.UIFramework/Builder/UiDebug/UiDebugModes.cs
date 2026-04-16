using System;

namespace Oxide.Ext.UiFramework.Builder;

[Flags]
public enum UiDebugModes : byte
{
    Console = 1 << 0,
    File = 1 << 1,
    AppendFile = 1 << 2,
}