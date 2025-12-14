using System;

namespace Rust.UiFramework.SourceGenerators.Builder;

[Flags]
public enum Keywords
{
    None        = 0,
    Static      = 1 << 0,
    Extern      = 1 << 1,
    Unsafe      = 1 << 2,
    Abstract    = 1 << 3,
    Virtual     = 1 << 4,
    Override    = 1 << 5,
    Sealed      = 1 << 6,
    Async       = 1 << 7,
    New         = 1 << 8,
    Readonly    = 1 << 9,
    Volatile    = 1 << 10,
    Const       = 1 << 11,
    Event       = 1 << 12,
    In          = 1 << 13,
    Out         = 1 << 14,
    Partial     = 1 << 15
}