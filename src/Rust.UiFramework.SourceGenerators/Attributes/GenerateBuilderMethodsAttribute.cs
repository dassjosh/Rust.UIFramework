using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal sealed class GenerateBuilderMethodsAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class GenerateBuilderMethodAttribute : Attribute;