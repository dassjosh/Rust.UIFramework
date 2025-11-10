using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class GenericsBuilder : IBuildable, IEnumerable<string>
{
    private readonly List<string> _generics = [];
    public int Count => _generics.Count;
    
    public GenericsBuilder Generic(string generic)
    {
        _generics.Add(generic);
        return this;
    }
    
    public GenericsBuilder Generics(IEnumerable<string> generic)
    {
        _generics.AddRange(generic);
        return this;
    }
    
    public GenericsBuilder Generics(IEnumerable<int> generic)
    {
        _generics.AddRange(generic.Select(g => $"T{g}"));
        return this;
    }

    public string GetGeneric(int index)
    {
        return _generics[index];
    }
    
    public string Build(int indent)
    {
        return string.Join(", ", _generics);
    }

    public IEnumerator<string> GetEnumerator() => _generics.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _generics.GetEnumerator();
}