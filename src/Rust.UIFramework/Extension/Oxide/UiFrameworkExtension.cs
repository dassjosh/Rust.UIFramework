#if OXIDE
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
}
#endif