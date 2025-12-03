using System;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries;

internal class CommandParser<T0>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0>)Command.Delegate)(data, arg0);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1>)Command.Delegate)(data, arg0, arg1);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2>)Command.Delegate)(data, arg0, arg1, arg2);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2, T3>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		T3 arg3 = iterator.ParseNext<T3>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3>)Command.Delegate)(data, arg0, arg1, arg2, arg3);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2, arg3), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2, arg3), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2, T3, T4>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		T3 arg3 = iterator.ParseNext<T3>(ref args);
		T4 arg4 = iterator.ParseNext<T4>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2, T3, T4, T5>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		T3 arg3 = iterator.ParseNext<T3>(ref args);
		T4 arg4 = iterator.ParseNext<T4>(ref args);
		T5 arg5 = iterator.ParseNext<T5>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		T3 arg3 = iterator.ParseNext<T3>(ref args);
		T4 arg4 = iterator.ParseNext<T4>(ref args);
		T5 arg5 = iterator.ParseNext<T5>(ref args);
		T6 arg6 = iterator.ParseNext<T6>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		T3 arg3 = iterator.ParseNext<T3>(ref args);
		T4 arg4 = iterator.ParseNext<T4>(ref args);
		T5 arg5 = iterator.ParseNext<T5>(ref args);
		T6 arg6 = iterator.ParseNext<T6>(ref args);
		T7 arg7 = iterator.ParseNext<T7>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		T3 arg3 = iterator.ParseNext<T3>(ref args);
		T4 arg4 = iterator.ParseNext<T4>(ref args);
		T5 arg5 = iterator.ParseNext<T5>(ref args);
		T6 arg6 = iterator.ParseNext<T6>(ref args);
		T7 arg7 = iterator.ParseNext<T7>(ref args);
		T8 arg8 = iterator.ParseNext<T8>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Oxide.Ext.UiFramework.Libraries.ICommandParserData data) : Oxide.Ext.UiFramework.Libraries.BaseCommandParser(data)
{
	protected override void RunCommandInternal(Oxide.Ext.UiFramework.Libraries.ExecutionData data, Oxide.Ext.UiFramework.Libraries.UiCommandTokenizer args)
	{
		Oxide.Ext.UiFramework.Libraries.ArgReaderIterator iterator = GetReader();
		T0 arg0 = iterator.ParseNext<T0>(ref args);
		T1 arg1 = iterator.ParseNext<T1>(ref args);
		T2 arg2 = iterator.ParseNext<T2>(ref args);
		T3 arg3 = iterator.ParseNext<T3>(ref args);
		T4 arg4 = iterator.ParseNext<T4>(ref args);
		T5 arg5 = iterator.ParseNext<T5>(ref args);
		T6 arg6 = iterator.ParseNext<T6>(ref args);
		T7 arg7 = iterator.ParseNext<T7>(ref args);
		T8 arg8 = iterator.ParseNext<T8>(ref args);
		T9 arg9 = iterator.ParseNext<T9>(ref args);
		switch (Command.Mode)
		{
			case ExecutorMode.Void:
				try
				{
					((System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, System.Threading.Tasks.Task>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9), OnException, data);
				break;
			case ExecutorMode.ValueTask:
				TaskExt.RunSafely(((System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, System.Threading.Tasks.ValueTask>)Command.Delegate)(data, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9), OnException, data);
				break;
			case ExecutorMode.UniTask:
				break;
		}
	}
}


