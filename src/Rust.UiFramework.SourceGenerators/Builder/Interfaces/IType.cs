using Rust.UiFramework.SourceGenerators.Builder.Enums;

namespace Rust.UiFramework.SourceGenerators.Builder.Interfaces;

internal interface IType
{
    BuilderType BuilderType { get; set; }
}