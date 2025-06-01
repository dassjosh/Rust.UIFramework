using Argon;
using Oxide.Ext.UiFramework;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.UnitTests.Global;
using Xunit.v3;

[assembly: TestFramework(typeof(AssemblyFixture))]

namespace Rust.UiFramework.UnitTests.Global;

public class AssemblyFixture : XunitTestFramework
{
    public AssemblyFixture()
    {
        ConfigureVerify();
        ConfigureExtension();
    }

    private static void ConfigureVerify()
    {
        //VerifierSettings.AutoVerify();
        VerifierSettings.UseStrictJson();
        VerifierSettings.DontIgnoreEmptyCollections();
        VerifierSettings.SortPropertiesAlphabetically();
        UseSourceFileRelativeDirectory("Snapshots");
        AddVerifyConverters();
    }
    
    private static void AddVerifyConverters()
    {
        JsonConverter[] converters = typeof(AssemblyFixture).Assembly.DefinedTypes
            .Where(t => t.IsAssignableTo(typeof(JsonConverter)))
            .Select(t => (JsonConverter)Activator.CreateInstance(t))
            .ToArray();
        VerifierSettings.AddExtraSettings(settings => settings.Converters.AddRange(converters));
    }

    private static void ConfigureExtension()
    {
        UiFrameworkExtension ext = new UiFrameworkExtension(null);
        if(UiFrameworkConfig.Instance == null) UiFrameworkConfig.LoadConfig();
        UiFrameworkExtension.GlobalLogger = Singleton<UiLoggerFactory>.Instance.CreateGlobalLogger();
        Singleton<DataHandler>.Instance.LoadAll();
        // var plugin = new UiFrameworkPlugin();
        // plugin.Init();
        // plugin.OnServerInitialized();
        AvatarData.Instance.AddAvatar(UnitTestsConstants.AvatarSteamId, "Test Avatar");
    }

    public override async ValueTask DisposeAsync()
    {
        Singleton<UiFrameworkPoolLib>.Instance.CheckForLeaks();
        await base.DisposeAsync();
    }
}