using Network;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class SendableAnimation : BaseAnimation, ISendableAnimation
{
    public bool IsSending { get; private set; }

    private SendInfo _send;
    public SendInfo Send
    {
        get => _send;
        set
        {
            if (!IsSending)
            {
                _send = value;
                IsSending = true;
                return;
            }

            throw new AnimationException($"{nameof(ISendableAnimation)}.{nameof(Send)}.set cannot be called after it has been set.");
        }
    }

    public virtual void Serialize(JsonFrameworkWriter writer)
    {
        for (int index = 0; index < Children.Count; index++)
        {
            if (Children[index] is ISendableAnimation animation)
            {
                animation.Serialize(writer);
            }
        }
    }

    public void AddPlayer(Connection connection)
    {
        if (connection is null)
        {
            return;
        }
        
        if (this.IsSinglePlayer())
        {
            throw new AnimationException($"{nameof(ISendableAnimation)}.{nameof(AddPlayer)} cannot be called on a single player animation.");
        }

        if (!Send.connections.Contains(connection))
        {
            Send.connections.Add(connection);
        }
    }

    public void RemovePlayer(ulong playerId)
    {
        if (Send.connections != null)
        {
            for (int index = Send.connections.Count - 1; index >= 0; index--)
            {
                Connection connection = Send.connections[index];
                if (connection.userid == playerId)
                {
                    Send.connections.RemoveAt(index);
                    break;
                }
            }

            if (Send.connections.Count == 0)
            {
                CancelAnimation();
            }

            return;
        }

        if (Send.connection.userid == playerId)
        {
            CancelAnimation();
        }
    }
    
    public override ISendableAnimation GetSendable() => IsSending ? this : base.GetSendable();

    protected override void EnterPool()
    {
        base.EnterPool();
        if (Send.connections != null)
        {
            PluginPool.FreeList(Send.connections);
        }
        _send = default;
        IsSending = false;
    }
}