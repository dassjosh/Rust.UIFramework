using System.Collections.Generic;
using Rust.UiFramework.SourceGenerators.Builder.Builders;

namespace Rust.UiFramework.SourceGenerators.Builder.Interfaces;

public interface IParameters
{
    List<ParameterBuilder> Parameters { get; set; }
}