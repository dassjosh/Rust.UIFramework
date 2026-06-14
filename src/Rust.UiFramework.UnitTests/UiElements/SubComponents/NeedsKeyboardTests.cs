using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;

namespace Rust.UiFramework.UnitTests.UiElements.SubComponents;

public class NeedsKeyboardTests : BaseUiElementsTests<UiSection>
{
    public record TheoryRow(UpdateMode Mode, bool Enabled);

    [Theory]
    [MemberData(nameof(TheoryData))]
    public async Task NeedsKeyboard_Sends_Enabled(TheoryRow row)
    {
        //Arrange
        using UiSection element = GetElementWithValues();
        element.SetUpdate(row.Mode);
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.Plugin);

        //Act
        element.NeedsKeyboard(row.Enabled);
        element.WriteElement(writer);

        //Assert
        string json = writer.ToString();
        await VerifyJson(json);
    }

    public static TheoryData<TheoryRow> TheoryData =>
        TheoryDataGenerator.Generate<TheoryRow, UpdateMode, bool>((updateMode, enabled) => new TheoryRow(updateMode, enabled));
}