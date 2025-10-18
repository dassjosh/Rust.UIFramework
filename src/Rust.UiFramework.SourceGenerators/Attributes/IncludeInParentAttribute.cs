using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Interface)]
internal sealed class IncludeInParentAttribute : Attribute;