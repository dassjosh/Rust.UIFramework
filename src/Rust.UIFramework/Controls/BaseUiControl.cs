using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Controls
{
    public abstract class BaseUiControl : BasePoolable
    {
        private bool _hasRendered;

        protected static T CreateControl<T>() where T : BaseUiControl, new()
        {
            return UiFrameworkPool.Get<T>();
        }

        public void RenderControl(BaseUiBuilder builder)
        {
            if (!_hasRendered)
            {
                Render(builder);
                _hasRendered = true;
            }
        }

        protected virtual void Render(BaseUiBuilder builder)
        {
            
        }

        protected override void EnterPool()
        {
            _hasRendered = false;
        }
    }
}