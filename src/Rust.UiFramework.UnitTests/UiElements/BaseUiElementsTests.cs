using System.Reflection;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.UiElements;

public abstract class BaseUiElementsTests<T> where T : BaseUiComponent, new()
{
    [Fact]
    public void Element_FromPool_IsNotNull()
    {
        // Arrange
        using T element = GetElement();
        
        // Act
        
        // Assert
        element.Should().NotBeNull();
    }
    
    [Fact]
    public Task Element_FromPool_HasDefaultValues()
    {
        // Arrange
        using T element = GetElement();
        
        // Act
        
        // Assert
        return ConfigureVerify(Verify(element), null);
    }

    protected static T GetElement()
    {
        T element = UiPool.Internal.Get<T>();
        return element;
    }
    
    protected static T GetElementWithValues()
    {
        T element = GetElement();
        element.SetPosition(new UiPosition(0.1f, 0.2f, 0.3f, 0.4f), new UiOffset(100, 200, 300, 400));
        element.Reference = new UiReference("parent", "element");
        return element;
    }
    
    protected static SettingsTask ConfigureVerify(SettingsTask settings, MethodInfo populateMethod, [CallerMemberName] string methodName = null)
    {
        settings.UseMethodName($"{methodName}{(populateMethod != null ? $"_{populateMethod.Name}" : null)}");
        //settings.IgnoreMember(nameof(BaseUiComponent.Component));
        return settings;
    }
}

public abstract class BasePopulateUiElementsTests<T>(params Action<T>[] populateElement) : BaseUiElementsTests<T> where T : BaseUiComponent, new()
{
    protected readonly Action<T>[] PopulateElement = populateElement;
    protected virtual bool VerifyAsJson => true;

    protected abstract void AssertValues(T element);

    [Fact]
    public async Task Element_AllValues_MatchExpected()
    {
        foreach (Action<T> populate in PopulateElement)
        {
            // Arrange
            using T element = GetElementWithValues();

            // Act
            populate(element);

            // Assert
            AssertValues(element);
            await ConfigureVerify(Verify(element), populate.Method);
        }
    }

    [Fact]
    public async Task Element_AllValues_GeneratesCorrectJson()
    {
        foreach (Action<T> populate in PopulateElement)
        {
            // Arrange
            using T element = GetElementWithValues();
            using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);

            // Act
            populate(element);
            element.WriteComponent(writer);

            // Assert
            string json = writer.ToString();
            await ConfigureVerify(VerifyAsJson ? VerifyJson(json) : Verify(json.Replace(",", ",\r\n").Replace(":", ": ")), populate.Method);
        }
    }

    [Fact]
    public async Task Element_AllValues_ResetToCorrectDefaults()
    {
        foreach (Action<T> populate in PopulateElement)
        {
            // Arrange
            using T element = GetElementWithValues();

            // Act
            populate(element);
            UnitTestHelpers.EnterPool(element);

            // Assert
            await ConfigureVerify(Verify(element), populate.Method);
        }
    }
}

#pragma warning disable xUnit1015
public abstract class BaseTheoryUiElementsTests<TElement, TTheoryRow>(params Action<TElement, TTheoryRow>[] populateElement) : BaseUiElementsTests<TElement> where TElement : BaseUiComponent, new()
{
    protected abstract void AssertValues(TElement element, TTheoryRow row);
    
    [Theory]
    [MemberData("TheoryData")]
    public void Element_Theory_MatchExpected(TTheoryRow row)
    {
        foreach (Action<TElement, TTheoryRow> populate in populateElement)
        {
            // Arrange
            using TElement element = GetElementWithValues();

            // Act
            populate(element, row);

            // Assert
            AssertValues(element, row);
        }
    }
    
    [Theory]
    [MemberData("TheoryData")]
    public async Task Element_Theory_GeneratesCorrectJson(TTheoryRow row)
    {
        foreach (Action<TElement, TTheoryRow> populate in populateElement)
        {
            // Arrange
            using TElement element = GetElementWithValues();
            using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);

            // Act
            populate(element, row);
            element.WriteComponent(writer);

            // Assert
            string json = writer.ToString();
            await ConfigureVerify(VerifyJson(json), populate.Method);
        }
    }
    
    [Theory]
    [MemberData("TheoryData")]
    public async Task Element_Theory_ResetToCorrectDefaults(TTheoryRow row)
    {
        foreach (Action<TElement, TTheoryRow> populate in populateElement)
        {
            // Arrange
            using TElement element = GetElement();

            // Act
            populate(element, row);
            UnitTestHelpers.EnterPool(element);

            // Assert
            await ConfigureVerify(Verify(element), populate.Method).IgnoreParametersForVerified();
        }
    }
}
#pragma warning restore xUnit1015