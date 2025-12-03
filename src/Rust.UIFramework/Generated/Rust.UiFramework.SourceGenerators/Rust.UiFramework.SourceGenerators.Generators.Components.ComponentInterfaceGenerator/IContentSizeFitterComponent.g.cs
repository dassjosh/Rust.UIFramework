using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IContentSizeFitterComponent : IBaseTypedComponent
{
	UnityEngine.UI.ContentSizeFitter.FitMode HorizontalFit { get; set; }
	UnityEngine.UI.ContentSizeFitter.FitMode VerticalFit { get; set; }
}


