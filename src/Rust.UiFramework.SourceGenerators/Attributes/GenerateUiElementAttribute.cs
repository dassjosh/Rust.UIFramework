using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal sealed class GenerateUiElementAttribute(Type interfaceType) : Attribute
{
    public Type InterfaceType { get; } = interfaceType;
}
