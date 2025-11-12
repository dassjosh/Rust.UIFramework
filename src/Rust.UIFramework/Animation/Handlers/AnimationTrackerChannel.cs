using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationTrackerChannel : ISingleton
{
    private readonly Channel<UiSendRequest> _channel = Channel.CreateUnbounded<UiSendRequest>();
    private readonly CancellationTokenSource _source = new();
    private readonly IUiLogger<AnimationTrackerChannel> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<AnimationTrackerChannel>();

    private AnimationTrackerChannel()
    {
#pragma warning disable EPC13
        Task.Factory.StartNew(Process, TaskCreationOptions.LongRunning, _source.Token);
#pragma warning restore EPC13
    }
    
    internal ChannelWriter<UiSendRequest> Writer => _channel.Writer;
    
    private async ValueTask Process(object state)
    {
        AnimationTracker instance = Singleton<AnimationTracker>.Instance;
        CancellationToken token = _source.Token;
        ChannelReader<UiSendRequest> reader = _channel.Reader;
        while (!token.IsCancellationRequested)
        {
            UiSendRequest request = await reader.ReadAsync(_source.Token).ConfigureAwait(false);
            
            try
            {
                if (request.Builder is BaseUiBuilder builder)
                {
                    SendInfo send = request.Send;
                    ReadOnlySpan<BaseUiComponent> span = builder.ComponentAsReadonly();
                    for (int index = 0; index < span.Length; index++)
                    {
                        BaseUiComponent component = span[index];
                        if (component.Update is not UpdateMode.Update)
                        {
                            instance.RemoveUiForSend(send, component.Name);
                        }
                    }
                }
            }
            catch(Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.Exception("An error occured processing animation tracker channel", ex);
            }
            finally
            {
                request.Dispose();
            }
        }
    }

    internal void OnServerShutdown()
    {
        _source.Cancel();
    }
}