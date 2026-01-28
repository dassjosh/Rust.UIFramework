using System;
using Cysharp.Threading.Tasks;

namespace Oxide.Ext.UiFramework.Libraries;

internal class CommandParser(ICommandParserData command) : BaseCommandParser(command)
{
    protected override void RunCommandInternal(ExecutionData data, UiCommandTokenizer args)
    {
        RunCommandAsync(data).Forget();
    }

    private async UniTask RunCommandAsync(ExecutionData data)
    {
        try
        {
            switch (Command.Mode)
            {
                case ExecutorMode.Void:
                    ((Action<ExecutionData>)Command.Delegate)(data);
                    break;
                case ExecutorMode.UniTask:
                    await ((Func<ExecutionData, UniTask>)Command.Delegate)(data);
                    break;
            }
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
        finally
        {
            data.TryDispose();
        }
    }
}