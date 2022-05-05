using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        /// <summary>
        /// Creates a UiBuilder that is designed to be a popup modal
        /// </summary>
        /// <param name="offset">Dimensions of the modal</param>
        /// <param name="modalColor">Modal form color</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateModal(UiOffset offset, UiColor modalColor, string name, UiLayer layer = UiLayer.Overlay)
        {
            UiBuilder builder = new UiBuilder();
            UiPanel backgroundBlur = UiPanel.Create(UiPosition.Full, null, new UiColor(0, 0, 0, 0.5f));
            backgroundBlur.AddMaterial(UiConstants.Materials.InGameBlur);
            builder.SetRoot(backgroundBlur, name, UiConstants.UiLayers.GetLayer(layer));
            UiPanel modal = UiPanel.Create(UiPosition.MiddleMiddle, offset, modalColor);
            builder.AddComponent(modal, backgroundBlur);
            builder.OverrideRoot(modal);
            return builder;
        }
        
        /// <summary>
        /// Creates a UI builder that when created before your main UI will run a command if the user click outside of the UI window
        /// </summary>
        /// <param name="cmd">Command to run when the button is clicked</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateOutsideClose(string cmd, string name, UiLayer layer = UiLayer.Overlay)
        {
            UiBuilder builder = new UiBuilder();
            UiButton button = UiButton.CreateCommand(UiPosition.Full, null, UiColors.StandardColors.Clear, cmd);
            builder.SetRoot(button, name, UiConstants.UiLayers.GetLayer(layer));
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
            UiBuilder builder = new UiBuilder(UiColors.StandardColors.Clear, UiPosition.None, name, UiConstants.UiLayers.GetLayer(layer));
            builder.NeedsMouse();
            return builder;
        }
    }
}