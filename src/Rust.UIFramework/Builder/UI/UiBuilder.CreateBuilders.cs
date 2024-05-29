using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls.Popover;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Builder.UI;

public partial class UiBuilder
{
    public static UiBuilder Create(in UiPosition pos, string name, string parent) => Create(pos, default(UiOffset), name, parent);
    public static UiBuilder Create(in UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), name, UiLayerCache.GetLayer(parent));
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, name, UiLayerCache.GetLayer(parent));
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, string name, string parent) => Create(UiSection.Create(pos, offset), name, parent);
    public static UiBuilder Create(in UiPosition pos, UiColor color, string name, string parent) => Create(pos, default, color, name, parent);
    public static UiBuilder Create(in UiPosition pos, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default, color, name, UiLayerCache.GetLayer(parent));
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, color, name, UiLayerCache.GetLayer(parent));
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, UiColor color, string name, string parent) => Create(UiPanel.Create(pos, offset, color), name, parent);
    public static UiBuilder Create(BaseUiComponent root, string name, UiLayer parent = UiLayer.Overlay) => Create(root, name, UiLayerCache.GetLayer(parent));
    public static UiBuilder Create(BaseUiComponent root, string name, string parent)
    {
        UiBuilder builder = Create();
        builder.SetRoot(root, name, parent);
        return builder;
    }

    public static UiBuilder Create()
    {
        return UiFrameworkPool.Get<UiBuilder>();
    }

    /// <summary>
    /// Creates a UiBuilder that is designed to be a popup modal
    /// </summary>
    /// <param name="modalSize">Dimensions of the modal</param>
    /// <param name="modalColor">Modal form color</param>
    /// <param name="name">Name of the UI</param>
    /// <param name="layer">Layer the UI is on</param>
    /// <param name="outsideCloseCommand">Command to run when the user clicks outside the modal</param>
    /// <returns></returns>
    public static UiBuilder CreateModal(in UiOffset modalSize, UiColor modalColor, string name, UiLayer layer = UiLayer.Overlay, string outsideCloseCommand = null)
    {
        return CreateModal(modalSize, modalColor, new UiColor(0, 0, 0, 0.5f), name, layer, UiConstants.Materials.InGameBlur, outsideCloseCommand);
    }

    /// <summary>
    /// Creates a UiBuilder that is designed to be a popup modal
    /// </summary>
    /// <param name="modalSize">Dimensions of the modal</param>
    /// <param name="modalColor">Modal form color</param>
    /// <param name="modalBackgroundColor">Color of the fullscreen background</param>
    /// <param name="name">Name of the UI</param>
    /// <param name="layer">Layer the UI is on</param>
    /// <param name="backgroundMaterial">Material of the full screen background</param>
    /// <param name="outsideCloseCommand">Command to run when the user clicks outside the modal</param>
    /// <returns></returns>
    public static UiBuilder CreateModal(in UiOffset modalSize, UiColor modalColor, UiColor modalBackgroundColor, string name, UiLayer layer = UiLayer.Overlay, string backgroundMaterial = null, string outsideCloseCommand = null)
    {
        UiPanel backgroundBlur = UiPanel.Create(UiPosition.Full, default, modalBackgroundColor);
        backgroundBlur.SetMaterial(backgroundMaterial);
            
        UiBuilder builder = Create(backgroundBlur, name, layer);
        if (!string.IsNullOrEmpty(outsideCloseCommand))
        {
            builder.CommandButton(builder.Root, UiPosition.Full, UiColor.Clear, outsideCloseCommand);
        }
            
        UiPanel modal = UiPanel.Create(UiPosition.MiddleMiddle, modalSize, modalColor);
        builder.AddComponent(modal, backgroundBlur);
        builder.OverrideRoot(modal);
        return builder;
    }
        
    public static UiBuilder CreateRootWithOutsideClose(in UiPosition pos, in UiOffset offset, UiColor color, string name, string closeCommand, UiLayer parent = UiLayer.Overlay)
    {
        UiSection mainRoot = UiSection.Create(UiPosition.Full, default);
        UiBuilder builder = Create(mainRoot, name, UiLayerCache.GetLayer(parent));
        builder.CommandButton(builder.Root, UiPosition.Full, default, UiColor.Clear, closeCommand);
        UiPanel panel =builder.Panel(builder.Root, pos, offset, color);
        builder.OverrideRoot(panel);
        return builder;
    }
        
    /// <summary>
    /// Creates a UI builder that when created before your main UI will run a command if the user click outside of the UI window
    /// </summary>
    /// <param name="command">Command to run when the button is clicked</param>
    /// <param name="name">Name of the UI</param>
    /// <param name="layer">Layer the UI is on</param>
    /// <returns></returns>
    public static UiBuilder CreateOutsideClose(string command, string name, UiLayer layer = UiLayer.Overlay)
    {
        UiBuilder builder = Create(UiButton.CreateCommand(UiPosition.Full, default, UiColor.Clear, command), name, layer);
        builder.NeedsMouse();
        return builder;
    }
        
    /// <summary>
    /// Creates a UI builder that will hold mouse input so the mouse doesn't reset position when updating other UI's
    /// </summary>
    /// <param name="name">Name of the UI</param>
    /// <param name="layer">Layer the UI is on</param>
    /// <returns></returns>
    public static UiBuilder CreateMouseLock(string name, UiLayer layer = UiLayer.Overlay)
    {
        UiBuilder builder = Create(UiPosition.None, UiColor.Clear, name, UiLayerCache.GetLayer(layer));
        builder.NeedsMouse();
        return builder;
    }
        
    public static UiPopover Popover(string parentName, Vector2Int size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
    {
        UiPopover control = UiPopover.Create(parentName, size, backgroundColor, position, menuSprite);
        return control;
    }
}