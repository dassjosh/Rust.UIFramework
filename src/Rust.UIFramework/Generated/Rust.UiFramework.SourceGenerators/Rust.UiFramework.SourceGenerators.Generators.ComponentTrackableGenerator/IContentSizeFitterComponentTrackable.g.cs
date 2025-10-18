using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IContentSizeFitterComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ContentSizeFitter.FitMode> HorizontalFit { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ContentSizeFitter.FitMode> VerticalFit { get; }
}


