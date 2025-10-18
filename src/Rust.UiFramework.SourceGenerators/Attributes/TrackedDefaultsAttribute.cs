using System;

namespace Rust.UiFramework.SourceGenerators.Attributes;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class TrackedDefaultsAttribute : Attribute
{
    public string Value { get; }
    
    public Type DefaultType { get; }
    public string DefaultName { get; }
    public Type DefaultFrameworkType { get; }
    public string DefaultFrameworkName { get; }

    public bool HasValue => !string.IsNullOrEmpty(Value);

    public TrackedDefaultsAttribute(bool value)
    {
        Value = value.ToString();
    }
    
    public TrackedDefaultsAttribute(Type defaultType, string defaultName)
    {
        DefaultType = defaultType;
        DefaultName = defaultName;
    }

    public TrackedDefaultsAttribute(Type frameworkDefaultType, string frameworkDefaultName, Type serializationDefaultType, string serializationDefaultName)
    {
        DefaultType = frameworkDefaultType;
        DefaultName = frameworkDefaultName;
        DefaultFrameworkType = serializationDefaultType;
        DefaultFrameworkName = serializationDefaultName;
    }
}