using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.UiElements;

[UiFrameworkSerializer(typeof(UiComponentSerializer<UiSection>))]
public class UiSection() : BaseUiComponent(new EmptyComponent());