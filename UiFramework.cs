using Network;
using Newtonsoft.Json;
using Oxide.Core.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Color = UnityEngine.Color;
using Net = Network.Net;

//UiFramework created with PluginMerge v(1.0.4.0) by MJSU @ https://github.com/dassjosh/Plugin.Merge
namespace Oxide.Plugins
{
    //[Info("Rust UI Framework", "MJSU", "1.3.0")]
    //[Description("UI Framework for Rust")]
    public partial class UiFramework : RustPlugin
    {
        #region Plugin\UiFramework.Methods.cs
        #region JSON Sending
        public void DestroyUi(BasePlayer player, string name)
        {
            UiBuilder.DestroyUi(player, name);
        }
        
        public void DestroyUi(List<Connection> connections, string name)
        {
            UiBuilder.DestroyUi(connections, name);
        }
        
        private void DestroyUiAll(string name)
        {
            UiBuilder.DestroyUi(name);
        }
        #endregion
        
        #region Unloading
        public override void HandleRemovedFromManager(PluginManager manager)
        {
            UiFrameworkPool.OnUnload();
            base.HandleRemovedFromManager(manager);
        }
        #endregion
        #endregion

        #region UiConstants.cs
        public class UiConstants
        {
            public static class UiFonts
            {
                private const string DroidSansMono = "droidsansmono.ttf";
                private const string PermanentMarker = "permanentmarker.ttf";
                private const string RobotoCondensedBold = "robotocondensed-bold.ttf";
                private const string RobotoCondensedRegular = "robotocondensed-regular.ttf";
                
                private static readonly Hash<UiFont, string> Fonts = new Hash<UiFont, string>
                {
                    [UiFont.DroidSansMono] = DroidSansMono,
                    [UiFont.PermanentMarker] = PermanentMarker,
                    [UiFont.RobotoCondensedBold] = RobotoCondensedBold,
                    [UiFont.RobotoCondensedRegular] = RobotoCondensedRegular,
                };
                
                public static string GetUiFont(UiFont font)
                {
                    return Fonts[font];
                }
            }
            
            public static class UiLayers
            {
                private const string Overall = "Overall";
                private const string Overlay = "Overlay";
                private const string Hud = "Hud";
                private const string HudMenu = "Hud.Menu";
                private const string Under = "Under";
                
                private static readonly Hash<UiLayer, string> Layers = new Hash<UiLayer, string>
                {
                    [UiLayer.Overall] = Overall,
                    [UiLayer.Overlay] = Overlay,
                    [UiLayer.Hud] = Hud,
                    [UiLayer.HudMenu] = HudMenu,
                    [UiLayer.Under] = Under,
                };
                
                public static string GetLayer(UiLayer layer)
                {
                    return Layers[layer];
                }
            }
            
            public static class RpcFunctions
            {
                public const string AddUiFunc = "AddUI";
                public const string DestroyUiFunc = "DestroyUI";
            }
            
            public static class Json
            {
                public const char QuoteChar = '\"';
            }
        }
        #endregion

        #region Builder\UiBuilder.Controls.cs
        public partial class UiBuilder
        {
            public UiButton Checkbox(BaseUiComponent parent, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd)
            {
                return TextButton(parent, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, pos, cmd);
            }
        }
        #endregion

        #region Builder\UiBuilder.cs
        public partial class UiBuilder : IDisposable
        {
            public BaseUiComponent Root;
            
            private bool _needsMouse;
            private bool _needsKeyboard;
            
            private string _cachedJson;
            private bool _disposed;
            
            private List<BaseUiComponent> _components;
            private Hash<string, BaseUiComponent> _componentLookup;
            private StringBuilder _nameBuilder;
            private string _font;
            
            private static string _globalFont;
            
            #region Constructor
            static UiBuilder()
            {
                SetGlobalFont(UiFont.RobotoCondensedRegular);
            }
            
            public UiBuilder(UiColor color, UiPosition pos, UiOffset offset, string name, string parent) : this(UiPanel.Create(pos, offset, color), name, parent) { }
            
            public UiBuilder(UiColor color, UiPosition pos, string name, string parent) : this(color, pos, null, name, parent) { }
            
            public UiBuilder(UiColor color, UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, null, name, UiConstants.UiLayers.GetLayer(parent)) { }
            
            public UiBuilder(UiColor color, UiPosition pos, UiOffset offset, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, offset, name, UiConstants.UiLayers.GetLayer(parent)) { }
            
            public UiBuilder(BaseUiComponent root, string name, string parent) : this()
            {
                SetRoot(root, name, parent);
            }
            
            public UiBuilder()
            {
                _components = UiFrameworkPool.GetList<BaseUiComponent>();
                _componentLookup = UiFrameworkPool.GetHash<string, BaseUiComponent>();
                _nameBuilder = UiFrameworkPool.GetStringBuilder();
                _font = _globalFont;
            }
            
            public void EnsureCapacity(int capacity)
            {
                if (_components.Capacity < capacity)
                {
                    _components.Capacity = capacity;
                }
            }
            
            public void SetRoot(BaseUiComponent component, string name, string parent)
            {
                Root = component;
                component.Parent = parent;
                component.Name = name;
                _components.Add(component);
                _nameBuilder.Append(name);
                _nameBuilder.Append('_');
            }
            
            public void NeedsMouse(bool enabled = true)
            {
                _needsMouse = enabled;
            }
            
            public void NeedsKeyboard(bool enabled = true)
            {
                _needsKeyboard = enabled;
            }
            
            public void SetCurrentFont(UiFont font)
            {
                _font = UiConstants.UiFonts.GetUiFont(font);
            }
            
            public static void SetGlobalFont(UiFont font)
            {
                _globalFont = UiConstants.UiFonts.GetUiFont(font);
            }
            
            public T GetUi<T>(string name) where T : BaseUiComponent
            {
                return (T)_componentLookup[name];
            }
            #endregion
            
            #region Decontructor
            ~UiBuilder()
            {
                DisposeInternal();
            }
            
            public void Dispose()
            {
                DisposeInternal();
                GC.SuppressFinalize(this);
            }
            
            private void DisposeInternal()
            {
                if (_disposed)
                {
                    return;
                }
                
                _disposed = true;
                
                for (int index = 0; index < _components.Count; index++)
                {
                    _components[index].Dispose();
                }
                
                UiFrameworkPool.FreeList(ref _components);
                UiFrameworkPool.FreeStringBuilder(ref _nameBuilder);
                UiFrameworkPool.FreeHash(ref _componentLookup);
                Root = null;
            }
            #endregion
            
            #region Add UI
            public void AddComponent(BaseUiComponent component, BaseUiComponent parent)
            {
                component.Parent = parent.Name;
                component.Name = GetComponentName();
                _componentLookup[component.Name] = component;
                _components.Add(component);
            }
            
            public string GetComponentName()
            {
                _nameBuilder.Length = Root.Name.Length + 1;
                _nameBuilder.Insert(Root.Name.Length + 1, _components.Count.ToString());
                return _nameBuilder.ToString();
            }
            
            public UiPanel Section(BaseUiComponent parent, UiPosition pos)
            {
                UiPanel section = UiPanel.Create(pos, null, UiColors.Clear);
                AddComponent(section, parent);
                return section;
            }
            
            public UiPanel Panel(BaseUiComponent parent, UiColor color, UiPosition pos)
            {
                UiPanel panel = UiPanel.Create(pos, null, color);
                AddComponent(panel, parent);
                return panel;
            }
            
            public UiPanel Panel(BaseUiComponent parent, UiColor color, UiPosition pos, UiOffset offset)
            {
                UiPanel panel = UiPanel.Create(pos, offset, color);
                AddComponent(panel, parent);
                return panel;
            }
            
            public UiButton EmptyCommandButton(BaseUiComponent parent, UiColor color, UiPosition pos, string cmd)
            {
                UiButton button = UiButton.CreateCommand(pos, color, cmd);
                AddComponent(button, parent);
                return button;
            }
            
            public UiButton EmptyCloseButton(BaseUiComponent parent, UiColor color, UiPosition pos, string close)
            {
                UiButton button = UiButton.CreateClose(pos, color, close);
                AddComponent(button, parent);
                return button;
            }
            
            public UiButton TextButton(BaseUiComponent parent, string text, int textSize, UiColor textColor, UiColor buttonColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                Label(button, text, textSize, textColor, UiPosition.FullPosition, align);
                return button;
            }
            
            public UiButton ImageButton(BaseUiComponent parent, UiColor buttonColor, string png, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                Image(button, png, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton WebImageButton(BaseUiComponent parent, UiColor buttonColor, string url, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                Image(button, url, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                ItemIcon(button, itemId, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, ulong skinId, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                ItemIcon(button, itemId, skinId, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton TextCloseButton(BaseUiComponent parent, string text, int textSize, UiColor textColor, UiColor buttonColor, UiPosition pos, string close, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
                Label(button, text, textSize, textColor, UiPosition.FullPosition, align);
                return button;
            }
            
            public UiButton ImageCloseButton(BaseUiComponent parent, UiColor buttonColor, string png, UiPosition pos, string close)
            {
                UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
                Image(button, png, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton WebImageCloseButton(BaseUiComponent parent, UiColor buttonColor, string url, UiPosition pos, string close)
            {
                UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
                WebImage(button, url, UiPosition.FullPosition);
                return button;
            }
            
            public UiImage Image(BaseUiComponent parent, string png, UiPosition pos, UiColor color)
            {
                uint _;
                if (!uint.TryParse(png, out _))
                {
                    throw new UiFrameworkException($"Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead");
                }
                
                UiImage image = UiImage.Create(png, pos, color);
                AddComponent(image, parent);
                return image;
            }
            
            public UiImage Image(BaseUiComponent parent, string png, UiPosition pos)
            {
                return Image(parent, png, pos, UiColors.White);
            }
            
            public UiWebImage WebImage(BaseUiComponent parent, string url, UiPosition pos, UiColor color)
            {
                if (!url.StartsWith("http"))
                {
                    throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use Image instead");
                }
                
                UiWebImage image = UiWebImage.Create(url, pos, color);
                AddComponent(image, parent);
                return image;
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos, UiColor color)
            {
                UiItemIcon image = UiItemIcon.Create(itemId, pos, color);
                AddComponent(image, parent);
                return image;
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos)
            {
                return ItemIcon(parent, itemId, pos, UiColors.White);
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos, UiColor color)
            {
                UiItemIcon image = UiItemIcon.Create(itemId, skinId, pos, color);
                AddComponent(image, parent);
                return image;
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos)
            {
                return ItemIcon(parent, itemId, skinId, pos, UiColors.White);
            }
            
            public UiWebImage WebImage(BaseUiComponent parent, string url, UiPosition pos)
            {
                return WebImage(parent, url, pos, UiColors.White);
            }
            
            public UiLabel Label(BaseUiComponent parent, string text, int size, UiColor color, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabel label = UiLabel.Create(text, size, color, pos, _font, align);
                AddComponent(label, parent);
                return label;
            }
            
            public UiLabel LabelBackground(BaseUiComponent parent, string text, int size, UiColor color, UiColor backgroundColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiPanel panel = Panel(parent, backgroundColor, pos);
                UiLabel label = UiLabel.Create(text, size, color, UiPosition.FullPosition, _font, align);
                AddComponent(label, panel);
                return label;
            }
            
            public UiLabel Countdown(UiLabel label, int startTime, int endTime, int step, string command)
            {
                label.AddCountdown(startTime, endTime, step, command);
                return label;
            }
            
            public T TextOutline<T>(T outline, UiColor color) where T : BaseUiTextOutline
            {
                outline.AddTextOutline(color);
                return outline;
            }
            
            public T TextOutline<T>(T outline, UiColor color, Vector2 distance) where T : BaseUiTextOutline
            {
                outline.AddTextOutline(color, distance);
                return outline;
            }
            
            public T TextOutline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha) where T : BaseUiTextOutline
            {
                outline.AddTextOutline(color, distance, useGraphicAlpha);
                return outline;
            }
            
            public UiInput Input(BaseUiComponent parent, string text, int size, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
            {
                parent = Panel(parent, backgroundColor, pos);
                UiInput input = UiInput.Create(text, size, textColor, pos, cmd, _font, align, charsLimit, isPassword, readOnly, lineType);
                AddComponent(input, parent);
                return input;
            }
            
            public void Border(BaseUiComponent parent, UiColor color, int size = 0, BorderMode border = BorderMode.Top | BorderMode.Bottom | BorderMode.Left | BorderMode.Right)
            {
                if (HasBorderFlag(border, BorderMode.Top))
                {
                    UiPanel panel = UiPanel.Create(UiPosition.Top.ToPosition(), new Offset(0, -1, 0, size), color);
                    AddComponent(panel, parent);
                }
                
                if (HasBorderFlag(border, BorderMode.Left))
                {
                    UiPanel panel = UiPanel.Create(UiPosition.Left.ToPosition(), new Offset(-size, -size, 1, 0), color);
                    AddComponent(panel, parent);
                }
                
                if (HasBorderFlag(border, BorderMode.Bottom))
                {
                    UiPanel panel = UiPanel.Create(UiPosition.Bottom.ToPosition(), new Offset(0, -size, 0, 1), color);
                    AddComponent(panel, parent);
                }
                
                if (HasBorderFlag(border, BorderMode.Right))
                {
                    UiPanel panel = UiPanel.Create(UiPosition.Right.ToPosition(), new Offset(-1, 0, size, 0), color);
                    AddComponent(panel, parent);
                }
            }
            
            private bool HasBorderFlag(BorderMode mode, BorderMode flag)
            {
                return (mode & flag) != 0;
            }
            #endregion
            
            #region JSON
            public string ToJson()
            {
                return JsonCreator.CreateJson(_components, _needsMouse, _needsKeyboard);
            }
            
            public void CacheJson()
            {
                _cachedJson = ToJson();
            }
            #endregion
            
            #region Add UI
            public void AddUi(BasePlayer player)
            {
                AddUi(player.Connection);
            }
            
            public void AddUiCached(BasePlayer player)
            {
                AddUiCached(player.Connection);
            }
            
            public void AddUi(Connection connection)
            {
                AddUi(connection, ToJson());
            }
            
            public void AddUiCached(Connection connection)
            {
                AddUi(connection, _cachedJson);
            }
            
            public void AddUi(List<Connection> connections)
            {
                AddUi(connections, ToJson());
            }
            
            public void AddUiCached(List<Connection> connections)
            {
                AddUi(connections, _cachedJson);
            }
            
            public void AddUi()
            {
                AddUi(ToJson());
            }
            
            public void AddUiCached()
            {
                AddUi(_cachedJson);
            }
            
            public static void AddUi(BasePlayer player, string json)
            {
                AddUi(player.Connection, json);
            }
            
            public static void AddUi(string json)
            {
                AddUi(Net.sv.connections, json);
            }
            
            public static void AddUi(Connection connection, string json)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, UiConstants.RpcFunctions.AddUiFunc, json);
            }
            
            public static void AddUi(List<Connection> connections, string json)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, UiConstants.RpcFunctions.AddUiFunc, json);
            }
            #endregion
            
            #region Destroy Ui
            public void DestroyUi(BasePlayer player)
            {
                DestroyUi(player, Root.Name);
            }
            
            public void DestroyUi(Connection connection)
            {
                DestroyUi(connection, Root.Name);
            }
            
            public void DestroyUi(List<Connection> connections)
            {
                DestroyUi(connections, Root.Name);
            }
            
            public void DestroyUi()
            {
                DestroyUi(Root.Name);
            }
            
            public void DestroyUiImages(BasePlayer player)
            {
                DestroyUiImages(player.Connection);
            }
            
            public void DestroyUiImages()
            {
                DestroyUiImages(Net.sv.connections);
            }
            
            public void DestroyUiImages(Connection connection)
            {
                for (int index = _components.Count - 1; index >= 0; index--)
                {
                    BaseUiComponent component = _components[index];
                    if (component is UiWebImage)
                    {
                        DestroyUi(connection, component.Name);
                    }
                }
            }
            
            public void DestroyUiImages(List<Connection> connections)
            {
                for (int index = _components.Count - 1; index >= 0; index--)
                {
                    BaseUiComponent component = _components[index];
                    if (component is UiWebImage)
                    {
                        DestroyUi(connections, component.Name);
                    }
                }
            }
            
            public static void DestroyUi(BasePlayer player, string name)
            {
                DestroyUi(player.Connection, name);
            }
            
            public static void DestroyUi(string name)
            {
                DestroyUi(Net.sv.connections, name);
            }
            
            public static void DestroyUi(Connection connection, string name)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, UiConstants.RpcFunctions.DestroyUiFunc, name);
            }
            
            public static void DestroyUi(List<Connection> connections, string name)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, UiConstants.RpcFunctions.DestroyUiFunc, name);
            }
            #endregion
        }
        #endregion

        #region Colors\UiColor.cs
        [JsonConverter(typeof(UiColorConverter))]
        public struct UiColor : IEquatable<UiColor>
        {
            #region Fields
            public readonly uint Value;
            public readonly Color Color;
            #endregion
            
            #region Constructors
            public UiColor(Color color)
            {
                Color = color;
                Value = ((uint)(color.r * 255) << 24) + ((uint)(color.g * 255) << 16) + ((uint)(color.b * 255) << 8) + (uint)(color.a * 255);
            }
            
            public UiColor(int red, int green, int blue, int alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
            {
                
            }
            
            public UiColor(byte red, byte green, byte blue, byte alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
            {
                
            }
            
            public UiColor(float red, float green, float blue, float alpha = 1f) : this(new Color(Mathf.Clamp01(red), Mathf.Clamp01(green), Mathf.Clamp01(blue), Mathf.Clamp01(alpha)))
            {
                
            }
            #endregion
            
            #region Modifiers
            public static UiColor WithAlpha(UiColor color, string hex)
            {
                return WithAlpha(color, int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
            }
            
            public static UiColor WithAlpha(UiColor color, int alpha)
            {
                return WithAlpha(color, alpha / 255f);
            }
            
            public static UiColor WithAlpha(UiColor color, float alpha)
            {
                return new UiColor(color.Color.WithAlpha(Mathf.Clamp01(alpha)));
            }
            
            public static UiColor Darken(UiColor color, float percentage)
            {
                percentage = Mathf.Clamp01(percentage);
                Color col = color.Color;
                float red = col.r * (1 - percentage);
                float green = col.g * (1 - percentage);
                float blue = col.b * (1 - percentage);
                
                return new UiColor(red, green, blue, col.a);
            }
            
            public static UiColor Lighten(UiColor color, float percentage)
            {
                percentage = Mathf.Clamp01(percentage);
                Color col = color.Color;
                float red = (1 - col.r) * percentage + col.r;
                float green = (1 - col.g) * percentage + col.g;
                float blue = (1 - col.b) * percentage + col.b;
                
                return new UiColor(red, green, blue, col.a);
            }
            #endregion
            
            #region Operators
            public static implicit operator UiColor(string value) => ParseHexColor(value);
            public static implicit operator UiColor(Color value) => new UiColor(value);
            public static implicit operator Color(UiColor value) => value.Color;
            public static bool operator ==(UiColor lhs, UiColor rhs) => lhs.Value == rhs.Value;
            public static bool operator !=(UiColor lhs, UiColor rhs) => !(lhs == rhs);
            
            public bool Equals(UiColor other)
            {
                return Value == other.Value;
            }
            
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is UiColor && Equals((UiColor)obj);
            }
            
            public override int GetHashCode()
            {
                return (int)Value;
            }
            #endregion
            
            #region Formats
            public string ToHtmlColor()
            {
                return string.Concat("#", ColorUtility.ToHtmlStringRGBA(Color));
            }
            #endregion
            
            #region Parsing
            /// <summary>
            /// Valid Rust Color Formats
            /// 0 0 0
            /// 0.0 0.0 0.0 0.0
            /// 1.0 1.0 1.0
            /// 1.0 1.0 1.0 1.0
            /// </summary>
            /// <param name="color"></param>
            public static UiColor ParseRustColor(string color)
            {
                return new UiColor(ColorEx.Parse(color));
            }
            
            /// <summary>
            /// <a href="https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html">Unity ColorUtility.TryParseHtmlString API reference</a>
            /// Valid Hex Color Formats
            /// #RGB
            /// #RRGGBB
            /// #RGBA
            /// #RRGGBBAA
            /// </summary>
            /// <param name="hexColor"></param>
            /// <returns></returns>
            /// <exception cref="UiFrameworkException"></exception>
            public static UiColor ParseHexColor(string hexColor)
            {
                #if !UiBenchmarks
                Color colorValue = Color.black;
                #else
                Color colorValue;
                if (!ColorUtility.TryParseHtmlString(hexColor, out colorValue))
                {
                    throw new UiFrameworkException($"Invalid Color: '{hexColor}' Hex colors must start with a '#'");
                }
                #endif
                
                return new UiColor(colorValue);
            }
            #endregion
        }
        #endregion

        #region Colors\UiColorConverter.cs
        public class UiColorConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(((UiColor)value).ToHtmlColor());
            }
            
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Null:
                    if (Nullable.GetUnderlyingType(objectType) != null)
                    {
                        return null;
                    }
                    
                    return default(UiColor);
                    
                    case JsonToken.String:
                    return UiColor.ParseHexColor(reader.Value.ToString());
                }
                
                return default(UiColor);
            }
            
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(UiColor);
            }
        }
        #endregion

        #region Colors\UiColors.cs
        public static class UiColors
        {
            public static class BootStrap
            {
                public static readonly UiColor Blue = "#007bff";
                public static readonly UiColor Indigo = "#6610f2";
                public static readonly UiColor Purple = "#6f42c1";
                public static readonly UiColor Pink = "#e83e8c";
                public static readonly UiColor Red = "#dc3545";
                public static readonly UiColor Orange = "#fd7e14";
                public static readonly UiColor Yellow = "#ffc107";
                public static readonly UiColor Green = "#28a745";
                public static readonly UiColor Teal = "#20c997";
                public static readonly UiColor Cyan = "#17a2b8";
                public static readonly UiColor White = "#fff";
                public static readonly UiColor Gray = "#6c757d";
                public static readonly UiColor DarkGray = "#343a40";
                public static readonly UiColor Primary = "#007bff";
                public static readonly UiColor Secondary = "#6c757d";
                public static readonly UiColor Success = "#28a745";
                public static readonly UiColor Info = "#17a2b8";
                public static readonly UiColor Warning = "#ffc107";
                public static readonly UiColor Danger = "#dc3545";
                public static readonly UiColor Light = "#f8f9fa";
                public static readonly UiColor Dark = "#343a40";
            }
            
            public static class Material
            {
                public static readonly UiColor MatPrimary = "#dfe6e9";
                public static readonly UiColor MatPrimaryLight = "#b2bec3";
                public static readonly UiColor MatPrimaryDark = "#636e72";
                public static readonly UiColor MatSecondary = "#2d3436";
                public static readonly UiColor MatSecondaryLight = "#74b9ff";
                public static readonly UiColor MatSecondaryDark = "#0984e3";
                public static readonly UiColor MatTextOnPrimary = "#0984e3";
                public static readonly UiColor MatTextOnSecondary = "#0984e3";
            }
            
            public static class StandardColors
            {
                public static readonly UiColor White = "#FFFFFF";
                public static readonly UiColor Silver = "#C0C0C0";
                public static readonly UiColor Gray = "#808080";
                public static readonly UiColor Black = "#000000";
                public static readonly UiColor Red = "#FF0000";
                public static readonly UiColor Maroon = "#800000";
                public static readonly UiColor Yellow = "#FFFF00";
                public static readonly UiColor Olive = "#808000";
                public static readonly UiColor Lime = "#00FF00";
                public static readonly UiColor Green = "#008000";
                public static readonly UiColor Aqua = "#00FFFF";
                public static readonly UiColor Teal = "#008080";
                public static readonly UiColor Blue = "#0000FF";
                public static readonly UiColor Navy = "#000080";
                public static readonly UiColor Fuchsia = "#FF00FF";
                public static readonly UiColor Purple = "#800080";
            }
            
            public static class Supreme
            {
                public static readonly UiColor Lime = "#acfa58";
                public static readonly UiColor SilverText = "#e3e3e3";
                public static readonly UiColor K1lly0usRed = "#ce422b";
            }
            
            public static class MJSU
            {
                public static readonly UiColor Orange = "#de8732";
            }
            
            public static class Rust
            {
                public static readonly UiColor Red = "#cd4632";
                public static readonly UiColor Green = "#8cc83c";
                public static readonly UiColor Panel = "#CCCCCC";
                
                public static class Ui
                {
                    public static readonly UiColor Panel = "#A6A6A60F";
                    public static readonly UiColor Header = "#DBDBDB33";
                    public static readonly UiColor PanelButton = "#A6A6A60F";
                    public static readonly UiColor OkButton = "#6A804266";
                    public static readonly UiColor Button = "#BFBFBF1A";
                    public static readonly UiColor PanelText = "#E8dED4";
                    public static readonly UiColor PanelButtonText = "#C4C4C4";
                    public static readonly UiColor OkButtonText = "#9BB46E";
                    public static readonly UiColor ButtonText = "#E8DED4CC";
                }
                
                public static class Chat
                {
                    public static readonly UiColor Player = "#55AAFF";
                    public static readonly UiColor Developer = "#FFAA55";
                    public static readonly UiColor Admin = "#AAFF55";
                }
                
                public static class Steam
                {
                    public static readonly UiColor InGame = "#A2DB40";
                    public static readonly UiColor Online = "#60CBF2";
                    public static readonly UiColor Normal = "#F7EBE1";
                }
            }
            
            public static class Form
            {
                public static readonly UiColor Body = "#00001F";
                public static readonly UiColor Header = "#00001F";
                public static readonly UiColor Text = StandardColors.White;
                public static readonly UiColor Panel = "#2B2B2B";
                public static readonly UiColor PanelSecondary = "#3f3f3f";
                public static readonly UiColor PanelTertiary = "#525252";
                public static readonly UiColor ButtonPrimary = Rust.Red;
                public static readonly UiColor ButtonSecondary = "#666666";
            }
            
            #region UI Colors
            public static readonly UiColor Clear = UiColor.WithAlpha(StandardColors.Black, 0f);
            public static readonly UiColor White = StandardColors.White;
            public static readonly UiColor Black = StandardColors.Black;
            public static readonly UiColor Body = UiColor.WithAlpha(Form.Body, "F2");
            public static readonly UiColor BodyHeader = Form.Header;
            public static readonly UiColor Text = UiColor.WithAlpha(Form.Text, "80");
            public static readonly UiColor Panel = Form.Panel;
            public static readonly UiColor PanelSecondary = Form.PanelSecondary;
            public static readonly UiColor PanelTertiary = Form.PanelTertiary;
            public static readonly UiColor CloseButton = Form.ButtonPrimary;
            public static readonly UiColor ButtonPrimary = Form.ButtonPrimary;
            public static readonly UiColor ButtonSecondary = Form.ButtonSecondary;
            public static readonly UiColor RustRed = Rust.Red;
            public static readonly UiColor RustGreen = Rust.Green;
            #endregion
        }
        #endregion

        #region Components\BaseColorComponent.cs
        public class BaseColorComponent : BaseComponent
        {
            public UiColor Color;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddField(writer, JsonDefaults.Color.ColorName, Color);
            }
        }
        #endregion

        #region Components\BaseComponent.cs
        public abstract class BaseComponent : BasePoolable
        {
            public abstract void WriteComponent(JsonFrameworkWriter writer);
        }
        #endregion

        #region Components\BaseImageComponent.cs
        public class BaseImageComponent : FadeInComponent
        {
            public string Sprite;
            public string Material;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Sprite = null;
                Material = null;
            }
        }
        #endregion

        #region Components\BaseTextComponent.cs
        public class BaseTextComponent : FadeInComponent
        {
            public int FontSize = JsonDefaults.BaseText.FontSize;
            public string Font;
            public TextAnchor Align;
            public string Text;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddTextField(writer, JsonDefaults.BaseText.TextName, Text);
                JsonCreator.AddField(writer, JsonDefaults.BaseText.FontSizeName, FontSize, JsonDefaults.BaseText.FontSize);
                JsonCreator.AddField(writer, JsonDefaults.BaseText.FontName, Font, JsonDefaults.BaseText.FontValue);
                JsonCreator.AddField(writer, JsonDefaults.BaseText.AlignName, Align);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                FontSize = JsonDefaults.BaseText.FontSize;
                Font = null;
                Align = TextAnchor.UpperLeft;
                Text = null;
            }
        }
        #endregion

        #region Components\ButtonComponent.cs
        public class ButtonComponent : BaseImageComponent
        {
            private const string Type = "UnityEngine.UI.Button";
            
            public string Command;
            public string Close;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
                JsonCreator.AddField(writer, JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Command = null;
                Close = null;
                Sprite = null;
                Material = null;
            }
        }
        #endregion

        #region Components\CountdownComponent.cs
        public class CountdownComponent : BaseComponent
        {
            private const string Type = "Countdown";
            
            public int StartTime;
            public int EndTime;
            public int Step;
            public string Command;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                StartTime = 0;
                EndTime = 0;
                Step = 0;
                Command = null;
            }
        }
        #endregion

        #region Components\FadeInComponent.cs
        public abstract class FadeInComponent : BaseColorComponent
        {
            public float FadeIn;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddField(writer, JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                FadeIn = 0;
            }
        }
        #endregion

        #region Components\ImageComponent.cs
        public class ImageComponent : BaseImageComponent
        {
            private const string Type = "UnityEngine.UI.Image";
            
            public string Png;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                if (!string.IsNullOrEmpty(Png))
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Image.PNGName, Png);
                }
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Png = null;
            }
        }
        #endregion

        #region Components\InputComponent.cs
        public class InputComponent : BaseTextComponent
        {
            private const string Type = "UnityEngine.UI.InputField";
            
            public int CharsLimit;
            public string Command;
            public bool IsPassword;
            public bool IsReadyOnly;
            public bool NeedsKeyboard = true;
            public InputField.LineType LineType;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
                JsonCreator.AddField(writer, JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
                JsonCreator.AddField(writer, JsonDefaults.Input.LineTypeName, LineType);
                
                if (IsPassword)
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Input.PasswordName, JsonDefaults.Input.PasswordValue);
                }
                
                if (IsReadyOnly)
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Input.ReadOnlyName, JsonDefaults.Input.ReadOnlyValue);
                }
                
                if (NeedsKeyboard)
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Input.InputNeedsKeyboardName, JsonDefaults.Input.InputNeedsKeyboardValue);
                }
                
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                CharsLimit = 0;
                Command = null;
                NeedsKeyboard = true;
                IsPassword = false;
                LineType = default(InputField.LineType);
            }
        }
        #endregion

        #region Components\ItemIconComponent.cs
        public class ItemIconComponent : BaseImageComponent
        {
            public int ItemId;
            public ulong SkinId;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.ItemIcon.ItemIdName, ItemId);
                JsonCreator.AddField(writer, JsonDefaults.ItemIcon.SkinIdName, SkinId, JsonDefaults.ItemIcon.DefaultSkinId);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                ItemId = 0;
                SkinId = 0;
            }
        }
        #endregion

        #region Components\OutlineComponent.cs
        public class OutlineComponent : BaseColorComponent
        {
            private const string Type = "UnityEngine.UI.Outline";
            
            public Vector2 Distance;
            public bool UseGraphicAlpha;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.DistanceValue);
                if (UseGraphicAlpha)
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Outline.UseGraphicAlphaName, JsonDefaults.Outline.UseGraphicAlphaValue);
                }
                
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                Distance = JsonDefaults.Outline.DistanceValue;
                UseGraphicAlpha = false;
            }
        }
        #endregion

        #region Components\RawImageComponent.cs
        public class RawImageComponent : FadeInComponent
        {
            private const string Type = "UnityEngine.UI.RawImage";
            
            public string Url;
            public string Texture;
            public string Material;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.SpriteName, Texture, JsonDefaults.RawImage.TextureValue);
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
                if (!string.IsNullOrEmpty(Url))
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Image.UrlName, Url);
                }
                
                base.WriteComponent(writer);
                
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Url = null;
                Texture = null;
                Material = null;
            }
        }
        #endregion

        #region Components\TextComponent.cs
        public class TextComponent : BaseTextComponent
        {
            private const string Type = "UnityEngine.UI.Text";
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
        }
        #endregion

        #region Enums\BorderMode.cs
        [Flags]
        public enum BorderMode : byte
        {
            Top = 1 << 0,
            Left = 1 << 1,
            Bottom = 1 << 2,
            Right = 1 << 3,
        }
        #endregion

        #region Enums\UiFont.cs
        public enum UiFont : byte
        {
            /// <summary>
            /// droidsansmono.ttf
            /// </summary>
            DroidSansMono,
            
            /// <summary>
            /// permanentmarker.ttf
            /// </summary>
            PermanentMarker,
            
            /// <summary>
            /// robotocondensed-bold.ttf
            /// </summary>
            RobotoCondensedBold,
            
            /// <summary>
            /// robotocondensed-regular.ttf
            /// </summary>
            RobotoCondensedRegular
        }
        #endregion

        #region Enums\UiLayer.cs
        public enum UiLayer : byte
        {
            Overall,
            Overlay,
            Hud,
            HudMenu,
            Under,
        }
        #endregion

        #region Exceptions\UiFrameworkException.cs
        public class UiFrameworkException : Exception
        {
            public UiFrameworkException(string message) : base(message)
            {
                
            }
        }
        #endregion

        #region Extensions\EnumExt{T}.cs
        public static class EnumExt<T>
        {
            private static readonly Hash<T, string> CachedStrings = new Hash<T, string>();
            
            static EnumExt()
            {
                foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
                {
                    CachedStrings[value] = value.ToString();
                }
            }
            
            public static string ToString(T value)
            {
                return CachedStrings[value];
            }
        }
        #endregion

        #region Extensions\UiColorExt.cs
        public static class UiColorExt
        {
            private const string Format = "0.####";
            private const string RGBFormat = "{0} ";
            private const string AFormat = "{0}";
            
            private static readonly Hash<uint, string> ColorCache = new Hash<uint, string>();
            
            public static void WriteColor(StringBuilder writer, UiColor uiColor)
            {
                string color = ColorCache[uiColor.Value];
                if (color == null)
                {
                    color = GetColor(uiColor);
                    ColorCache[uiColor.Value] = color;
                }
                
                writer.Append(color);
            }
            
            public static string GetColor(Color color)
            {
                StringBuilder builder = UiFrameworkPool.GetStringBuilder();
                builder.AppendFormat(RGBFormat, color.r.ToString(Format));
                builder.AppendFormat(RGBFormat, color.g.ToString(Format));
                builder.AppendFormat(RGBFormat, color.b.ToString(Format));
                builder.AppendFormat(AFormat, color.a.ToString(Format));
                return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
            }
        }
        #endregion

        #region Extensions\VectorExt.cs
        public static class VectorExt
        {
            private const string Format = "0.####";
            private const char Space = ' ';
            private const short PositionRounder = 10000;
            
            private static readonly Dictionary<ushort, string> PositionCache = new Dictionary<ushort, string>();
            private static readonly Dictionary<short, string> OffsetCache = new Dictionary<short, string>();
            
            static VectorExt()
            {
                for (ushort i = 0; i <= PositionRounder; i++)
                {
                    PositionCache[i] = (i / (float)PositionRounder).ToString(Format);
                }
            }
            
            public static void WritePos(StringBuilder sb, Vector2 pos)
            {
                sb.Append(PositionCache[(ushort)(pos.x * PositionRounder)]);
                sb.Append(Space);
                sb.Append(PositionCache[(ushort)(pos.y * PositionRounder)]);
            }
            
            public static void WritePos(StringBuilder sb, Vector2Short pos)
            {
                string formattedPos;
                if (!OffsetCache.TryGetValue(pos.X, out formattedPos))
                {
                    formattedPos = pos.X.ToString();
                    OffsetCache[pos.X] = formattedPos;
                }
                
                sb.Append(formattedPos);
                sb.Append(Space);
                
                if (!OffsetCache.TryGetValue(pos.Y, out formattedPos))
                {
                    formattedPos = pos.Y.ToString();
                    OffsetCache[pos.Y] = formattedPos;
                }
                
                sb.Append(formattedPos);
            }
        }
        #endregion

        #region Json\JsonCreator.cs
        public static class JsonCreator
        {
            public static string CreateJson(List<BaseUiComponent> components, bool needsMouse, bool needsKeyboard)
            {
                JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
                
                writer.WriteStartArray();
                components[0].WriteRootComponent(writer, needsMouse, needsKeyboard);
                
                for (int index = 1; index < components.Count; index++)
                {
                    components[index].WriteComponent(writer);
                }
                
                writer.WriteEndArray();
                
                return writer.ToJson();
            }
            
            public static void AddFieldRaw(JsonFrameworkWriter writer, string name, string value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
            
            public static void AddFieldRaw(JsonFrameworkWriter writer, string name, int value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
            
            public static void AddFieldRaw(JsonFrameworkWriter writer, string name, bool value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, string value, string defaultValue)
            {
                if (value != null && value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, Vector2 value, Vector2 defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, Vector2Short value, Vector2Short defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, TextAnchor value)
            {
                if (value != TextAnchor.UpperLeft)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(EnumExt<TextAnchor>.ToString(value));
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, InputField.LineType value)
            {
                if (value != InputField.LineType.SingleLine)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(EnumExt<InputField.LineType>.ToString(value));
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, int value, int defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, float value, float defaultValue)
            {
                if (Math.Abs(value - defaultValue) >= 0.0001)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, UiColor color)
            {
                if (color.Value != JsonDefaults.Color.ColorValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(color);
                }
            }
            
            public static void AddTextField(JsonFrameworkWriter writer, string name, string value)
            {
                writer.WritePropertyName(name);
                writer.WriteTextValue(value);
            }
            
            public static void AddMouse(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
                writer.WriteEndObject();
            }
            
            public static void AddKeyboard(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
                writer.WriteEndObject();
            }
        }
        #endregion

        #region Json\JsonDefaults.cs
        public static class JsonDefaults
        {
            public static class Common
            {
                public const string ComponentTypeName = "type";
                public const string ComponentName = "name";
                public const string ParentName = "parent";
                public const string FadeInName = "fadeIn";
                public const string FadeOutName = "fadeOut";
                public const float FadeOut = 0;
                public const float FadeIn = 0;
                public const string RectTransformName = "RectTransform";
                public const string NullValue = null;
                public const string NeedsCursorValue = "NeedsCursor";
                public const string NeedsKeyboardValue = "NeedsKeyboard";
                public const string CommandName = "command";
            }
            
            public static class Position
            {
                public const string AnchorMinName = "anchormin";
                public const string AnchorMaxName = "anchormax";
                public static readonly Vector2 AnchorMin = new Vector2(0, 0);
                public static readonly Vector2 AnchorMax = new Vector2(1, 1);
            }
            
            public static class Offset
            {
                public const string OffsetMinName = "offsetmin";
                public const string OffsetMaxName = "offsetmax";
                
                public static readonly Vector2Short OffsetMin = new Vector2Short(0, 0);
                public static readonly Vector2Short OffsetMax = new Vector2Short(0, 0);
                public const string DefaultOffsetMax = "0 0";
            }
            
            public static class Color
            {
                public const string ColorName = "color";
                public const uint ColorValue = ((uint)255 << 24) + (255 << 16) + (255 << 8) + 255;
            }
            
            public static class BaseImage
            {
                public const string SpriteName = "sprite";
                public const string MaterialName = "material";
                public const string Sprite = "Assets/Content/UI/UI.Background.Tile.psd";
                public const string Material = "Assets/Icons/IconMaterial.mat";
            }
            
            public static class RawImage
            {
                public const string TextureValue = "Assets/Icons/rust.png";
            }
            
            public static class BaseText
            {
                public const int FontSize = 14;
                public const string FontValue = "RobotoCondensed-Bold.ttf";
                public const string FontName = "font";
                public const string TextName = "text";
                public const string FontSizeName = "fontSize";
                public const string AlignName = "align";
            }
            
            public static class Outline
            {
                public const string DistanceName = "distance";
                public const string UseGraphicAlphaName = "useGraphicAlpha";
                public const string UseGraphicAlphaValue = "True";
                public static readonly Vector2 DistanceValue = new Vector2(1.0f, -1.0f);
            }
            
            public static class Button
            {
                public const string CloseName = "close";
            }
            
            public static class Image
            {
                public const string PNGName = "png";
                public const string UrlName = "url";
            }
            
            public static class ItemIcon
            {
                public const string ItemIdName = "itemid";
                public const string SkinIdName = "skinid";
                public const ulong DefaultSkinId = 0;
            }
            
            public static class Input
            {
                public const string CharacterLimitName = "characterLimit";
                public const int CharacterLimitValue = 0;
                public const string PasswordName = "password";
                public const string PasswordValue = "";
                public const string ReadOnlyName = "readOnly";
                public const bool ReadOnlyValue = false;
                public const string LineTypeName = "lineType";
                public const string InputNeedsKeyboardName = "needsKeyboard";
                public const string InputNeedsKeyboardValue = "";
            }
            
            public static class Countdown
            {
                public const string StartTimeName = "startTime";
                public const int StartTimeValue = 0;
                public const string EndTimeName = "endTime";
                public const int EndTimeValue = 0;
                public const string StepName = "step";
                public const int StepValue = 1;
                public const string CountdownCommandName = "command";
            }
        }
        #endregion

        #region Json\JsonFrameworkWriter.cs
        public class JsonFrameworkWriter : BasePoolable
        {
            private const char QuoteChar = '\"';
            private const char ArrayStartChar = '[';
            private const char ArrayEndChar = ']';
            private const char ObjectStartChar = '{';
            private const char ObjectEndChar = '}';
            private const char CommaChar = ',';
            private const string Separator = "\":";
            private const string PropertyComma = ",\"";
            
            private bool _propertyComma;
            private bool _objectComma;
            
            private StringBuilder _writer;
            
            private void Init()
            {
                _writer = UiFrameworkPool.GetStringBuilder();
            }
            
            public static JsonFrameworkWriter Create()
            {
                JsonFrameworkWriter writer = UiFrameworkPool.Get<JsonFrameworkWriter>();
                writer.Init();
                return writer;
            }
            
            private void OnDepthIncrease()
            {
                if (_objectComma)
                {
                    _writer.Append(CommaChar);
                }
                
                _propertyComma = false;
                _objectComma = false;
            }
            
            private void OnDepthDecrease()
            {
                _objectComma = true;
            }
            
            public void WriteStartArray()
            {
                OnDepthIncrease();
                _writer.Append(ArrayStartChar);
            }
            
            public void WriteEndArray()
            {
                _writer.Append(ArrayEndChar);
                OnDepthDecrease();
            }
            
            public void WriteStartObject()
            {
                OnDepthIncrease();
                _writer.Append(ObjectStartChar);
            }
            
            public void WriteEndObject()
            {
                _writer.Append(ObjectEndChar);
                OnDepthDecrease();
            }
            
            public void WritePropertyName(string name)
            {
                if (_propertyComma)
                {
                    _writer.Append(PropertyComma);
                }
                else
                {
                    _propertyComma = true;
                    _writer.Append(QuoteChar);
                }
                
                _writer.Append(name);
                _writer.Append(Separator);
            }
            
            public void WriteValue(bool value)
            {
                _writer.Append(value ? "true" : "false");
            }
            
            public void WriteValue(int value)
            {
                _writer.Append(value.ToString());
            }
            
            public void WriteValue(float value)
            {
                _writer.Append(value.ToString());
            }
            
            public void WriteValue(string value)
            {
                _writer.Append(QuoteChar);
                _writer.Append(value);
                _writer.Append(QuoteChar);
            }
            
            public void WriteTextValue(string value)
            {
                _writer.Append(QuoteChar);
                for (int i = 0; i < value.Length; i++)
                {
                    char character = value[i];
                    if (character == '\"')
                    {
                        _writer.Append("\\\"");
                    }
                    else
                    {
                        _writer.Append(character);
                    }
                }
                _writer.Append(QuoteChar);
            }
            
            public void WriteValue(Vector2 pos)
            {
                _writer.Append(QuoteChar);
                VectorExt.WritePos(_writer, pos);
                _writer.Append(QuoteChar);
            }
            
            public void WriteValue(Vector2Short offset)
            {
                _writer.Append(QuoteChar);
                VectorExt.WritePos(_writer, offset);
                _writer.Append(QuoteChar);
            }
            
            public void WriteValue(UiColor color)
            {
                _writer.Append(QuoteChar);
                UiColorExt.WriteColor(_writer, color);
                _writer.Append(QuoteChar);
            }
            
            protected override void EnterPool()
            {
                UiFrameworkPool.FreeStringBuilder(ref _writer);
            }
            
            public override string ToString()
            {
                return _writer.ToString();
            }
            
            public string ToJson()
            {
                string json = _writer.ToString();
                Dispose();
                return json;
            }
        }
        #endregion

        #region Offsets\MovableUiOffset.cs
        public class MovableUiOffset : UiOffset
        {
            public int XMin;
            public int YMin;
            public int XMax;
            public int YMax;
            private readonly Vector2Short _initialMin;
            private readonly Vector2Short _initialMax;
            
            public MovableUiOffset(int x, int y, int width, int height)
            {
                XMin = x;
                YMin = y;
                XMax = x + width;
                YMax = y + height;
                _initialMin = new Vector2Short(XMin, YMin);
                _initialMax = new Vector2Short(XMax, YMax);
            }
            
            public void MoveX(int pixels)
            {
                XMin += pixels;
                XMax += pixels;
            }
            
            public void MoveY(int pixels)
            {
                YMin += pixels;
                YMax += pixels;
            }
            
            public void SetWidth(int width)
            {
                XMax = XMin + width;
            }
            
            public void SetHeight(int height)
            {
                YMax = YMin + height;
            }
            
            public override Offset ToOffset()
            {
                return new Offset(XMin, YMin, XMax, YMax);
            }
            
            public UiOffset ToStatic()
            {
                return new StaticUiOffset(XMin, YMin, XMax, YMax);
            }
            
            public void Reset()
            {
                XMin = _initialMin.X;
                YMin = _initialMin.Y;
                XMax = _initialMax.X;
                YMax = _initialMax.Y;
            }
        }
        #endregion

        #region Offsets\Offset.cs
        public struct Offset
        {
            public Vector2Short Min;
            public Vector2Short Max;
            
            public Offset(int xMin, int yMin, int xMax, int yMax)
            {
                Min = new Vector2Short(xMin, yMin);
                Max = new Vector2Short(xMax, yMax);
            }
        }
        #endregion

        #region Offsets\StaticUiOffset.cs
        public class StaticUiOffset : UiOffset
        {
            private readonly Offset _offset;
            
            public StaticUiOffset(int width, int height)
            {
                _offset = new Offset(-width / 2, -height / 2, width, height);
            }
            
            public StaticUiOffset(int x, int y, int width, int height)
            {
                if (width < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(width), "width cannot be less than 0");
                }
                
                if (height < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(height), "height cannot be less than 0");
                }
                
                _offset = new Offset(x, y, x + width, y + height);
            }
            
            public override Offset ToOffset()
            {
                return _offset;
            }
        }
        #endregion

        #region Offsets\UiOffset.cs
        public abstract class UiOffset
        {
            public static readonly UiOffset DefaultOffset = new StaticUiOffset(0, 0, 0, 0);
            
            public abstract Offset ToOffset();
        }
        #endregion

        #region Offsets\Vector2Short.cs
        public struct Vector2Short : IEquatable<Vector2Short>
        {
            public readonly short X;
            public readonly short Y;
            
            public Vector2Short(short x, short y)
            {
                X = x;
                Y = y;
            }
            
            public Vector2Short(int x, int y) : this((short)x, (short)y) { }
            
            public bool Equals(Vector2Short other)
            {
                return X == other.X && Y == other.Y;
            }
            
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Vector2Short && Equals((Vector2Short)obj);
            }
            
            public override int GetHashCode()
            {
                return (int)X | (Y << 16);
            }
            
            public static bool operator ==(Vector2Short lhs, Vector2Short rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;
            
            public static bool operator !=(Vector2Short lhs, Vector2Short rhs) => !(lhs == rhs);
        }
        #endregion

        #region Pooling\BasePool.cs
        public abstract class BasePool<T> : IPool<T> where T : class, new()
        {
            private readonly Queue<T> _pool;
            private readonly int _maxSize;
            
            /// <summary>
            /// Base Pool Constructor
            /// </summary>
            /// <param name="maxSize">Max Size of the pool</param>
            protected BasePool(int maxSize)
            {
                _maxSize = maxSize;
                _pool = new Queue<T>(maxSize);
                for (int i = 0; i < maxSize; i++)
                {
                    _pool.Enqueue(new T());
                }
                
                UiFrameworkPool.AddPool(this);
            }
            
            /// <summary>
            /// Returns an element from the pool if it exists else it creates a new one
            /// </summary>
            /// <returns></returns>
            public T Get()
            {
                T item = _pool.Count != 0 ? _pool.Dequeue() : new T();
                OnGetItem(item);
                return item;
            }
            
            /// <summary>
            /// Frees an item back to the pool
            /// </summary>
            /// <param name="item">Item being freed</param>
            public void Free(ref T item)
            {
                if (item == null)
                {
                    return;
                }
                
                if (!OnFreeItem(ref item))
                {
                    return;
                }
                
                if (_pool.Count >= _maxSize)
                {
                    return;
                }
                
                _pool.Enqueue(item);
                
                item = null;
            }
            
            /// <summary>
            /// Called when an item is retrieved from the pool
            /// </summary>
            /// <param name="item">Item being retrieved</param>
            protected virtual void OnGetItem(T item)
            {
                
            }
            
            /// <summary>
            /// Returns if an item can be freed to the pool
            /// </summary>
            /// <param name="item">Item to be freed</param>
            /// <returns>True if can be freed; false otherwise</returns>
            protected virtual bool OnFreeItem(ref T item)
            {
                return true;
            }
            
            public virtual void Clear()
            {
                _pool.Clear();
            }
        }
        #endregion

        #region Pooling\BasePoolable.cs
        public class BasePoolable : IDisposable
        {
            internal bool Disposed;
            
            /// <summary>
            /// Returns if the object should be pooled.
            /// This field is set to true when leaving the pool.
            /// If the object instantiated using new() outside the pool it will be false
            /// </summary>
            private bool _shouldPool;
            
            internal void EnterPoolInternal()
            {
                EnterPool();
                _shouldPool = false;
                Disposed = true;
            }
            
            internal void LeavePoolInternal()
            {
                _shouldPool = true;
                Disposed = false;
                LeavePool();
            }
            
            /// <summary>
            /// Called when the object is returned to the pool.
            /// Can be overriden in child classes to cleanup used data
            /// </summary>
            protected virtual void EnterPool()
            {
                
            }
            
            /// <summary>
            /// Called when the object leaves the pool.
            /// Can be overriden in child classes to set the initial object state
            /// </summary>
            protected virtual void LeavePool()
            {
                
            }
            
            /// <summary>
            /// Frees a pooled object that is part of a field on this object
            /// </summary>
            /// <param name="obj">Object to free</param>
            /// <typeparam name="T">Type of object being freed</typeparam>
            protected void Free<T>(ref T obj) where T : BasePoolable, new()
            {
                if (obj != null && obj._shouldPool)
                {
                    UiFrameworkPool.Free(ref obj);
                }
            }
            
            /// <summary>
            /// Frees a pooled list that is part of a field on this object
            /// </summary>
            /// <param name="obj">List to be freed</param>
            /// <typeparam name="T">Type of the list</typeparam>
            protected void FreeList<T>(ref List<T> obj)
            {
                UiFrameworkPool.FreeList(ref obj);
            }
            
            /// <summary>
            /// Disposes the object when used in a using statement
            /// </summary>
            public void Dispose()
            {
                if (_shouldPool)
                {
                    UiFrameworkPool.Free(this);
                }
            }
        }
        #endregion

        #region Pooling\HashPool.cs
        public class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>>
        {
            public static IPool<Hash<TKey, TValue>> Instance;
            
            static HashPool()
            {
                Instance = new HashPool<TKey, TValue>();
            }
            
            private HashPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
            {
                item.Clear();
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        #endregion

        #region Pooling\IPool.cs
        public interface IPool
        {
            void Clear();
        }
        #endregion

        #region Pooling\IPool{T}.cs
        public interface IPool<T> : IPool
        {
            /// <summary>
            /// Returns the Pooled type or a new instance if pool is empty.
            /// </summary>
            /// <returns></returns>
            T Get();
            
            /// <summary>
            /// Returns the pooled type back to the pool
            /// </summary>
            /// <param name="item"></param>
            void Free(ref T item);
        }
        #endregion

        #region Pooling\ListPool.cs
        public class ListPool<T> : BasePool<List<T>>
        {
            public static IPool<List<T>> Instance;
            
            static ListPool()
            {
                Instance = new ListPool<T>();
            }
            
            private ListPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref List<T> item)
            {
                item.Clear();
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        #endregion

        #region Pooling\MemoryStreamPool.cs
        public class MemoryStreamPool : BasePool<MemoryStream>
        {
            public static IPool<MemoryStream> Instance;
            
            static MemoryStreamPool()
            {
                Instance = new MemoryStreamPool();
            }
            
            private MemoryStreamPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref MemoryStream item)
            {
                item.Position = 0;
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        #endregion

        #region Pooling\ObjectPool.cs
        public class ObjectPool<T> : BasePool<T> where T : BasePoolable, new()
        {
            public static IPool<T> Instance;
            
            static ObjectPool()
            {
                Instance = new ObjectPool<T>();
            }
            
            private ObjectPool() : base(1024) { }
            
            protected override void OnGetItem(T item)
            {
                item.LeavePoolInternal();
            }
            
            protected override bool OnFreeItem(ref T item)
            {
                if (item.Disposed)
                {
                    return false;
                }
                
                item.EnterPoolInternal();
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        #endregion

        #region Pooling\StringBuilderPool.cs
        public class StringBuilderPool : BasePool<StringBuilder>
        {
            public static IPool<StringBuilder> Instance;
            
            static StringBuilderPool()
            {
                Instance = new StringBuilderPool();
            }
            
            private StringBuilderPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref StringBuilder item)
            {
                item.Length = 0;
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        #endregion

        #region Pooling\UiFrameworkPool.cs
        public static class UiFrameworkPool
        {
            private static readonly Hash<Type, IPool> Pools = new Hash<Type, IPool>();
            
            /// <summary>
            /// Returns a pooled object of type T
            /// Must inherit from <see cref="BasePoolable"/> and have an empty default constructor
            /// </summary>
            /// <typeparam name="T">Type to be returned</typeparam>
            /// <returns>Pooled object of type T</returns>
            public static T Get<T>() where T : BasePoolable, new()
            {
                return ObjectPool<T>.Instance.Get();
            }
            
            /// <summary>
            /// Returns a <see cref="BasePoolable"/> back into the pool
            /// </summary>
            /// <param name="value">Object to free</param>
            /// <typeparam name="T">Type of object being freed</typeparam>
            public static void Free<T>(ref T value) where T : BasePoolable, new()
            {
                ObjectPool<T>.Instance.Free(ref value);
            }
            
            /// <summary>
            /// Returns a <see cref="BasePoolable"/> back into the pool
            /// </summary>
            /// <param name="value">Object to free</param>
            /// <typeparam name="T">Type of object being freed</typeparam>
            internal static void Free<T>(T value) where T : BasePoolable, new()
            {
                ObjectPool<T>.Instance.Free(ref value);
            }
            
            /// <summary>
            /// Returns a pooled <see cref="List{T}"/>
            /// </summary>
            /// <typeparam name="T">Type for the list</typeparam>
            /// <returns>Pooled List</returns>
            public static List<T> GetList<T>()
            {
                return ListPool<T>.Instance.Get();
            }
            
            /// <summary>
            /// Returns a pooled <see cref="Hash{TKey, TValue}"/>
            /// </summary>
            /// <typeparam name="TKey">Type for the key</typeparam>
            /// <typeparam name="TValue">Type for the value</typeparam>
            /// <returns>Pooled Hash</returns>
            public static Hash<TKey, TValue> GetHash<TKey, TValue>()
            {
                return HashPool<TKey, TValue>.Instance.Get();
            }
            
            /// <summary>
            /// Returns a pooled <see cref="StringBuilder"/>
            /// </summary>
            /// <returns>Pooled <see cref="StringBuilder"/></returns>
            public static StringBuilder GetStringBuilder()
            {
                return StringBuilderPool.Instance.Get();
            }
            
            /// <summary>
            /// Returns a pooled <see cref="MemoryStream"/>
            /// </summary>
            /// <returns>Pooled <see cref="MemoryStream"/></returns>
            public static MemoryStream GetMemoryStream()
            {
                return MemoryStreamPool.Instance.Get();
            }
            
            /// <summary>
            /// Free's a pooled <see cref="List{T}"/>
            /// </summary>
            /// <param name="list">List to be freed</param>
            /// <typeparam name="T">Type of the list</typeparam>
            public static void FreeList<T>(ref List<T> list)
            {
                ListPool<T>.Instance.Free(ref list);
            }
            
            /// <summary>
            /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
            /// </summary>
            /// <param name="hash">Hash to be freed</param>
            /// <typeparam name="TKey">Type for key</typeparam>
            /// <typeparam name="TValue">Type for value</typeparam>
            public static void FreeHash<TKey, TValue>(ref Hash<TKey, TValue> hash)
            {
                HashPool<TKey, TValue>.Instance.Free(ref hash);
            }
            
            /// <summary>
            /// Frees a <see cref="StringBuilder"/> back to the pool
            /// </summary>
            /// <param name="sb">StringBuilder being freed</param>
            public static void FreeStringBuilder(ref StringBuilder sb)
            {
                StringBuilderPool.Instance.Free(ref sb);
            }
            
            /// <summary>
            /// Frees a <see cref="MemoryStream"/> back to the pool
            /// </summary>
            /// <param name="stream">MemoryStream being freed</param>
            public static void FreeMemoryStream(ref MemoryStream stream)
            {
                MemoryStreamPool.Instance.Free(ref stream);
            }
            
            /// <summary>
            /// Frees a <see cref="StringBuilder"/> back to the pool returning the <see cref="string"/>
            /// </summary>
            /// <param name="sb"><see cref="StringBuilder"/> being freed</param>
            public static string ToStringAndFreeStringBuilder(ref StringBuilder sb)
            {
                string result = sb.ToString();
                StringBuilderPool.Instance.Free(ref sb);
                return result;
            }
            
            public static void AddPool<TType>(BasePool<TType> pool) where TType : class, new()
            {
                Pools[typeof(TType)] = pool;
            }
            
            public static void OnUnload()
            {
                foreach (IPool pool in Pools.Values)
                {
                    pool.Clear();
                }
                
                Pools.Clear();
            }
        }
        #endregion

        #region Positions\GridPosition.cs
        public class GridPosition : MovablePosition
        {
            public readonly float NumCols;
            public readonly float NumRows;
            
            public GridPosition(float xMin, float yMin, float xMax, float yMax, float numCols, float numRows) : base(xMin, yMin, xMax, yMax)
            {
                NumCols = numCols;
                NumRows = numRows;
            }
            
            public void MoveCols(int cols)
            {
                XMin += cols / NumCols;
                XMax += cols / NumCols;
                
                if (XMax > 1)
                {
                    XMin -= 1;
                    XMax -= 1;
                    MoveRows(-1);
                }
                
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveRows(int rows)
            {
                YMin += rows / NumRows;
                YMax += rows / NumRows;
                
                #if UiDebug
                ValidatePositions();
                #endif
            }
        }
        #endregion

        #region Positions\GridPositionBuilder.cs
        public class GridPositionBuilder
        {
            private float _xMin;
            private float _yMin;
            private float _xMax;
            private float _yMax;
            
            private readonly float _numCols;
            private readonly float _numRows;
            private int _rowHeight = 1;
            private int _rowOffset;
            private int _colWidth = 1;
            private int _colOffset;
            private float _xPad;
            private float _yPad;
            
            public GridPositionBuilder(int size) : this(size, size)
            {
            }
            
            public GridPositionBuilder(int numCols, int numRows)
            {
                _numCols = numCols;
                _numRows = numRows;
            }
            
            public GridPositionBuilder SetRowHeight(int height)
            {
                _rowHeight = height;
                return this;
            }
            
            public GridPositionBuilder SetRowOffset(int offset)
            {
                _rowOffset = offset;
                return this;
            }
            
            public GridPositionBuilder SetColWidth(int width)
            {
                _colWidth = width;
                return this;
            }
            
            public GridPositionBuilder SetColOffset(int offset)
            {
                _colOffset = offset;
                return this;
            }
            
            public GridPositionBuilder SetPadding(float padding)
            {
                _xPad = padding;
                _yPad = padding;
                return this;
            }
            
            public GridPositionBuilder SetPadding(float xPad, float yPad)
            {
                _xPad = xPad;
                _yPad = yPad;
                return this;
            }
            
            public GridPositionBuilder SetRowPadding(float padding)
            {
                _xPad = padding;
                return this;
            }
            
            public GridPositionBuilder SetColPadding(float padding)
            {
                _yPad = padding;
                return this;
            }
            
            public GridPosition Build()
            {
                if (_rowHeight != 0)
                {
                    float size = _rowHeight / _numCols;
                    _xMax += size;
                }
                
                if (_rowOffset != 0)
                {
                    float size = _rowOffset / _numCols;
                    _xMin += size;
                    _xMax += size;
                }
                
                if (_colWidth != 0)
                {
                    float size = _colWidth / _numRows;
                    _yMax += size;
                }
                
                if (_colOffset != 0)
                {
                    float size = _colOffset / _numRows;
                    _yMin += size;
                    _yMax += size;
                }
                
                _xMin += _xPad;
                _xMax -= _xPad;
                float yMin = _yMin; //Need to save yMin before we overwrite it
                _yMin = 1 - _yMax + _yPad;
                _yMax = 1 - yMin - _yPad;
                
                #if UiDebug
                ValidatePositions();
                #endif
                
                return new GridPosition(_xMin, _yMin, _xMax, _yMax, _numCols, _numRows);
            }
        }
        #endregion

        #region Positions\MovablePosition.cs
        public class MovablePosition : UiPosition
        {
            public float XMin;
            public float YMin;
            public float XMax;
            public float YMax;
            private readonly Vector4 _state;
            
            public MovablePosition(float xMin, float yMin, float xMax, float yMax)
            {
                XMin = xMin;
                YMin = yMin;
                XMax = xMax;
                YMax = yMax;
                _state = new Vector4(XMin, YMin, XMax, YMax);
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public override Position ToPosition()
            {
                return new Position(XMin, YMin, XMax, YMax);
            }
            
            public void Set(float xMin, float yMin, float xMax, float yMax)
            {
                SetX(xMin, xMax);
                SetY(yMin, yMax);
            }
            
            public void SetX(float xMin, float xMax)
            {
                XMin = xMin;
                XMax = xMax;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void SetY(float yMin, float yMax)
            {
                YMin = yMin;
                YMax = yMax;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveX(float delta)
            {
                XMin += delta;
                XMax += delta;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveXPadded(float padding)
            {
                float spacing = (XMax - XMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
                XMin += spacing;
                XMax += spacing;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveY(float delta)
            {
                YMin += delta;
                YMax += delta;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveYPadded(float padding)
            {
                float spacing = (YMax - YMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
                YMin += spacing;
                YMax += spacing;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public StaticUiPosition ToStatic()
            {
                return new StaticUiPosition(XMin, YMin, XMax, YMax);
            }
            
            public void Reset()
            {
                XMin = _state.x;
                YMin = _state.y;
                XMax = _state.z;
                YMax = _state.w;
            }
            
            #if UiDebug
            protected void ValidatePositions()
            {
                if (XMin < 0 || XMin > 1)
                {
                    PrintError($"[{GetType().Name}] XMin is out or range at: {XMin}");
                }
                
                if (XMax > 1 || XMax < 0)
                {
                    PrintError($"[{GetType().Name}] XMax is out or range at: {XMax}");
                }
                
                if (YMin < 0 || YMin > 1)
                {
                    PrintError($"[{GetType().Name}] YMin is out or range at: {YMin}");
                }
                
                if (YMax > 1 || YMax < 0)
                {
                    PrintError($"[{GetType().Name}] YMax is out or range at: {YMax}");
                }
            }
            
            private void PrintError(string format)
            {
                _ins.PrintError(format);
            }
            #endif
            
            public override string ToString()
            {
                return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
            }
        }
        #endregion

        #region Positions\Position.cs
        public struct Position
        {
            public readonly Vector2 Min;
            public readonly Vector2 Max;
            
            public Position(float xMin, float yMin, float xMax, float yMax)
            {
                Min = new Vector2(Mathf.Clamp01(xMin), Mathf.Clamp01(yMin));
                Max = new Vector2(Mathf.Clamp01(xMax), Mathf.Clamp01(yMax));
            }
        }
        #endregion

        #region Positions\StaticUiPosition.cs
        public class StaticUiPosition : UiPosition
        {
            private readonly Position _pos;
            
            public StaticUiPosition(float xMin, float yMin, float xMax, float yMax)
            {
                _pos = new Position(xMin, yMin, xMax, yMax);
            }
            
            public override Position ToPosition()
            {
                return _pos;
            }
        }
        #endregion

        #region Positions\UiPosition.cs
        public abstract class UiPosition
        {
            public static readonly UiPosition FullPosition = new StaticUiPosition(0, 0, 1, 1);
            public static readonly UiPosition TopLeft = new StaticUiPosition(0, 1, 0, 1);
            public static readonly UiPosition MiddleLeft = new StaticUiPosition(0, .5f, 0, .5f);
            public static readonly UiPosition BottomLeft = new StaticUiPosition(0, 0, 0, 0);
            public static readonly UiPosition TopMiddle = new StaticUiPosition(.5f, 1, .5f, 1);
            public static readonly UiPosition MiddleMiddle = new StaticUiPosition(.5f, .5f, .5f, .5f);
            public static readonly UiPosition BottomMiddle = new StaticUiPosition(.5f, 0, .5f, 0);
            public static readonly UiPosition TopRight = new StaticUiPosition(1, 1, 1, 1);
            public static readonly UiPosition MiddleRight = new StaticUiPosition(1, .5f, 1, .5f);
            public static readonly UiPosition BottomRight = new StaticUiPosition(1, 0, 1, 0);
            
            public static readonly UiPosition Top = new StaticUiPosition(0, 1, 1, 1);
            public static readonly UiPosition Bottom = new StaticUiPosition(0, 0, 1, 0);
            public static readonly UiPosition Left = new StaticUiPosition(0, 0, 0, 1);
            public static readonly UiPosition Right = new StaticUiPosition(1, 0, 1, 1);
            
            public abstract Position ToPosition();
        }
        #endregion

        #region UiElements\BaseUiComponent.cs
        public abstract class BaseUiComponent : BasePoolable
        {
            public string Name;
            public string Parent;
            public float FadeOut;
            public Position Position;
            public Offset? Offset;
            
            protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
            {
                T component = UiFrameworkPool.Get<T>();
                component.Position = pos.ToPosition();
                component.Offset = offset?.ToOffset();
                return component;
            }
            
            protected static T CreateBase<T>(Position pos, Offset? offset) where T : BaseUiComponent, new()
            {
                T component = UiFrameworkPool.Get<T>();
                component.Position = pos;
                component.Offset = offset;
                return component;
            }
            
            protected static T CreateBase<T>(UiPosition pos) where T : BaseUiComponent, new()
            {
                T component = UiFrameworkPool.Get<T>();
                component.Position = pos.ToPosition();
                return component;
            }
            
            public void WriteRootComponent(JsonFrameworkWriter writer, bool needsMouse, bool needsKeyboard)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentName, Name);
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ParentName, Parent);
                JsonCreator.AddField(writer, JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
                
                writer.WritePropertyName("components");
                writer.WriteStartArray();
                WriteComponents(writer);
                
                if (needsMouse)
                {
                    JsonCreator.AddMouse(writer);
                }
                
                if (needsKeyboard)
                {
                    JsonCreator.AddKeyboard(writer);
                }
                
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            
            public void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentName, Name);
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ParentName, Parent);
                JsonCreator.AddField(writer, JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
                
                writer.WritePropertyName("components");
                writer.WriteStartArray();
                WriteComponents(writer);
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            
            protected virtual void WriteComponents(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
                JsonCreator.AddField(writer, JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.Position.AnchorMin);
                JsonCreator.AddField(writer, JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.Position.AnchorMax);
                
                if (Offset.HasValue)
                {
                    Offset offset = Offset.Value;
                    JsonCreator.AddField(writer, JsonDefaults.Offset.OffsetMinName, offset.Min, JsonDefaults.Offset.OffsetMin);
                    JsonCreator.AddField(writer, JsonDefaults.Offset.OffsetMaxName, offset.Max, JsonDefaults.Offset.OffsetMax);
                }
                else
                {
                    //Fixes issue with UI going outside of bounds
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Offset.OffsetMaxName, JsonDefaults.Offset.DefaultOffsetMax);
                }
                
                writer.WriteEndObject();
            }
            
            public void SetFadeOut(float duration)
            {
                FadeOut = duration;
            }
            
            public abstract void SetFadeIn(float duration);
            
            protected override void EnterPool()
            {
                Name = null;
                Parent = null;
                FadeOut = 0;
                Position = default(Position);
                Offset = null;
            }
        }
        #endregion

        #region UiElements\BaseUiTextOutline.cs
        public abstract class BaseUiTextOutline : BaseUiComponent
        {
            public OutlineComponent Outline;
            
            public void AddTextOutline(UiColor color)
            {
                Outline = UiFrameworkPool.Get<OutlineComponent>();
                Outline.Color = color;
            }
            
            public void AddTextOutline(UiColor color, Vector2 distance)
            {
                AddTextOutline(color);
                Outline.Distance = distance;
            }
            
            public void AddTextOutline(UiColor color, Vector2 distance, bool useGraphicAlpha)
            {
                AddTextOutline(color, distance);
                Outline.UseGraphicAlpha = useGraphicAlpha;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Outline?.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                if (Outline != null)
                {
                    UiFrameworkPool.Free(ref Outline);
                }
            }
        }
        #endregion

        #region UiElements\UiButton.cs
        public class UiButton : BaseUiComponent
        {
            public ButtonComponent Button;
            
            public static UiButton CreateCommand(UiPosition pos, UiColor color, string command)
            {
                UiButton button = CreateBase<UiButton>(pos);
                button.Button.Color = color;
                button.Button.Command = command;
                return button;
            }
            
            public static UiButton CreateClose(UiPosition pos, UiColor color, string close)
            {
                UiButton button = CreateBase<UiButton>(pos);
                button.Button.Color = color;
                button.Button.Close = close;
                return button;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Button.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Button);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Button = UiFrameworkPool.Get<ButtonComponent>();
            }
            
            public override void SetFadeIn(float duration)
            {
                Button.FadeIn = duration;
            }
        }
        #endregion

        #region UiElements\UiImage.cs
        public class UiImage : BaseUiComponent
        {
            public ImageComponent Image;
            
            public static UiImage Create(string png, UiPosition pos, UiColor color)
            {
                UiImage image = CreateBase<UiImage>(pos);
                image.Image.Color = color;
                image.Image.Png = png;
                return image;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Image.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Image);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Image = UiFrameworkPool.Get<ImageComponent>();
            }
            
            public override void SetFadeIn(float duration)
            {
                Image.FadeIn = duration;
            }
        }
        #endregion

        #region UiElements\UiInput.cs
        public class UiInput : BaseUiTextOutline
        {
            public InputComponent Input;
            
            public static UiInput Create(string text, int size, UiColor textColor, UiPosition pos, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
            {
                UiInput input = CreateBase<UiInput>(pos);
                InputComponent comp = input.Input;
                comp.Text = text;
                comp.FontSize = size;
                comp.Color = textColor;
                comp.Align = align;
                comp.Font = font;
                comp.Command = cmd;
                comp.CharsLimit = charsLimit;
                comp.IsPassword = isPassword;
                comp.IsReadyOnly = readOnly;
                comp.LineType = lineType;
                return input;
            }
            
            public void SetTextAlign(TextAnchor align)
            {
                Input.Align = align;
            }
            
            public void SetCharsLimit(int limit)
            {
                Input.CharsLimit = limit;
            }
            
            public void SetIsPassword(bool isPassword)
            {
                Input.IsPassword = isPassword;
            }
            
            public void SetIsReadonly(bool isReadonly)
            {
                Input.IsReadyOnly = isReadonly;
            }
            
            public void SetLineType(InputField.LineType lineType)
            {
                Input.LineType = lineType;
            }
            
            /// <summary>
            /// Sets if the input should block keyboard input when focused.
            /// Default value is true
            /// </summary>
            /// <param name="needsKeyboard"></param>
            public void SetRequiresKeyboard(bool needsKeyboard = true)
            {
                Input.NeedsKeyboard = needsKeyboard;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Input.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Input);
                if (Outline != null)
                {
                    UiFrameworkPool.Free(ref Outline);
                }
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Input = UiFrameworkPool.Get<InputComponent>();
            }
            
            public override void SetFadeIn(float duration)
            {
                Input.FadeIn = duration;
            }
        }
        #endregion

        #region UiElements\UiItemIcon.cs
        public class UiItemIcon : BaseUiComponent
        {
            public ItemIconComponent Icon;
            
            public static UiItemIcon Create(int itemId, UiPosition pos, UiColor color)
            {
                return Create(itemId, 0, pos, color);
            }
            
            public static UiItemIcon Create(int itemId, ulong skinId, UiPosition pos, UiColor color)
            {
                UiItemIcon icon = CreateBase<UiItemIcon>(pos);
                icon.Icon.Color = color;
                icon.Icon.ItemId = itemId;
                icon.Icon.SkinId = skinId;
                return icon;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Icon.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Icon);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Icon = UiFrameworkPool.Get<ItemIconComponent>();
            }
            
            public override void SetFadeIn(float duration)
            {
                Icon.FadeIn = duration;
            }
        }
        #endregion

        #region UiElements\UiLabel.cs
        public class UiLabel : BaseUiTextOutline
        {
            public TextComponent Text;
            public CountdownComponent Countdown;
            
            public static UiLabel Create(string text, int size, UiColor color, UiPosition pos, string font, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabel label = CreateBase<UiLabel>(pos);
                TextComponent textComp = label.Text;
                textComp.Text = text;
                textComp.FontSize = size;
                textComp.Color = color;
                textComp.Align = align;
                textComp.Font = font;
                return label;
            }
            
            public void AddCountdown(int startTime, int endTime, int step, string command)
            {
                Countdown = UiFrameworkPool.Get<CountdownComponent>();
                Countdown.StartTime = startTime;
                Countdown.EndTime = endTime;
                Countdown.Step = step;
                Countdown.Command = command;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Text.WriteComponent(writer);
                Countdown?.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Text);
                
                if (Countdown != null)
                {
                    UiFrameworkPool.Free(ref Countdown);
                }
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Text = UiFrameworkPool.Get<TextComponent>();
            }
            
            public override void SetFadeIn(float duration)
            {
                Text.FadeIn = duration;
            }
        }
        #endregion

        #region UiElements\UiPanel.cs
        public class UiPanel : BaseUiComponent
        {
            public ImageComponent Image;
            
            public void AddSprite(string sprite)
            {
                Image.Sprite = sprite;
            }
            
            public void AddMaterial(string material)
            {
                Image.Material = material;
            }
            
            public static UiPanel Create(UiPosition pos, UiOffset offset, UiColor color)
            {
                UiPanel panel = CreateBase<UiPanel>(pos, offset);
                panel.Image.Color = color;
                return panel;
            }
            
            public static UiPanel Create(Position pos, Offset offset, UiColor color)
            {
                UiPanel panel = CreateBase<UiPanel>(pos, offset);
                panel.Image.Color = color;
                return panel;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Image.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Image);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Image = UiFrameworkPool.Get<ImageComponent>();
            }
            
            public override void SetFadeIn(float duration)
            {
                Image.FadeIn = duration;
            }
        }
        #endregion

        #region UiElements\UiWebImage.cs
        public class UiWebImage : BaseUiComponent
        {
            public RawImageComponent RawImage;
            
            public static UiWebImage Create(string png, UiPosition pos, UiColor color)
            {
                UiWebImage image = CreateBase<UiWebImage>(pos);
                image.RawImage.Color = color;
                image.RawImage.Url = png;
                
                return image;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                RawImage.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref RawImage);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                RawImage = UiFrameworkPool.Get<RawImageComponent>();
            }
            
            public override void SetFadeIn(float duration)
            {
                RawImage.FadeIn = duration;
            }
        }
        #endregion

    }

}
