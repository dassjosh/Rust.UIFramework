using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries;
internal class CommandBuilder<T0>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0>
{
	public string Build(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return FinishBuilding(writer);
	}
}

internal class CommandBuilder<T0, T1>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1>
{
	public string Build(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2, T3>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2, T3> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2, T3>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2, T3> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2, T3>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T3> Partial(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T3>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2, T3, T4>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2, T3, T4> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2, T3, T4>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2, T3, T4> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2, T3, T4>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T3, T4> Partial(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T3, T4>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T4> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T4>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2, T3, T4, T5> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2, T3, T4, T5>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2, T3, T4, T5> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2, T3, T4, T5>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T3, T4, T5> Partial(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T3, T4, T5>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T4, T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T4, T5>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T5>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2, T3, T4, T5, T6> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2, T3, T4, T5, T6>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2, T3, T4, T5, T6> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2, T3, T4, T5, T6>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T3, T4, T5, T6> Partial(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T3, T4, T5, T6>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T4, T5, T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T4, T5, T6>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T5, T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T5, T6>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T6>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2, T3, T4, T5, T6, T7> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2, T3, T4, T5, T6, T7>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2, T3, T4, T5, T6, T7> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2, T3, T4, T5, T6, T7>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T3, T4, T5, T6, T7> Partial(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T3, T4, T5, T6, T7>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T4, T5, T6, T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T4, T5, T6, T7>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T5, T6, T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T5, T6, T7>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T6, T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T6, T7>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T7>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2, T3, T4, T5, T6, T7, T8> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2, T3, T4, T5, T6, T7, T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2, T3, T4, T5, T6, T7, T8> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2, T3, T4, T5, T6, T7, T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T3, T4, T5, T6, T7, T8> Partial(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T3, T4, T5, T6, T7, T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T4, T5, T6, T7, T8> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T4, T5, T6, T7, T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T5, T6, T7, T8> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T5, T6, T7, T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T6, T7, T8> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T6, T7, T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T7, T8> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T7, T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T8> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T8>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Oxide.Ext.UiFramework.Libraries.ICommandBuilderData data, Oxide.Ext.UiFramework.Libraries.PartialArgs partial = default) : Oxide.Ext.UiFramework.Libraries.BaseCommandBuilder(data, partial), Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
	public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		return FinishBuilding(writer);
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T1, T2, T3, T4, T5, T6, T7, T8, T9> Partial(T0 arg0)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T2, T3, T4, T5, T6, T7, T8, T9> Partial(T0 arg0, T1 arg1)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T2, T3, T4, T5, T6, T7, T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T3, T4, T5, T6, T7, T8, T9> Partial(T0 arg0, T1 arg1, T2 arg2)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T3, T4, T5, T6, T7, T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T4, T5, T6, T7, T8, T9> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T4, T5, T6, T7, T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T5, T6, T7, T8, T9> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T5, T6, T7, T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T6, T7, T8, T9> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T6, T7, T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T7, T8, T9> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T7, T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T8, T9> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T8, T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
	public Oxide.Ext.UiFramework.Libraries.ICommandBuilder<T9> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
	{
		Oxide.Ext.UiFramework.Libraries.ArgWriterIterator writer = StartBuilding();
		writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		return new Oxide.Ext.UiFramework.Libraries.CommandBuilder<T9>(Data, new Oxide.Ext.UiFramework.Libraries.PartialArgs(writer));
	}
}


