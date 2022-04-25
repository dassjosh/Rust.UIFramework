using Oxide.Core;
using Oxide.Core.Extensions;

namespace Oxide.Ext.UiFramework.OxideExtension
{
    public class UiFrameworkExtension : Extension
    {
        public override string Name => "UI Framework Extension";
        public override string Author => "MJSU";
        public override VersionNumber Version => new VersionNumber(1, 3, 0);
        
        public UiFrameworkExtension(ExtensionManager manager) : base(manager)
        {
        }
    }
}