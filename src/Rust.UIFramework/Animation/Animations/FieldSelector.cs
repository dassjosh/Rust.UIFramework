using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public delegate Tracked<TField> FieldSelector<TField, in T>(T element) where T : BaseUiComponent;