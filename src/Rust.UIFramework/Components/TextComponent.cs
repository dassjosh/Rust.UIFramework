﻿using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class TextComponent : BaseTextComponent
{
    private const string Type = "UnityEngine.UI.Text";

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }
}