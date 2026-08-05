using System;
using System.Collections.Generic;
using Facepunch;
using Network;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Plugins;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Builder;

public class PieMenuBuilder : BasePieMenuBuilder
{
    private string _closeCommand;
    private readonly List<PieMenuItemBuilder> _items = [];

    public IUiFrameworkPlugin Plugin { get; private set; }

    public static PieMenuBuilder Create(IUiFrameworkCorePlugin plugin) => plugin.PluginPool.Get<PieMenuBuilder>().Init(plugin);

    private PieMenuBuilder Init(IUiFrameworkPlugin plugin)
    {
        Plugin = plugin;
        return this;
    }

    public PieMenuBuilder SetCloseCommand(string command)
    {
        _closeCommand = command;
        return this;
    }

    public PieMenuItemBuilder AddItem()
    {
        PieMenuItemBuilder builder = PieMenuItemBuilder.Create(Plugin);
        _items.Add(builder);
        return builder;
    }

    public PieMenuBuilder AddItem(Action<PieMenuItemBuilder> action)
    {
        PieMenuItemBuilder builder = AddItem();
        action(builder);
        return this;
    }

    public PieMenuItemBuilder AddItem(string name, string description, string image, bool disabled, bool selected, int order,
        PieMenu.MenuOption.ColorMode.PieMenuSpriteColorOption colorMode, UiColor color, string command = null, string nextCommand = null, string prevCommand = null, string disabledCommand = null)
    {
        return AddItem().SetName(name).SetDescription(description).SetCommand(command).SetImage(image).SetDisabled(disabled).SetSelected(selected).SetOrder(order)
            .SetColorMode(colorMode).SetColor(color).SetNextCommand(nextCommand).SetPreviousCommand(prevCommand).SetDisabledCommand(disabledCommand);
    }

    public CachedPieMenu ToCache()
    {
       using BufferStream stream = Pool.Get<BufferStream>().Initialize();
       using CustomPie pie = ToProto();
       pie.WriteToStream(stream);
       TryDispose();
       return new CachedPieMenu(stream.GetBuffer().ToArray());
    }

    public override void SendUi(SendInfo send)
    {
        using CustomPie pie = ToProto();
        RpcFunctions.SendPieMenu(send, pie);
    }

    private CustomPie ToProto()
    {
        CustomPie pie = Pool.Get<CustomPie>();
        List<CustomPieMenu> menus = pie.menus = Pool.Get<List<CustomPieMenu>>();
        pie.closeCommand = _closeCommand;
        for (int index = 0; index < _items.Count; index++)
        {
            PieMenuItemBuilder item = _items[index];
            menus.Add(item.Build());
        }
        return pie;
    }

    protected override void EnterPool()
    {
        _closeCommand = null;
        _items.FreeValues();
    }
}