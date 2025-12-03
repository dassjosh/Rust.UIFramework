using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IBaseLayoutComponent : IBaseTypedComponent
{
	UnityEngine.TextAnchor ChildAlignment { get; set; }
	Oxide.Ext.UiFramework.Types.UiPadding Padding { get; set; }
}


