# Rust UI Framework
UI Framework for Rust (The Game) using the [Oxide/uMod](https://umod.org) plugin platforms

## Performance Comparison vs Oxide

This benchmark shows the performance comparision when generating 100 UI elements and then serializing them to JSON.
In comparision to oxide when performing the serialization UiFramework is ~8.45x faster at generating the UI elements and JSON.
Along with the performance improvements the Memory allocated on the heap is ~3.01x smaller than what oxide would allocate.


|                         Method |      Mean |     Error |    StdDev | Ratio |   Gen 0 |  Gen 1 | Allocated |
|------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|
|     OxideBenchmark_WithoutJson |  48.02 us |  0.541 us |  0.480 us |  0.08 | 12.3291 |      - |     51 KB |
| FrameworkBenchmark_WithoutJson |  22.74 us |  0.367 us |  0.464 us |  0.04 |  3.8147 |      - |     16 KB |
|        OxideBenchmark_WithJson | 601.09 us | 11.881 us | 15.449 us |  1.00 | 39.0625 | 9.7656 |    162 KB |
|    FrameworkBenchmark_WithJson |  71.06 us |  1.231 us |  1.092 us |  0.12 | 13.0615 |      - |     54 KB |




## Getting Started

### Plugin
If you wish to use this UI Framework in your plugin  
1. Grab the `UiFramework.cs` and copy the class under your main plugin class.
2. Rename the UiFramework class to match the same as the plugin
3. Remove the ` : RustPlugin` from the copied UiFramework class
4. Should look something like the below

```c#
namespace Oxide.Plugins
{
    [Info("My Main Plugin", "MJSU", "1.0.0")]
    [Description("My Plugin Description")]
    public partial class MyPlugin : RustPlugin
    {
        ///Code from your plugin
    }

    //[Info("Rust UI Framework", "MJSU", "1.1.0")]
    //[Description("UI Framework for Rust")]
    public partial class MyPlugin
    {
        //Code from UIFramework.cs
    }
 }
```

### Extension
If you wish to use the included extension instead of including the framework files in the plugin.
1. Grab the Oxide.Ext.UiFramework.dll
2. Put the DLL `rust_dedicated\RustDedicated_Data\Managed` folder
3. Restart the server
4. Add the DLL to your project repo
