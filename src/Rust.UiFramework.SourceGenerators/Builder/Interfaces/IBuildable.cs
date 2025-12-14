namespace Rust.UiFramework.SourceGenerators.Builder.Interfaces;

internal interface IBuildable : IConditional
{
    string Build(int indent);
}