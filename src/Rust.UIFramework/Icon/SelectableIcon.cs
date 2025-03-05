using System;
using Oxide.Ext.UiFramework.Libraries.UiIcons;
using Oxide.Ext.UiFramework.Types;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Icon;

public readonly struct SelectableIcon
{
    private readonly IconId _iconId;
    private readonly ushort _icon;
    
    public SelectableIcon(Enum @enum)
    {
        _iconId = Singleton<UiIcons>.Instance.GetIconId(@enum);
        _icon =  Convert.ToUInt16(@enum);
    }

    private SelectableIcon(Type enumType, ushort icon)
    {
        _iconId = Singleton<UiIcons>.Instance.GetIconId(enumType);
        _icon = icon;
    }
    
    // public SelectableIcon(Icons icon) : this(IconType.RustIcons, (ushort)icon) {}
    //
    // public SelectableIcon(FontAwesomeRegularIcons icon) : this(IconType.FontAwesomeRegular, (ushort)icon) {}
    //
    // public SelectableIcon(FontAwesomeSolidIcons icon) : this(IconType.FontAwesomeSolid, (ushort)icon) { }

    public static implicit operator SelectableIcon(Icons icon) => new(typeof(Icons), (ushort)icon);
    public static implicit operator SelectableIcon(FontAwesomeRegularIcons icon) => new(icon);
    public static implicit operator SelectableIcon(FontAwesomeSolidIcons icon) => new(icon);
    public static implicit operator SelectableIcon(Enum icon) => new SelectableIcon(icon);

    public string GetIcon()
    {
        return Singleton<UiIcons>.Instance.GetIconUrl(_iconId, _icon);
    }
}