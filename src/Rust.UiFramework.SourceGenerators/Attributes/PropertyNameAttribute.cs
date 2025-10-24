using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Property)]
#pragma warning disable CS9113 // Parameter is unread.
internal sealed class PropertyNameAttribute(string propertyName) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
