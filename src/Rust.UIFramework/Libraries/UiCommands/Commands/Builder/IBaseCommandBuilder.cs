namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public interface IBaseCommandBuilder
{
    string Build();
}

public interface IBaseCommandBuilder<in T0>
{
    string Build(T0 arg0);
}

public interface IBaseCommandBuilder<in T0, in T1>
{
    string Build(T0 arg0, T1 arg1);
}

public interface IBaseCommandBuilder<in T0, in T1, in T2>
{
    string Build(T0 arg0, T1 arg1, T2 arg2);
}

public interface IBaseCommandBuilder<in T0, in T1, in T2, in T3>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
}

public interface IBaseCommandBuilder<in T0, in T1, in T2, in T3, in T4>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}

public interface IBaseCommandBuilder<in T0, in T1, in T2, in T3, in T4, in T5>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}

public interface IBaseCommandBuilder<in T0, in T1, in T2, in T3, in T4, in T5, in T6>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
}

public interface IBaseCommandBuilder<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7>
{
    string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
}