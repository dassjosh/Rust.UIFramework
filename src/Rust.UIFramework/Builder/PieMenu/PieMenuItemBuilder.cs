using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Builder;

public class PieMenuItemBuilder : BasePoolable
{
    private string _name;
    private string _description;
    private string _command;
    private string _image;
    private bool _disabled;
    private bool _selected;
    private int _order;
    private PieMenu.MenuOption.ColorMode.PieMenuSpriteColorOption _colorMode;
    private UiColor _color;
    private string _nextCommand;
    private string _prevCommand;
    private string _disabledCommand;

    public static PieMenuItemBuilder Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<PieMenuItemBuilder>().Init();

    public PieMenuItemBuilder Init() => this;

    public PieMenuItemBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public PieMenuItemBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public PieMenuItemBuilder SetCommand(string command)
    {
        _command = command;
        return this;
    }

    public PieMenuItemBuilder SetImage(string image)
    {
        _image = image;
        return this;
    }

    public PieMenuItemBuilder SetDisabled(bool disabled)
    {
        _disabled = disabled;
        return this;
    }

    public PieMenuItemBuilder SetSelected(bool selected)
    {
        _selected = selected;
        return this;
    }

    public PieMenuItemBuilder SetOrder(int order)
    {
        _order = order;
        return this;
    }

    public PieMenuItemBuilder SetColorMode(PieMenu.MenuOption.ColorMode.PieMenuSpriteColorOption colorMode)
    {
        _colorMode = colorMode;
        return this;
    }

    public PieMenuItemBuilder SetColor(UiColor color)
    {
        _color = color;
        return this;
    }

    public PieMenuItemBuilder SetNextCommand(string command)
    {
        _nextCommand = command;
        return this;
    }

    public PieMenuItemBuilder SetPreviousCommand(string command)
    {
        _prevCommand = command;
        return this;
    }

    public PieMenuItemBuilder SetDisabledCommand(string command)
    {
        _disabledCommand = command;
        return this;
    }

    public CustomPieMenu Build()
    {
        CustomPieMenu menu = Facepunch.Pool.Get<CustomPieMenu>();
        menu.name = _name;
        menu.description = _description;
        menu.command = _command;
        menu.disabled = _disabled;
        menu.selected = _selected;
        menu.order = _order;
        menu.colorMode = (int)_colorMode;
        menu.color = _color;
        menu.nextCommand = _nextCommand;
        menu.prevCommand = _prevCommand;
        menu.disabledCommand = _disabledCommand;
        if (!string.IsNullOrEmpty(_image))
        {
            if (uint.TryParse(_image, out uint imageId))
            {
                menu.imageId = imageId;
            }
            else
            {
                menu.sprite = _image;
            }
        }
        return menu;
    }

    protected override void EnterPool()
    {
        _name = null;
        _description = null;
        _command = null;
        _image = null;
        _disabled = false;
        _selected = false;
        _order = 0;
        _colorMode = PieMenu.MenuOption.ColorMode.PieMenuSpriteColorOption.CustomColor;
        _color = UiColors.Clear;
        _nextCommand = null;
        _prevCommand = null;
        _disabledCommand = null;
    }
}