using System.IO;
using Oxide.Core;

namespace Oxide.Ext.UiFramework.Constants
{
    internal static class PathConstants
    {
        internal static readonly string UiFrameworkDataFolder = Path.Combine(Interface.Oxide.DataDirectory, "UiFramework");
    }
}