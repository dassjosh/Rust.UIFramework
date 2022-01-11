# Rust UI Framework
UI Framework for Rust (The Game) using the [Oxide/uMod](https://umod.org) plugin platforms

## Getting Started
If you wish to use this UI Framework in your plugin  
1. Grab the ./build/UiFramework.cs and copy the class under your main plugin class.
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
