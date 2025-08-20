using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder.UI;

public partial class UiBuilder
{
    public static UiBuilder Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset) => Create(plugin, reference, out UiSection _).SetPosition(pos, offset);
    public static UiBuilder Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color)
    {
        UiBuilder builder = Create(plugin, reference, out UiPanel panel).SetPosition(pos, offset);
        panel.SetColor(color);
        return builder;
    }
    
    public static UiBuilder Create<T>(IUiFrameworkPlugin plugin, in UiReference reference, out T root) where T : BaseUiComponent, new() => Create(plugin).SetRoot(reference, out root);
    public static UiBuilder Create(IUiFrameworkPlugin plugin) => Create(plugin.Pool);
    private static UiBuilder Create(UiPluginPool pool) => pool.Get<UiBuilder>();


    /// <summary>
    /// Creates a UiBuilder to update existing UI
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="mode">Update mode to use</param>
    /// <returns></returns>
    public static UiBuilder CreateUpdate(IUiFrameworkPlugin plugin, UpdateMode mode = UpdateMode.Update) => Create(plugin).SetUpdateMode(mode);
    
    /// <summary>
    /// Creates a UiBuilder that is designed to be a popup modal
    /// </summary>
    /// <param name="reference">Name and Parent of the UI</param>
    /// <param name="modalSize">Dimensions of the modal</param>
    /// <param name="modalColor">Modal form color</param>
    /// <param name="closeCommand">Command to run when the user clicks outside the modal</param>
    /// <returns></returns>
    public static UiBuilder CreateModal(IUiFrameworkPlugin plugin, in UiReference reference, in UiOffset modalSize, UiColor modalColor, string closeCommand = null, ButtonType buttonType = ButtonType.Close)
    {
        return CreateModal(plugin.Pool, reference, modalSize, modalColor, new UiColor(0, 0, 0, 0.5f), UiMaterials.Content.Ui.UiBackgroundBlurInGameMenu, closeCommand, buttonType);
    }

    /// <summary>
    /// Creates a UiBuilder that is designed to be a popup modal
    /// </summary>
    /// <param name="reference">Name and Parent of the UI</param>
    /// <param name="modalSize">Dimensions of the modal</param>
    /// <param name="modalColor">Modal form color</param>
    /// <param name="modalBackgroundColor">Color of the fullscreen background</param>
    /// <param name="backgroundMaterial">Material of the full-screen background</param>
    /// <param name="closeCommand">Command to run when the user clicks outside the modal</param>
    /// <returns></returns>
    public static UiBuilder CreateModal(IUiFrameworkPlugin plugin, in UiReference reference, in UiOffset modalSize, UiColor modalColor, UiColor modalBackgroundColor, string backgroundMaterial = null, string closeCommand = null, ButtonType buttonType = ButtonType.Close)
    {
        return CreateModal(plugin.Pool, reference, modalSize, modalColor, modalBackgroundColor, backgroundMaterial, closeCommand, buttonType);
    }
    
    private static UiBuilder CreateModal(UiPluginPool pool, in UiReference reference, in UiOffset modalSize, UiColor modalColor, UiColor modalBackgroundColor, string backgroundMaterial = null, string closeCommand = null, ButtonType buttonType = ButtonType.Close)
    {
        UiBuilder builder = Create(pool).SetRoot(reference, out UiPanel backgroundBlur);
        backgroundBlur.SetPosition(UiPosition.Full).SetColor(modalBackgroundColor).SetMaterial(backgroundMaterial);
        builder.Button(builder.Root, UiPosition.Full, default, UiColors.Clear, closeCommand ?? reference.Name, buttonType);

        UiPanel root = builder.Panel(builder.Root, UiPosition.Full, modalSize, modalColor);
        builder.OverrideRoot(root);    
        return builder;
    }

    /// <summary>
    /// Create a UI Builder with a button that will close the UI when clicked outside the UI window
    /// </summary>
    /// <param name="plugin">Plugin the UI is for</param>
    /// <param name="reference">Name and Parent of the UI</param>
    /// <param name="pos">Position of the UI</param>
    /// <param name="offset">Offset of the UI</param>
    /// <param name="color">Color of the UI</param>
    /// <param name="closeCommand">Command to run when outside the UI is clicked</param>
    /// <returns></returns>
    public static UiBuilder CreateRootWithOutsideClose(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color, string closeCommand = null, ButtonType buttonType = ButtonType.Command)
    {
        return CreateRootWithOutsideClose(plugin.Pool, reference, pos, offset, color, closeCommand, buttonType);
    }
    
    private static UiBuilder CreateRootWithOutsideClose(UiPluginPool pool, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color, string closeCommand, ButtonType buttonType)
    {
        UiBuilder builder = Create(pool).SetRoot(reference, out UiSection _);
        builder.Button(builder.Root, UiPosition.Full, default, UiColors.Clear, closeCommand, buttonType);
        UiPanel panel = builder.Panel(builder.Root, pos, offset, color);
        builder.OverrideRoot(panel);
        return builder;
    }

    /// <summary>
    /// Creates a UI builder that when created before your main UI will run a command if the user clicks outside the UI window
    /// </summary>
    /// <param name="plugin">Plugin the UI is for</param>
    /// <param name="reference">Name and Parent of the UI</param>
    /// <param name="command">Command to run when the button is clicked</param>
    /// <param name="buttonType">Type of button</param>
    /// <returns></returns>
    public static UiBuilder CreateOutsideClose(IUiFrameworkPlugin plugin, in UiReference reference, string command, ButtonType buttonType = ButtonType.Command)
    {
        return CreateOutsideClose(plugin.Pool, reference, command, buttonType);
    }
    
    private static UiBuilder CreateOutsideClose(UiPluginPool pool, in UiReference reference, string command, ButtonType buttonType)
    {
        UiBuilder builder = Create(pool).SetRoot(reference, out UiButton button).NeedsMouse();
        builder.Button(button, UiPosition.Full, default, UiColors.Clear, command, buttonType);
        return builder;
    }

    public static UiBuilder Popover(IUiFrameworkPlugin plugin, in UiReference parent, Vector2 size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiSprites.Content.Ui.UiBackgroundRounded, UiColor? outlineColor = null)
    {
        return Popover(plugin.Pool, parent, size, backgroundColor, position, menuSprite, outlineColor);
    }
    
    private static UiBuilder Popover(UiPluginPool pool, in UiReference parent, Vector2 size, UiColor backgroundColor, PopoverPosition position, string menuSprite, UiColor? outlineColor)
    {
        string name = $"{parent.Name}_Popover";
        UiBuilder builder = Create(pool).SetRoot(parent.WithChild(name), out UiSection _);
            
        UiPosition anchor = position switch
        {
            PopoverPosition.Top or PopoverPosition.Left => UiPosition.TopLeft,
            PopoverPosition.Right => UiPosition.TopRight,
            PopoverPosition.Bottom => UiPosition.BottomLeft,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
        
        UiOffset offset = position switch
        {
            PopoverPosition.Top => new UiOffset(0, 1, 1 + size.x, size.y),
            PopoverPosition.Left => new UiOffset(-size.x, -size.y - 1, 0, -1),
            PopoverPosition.Right => new UiOffset(0, -size.y - 1, size.x, -1),
            PopoverPosition.Bottom => new UiOffset(1, -size.y, 1 + size.x, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
        
        builder.Button(builder.Root, UiPosition.Full, new UiOffset(9999 * 2), UiColors.Clear, name, ButtonType.Close);
        UiPanel background = builder.Panel(builder.Root, anchor, offset, backgroundColor).SetSprite(menuSprite).SetImageType(Image.Type.Sliced);
        background.AddOutline(outlineColor ?? UiColors.Black.WithAlpha(0.75f));
        builder.OverrideRoot(background);
        
        return builder;
    }
}