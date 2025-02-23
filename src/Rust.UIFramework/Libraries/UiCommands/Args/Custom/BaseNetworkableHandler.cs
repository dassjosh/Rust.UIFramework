using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands.Custom;

internal class BaseNetworkableHandler<T> : IArgHandler<T>
{
    public T Read(ReadOnlySpan<char> arg)
    {
        if(arg is UiCommands.NullArg) return default;
        BaseNetworkable networkable = BaseNetworkable.serverEntities.Find(new NetworkableId(ulong.Parse(arg)));
        return networkable is T entity ? entity : default;
    }

    public void Write(UiArgWriter writer, T arg)
    {
        if (arg is not BaseNetworkable networkable || !networkable.IsValid())
        {
            writer.AppendNull();
            return;
        }

        writer.Append(networkable.net.ID.Value);
    }
}