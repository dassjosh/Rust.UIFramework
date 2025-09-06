using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Builder;

[CollectionDefinition(nameof(UiBuilderTests), DisableParallelization = true)]
public class UiBuilderTests
{
    [Fact]
    public void UiBuilder_AddUi_DoesNotLeak()
    {
        //Arrange
        UiPluginPool pool = UnitTestHelpers.CreatePoolForTest($"{nameof(UiBuilderTests)}_{nameof(UiBuilder_AddUi_DoesNotLeak)}");
        
        //Act
        for (int i = 0; i < 1000; i++)
        {
            UiBuilder builder = UiBuilder.Create(pool).SetRoot(new UiReference(UiLayer.Overlay, "UI"), out UiSection _);
            for (int j = 0; j < 1000; j++)
            {
                builder.Panel(builder.Root, UiPosition.Full, default, UiColors.Clear);
            }

            switch (i % 4)
            {
                case 0:
                    builder.AddUi(new SendInfo());
                    break;
                case 1:
                    builder.AddUi((Connection)null);
                    break;
                case 2:
                    builder.AddUi([]);
                    break;
                case 3:
                    builder.TryDispose();
                    break;
            }
        }
        
        Singleton<SendHandler>.Instance.WaitUntilFinished();
        
        //Assert
        pool.CheckForLeaks().Should().BeFalse();
    }
}