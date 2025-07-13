using Oxide.Ext.UiFramework.Components;
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
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);
        
        // Act
        PopulateComponent(component);
        component.WriteComponent(writer);
        
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
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);
        
        // Act
        PopulateComponent(component);
        PopulateTheory(component, row);
        component.WriteComponent(writer);
        
        // Assert
        string json = writer.ToString();
        return VerifyJson(json);
    }
    
    protected abstract void PopulateTheory(TComponent component, TTheoryRow row);
}
#pragma warning restore xUnit1015