using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
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
        UnitTestAnimationTime time = new();
        JsonFrameworkWriter writer = StartWriter();
        
        // Act
        animation.AnimateColor().Duration(5f).Lerp(UiColors.Red, UiColors.Blue).WithTime(time);
        RunSendableAnimation(animation, writer, time);
        
        // Assert
        animation.IsValid.Should().BeFalse();
        
        string json = FinishWriter(writer);
        return VerifyJson(json);
    }
}