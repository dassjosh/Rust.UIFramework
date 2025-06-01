
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Exceptions
{
    /// <summary>
    /// An exception when something is invalid with a pool
    /// </summary>
    public class InvalidPoolException : BaseUiFrameworkException
    {
        private InvalidPoolException(string message) : base(message) {}

        internal static void ThrowIfInvalidPoolSize(PoolSize size)
        {
            if (!size.IsValid)
            {
                throw new InvalidPoolException($"Pool Size is not a valid size. Starting: {size.StartingSize} Max: {size.MaxSize}");
            }
        }

        internal static void ThrowIfNotPowerOf2(int value, [CallerArgumentExpression("value")] string valueName = null)
        {
            if (!IsPowerOfTwo(value))
            {
                throw new InvalidPoolException($"Pool Size {valueName}: {value} is not a valid power of 2");
            }
        }

        private static bool IsPowerOfTwo(int x)
        {
            return x > 0 && (x & (x - 1)) == 0;
        }
    }
}