using Network;
using Newtonsoft.Json;
using Oxide.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Color = UnityEngine.Color;
using Net = Network.Net;

//UiMergeFramework created with PluginMerge v(1.0.4.0) by MJSU @ https://github.com/dassjosh/Plugin.Merge
namespace Oxide.Plugins
{
    //Define:Framework
    //[Info("Rust UI Framework", "MJSU", "1.3.0")]
    //[Description("UI Framework for Rust")]
    public partial class UiMergeFramework : RustPlugin
    {
        #region Plugin\UiFramework.Methods.cs
        #region Unloading
        public override void HandleRemovedFromManager(PluginManager manager)
        {
            UiFrameworkPool.OnUnload();
            UiFrameworkArrayPool<byte>.Clear();
            UiFrameworkArrayPool<char>.Clear();
            UiColorCache.OnUnload();
            UiNameCache.OnUnload();
            base.HandleRemovedFromManager(manager);
        }
        #endregion
        #endregion

    }

    #region UiConstants.cs
    public class UiConstants
    {
        public static class RpcFunctions
        {
            public const string AddUiFunc = "AddUI";
            public const string DestroyUiFunc = "DestroyUI";
        }
        
        public static class Materials
        {
            public const string InGameBlur = "assets/content/ui/uibackgroundblur-ingamemenu.mat";
            public const string NoticeBlur = "assets/content/ui/uibackgroundblur-notice.mat";
            public const string BackgroundBlur = "assets/content/ui/uibackgroundblur.mat";
            public const string Icon = "assets/icons/iconmaterial.mat";
        }
        
        public static class Sprites
        {
            public const string Default = "Assets/Content/UI/UI.Background.Tile.psd";
            public const string Transparent = "Assets/Content/Textures/Generic/fulltransparent.tga";
            public const string RoundedBackground1 = "Assets/Content/UI/UI.Rounded.tga";
            public const string RoundedBackground2 = "Assets/Content/UI/UI.Background.Rounded.png";
            public const string GradientUp = "Assets/Content/UI/UI.Gradient.Up.psd";
            public const string BackgroundTransparentLinear = "Assets/Content/UI/UI.Background.Transparent.Linear.png";
            public const string BackgroundTransparentLinearLtr = "Assets/Content/UI/UI.Background.Transparent.LinearLTR.png";
            public const string White = "Assets/Content/UI/UI.White.tga";
            public const string Circle = "Assets/Icons/circle_closed.png";
            public const string Box = "Assets/Content/UI/UI.Box.tga";
            public const string BoxSharp = "Assets/Content/UI/UI.Box.Sharp.tga";
        }
    }
    #endregion

    #region Builder\UiBuilder.AddUi.cs
    public partial class UiBuilder
    {
        #region Add UI
        public void AddUi(BasePlayer player)
        {
            AddUi(new SendInfo(player.Connection));
        }
        
        public void AddUi(Connection connection)
        {
            AddUi(new SendInfo(connection));
        }
        
        public void AddUi(List<Connection> connections)
        {
            AddUi(new SendInfo(connections));
        }
        
        public void AddUi()
        {
            AddUi(new SendInfo(Net.sv.connections));
        }
        
        private void AddUi(SendInfo send)
        {
            JsonFrameworkWriter writer = CreateWriter();
            AddUi(send, writer);
            UiFrameworkPool.Free(ref writer);
        }
        #endregion
        
        #region Add UI Cached
        public void AddUiCached(BasePlayer player)
        {
            AddUiCached(new SendInfo(player.Connection));
        }
        
        public void AddUiCached(Connection connection)
        {
            AddUiCached(new SendInfo(connection));
        }
        
        public void AddUiCached(List<Connection> connections)
        {
            AddUiCached(new SendInfo(connections));
        }
        
        public void AddUiCached()
        {
            AddUiCached(new SendInfo(Net.sv.connections));
        }
        
        private void AddUiCached(SendInfo send)
        {
            AddUi(send, _cachedJson);
        }
        #endregion
        
        #region Net Write
        private static void AddUi(SendInfo send, JsonFrameworkWriter writer)
        {
            if (!ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc))
            {
                return;
            }
            
            writer.WriteToNetwork();
            Net.sv.write.Send(send);
        }
        
        private static void AddUi(SendInfo send, byte[] bytes)
        {
            if (!ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc))
            {
                return;
            }
            
            Net.sv.write.BytesWithSize(bytes);
            Net.sv.write.Send(send);
        }
        
        private static bool ClientRPCStart(string funcName)
        {
            if (!Net.sv.IsConnected() || CommunityEntity.ServerInstance.net == null || !Net.sv.write.Start())
            {
                return false;
            }
            
            Net.sv.write.PacketID(Message.Type.RPCMessage);
            Net.sv.write.UInt32(CommunityEntity.ServerInstance.net.ID);
            Net.sv.write.UInt32(StringPool.Get(funcName));
            Net.sv.write.UInt64(0UL);
            return true;
        }
        #endregion
    }
    #endregion

    #region Builder\UiBuilder.Components.cs
    public partial class UiBuilder
    {
        #region Add Components
        public void AddComponent(BaseUiComponent component, BaseUiComponent parent)
        {
            component.Parent = parent.Name;
            component.Name = UiNameCache.GetName(_rootName, _components.Count);
            //_componentLookup[component.Name] = component;
            _components.Add(component);
        }
        #endregion
        
        #region Section
        public UiSection Section(BaseUiComponent parent, UiPosition pos, UiOffset offset = default(UiOffset))
        {
            UiSection section = UiSection.Create(pos, offset);
            AddComponent(section, parent);
            return section;
        }
        #endregion
        
        #region Panel
        public UiPanel Panel(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color)
        {
            UiPanel panel = UiPanel.Create(pos, offset, color);
            AddComponent(panel, parent);
            return panel;
        }
        
        public UiPanel Panel(BaseUiComponent parent, UiPosition pos, UiColor color) => Panel(parent, pos, default(UiOffset), color);
        #endregion
        
        #region Button
        public UiButton CommandButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color, string command)
        {
            UiButton button = UiButton.CreateCommand(pos, offset, color, command);
            AddComponent(button, parent);
            return button;
        }
        
        public UiButton CommandButton(BaseUiComponent parent, UiPosition pos, UiColor color, string command) => CommandButton(parent, pos, default(UiOffset), color, command);
        
        public UiButton CloseButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color, string close)
        {
            UiButton button = UiButton.CreateClose(pos, offset, color, close);
            AddComponent(button, parent);
            return button;
        }
        
        public UiButton CloseButton(BaseUiComponent parent, UiPosition pos, UiColor color, string close) => CloseButton(parent, pos, default(UiOffset), color, close);
        #endregion
        
        #region Image
        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string png, UiColor color)
        {
            uint _;
            if (!uint.TryParse(png, out _))
            {
                throw new UiFrameworkException($"Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead");
            }
            
            UiImage image = UiImage.CreateFileImage(pos, offset, color, png);
            AddComponent(image, parent);
            return image;
        }
        
        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, string png, UiColor color) => ImageFileStorage(parent, pos, default(UiOffset), png, color);
        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string png) => ImageFileStorage(parent, pos, offset, png, UiColor.White);
        public UiImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, string png) => ImageFileStorage(parent, pos, default(UiOffset), png, UiColor.White);
        
        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, UiOffset offset, string sprite, UiColor color)
        {
            uint _;
            if (!uint.TryParse(sprite, out _))
            {
                throw new UiFrameworkException($"Image PNG '{sprite}' is not a valid uint. If trying to use a url please use WebImage instead");
            }
            
            UiImage image = UiImage.CreateSpriteImage(pos, offset, color, sprite);
            AddComponent(image, parent);
            return image;
        }
        
        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, UiOffset offset, string sprite) => ImageSprite(parent, pos, offset, sprite, UiColor.White);
        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, string sprite, UiColor color) => ImageSprite(parent, pos, default(UiOffset), sprite, color);
        public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, string sprite) => ImageSprite(parent, pos, sprite, UiColor.White);
        #endregion
        
        #region Item Icon
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId, UiColor color)
        {
            UiItemIcon image = UiItemIcon.Create(pos, offset, color, itemId, skinId);
            AddComponent(image, parent);
            return image;
        }
        
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId) => ItemIcon(parent, pos, offset, itemId, skinId, UiColor.White);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColor.White);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default(UiOffset), itemId, skinId);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default(UiOffset), itemId, color);
        public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId) => ItemIcon(parent, pos, default(UiOffset), itemId);
        #endregion
        
        #region Raw Image
        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string url, UiColor color)
        {
            if (!url.StartsWith("http"))
            {
                throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use {nameof(ImageFileStorage)} instead");
            }
            
            UiRawImage image = UiRawImage.CreateUrl(pos, offset, color, url);
            AddComponent(image, parent);
            return image;
        }
        
        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string url) => WebImage(parent, pos, offset, url, UiColor.White);
        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, string url, UiColor color) => WebImage(parent, pos, default(UiOffset), url, color);
        public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, string url) => WebImage(parent, pos, url, UiColor.White);
        
        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string texture, UiColor color)
        {
            UiRawImage image = UiRawImage.CreateTexture(pos, offset, color, texture);
            AddComponent(image, parent);
            return image;
        }
        
        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string texture) => TextureImage(parent, pos, offset, texture, UiColor.White);
        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, string texture, UiColor color) => TextureImage(parent, pos, default(UiOffset), texture, color);
        public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, string texture) => TextureImage(parent, pos, texture, UiColor.White);
        #endregion
        
        #region Label
        public UiLabel Label(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = UiLabel.Create(pos, offset, textColor, text, size, _font, align);
            AddComponent(label, parent);
            return label;
        }
        
        public UiLabel Label(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default(UiOffset), text, fontSize, textColor, align);
        
        public UiLabel LabelBackground(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiPanel panel = Panel(parent, pos, backgroundColor);
            UiLabel label = UiLabel.Create(UiPosition.HorizontalPaddedFull, offset, textColor, text, fontSize, _font, align);
            AddComponent(label, panel);
            return label;
        }
        
        public UiLabel LabelBackground(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default(UiOffset), text, fontSize, textColor, backgroundColor, align);
        #endregion
        
        #region Input
        public UiInput Input(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor,  string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = UiInput.Create(pos, offset, textColor, text, fontSize, command, _font, align, charsLimit, isPassword, readOnly, lineType);
            AddComponent(input, parent);
            return input;
        }
        
        public UiInput Input(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        => Input(parent, pos, default(UiOffset), text, fontSize, textColor, command, align, charsLimit, isPassword, readOnly, lineType);
        #endregion
        
        #region Countdown
        public UiLabel Countdown(UiLabel label, int startTime, int endTime, int step, string command)
        {
            label.AddCountdown(startTime, endTime, step, command);
            return label;
        }
        #endregion
        
        #region Outline
        public T TextOutline<T>(T outline, UiColor color) where T : BaseUiTextOutline
        {
            outline.AddTextOutline(color);
            return outline;
        }
        
        public T TextOutline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha = false) where T : BaseUiTextOutline
        {
            outline.AddTextOutline(color, distance, useGraphicAlpha);
            return outline;
        }
        #endregion
    }
    #endregion

    #region Builder\UiBuilder.Controls.cs
    public partial class UiBuilder
    {
        public UiButton TextButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            Label(button, UiPosition.HorizontalPaddedFull, default(UiOffset), text, textSize, textColor , align);
            return button;
        }
        
        public UiButton TextButton(BaseUiComponent parent, UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter)
        => TextButton(parent, pos, default(UiOffset), text, textSize, textColor, buttonColor, command, align);
        
        public UiButton ImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string png, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ImageFileStorage(button, UiPosition.Full, png);
            return button;
        }
        
        public UiButton ImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string png, string command) => ImageFileStorageButton(parent, pos, default(UiOffset), buttonColor, png, command);
        
        public UiButton ImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string sprite, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ImageSprite(button, UiPosition.Full, sprite);
            return button;
        }
        
        public UiButton ImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string sprite, string command) => ImageSpriteButton(parent, pos, default(UiOffset), buttonColor, sprite, command);
        
        public UiButton WebImageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string url, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            WebImage(button, UiPosition.Full, url);
            return button;
        }
        
        public UiButton WebImageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string url, string command) => WebImageButton(parent, pos, default(UiOffset), buttonColor, url, command);
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ItemIcon(button, UiPosition.Full, itemId);
            return button;
        }
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, string command) => ItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, command);
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command)
        {
            UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
            ItemIcon(button, UiPosition.Full, itemId, skinId);
            return button;
        }
        
        public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string command) => ItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, skinId, command);
        
        public UiInput InputBackground(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            parent = Panel(parent,  pos, offset, backgroundColor);
            UiInput input = Input(parent, UiPosition.HorizontalPaddedFull, text, fontSize, textColor, command, align, charsLimit, isPassword, readOnly, lineType);
            return input;
        }
        
        public UiInput InputBackground(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false,
        InputField.LineType lineType = InputField.LineType.SingleLine) =>
        InputBackground(parent, pos, default(UiOffset), text, fontSize, textColor, backgroundColor, command, align, charsLimit, isPassword, readOnly, lineType);
        
        public UiButton Checkbox(BaseUiComponent parent, UiPosition pos, UiOffset offset, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command)
        {
            return TextButton(parent, pos, offset, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, command);
        }
        
        public UiButton Checkbox(BaseUiComponent parent, UiPosition pos, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command) => Checkbox(parent, pos, default(UiOffset), isChecked, textSize, textColor, backgroundColor, command);
        
        public UiPanel ProgressBar(BaseUiComponent parent, UiPosition pos, float percentage, UiColor barColor, UiColor backgroundColor)
        {
            UiPanel background = Panel(parent, pos, backgroundColor);
            Panel(parent, UiPosition.SliceHorizontal(pos,0, Mathf.Clamp01(percentage)), barColor);
            return background;
        }
        
        public void ButtonNumberPicker(BaseUiComponent parent, UiPosition pos, int currentValue, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
        {
            float size = 1f / (maxValue - minValue + 1);
            UiSection section = Section(parent, pos);
            for (int i = minValue; i <= maxValue; i++)
            {
                UiPosition buttonPos = UiPosition.SliceHorizontal(UiPosition.Full, size * (i - minValue), size * (i + 1 - minValue));
                if (i == currentValue)
                {
                    TextButton(section, buttonPos, NumberCache<int>.ToString(i),textSize, textColor, currentButtonColor, $"{command} {i}");
                }
                else
                {
                    TextButton(section, buttonPos, NumberCache<int>.ToString(i), textSize, textColor, buttonColor, $"{command} {i}");
                }
            }
        }
        
        public void SimpleNumberPicker(BaseUiComponent parent, UiPosition pos, int value, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, string command, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.1f)
        {
            if (value > minValue)
            {
                UiPosition subtractSlice = UiPosition.SliceHorizontal(pos,0, buttonWidth);
                TextButton(parent, subtractSlice, "-", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(value - 1)}");
            }
            
            if (value < maxValue)
            {
                UiPosition addSlice = UiPosition.SliceHorizontal(pos, 1 - buttonWidth, 1);
                TextButton(parent, addSlice, "+", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(value + 1)}");
            }
            
            LabelBackground(parent, UiPosition.SliceHorizontal(pos,buttonWidth, 1 - buttonWidth), NumberCache<int>.ToString(value), fontSize, textColor, backgroundColor);
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, UiPosition pos, int value, int[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, string command, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.3f, bool readOnly = false)
        {
            int incrementCount = increments.Length;
            float buttonSize = buttonWidth / incrementCount;
            for (int i = 0; i < incrementCount; i++)
            {
                int increment = increments[i];
                UiPosition subtractSlice = UiPosition.SliceHorizontal(pos, i * buttonSize, (i + 1) * buttonSize);
                UiPosition addSlice = UiPosition.SliceHorizontal(pos, 1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);
                
                string incrementDisplay = increment.ToString();
                if (value - increment > minValue)
                {
                    TextButton(parent, subtractSlice, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value - increment)}");
                }
                
                if (value + increment < maxValue)
                {
                    TextButton(parent, addSlice, string.Concat("+", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value + increment)}");
                }
            }
            
            UiInput input = InputBackground(parent, UiPosition.SliceHorizontal(pos, buttonWidth, 1f - buttonWidth), value.ToString(), fontSize, textColor, backgroundColor, command, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }
        
        public void IncrementalNumberPicker(BaseUiComponent parent, UiPosition pos, float value, float[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, string command, float minValue = float.MinValue, float maxValue = float.MaxValue, float buttonWidth = 0.3f, bool readOnly = false, string incrementFormat = "0.##")
        {
            int incrementCount = increments.Length;
            float buttonSize = buttonWidth / incrementCount;
            for (int i = 0; i < incrementCount; i++)
            {
                float increment = increments[i];
                UiPosition subtractSlice = UiPosition.SliceHorizontal(pos, i * buttonSize, (i + 1) * buttonSize);
                UiPosition addSlice = UiPosition.SliceHorizontal(pos,1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);
                
                string incrementDisplay = increment.ToString(incrementFormat);
                if (value - increment > minValue)
                {
                    TextButton(parent, subtractSlice, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value - increment)}");
                }
                
                if (value + increment < maxValue)
                {
                    TextButton(parent, addSlice, incrementDisplay, fontSize, textColor, buttonColor, $"{command} {NumberCache<float>.ToString(value + increment)}");
                }
            }
            
            UiInput input = InputBackground(parent, UiPosition.SliceHorizontal(pos, buttonWidth, 1f - buttonWidth), value.ToString(), fontSize, textColor, backgroundColor, command, readOnly: readOnly);
            input.SetRequiresKeyboard();
        }
        
        public void Paginator(BaseUiComponent parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command)
        {
            grid.Reset();
            
            int totalButtons = (int)Math.Round(grid.NumCols, 0);
            int pageButtons = totalButtons - 5;
            
            int startPage = Math.Max(currentPage - pageButtons / 2, 0);
            int endPage = Math.Min(maxPage, startPage + pageButtons);
            if (endPage - startPage != pageButtons)
            {
                startPage = Math.Max(endPage - pageButtons, 0);
                if (endPage - startPage != pageButtons)
                {
                    grid.MoveCols((pageButtons - endPage - startPage) / 2f);
                }
            }
            
            TextButton(parent, grid, "<<<", fontSize, textColor, buttonColor, $"{command} 0");
            grid.MoveCols(1);
            TextButton(parent, grid, "<", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(Math.Max(0, currentPage - 1))}");
            grid.MoveCols(1);
            
            for (int i = startPage; i <= endPage; i++)
            {
                TextButton(parent, grid, (i + 1).ToString(), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, $"{command} {NumberCache<int>.ToString(i)}");
                grid.MoveCols(1);
            }
            
            TextButton(parent, grid, ">", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(Math.Min(maxPage, currentPage + 1))}");
            grid.MoveCols(1);
            TextButton(parent, grid, ">>>", fontSize, textColor, buttonColor, $"{command} {NumberCache<int>.ToString(maxPage)}");
        }
        
        public void ScrollBar(BaseUiComponent parent, UiPosition position, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiConstants.Sprites.RoundedBackground2)
        {
            UiPanel background = Panel(parent, position, backgroundColor);
            background.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
            float buttonSize = 1f / maxPage;
            for (int i = 0; i < maxPage; i++)
            {
                float min = buttonSize * i;
                float max = buttonSize * (i + 1);
                UiPosition pagePosition = direction == ScrollbarDirection.Vertical ? UiPosition.SliceVertical(UiPosition.Full, min, max) : UiPosition.SliceHorizontal(UiPosition.Full, min, max);
                if (i != currentPage)
                {
                    UiButton button = CommandButton(background, pagePosition, backgroundColor, $"{command} {NumberCache<int>.ToString(i)}");
                    button.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                }
                else
                {
                    UiPanel panel = Panel(background, pagePosition, barColor);
                    panel.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                }
            }
        }
        
        public void Border(BaseUiComponent parent, UiColor color, int width = 1, BorderMode border = BorderMode.All)
        {
            //If width is 0 nothing is displayed so don't try to render
            if (width == 0)
            {
                return;
            }
            
            bool top = HasBorderFlag(border, BorderMode.Top);
            bool left = HasBorderFlag(border, BorderMode.Left);
            bool bottom = HasBorderFlag(border, BorderMode.Bottom);
            bool right = HasBorderFlag(border, BorderMode.Right);
            
            if (width > 0)
            {
                int tbMin = left ? -width : 0;
                int tbMax = right ? width : 0;
                int lrMin = top ? -width : 0;
                int lrMax = bottom ? width : 0;
                
                if (top)
                {
                    Panel(parent, UiPosition.Top, new UiOffset(tbMin, 0, tbMax, width), color);
                }
                
                if (left)
                {
                    Panel(parent, UiPosition.Left, new UiOffset(-width, lrMin, 0, lrMax), color);
                }
                
                if (bottom)
                {
                    Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, -width, tbMax, 0), color);
                }
                
                if (right)
                {
                    Panel(parent, UiPosition.Right, new UiOffset(0, lrMin, width, lrMax), color);
                }
            }
            else
            {
                int tbMin = left ? width : 0;
                int tbMax = right ? -width : 0;
                int lrMin = top ? width : 0;
                int lrMax = bottom ? -width : 0;
                
                if (top)
                {
                    Panel(parent, UiPosition.Top, new UiOffset(tbMin, width, tbMax, 0), color);
                }
                
                if (left)
                {
                    Panel(parent, UiPosition.Left, new UiOffset(0, lrMin, -width, lrMax), color);
                }
                
                if (bottom)
                {
                    Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, 0, tbMax, -width), color);
                }
                
                if (right)
                {
                    Panel(parent, UiPosition.Right, new UiOffset(width, lrMin, 0, lrMax), color);
                }
            }
        }
        
        private bool HasBorderFlag(BorderMode mode, BorderMode flag)
        {
            return (mode & flag) != 0;
        }
    }
    #endregion

    #region Builder\UiBuilder.CreateBuilders.cs
    public partial class UiBuilder
    {
        public static UiBuilder Create(UiPosition pos, string name, string parent) => Create(pos, default(UiOffset), name, parent);
        public static UiBuilder Create(UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, string name, string parent) => Create(UiSection.Create(pos, offset), name, parent);
        public static UiBuilder Create(UiPosition pos, UiColor color, string name, string parent) => Create(pos, default(UiOffset), color, name, parent);
        public static UiBuilder Create(UiPosition pos, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), color, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, color, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, UiColor color, string name, string parent) => Create(UiPanel.Create(pos, offset, color), name, parent);
        public static UiBuilder Create(BaseUiComponent root, string name, UiLayer parent = UiLayer.Overlay) => Create(root, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(BaseUiComponent root, string name, string parent)
        {
            UiBuilder builder = Create();
            builder.SetRoot(root, name, parent);
            return builder;
        }
        
        public static UiBuilder Create()
        {
            return UiFrameworkPool.Get<UiBuilder>();
        }
        
        /// <summary>
        /// Creates a UiBuilder that is designed to be a popup modal
        /// </summary>
        /// <param name="offset">Dimensions of the modal</param>
        /// <param name="modalColor">Modal form color</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateModal(string name, UiColor modalColor, UiOffset offset, UiLayer layer = UiLayer.Overlay)
        {
            return CreateModal(name, modalColor, offset, new UiColor(0, 0, 0, 0.5f), UiConstants.Materials.InGameBlur, layer);
        }
        
        /// <summary>
        /// Creates a UiBuilder that is designed to be a popup modal
        /// </summary>
        /// <param name="offset">Dimensions of the modal</param>
        /// <param name="modalColor">Modal form color</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <param name="modalBackgroundColor">Color of the fullscreen background</param>
        /// <param name="backgroundMaterial">Material of the full screen background</param>
        /// <returns></returns>
        public static UiBuilder CreateModal(string name, UiColor modalColor, UiOffset offset, UiColor modalBackgroundColor, string backgroundMaterial = null, UiLayer layer = UiLayer.Overlay)
        {
            UiPanel backgroundBlur = UiPanel.Create(UiPosition.Full, default(UiOffset), modalBackgroundColor);
            backgroundBlur.SetMaterial(backgroundMaterial);
            
            UiBuilder builder = Create(backgroundBlur, name, layer);
            
            UiPanel modal = UiPanel.Create(UiPosition.MiddleMiddle, offset, modalColor);
            builder.AddComponent(modal, backgroundBlur);
            builder.OverrideRoot(modal);
            return builder;
        }
        
        /// <summary>
        /// Creates a UI builder that when created before your main UI will run a command if the user click outside of the UI window
        /// </summary>
        /// <param name="command">Command to run when the button is clicked</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateOutsideClose(string command, string name, UiLayer layer = UiLayer.Overlay)
        {
            UiBuilder builder = Create(UiButton.CreateCommand(UiPosition.Full, default(UiOffset), UiColor.Clear, command), name, layer);
            builder.NeedsMouse();
            return builder;
        }
        
        /// <summary>
        /// Creates a UI builder that will hold mouse input so the mouse doesn't reset position when updating other UI's
        /// </summary>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateMouseLock(string name, UiLayer layer = UiLayer.Overlay)
        {
            UiBuilder builder = Create(UiPosition.None, UiColor.Clear, name, UiLayerCache.GetLayer(layer));
            builder.NeedsMouse();
            return builder;
        }
    }
    #endregion

    #region Builder\UiBuilder.cs
    public partial class UiBuilder : BasePoolable
    {
        public BaseUiComponent Root;
        
        private bool _needsMouse;
        private bool _needsKeyboard;
        
        private string _rootName;
        private string _font;
        private byte[] _cachedJson;
        
        private List<BaseUiComponent> _components;
        //private Hash<string, BaseUiComponent> _componentLookup;
        
        private static string _globalFont;
        
        #region Constructor
        static UiBuilder()
        {
            SetGlobalFont(UiFont.RobotoCondensedRegular);
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
            _rootName = name;
        }
        
        public void OverrideRoot(BaseUiComponent component)
        {
            Root = component;
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
            _font = UiFontCache.GetUiFont(font);
        }
        
        public static void SetGlobalFont(UiFont font)
        {
            _globalFont = UiFontCache.GetUiFont(font);
        }
        
        // public T GetUi<T>(string name) where T : BaseUiComponent
        // {
            //     return (T)_componentLookup[name];
        // }
        #endregion
        
        #region Decontructor
        ~UiBuilder()
        {
            Dispose();
            //Need this because there is a global GC class that causes issues
            //ReSharper disable once RedundantNameQualifier
            System.GC.SuppressFinalize(this);
        }
        
        public override void DisposeInternal()
        {
            for (int index = 0; index < _components.Count; index++)
            {
                _components[index].Dispose();
            }
            
            UiFrameworkPool.FreeList(ref _components);
            //UiFrameworkPool.FreeHash(ref _componentLookup);
            UiFrameworkPool.Free(this);
        }
        
        protected override void EnterPool()
        {
            Root = null;
            _cachedJson = null;
            _needsKeyboard = false;
            _needsMouse = false;
            _font = null;
            _rootName = null;
        }
        
        protected override void LeavePool()
        {
            _components = UiFrameworkPool.GetList<BaseUiComponent>();
            //_componentLookup = UiFrameworkPool.GetHash<string, BaseUiComponent>();
            _font = _globalFont;
        }
        #endregion
        
        #region JSON
        public int WriteBuffer(byte[] buffer)
        {
            JsonFrameworkWriter writer = CreateWriter();
            int bytes = writer.WriteTo(buffer);
            writer.Dispose();
            return bytes;
        }
        
        public JsonFrameworkWriter CreateWriter()
        {
            JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
            
            writer.WriteStartArray();
            _components[0].WriteRootComponent(writer, _needsMouse, _needsKeyboard);
            
            int count = _components.Count;
            for (int index = 1; index < count; index++)
            {
                _components[index].WriteComponent(writer);
            }
            
            writer.WriteEndArray();
            return writer;
        }
        
        public void CacheJson()
        {
            JsonFrameworkWriter writer = CreateWriter();
            _cachedJson = writer.ToArray();
            writer.Dispose();
        }
        
        public byte[] GetBytes()
        {
            CacheJson();
            return _cachedJson;
        }
        
        /// <summary>
        /// Warning this is only recommend to use for debugging purposes
        /// </summary>
        /// <returns></returns>
        public string GetJsonString()
        {
            return Encoding.UTF8.GetString(GetBytes());
        }
        #endregion
    }
    #endregion

    #region Builder\UiBuilder.DestroyAndAddUi.cs
    public partial class UiBuilder
    {
        #region Destroy & Add UI
        public void DestroyAndAddUi(BasePlayer player)
        {
            DestroyAndAddUi(new SendInfo(player.Connection));
        }
        
        public void DestroyAndAddUi(Connection connection)
        {
            DestroyAndAddUi(new SendInfo(connection));
        }
        
        public void DestroyAndAddUi(List<Connection> connections)
        {
            DestroyAndAddUi(new SendInfo(connections));
        }
        
        public void DestroyAndAddUi()
        {
            DestroyAndAddUi(new SendInfo(Net.sv.connections));
        }
        
        private void DestroyAndAddUi(SendInfo send)
        {
            JsonFrameworkWriter writer = CreateWriter();
            DestroyUi(send, _rootName);
            AddUi(send, writer);
            UiFrameworkPool.Free(ref writer);
        }
        #endregion
    }
    #endregion

    #region Builder\UiBuilder.DestroyUi.cs
    public partial class UiBuilder
    {
        public void DestroyUi(BasePlayer player)
        {
            DestroyUi(player, _rootName);
        }
        
        public void DestroyUi(Connection connection)
        {
            DestroyUi(new SendInfo(connection), _rootName);
        }
        
        public void DestroyUi(List<Connection> connections)
        {
            DestroyUi(new SendInfo(connections), _rootName);
        }
        
        public void DestroyUi()
        {
            DestroyUi(_rootName);
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
                if (component is UiRawImage)
                {
                    DestroyUi(new SendInfo(connection), component.Name);
                }
            }
        }
        
        public void DestroyUiImages(List<Connection> connections)
        {
            for (int index = _components.Count - 1; index >= 0; index--)
            {
                BaseUiComponent component = _components[index];
                if (component is UiRawImage)
                {
                    DestroyUi(new SendInfo(connections), component.Name);
                }
            }
        }
        
        public static void DestroyUi(BasePlayer player, string name)
        {
            DestroyUi(new SendInfo(player.Connection), name);
        }
        
        public static void DestroyUi(string name)
        {
            DestroyUi(new SendInfo(Net.sv.connections), name);
        }
        
        public static void DestroyUi(SendInfo send, string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(send, null, UiConstants.RpcFunctions.DestroyUiFunc, name);
        }
    }
    #endregion

    #region Cache\EnumCache{T}.cs
    public static class EnumCache<T>
    {
        private static readonly Dictionary<T, string> CachedStrings = new Dictionary<T, string>();
        
        static EnumCache()
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

    #region Cache\NumberCache.cs
    public static class NumberCache<T>
    {
        private static readonly Dictionary<T, string> Cache = new Dictionary<T, string>();
        
        public static string ToString(T value)
        {
            string text;
            if (!Cache.TryGetValue(value, out text))
            {
                text = value.ToString();
                Cache[value] = text;
            }
            
            return text;
        }
    }
    #endregion

    #region Cache\UiColorCache.cs
    public static class UiColorCache
    {
        private const string Format = "0.####";
        private const char Space = ' ';
        
        private static readonly Dictionary<uint, string> ColorCache = new Dictionary<uint, string>();
        
        public static void WriteColor(JsonBinaryWriter writer, UiColor uiColor)
        {
            string color;
            if (!ColorCache.TryGetValue(uiColor.Value, out color))
            {
                color = GetColor(uiColor);
                ColorCache[uiColor.Value] = color;
            }
            
            writer.Write(color);
        }
        
        private static string GetColor(Color color)
        {
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.Append(color.r.ToString(Format));
            builder.Append(Space);
            builder.Append(color.g.ToString(Format));
            builder.Append(Space);
            builder.Append(color.b.ToString(Format));
            if (color.a != 1f)
            {
                builder.Append(Space);
                builder.Append(color.a.ToString(Format));
            }
            return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
        }
        
        public static void OnUnload()
        {
            ColorCache.Clear();
        }
    }
    #endregion

    #region Cache\UiFontCache.cs
    public static class UiFontCache
    {
        private const string DroidSansMono = "droidsansmono.ttf";
        private const string PermanentMarker = "permanentmarker.ttf";
        private const string RobotoCondensedBold = "robotocondensed-bold.ttf";
        private const string RobotoCondensedRegular = "robotocondensed-regular.ttf";
        private const string PressStart2PRegular = "PressStart2P-Regular.ttf";
        
        private static readonly Dictionary<UiFont, string> Fonts = new Dictionary<UiFont, string>
        {
            [UiFont.DroidSansMono] = DroidSansMono,
            [UiFont.PermanentMarker] = PermanentMarker,
            [UiFont.RobotoCondensedBold] = RobotoCondensedBold,
            [UiFont.RobotoCondensedRegular] = RobotoCondensedRegular,
            [UiFont.PressStart2PRegular] = PressStart2PRegular,
        };
        
        public static string GetUiFont(UiFont font)
        {
            return Fonts[font];
        }
    }
    #endregion

    #region Cache\UiLayerCache.cs
    public static class UiLayerCache
    {
        private const string Overall = "Overall";
        private const string Overlay = "Overlay";
        private const string Hud = "Hud";
        private const string HudMenu = "Hud.Menu";
        private const string Under = "Under";
        
        private static readonly Dictionary<UiLayer, string> Layers = new Dictionary<UiLayer, string>
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
    #endregion

    #region Cache\UiNameCache.cs
    public static class UiNameCache
    {
        private static readonly Dictionary<string, List<string>> NameCache = new Dictionary<string, List<string>>();
        
        public static string GetName(string baseName, int index)
        {
            List<string> names;
            if (!NameCache.TryGetValue(baseName, out names))
            {
                names = new List<string>();
                NameCache[baseName] = names;
            }
            
            if (index >= names.Count)
            {
                for (int i = names.Count; i <= index; i++)
                {
                    names.Add(string.Concat(baseName, "_", index.ToString()));
                }
            }
            
            return names[index];
        }
        
        public static void OnUnload()
        {
            NameCache.Clear();
        }
    }
    #endregion

    #region Cache\VectorCache.cs
    public static class VectorCache
    {
        private const string Format = "0.####";
        private const char Space = ' ';
        private const short PositionRounder = 10000;
        
        private static readonly Dictionary<ushort, string> PositionCache = new Dictionary<ushort, string>();
        
        static VectorCache()
        {
            for (ushort i = 0; i <= PositionRounder; i++)
            {
                PositionCache[i] = (i / (float)PositionRounder).ToString(Format);
            }
        }
        
        public static void WritePosition(JsonBinaryWriter writer, Vector2 pos)
        {
            WriteFromCache(writer, pos.x);
            writer.Write(Space);
            WriteFromCache(writer, pos.y);
        }
        
        private static void WriteFromCache(JsonBinaryWriter writer, float pos)
        {
            if (pos >= 0f && pos <= 1f)
            {
                writer.Write(PositionCache[(ushort)(pos * PositionRounder)]);
            }
            else
            {
                string value;
                if (!PositionCache.TryGetValue((ushort)(pos * PositionRounder), out value))
                {
                    value = pos.ToString(Format);
                    PositionCache[(ushort)(pos * PositionRounder)] = value;
                }
                
                writer.Write(value);
            }
        }
        
        public static void WriteVector(JsonBinaryWriter writer, Vector2 pos)
        {
            string formattedPos;
            if (!PositionCache.TryGetValue((ushort)(pos.x * PositionRounder), out formattedPos))
            {
                formattedPos = pos.x.ToString(Format);
                PositionCache[(ushort)(pos.x * PositionRounder)] = formattedPos;
            }
            
            writer.Write(formattedPos);
            writer.Write(Space);
            
            if (!PositionCache.TryGetValue((ushort)(pos.y * PositionRounder), out formattedPos))
            {
                formattedPos = pos.y.ToString(Format);
                PositionCache[(ushort)(pos.y * PositionRounder)] = formattedPos;
            }
            
            writer.Write(formattedPos);
        }
        
        public static void WriteOffset(JsonBinaryWriter writer, Vector2 pos)
        {
            writer.Write(NumberCache<short>.ToString((short)Math.Round(pos.x)));
            writer.Write(Space);
            writer.Write(NumberCache<short>.ToString((short)Math.Round(pos.y)));
        }
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
        
        #region Static Colors
        public static readonly UiColor Black =  "#000000";
        public static readonly UiColor White = "#FFFFFF";
        public static readonly UiColor Silver =  "#C0C0C0";
        public static readonly UiColor Gray = "#808080";
        public static readonly UiColor Red = "#FF0000";
        public static readonly UiColor Maroon = "#800000";
        public static readonly UiColor Orange = "#FFA500";
        public static readonly UiColor Yellow = "#FFEB04";
        public static readonly UiColor Olive = "#808000";
        public static readonly UiColor Lime = "#00FF00";
        public static readonly UiColor Green = "#008000";
        public static readonly UiColor Teal = "#008080";
        public static readonly UiColor Cyan = "#00FFFF";
        public static readonly UiColor Blue = "#0000FF";
        public static readonly UiColor Navy = "#000080";
        public static readonly UiColor Magenta = "#FF00FF";
        public static readonly UiColor Purple = "#800080";
        public static readonly UiColor Clear = "#00000000";
        #endregion
        
        #region Constructors
        public UiColor(Color color)
        {
            Color = color;
            Value = ((uint)(color.r * 255) << 24) | ((uint)(color.g * 255) << 16) | ((uint)(color.b * 255) << 8) | (uint)(color.a * 255);
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
        
        public static UiColor Lerp(UiColor start, UiColor end, float value)
        {
            return Color.Lerp(start, end, value);
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
            #if UiBenchmarks
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
            public static readonly UiColor Text = UiColor.White;
            public static readonly UiColor Panel = "#2B2B2B";
            public static readonly UiColor PanelSecondary = "#3f3f3f";
            public static readonly UiColor PanelTertiary = "#525252";
            public static readonly UiColor ButtonPrimary = Rust.Red;
            public static readonly UiColor ButtonSecondary = "#666666";
        }
        
        #region UI Colors
        public static readonly UiColor Body = UiColor.WithAlpha(Form.Body, "B2");
        public static readonly UiColor BodyHeader = Form.Header;
        public static readonly UiColor Text = UiColor.WithAlpha(Form.Text, "80");
        public static readonly UiColor Panel = Form.Panel;
        public static readonly UiColor PanelSecondary = Form.PanelSecondary;
        public static readonly UiColor PanelTertiary = Form.PanelTertiary;
        public static readonly UiColor CloseButton = Form.ButtonPrimary;
        public static readonly UiColor ButtonPrimary = Form.ButtonPrimary;
        public static readonly UiColor ButtonSecondary = Form.ButtonSecondary;
        #endregion
    }
    #endregion

    #region Components\BaseColorComponent.cs
    public abstract class BaseColorComponent : BaseComponent
    {
        public UiColor Color;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddField(JsonDefaults.Color.ColorName, Color);
        }
    }
    #endregion

    #region Components\BaseComponent.cs
    public abstract class BaseComponent : BasePoolable
    {
        public abstract void WriteComponent(JsonFrameworkWriter writer);
    }
    #endregion

    #region Components\BaseFadeInComponent.cs
    public abstract class BaseFadeInComponent : BaseColorComponent
    {
        public float FadeIn;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
            base.WriteComponent(writer);
        }
        
        protected override void EnterPool()
        {
            FadeIn = 0;
        }
    }
    #endregion

    #region Components\BaseImageComponent.cs
    public abstract class BaseImageComponent : BaseFadeInComponent
    {
        public string Sprite;
        public string Material;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddField(JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
            writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
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
    public abstract class BaseTextComponent : BaseFadeInComponent
    {
        public int FontSize = JsonDefaults.BaseText.FontSize;
        public string Font;
        public TextAnchor Align;
        public string Text;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddTextField(JsonDefaults.BaseText.TextName, Text);
            writer.AddField(JsonDefaults.BaseText.FontSizeName, FontSize, JsonDefaults.BaseText.FontSize);
            writer.AddField(JsonDefaults.BaseText.FontName, Font, JsonDefaults.BaseText.FontValue);
            writer.AddField(JsonDefaults.BaseText.AlignName, Align);
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
        public Image.Type ImageType;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
            writer.AddField(JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
            writer.AddField(JsonDefaults.Image.ImageType, ImageType);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
        
        protected override void EnterPool()
        {
            base.EnterPool();
            Command = null;
            Close = null;
            ImageType = Image.Type.Simple;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
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
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
            writer.AddField(JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
            writer.AddField(JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
            writer.AddField(JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
            writer.WriteEndObject();
        }
        
        protected override void EnterPool()
        {
            StartTime = JsonDefaults.Countdown.StartTimeValue;
            EndTime = JsonDefaults.Countdown.EndTimeValue;
            Step = JsonDefaults.Countdown.StepValue;
            Command = null;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region Components\ImageComponent.cs
    public class ImageComponent : BaseImageComponent
    {
        private const string Type = "UnityEngine.UI.Image";
        
        public string Png;
        public Image.Type ImageType;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Image.PngName, Png, JsonDefaults.Common.NullValue);
            writer.AddField(JsonDefaults.Image.ImageType, ImageType);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
        
        protected override void EnterPool()
        {
            base.EnterPool();
            Png = JsonDefaults.Common.NullValue;
            ImageType = Image.Type.Simple;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
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
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
            writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
            writer.AddField(JsonDefaults.Input.LineTypeName, LineType);
            
            if (IsPassword)
            {
                writer.AddKeyField(JsonDefaults.Input.PasswordName);
            }
            
            if (IsReadyOnly)
            {
                writer.AddFieldRaw(JsonDefaults.Input.ReadOnlyName, true);
            }
            
            if (NeedsKeyboard)
            {
                writer.AddKeyField(JsonDefaults.Input.InputNeedsKeyboardName);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
        
        protected override void EnterPool()
        {
            base.EnterPool();
            CharsLimit = JsonDefaults.Input.CharacterLimitValue;
            Command = null;
            NeedsKeyboard = true;
            IsPassword = false;
            LineType = default(InputField.LineType);
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region Components\ItemIconComponent.cs
    public class ItemIconComponent : BaseImageComponent
    {
        private const string Type = "UnityEngine.UI.Image";
        
        public int ItemId;
        public ulong SkinId;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddFieldRaw(JsonDefaults.ItemIcon.ItemIdName, ItemId);
            writer.AddField(JsonDefaults.ItemIcon.SkinIdName, SkinId, default(ulong));
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
        
        protected override void EnterPool()
        {
            ItemId = default(int);
            SkinId = default(ulong);
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
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
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Outline.DistanceName, Distance, new Vector2(1.0f, -1.0f));
            if (UseGraphicAlpha)
            {
                writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
        
        protected override void EnterPool()
        {
            Distance = new Vector2(1.0f, -1.0f);
            UseGraphicAlpha = false;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region Components\RawImageComponent.cs
    public class RawImageComponent : BaseFadeInComponent
    {
        private const string Type = "UnityEngine.UI.RawImage";
        
        public string Url;
        public string Texture;
        public string Material;
        
        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.BaseImage.SpriteName, Texture, JsonDefaults.RawImage.TextureValue);
            writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
            if (!string.IsNullOrEmpty(Url))
            {
                writer.AddFieldRaw(JsonDefaults.Image.UrlName, Url);
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
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
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
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
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
        All = Top | Left | Bottom | Right
    }
    #endregion

    #region Enums\ScrollbarDirection.cs
    public enum ScrollbarDirection
    {
        Vertical,
        Horizontal
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
        RobotoCondensedRegular,
        
        /// <summary>
        /// PressStart2P-Regular.ttf
        /// </summary>
        PressStart2PRegular
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
        public UiFrameworkException(string message) : base(message) { }
    }
    #endregion

    #region Json\JsonBinaryWriter.cs
    public class JsonBinaryWriter : BasePoolable
    {
        private const int SegmentSize = 2048;
        
        private List<SizedArray<byte>> _segments;
        private int _charIndex;
        private int _size;
        private char[] _charBuffer;
        
        public void Write(char character)
        {
            _charBuffer[_charIndex] = character;
            _charIndex++;
            if (_charIndex >= SegmentSize)
            {
                Flush();
            }
        }
        
        public void Write(string text)
        {
            int length = text.Length;
            char[] buffer = _charBuffer;
            int charIndex = _charIndex;
            for (int i = 0; i < length; i++)
            {
                buffer[charIndex + i] = text[i];
            }
            _charIndex += length;
            if (_charIndex >= SegmentSize)
            {
                Flush();
            }
        }
        
        private void Flush()
        {
            if (_charIndex == 0)
            {
                return;
            }
            
            byte[] segment = UiFrameworkArrayPool<byte>.Shared.Rent(SegmentSize * 2);
            int size = Encoding.UTF8.GetBytes(_charBuffer, 0, _charIndex, segment, 0);
            _segments.Add(new SizedArray<byte>(segment, size));
            _size += size;
            _charIndex = 0;
        }
        
        public int WriteToArray(byte[] bytes)
        {
            Flush();
            int writeIndex = 0;
            for (int i = 0; i < _segments.Count; i++)
            {
                SizedArray<byte> segment = _segments[i];
                Buffer.BlockCopy(segment.Array, 0, bytes, writeIndex, segment.Size);
                writeIndex += segment.Size;
            }
            
            return _size;
        }
        
        public void WriteToNetwork()
        {
            Flush();
            Net.sv.write.UInt32((uint)_size);
            for (int i = 0; i < _segments.Count; i++)
            {
                SizedArray<byte> segment = _segments[i];
                Net.sv.write.Write(segment.Array, 0, segment.Size);
            }
        }
        
        public byte[] ToArray()
        {
            Flush();
            byte[] bytes = new byte[_size];
            WriteToArray(bytes);
            return bytes;
        }
        
        protected override void LeavePool()
        {
            _segments = UiFrameworkPool.GetList<SizedArray<byte>>();
            if (_segments.Capacity < 100)
            {
                _segments.Capacity = 100;
            }
            _charBuffer = UiFrameworkArrayPool<char>.Shared.Rent(SegmentSize * 2);
        }
        
        protected override void EnterPool()
        {
            for (int index = 0; index < _segments.Count; index++)
            {
                byte[] bytes = _segments[index].Array;
                UiFrameworkArrayPool<byte>.Shared.Return(bytes);
            }
            
            UiFrameworkArrayPool<char>.Shared.Return(_charBuffer);
            UiFrameworkPool.FreeList(ref _segments);
            _charBuffer = null;
            _size = 0;
            _charIndex = 0;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
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
        }
        
        public static class Offset
        {
            public const string OffsetMinName = "offsetmin";
            public const string OffsetMaxName = "offsetmax";
            public const string DefaultOffsetMax = "0 0";
        }
        
        public static class Color
        {
            public const string ColorName = "color";
            public const uint ColorValue = 0xFFFFFFFF;
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
        }
        
        public static class Button
        {
            public const string CloseName = "close";
        }
        
        public static class Image
        {
            public const string PngName = "png";
            public const string UrlName = "url";
            public const string ImageType = "imagetype";
        }
        
        public static class ItemIcon
        {
            public const string ItemIdName = "itemid";
            public const string SkinIdName = "skinid";
        }
        
        public static class Input
        {
            public const string CharacterLimitName = "characterLimit";
            public const int CharacterLimitValue = 0;
            public const string PasswordName = "password";
            public const string ReadOnlyName = "readOnly";
            public const string LineTypeName = "lineType";
            public const string InputNeedsKeyboardName = "needsKeyboard";
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
        
        private JsonBinaryWriter _writer;
        
        private void Init()
        {
            _writer = UiFrameworkPool.Get<JsonBinaryWriter>();
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
                _writer.Write(CommaChar);
                _objectComma = false;
            }
            
            _propertyComma = false;
        }
        
        private void OnDepthDecrease()
        {
            _objectComma = true;
        }
        
        #region Field Handling
        public void AddFieldRaw(string name, string value)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
        
        public void AddFieldRaw(string name, int value)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
        
        public void AddFieldRaw(string name, bool value)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
        
        public void AddField(string name, string value, string defaultValue)
        {
            if (value != null && value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }
        
        public void AddField(string name, Vector2 value, Vector2 defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }
        
        public void AddPosition(string name, Vector2 value, Vector2 defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WritePosition(value);
            }
        }
        
        public void AddOffset(string name, Vector2 value, Vector2 defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteOffset(value);
            }
        }
        
        public void AddField(string name, TextAnchor value)
        {
            if (value != TextAnchor.UpperLeft)
            {
                WritePropertyName(name);
                WriteValue(EnumCache<TextAnchor>.ToString(value));
            }
        }
        
        public void AddField(string name, InputField.LineType value)
        {
            if (value != InputField.LineType.SingleLine)
            {
                WritePropertyName(name);
                WriteValue(EnumCache<InputField.LineType>.ToString(value));
            }
        }
        
        public void AddField(string name, Image.Type value)
        {
            if (value != Image.Type.Simple)
            {
                WritePropertyName(name);
                WriteValue(EnumCache<Image.Type>.ToString(value));
            }
        }
        
        public void AddField(string name, int value, int defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }
        
        public void AddField(string name, float value, float defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }
        
        public void AddField(string name, ulong value, ulong defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }
        
        public void AddField(string name, UiColor color)
        {
            if (color.Value != JsonDefaults.Color.ColorValue)
            {
                WritePropertyName(name);
                WriteValue(color);
            }
        }
        
        public void AddKeyField(string name)
        {
            WritePropertyName(name);
            WriteBlankValue();
        }
        
        public void AddTextField(string name, string value)
        {
            WritePropertyName(name);
            WriteTextValue(value);
        }
        
        public void AddMouse()
        {
            WriteStartObject();
            AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
            WriteEndObject();
        }
        
        public void AddKeyboard()
        {
            WriteStartObject();
            AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
            WriteEndObject();
        }
        #endregion
        
        #region Writing
        
        #endregion
        public void WriteStartArray()
        {
            OnDepthIncrease();
            _writer.Write(ArrayStartChar);
        }
        
        public void WriteEndArray()
        {
            _writer.Write(ArrayEndChar);
            OnDepthDecrease();
        }
        
        public void WriteStartObject()
        {
            OnDepthIncrease();
            _writer.Write(ObjectStartChar);
        }
        
        public void WriteEndObject()
        {
            _writer.Write(ObjectEndChar);
            OnDepthDecrease();
        }
        
        public void WritePropertyName(string name)
        {
            if (_propertyComma)
            {
                _writer.Write(PropertyComma);
            }
            else
            {
                _propertyComma = true;
                _writer.Write(QuoteChar);
            }
            
            _writer.Write(name);
            _writer.Write(Separator);
        }
        
        public void WriteValue(bool value)
        {
            _writer.Write(value ? '1' : '0');
        }
        
        public void WriteValue(int value)
        {
            _writer.Write(NumberCache<int>.ToString(value));
        }
        
        public void WriteValue(float value)
        {
            _writer.Write(NumberCache<float>.ToString(value));
        }
        
        public void WriteValue(ulong value)
        {
            _writer.Write(NumberCache<ulong>.ToString(value));
        }
        
        public void WriteValue(string value)
        {
            _writer.Write(QuoteChar);
            _writer.Write(value);
            _writer.Write(QuoteChar);
        }
        
        public void WriteBlankValue()
        {
            _writer.Write(QuoteChar);
            _writer.Write(QuoteChar);
        }
        
        public void WriteTextValue(string value)
        {
            _writer.Write(QuoteChar);
            if (value != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    char character = value[i];
                    if (character == '\"')
                    {
                        _writer.Write("\\\"");
                    }
                    else
                    {
                        _writer.Write(character);
                    }
                }
            }
            _writer.Write(QuoteChar);
        }
        
        public void WriteValue(Vector2 pos)
        {
            _writer.Write(QuoteChar);
            VectorCache.WriteVector(_writer, pos);
            _writer.Write(QuoteChar);
        }
        
        public void WritePosition(Vector2 pos)
        {
            _writer.Write(QuoteChar);
            VectorCache.WritePosition(_writer, pos);
            _writer.Write(QuoteChar);
        }
        
        public void WriteOffset(Vector2 offset)
        {
            _writer.Write(QuoteChar);
            VectorCache.WriteOffset(_writer, offset);
            _writer.Write(QuoteChar);
        }
        
        public void WriteValue(UiColor color)
        {
            _writer.Write(QuoteChar);
            UiColorCache.WriteColor(_writer, color);
            _writer.Write(QuoteChar);
        }
        
        protected override void EnterPool()
        {
            _objectComma = false;
            _propertyComma = false;
            UiFrameworkPool.Free(ref _writer);
        }
        
        public override string ToString()
        {
            return _writer.ToString();
        }
        
        public int WriteTo(byte[] buffer)
        {
            return _writer.WriteToArray(buffer);
        }
        
        public void WriteToNetwork()
        {
            _writer.WriteToNetwork();
        }
        
        public byte[] ToArray()
        {
            return _writer.ToArray();
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region Json\SizedArray.cs
    public struct SizedArray<T>
    {
        public readonly T[] Array;
        public readonly int Size;
        
        public SizedArray(T[] array, int size)
        {
            Array = array;
            Size = size;
        }
    }
    #endregion

    #region Offsets\GridOffset.cs
    public class GridOffset : MovableOffset
    {
        public readonly int NumCols;
        public readonly int NumRows;
        public readonly float Width;
        public readonly float Height;
        
        public GridOffset(float xMin, float yMin, float xMax, float yMax, int numCols, int numRows, float width, float height) : base(xMin, yMin, xMax, yMax)
        {
            NumCols = numCols;
            NumRows = numRows;
            Width = width;
            Height = height;
        }
        
        public void MoveCols(int cols)
        {
            MoveCols((float)cols);
        }
        
        public void MoveCols(float cols)
        {
            float distance = cols / NumCols * Width;
            XMin += distance;
            XMax += distance;
            
            if (XMax > Width)
            {
                XMin -= Width;
                XMax -= Width;
                MoveRows(1);
            }
        }
        
        public void MoveRows(int rows)
        {
            float distance = rows / (float)NumRows * Height;
            YMin -= distance;
            YMax -= distance;
        }
    }
    #endregion

    #region Offsets\GridOffsetBuilder.cs
    public class GridOffsetBuilder
    {
        private readonly int _numCols;
        private readonly int _numRows;
        private readonly UiOffset _area;
        private readonly float _width;
        private readonly float _height;
        private int _rowHeight = 1;
        private int _rowOffset;
        private int _colWidth = 1;
        private int _colOffset;
        private int _xPad;
        private int _yPad;
        
        public GridOffsetBuilder(int size, UiOffset area) : this(size, size, area)
        {
            
        }
        
        public GridOffsetBuilder(int numCols, int numRows, UiOffset area)
        {
            if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            _numCols = numCols;
            _numRows = numRows;
            _area = area;
            _width = area.Max.x - area.Min.x;
            _height = area.Max.y - area.Min.y;
        }
        
        public GridOffsetBuilder SetRowHeight(int height)
        {
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            _rowHeight = height;
            return this;
        }
        
        public GridOffsetBuilder SetRowOffset(int offset)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            _rowOffset = offset;
            return this;
        }
        
        public GridOffsetBuilder SetColWidth(int width)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            _colWidth = width;
            return this;
        }
        
        public GridOffsetBuilder SetColOffset(int offset)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            _colOffset = offset;
            return this;
        }
        
        public GridOffsetBuilder SetPadding(int padding)
        {
            _xPad = padding;
            _yPad = padding;
            return this;
        }
        
        public GridOffsetBuilder SetPadding(int xPad, int yPad)
        {
            _xPad = xPad;
            _yPad = yPad;
            return this;
        }
        
        public GridOffsetBuilder SetRowPadding(int padding)
        {
            _xPad = padding;
            return this;
        }
        
        public GridOffsetBuilder SetColPadding(int padding)
        {
            _yPad = padding;
            return this;
        }
        
        public GridOffset Build()
        {
            float xMin = _area.Min.x;
            float yMin = _area.Max.y - _rowHeight / (float)_numRows * _height;
            float xMax = _colWidth / (float)_numCols * _width;
            float yMax = _rowHeight / (float)_numRows * _height;
            
            if (_colOffset != 0)
            {
                int size = (int)(_colOffset / (float)_numCols * _width);
                xMin += size;
                xMax += size;
            }
            
            if (_rowOffset != 0)
            {
                int size = (int)(_rowOffset / (float)_numRows * _height);
                yMin += size;
                yMax += size;
            }
            
            xMin += _xPad;
            xMax -= _xPad * 2;
            yMin += _yPad;
            yMax -= _yPad * 2;
            
            return new GridOffset(xMin, yMin, xMax, yMax, _numCols, _numRows, _width, _height);
        }
    }
    #endregion

    #region Offsets\MovableOffset.cs
    public class MovableOffset
    {
        public float XMin;
        public float YMin;
        public float XMax;
        public float YMax;
        private readonly UiOffset _initialState;
        
        public MovableOffset(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMin + xMax;
            YMax = yMin + yMax;
            _initialState = new UiOffset(XMin, YMin, XMax, YMax);
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
        
        public UiOffset ToOffset()
        {
            return new UiOffset(XMin, YMin, XMax, YMax);
        }
        
        public void Reset()
        {
            XMin = _initialState.Min.x;
            YMin = _initialState.Min.y;
            XMax = _initialState.Max.x;
            YMax = _initialState.Max.y;
        }
        
        public static implicit operator UiOffset(MovableOffset offset) => offset.ToOffset();
    }
    #endregion

    #region Offsets\UiOffset.cs
    public struct UiOffset
    {
        public static readonly UiOffset None = new UiOffset(0, 0, 0, 0);
        public static readonly UiOffset Scaled = new UiOffset(1280, 720);
        
        public readonly Vector2 Min;
        public readonly Vector2 Max;
        
        public UiOffset(int width, int height) : this(-width / 2f, -height / 2f, width / 2f, height / 2f) { }
        
        public UiOffset(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(xMin, yMin);
            Max = new Vector2(xMax, yMax);
        }
        
        /// <summary>
        /// Returns a slice of the position
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="left">Pixels to remove from the left</param>
        /// <param name="bottom">Pixels to remove from the bottom</param>
        /// <param name="right">>Pixels to remove from the right</param>
        /// <param name="top">Pixels to remove from the top</param>
        /// <returns>Sliced <see cref="UiOffset"/></returns>
        public static UiOffset Slice(UiOffset pos, int left, int bottom, int right, int top)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x + left, min.y + bottom, max.x - right, max.y - top);
        }
        
        /// <summary>
        /// Returns a horizontal slice of the position
        /// </summary>
        /// <param name="pos">Offset to slice</param>
        /// <param name="left">Pixels to remove from the left</param>
        /// <param name="right">>Pixels to remove from the right</param>
        /// <returns>Sliced <see cref="UiOffset"/></returns>
        public static UiOffset SliceHorizontal(UiOffset pos, int left, int right)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x + left, min.y, max.x - right,max.y);
        }
        
        /// <summary>
        /// Returns a vertical slice of the position
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="bottom">Pixels to remove from the bottom</param>
        /// <param name="top">Pixels to remove from the top</param>
        /// <returns>Sliced <see cref="UiOffset"/></returns>
        public static UiOffset SliceVertical(UiOffset pos, int bottom, int top)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(max.x, min.y + bottom, max.x, max.y - top);
        }
        
        public override string ToString()
        {
            return $"({Min.x:0}, {Min.y:0}) ({Max.x:0}, {Max.y:0})";
        }
    }
    #endregion

    #region Pooling\BasePool.cs
    public abstract class BasePool<T> : IPool<T> where T : class, new()
    {
        private readonly T[] _pool;
        private int _index;
        
        /// <summary>
        /// Base Pool Constructor
        /// </summary>
        /// <param name="maxSize">Max Size of the pool</param>
        protected BasePool(int maxSize)
        {
            _pool = new T[maxSize];
            UiFrameworkPool.AddPool(this);
        }
        
        /// <summary>
        /// Returns an element from the pool if it exists else it creates a new one
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T item = null;
            if (_index < _pool.Length)
            {
                item = _pool[_index];
                _pool[_index] = null;
                _index++;
            }
            
            if (item == null)
            {
                item = new T();
            }
            
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
            
            if (_index != 0)
            {
                _index--;
                _pool[_index] = item;
            }
            
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
        
        public void Clear()
        {
            for (int index = 0; index < _pool.Length; index++)
            {
                _pool[index] = null;
                _index = 0;
            }
        }
    }
    #endregion

    #region Pooling\BasePoolable.cs
    public abstract class BasePoolable : IDisposable
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
        
        public void Dispose()
        {
            if (_shouldPool)
            {
                DisposeInternal();
            }
        }
        
        public abstract void DisposeInternal();
    }
    #endregion

    #region Pooling\HashPool.cs
    public class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>>
    {
        public static readonly IPool<Hash<TKey, TValue>> Instance;
        
        static HashPool()
        {
            Instance = new HashPool<TKey, TValue>();
        }
        
        private HashPool() : base(128) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
        {
            item.Clear();
            return true;
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
        public static readonly IPool<List<T>> Instance;
        
        static ListPool()
        {
            Instance = new ListPool<T>();
        }
        
        private ListPool() : base(128) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref List<T> item)
        {
            item.Clear();
            return true;
        }
    }
    #endregion

    #region Pooling\ObjectPool.cs
    public class ObjectPool<T> : BasePool<T> where T : BasePoolable, new()
    {
        public static readonly IPool<T> Instance;
        
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
    }
    #endregion

    #region Pooling\StringBuilderPool.cs
    public class StringBuilderPool : BasePool<StringBuilder>
    {
        public static readonly IPool<StringBuilder> Instance;
        
        static StringBuilderPool()
        {
            Instance = new StringBuilderPool();
        }
        
        private StringBuilderPool() : base(64) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Length = 0;
            return true;
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
        /// Frees a <see cref="StringBuilder"/> back to the pool returning the <see cref="string"/>
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> being freed</param>
        public static string ToStringAndFreeStringBuilder(ref StringBuilder sb)
        {
            string result = sb.ToString();
            FreeStringBuilder(ref sb);
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
                MoveRows(1);
            }
        }
        
        public void MoveCols(float cols)
        {
            XMin += cols / NumCols;
            XMax += cols / NumCols;
            
            if (XMax > 1)
            {
                XMin -= 1;
                XMax -= 1;
                MoveRows(1);
            }
        }
        
        public void MoveRows(int rows)
        {
            YMin -= rows / NumRows;
            YMax -= rows / NumRows;
        }
    }
    #endregion

    #region Positions\GridPositionBuilder.cs
    public class GridPositionBuilder
    {
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
            if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
            _numCols = numCols;
            _numRows = numRows;
        }
        
        public GridPositionBuilder SetRowHeight(int height)
        {
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            _rowHeight = height;
            return this;
        }
        
        public GridPositionBuilder SetRowOffset(int offset)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            _rowOffset = offset;
            return this;
        }
        
        public GridPositionBuilder SetColWidth(int width)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            _colWidth = width;
            return this;
        }
        
        public GridPositionBuilder SetColOffset(int offset)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
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
            float xMin = 0;
            float yMin = 1 - _rowHeight / _numRows;
            float xMax = _colWidth / _numCols;
            float yMax = 1;
            
            if (_colOffset != 0)
            {
                float size = _colOffset / _numCols;
                xMin += size;
                xMax += size;
            }
            
            if (_rowOffset != 0)
            {
                float size = _rowOffset / _numRows;
                yMin += size;
                yMax += size;
            }
            
            xMin += _xPad;
            xMax -= _xPad;
            yMin += _yPad;
            yMax -= _yPad;
            
            return new GridPosition(xMin, yMin, xMax, yMax, _numCols, _numRows);
        }
    }
    #endregion

    #region Positions\MovablePosition.cs
    public class MovablePosition
    {
        public float XMin;
        public float YMin;
        public float XMax;
        public float YMax;
        private readonly UiPosition _initialState;
        
        public MovablePosition(float xMin, float yMin, float xMax, float yMax)
        {
            XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
            _initialState = new UiPosition(XMin, YMin, XMax, YMax);
        }
        
        public UiPosition ToPosition()
        {
            return new UiPosition(XMin, YMin, XMax, YMax);
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
        }
        
        public void SetY(float yMin, float yMax)
        {
            YMin = yMin;
            YMax = yMax;
        }
        
        public void MoveX(float delta)
        {
            XMin += delta;
            XMax += delta;
        }
        
        public void MoveXPadded(float padding)
        {
            float spacing = (XMax - XMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            XMin += spacing;
            XMax += spacing;
        }
        
        public void MoveY(float delta)
        {
            YMin += delta;
            YMax += delta;
        }
        
        public void MoveYPadded(float padding)
        {
            float spacing = (YMax - YMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            YMin += spacing;
            YMax += spacing;
        }
        
        public void Expand(float amount)
        {
            ExpandHorizontal(amount);
            ExpandVertical(amount);
        }
        
        public void ExpandHorizontal(float amount)
        {
            XMin -= amount;
            XMax += amount;
        }
        
        public void ExpandVertical(float amount)
        {
            YMin -= amount;
            YMax += amount;
        }
        
        public void Shrink(float amount)
        {
            Expand(-amount);
        }
        
        public void ShrinkHorizontal(float amount)
        {
            ExpandHorizontal(-amount);
        }
        
        public void ShrinkVertical(float amount)
        {
            ExpandVertical(-amount);
        }
        
        public void Reset()
        {
            XMin = _initialState.Min.x;
            YMin = _initialState.Min.y;
            XMax = _initialState.Max.x;
            YMax = _initialState.Max.y;
        }
        
        public override string ToString()
        {
            return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
        }
        
        public static implicit operator UiPosition(MovablePosition pos) => pos.ToPosition();
    }
    #endregion

    #region Positions\UiPosition.cs
    public struct UiPosition
    {
        public static readonly UiPosition None = new UiPosition(0, 0, 0, 0);
        public static readonly UiPosition Full = new UiPosition(0, 0, 1, 1);
        public static readonly UiPosition HorizontalPaddedFull = SliceHorizontal(Full, 0.01f, 0.99f);
        public static readonly UiPosition VerticalPaddedFull = SliceVertical(Full, 0.01f, 0.99f);
        public static readonly UiPosition TopLeft = new UiPosition(0, 1, 0, 1);
        public static readonly UiPosition MiddleLeft = new UiPosition(0, .5f, 0, .5f);
        public static readonly UiPosition BottomLeft = new UiPosition(0, 0, 0, 0);
        public static readonly UiPosition TopMiddle = new UiPosition(.5f, 1, .5f, 1);
        public static readonly UiPosition MiddleMiddle = new UiPosition(.5f, .5f, .5f, .5f);
        public static readonly UiPosition BottomMiddle = new UiPosition(.5f, 0, .5f, 0);
        public static readonly UiPosition TopRight = new UiPosition(1, 1, 1, 1);
        public static readonly UiPosition MiddleRight = new UiPosition(1, .5f, 1, .5f);
        public static readonly UiPosition BottomRight = new UiPosition(1, 0, 1, 0);
        
        public static readonly UiPosition Top = new UiPosition(0, 1, 1, 1);
        public static readonly UiPosition Bottom = new UiPosition(0, 0, 1, 0);
        public static readonly UiPosition Left = new UiPosition(0, 0, 0, 1);
        public static readonly UiPosition Right = new UiPosition(1, 0, 1, 1);
        
        public static readonly UiPosition LeftHalf = new UiPosition(0, 0, 0.5f, 1);
        public static readonly UiPosition TopHalf = new UiPosition(0, 0.5f, 1, 1);
        public static readonly UiPosition RightHalf = new UiPosition(0.5f, 0, 1, 1);
        public static readonly UiPosition BottomHalf = new UiPosition(0, 0, 1, 0.5f);
        
        public Vector2 Min;
        public Vector2 Max;
        
        public UiPosition(float xMin, float yMin, float xMax, float yMax)
        {
            Min = new Vector2(xMin, yMin);
            Max = new Vector2(xMax, yMax);
        }
        
        /// <summary>
        /// Returns a slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition Slice(UiPosition pos, float xMin, float yMin, float xMax, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            Vector2 distance = max - min;
            return new UiPosition(min.x + distance.x * xMin, min.y + distance.y * yMin, min.x + distance.x * xMax, min.y + distance.y * yMax);
        }
        
        /// <summary>
        /// Returns a horizontal slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition SliceHorizontal(UiPosition pos, float xMin, float xMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + (max.x - min.x) * xMin, min.y, min.x + (max.x - min.x) * xMax, max.y);
        }
        
        /// <summary>
        /// Returns a vertical slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition SliceVertical(UiPosition pos, float yMin, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(max.x, min.y + (max.y - min.y) * yMin, max.x, min.y + (max.y - min.y) * yMax);
        }
        
        public override string ToString()
        {
            return $"({Min.x:0.####}, {Min.y:0.####}) ({Max.x:0.####}, {Max.y:0.####})";
        }
    }
    #endregion

    #region UiElements\BaseUiComponent.cs
    public abstract class BaseUiComponent : BasePoolable
    {
        public string Name;
        public string Parent;
        public float FadeOut;
        public UiPosition Position;
        public UiOffset Offset;
        
        protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
        {
            T component = UiFrameworkPool.Get<T>();
            component.Position = pos;
            component.Offset = offset;
            return component;
        }
        
        public void WriteRootComponent(JsonFrameworkWriter writer, bool needsMouse, bool needsKeyboard)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Name);
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Parent);
            writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
            
            writer.WritePropertyName("components");
            writer.WriteStartArray();
            WriteComponents(writer);
            
            if (needsMouse)
            {
                writer.AddMouse();
            }
            
            if (needsKeyboard)
            {
                writer.AddKeyboard();
            }
            
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        
        public void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Name);
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Parent);
            writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
            
            writer.WritePropertyName("components");
            writer.WriteStartArray();
            WriteComponents(writer);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        
        protected virtual void WriteComponents(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
            writer.AddPosition(JsonDefaults.Position.AnchorMinName, Position.Min, new Vector2(0, 0));
            writer.AddPosition(JsonDefaults.Position.AnchorMaxName, Position.Max, new Vector2(1, 1));
            writer.AddOffset(JsonDefaults.Offset.OffsetMinName, Offset.Min, new Vector2(0, 0));
            writer.AddOffset(JsonDefaults.Offset.OffsetMaxName, Offset.Max, new Vector2(1, 1));
            writer.WriteEndObject();
        }
        
        public void SetFadeOut(float duration)
        {
            FadeOut = duration;
        }
        
        protected override void EnterPool()
        {
            Name = null;
            Parent = null;
            FadeOut = 0;
            Position = default(UiPosition);
            Offset = default(UiOffset);
        }
    }
    #endregion

    #region UiElements\BaseUiImage.cs
    public abstract class BaseUiImage : BaseUiComponent
    {
        public ImageComponent Image;
        
        public void SetImageType(Image.Type type)
        {
            Image.ImageType = type;
        }
        
        public void SetSprite(string sprite)
        {
            Image.Sprite = sprite;
        }
        
        public void SetMaterial(string material)
        {
            Image.Material = material;
        }
        
        public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = UnityEngine.UI.Image.Type.Simple)
        {
            Image.Sprite = sprite;
            Image.Material = material;
            Image.ImageType = type;
        }
        
        public void SetFadeIn(float duration)
        {
            Image.FadeIn = duration;
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
        
        public static UiButton CreateCommand(UiPosition pos, UiOffset offset, UiColor color, string command)
        {
            UiButton button = CreateBase<UiButton>(pos, offset);
            button.Button.Color = color;
            button.Button.Command = command;
            return button;
        }
        
        public static UiButton CreateClose(UiPosition pos, UiOffset offset, UiColor color, string close)
        {
            UiButton button = CreateBase<UiButton>(pos, offset);
            button.Button.Color = color;
            button.Button.Close = close;
            return button;
        }
        
        public void SetFadeIn(float duration)
        {
            Button.FadeIn = duration;
        }
        
        public void SetImageType(Image.Type type)
        {
            Button.ImageType = type;
        }
        
        public void SetSprite(string sprite)
        {
            Button.Sprite = sprite;
        }
        
        public void SetMaterial(string material)
        {
            Button.Material = material;
        }
        
        public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
        {
            Button.Sprite = sprite;
            Button.Material = material;
            Button.ImageType = type;
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
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region UiElements\UiImage.cs
    public class UiImage : BaseUiImage
    {
        public static UiImage CreateFileImage(UiPosition pos, UiOffset offset, UiColor color, string png)
        {
            UiImage image = CreateBase<UiImage>(pos, offset);
            image.Image.Color = color;
            image.Image.Png = png;
            return image;
        }
        
        public static UiImage CreateSpriteImage(UiPosition pos, UiOffset offset, UiColor color, string sprite)
        {
            UiImage image = CreateBase<UiImage>(pos, offset);
            image.Image.Color = color;
            image.Image.Sprite = sprite;
            return image;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region UiElements\UiInput.cs
    public class UiInput : BaseUiTextOutline
    {
        public InputComponent Input;
        
        public static UiInput Create(UiPosition pos, UiOffset offset, UiColor textColor, string text, int size, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = CreateBase<UiInput>(pos, offset);
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
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region UiElements\UiItemIcon.cs
    public class UiItemIcon : BaseUiComponent
    {
        public ItemIconComponent Icon;
        
        public static UiItemIcon Create(UiPosition pos, UiOffset offset, UiColor color, int itemId, ulong skinId = 0)
        {
            UiItemIcon icon = CreateBase<UiItemIcon>(pos, offset);
            icon.Icon.Color = color;
            icon.Icon.ItemId = itemId;
            icon.Icon.SkinId = skinId;
            return icon;
        }
        
        public void SetFadeIn(float duration)
        {
            Icon.FadeIn = duration;
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
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region UiElements\UiLabel.cs
    public class UiLabel : BaseUiTextOutline
    {
        public TextComponent Text;
        public CountdownComponent Countdown;
        
        public static UiLabel Create(UiPosition pos, UiOffset offset, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = CreateBase<UiLabel>(pos, offset);
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
        
        public void SetFadeIn(float duration)
        {
            Text.FadeIn = duration;
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
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region UiElements\UiPanel.cs
    public class UiPanel : BaseUiImage
    {
        public static UiPanel Create(UiPosition pos, UiOffset offset, UiColor color)
        {
            UiPanel panel = CreateBase<UiPanel>(pos, offset);
            panel.Image.Color = color;
            return panel;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region UiElements\UiRawImage.cs
    public class UiRawImage : BaseUiComponent
    {
        public RawImageComponent RawImage;
        
        public static UiRawImage CreateUrl(UiPosition pos, UiOffset offset, UiColor color, string url)
        {
            UiRawImage image = CreateBase<UiRawImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Url = url;
            return image;
        }
        
        public static UiRawImage CreateTexture(UiPosition pos, UiOffset offset, UiColor color, string icon)
        {
            UiRawImage image = CreateBase<UiRawImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Texture = icon;
            return image;
        }
        
        public void SetMaterial(string material)
        {
            RawImage.Material = material;
        }
        
        public void SetFadeIn(float duration)
        {
            RawImage.FadeIn = duration;
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
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region UiElements\UiSection.cs
    public class UiSection : BaseUiComponent
    {
        public static UiSection Create(UiPosition pos, UiOffset offset)
        {
            UiSection panel = CreateBase<UiSection>(pos, offset);
            return panel;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
    #endregion

    #region Pooling\ArrayPool\UiFrameworkArrayPool.cs
    public class UiFrameworkArrayPool<T>
    {
        public static readonly UiFrameworkArrayPool<T> Shared;
        
        private const int DefaultMaxArrayLength = 1024 * 16;
        private const int DefaultMaxNumberOfArraysPerBucket = 50;
        
        private readonly Bucket[] _buckets;
        
        static UiFrameworkArrayPool()
        {
            Shared = new UiFrameworkArrayPool<T>();
        }
        
        private UiFrameworkArrayPool() : this(DefaultMaxArrayLength, DefaultMaxNumberOfArraysPerBucket) { }
        
        private UiFrameworkArrayPool(int maxArrayLength, int maxArraysPerBucket)
        {
            if (maxArrayLength <= 0) throw new ArgumentOutOfRangeException(nameof(maxArrayLength));
            if (maxArraysPerBucket <= 0) throw new ArgumentOutOfRangeException(nameof(maxArraysPerBucket));
            
            maxArrayLength = Mathf.Clamp(maxArrayLength, 16, DefaultMaxArrayLength);
            
            _buckets = new Bucket[SelectBucketIndex(maxArrayLength) + 1];
            for (int i = 0; i < _buckets.Length; i++)
            {
                _buckets[i] = new Bucket(GetMaxSizeForBucket(i), maxArraysPerBucket);
            }
        }
        
        public T[] Rent(int minimumLength)
        {
            if (minimumLength < 0)  throw new ArgumentOutOfRangeException(nameof(minimumLength));
            
            if (minimumLength == 0)
            {
                return Array.Empty<T>();
            }
            
            T[] array;
            int bucketIndex = SelectBucketIndex(minimumLength);
            int index = bucketIndex;
            do
            {
                array = _buckets[index].Rent();
                if (array != null)
                {
                    return array;
                }
                
                index++;
            }
            while (index < _buckets.Length && index != bucketIndex + 2);
            array = new T[_buckets[bucketIndex].BufferLength];
            return array;
        }
        
        public void Return(T[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Length == 0)
            {
                return;
            }
            
            int num = SelectBucketIndex(array.Length);
            if (num < _buckets.Length)
            {
                _buckets[num].Return(array);
            }
        }
        
        private static int GetMaxSizeForBucket(int binIndex)
        {
            return 16 << (binIndex & 31);
        }
        
        private static int SelectBucketIndex(int bufferSize)
        {
            uint num = (uint)(bufferSize - 1 >> 4);
            int num1 = 0;
            if (num > 255)
            {
                num >>= 8;
                num1 += 8;
            }
            if (num > 15)
            {
                num >>= 4;
                num1 += 4;
            }
            if (num > 3)
            {
                num >>= 2;
                num1 += 2;
            }
            if (num > 1)
            {
                num >>= 1;
                num1++;
            }
            return (int)(num1 + num);
        }
        
        private void ClearInternal()
        {
            for (int index = 0; index < _buckets.Length; index++)
            {
                _buckets[index].Clear();
            }
        }
        
        public static void Clear()
        {
            Shared.ClearInternal();
        }
        
        private sealed class Bucket
        {
            internal readonly int BufferLength;
            
            private readonly T[][] _buffers;
            
            private int _index;
            
            internal Bucket(int bufferLength, int numberOfBuffers)
            {
                _buffers = new T[numberOfBuffers][];
                BufferLength = bufferLength;
            }
            
            internal T[] Rent()
            {
                if (_index < _buffers.Length)
                {
                    T[] array = _buffers[_index];
                    _buffers[_index] = null;
                    _index++;
                    if (array != null)
                    {
                        return array;
                    }
                }
                return new T[BufferLength];
            }
            
            internal void Return(T[] array)
            {
                if (array.Length != BufferLength)  throw new ArgumentException("Buffer not from pool", nameof(array));
                if (_index != 0)
                {
                    _index--;
                    _buffers[_index] = array;
                }
            }
            
            public void Clear()
            {
                for (int index = 0; index < _buffers.Length; index++)
                {
                    _buffers[index] = null;
                }
                
                _index = 0;
            }
        }
    }
    #endregion

}
