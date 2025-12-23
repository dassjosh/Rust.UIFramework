using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Animations;

public abstract class BaseAnimationTests
{
    protected static void RunSendableAnimation<T>(AnimationRef<T> animation, float timePerTick = 1f, Action<AnimationRef<T>, int> onTick = null, Action<AnimationRef<T>> onFinished = null) where T : class, ISendableAnimation
    {
        UnitTestAnimationTime time = new();
        animation.WithTime(time);
        UnitTestAnimationHelpers.QueueAnimation(animation);
        UnitTestAnimationHelpers.StartAnimation(animation);
        int tickCount = 0;
        while (animation.Animation.State <= AnimationState.Running)
        {
            animation.Animation.OnTick();
            onTick?.Invoke(animation, tickCount++);
            time.AddSeconds(timePerTick);
        }
        
        onFinished?.Invoke(animation);
        
        animation.Animation.Dispose();
    }

    protected static AnimationRef<IElementAnimation<T>> CreateElementAnimation<T>(string name) where T : BaseUiComponent, new()
    {
        return new AnimationRef<IElementAnimation<T>>(ElementAnimation<T>.Create(UnitTestHelpers.Plugin, name));
    }
    
    protected static JsonFrameworkWriter StartWriter()
    {
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.Plugin);
        writer.WriteStartArray();
        return writer;
    }

    protected static string FinishWriter(JsonFrameworkWriter writer)
    {
        writer.WriteEndArray();
        string json = writer.ToString();
        writer.Dispose();
        return json;
    }
}