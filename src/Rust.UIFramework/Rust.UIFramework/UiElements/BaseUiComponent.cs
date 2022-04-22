using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class BaseUiComponent : Pool.IPooled
    {
        public string Name;
        public string Parent;
        public float FadeOut;
        private Position _position;
        private Offset? _offset;
        private bool _inPool = true;

        protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos.ToPosition();
            component._offset = offset?.ToOffset();
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }

        protected static T CreateBase<T>(Position pos, Offset? offset) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos;
            component._offset = offset;
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }

        protected static T CreateBase<T>(UiPosition pos) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos.ToPosition();
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }

        public virtual void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, _position, _offset);
        }

        public void SetFadeOut(float duration)
        {
            FadeOut = duration;
        }

        public virtual void SetFadeIn(float duration)
        {
            throw new NotSupportedException($"FadeIn is not supported on this component {GetType().Name}");
        }

        public void UpdateOffset(UiOffset offset)
        {
            _offset = offset?.ToOffset();
        }

        public void UpdatePosition(UiPosition position, UiOffset offset = null)
        {
            _position = position.ToPosition();
            _offset = offset?.ToOffset();
        }

        public virtual void EnterPool()
        {
            Name = null;
            Parent = null;
            FadeOut = 0;
            _position = default(Position);
            _offset = null;
            _inPool = true;
        }

        public virtual void LeavePool()
        {
            _inPool = false;
        }
    }
}