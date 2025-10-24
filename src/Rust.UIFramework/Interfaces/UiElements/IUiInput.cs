using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiInput : IUiBaseText, IFadeIn<UiInput>, IUiColor<UiInput>
{
    int CharsLimit { get; set; }
    string Command { get; set; }
    InputMode Mode { get; set; }
    InputField.LineType LineType { get; set; }
    UiReference Placeholder { get; set; }
}