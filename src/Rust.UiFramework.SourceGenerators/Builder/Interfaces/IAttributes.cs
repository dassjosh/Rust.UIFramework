using System.Collections.Generic;
using Rust.UiFramework.SourceGenerators.Builder.Builders;

namespace Rust.UiFramework.SourceGenerators.Builder.Interfaces;

public interface IAttributes
{
    List<AttributeBuilder> Attributes { get; set; }
}