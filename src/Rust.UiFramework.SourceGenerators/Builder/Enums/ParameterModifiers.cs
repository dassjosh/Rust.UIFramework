using System;

namespace Rust.UiFramework.SourceGenerators.Builder.Enums;

[Flags]
public enum ParameterModifiers : byte
{
    None = 0,
    Ref = 1 << 0,
    Out = 1 << 1,
    In = 1 << 2,
    Readonly = 1 << 3,
    This = 1 << 4
}