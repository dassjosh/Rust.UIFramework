namespace Oxide.Ext.UiFramework.Libraries;

public interface ICommandBuilder
{
    string Build();
}

public interface ICommandBuilder<in T0>
{
    string Build(T0 arg0);
}

public interface ICommandBuilder<in T0, in T1>
{
    string Build(T0 arg0, T1 arg1);
    ICommandBuilder<T1> Partial(T0 arg0);
}

public interface ICommandBuilder<in T0, in T1, in T2>
{
    string Build(T0 arg0, T1 arg1, T2 arg2);
    ICommandBuilder<T2> Partial(T0 arg0, T1 arg1);
    ICommandBuilder<T1, T2> Partial(T0 arg0);
}

public interface ICommandBuilder<in T0, in T1, in T2, in T3>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    ICommandBuilder<T3> Partial(T0 arg0, T1 arg1, T2 arg2);
    ICommandBuilder<T2, T3> Partial(T0 arg0, T1 arg1);
}

public interface ICommandBuilder<in T0, in T1, in T2, in T3, in T4>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    ICommandBuilder<T4> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    ICommandBuilder<T3, T4> Partial(T0 arg0, T1 arg1, T2 arg2);
}

public interface ICommandBuilder<in T0, in T1, in T2, in T3, in T4, in T5>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    ICommandBuilder<T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    ICommandBuilder<T4, T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
}

public interface ICommandBuilder<in T0, in T1, in T2, in T3, in T4, in T5, in T6>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    ICommandBuilder<T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    ICommandBuilder<T5, T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}

public interface ICommandBuilder<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
    ICommandBuilder<T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    ICommandBuilder<T6, T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}