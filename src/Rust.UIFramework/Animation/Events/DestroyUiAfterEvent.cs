using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public sealed class DestroyUiAfterEvent : BasePoolable, IAnimationCompleted
{
    public string Name;
    
    public static DestroyUiAfterEvent Create(UiPluginPool pool, string name) => pool.Get<DestroyUiAfterEvent>().Init(name);

    private DestroyUiAfterEvent Init(string name)
    {
        Name = name;
        return this;
    }
    
    public void OnAnimationCompleted(BaseAnimation animation)
    {
        BaseBuilder.DestroyUi(animation.Send, Name);
    }

    protected override void EnterPool()
    {
        Name = default;
    }
}