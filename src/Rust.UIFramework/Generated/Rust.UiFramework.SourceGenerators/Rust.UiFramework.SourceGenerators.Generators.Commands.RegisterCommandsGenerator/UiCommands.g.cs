using System;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public partial class UiCommands
{
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder> RegisterCommand(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0>> RegisterCommand<T0>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1>> RegisterCommand<T0, T1>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2>> RegisterCommand<T0, T1, T2>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3>> RegisterCommand<T0, T1, T2, T3>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4>> RegisterCommand<T0, T1, T2, T3, T4>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5>> RegisterCommand<T0, T1, T2, T3, T4, T5>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Action<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder> RegisterCommand(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0>> RegisterCommand<T0>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1>> RegisterCommand<T0, T1>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2>> RegisterCommand<T0, T1, T2>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3>> RegisterCommand<T0, T1, T2, T3>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4>> RegisterCommand<T0, T1, T2, T3, T4>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5>> RegisterCommand<T0, T1, T2, T3, T4, T5>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, System.Threading.Tasks.Task> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder> RegisterCommand(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0>> RegisterCommand<T0>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1>> RegisterCommand<T0, T1>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2>> RegisterCommand<T0, T1, T2>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3>> RegisterCommand<T0, T1, T2, T3>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4>> RegisterCommand<T0, T1, T2, T3, T4>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5>> RegisterCommand<T0, T1, T2, T3, T4, T5>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
	public Oxide.Ext.UiFramework.Types.UiTuple<Oxide.Ext.UiFramework.Libraries.RegisteredCommand, Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Oxide.Ext.UiFramework.Plugins.IUiFrameworkPlugin plugin, System.Func<Oxide.Ext.UiFramework.Libraries.ExecutionData, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, System.Threading.Tasks.ValueTask> method)
	{
		Oxide.Ext.UiFramework.Libraries.RegisteredCommand command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(plugin.Id()));
		_commands[command.Id] = new Oxide.Ext.UiFramework.Libraries.CommandParser<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);
		Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> builder = new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);
		return Oxide.Ext.UiFramework.Types.UiTuple.Create(command, builder);
	}
}


