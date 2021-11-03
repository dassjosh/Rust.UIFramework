#define UiDebug
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Facepunch;
using Network;
using Newtonsoft.Json;
using Oxide.Plugins;
using UnityEngine;

namespace SharedPlugins.UiFramework
{
    /// <summary>
    /// Plugin class
    /// </summary>
    public partial class UiFramework : RustPlugin
    {
        private static UiFramework _ins;
    }
    
    /// <summary>
    /// UI Framework Core
    /// Version 1.1.0 BY MJSU
    /// </summary>
    public partial class UiFramework
    {
        #region Framework Variables
        private const string UiFont = "robotocondensed-regular.ttf";
        #endregion
        
        #region UI Enums
        [Flags]
        public enum BorderMode : byte
        {
            Top = 1,
            Left = 2,
            Bottom = 4,
            Right = 8,
            All = 15
        }

        public enum Layer
        {
            Overall,
            Overlay,
            Hud,
            HudMenu,
            Under,
        }
        #endregion

        #region UI Colors
        public static class UiColors
        {
            public static class BootStrap
            {
                public static readonly UiColor BootStrapBlue = "#007bff";
                public static readonly UiColor BootStrapIndigo = "#6610f2";
                public static readonly UiColor BootStrapPurple = "#6f42c1";
                public static readonly UiColor BootStrapPink = "#e83e8c";
                public static readonly UiColor BootStrapRed = "#dc3545";
                public static readonly UiColor BootStrapOrange = "#fd7e14";
                public static readonly UiColor BootStrapYellow = "#ffc107";
                public static readonly UiColor BootStrapGreen = "#28a745";
                public static readonly UiColor BootStrapTeal = "#20c997";
                public static readonly UiColor BootStrapCyan = "#17a2b8";
                public static readonly UiColor BootStrapWhite = "#fff";
                public static readonly UiColor BootStrapGray = "#6c757d";
                public static readonly UiColor BootStrapDarkGray = "#343a40";
                public static readonly UiColor BootStrapPrimary = "#007bff";
                public static readonly UiColor BootStrapSecondary = "#6c757d";
                public static readonly UiColor BootStrapSuccess = "#28a745";
                public static readonly UiColor BootStrapInfo = "#17a2b8";
                public static readonly UiColor BootStrapWarning = "#ffc107";
                public static readonly UiColor BootStrapDanger = "#dc3545";
                public static readonly UiColor BootStrapLight = "#f8f9fa";
                public static readonly UiColor BootStrapDark = "#343a40";
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
            public static readonly UiColor Clear = StandardColors.Black.WithAlpha(0f);
            public static readonly UiColor White = StandardColors.White;
            public static readonly UiColor Black = StandardColors.Black;
            public static readonly UiColor Body = Form.Body.WithAlpha("B2");
            public static readonly UiColor BodyHeader = Form.Header;
            public static readonly UiColor Text = Form.Text.WithAlpha("80") ;
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

        public class UiColor
        {
            public string Color { get; private set; }
            public int Value { get; private set; }

            /// <summary>
            /// Checks for format "0.0 0.0 0.0 0.0"
            /// Any permutation of normal rust color string will work
            /// </summary>
            private static readonly Regex RustColorFormat = new Regex("\\d*.?\\d* \\d*.?\\d* \\d*.?\\d* \\d*.?\\d*", RegexOptions.Compiled | RegexOptions.ECMAScript);
            
            /// <summary>
            /// Valid Hex Color Formats
            /// #RGB
            /// #RRGGBB
            /// #RGBA
            /// #RRGGBBAA
            /// </summary>
            /// <param name="color"></param>
            public UiColor(string color)
            {
                Color colorValue;
                if (RustColorFormat.IsMatch(color))
                {
                    colorValue = ColorEx.Parse(color);
                }
                else
                {
                    if (!color.StartsWith("#"))
                    {
                        color = "#" + color;
                    }
                    
                    ColorUtility.TryParseHtmlString(color, out colorValue);
                }
                
                SetValue(colorValue);
            }

            public UiColor(Color color)
            {
                SetValue(color);
            }

            public UiColor(string hexColor, int alpha = 255)
            {
                if (!hexColor.StartsWith("#"))
                {
                    hexColor = "#" + hexColor;
                }
                
                alpha = Mathf.Clamp(alpha, 0, 255);
                Color colorValue;
                ColorUtility.TryParseHtmlString(hexColor, out colorValue);
                colorValue.a = alpha / 255f;
                SetValue(colorValue);
            }
            
            public UiColor(string hexColor, float alpha = 1f)
            {
                if (!hexColor.StartsWith("#"))
                {
                    hexColor = "#" + hexColor;
                }
                
                alpha = Mathf.Clamp01(alpha);
                Color colorValue;
                ColorUtility.TryParseHtmlString(hexColor, out colorValue);
                colorValue.a = alpha;
                SetValue(colorValue);
            }

            public UiColor(int red, int green, int blue, int alpha = 255)
            {
                red = Mathf.Clamp(red, 0, 255);
                green = Mathf.Clamp(green, 0, 255);
                blue = Mathf.Clamp(blue, 0, 255);
                alpha = Mathf.Clamp(alpha, 0, 255);
                
                SetValue(red / 255f, green / 255f, blue / 255f, alpha / 255f);
            }

            public UiColor(float red, float green, float blue, float alpha = 1f)
            {
                red = Mathf.Clamp01(red);
                green = Mathf.Clamp01(green);
                blue = Mathf.Clamp01(blue);
                alpha = Mathf.Clamp01(alpha);
                
                SetValue(red, green, blue, alpha);
            }

            public UiColor WithAlpha(string hex)
            {
                return WithAlpha(int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
            }
            
            public UiColor WithAlpha(int alpha)
            {
                return WithAlpha(Mathf.Clamp(alpha, 0, 255) / 255f);
            }

            public UiColor WithAlpha(float alpha)
            {
                Color color = ColorEx.Parse(Color);
                color.a = Mathf.Clamp01(alpha);
                return new UiColor(color);
            }

            private void SetValue(Color color)
            {
                SetValue(color.r, color.g, color.b, color.a);
            }
            
            private void SetValue(float red, float green, float blue, float alpha)
            {
                Color = $"{red:0.###} {green:0.###} {blue:0.###} {alpha:0.###}";
                Value = ((int)(red * 255) << 24) + ((int)(green * 255) << 16) + ((int)(blue * 255) << 8) + (int)(alpha * 255);
            }
            
            public static implicit operator UiColor(string value) => new UiColor(value);
        }
        #endregion

        #region UI Builder
        public class UiBuilder
        {
            private List<BaseUiComponent> _components = Pool.GetList<BaseUiComponent>();
            public BaseUiComponent Root;
            private string _cachedJson;
            
            #region Constructor
            public UiBuilder(UiColor color, UiPosition pos, UiOffset offset, bool useCursor, string name, string parent)
            {
                UiPanel panel = UiPanel.Create(pos, offset, color);
                panel.NeedsCursor = useCursor;
                SetRoot(panel, name, parent);
            }

            public UiBuilder(UiColor color, UiPosition pos, bool useCursor, string name, string parent) : this(color, pos, null, useCursor, name, parent)
            {
                
            }

            public UiBuilder(UiColor color, UiPosition pos, bool useCursor, string name, Layer parent = Layer.Overlay) : this(color, pos, null, useCursor, name, GetLayerValue(parent))
            {
                
            }
            
            public UiBuilder(UiColor color, UiPosition pos, UiOffset offset, bool useCursor, string name, Layer parent = Layer.Overlay) : this(color, pos, offset, useCursor, name, GetLayerValue(parent))
            {
                
            }

            public UiBuilder()
            {
                
            }

            public static string GetLayerValue(Layer layer)
            {
                if (layer == Layer.HudMenu)
                {
                    return "Hud.Menu";
                }

                return layer.ToString();
            }
            
            public void SetRoot(BaseUiComponent component, string name, string parent)
            {
                Root = component;
                component.Parent = parent;
                component.Name = name;
                _components.Add(component);
            }
            #endregion

            #region Decontructor
            ~UiBuilder()
            {
                for (int index = 0; index < _components.Count; index++)
                {
                    BaseUiComponent component = _components[index];
                    Pool.Free(ref component);
                }

                Pool.FreeList(ref _components);
                Root = null;
                _cachedJson = null;
            }
            #endregion

            #region Add UI
            public void AddComponent(BaseUiComponent component, BaseUiComponent parent)
            {
                component.Parent = parent.Name;
                component.Name = GetComponentName();
                _components.Add(component);
            }
            
            public string GetComponentName()
            {
                return string.Concat(Root.Name, "_", _components.Count.ToString());
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
                UiButton button = UiButton.CreateCommand(pos, color , cmd);
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

            public UiImage Image(BaseUiComponent parent, string png, UiPosition pos, UiColor color)
            {
                UiImage image = UiImage.Create(png, pos, color);
                AddComponent(image, parent);
                return image;
            }

            public UiImage Image(BaseUiComponent parent, string png, UiPosition pos)
            {
                return Image(parent, png, pos, UiColors.White);
            }

            public UiLabel Label(BaseUiComponent parent, string text, int size, UiColor color, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabel label = UiLabel.Create(text, size, color, pos, align);
                AddComponent(label, parent);
                return label;
            }
            
            public UiLabel LabelBackground(BaseUiComponent parent, string text, int size, UiColor color, UiColor backgroundColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiPanel panel = Panel(parent, backgroundColor, pos);
                UiLabel label = UiLabel.Create(text, size, color, UiPosition.FullPosition, align);
                AddComponent(label, panel);
                return label;
            }

            public UiInput Input(BaseUiComponent parent, int size, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false)
            {
                parent = Panel(parent, backgroundColor, pos);
                UiInput input = UiInput.Create(size, textColor, pos, cmd, align, charsLimit, isPassword);
                AddComponent(input, parent);
                return input;
            }

            private readonly Position _boarderTop = new Position("0 1", "1 1");
            private readonly Position _boarderBottom = new Position("0 0", "1 0");
            private readonly Position _boarderLeft = new Position("0 0", "0 1");
            private readonly Position _boarderRight = new Position("1 0", "1 1");
            
            public void Border(BaseUiComponent parent, UiColor color, int size = 0, BorderMode border = BorderMode.All)
            {
                string sizeString = size.ToString();
                if (border.HasFlag(BorderMode.Top))
                {
                    UiPanel panel = UiPanel.Create(_boarderTop, new Offset("0 -1", $"0 {sizeString}"), color);
                    AddComponent(panel, parent);
                }

                if (border.HasFlag(BorderMode.Left))
                {
                    UiPanel panel = UiPanel.Create(_boarderLeft, new Offset($"-{sizeString} -{sizeString}", "1 0"), color);
                    AddComponent(panel, parent);
                }

                if (border.HasFlag(BorderMode.Bottom))
                {
                    UiPanel panel = UiPanel.Create(_boarderBottom, new Offset($"0 -{sizeString}", "0 1"), color);
                    AddComponent(panel, parent);
                }

                if (border.HasFlag(BorderMode.Right))
                {
                    UiPanel panel = UiPanel.Create(_boarderRight, new Offset("-1 0", $"{sizeString} 0"), color);
                    AddComponent(panel, parent);
                }
            }
            #endregion
            
            #region JSON
            public StringBuilder ToJson()
            {
                return JsonCreator.CreateJson(_components);
            }

            public void CacheJson()
            {
                _cachedJson = ToJson().ToString();
            }
            #endregion

            #region Add UI
            public void AddUi(BasePlayer player)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(player.Connection), null, "AddUI", ToJson().ToString());
            }

            public void AddUiCached(BasePlayer player)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(player.Connection), null, "AddUI", _cachedJson);
            }
            
            public void AddUi(Connection connection)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, "AddUI", ToJson().ToString());
            }
            
            public void AddUiCached(Connection connection)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, "AddUI", _cachedJson);
            }
            
            public void AddUi(List<Connection> connections)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, "AddUI", ToJson().ToString());
            }
            
            public void AddUiCached(List<Connection> connections)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, "AddUI", _cachedJson);
            }
        
            public void AddUi()
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(Net.sv.connections), null, "AddUI", ToJson().ToString());
            }
            
            public void AddUiCached()
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(Net.sv.connections), null, "AddUI", _cachedJson);
            }
            #endregion

            #region Destroy Ui
            public void DestroyUi(BasePlayer player)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(player.Connection), null, "DestroyUI", Root.Name);
            }
            
            public void DestroyUi(Connection connection)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, "DestroyUI", Root.Name);
            }
            
            public void DestroyUi(List<Connection> connections)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, "DestroyUI", Root.Name);
            }
            
            public void DestroyUi()
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(Net.sv.connections), null, "DestroyUI", Root.Name);
            }

            #endregion
        }
        #endregion

        #region JSON Creator
        public static class JsonCreator
        {
            //Position & Offset
            private static string _rectTransformName = "RectTransform";
            private static string _anchorMinName = "anchormin";
            private static string _anchorMaxName = "anchormax";
            private static string _offsetMinName = "offsetmin";
            private static string _offsetMaxName = "offsetmax";
            private const string DefaultMin = "0.0 0.0";
            private static readonly string[] DefaultMinValues = {DefaultMin, "0 0"};
            private const string DefaultMax = "1.0 1.0";
            private static readonly string[] DefaultMaxValues = {DefaultMax, "1 1"};
            private static string _offsetMaxValue = "0 0";
            
            //Text
            private static string _alignValue = "UpperLeft";
            private static int _fontSizeValue = 14;
            private static string _fontValue = "RobotoCondensed-Bold.ttf";
            private static string _fontName = "font";
            private static string _textName = "text";
            private static string _textValue = "Text";
            private static string _fontSizeName = "fontSize";
            private static string _alignName = "align";
            
            //Material & Sprite
            private static string _spriteName = "sprite";
            private static string _materialName = "material";
            private static string _spriteValue = "Assets/Content/UI/UI.Background.Tile.psd";
            private static string _spriteImageValue = "Assets/Icons/rust.png";
            private static string _materialValue = "Assets/Icons/IconMaterial.mat";
            
            //Common
            private static string _componentTypeName = "type";
            private static string _colorName = "color";
            private static string _nullValue;
            private static string _emptyString = "";
            private static string _componentName = "name";
            private static string _parentName = "parent";
            private static string _fadeoutName = "fadeout";
            private static float _fadeoutValue;
            private static readonly UiColor ColorValue = "1 1 1 1";
            
            //Outline
            private static string _distanceName = "distance";
            private static string _useGraphicAlphaName = "useGraphicAlpha";
            private static string _useGraphicAlphaValue = "True";
            private static string _distanceValue = "1.0 -1.0";
            
            //Button
            private static string _commandName = "command";
            private static string _closeName = "close";
            
            //Needs Cursor
            private static string _needsCursorValue = "NeedsCursor";

            //Image
            private static string _pngName = "png";
            private static string _urlName = "url";
            
            //Input
            private static string _characterLimitName = "characterLimit";
            private static int _characterLimitValue;
            private static string _passwordName = "password";
            private static string _passwordValue = "true";


            public static StringBuilder CreateJson(List<BaseUiComponent> components)
            {
                StringWriter sw = new StringWriter();
                JsonTextWriter writer = new JsonTextWriter(sw);

                writer.WriteStartArray();

                for (int index = 0; index < components.Count; index++)
                {
                    BaseUiComponent component = components[index];
                    writer.WriteStartObject();
                    AddFieldRaw(writer, ref _componentName, ref component.Name);
                    AddFieldRaw(writer, ref _parentName, ref component.Parent);
                    AddField(writer, ref _fadeoutName, ref component.FadeOut, ref _fadeoutValue);

                    writer.WritePropertyName("components");
                    writer.WriteStartArray();
                    component.WriteComponents(writer);
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();

                return sw.GetStringBuilder();
            }

            public static void AddFieldRaw(JsonTextWriter writer, ref string name, ref string value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }

            public static void AddField(JsonTextWriter writer, ref string name, ref string value, ref string defaultValue)
            {
                if (value != null && value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }

            public static void AddMultiField(JsonTextWriter writer, ref string name, ref string value, string[] defaultValues)
            {
                if (value != null && !defaultValues.Contains(value))
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }

            public static void AddField(JsonTextWriter writer, ref string name, ref int value, ref int defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                } 
            }

            public static void AddField(JsonTextWriter writer, ref string name, ref float value, ref float defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonTextWriter writer, ref string name, UiColor value, UiColor defaultValue)
            {
                if (value.Value != defaultValue.Value)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value.Color);
                }
            }

            public static void Add(JsonTextWriter writer, ref Position position, ref Offset? offset)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref _rectTransformName);
                AddMultiField(writer, ref _anchorMinName, ref position.Min, DefaultMinValues);
                AddMultiField(writer, ref _anchorMaxName, ref position.Max, DefaultMaxValues);

                if (offset.HasValue)
                {
                    string min = offset.Value.Min;
                    string max = offset.Value.Max;
                    AddMultiField(writer, ref _offsetMinName, ref min, DefaultMinValues);
                    AddMultiField(writer, ref _offsetMaxName, ref max, DefaultMaxValues);
                }
                else
                {
                    //Fixes issue with UI going outside of bounds
                    AddFieldRaw(writer, ref _offsetMaxName, ref _offsetMaxValue);
                }

                writer.WriteEndObject();
            }

            public static void Add(JsonTextWriter writer, ButtonComponent button)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref ButtonComponent.Type);
                AddField(writer, ref _commandName, ref button.Command, ref _nullValue);
                AddField(writer, ref _closeName, ref button.Close, ref _nullValue);
                AddField(writer, ref _colorName, button.Color, ColorValue);
                AddField(writer, ref _spriteName, ref button.Sprite, ref _spriteValue);
                writer.WriteEndObject();
            }

            public static void Add(JsonTextWriter writer, TextComponent textComponent)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref TextComponent.Type);
                AddField(writer, ref _textName, ref textComponent.Text, ref _textValue);
                AddField(writer, ref _fontSizeName, ref textComponent.FontSize, ref _fontSizeValue);
                AddField(writer, ref _fontName, ref textComponent.Font, ref _fontValue);
                AddField(writer, ref _colorName, textComponent.Color, ColorValue);
                string align = textComponent.Align.ToString();
                AddField(writer, ref _alignName, ref align, ref _alignValue);
                writer.WriteEndObject();
            }

            public static void Add(JsonTextWriter writer, RawImageComponent image)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref RawImageComponent.Type);
                AddField(writer, ref _colorName, image.Color, ColorValue);
                AddField(writer, ref _spriteName, ref image.Sprite, ref _spriteImageValue);
                if (!string.IsNullOrEmpty(image.Png))
                {
                    AddField(writer, ref _pngName, ref image.Png, ref _emptyString);
                }

                if (!string.IsNullOrEmpty(image.Url))
                {
                    AddField(writer, ref _urlName, ref image.Url, ref _emptyString);
                }

                writer.WriteEndObject();
            }

            public static void Add(JsonTextWriter writer, ImageComponent image)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref ImageComponent.Type);
                AddField(writer, ref _colorName, image.Color, ColorValue);
                AddField(writer, ref _spriteName, ref image.Sprite, ref _spriteValue);
                AddField(writer, ref _materialName, ref image.Material, ref _materialValue);
                writer.WriteEndObject();
            }

            public static void AddCursor(JsonTextWriter writer)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref _needsCursorValue);
                writer.WriteEndObject();
            }

            public static void Add(JsonTextWriter writer, OutlineComponent outline)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref OutlineComponent.Type);
                AddField(writer, ref _colorName, outline.Color, ColorValue);
                AddField(writer, ref _distanceName, ref outline.Distance, ref _distanceValue);
                if (outline.UseGraphicAlpha)
                {
                    AddFieldRaw(writer, ref _useGraphicAlphaName, ref _useGraphicAlphaValue);
                }

                writer.WriteEndObject();
            }

            public static void Add(JsonTextWriter writer, InputComponent input)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, ref _componentTypeName, ref InputComponent.Type);
                AddField(writer, ref _fontSizeName, ref input.FontSize, ref _fontSizeValue);
                AddField(writer, ref _fontName, ref input.Font, ref _fontValue);
                string align = input.Align.ToString();
                AddField(writer, ref _alignName, ref align, ref _alignValue);
                AddField(writer, ref _colorName, input.Color, ColorValue);
                AddField(writer, ref _characterLimitName, ref input.CharsLimit, ref _characterLimitValue);
                AddField(writer, ref _commandName, ref input.Command, ref _nullValue);

                if (input.IsPassword)
                {
                    AddFieldRaw(writer, ref _passwordName, ref _passwordValue);
                }

                writer.WriteEndObject();
            }
        }
        #endregion

        #region UI Position
        public struct Position
        {
            public string Min;
            public string Max;
            
            private const string PosFormat = "0.####";

            public Position(float xMin, float yMin, float xMax, float yMax)
            {
                Min = string.Concat(xMin.ToString(PosFormat), " ", yMin.ToString(PosFormat));
                Max = string.Concat(xMax.ToString(PosFormat), " ", yMax.ToString(PosFormat));
            }

            public Position(string min, string max)
            {
                Min = min;
                Max = max;
            }

            public override string ToString()
            {
                return string.Concat(Min, " ", Max);;
            }
        }
        
        public struct Offset
        {
            public string Min;
            public string Max;

            public Offset(int xMin, int yMin, int xMax, int yMax)
            {
                Min = string.Concat(xMin.ToString(), " ", yMin.ToString());
                Max = string.Concat(xMax.ToString(), " ", yMax.ToString());
            }
            
            public Offset(string min, string max)
            {
                Min = min;
                Max = max;
            }
            
            public override string ToString()
            {
                return string.Concat(Min, " ", Max);;
            }
        }
        
        public abstract class UiPosition
        {
            public static readonly UiPosition FullPosition = new StaticUiPosition(0, 0, 1, 1);
            public static readonly UiPosition Center = new StaticUiPosition(.5f, .5f, .5f, .5f);

            public abstract Position ToPosition();
        }

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

        public class MovablePosition : UiPosition
        {
            public float XMin;
            public float YMin;
            public float XMax;
            public float YMax;
            
            protected MovablePosition() {}
            
            public MovablePosition(float xMin, float yMin, float xMax, float yMax)
            {
                XMin = xMin;
                YMin = yMin;
                XMax = xMax;
                YMax = yMax;
#if UiDebug
                ValidatePositions();
#endif
            }

            public override Position ToPosition()
            {
                return new Position(XMin, YMin, XMax, YMax);
            }

            public void SetX(float xPos, float xMax)
            {
                XMin = xPos;
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

            public MovablePosition CopyX(float yPos, float yMax)
            {
                return new MovablePosition(XMin, yPos, XMax, yMax);
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

            public MovablePosition CopyY(float xPos, float yMax)
            {
                return new MovablePosition(xPos, YMin, yMax, YMax);
            }

            public MovablePosition Clone()
            {
                return new MovablePosition(XMin, YMin, XMax, YMax);
            }
            
            public StaticUiPosition ToStatic()
            {
                return new StaticUiPosition(XMin, YMin, XMax, YMax);
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

        public class GridPosition : MovablePosition
        {
            public float NumCols { get; }
            public float NumRows { get; }
            private int _xSize = 1;
            private int _xOffset;
            private int _ySize = 1;
            private int _yOffset;
            private float _xPad;
            private float _yPad;

            public GridPosition(int size) : this( size, size)
            {

            }

            public GridPosition(int numCols, int numRows)
            {
                NumCols = numCols;
                NumRows = numRows;
            }

            public GridPosition SetRowLength(int size, int offset = 0)
            {
                _xSize = size;
                _xOffset = offset;
                return this;
            }

            public new GridPosition SetX(float xMin, float xMax)
            {
                XMin = xMin;
                XMax = xMax;
                return this;
            }

            public GridPosition SetColLength(int size, int offset = 0)
            {
                _ySize = size;
                _yOffset = offset;
                return this;
            }

            public new GridPosition SetY(float yMin, float yMax)
            {
                YMin = yMin;
                YMax = yMax;
                return this;
            }

            public GridPosition SetPadding(float padding)
            {
                _xPad = padding;
                _yPad = padding;
                return this;
            }
            
            public GridPosition SetPadding(float xPad, float yPad)
            {
                _xPad = xPad;
                _yPad = yPad;
                return this;
            }

            public GridPosition SetRowPadding(float padding)
            {
                _xPad = padding;
                return this;
            }

            public GridPosition SetColPadding(float padding)
            {
                _yPad = padding;
                return this;
            }

            public void MoveCols(int cols)
            {
                XMin += cols / NumCols;
                XMax += cols / NumCols;

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

            public GridPosition Build()
            {
                if (_xSize != 0)
                {
                    float size = _xSize / NumCols;
                    XMax += size;
                }

                if (_xOffset != 0)
                {
                    float size = _xOffset / NumCols;
                    XMin += size;
                    XMax += size;
                }

                if (_ySize != 0)
                {
                    float size = _ySize / NumRows;
                    YMax += size;
                }

                if (_yOffset != 0)
                {
                    float size = _yOffset / NumRows;
                    YMin += size;
                    YMax += size;
                }

                XMin += _xPad;
                XMax -= _xPad;
                float yMin = YMin; //Need to save yMin before we overwrite it
                YMin = 1 - YMax + _yPad;
                YMax = 1 - yMin - _yPad;

#if UiDebug
                ValidatePositions();
#endif

                return this;
            }

            public new GridPosition Clone()
            {
                return (GridPosition) MemberwiseClone();
            }
        }

        public abstract class UiOffset
        {
            public static readonly UiOffset DefaultOffset = new StaticUiOffset(0, 0, 0, 0);
            public static readonly Offset Default = new Offset(0, 0, 0, 0);
            
            public abstract Offset ToOffset();
        }

        public class StaticUiOffset : UiOffset
        {
            private readonly Offset _offset;
            
            public StaticUiOffset(int width, int height) : this(-width / 2, -height / 2, width / 2, height / 2)
            {
                
            }
            
            public StaticUiOffset(int x, int y, int width, int height)
            {
                _offset = new Offset(x, y, x + width, y + height);
            }

            public override Offset ToOffset()
            {
                return _offset;
            }
        }
        
        public class MovableUiOffset : UiOffset
        {
            public int XMin;
            public int YMin;
            public int XMax;
            public int YMax;

            public MovableUiOffset(int x, int y, int width, int height)
            {
                XMin = x;
                YMin = y;
                XMax = x + width;
                YMax = y + height;
            }

            public override Offset ToOffset()
            {
                return new Offset(XMin, YMin, XMax, YMax);
            }
        }
        #endregion

        #region UI Component Classes
        public class BaseUiComponent : Pool.IPooled
        {
            public string Name;
            public string Parent;
            public float FadeOut;
            public Position Position;
            public Offset? Offset;

            protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
            {
                T component = Pool.Get<T>();
                component.Position = pos.ToPosition();
                component.Offset = offset?.ToOffset();
                return component;
            }
            
            protected static T CreateBase<T>(Position pos, Offset? offset) where T : BaseUiComponent, new()
            {
                T component = Pool.Get<T>();
                component.Position = pos;
                component.Offset = offset;
                return component;
            }
            
            protected static T CreateBase<T>(UiPosition pos) where T : BaseUiComponent, new()
            {
                T component = Pool.Get<T>();
                component.Position = pos.ToPosition();
                return component;
            }

            public virtual void WriteComponents(JsonTextWriter writer)
            {
                JsonCreator.Add(writer, ref Position, ref Offset);
            }

            public void SetFadeout(float duration)
            {
                FadeOut = duration;
            }

            public virtual void EnterPool()
            {
                Name = null;
                Parent = null;
                FadeOut = 0;
                Position = default(Position);
                Offset = null;
            }

            public virtual void LeavePool()
            {
                
            }
        }

        public class UiButton : BaseUiComponent
        {
            public ButtonComponent Button = Pool.Get<ButtonComponent>();

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
            
            public override void WriteComponents(JsonTextWriter writer)
            {
                JsonCreator.Add(writer, Button);
                base.WriteComponents(writer);
            }

            public override void EnterPool()
            {
                base.EnterPool();
                Pool.Free(ref Button);
            }

            public override void LeavePool()
            {
                base.LeavePool();
                Button = Pool.Get<ButtonComponent>();
            }
        }

        public class UiPanel : BaseUiComponent
        {
            public bool NeedsCursor;
            public ImageComponent Image = Pool.Get<ImageComponent>();

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
            
            public static UiPanel Create(Position pos, Offset? offset, UiColor color)
            {
                UiPanel panel = CreateBase<UiPanel>(pos, offset);
                panel.Image.Color = color;
                return panel;
            }
            
            public override void WriteComponents(JsonTextWriter writer)
            {
                JsonCreator.Add(writer, Image);
                base.WriteComponents(writer);

                if (NeedsCursor)
                {
                    JsonCreator.AddCursor(writer);
                }
            }
            
            public override void EnterPool()
            {
                base.EnterPool();
                NeedsCursor = false;
                Pool.Free(ref Image);
            }
            
            public override void LeavePool()
            {
                base.LeavePool();
                Image = Pool.Get<ImageComponent>();
            }
        }

        public class UiImage : BaseUiComponent
        {
            public RawImageComponent RawImage = Pool.Get<RawImageComponent>();

            public static UiImage Create(string png, UiPosition pos, UiColor color)
            {
                UiImage image = CreateBase<UiImage>(pos);
                image.RawImage.Color = color;
                if (png.StartsWith("http"))
                {
                    image.RawImage.Url = png;
                }
                else
                {
                    image.RawImage.Png = png;
                }

                return image;
            }

            public override void WriteComponents(JsonTextWriter writer)
            {
                JsonCreator.Add(writer, RawImage);
                base.WriteComponents(writer);
            }
            
            public override void EnterPool()
            {
                base.EnterPool();
                Pool.Free(ref RawImage);
            }
            
            public override void LeavePool()
            {
                base.LeavePool();
                RawImage = Pool.Get<RawImageComponent>();
            }
        }

        public class UiLabel : BaseUiComponent
        {
            public TextComponent TextComponent = Pool.Get<TextComponent>();
            public OutlineComponent Outline;

            public static UiLabel Create(string text, int size, UiColor color, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabel label = CreateBase<UiLabel>(pos);
                TextComponent textComp = label.TextComponent;
                textComp.Text = text;
                textComp.FontSize = size;
                textComp.Color = color;
                textComp.Align = align;
                textComp.Font = UiFont;
                return label;
            }

            public void AddTextOutline(UiColor color)
            {
                Outline = Pool.Get<OutlineComponent>();
                Outline.Color = color;
            }

            public void AddTextOutline(UiColor color, string distance)
            {
                AddTextOutline(color);
                Outline.Distance = distance;
            }

            public void AddTextOutline(UiColor color, string distance, bool useGraphicAlpha)
            {
                AddTextOutline(color, distance);
                Outline.UseGraphicAlpha = useGraphicAlpha;
            }

            public override void WriteComponents(JsonTextWriter writer)
            {
                JsonCreator.Add(writer, TextComponent);
                if (Outline != null)
                {
                    JsonCreator.Add(writer, Outline);
                }

                base.WriteComponents(writer);
            }
            
            public override void EnterPool()
            {
                base.EnterPool();
                Pool.Free(ref TextComponent);
                if (Outline != null)
                {
                    Pool.Free(ref Outline);
                }
            }
            
            public override void LeavePool()
            {
                base.LeavePool();
                TextComponent = Pool.Get<TextComponent>();
            }
        }

        public class UiInput : BaseUiComponent
        {
            public InputComponent Input = Pool.Get<InputComponent>();

            public static UiInput Create(int size, UiColor textColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false)
            {
                UiInput input = CreateBase<UiInput>(pos);
                InputComponent inputComp = input.Input;
                inputComp.FontSize = size;
                inputComp.Color = textColor;
                inputComp.Align = align;
                inputComp.Font = UiFont;
                inputComp.Command = cmd;
                inputComp.CharsLimit = charsLimit;
                inputComp.IsPassword = isPassword;
                return input;
            }

            public override void WriteComponents(JsonTextWriter writer)
            {
                JsonCreator.Add(writer, Input);
                base.WriteComponents(writer);
            }
            
            public override void EnterPool()
            {
                base.EnterPool();
                Input.Align = TextAnchor.UpperLeft;
                Pool.Free(ref Input);
            }
            
            public override void LeavePool()
            {
                base.LeavePool();
                Input = Pool.Get<InputComponent>();
            }
        }
        
        #region UI Components
        public class BaseComponent : Pool.IPooled
        {
            public UiColor Color;
            
            public virtual void EnterPool()
            {
                Color = null;
            }

            public virtual void LeavePool()
            {
                
            }
        }

        public class BaseTextComponent : BaseComponent
        {
            public int FontSize = 14;
            public string Font;
            public TextAnchor Align;
            
            public override void EnterPool()
            {
                base.EnterPool();
                FontSize = 14;
                Font = null;
                Align = TextAnchor.UpperLeft;
            }
        }
        
        public class ButtonComponent : BaseComponent
        {
            public static string Type = "UnityEngine.UI.Button";
            
            public string Command;
            public string Close;
            public string Sprite;
            public string Material;

            public override void EnterPool()
            {
                base.EnterPool();
                Command = null;
                Close = null;
                Sprite = null;
                Material = null;
            }
        }
        
        public class ImageComponent : BaseComponent
        {
            public static string Type = "UnityEngine.UI.Image";
            
            public string Sprite;
            public string Material;
            public string Png;

            public override void EnterPool()
            {
                base.EnterPool();
                Sprite = null;
                Material = null;
                Png = null;
            }
        }
        
        public class RawImageComponent : BaseComponent
        {
            public static string Type = "UnityEngine.UI.RawImage";

            public string Sprite;
            public string Url;
            public string Png;
            
            public override void EnterPool()
            {
                base.EnterPool();
                Sprite = null;
                Url = null;
                Png = null;
            }
        }
        
        public class TextComponent : BaseTextComponent
        {
            public static string Type = "UnityEngine.UI.Text";

            public string Text;
           
            public override void EnterPool()
            {
                base.EnterPool();
                Text = null;
            }
        }        
        
        public class OutlineComponent : BaseComponent
        {
            public static string Type = "UnityEngine.UI.Outline";
            
            public string Distance;
            public bool UseGraphicAlpha;
            
            public override void EnterPool()
            {
                base.EnterPool();
                Distance = null;
                UseGraphicAlpha = false;
            }
        }
        
        public class InputComponent : BaseTextComponent
        {
            public static string Type = "UnityEngine.UI.InputField";
            
            public int CharsLimit;
            public string Command;
            public bool IsPassword;

            public override void EnterPool()
            {
                base.EnterPool();
                CharsLimit = 0;
                Command = null;
                IsPassword = false;
            }
        }
        #endregion
        #endregion

        #region JSON Sending
        public void DestroyUi(BasePlayer player, string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(player.Connection), null, "DestroyUI", name);
        }
        
        public void DestroyUi(List<Connection> connections, string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, "DestroyUI", name);
        }
        
        private void DestroyUiAll(string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(Net.sv.connections), null, "DestroyUI", name);
        }
        #endregion
    }


    /// <summary>
    /// UI Framework Prevent Movement
    /// Version 1.0.0 BY MJSU
    /// </summary>
    public partial class UiFramework
    {
        private const string ChairPrefab = "assets/prefabs/vehicle/seats/standingdriver.prefab";
        
        private uint _prefabId;
        
        public void PreventMovement(BasePlayer player)
        {
            if (_prefabId == 0)
            {
                _prefabId = StringPool.Get(ChairPrefab);
            }

            BaseMountable currentMount = player.GetMounted();
            if (currentMount != null)
            {
                return;
            }

            BaseVehicleSeat chair = GameManager.server.CreateEntity(ChairPrefab, player.transform.position, player.transform.rotation) as BaseVehicleSeat;
            chair.Spawn();
            player.MountObject(chair);
        }

        public void AllowMovement(BasePlayer player)
        {
            BaseMountable mounted = player.GetMounted();
            if (mounted != null && mounted.prefabID == _prefabId && mounted.parentEntity.uid == 0)
            {
                player.DismountObject();
                mounted.Kill();
            }
        }
    }
}