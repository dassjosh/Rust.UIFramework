using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Interface)]
internal sealed class SkipBuilderAttribute : Attribute;