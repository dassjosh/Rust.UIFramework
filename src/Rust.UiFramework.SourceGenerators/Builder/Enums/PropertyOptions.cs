using System;

namespace Rust.UiFramework.SourceGenerators.Builder.Enums;

[Flags]
public enum PropertyOptions : byte
{
    None = 0,
    Get = 1 << 0,
    Set = 1 << 1
}