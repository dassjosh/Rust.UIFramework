using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IItemIconComponentTrackable : IImageComponentTrackable
{

	Oxide.Ext.UiFramework.Types.Tracked<int> ItemId { get; }
	Oxide.Ext.UiFramework.Types.Tracked<ulong> SkinId { get; }

}


