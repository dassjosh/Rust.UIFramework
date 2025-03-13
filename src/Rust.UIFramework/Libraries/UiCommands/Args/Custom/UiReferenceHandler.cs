using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Libraries;

internal class UiReferenceHandler : IArgHandler<UiReference>, IArgHandler<UiReference?>, ISingleton
{
    private const string Separator = "&:&";

    private UiReferenceHandler() { }
    
    UiReference IArgReader<UiReference>.Read(ReadOnlySpan<char> arg)
    {
        arg.ParseNextString(Separator, out ReadOnlySpan<char> parent, out arg);
        arg.ParseNextString(Separator, out ReadOnlySpan<char> name, out arg);
        return new UiReference(parent.ToString(), name.ToString());
    }
    
    UiReference? IArgReader<UiReference?>.Read(ReadOnlySpan<char> arg) => arg is UiCommands.NullArg ? null : ((IArgReader<UiReference>)this).Read(arg);

    public void Write(UiArgWriter writer, UiReference arg)
    {
        writer.AppendQuote();
        writer.Append(arg.Parent);
        writer.Append(Separator);
        writer.Append(arg.Name);
        writer.AppendQuote();
    }
    
    public void Write(UiArgWriter writer, UiReference? arg)
    {
        if (!arg.HasValue)
        {
            writer.AppendNull();
            return;
        }
        
        Write(writer, arg.Value);
    }
}