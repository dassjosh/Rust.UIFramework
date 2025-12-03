using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface INineSliceComponent : IImageComponent
{
	string Png { get; set; }
	Oxide.Ext.UiFramework.Types.UiBorderWidth Slice { get; set; }

	Oxide.Ext.UiFramework.Components.NineSliceComponent SetPng(string png);
	Oxide.Ext.UiFramework.Components.NineSliceComponent SetSlice(in Oxide.Ext.UiFramework.Types.UiBorderWidth slice);
}


