using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        public UiButton Checkbox(BaseUiComponent parent, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd)
        {
            return TextButton(parent, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, pos, cmd);
        }
    }
}