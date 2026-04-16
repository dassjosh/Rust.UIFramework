using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
internal sealed class SkipBuilderAttribute : Attribute;