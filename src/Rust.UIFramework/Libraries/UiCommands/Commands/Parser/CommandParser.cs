using System;
using Cysharp.Threading.Tasks;

namespace Oxide.Ext.UiFramework.Libraries;

internal class CommandParser(ICommandParserData command) : BaseCommandParser(command)
{
    private readonly Action<ExecutionData> _command = (Action<ExecutionData>)command.Delegate;

    protected override void RunCommandInternal(ExecutionData data, UiCommandTokenizer args)
    {
        RunCommand(data);
    }

    private void RunCommand(ExecutionData data)
    {
        try
        {
            _command(data);
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

internal class CommandParserAsync(ICommandParserData command) : BaseCommandParser(command)
{
    private readonly Func<ExecutionData, UniTask> _command = (Func<ExecutionData, UniTask>)command.Delegate;

    protected override void RunCommandInternal(ExecutionData data, UiCommandTokenizer args)
    {
        RunCommand(data).Forget();
    }

    private async UniTaskVoid RunCommand(ExecutionData data)
    {
        try
        {
            await _command(data);
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