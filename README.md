# Rust UI Framework
UI Framework for Rust (The Game) using the [Oxide/uMod](https://umod.org) plugin platform

## Performance Comparison vs Oxide

This benchmark shows the performance comparison when creating 100 UI Elements and then serializing them to JSON.
UiFramework_Async is 51.31x faster than CUI provided by oxide and 5.66x faster than Oxide AddUiAsync extension method UI Framework provides.
The async option in the framework queues the UI to be serialized and sent on a separate thread while CUI from oxide is all ran on the main thread.
UI Framework also doesn't have any memory allocations from the framework and pooling is used heavily for performance.

| Method            | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0    | Gen1   | Allocated |
|------------------ |-----------:|----------:|----------:|------:|--------:|--------:|-------:|----------:|
| UiFramework_Async |   6.329 us | 0.0143 us | 0.0214 us |  1.00 |    0.00 |       - |      - |         - |
| Oxide_Async       |  35.812 us | 0.2101 us | 0.3145 us |  5.66 |    0.05 |  2.9907 | 0.3052 |   51792 B |
| Oxide_Full        | 324.565 us | 1.8106 us | 2.4783 us | 51.31 |    0.33 | 11.7188 | 1.9531 |  204088 B |


## Getting Started

### Server Installation
1. Grab the Oxide.Ext.UiFramework.dll from the latest release
2. Put the DLL into `RustDedicated_Data\Managed` folder
3. Restart the server

### Developer Install
1. Grab the Oxide.Ext.UiFramework.dll from the latest release
2. Add the DLL to your project repo


## Plugin Examples

### Generic Example
```csharp
private const string MainUi = "UI_Name";
private const int TitleFontSize = 20;

private readonly UiPosition _titleBarPos = new(0, .95f, 1f, 1f);
private readonly UiPosition _closeButtonPos = new(0.95f, 0f, 1f, 1f);

private void CreateUi(BasePlayer player 
{
    UiBuilder builder = UiBuilder.Create(UiPosition.MiddleMiddle, new UiOffset(-420, -270, 420, 330), UiColors.Body, MainUi, UiLayer.Hud);

    builder.NeedsMouse();
    builder.NeedsKeyboard();
    
    UiLabelBackground title = builder.LabelBackground(builder.Root, _titleBarPos, Title, TitleFontSize, UiColors.Text, UiColors.BodyHeader);
    builder.TextButton(title.Background, _closeButtonPos, "<b>X</b>", TitleFontSize, UiColors.Text, UiColors.CloseButton, nameof(CloseCommand));
    
    builder.AddUi(player); //By default the builder will auto destroy the previous UI if it exists on the client
}
    
    
[ConsoleCommand(nameof(CloseCommand))]
private void CloseCommand(ConsoleSystem.Arg arg)
{
    BasePlayer player = arg.Player();
    if (player)
    {
        UiBuilder.DestroyUi(player, MainUi);
    }
}
```