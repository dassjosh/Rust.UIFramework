using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IBaseUiComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<float> FadeOut { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> Active { get; }
}


