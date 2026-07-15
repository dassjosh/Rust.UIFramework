using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Builder;

public class PieMenuItemBuilder : BasePoolable
{
    public string Name;
    public string Description;
    public string Command;
    public string Image;
    public bool Disabled;
    public bool Selected;
    public int Order;
    public PieMenu.MenuOption.ColorMode.PieMenuSpriteColorOption ColorMode;
    public UiColor Color;
    public string NextCommand;
    public string PrevCommand;
    public string DisabledCommand;

    public static PieMenuItemBuilder Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<PieMenuItemBuilder>().Init();

    public PieMenuItemBuilder Init()
    {
        return this;
    }

    public PieMenuItemBuilder SetName(string name)
    {
        Name = name;
        return this;
    }

    public PieMenuItemBuilder SetDescription(string description)
    {
        Description = description;
        return this;
    }

    public PieMenuItemBuilder SetCommand(string command)
    {
        Command = command;
        return this;
    }

    public PieMenuItemBuilder SetImage(string image)
    {
        Image = image;
        return this;
    }

    public PieMenuItemBuilder SetDisabled(bool disabled)
    {
        Disabled = disabled;
        return this;
    }

    public PieMenuItemBuilder SetSelected(bool selected)
    {
        Selected = selected;
        return this;
    }

    public PieMenuItemBuilder SetOrder(int order)
    {
        Order = order;
        return this;
    }

    public PieMenuItemBuilder SetColorMode(PieMenu.MenuOption.ColorMode.PieMenuSpriteColorOption colorMode)
    {
        ColorMode = colorMode;
        return this;
    }

    public PieMenuItemBuilder SetColor(UiColor color)
    {
        Color = color;
        return this;
    }

    public PieMenuItemBuilder SetNextCommand(string command)
    {
        NextCommand = command;
        return this;
    }

    public PieMenuItemBuilder SetPreviousCommand(string command)
    {
        PrevCommand = command;
        return this;
    }

    public PieMenuItemBuilder SetDisabledCommand(string command)
    {
        DisabledCommand = command;
        return this;
    }

    public CustomPieMenu Build()
    {
        CustomPieMenu menu = Facepunch.Pool.Get<CustomPieMenu>();
        menu.name = Name;
        menu.description = Description;
        menu.command = Command;
        menu.disabled = Disabled;
        menu.selected = Selected;
        menu.order = Order;
        menu.colorMode = (int)ColorMode;
        menu.color = Color;
        menu.nextCommand = NextCommand;
        menu.prevCommand = PrevCommand;
        menu.disabledCommand = DisabledCommand;
        if (!string.IsNullOrEmpty(Image))
        {
            if (uint.TryParse(Image, out uint imageId))
            {
                menu.imageId = imageId;
            }
            else
            {
                menu.sprite = Image;
            }
        }
        return menu;
    }
}