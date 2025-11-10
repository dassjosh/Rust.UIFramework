using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface INineSliceComponentTrackable : IImageComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<string> Png { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiBorderWidth> Slice { get; }
}


