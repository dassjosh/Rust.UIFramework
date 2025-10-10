using System;
using System.IO;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Config;

/// <summary>
/// Represents Ui Framework Extension Config
/// </summary>
internal class UiFrameworkConfig : ConfigFile
{
    internal static UiFrameworkConfig Instance;
    
    /// <summary>
    /// UiFramework Image Storage Options
    /// </summary>
    [JsonProperty("Font")]
    public UiFontConfig Font { get; set; }
    
    /// <summary>
    /// UiFramework Image Storage Options
    /// </summary>
    [JsonProperty("ImageStorage")]
    public UiImageStorageConfig ImageStorage { get; set; }
    
    /// <summary>
    /// UiFramework Image DB Options
    /// </summary>
    [JsonProperty("ImageDB")]
    public UiImageDatabaseConfig ImageDb { get; set; }
    
    /// <summary>
    /// UiFramework Image Storage Options
    /// </summary>
    [JsonProperty("Animations")]
    public UiAnimationConfig Animations { get; set; }
    
    /// <summary>
    /// UiFramework Image Storage Options
    /// </summary>
    [JsonProperty("Steam")]
    public UiSteamConfig Steam { get; set; }
            
    /// <summary>
    /// UiFramework Harmony Options
    /// </summary>
    [JsonProperty("Harmony")]
    public UiHarmonyConfig Harmony { get; set; }
    
    /// <summary>
    /// UiFramework Proxy Options
    /// </summary>
    [JsonProperty("Proxy")]
    public UiProxyConfig Proxy { get; set; }
    
    /// <summary>
    /// UiFramework Logging Options
    /// </summary>
    [JsonProperty("Logging")]
    public UiLoggingConfig Logging { get; set; }

    /// <summary>
    /// Constructor for Ui Framework Config
    /// </summary>
    /// <param name="filename">Filename to use</param>
    // Has to be public
    public UiFrameworkConfig(string filename) : base(filename)
    {
        if (Instance != null)
        {
            throw new Exception("Duplicate UiFrameworkConfig Instances");
        }
            
        Instance = this;
        ApplyDefaults();
    }

    internal static void LoadConfig()
    {
#if !SERVER
        new UiFrameworkConfig(null);
#else
        string configPath = Path.Combine(PathConstants.ConfigFolder, "UiFramework.json");
        UiFrameworkConfig config = File.Exists(configPath) ? Load<UiFrameworkConfig>(configPath) : new UiFrameworkConfig(configPath);
        config.Save();
#endif

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
        Font = new UiFontConfig
        {
            DefaultFont = Font?.DefaultFont ?? UiFontCache.GetUiFont(UiFont.RobotoCondensedRegular)
        };
        
        Harmony = new UiHarmonyConfig
        {
            PatchAddUiMethod = Harmony?.PatchAddUiMethod ?? false
        };

        ImageStorage = new UiImageStorageConfig
        {
            MaxConcurrentDownloads = ImageStorage?.MaxConcurrentDownloads ?? 5,
            MaxDownloadAttempts = ImageStorage?.MaxDownloadAttempts ?? 3,
        };

        ImageDb = new UiImageDatabaseConfig
        {
            Enabled = ImageDb?.Enabled ?? true,
            CacheSize = new MemorySize(25 * 1024 * 1024),
            UnusedImageMaxDays = 30
        };
        
        Steam = new UiSteamConfig
        {
            ApiKey = Steam?.ApiKey ?? string.Empty
        };

        Animations = new UiAnimationConfig
        {
            Enabled = Animations?.Enabled ?? true,
            UpdateRate = Animations?.UpdateRate ?? 25
        };

        Proxy = new UiProxyConfig(Proxy);
        
        Logging = new UiLoggingConfig
        {
            ConsoleLogLevel = Logging?.ConsoleLogLevel ?? UiLogLevel.Info,
            FileLogLevel = Logging?.FileLogLevel ?? UiLogLevel.Off,
            FileDateTimeFormat = "HH:mm:ss.ff"
        };
    }
}