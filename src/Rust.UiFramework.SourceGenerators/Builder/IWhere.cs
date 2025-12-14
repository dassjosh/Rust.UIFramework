using System.Collections.Generic;

namespace Rust.UiFramework.SourceGenerators.Builder;

public interface IWhere
{
    List<WhereConstraint> Constraints { get; }
    List<string> TypeConstraints { get; }
}