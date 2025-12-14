using System.Collections.Generic;

namespace Rust.UiFramework.SourceGenerators.Builder;

public interface IWhereBuildable
{
    List<WhereBuilder> Where { get; set; }
}