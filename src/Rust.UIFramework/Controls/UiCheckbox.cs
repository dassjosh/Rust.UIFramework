using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiCheckbox : BaseUiControl
    {
        private const string DefaultCheckmark = "<b>✓</b>";
        
        public bool IsChecked;
        public string Checkmark = DefaultCheckmark;
        public UiButton Button;
        public UiLabel Label;
        
        public static UiCheckbox CreateCheckbox(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command)
        {
            UiCheckbox control = CreateControl<UiCheckbox>();
            control.IsChecked = isChecked;
            control.Button = builder.CommandButton(parent, pos, offset, backgroundColor, command);
            control.Label = builder.Label(control.Button, UiPosition.Full, string.Empty, textSize, textColor);
            control.Button.AddElementOutline(UiColor.Black.WithAlpha(0.75f));
            return control;
        }
        
        protected override void Render(BaseUiBuilder builder)
        {
            if (IsChecked)
            {
                Label.Text.Text = Checkmark;
            }
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            IsChecked = false;
            Button = null;
            Label = null;
            Checkmark = DefaultCheckmark;
        }
    }
}