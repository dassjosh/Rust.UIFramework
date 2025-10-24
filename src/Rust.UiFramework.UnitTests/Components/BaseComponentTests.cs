using System.Reflection;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Components;

public abstract class BaseComponentTests<T> where T : BaseComponent, new()
{
    [Fact]
    public void Component_FromPool_IsNotNull()
    {
        // Arrange
        using T component = UiPool.Internal.Get<T>();
        
        // Act
        
        // Assert
        component.Should().NotBeNull();
    }
    
    [Fact]
    public Task Component_FromPool_HasDefaultValues()
    {
        // Arrange
        using T component = UiPool.Internal.Get<T>();
        
        // Act
        
        // Assert
        return Verify(component);
    }
}

public abstract class BasePopulateComponentTests<T>(Action<T> populateComponent) : BaseComponentTests<T> where T : BaseComponent, new()
{
    protected readonly Action<T> PopulateComponent = populateComponent;
    protected virtual bool VerifyAsJson => true;

    [Fact]
    public Task Component_AllValues_MatchExpected()
    {
        // Arrange
        using T component = UiPool.Internal.Get<T>();
        
        // Act
        PopulateComponent(component);
        
        // Assert
        return Verify(component);
    }
    
    [Fact]
    public Task Component_AllValues_GeneratesCorrectJson()
    {
        // Arrange
        using T component = UiPool.Internal.Get<T>();
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.Plugin);
        
        // Act
        PopulateComponent(component);
        component.WriteComponent(writer, SerializeMode.Create);
        
        // Assert
        string json = writer.ToString();
        return VerifyAsJson ? VerifyJson(json) : Verify(json.Replace(",", ",\r\n").Replace(":", ": "));
    }
    
    [Fact]
    public Task Component_AllValues_ResetToCorrectDefaults()
    {
        // Arrange
        using T component = UiPool.Internal.Get<T>();
        
        // Act
        PopulateComponent(component);
        UnitTestHelpers.EnterPool(component);
        
        // Assert
        return Verify(component).IgnoreParametersForVerified();
    }

    [Fact]
    public Task Component_Update_GeneratesCorrectJson()
    {
        // Arrange
        using T component = UiPool.Internal.Get<T>();
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.Plugin);
        
        // Act
        PopulateComponent(component);
        component.WriteComponent(writer, SerializeMode.Update);
        
        // Assert
        string json = writer.ToString();
        return VerifyAsJson ? VerifyJson(json) : Verify(json.Replace(",", ",\r\n").Replace(":", ": "));
    }
    
    [Fact]
    public void Component_TrackedValues_HaveExpectedValues()
    {
        // Arrange
        using T component = UiPool.Internal.Get<T>();
        Assert.SkipWhen(component is NeedsKeyboardComponent or NeedsMouseComponent, $"Skipping TrackedValues_HaveExpectedValues for {typeof(T).Name}");
        
        // Act
        var values = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .Where(f => f.FieldType.IsAssignableTo(typeof(ITracked)))
            .Select(f => new {Field = f, Tracked = (ITracked)f.GetValue(component)})
            .ToArray();
        
        // Assert
        values.Should().AllSatisfy(v => v.Tracked.IsDefaultValue.Should().BeTrue($"Is Default: {v.Field.Name}"));
        values.Should().AllSatisfy(v => v.Tracked.HasChanged.Should().BeFalse($"Has Not Changed Before Populate: {v.Field.Name}"));
        component.HasChanged().Should().BeFalse("Component Has Not Changed Before Populate");
        PopulateComponent(component);
        values.Should().AllSatisfy(v => v.Tracked.HasChanged.Should().BeTrue($"Has Changed After Populate: {v.Field.Name}"));
        component.HasChanged().Should().BeTrue("Component Has Changed After Populate");
        component.ResetHasChanged();
        values.Should().AllSatisfy(v => v.Tracked.HasChanged.Should().BeFalse($"Has Not Changed After ResetHasChanged: {v.Field.Name}"));
        component.HasChanged().Should().BeFalse("Component Has Not Changed After ResetHasChanged");
        PopulateComponent(component);
        component.Reset();
        values.Should().AllSatisfy(v => v.Tracked.IsDefaultValue.Should().BeTrue($"Is Default After Reset: {v.Field.Name}"));
        values.Should().AllSatisfy(v => v.Tracked.HasChanged.Should().BeFalse($"Has Not Changed After Reset: {v.Field.Name}"));
        component.HasChanged().Should().BeFalse("Component Has Not Changed After Reset");
    }
}

#pragma warning disable xUnit1015
public abstract class BaseTheoryComponentTests<TComponent, TTheoryRow>(Action<TComponent> populateComponent) : BasePopulateComponentTests<TComponent>(populateComponent) where TComponent : BaseComponent, new()
{
    [Theory]
    [MemberData("TheoryData")]
    public Task Component_Theory_AllValues_MatchExpected(TTheoryRow row)
    {
        // Arrange
        using TComponent component = UiPool.Internal.Get<TComponent>();
        
        // Act
        PopulateComponent(component);
        PopulateTheory(component, row);
        
        // Assert
        return Verify(component);
    }
    
    [Theory]
    [MemberData("TheoryData")]
    public Task Component_Theory_AllValues_GeneratesCorrectJson(TTheoryRow row)
    {
        // Arrange
        using TComponent component = UiPool.Internal.Get<TComponent>();
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.Plugin);
        
        // Act
        PopulateComponent(component);
        PopulateTheory(component, row);
        component.WriteComponent(writer, SerializeMode.Create);
        
        // Assert
        string json = writer.ToString();
        return VerifyJson(json);
    }
    
    protected abstract void PopulateTheory(TComponent component, TTheoryRow row);
}
#pragma warning restore xUnit1015