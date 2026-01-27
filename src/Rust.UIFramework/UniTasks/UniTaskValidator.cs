using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.UniTasks;

internal static class UniTaskValidator
{
    private const string ExtensionDownloadUrl = "https://github.com/dassjosh/Oxide.Ext.UniTask/releases/latest/download/Oxide.Ext.UniTask.dll";

    public static void ValidateUniTask()
    {
        if (!IsUniTaskInstalled())
        {
            UiFrameworkExtension.GlobalLogger.Info("UniTask extension is not installed. Attempting to download and load...");
            DownloadUniTaskExtension();
            LoadUniTaskExtension();
        }
    }
    
    private static bool IsUniTaskInstalled() => File.Exists(Path.Combine(OxideLibrary.ExtensionFolder, "UniTask.dll")) || File.Exists(Path.Combine(OxideLibrary.ExtensionFolder, "Oxide.Ext.UniTask.dll"));

    private static void DownloadUniTaskExtension()
    {
        try
        {
            using HttpClient client = new();
            byte[] result = client.GetByteArrayAsync(ExtensionDownloadUrl).Result;
            File.WriteAllBytes(Path.Combine(OxideLibrary.ExtensionFolder, "Oxide.Ext.UniTask.dll"), result);
        }
        catch (Exception ex)
        {
            UiFrameworkExtension.GlobalLogger.Info("An error occured downloading required dependency Oxide.Ext.UniTask", ex);
            throw;
        }
    }
    
    private static void LoadUniTaskExtension()
    {
        if (GetUniTaskExtension() == null)
        {
            Interface.Oxide.LoadExtension("Oxide.Ext.UniTask");
            GetUniTaskExtension()?.OnModLoad(); //Need to call OnModLoad here since Oxide won't when we load extensions this way
        }
    }

    private static Extension GetUniTaskExtension() => Interface.Oxide.GetAllExtensions().FirstOrDefault(e => e.Name == "UniTask");
}