#if OXIDE
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Extensions;

// ReSharper disable once CheckNamespace
namespace Oxide.Ext.UiFramework;

public class UiFrameworkExtension : Extension
{
    public override string Name => "UiFramework";
    public override string Author => "MJSU";
    public override VersionNumber Version { get; }

    public UiFrameworkExtension(ExtensionManager manager) : base(manager)
    {
        AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
        Version = new VersionNumber(assembly.Version.Major, assembly.Version.Minor, assembly.Version.Build);
    }
    
    public override IEnumerable<string> GetPreprocessorDirectives()
    {
        string name = Name.ToUpper();
        yield return $"{name}_EXT";
        for (int i = 0; i <= Version.Minor; i++)
        {
            yield return $"{name}_EXT_{Version.Major}_{i}";
        }
    }
}
#endif