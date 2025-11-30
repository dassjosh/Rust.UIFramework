using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public static class IAnimatorExt
{
    extension<T>(IAnimator<T> animator)
    {
        public IAnimator<TTo> Convert<TTo>(IUiFrameworkPlugin plugin, IUiConvertable<TTo, T> convertable) => ConvertAnimator.Create(plugin, animator, convertable);
    }
}