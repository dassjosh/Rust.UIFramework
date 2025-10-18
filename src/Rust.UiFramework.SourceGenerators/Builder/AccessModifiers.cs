using System;

namespace Rust.UiFramework.SourceGenerators.Builder;

[Flags]
public enum AccessModifiers : byte
{
    None = 0,
    Private = 1 << 0,
    Protected = 1 << 1,
    Internal = 1 << 2,
    Public = 1 << 3,
}