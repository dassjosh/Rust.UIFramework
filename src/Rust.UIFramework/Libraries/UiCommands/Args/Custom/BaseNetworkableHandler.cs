using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BaseNetworkableHandler<T> : IArgHandler<T>
{
    public T Read(in UiStringView view)
    {
        if(view.AsSpan() is UiCommands.NullArg) return default;
        BaseNetworkable networkable = BaseNetworkable.serverEntities.Find(new NetworkableId(ulong.Parse(view)));
        return networkable && networkable is T entity ? entity : default;
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