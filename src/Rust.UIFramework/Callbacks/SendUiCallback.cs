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
    
    public static void Start(BaseUiBuilder builder, SendInfo send)
    {
        SendUiCallback callback = UiFrameworkPool.Get<SendUiCallback>();
        callback.Init(builder, send);
        callback.Run();
    }

    private void Init(BaseUiBuilder builder, SendInfo send)
    {
        _builder = builder;
        _send = send;
    }
    
    protected override ValueTask HandleCallback()
    {
        JsonFrameworkWriter writer = _builder.CreateWriter();
        _builder.AddUi(_send, writer);
        writer.Dispose();
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