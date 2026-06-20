using System.Runtime.CompilerServices;
using Facepunch;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal delegate T ReadFunc<out T>(in UiStringView view);
internal delegate void WriteFunc<in T>(UiArgWriter writer, T arg);

internal class ArgHandler<T>(ReadFunc<T> readerFunc, WriteFunc<T> writerFunc) : IArgHandler<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Read(in UiStringView view) => readerFunc(view);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(UiArgWriter writer, T arg) => writerFunc(writer, arg);
}