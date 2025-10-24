using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Property)]
#pragma warning disable CS9113 // Parameter is unread.
internal sealed class PropertyTargetAttribute(string target, PropertyTargetType type) : Attribute;

internal enum PropertyTargetType
{
    Self,
    Field,
    Property,
    Method
}
#pragma warning restore CS9113 // Parameter is unread.
