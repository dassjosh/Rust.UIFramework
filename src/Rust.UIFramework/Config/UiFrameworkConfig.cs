using System;
using System.IO;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Config;

/// <summary>
/// Represents Ui Framework Extension Config
/// </summary>
internal class UiFrameworkConfig : ConfigFile
{
    internal static UiFrameworkConfig Instance;
        
    /// <summary>
    /// UiFramework Harmony Options
    /// </summary>
    [JsonProperty("Harmony")]
    public UiHarmonyConfig Harmony { get; set; }
    
    /// <summary>
    /// UiFramework Image Storage Options
    /// </summary>
    [JsonProperty("ImageStorage")]
    public UiImageStorageSettings ImageStorage { get; set; }
    
    /// <summary>
    /// UiFramework Logging Options
    /// </summary>
    [JsonProperty("Logging")]
    public UiLoggingConfig Logging { get; set; }

    /// <summary>
    /// Constructor for Ui Framework Config
    /// </summary>
    /// <param name="filename">Filename to use</param>
    public UiFrameworkConfig(string filename) : base(filename)
    {
        if (Instance != null)
        {
            throw new Exception("Duplicate UiFrameworkConfig Instances");
        }
            
        Instance = this;
        ApplyDefaults();
    }

    public static void LoadConfig()
    {
        string configPath = Path.Combine(PathConstants.ConfigFolder, "UiFramework.json");
        UiFrameworkConfig config = File.Exists(configPath) ? Load<UiFrameworkConfig>(configPath) : new UiFrameworkConfig(configPath);
        config.Save();
    }
        
    /// <summary>
    /// Load the config file and populate it.
    /// </summary>
    /// <param name="filename"></param>
    public override void Load(string filename = null)
    {
        try
        {
            base.Load(filename);
            ApplyDefaults();
        }
        catch (Exception ex)
        {
            Interface.Oxide.LogException($"[UiFramework] Failed to load config file. Using default config. {ex}", ex);
            ApplyDefaults();
        }
    }

    private void ApplyDefaults()
    {
        Harmony = new UiHarmonyConfig
        {
            PatchAddUiMethod = Harmony?.PatchAddUiMethod ?? false
        };

        ImageStorage = new UiImageStorageSettings
        {
            MaxConcurrentDownloads = ImageStorage?.MaxConcurrentDownloads ?? 5,
            MaxDownloadAttempts = ImageStorage?.MaxDownloadAttempts ?? 3,
        };
        
        Logging = new UiLoggingConfig
        {
            ConsoleLogLevel = Logging?.ConsoleLogLevel ?? UiLogLevel.Info,
            FileLogLevel = Logging?.FileLogLevel ?? UiLogLevel.Off,
            FileDateTimeFormat = "HH:mm:ss.ff"
        };
    }
}