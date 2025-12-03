using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

// ReSharper disable MemberCanBePrivate.Global
namespace Oxide.Ext.UiFramework.UiElements;

public static class UiComponentExt
{
    extension<T>(T component) where T : BaseUiComponent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetName(string name)
        {
            component.Name = name;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetParent(string parent)
        {
            component.Parent = parent;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetParent(UiLayer layer) => SetParent(component, UiLayerCache.GetLayer(layer));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetReference(in UiReference reference)
        {
            component.Reference = reference;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetPosition(in UiPosition position)
        {
            component.Position = position;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetPosition(in UiPosition position, in UiOffset offset)
        {
            component.Position = position;
            component.Offset = offset;
            return component; 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetOffset(in UiOffset offset)
        {
            component.Offset = offset;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetRotation(UiRotation rotation)
        {
            component.Rotation = rotation;
            return component;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetPositionTranslate(in UiTranslate translate)
        {
            component.PositionTranslate = translate;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetOffsetTranslate(in UiTranslate translate)
        {
            component.OffsetTranslate = translate;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetPositionPadding(in UiPadding padding)
        {
            component.PositionPadding = padding;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetOffsetPadding(in UiPadding padding)
        {
            component.OffsetPadding = padding;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetEnabled(bool enabled)
        {
            component.Enabled = enabled;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetActive(bool active)
        {
            component.Active = active;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetFadeOut(float duration)
        {
            component.FadeOut = duration;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetUpdate(UpdateMode mode)
        {
            component.Update = mode;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Update() => component.SetUpdate(UpdateMode.Update);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Replace() => component.SetUpdate(UpdateMode.Replace);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetPositionScale(UiScale scale)
        {
            component.PositionScale = scale;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SetOffsetScale(UiScale scale)
        {
            component.OffsetScale = scale;
            return component;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T SetLayout(BaseUiLayout layout)
        {
            layout.AddElement(component);
            return component;
        }
    }
}