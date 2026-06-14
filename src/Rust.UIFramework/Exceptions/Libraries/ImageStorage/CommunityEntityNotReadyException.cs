namespace Oxide.Ext.UiFramework.Exceptions;

public class CommunityEntityNotReadyException : BaseUiFrameworkException
{
    internal CommunityEntityNotReadyException() : base("Community Entity is not ready yet") { }
}