using Argon;
using Oxide.Ext.UiFramework;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.UnitTests.Global;
using Rust.UiFramework.UnitTests.Global.Verify.IgnoreMembers;
using Rust.UiFramework.UnitTests.Global.XUnit.Serializers;
using Rust.UiFramework.UnitTests.Mocks.Libraries;
using Rust.UiFramework.UnitTests.Mocks.Libraries.ImageDb;
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
        VerifierSettings.SortJsonObjects();
        UseSourceFileRelativeDirectory("Snapshots");
        AddVerifyConverters();
        AddVerifyIgnores();
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
    
    private static void AddVerifyIgnores()
    {
        IVerifyIgnoreMembers[] ignores = typeof(AssemblyFixture).Assembly.DefinedTypes
            .Where(t => t.IsAssignableTo(typeof(IVerifyIgnoreMembers)) && !t.IsInterface)
            .Select(t => (IVerifyIgnoreMembers)Activator.CreateInstance(t))
            .ToArray();
        foreach (IVerifyIgnoreMembers ignore in ignores)
        {
            ignore.Register();
        }
    }

    private static void AddVerifyParameterConverters()
    {
        VerifierSettings.NameForParameter<GridPosition>(pos => GridPositionSerializer.Instance.Serialize(pos));
        VerifierSettings.NameForParameter<UiPosition>(pos => UiPositionSerializer.Instance.Serialize(pos));
    }

    private static void ConfigureExtension()
    {
        UiFrameworkExtension ext = new(null);
        if(UiFrameworkConfig.Instance == null) Singleton<DataHandler>.Instance.LoadAll();
        UiFrameworkExtension.GlobalLogger = Singleton<UiLoggerFactory>.Instance.CreateGlobalLogger();
        Singleton<DataHandler>.Instance.LoadAll();
        OxideLibrary.RegisterLibrary(nameof(IImageDatabase), new ImageDatabaseMock());
        OxideLibrary.RegisterLibrary(nameof(UiImageStorage), Singleton<UiImageStorage>.Instance);
        //new UiFrameworkPlugin().Init();
        BaseUiFrameworkLibrary.ProcessOnCommunityEntitySpawned(new CommunityEntityMock());
        // var plugin = new UiFrameworkPlugin();
        // plugin.Init();
        // plugin.OnServerInitialized();
        AvatarData.Instance.AddAvatar(UnitTestsConstants.AvatarSteamId, "Test Avatar");
    }

    public override async ValueTask DisposeAsync()
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        if (Singleton<UiPool>.Instance.CheckForLeaks())
        {
            DebugLogger logger = new();
            Singleton<UiPool>.Instance.LogDebug(logger);
            Console.WriteLine(logger.ToString());
        }
        
        await base.DisposeAsync();
    }
}