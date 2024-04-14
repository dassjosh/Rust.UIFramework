using System.Threading.Tasks;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Callbacks;

public class SendUiCallback : BaseAsyncCallback
{
    private BaseUiBuilder _builder;
    private SendInfo _send;
    private bool _freeConnections;

    public static void Start(BaseUiBuilder builder, SendInfo send, bool freeConnections)
    {
        SendUiCallback callback = UiFrameworkPool.Get<SendUiCallback>();
        callback.Init(builder, send, freeConnections);
        callback.Run();
    }

    private void Init(BaseUiBuilder builder, SendInfo send, bool freeConnections)
    {
        _builder = builder;
        _send = send;
        _freeConnections = freeConnections;
    }

    protected override ValueTask HandleCallback()
    {
        JsonFrameworkWriter writer = _builder.CreateWriter();
        _builder.AddUi(_send, writer);
        writer.Dispose();

        if (_freeConnections && _send.connections != null)
            Facepunch.Pool.Free(ref _send.connections);

        return new ValueTask();
    }

    protected override string GetExceptionMessage()
    {
        return "";
    }

    protected override void EnterPool()
    {
        _builder.Dispose();
        _send = default;
    }
}