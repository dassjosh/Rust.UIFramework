using Rust.UiFramework.SourceGenerators.Builder.Enums;

namespace Rust.UiFramework.SourceGenerators.Builder.Interfaces;

internal interface IParameterModifier
{
    ParameterModifiers Modifiers { get; set; }
}