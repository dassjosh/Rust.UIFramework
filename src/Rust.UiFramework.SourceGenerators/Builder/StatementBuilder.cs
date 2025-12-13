using System.Collections.Generic;
using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class StatementBuilder : IStatement, IBuildable
{
    private readonly StringBuilder _sb = new();

    public StatementBuilder Return(string @return)
    {
        _sb.Append("return ");
        _sb.Append(@return);
        return this;
    }
    
    public StatementBuilder Or(string value)
    {
        _sb.Append(" || ");
        _sb.Append(value);
        return this;
    }
    
    public StatementBuilder Or(IEnumerable<string> values)
    {
        if (TryCombineValues(values, " || ", out string combined))
        {
            _sb.Append(" || ");
            _sb.Append(combined);
        }
        return this;
    }
    
    public StatementBuilder And(string value)
    {
        _sb.Append(" && ");
        _sb.Append(value);
        return this;
    }
    
    public StatementBuilder And(IEnumerable<string> values)
    {
        if (TryCombineValues(values, " && ", out string combined))
        {
            _sb.Append(" || ");
            _sb.Append(combined);
        }
        return this;
    }

    public StatementBuilder Invoke(string call)
    {
        _sb.Append(call);
        _sb.AppendLine(";");
        return this;
    }
    
    public StatementBuilder Invoke(IEnumerable<string> values)
    {
        foreach (string value in values)
        {
            Invoke(value);
        }
        return this;
    }

    private bool TryCombineValues(IEnumerable<string> values, string seperator, out string combined)
    {
        combined = string.Join(seperator, values);
        if (!string.IsNullOrEmpty(combined))
        {
            combined = $"({combined})";
            return true;
        }

        return false;
    }

    public StatementBuilder Semicolon()
    {
        _sb.Append(';');
        return this;
    }
    
    public string Build(int indent)
    {
        return _sb.ToString();
    }
}