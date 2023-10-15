# Rust UI Framework
UI Framework for Rust (The Game) using the [Oxide/uMod](https://umod.org) plugin platforms

## Performance Comparison vs Oxide

This benchmark shows the performance comparision when generating 100 UI elements and then serializing them to JSON.
In comparision to oxide when performing the serialization UiFramework is ~9.40x faster at generating the UI elements and JSON.
Along with the performance improvements there are 0 memory allocations.

|                    Method |       Mean |     Error |    StdDev | Ratio |   Gen 0 |  Gen 1 | Allocated |
|-------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|
|     Oxide_CreateContainer |  49.927 us | 0.9620 us | 1.1451 us | 0.081 | 12.3291 |      - |  51,953 B |
| Framework_CreateContainer |   9.392 us | 0.0491 us | 0.0460 us | 0.015 |       - |      - |         - |
|          Oxide_CreateJson | 548.430 us | 3.3054 us | 2.9301 us | 0.881 | 26.3672 | 0.9766 | 114,584 B |
|      Framework_CreateJson |  51.130 us | 0.2379 us | 0.2109 us | 0.082 |       - |      - |         - |
|          Oxide_EncodeJson |  20.913 us | 0.2681 us | 0.2507 us | 0.034 |  4.7607 |      - |  19,984 B |
|      Framework_EncodeJson |   1.889 us | 0.0025 us | 0.0024 us | 0.003 |       - |      - |         - |
|                Oxide_Full | 622.410 us | 4.9448 us | 4.3835 us | 1.000 | 43.9453 |      - | 186,398 B |
|            Framework_Full |  66.159 us | 0.7597 us | 0.6734 us | 0.106 |       - |      - |         - |



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
1. Grab the Oxide.Ext.UiFramework.dll from latest release
2. Put the DLL into `RustDedicated_Data\Managed` folder
3. Restart the server
4. Add the DLL to your project repo
