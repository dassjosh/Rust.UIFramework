using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IRawImageComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> Color { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> FadeIn { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Image { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Material { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> PlaceholderFor { get; }
}


