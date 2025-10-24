using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class SkipPropertyAttribute : Attribute;