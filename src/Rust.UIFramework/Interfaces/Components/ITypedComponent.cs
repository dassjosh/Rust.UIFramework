using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ITypedComponent
{
    [TrackedDefaults(true)]
    public bool Enabled { get; set; }
}