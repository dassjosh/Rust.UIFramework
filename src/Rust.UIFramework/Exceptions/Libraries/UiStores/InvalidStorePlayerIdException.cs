namespace Oxide.Ext.UiFramework.Exceptions;

public class InvalidStorePlayerIdException : BaseUiFrameworkException
{
    private InvalidStorePlayerIdException() : base("Player ID cannot be 0") { }

    internal static void ThrowIfInvalidPlayerId(ulong playerId)
    {
        if (playerId == 0)
        {
            throw new InvalidStorePlayerIdException();
        }
    }
}