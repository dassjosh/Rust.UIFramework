using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class ValueTaskExt
{
    extension(ValueTask)
    {
        public static ValueTask Completed { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => default; }
    }
}