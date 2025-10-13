using System.IO;
using System.Text;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Builder;

internal static class UiDebugHandler
{
    public static void HandleDebug(IUiFrameworkPlugin plugin, JsonFrameworkWriter writer, in UiDebugOptions options)
    {
        WriteToStream(plugin, writer, options);
        WriteToConsole(writer.ToString(), options);
    }
    
    public static void HandleDebug(IUiFrameworkPlugin plugin, byte[] bytes, in UiDebugOptions options)
    {
        WriteToStream(plugin, bytes, options);
        WriteToConsole(Encoding.UTF8.GetString(bytes), options);
    }

    public static void WriteToStream(IUiFrameworkPlugin plugin, JsonFrameworkWriter writer, in UiDebugOptions options)
    {
        if (options.Mode.HasFlag(UiDebugModes.File))
        {
            using FileStream stream = GetDebugStream(plugin, in options);
            writer.WriteToStream(stream);
            stream.Flush(true);
        }
    }
    
    public static void WriteToStream(IUiFrameworkPlugin plugin, byte[] bytes, in UiDebugOptions options)
    {
        if (options.Mode.HasFlag(UiDebugModes.File))
        {
            using FileStream stream = GetDebugStream(plugin, in options);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush(true);
        }
    }
    
    private static FileStream GetDebugStream(IUiFrameworkPlugin plugin, in UiDebugOptions options)
    {
        string path = Path.Join(OxideLibrary.LogFolder, UiFrameworkExtension.Instance.Name, plugin.Name);
        Directory.CreateDirectory(path);
        return new FileStream(Path.Join(path, $"{options.Identifier}.txt"), options.Mode.HasFlag(UiDebugModes.AppendFile) ? FileMode.Append : FileMode.Create, FileAccess.Write);
    }

    public static void WriteToConsole(string json, in UiDebugOptions options)
    {
        if (options.Mode.HasFlag(UiDebugModes.Console))
        {
            UiFrameworkExtension.GlobalLogger.Debug($"{options.Identifier}:\n{new string('=', 20)}\n{json}\n{new string('=', 20)}");
        }
    }
}