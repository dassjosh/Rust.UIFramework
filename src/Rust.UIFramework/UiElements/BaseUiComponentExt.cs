using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

// ReSharper disable MemberCanBePrivate.Global
namespace Oxide.Ext.UiFramework.UiElements;

public static partial class BaseUiComponentExt
{
    extension<T>(T component) where T : BaseUiComponent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetParent(UiLayer layer) => SetParent(component, UiLayerCache.GetLayer(layer));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetPosition(in UiPosition position, in UiOffset offset)
        {
            component.Position = position;
            component.Offset = offset;
            return component; 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Update() => component.SetUpdate(UpdateMode.Update);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Replace() => component.SetUpdate(UpdateMode.Replace);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T SetLayout(BaseUiLayout layout)
        {
            layout.AddElement(component);
            return component;
        }
    }
}