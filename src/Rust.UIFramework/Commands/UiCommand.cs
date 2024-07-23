using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Commands;

public struct UiCommand : IDisposable
{
    public readonly string Command;
    public List<string> Args;
    public bool IsEmpty => string.IsNullOrEmpty(Command) && (Args == null || Args.Count == 0);
    private readonly bool _disposable;

    public UiCommand(string command, bool disposable = true)
    {
        Command = command;
        Args = null;
        _disposable = disposable;
    }
        
    public static UiCommand Create(string command)
    {
        return new UiCommand(command);
    }
        
    public static UiCommand Create<T0>(string command, T0 arg0, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1>(string command, T0 arg0, T1 arg1, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1, T2>(string command, T0 arg0, T1 arg1, T2 arg2, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1, T2, T3>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        cmd.AddArg(arg3);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1, T2, T3, T4>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        cmd.AddArg(arg3);
        cmd.AddArg(arg4);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1, T2, T3, T4, T5>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        cmd.AddArg(arg3);
        cmd.AddArg(arg4);
        cmd.AddArg(arg5);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        cmd.AddArg(arg3);
        cmd.AddArg(arg4);
        cmd.AddArg(arg5);
        cmd.AddArg(arg6);
        return cmd;
    }

    public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6, T7>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        cmd.AddArg(arg3);
        cmd.AddArg(arg4);
        cmd.AddArg(arg5);
        cmd.AddArg(arg6);
        cmd.AddArg(arg7);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        cmd.AddArg(arg3);
        cmd.AddArg(arg4);
        cmd.AddArg(arg5);
        cmd.AddArg(arg6);
        cmd.AddArg(arg7);
        cmd.AddArg(arg8);
        return cmd;
    }
        
    public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, bool disposable = true)
    {
        UiCommand cmd = new(command, disposable);
        cmd.Args = UiFrameworkPool.GetList<string>();;
        cmd.AddArg(arg0);
        cmd.AddArg(arg1);
        cmd.AddArg(arg2);
        cmd.AddArg(arg3);
        cmd.AddArg(arg4);
        cmd.AddArg(arg5);
        cmd.AddArg(arg6);
        cmd.AddArg(arg7);
        cmd.AddArg(arg8);
        cmd.AddArg(arg9);
        return cmd;
    }

    public void AddArg<T>(T arg)
    {
        Args.Add(arg as string ?? arg.ToString());
    }

    public void Dispose()
    {
        if (_disposable && Args != null)
        {
            UiFrameworkPool.FreeList(Args);
        }
    }

    public static implicit operator UiCommand(string command) => new(command);
}