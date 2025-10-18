using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface ITextComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> Color { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> FadeIn { get; }
	Oxide.Ext.UiFramework.Types.Tracked<int> FontSize { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Font { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.TextAnchor> Align { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Text { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.VerticalWrapMode> VerticalOverflow { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> PlaceholderFor { get; }
}


