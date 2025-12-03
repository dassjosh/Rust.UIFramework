using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiPlayingCardTrackable : IBaseUiComponentTrackable
{
	IPlayingCardComponentTrackable Card { get; }
}


