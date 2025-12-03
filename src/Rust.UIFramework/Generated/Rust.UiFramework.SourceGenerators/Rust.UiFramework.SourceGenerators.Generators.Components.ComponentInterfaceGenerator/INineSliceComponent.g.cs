using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface INineSliceComponent : IImageComponent
{
	string Png { get; set; }
	Oxide.Ext.UiFramework.Types.UiBorderWidth Slice { get; set; }
}


