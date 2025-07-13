using Argon;
using Oxide.Ext.UiFramework;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.UnitTests.Global;
using Rust.UiFramework.UnitTests.Global.XUnit.Serializers;
using Xunit.Sdk;
using Xunit.v3;

[assembly: TestFramework(typeof(AssemblyFixture))]

namespace Rust.UiFramework.UnitTests.Global;

public class AssemblyFixture : XunitTestFramework
{
    public AssemblyFixture()
    {
        ConfigureXUnit();
        ConfigureVerify();
        ConfigureExtension();
    }

    private static void ConfigureXUnit()
    {
        SerializationHelper.Instance.AddRegisteredSerializers(typeof(AssemblyFixture).Assembly);
    }

    private static void ConfigureVerify()
    {
        //VerifierSettings.AutoVerify();
        VerifierSettings.UseStrictJson();
        VerifierSettings.DontIgnoreEmptyCollections();
        VerifierSettings.SortPropertiesAlphabetically();
        UseSourceFileRelativeDirectory("Snapshots");
        AddVerifyConverters();
        AddVerifyParameterConverters();
    }
    
    private static void AddVerifyConverters()
    {
        JsonConverter[] converters = typeof(AssemblyFixture).Assembly.DefinedTypes
            .Where(t => t.IsAssignableTo(typeof(JsonConverter)))
            .Select(t => (JsonConverter)Activator.CreateInstance(t))
            .ToArray();
        VerifierSettings.AddExtraSettings(settings => settings.Converters.AddRange(converters));
    }

    private static void AddVerifyParameterConverters()
    {
        VerifierSettings.NameForParameter<GridPosition>(pos => GridPositionSerializer.Instance.Serialize(pos));
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
        Singleton<UiPool>.Instance.CheckForLeaks();
        await base.DisposeAsync();
    }
}