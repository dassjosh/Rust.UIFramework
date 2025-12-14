using System.Collections.Generic;
using Rust.UiFramework.SourceGenerators.Builder.Enums;

namespace Rust.UiFramework.SourceGenerators.Builder.Interfaces;

public interface IWhere
{
    List<WhereConstraint> Constraints { get; set; }
    List<string> TypeConstraints { get; set; }
}