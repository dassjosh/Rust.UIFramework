using System.Collections.Generic;
using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class WhereBuilder : IWhere, IBuildable
{
    List<WhereConstraint> IWhere.Constraints => _constraints;
    List<string> IWhere.TypeConstraints => _typeConstraints;
    
    private string _type;
    private readonly List<WhereConstraint> _constraints = [];
    private readonly List<string> _typeConstraints = [];

    public WhereBuilder Type(string type)
    {
        _type = type;
        return this;
    }

    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append(" where ");
        sb.Append(_type);
        sb.Append(" : ");
        sb.Append(this.GetConstraints());
        return sb.ToString();
    }
}