using System.Collections.Generic;

namespace Rust.UiFramework.SourceGenerators.Builder;

public interface IAttributes
{
    List<AttributeBuilder> Attributes { get; }
}