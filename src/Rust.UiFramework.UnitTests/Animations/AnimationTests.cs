using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Animations;

public class AnimationTests : BaseAnimationTests
{
    [Fact]
    public Task Animation_Duration_Lerp_ProducesCorrectJson()
    {
        // Arrange
        AnimationRef<IElementAnimation<UiPanel>> animation = CreateElementAnimation<UiPanel>("name");
        JsonFrameworkWriter writer = StartWriter();
        
        // Act
        animation.AnimateColor().Duration(5f).Lerp(UiColors.Red, UiColors.Blue);
        RunSendableAnimation(animation, onTick: (a, _) => a.Animation.Serialize(writer));
        
        // Assert
        animation.IsValid.Should().BeFalse();
        
        string json = FinishWriter(writer);
        return VerifyJson(json);
    }
    
    [Fact]
    public Task Animation_Delay_Duration_Lerp_ProducesCorrectJson()
    {
        // Arrange
        AnimationRef<IElementAnimation<UiPanel>> animation = CreateElementAnimation<UiPanel>("name");
        JsonFrameworkWriter writer = StartWriter();
        
        // Act
        animation.AnimateColor().Delay(2f).Duration(5f).Lerp(UiColors.Red, UiColors.Blue);
        RunSendableAnimation(animation, onTick: (a, _) => a.Animation.Serialize(writer));
        
        // Assert
        animation.IsValid.Should().BeFalse();
        
        string json = FinishWriter(writer);
        return VerifyJson(json);
    }
    
    [Fact]
    public void Animation_WithTimeoutGreaterThanDelay_TimesOutTheAnimations()
    {
        // Arrange
        AnimationRef<IElementAnimation<UiPanel>> animation = CreateElementAnimation<UiPanel>("name");
        AnimationRef<IFieldAnimation<UiColor>> color = animation.AnimateColor();
        
        // Act
        bool timeoutCalled = false;
        color.Timeout(3f).Delay(5f).Duration(5f).Lerp(UiColors.Red, UiColors.Blue);
        color.OnTimeout(a => timeoutCalled = true);
        RunSendableAnimation(animation, onTick: (a, tickIndex) =>
        {
            if (tickIndex == 3)
            {
                color.IsValid.Should().BeFalse();
                a.IsValid.Should().BeTrue();
                a.Animation.State.Should().Be(AnimationState.Completed);
            }
        });
        
        // Assert
        timeoutCalled.Should().BeTrue();
        animation.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public Task Animation_WithTimeoutLessThanDelay_ProducesCorrectJson()
    {
        // Arrange
        AnimationRef<IElementAnimation<UiPanel>> animation = CreateElementAnimation<UiPanel>("name");
        AnimationRef<IFieldAnimation<UiColor>> color = animation.AnimateColor();
        JsonFrameworkWriter writer = StartWriter();
        
        // Act
        color.Timeout(5f).Delay(3f).Duration(5f).Lerp(UiColors.Red, UiColors.Blue);
        RunSendableAnimation(animation, onTick: (a, _) => a.Animation.Serialize(writer));
        
        // Assert
        animation.IsValid.Should().BeFalse();
        
        string json = FinishWriter(writer);
        return VerifyJson(json);
    }
    
    [Fact]
    public void Animation_WithTimeoutOnParentGreaterThanDelay_TimesOutTheAnimations()
    {
        // Arrange
        AnimationRef<IElementAnimation<UiPanel>> animation = CreateElementAnimation<UiPanel>("name").Timeout(3f).Delay(5f);
        AnimationRef<IFieldAnimation<UiColor>> color = animation.AnimateColor();
        
        // Act
        bool timeoutCalled = false;
        color.Duration(5f).Lerp(UiColors.Red, UiColors.Blue);
        color.OnTimeout(a => timeoutCalled = true);
        RunSendableAnimation(animation, onTick: (a, tickIndex) =>
        {
            if (tickIndex == 3)
            {
                color.IsValid.Should().BeTrue();
                color.Animation.State.Should().Be(AnimationState.Timeout);
                a.IsValid.Should().BeTrue();
                a.Animation.State.Should().Be(AnimationState.Timeout);
            }
        });
        
        // Assert
        timeoutCalled.Should().BeTrue();
        animation.IsValid.Should().BeFalse();
    }
}