using Oxide.Core;
using Oxide.Core.Extensions;

// ReSharper disable once CheckNamespace
namespace Oxide.Ext.UiFramework
{
    public class UiFrameworkExtension : Extension
    {
        public override string Name => "UiFramework";
        public override string Author => "MJSU";
        public override VersionNumber Version => new VersionNumber(1, 4, 4);
        
        public UiFrameworkExtension(ExtensionManager manager) : base(manager) { }
    }
}