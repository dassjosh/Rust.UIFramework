using Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

using Color = UnityEngine.Color;
using Net = Network.Net;
using Pool = Facepunch.Pool;

//UiMergeFramework created with PluginMerge v(1.0.0.0) by MJSU
namespace Oxide.Plugins
{
    //Define:Framework
    //[Info("Rust UI Framework", "MJSU", "1.2.0")]
    //[Description("UI Framework for Rust")]
    public partial class UiMergeFramework : RustPlugin
    {
        #region Plugin\UiFramework.Methods.cs
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
        #endregion

        #region Plugin\UiFramework.PreventMovement.cs
        private const string ChairPrefab = "assets/prefabs/vehicle/seats/standingdriver.prefab";
        private const ulong PreventMovementSkinId = ulong.MaxValue - 51234;
        
        private uint _prefabId;
        
        public void InitPreventMovement()
        {
            if (_prefabId == 0)
            {
                _prefabId = StringPool.Get(ChairPrefab);
            }
        }
        
        public void PreventMovement(BasePlayer player)
        {
            InitPreventMovement();
            
            BaseMountable currentMount = player.GetMounted();
            if (currentMount != null)
            {
                return;
            }
            
            Transform transform = player.transform;
            BaseVehicleSeat chair = GameManager.server.CreateEntity(ChairPrefab, transform.position, transform.rotation) as BaseVehicleSeat;
            chair.skinID = PreventMovementSkinId;
            chair.Spawn();
            player.MountObject(chair);
        }
        
        public void AllowMovement(BasePlayer player)
        {
            InitPreventMovement();
            BaseMountable mounted = player.GetMounted();
            if (mounted != null && mounted.prefabID == _prefabId && mounted.parentEntity.uid == 0 && mounted.skinID == PreventMovementSkinId)
            {
                player.DismountObject();
                mounted.Kill();
            }
        }
        #endregion

    }

    #region UiConstants.cs
    public class UiConstants
    {
        public static class UiFonts
        {
            private const string DroidSansMono = "droidsansmono.ttf";
            private const string PermanentMarker = "permanentmarker.ttf";
            private const string RobotoCondensedBold = "robotocondensed-bold.ttf";
            private const string RobotoCondensedRegular = "robotocondensed-regular.ttf";
            
            private static readonly Hash<UiFont, string> _fonts = new Hash<UiFont, string>
            {
                [UiFont.DroidSansMono] = DroidSansMono,
                [UiFont.PermanentMarker] = PermanentMarker,
                [UiFont.RobotoCondensedBold] = RobotoCondensedBold,
                [UiFont.RobotoCondensedRegular] = RobotoCondensedRegular,
            };
            
            public static string GetUiFont(UiFont font)
            {
                return _fonts[font];
            }
        }
        
        public static class UiLayers
        {
            private const string Overall = "Overall";
            private const string Overlay = "Overlay";
            private const string Hud = "Hud";
            private const string HudMenu = "Hud.Menu";
            private const string Under = "Under";
            
            private static readonly Hash<UiLayer, string> _layers = new Hash<UiLayer, string>
            {
                [UiLayer.Overall] = Overall,
                [UiLayer.Overlay] = Overlay,
                [UiLayer.Hud] = Hud,
                [UiLayer.HudMenu] = HudMenu,
                [UiLayer.Under] = Under,
            };
            
            public static string GetLayer(UiLayer layer)
            {
                return _layers[layer];
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

    #region Builder\UiBuilder.cs
    public partial class UiBuilder : IDisposable
    {
        public BaseUiComponent Root;
        
        private string _cachedJson;
        private bool _disposed;
        
        private List<BaseUiComponent> _components;
        private readonly StringBuilder _sb = new StringBuilder();
        
        private static string _font;
        
        #region Constructor
        static UiBuilder()
        {
            SetFont(UiFont.RobotoCondensedRegular);
        }
        
        public UiBuilder(UiColor color, UiPosition pos, UiOffset offset, bool useCursor, string name, string parent) : this()
        {
            UiPanel panel = UiPanel.Create(pos, offset, color);
            panel.NeedsCursor = useCursor;
            SetRoot(panel, name, parent);
        }
        
        public UiBuilder(UiColor color, UiPosition pos, bool useCursor, string name, string parent) : this(color, pos, null, useCursor, name, parent)
        {
        }
        
        public UiBuilder(UiColor color, UiPosition pos, bool useCursor, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, null, useCursor, name, UiConstants.UiLayers.GetLayer(parent))
        {
        }
        
        public UiBuilder(UiColor color, UiPosition pos, UiOffset offset, bool useCursor, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, offset, useCursor, name, UiConstants.UiLayers.GetLayer(parent))
        {
        }
        
        public UiBuilder(BaseUiComponent root, string name, string parent)
        {
            SetRoot(root, name, parent);
        }
        
        public UiBuilder()
        {
            _components = Pool.GetList<BaseUiComponent>() ?? new List<BaseUiComponent>();
        }
        
        public void EnsureCapacity(int capacity)
        {
            _components.Capacity = capacity;
        }
        
        public void SetRoot(BaseUiComponent component, string name, string parent)
        {
            Root = component;
            component.Parent = parent;
            component.Name = name;
            _components.Add(component);
            _sb.Append(name);
            _sb.Append('_');
        }
        
        public static void SetFont(UiFont font)
        {
            _font = UiConstants.UiFonts.GetUiFont(font);
        }
        #endregion
        
        #region Decontructor
        ~UiBuilder()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            
            _disposed = true;
            
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
            _sb.Length = Root.Name.Length + 1;
            _sb.Insert(Root.Name.Length, _components.Count.ToString());
            //_sb.Append(_components.Count.ToString());
            return _sb.ToString();
            //return string.Concat(Root.Name, "_", _components.Count.ToString());
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
        
        public UiInput Input(BaseUiComponent parent, int size, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false)
        {
            parent = Panel(parent, backgroundColor, pos);
            UiInput input = UiInput.Create(size, textColor, pos, cmd, _font, align, charsLimit, isPassword);
            AddComponent(input, parent);
            return input;
        }
        
        private static readonly Position _boarderTop = new Position("0 1", "1 1");
        private static readonly Position _boarderBottom = new Position("0 0", "1 0");
        private static readonly Position _boarderLeft = new Position("0 0", "0 1");
        private static readonly Position _boarderRight = new Position("1 0", "1 1");
        
        public void Border(BaseUiComponent parent, UiColor color, int size = 0, BorderMode border = BorderMode.Top | BorderMode.Bottom | BorderMode.Left | BorderMode.Right)
        {
            string sizeString = size.ToString();
            if (HasBorderFlag(border, BorderMode.Top))
            {
                UiPanel panel = UiPanel.Create(_boarderTop, new Offset("0 -1", $"0 {sizeString}"), color);
                AddComponent(panel, parent);
            }
            
            if (HasBorderFlag(border, BorderMode.Left))
            {
                UiPanel panel = UiPanel.Create(_boarderLeft, new Offset($"-{sizeString} -{sizeString}", "1 0"), color);
                AddComponent(panel, parent);
            }
            
            if (HasBorderFlag(border, BorderMode.Bottom))
            {
                UiPanel panel = UiPanel.Create(_boarderBottom, new Offset($"0 -{sizeString}", "0 1"), color);
                AddComponent(panel, parent);
            }
            
            if (HasBorderFlag(border, BorderMode.Right))
            {
                UiPanel panel = UiPanel.Create(_boarderRight, new Offset("-1 0", $"{sizeString} 0"), color);
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
            return JsonCreator.CreateJson(_components);
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
                if (component is UiImage)
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
                if (component is UiImage)
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
    public class UiColor
    {
        public string Color;
        public int Value;
        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;
        
        /// <summary>
        /// Checks for format "0.0 0.0 0.0 0.0"
        /// Any permutation of normal rust color string will work
        /// </summary>
        private static readonly Regex _rustColorFormat = new Regex("\\d*.?\\d* \\d*.?\\d* \\d*.?\\d* \\d*.?\\d*", RegexOptions.Compiled | RegexOptions.ECMAScript);
        
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
            if (_rustColorFormat.IsMatch(color))
            {
                colorValue = ColorEx.Parse(color);
            }
            else
            {
                if (!color.StartsWith("#"))
                {
                    color = "#" + color;
                }
                
                #if UiBenchmarks
                colorValue = UnityEngine.Color.black;
                #else
                ColorUtility.TryParseHtmlString(color, out colorValue);
                #endif
            }
            
            SetValue(colorValue);
        }
        
        public UiColor(Color color)
        {
            SetValue(color);
        }
        
        public UiColor(string hexColor, int alpha = 255) : this(hexColor, alpha / 255f)
        {
            
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
        
        public UiColor(int red, int green, int blue, int alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
        {
            
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
            return WithAlpha(alpha / 255f);
        }
        
        public UiColor WithAlpha(float alpha)
        {
            return new UiColor(Red, Green, Blue, Mathf.Clamp01(alpha));
        }
        
        private void SetValue(Color color)
        {
            SetValue(color.r, color.g, color.b, color.a);
        }
        
        private void SetValue(float red, float green, float blue, float alpha)
        {
            Color = $"{red:0.###} {green:0.###} {blue:0.###} {alpha:0.###}";
            Value = ((int)(red * 255) << 24) + ((int)(green * 255) << 16) + ((int)(blue * 255) << 8) + (int)(alpha * 255);
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
        
        public static implicit operator UiColor(string value) => new UiColor(value);
        
        public static UiColor Lerp(UiColor start, UiColor end, float value)
        {
            value = Mathf.Clamp01(value);
            return new UiColor(start.Red + (end.Red - start.Red) * value, start.Green + (end.Green - start.Green) * value, start.Blue + (end.Blue - start.Blue) * value, start.Alpha + (end.Alpha - start.Alpha) * value);
        }
    }
    #endregion

    #region Colors\UiColors.cs
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
        public static readonly UiColor Text = Form.Text.WithAlpha("80");
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

    #region Components\BaseComponent.cs
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
    #endregion

    #region Components\BaseTextComponent.cs
    public class BaseTextComponent : FadeInComponent
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
    #endregion

    #region Components\ButtonComponent.cs
    public class ButtonComponent : FadeInComponent
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
    #endregion

    #region Components\FadeInComponent.cs
    public class FadeInComponent : BaseComponent
    {
        public float FadeIn;
        
        public override void EnterPool()
        {
            base.EnterPool();
            FadeIn = 0;
        }
    }
    #endregion

    #region Components\ImageComponent.cs
    public class ImageComponent : FadeInComponent
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
    #endregion

    #region Components\InputComponent.cs
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

    #region Components\OutlineComponent.cs
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
    #endregion

    #region Components\RawImageComponent.cs
    public class RawImageComponent : FadeInComponent
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
    #endregion

    #region Components\TextComponent.cs
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
    public enum UiLayer
    {
        Overall,
        Overlay,
        Hud,
        HudMenu,
        Under,
    }
    #endregion

    #region Json\JsonCreator.cs
    public static class JsonCreator
    {
        private static readonly StringBuilder _sb = new StringBuilder();
        
        public static string CreateJson(List<BaseUiComponent> components)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);
            
            writer.WriteStartArray();
            
            for (int index = 0; index < components.Count; index++)
            {
                BaseUiComponent component = components[index];
                writer.WriteStartObject();
                AddFieldRaw(writer, JsonDefaults.ComponentName, component.Name);
                AddFieldRaw(writer, JsonDefaults.ParentName, component.Parent);
                AddField(writer, JsonDefaults.FadeOutName, component.FadeOut, JsonDefaults.FadeOutValue);
                
                writer.WritePropertyName("components");
                writer.WriteStartArray();
                component.WriteComponents(writer);
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            
            writer.WriteEndArray();
            
            return sw.ToString();
        }
        
        public static void AddFieldRaw(JsonTextWriter writer, string name, string value)
        {
            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }
        
        public static void AddField(JsonTextWriter writer, string name, string value, string defaultValue)
        {
            if (value != null && value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }
        
        public static void AddTextField(JsonTextWriter writer, string name, string value, string defaultValue)
        {
            if (value != null && value != defaultValue)
            {
                writer.WritePropertyName(name);
                _sb.Clear();
                _sb.Append(UiConstants.Json.QuoteChar);
                _sb.Append(value);
                _sb.Append(UiConstants.Json.QuoteChar);
                writer.WriteRawValue(_sb.ToString());
            }
        }
        
        public static void AddMultiField(JsonTextWriter writer, string name, string value, string[] defaultValues)
        {
            if (value != null && !defaultValues.Contains(value))
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }
        
        public static void AddField(JsonTextWriter writer, string name, int value, int defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }
        
        public static void AddField(JsonTextWriter writer, string name, float value, float defaultValue)
        {
            if (value != defaultValue)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }
        
        public static void AddField(JsonTextWriter writer, string name, UiColor value, UiColor defaultValue)
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
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, JsonDefaults.RectTransformName);
            AddMultiField(writer, JsonDefaults.AnchorMinName, position.Min, JsonDefaults.DefaultMinValues);
            AddMultiField(writer, JsonDefaults.AnchorMaxName, position.Max, JsonDefaults.DefaultMaxValues);
            
            if (offset.HasValue)
            {
                string min = offset.Value.Min;
                string max = offset.Value.Max;
                AddMultiField(writer, JsonDefaults.OffsetMinName, min, JsonDefaults.DefaultMinValues);
                AddMultiField(writer, JsonDefaults.OffsetMaxName, max, JsonDefaults.DefaultMaxValues);
            }
            else
            {
                //Fixes issue with UI going outside of bounds
                AddFieldRaw(writer, JsonDefaults.OffsetMaxName, JsonDefaults.OffsetMaxValue);
            }
            
            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, ButtonComponent button)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, ButtonComponent.Type);
            AddField(writer, JsonDefaults.CommandName, button.Command, JsonDefaults.NullValue);
            AddField(writer, JsonDefaults.CloseName, button.Close, JsonDefaults.NullValue);
            AddField(writer, JsonDefaults.ColorName, button.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, button.Sprite, JsonDefaults.SpriteValue);
            AddField(writer, JsonDefaults.FadeInName, button.FadeIn, JsonDefaults.FadeOutValue);
            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, TextComponent text)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, TextComponent.Type);
            AddTextField(writer, JsonDefaults.TextName, text.Text, JsonDefaults.TextValue);
            AddField(writer, JsonDefaults.FontSizeName, text.FontSize, JsonDefaults.FontSizeValue);
            AddField(writer, JsonDefaults.FontName, text.Font, JsonDefaults.FontValue);
            AddField(writer, JsonDefaults.ColorName, text.Color, JsonDefaults.ColorValue);
            string align = text.Align.ToString();
            AddField(writer, JsonDefaults.AlignName, align, JsonDefaults.AlignValue);
            AddField(writer, JsonDefaults.FadeInName, text.FadeIn, JsonDefaults.FadeOutValue);
            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, RawImageComponent image)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, RawImageComponent.Type);
            AddField(writer, JsonDefaults.ColorName, image.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, image.Sprite, JsonDefaults.SpriteImageValue);
            AddField(writer, JsonDefaults.FadeInName, image.FadeIn, JsonDefaults.FadeOutValue);
            if (!string.IsNullOrEmpty(image.Png))
            {
                AddField(writer, JsonDefaults.PNGName, image.Png, JsonDefaults.EmptyString);
            }
            
            if (!string.IsNullOrEmpty(image.Url))
            {
                AddField(writer, JsonDefaults.URLName, image.Url, JsonDefaults.EmptyString);
            }
            
            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, ImageComponent image)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, ImageComponent.Type);
            AddField(writer, JsonDefaults.ColorName, image.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.SpriteName, image.Sprite, JsonDefaults.SpriteValue);
            AddField(writer, JsonDefaults.MaterialName, image.Material, JsonDefaults.MaterialValue);
            AddField(writer, JsonDefaults.FadeInName, image.FadeIn, JsonDefaults.FadeOutValue);
            writer.WriteEndObject();
        }
        
        public static void AddCursor(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, JsonDefaults.NeedsCursorValue);
            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, OutlineComponent outline)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, OutlineComponent.Type);
            AddField(writer, JsonDefaults.ColorName, outline.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.DistanceName, outline.Distance, JsonDefaults.DistanceValue);
            if (outline.UseGraphicAlpha)
            {
                AddFieldRaw(writer, JsonDefaults.UseGraphicAlphaName, JsonDefaults.UseGraphicAlphaValue);
            }
            
            writer.WriteEndObject();
        }
        
        public static void Add(JsonTextWriter writer, InputComponent input)
        {
            writer.WriteStartObject();
            AddFieldRaw(writer, JsonDefaults.ComponentTypeName, InputComponent.Type);
            AddField(writer, JsonDefaults.FontSizeName, input.FontSize, JsonDefaults.FontSizeValue);
            AddField(writer, JsonDefaults.FontName, input.Font, JsonDefaults.FontValue);
            string align = input.Align.ToString();
            AddField(writer, JsonDefaults.AlignName, align, JsonDefaults.AlignValue);
            AddField(writer, JsonDefaults.ColorName, input.Color, JsonDefaults.ColorValue);
            AddField(writer, JsonDefaults.CharacterLimitName, input.CharsLimit, JsonDefaults.CharacterLimitValue);
            AddField(writer, JsonDefaults.CommandName, input.Command, JsonDefaults.NullValue);
            AddField(writer, JsonDefaults.FadeInName, input.FadeIn, JsonDefaults.FadeOutValue);
            
            if (input.IsPassword)
            {
                AddFieldRaw(writer, JsonDefaults.PasswordName, JsonDefaults.PasswordValue);
            }
            
            writer.WriteEndObject();
        }
    }
    #endregion

    #region Json\JsonDefaults.cs
    public class JsonDefaults
    {
        //Position & Offset
        private const string DefaultMin = "0.0 0.0";
        private const string DefaultMax = "1.0 1.0";
        public const string RectTransformName = "RectTransform";
        public const string AnchorMinName = "anchormin";
        public const string AnchorMaxName = "anchormax";
        public const string OffsetMinName = "offsetmin";
        public const string OffsetMaxName = "offsetmax";
        public static readonly string[] DefaultMinValues = { DefaultMin, "0 0" };
        public static readonly string[] DefaultMaxValues = { DefaultMax, "1 1" };
        public const string OffsetMaxValue = "0 0";
        
        //Text
        public const string AlignValue = "UpperLeft";
        public const int FontSizeValue = 14;
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        public const string FontName = "font";
        public const string TextName = "text";
        public const string TextValue = "Text";
        public const string FontSizeName = "fontSize";
        public const string AlignName = "align";
        
        //Material & Sprite
        public const string SpriteName = "sprite";
        public const string MaterialName = "material";
        public const string SpriteValue = "Assets/Content/UI/UI.Background.Tile.psd";
        public const string SpriteImageValue = "Assets/Icons/rust.png";
        public const string MaterialValue = "Assets/Icons/IconMaterial.mat";
        
        //Common
        public const string ComponentTypeName = "type";
        public const string ColorName = "color";
        public const string NullValue = null;
        public const string EmptyString = "";
        public const string ComponentName = "name";
        public const string ParentName = "parent";
        public const string FadeInName = "fadeIn";
        public const string FadeOutName = "fadeOut";
        public const float FadeOutValue = 0;
        public static readonly UiColor ColorValue = "1 1 1 1";
        
        //Outline
        public const string DistanceName = "distance";
        public const string UseGraphicAlphaName = "useGraphicAlpha";
        public const string UseGraphicAlphaValue = "True";
        public const string DistanceValue = "1.0 -1.0";
        
        //Button
        public const string CommandName = "command";
        public const string CloseName = "close";
        
        //Needs Cursor
        public const string NeedsCursorValue = "NeedsCursor";
        
        //Image
        public const string PNGName = "png";
        public const string URLName = "url";
        
        //Input
        public const string CharacterLimitName = "characterLimit";
        public const int CharacterLimitValue = 0;
        public const string PasswordName = "password";
        public const string PasswordValue = "true";
    }
    #endregion

    #region Positions\GridPosition.cs
    public class GridPosition : MovablePosition
    {
        public float NumCols => _numCols;
        public float NumRows => _numRows;
        
        private readonly float _numCols;
        private readonly float _numRows;
        
        public GridPosition(float xMin, float yMin, float xMax, float yMax, float numCols, float numRows) : base(xMin, yMin, xMax, yMax)
        {
            _numCols = numCols;
            _numRows = numRows;
        }
        
        public void MoveCols(int cols)
        {
            XMin += cols / _numCols;
            XMax += cols / _numCols;
            
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
            YMin += rows / _numRows;
            YMax += rows / _numRows;
            
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
        private readonly PositionState _state;
        
        public MovablePosition(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
            _state = new PositionState(XMin, YMin, XMax, YMax);
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
            XMin = _state.XMin;
            YMin = _state.YMin;
            XMax = _state.XMax;
            YMax = _state.YMax;
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

    #region Positions\MovableUiOffset.cs
    public class MovableUiOffset : UiOffset
    {
        public int XMin;
        public int YMin;
        public int XMax;
        public int YMax;
        private OffsetState _state;
        
        public MovableUiOffset(int x, int y, int width, int height)
        {
            XMin = x;
            YMin = y;
            XMax = x + width;
            YMax = y + height;
            _state = new OffsetState(XMin, XMax, YMin, YMax);
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
            XMin = _state.XMin;
            YMin = _state.YMin;
            XMax = _state.XMax;
            YMax = _state.YMax;
        }
    }
    #endregion

    #region Positions\Offset.cs
    public struct Offset
    {
        public readonly string Min;
        public readonly string Max;
        
        private const string PosFormat = "0.####";
        private const char Space = ' ';
        private static readonly StringBuilder _builder = new StringBuilder();
        
        public Offset(int xMin, int yMin, int xMax, int yMax)
        {
            Min = null;
            Max = null;
            Min = Build(xMin, yMin);
            Max = Build(xMax, yMax);
        }
        
        private static string Build(float min, float max)
        {
            _builder.Clear();
            _builder.Append(min.ToString(PosFormat));
            _builder.Append(Space);
            _builder.Append(max.ToString(PosFormat));
            return _builder.ToString();
        }
        
        public Offset(string min, string max)
        {
            Min = min;
            Max = max;
        }
        
        public override string ToString()
        {
            return string.Concat(Min, " ", Max);
        }
    }
    #endregion

    #region Positions\OffsetState.cs
    public struct OffsetState
    {
        public readonly int XMin;
        public readonly int YMin;
        public readonly int XMax;
        public readonly int YMax;
        
        public OffsetState(int xMin, int yMin, int xMax, int yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
        }
    }
    #endregion

    #region Positions\Position.cs
    public struct Position
    {
        public readonly string Min;
        public readonly string Max;
        
        private const string PosFormat = "0.####";
        private const char Space = ' ';
        private static readonly StringBuilder _builder = new StringBuilder();
        
        public Position(float xMin, float yMin, float xMax, float yMax)
        {
            Min = null;
            Max = null;
            Min = Build(xMin, yMin);
            Max = Build(xMax, yMax);
        }
        
        private static string Build(float min, float max)
        {
            _builder.Clear();
            _builder.Append(min.ToString(PosFormat));
            _builder.Append(Space);
            _builder.Append(max.ToString(PosFormat));
            return _builder.ToString();
        }
        
        public Position(string min, string max)
        {
            Min = min;
            Max = max;
        }
        
        public override string ToString()
        {
            return string.Concat(Min, " ", Max);
        }
    }
    #endregion

    #region Positions\PositionState.cs
    public struct PositionState
    {
        public readonly float XMin;
        public readonly float YMin;
        public readonly float XMax;
        public readonly float YMax;
        
        public PositionState(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
        }
    }
    #endregion

    #region Positions\StaticUiOffset.cs
    public class StaticUiOffset : UiOffset
    {
        private readonly Offset _offset;
        
        public StaticUiOffset(int width, int height) : this(-width / 2, -height / 2, width, height)
        {
            
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

    #region Positions\UiOffset.cs
    public abstract class UiOffset
    {
        public static readonly UiOffset DefaultOffset = new StaticUiOffset(0, 0, 0, 0);
        public static readonly Offset Default = new Offset(0, 0, 0, 0);
        
        public abstract Offset ToOffset();
    }
    #endregion

    #region Positions\UiPosition.cs
    public abstract class UiPosition
    {
        public static readonly UiPosition FullPosition = new StaticUiPosition(0, 0, 1, 1);
        public static readonly UiPosition TopLeft = new StaticUiPosition(0, 1, 0, 1);
        public static readonly UiPosition Left = new StaticUiPosition(0, .5f, 0, .5f);
        public static readonly UiPosition BottomLeft = new StaticUiPosition(0, 0, 0, 0);
        public static readonly UiPosition Top = new StaticUiPosition(.5f, 1, .5f, 1);
        public static readonly UiPosition Middle = new StaticUiPosition(.5f, .5f, .5f, .5f);
        public static readonly UiPosition Bottom = new StaticUiPosition(.5f, 0, .5f, 0);
        public static readonly UiPosition TopRight = new StaticUiPosition(1, 1, 1, 1);
        public static readonly UiPosition Right = new StaticUiPosition(1, .5f, 1, .5f);
        public static readonly UiPosition BottomRight = new StaticUiPosition(1, 0, 1, 0);
        
        public abstract Position ToPosition();
    }
    #endregion

    #region UiElements\BaseUiComponent.cs
    public class BaseUiComponent : Pool.IPooled
    {
        public string Name;
        public string Parent;
        public float FadeOut;
        private Position _position;
        private Offset? _offset;
        private bool _inPool = true;
        
        protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos.ToPosition();
            component._offset = offset?.ToOffset();
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }
        
        protected static T CreateBase<T>(Position pos, Offset? offset) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos;
            component._offset = offset;
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }
        
        protected static T CreateBase<T>(UiPosition pos) where T : BaseUiComponent, new()
        {
            T component = Pool.Get<T>();
            component._position = pos.ToPosition();
            if (component._inPool)
            {
                component.LeavePool();
            }
            return component;
        }
        
        public virtual void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, ref _position, ref _offset);
        }
        
        public void SetFadeOut(float duration)
        {
            FadeOut = duration;
        }
        
        public virtual void SetFadeIn(float duration)
        {
            throw new NotSupportedException($"FadeIn is not supported on this component {GetType().Name}");
        }
        
        public void UpdateOffset(UiOffset offset)
        {
            _offset = offset?.ToOffset();
        }
        
        public void UpdatePosition(UiPosition position, UiOffset offset = null)
        {
            _position = position.ToPosition();
            _offset = offset?.ToOffset();
        }
        
        public virtual void EnterPool()
        {
            Name = null;
            Parent = null;
            FadeOut = 0;
            _position = default(Position);
            _offset = null;
            _inPool = true;
        }
        
        public virtual void LeavePool()
        {
            _inPool = false;
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
        
        public override void SetFadeIn(float duration)
        {
            Button.FadeIn = duration;
        }
    }
    #endregion

    #region UiElements\UiImage.cs
    public class UiImage : BaseUiComponent
    {
        public RawImageComponent RawImage;
        
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
        
        public override void SetFadeIn(float duration)
        {
            RawImage.FadeIn = duration;
        }
    }
    #endregion

    #region UiElements\UiInput.cs
    public class UiInput : BaseUiComponent
    {
        public InputComponent Input;
        
        public static UiInput Create(int size, UiColor textColor, UiPosition pos, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false)
        {
            UiInput input = CreateBase<UiInput>(pos);
            InputComponent inputComp = input.Input;
            inputComp.FontSize = size;
            inputComp.Color = textColor;
            inputComp.Align = align;
            inputComp.Font = font;
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
        
        public override void SetFadeIn(float duration)
        {
            Input.FadeIn = duration;
        }
    }
    #endregion

    #region UiElements\UiLabel.cs
    public class UiLabel : BaseUiComponent
    {
        public TextComponent TextComponent;
        public OutlineComponent Outline;
        
        public static UiLabel Create(string text, int size, UiColor color, UiPosition pos, string font, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = CreateBase<UiLabel>(pos);
            TextComponent textComp = label.TextComponent;
            textComp.Text = text;
            textComp.FontSize = size;
            textComp.Color = color;
            textComp.Align = align;
            textComp.Font = font;
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
        
        public override void SetFadeIn(float duration)
        {
            TextComponent.FadeIn = duration;
        }
    }
    #endregion

    #region UiElements\UiPanel.cs
    public class UiPanel : BaseUiComponent
    {
        public bool NeedsCursor;
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
        
        public override void SetFadeIn(float duration)
        {
            Image.FadeIn = duration;
        }
    }
    #endregion

}
