using System.IO;

namespace Rust.UiFramework.SourceGenerators.Logging;
#pragma warning disable RS1035
public static class LoggingHelper
{
    private const string FileName = "output.txt";
    private static readonly object _lock = new();
    
    static LoggingHelper()
    {
        if (File.Exists(FileName))
        {
            File.WriteAllText(FileName, string.Empty); 
        }
    }

    public static void Log(string message)
    {
        // lock (_lock)
        // {
        //     File.AppendAllText(FileName, $"{message}\n");
        // }
    }
}
#pragma warning restore RS1035