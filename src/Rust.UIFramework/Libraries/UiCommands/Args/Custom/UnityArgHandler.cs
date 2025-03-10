using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class UnityArgHandler : IArgHandler<Vector2>, IArgHandler<Vector3>, IArgHandler<Vector4>, IArgHandler<Vector2?>, IArgHandler<Vector3?>, IArgHandler<Vector4?>, IArgHandler<Quaternion>, IArgHandler<Quaternion?>, ISingleton
{
    private const string Separator = ";";
    
    private UnityArgHandler() { }
    
    Vector2 IArgReader<Vector2>.Read(ReadOnlySpan<char> arg)
    {
        arg.ParseNextString(Separator, out ReadOnlySpan<char> x, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> y, out arg);
        return new Vector2(float.Parse(x), float.Parse(y));
    }
    
    Vector3 IArgReader<Vector3>.Read(ReadOnlySpan<char> arg)
    {
        arg.ParseNextString(Separator, out ReadOnlySpan<char> x, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> y, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> z, out arg);
        return new Vector3(float.Parse(x), float.Parse(y), float.Parse(z));
    }
    
    Vector4 IArgReader<Vector4>.Read(ReadOnlySpan<char> arg)
    {
        arg.ParseNextString(Separator, out ReadOnlySpan<char> w, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> x, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> y, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> z, out arg);
        return new Vector4(float.Parse(x), float.Parse(y), float.Parse(z), float.Parse(w));
    }
    
    Quaternion IArgReader<Quaternion>.Read(ReadOnlySpan<char> arg)
    {
        arg.ParseNextString(Separator, out ReadOnlySpan<char> w, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> x, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> y, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> z, out arg);
        return new Quaternion(float.Parse(x), float.Parse(y), float.Parse(z), float.Parse(w));
    }

    Vector2? IArgReader<Vector2?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<Vector2>)this).Read(arg);
    Vector3? IArgReader<Vector3?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<Vector3>)this).Read(arg);
    Vector4? IArgReader<Vector4?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<Vector4>)this).Read(arg);
    Quaternion? IArgReader<Quaternion?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<Quaternion>)this).Read(arg);
    
    public void Write(UiArgWriter writer, Vector2 arg)
    {
        writer.Append(arg.x);
        writer.Append(Separator);
        writer.Append(arg.y);
    }
    
    public void Write(UiArgWriter writer, Vector3 arg)
    {
        writer.Append(arg.x);
        writer.Append(Separator);
        writer.Append(arg.y);
        writer.Append(Separator);
        writer.Append(arg.z);
    }

    public void Write(UiArgWriter writer, Vector4 arg)
    {
        writer.Append(arg.w);
        writer.Append(Separator);
        writer.Append(arg.x);
        writer.Append(Separator);
        writer.Append(arg.y);
        writer.Append(Separator);
        writer.Append(arg.z);
    }
    
    public void Write(UiArgWriter writer, Quaternion arg)
    {
        writer.Append(arg.w);
        writer.Append(Separator);
        writer.Append(arg.x);
        writer.Append(Separator);
        writer.Append(arg.y);
        writer.Append(Separator);
        writer.Append(arg.z);
    }

    public void Write(UiArgWriter writer, Vector2? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
            return;
        }
        Write(writer, arg.Value);
    }
    
    public void Write(UiArgWriter writer, Vector3? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
            return;
        }
        Write(writer, arg.Value);
    }
    
    public void Write(UiArgWriter writer, Vector4? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
            return;
        }
        Write(writer, arg.Value);
    }
    
    public void Write(UiArgWriter writer, Quaternion? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
            return;
        }
        Write(writer, arg.Value);
    }
}