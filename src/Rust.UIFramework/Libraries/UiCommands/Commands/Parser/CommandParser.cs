using System;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries;

internal class CommandParser(ICommandParserData command) : BaseCommandParser(command)
{
    protected override void RunCommandInternal(ExecutionData data, UiCommandTokenizer args)
    {
        switch (Command.Mode)
        {
            case ExecutorMode.Void:
                try
                {
                    ((Action<ExecutionData>)Command.Delegate)(data);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
                finally
                {
                    data.TryDispose();
                }
                break;
            case ExecutorMode.Task:
                TaskExt.RunSafely(((Func<ExecutionData, Task>)Command.Delegate)(data), LogException, data);
                break;
            case ExecutorMode.ValueTask:
                TaskExt.RunSafely(((Func<ExecutionData, ValueTask>)Command.Delegate)(data), LogException, data);
                break;
            case ExecutorMode.UniTask:
                break;
        }
    }
}