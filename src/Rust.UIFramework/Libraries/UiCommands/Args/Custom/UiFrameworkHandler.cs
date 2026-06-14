using System;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiFrameworkHandler : 
    IArgHandler<UiPosition>, IArgHandler<UiPosition?>, 
    IArgHandler<UiOffset>, IArgHandler<UiOffset?>, 
    IArgHandler<UiPadding>, IArgHandler<UiPadding?>, 
    IArgHandler<UiScale>, IArgHandler<UiScale?>,
    IArgHandler<UiBorderWidth>, IArgHandler<UiBorderWidth?>,
    IArgHandler<UiUnit>, IArgHandler<UiUnit?>,
    ISingleton
{
    private const string Separator = ";";

    private UiFrameworkHandler() { }

    UiPosition IArgReader<UiPosition>.Read(ReadOnlySpan<char> arg) => UiPosition.Parse(arg, Separator);
    UiPosition? IArgReader<UiPosition?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<UiPosition>)this).Read(arg);
    UiOffset IArgReader<UiOffset>.Read(ReadOnlySpan<char> arg) => UiOffset.Parse(arg, Separator);
    UiOffset? IArgReader<UiOffset?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<UiOffset>)this).Read(arg);
    UiPadding IArgReader<UiPadding>.Read(ReadOnlySpan<char> arg) => UiPadding.Parse(arg, Separator);
    UiPadding? IArgReader<UiPadding?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<UiPadding>)this).Read(arg);
    UiScale IArgReader<UiScale>.Read(ReadOnlySpan<char> arg) => UiScale.Parse(arg, Separator);
    UiScale? IArgReader<UiScale?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<UiScale>)this).Read(arg);
    UiBorderWidth IArgReader<UiBorderWidth>.Read(ReadOnlySpan<char> arg) => UiBorderWidth.Parse(arg, Separator);
    UiBorderWidth? IArgReader<UiBorderWidth?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<UiBorderWidth>)this).Read(arg);
    UiUnit IArgReader<UiUnit>.Read(ReadOnlySpan<char> arg) => UiUnit.Parse(arg);
    UiUnit? IArgReader<UiUnit?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<UiUnit>)this).Read(arg);

    public void Write(UiArgWriter writer, UiPosition arg)
    {
        writer.AppendStartQuote();
        writer.Append(arg.XMin);
        writer.Append(Separator);
        writer.Append(arg.YMin);
        writer.Append(Separator);
        writer.Append(arg.XMax);
        writer.Append(Separator);
        writer.Append(arg.YMax);
        writer.AppendEndQuote();
    }
    
    public void Write(UiArgWriter writer, UiOffset arg)
    {
        writer.AppendStartQuote();
        writer.Append(arg.XMin);
        writer.Append(Separator);
        writer.Append(arg.YMin);
        writer.Append(Separator);
        writer.Append(arg.XMax);
        writer.Append(Separator);
        writer.Append(arg.YMax);
        writer.AppendEndQuote();
    }

    public void Write(UiArgWriter writer, UiPadding arg)
    {
        writer.AppendStartQuote();
        writer.Append(arg.Left);
        writer.Append(Separator);
        writer.Append(arg.Top);
        writer.Append(Separator);
        writer.Append(arg.Right);
        writer.Append(Separator);
        writer.Append(arg.Bottom);
        writer.AppendEndQuote();
    }
    
    public void Write(UiArgWriter writer, UiScale arg)
    {
        writer.AppendStartQuote();
        writer.Append(arg.Horizontal);
        writer.Append(Separator);
        writer.Append(arg.Vertical);
        writer.AppendEndQuote();
    }

    public void Write(UiArgWriter writer, UiBorderWidth arg)
    {
        writer.AppendStartQuote();
        writer.Append(arg.Left);
        writer.Append(Separator);
        writer.Append(arg.Top);
        writer.Append(Separator);
        writer.Append(arg.Right);
        writer.Append(Separator);
        writer.Append(arg.Bottom);
        writer.AppendEndQuote();
    }

    public void Write(UiArgWriter writer, UiUnit arg)
    {
        writer.AppendStartQuote();
        writer.Append(arg.Value);
        switch (arg.Type)
        {
            case UiUnitType.Percent:
                writer.Append("%");
                break;
            case UiUnitType.Px:
                writer.Append("px");
                break;
        }
        writer.AppendEndQuote();
    }
    
    public void Write(UiArgWriter writer, UiPosition? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
        }
        else
        {
            Write(writer, arg.Value);
        }
    }

    public void Write(UiArgWriter writer, UiOffset? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
        }
        else
        {
            Write(writer, arg.Value);
        }
    }

    
    public void Write(UiArgWriter writer, UiPadding? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
        }
        else
        {
            Write(writer, arg.Value);
        }
    }

    public void Write(UiArgWriter writer, UiScale? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
        }
        else
        {
            Write(writer, arg.Value);
        }
    }

    public void Write(UiArgWriter writer, UiBorderWidth? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
        }
        else
        {
            Write(writer, arg.Value);
        }
    }

    public void Write(UiArgWriter writer, UiUnit? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
        }
        else
        {
            Write(writer, arg.Value);
        }
    }
}