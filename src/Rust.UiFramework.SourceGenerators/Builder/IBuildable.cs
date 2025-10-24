namespace Rust.UiFramework.SourceGenerators.Builder;

internal interface IBuildable : IConditional
{
    string Build(int indent);
}