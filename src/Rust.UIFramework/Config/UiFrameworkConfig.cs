using System;
using System.IO;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Ext.UiFramework.Constants;

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
    /// Constructor for discord config
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
            Interface.Oxide.LogException($"[UiFramework Extension] Failed to load config file. Using default config. {ex}", ex);
            ApplyDefaults();
        }
    }

    private void ApplyDefaults()
    {
        Harmony = new UiHarmonyConfig
        {
            PatchAddUiMethod = Harmony?.PatchAddUiMethod ?? false
        };
    }
}