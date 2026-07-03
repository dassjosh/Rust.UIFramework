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

    UiPosition IArgReader<UiPosition>.Read(in UiStringView view) => UiPosition.Parse(view, Separator);
    UiPosition? IArgReader<UiPosition?>.Read(in UiStringView view) => view.AsSpan() is UiCommands.NullArg ? null : ((IArgReader<UiPosition>)this).Read(view);
    UiOffset IArgReader<UiOffset>.Read(in UiStringView view) => UiOffset.Parse(view, Separator);
    UiOffset? IArgReader<UiOffset?>.Read(in UiStringView view) => view.AsSpan() is UiCommands.NullArg ? null : ((IArgReader<UiOffset>)this).Read(view);
    UiPadding IArgReader<UiPadding>.Read(in UiStringView view) => UiPadding.Parse(view, Separator);
    UiPadding? IArgReader<UiPadding?>.Read(in UiStringView view) => view.AsSpan() is UiCommands.NullArg ? null : ((IArgReader<UiPadding>)this).Read(view);
    UiScale IArgReader<UiScale>.Read(in UiStringView view) => UiScale.Parse(view, Separator);
    UiScale? IArgReader<UiScale?>.Read(in UiStringView view) => view.AsSpan() is UiCommands.NullArg ? null : ((IArgReader<UiScale>)this).Read(view);
    UiBorderWidth IArgReader<UiBorderWidth>.Read(in UiStringView view) => UiBorderWidth.Parse(view, Separator);
    UiBorderWidth? IArgReader<UiBorderWidth?>.Read(in UiStringView view) => view.AsSpan() is UiCommands.NullArg ? null : ((IArgReader<UiBorderWidth>)this).Read(view);
    UiUnit IArgReader<UiUnit>.Read(in UiStringView view) => UiUnit.Parse(view);
    UiUnit? IArgReader<UiUnit?>.Read(in UiStringView view) => view.AsSpan() is UiCommands.NullArg ? null : ((IArgReader<UiUnit>)this).Read(view);

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