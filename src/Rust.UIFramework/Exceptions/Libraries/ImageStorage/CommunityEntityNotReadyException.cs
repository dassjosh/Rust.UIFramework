namespace Oxide.Ext.UiFramework.Exceptions;

public class CommunityEntityNotReadyException : BaseUiFrameworkException
{
    private CommunityEntityNotReadyException() : base("Community Entity is not ready yet") { }
    
    internal static void ThrowIfNotReady()
    {
        if (!CommunityEntity.ServerInstance.IsValid())
        {
            throw new CommunityEntityNotReadyException();
        }
    }
}