using System;

namespace Rust.UiFramework.SourceGenerators.Builder;

[Flags]
public enum ParameterModifiers : byte
{
    None = 0,
    Ref = 1 << 0,
    Out = 1 << 1,
    In = 1 << 2,
    Readonly = 1 << 3,
}