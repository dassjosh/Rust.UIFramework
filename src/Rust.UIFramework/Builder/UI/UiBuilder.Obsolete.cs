using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Builder.UI;

#pragma warning disable S1133 // SonarCube Obsolete remove one day

public partial class UiBuilder
{
    #region Create
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset) Instead")] 
    public static UiBuilder Create(in UiPosition pos, in UiReference reference) => Create(pos, reference.Name, reference.Parent);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset) Instead")] 
    public static UiBuilder Create(in UiPosition pos, string name, in UiReference reference) => Create(pos, name, reference.Name);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset) Instead")] 
    public static UiBuilder Create(in UiPosition pos, string name, string parent) => Create(pos, default(UiOffset), name, parent);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, new UiReference(UiLayer parent, string name), in UiPosition pos, in UiOffset offset) Instead")] 
    public static UiBuilder Create(in UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), name, UiLayerCache.GetLayer(parent));
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, new UiReference(UiLayer parent, string name), in UiPosition pos, in UiOffset offset) Instead")] 
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, name, UiLayerCache.GetLayer(parent));
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset) Instead")] 
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, string name, string parent) => Create(UiFrameworkPool.Get<UiSection>().SetPosition(pos, offset), name, parent);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, UiColor color, string name, string parent) => Create(pos, default, color, name, parent);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, UiColor color, in UiReference reference) => Create(pos, default, color, reference.Name, reference.Parent);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, UiColor color, string name, in UiReference reference) => Create(pos, default, color, name, reference.Name);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, new UiReference(UiLayer parent, string name), in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default, color, name, UiLayerCache.GetLayer(parent));
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, new UiReference(UiLayer parent, string name), in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, color, name, UiLayerCache.GetLayer(parent));
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, UiColor color, string name, string parent)
    {
        UiPanel panel = UiFrameworkPool.Get<UiPanel>().SetColor(color);
        panel.SetPosition(pos, offset);
        return Create(panel, name, parent);
    }
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, UiColor color, in UiReference reference) => Create(pos, offset, color, reference.Name, reference.Parent);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color) Instead")] 
    public static UiBuilder Create(in UiPosition pos, in UiOffset offset, UiColor color, string name, in UiReference reference) => Create(pos, offset, color, name, reference.Name);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, new UiReference(UiLayer parent, string name), out T root) Instead")] 
    public static UiBuilder Create(BaseUiComponent root, string name, UiLayer parent = UiLayer.Overlay) => Create(root, name, UiLayerCache.GetLayer(parent));
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, out T root) Instead")] 
    public static UiBuilder Create(BaseUiComponent root, in UiReference reference) => Create(root, reference.Name, reference.Parent);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, reference.WithChild(name), out T root) Instead")] 
    public static UiBuilder Create(BaseUiComponent root, string name, in UiReference reference) => Create(root, name, reference.Name);
    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin, in UiReference reference, out T root) Instead")] 
    public static UiBuilder Create(BaseUiComponent root, string name, string parent)
    {
        UiBuilder builder = Create();
        builder.SetRoot(root, name, parent);
        return builder;
    }

    [Obsolete("Use UiBuilder.Create(IUiFrameworkPlugin plugin) Instead")]
    public static UiBuilder Create()
    {
        return UiFrameworkPool.Get<UiBuilder>();
    }

    [Obsolete("Use UiBuilder.CreateModal(UiPluginPool pool, in UiReference reference, in UiOffset modalSize, UiColor modalColor, UiColor modalBackgroundColor, string backgroundMaterial = null, string closeCommand = null, ButtonType buttonType = ButtonType.Close) Instead")]
    public static UiBuilder CreateModal(in UiOffset modalSize, UiColor modalColor, string name, UiLayer layer = UiLayer.Overlay, string outsideCloseCommand = null)
    {
        return CreateModal(UiFrameworkPool.Pool, new UiReference(layer, name), modalSize, modalColor, UiColors.Clear, null, outsideCloseCommand);
    }

    [Obsolete("Use UiBuilder.CreateModal(UiPluginPool pool, in UiReference reference, in UiOffset modalSize, UiColor modalColor, UiColor modalBackgroundColor, string backgroundMaterial = null, string closeCommand = null, ButtonType buttonType = ButtonType.Close) Instead")]
    public static UiBuilder CreateModal(in UiOffset modalSize, UiColor modalColor, UiColor modalBackgroundColor, string name, UiLayer layer = UiLayer.Overlay, string backgroundMaterial = null, string outsideCloseCommand = null)
    {
        return CreateModal(UiFrameworkPool.Pool, new UiReference(layer, name), modalSize, modalColor, modalBackgroundColor, backgroundMaterial, outsideCloseCommand);
    }

    [Obsolete("Use UiBuilder.CreateRootWithOutsideClose(UiPluginPool pool, in UiReference reference, in UiPosition pos, in UiOffset offset, UiColor color, string closeCommand, ButtonType buttonType) Instead")]
    public static UiBuilder CreateRootWithOutsideClose(in UiPosition pos, in UiOffset offset, UiColor color, string name, string closeCommand, UiLayer parent = UiLayer.Overlay)
    {
        return CreateRootWithOutsideClose(UiFrameworkPool.Pool, new UiReference(parent, name), pos, offset, color, closeCommand, ButtonType.Command);
    }

    [Obsolete("Use UiBuilder.CreateOutsideClose(UiPluginPool pool, in UiReference reference, string command, ButtonType buttonType) Instead")]
    public static UiBuilder CreateOutsideClose(string command, string name, UiLayer layer = UiLayer.Overlay)
    {
        return CreateOutsideClose(UiFrameworkPool.Pool, new UiReference(layer, name), command, ButtonType.Command);
    }
    
    /// <summary>
    /// Creates a UI builder that will hold mouse input so the mouse doesn't reset position when updating other UI's
    /// </summary>
    /// <param name="name">Name of the UI</param>
    /// <param name="layer">Layer the UI is on</param>
    /// <returns></returns>
    [Obsolete("This isn't needed anymore due to UI destroy and create being in the same call")]
    public static UiBuilder CreateMouseLock(string name, UiLayer layer = UiLayer.Overlay)
    {
        UiBuilder builder = Create(UiPosition.None, UiColors.Clear, name, UiLayerCache.GetLayer(layer));
        builder.NeedsMouse();
        return builder;
    }

    [Obsolete("Use UiBuilder.Popover(UiPluginPool pool, in UiReference parent, Vector2 size, UiColor backgroundColor, PopoverPosition position, string menuSprite, UiColor? outlineColor = null) Instead")]
    public static UiBuilder Popover(in UiReference parent, Vector2 size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiSprites.Content.Ui.UiBackgroundRounded)
    {
        return Popover(UiFrameworkPool.Pool, parent, size, backgroundColor, position, menuSprite);
    }
    #endregion
}