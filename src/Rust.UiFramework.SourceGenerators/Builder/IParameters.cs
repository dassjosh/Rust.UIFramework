using System.Collections.Generic;

namespace Rust.UiFramework.SourceGenerators.Builder;

public interface IParameters
{
    List<ParameterBuilder> Parameters { get; set; }
}