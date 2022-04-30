# Rust UI Framework
UI Framework for Rust (The Game) using the [Oxide/uMod](https://umod.org) plugin platforms

## Performance Comparison vs Oxide

This benchmark shows the performance comparision when generating 100 UI elements and then serializing them to JSON.
In comparision to oxide when performing the serialization UiFramework is ~4.34x faster at generating the UI elements and JSON.
Along with the performance improvements the Memory allocated on the heap is ~2.5x smaller than what oxide would allocate.


| Method                         |      Mean |     Error |    StdDev | Ratio |   Gen 0 |  Gen 1 | Allocated |
|--------------------------------|----------:|----------:|----------:|------:|--------:|-------:|----------:|
| OxideBenchmark_WithoutJson     |  76.44 us |  4.580 us |  6.855 us |  0.11 |  8.0000 | 0.8000 |     33 KB |
| FrameworkBenchmark_WithoutJson |  44.13 us |  1.252 us |  1.835 us |  0.06 |  3.2000 |      - |     14 KB |
| OxideBenchmark_WithJson        | 711.20 us | 10.117 us | 15.143 us |  1.00 | 32.0000 |      - |    133 KB |
| FrameworkBenchmark_WithJson    | 163.52 us |  1.845 us |  2.704 us |  0.23 | 12.8000 |      - |     53 KB |


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
