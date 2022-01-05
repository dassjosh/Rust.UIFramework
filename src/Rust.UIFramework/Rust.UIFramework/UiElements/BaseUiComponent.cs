using Newtonsoft.Json;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.UiElements
{
    public class BaseUiComponent : Pool.IPooled
    {
        public string Name;
        public string Parent;
        public float FadeOut;
        public float FadeIn;
        private Position _position;
        private Offset? _offset;

        protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos.ToPosition();
            component._offset = offset?.ToOffset();
            return component;
        }

        protected static T CreateBase<T>(Position pos, Offset? offset) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos;
            component._offset = offset;
            return component;
        }

        protected static T CreateBase<T>(UiPosition pos) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos.ToPosition();
            return component;
        }

        public virtual void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, ref _position, ref _offset);
        }

        public void SetFadeout(float duration)
        {
            FadeOut = duration;
        }

        public void SetFadeIn(float duration)
        {
            FadeIn = duration;
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
            FadeIn = 0;
            _position = default(Position);
            _offset = null;
        }

        public virtual void LeavePool()
        {
        }
    }
}