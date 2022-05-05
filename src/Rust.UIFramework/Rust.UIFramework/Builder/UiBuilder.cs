using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;
using Net = Network.Net;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder : IDisposable
    {
        public BaseUiComponent Root;

        private bool _needsMouse;
        private bool _needsKeyboard;
        private bool _disposed;
        
        private string _baseName;
        private string _font;
        private string _cachedJson;
        
        private List<BaseUiComponent> _components;
        //private Hash<string, BaseUiComponent> _componentLookup;
        
        private static string _globalFont;

        #region Constructor
        static UiBuilder()
        {
            SetGlobalFont(UiFont.RobotoCondensedRegular);
        }
        
        public UiBuilder(UiPosition pos, string name, string parent) : this(pos, null, name, parent) { }

        public UiBuilder(UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) : this(pos, null, name, UiConstants.UiLayers.GetLayer(parent)) { }

        public UiBuilder(UiPosition pos, UiOffset? offset, string name, UiLayer parent = UiLayer.Overlay) : this(pos, offset, name, UiConstants.UiLayers.GetLayer(parent)) { }

        public UiBuilder(UiPosition pos, UiOffset? offset, string name, string parent) : this(UiSection.Create(pos, offset), name, parent) { } 
        
        public UiBuilder(UiColor color, UiPosition pos, string name, string parent) : this(color, pos, null, name, parent) { }

        public UiBuilder(UiColor color, UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, null, name, UiConstants.UiLayers.GetLayer(parent)) { }

        public UiBuilder(UiColor color, UiPosition pos, UiOffset? offset, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, offset, name, UiConstants.UiLayers.GetLayer(parent)) { }

        public UiBuilder(UiColor color, UiPosition pos, UiOffset? offset, string name, string parent) : this(UiPanel.Create(pos, offset, color), name, parent) { }
        
        public UiBuilder(BaseUiComponent root, string name, string parent) : this()
        {
            SetRoot(root, name, parent);
        }

        public UiBuilder()
        {
            _components = UiFrameworkPool.GetList<BaseUiComponent>();
            //_componentLookup = UiFrameworkPool.GetHash<string, BaseUiComponent>();
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
            _baseName = name + "_";
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
            _font = UiConstants.UiFonts.GetUiFont(font);
        }
        
        public static void SetGlobalFont(UiFont font)
        {
            _globalFont = UiConstants.UiFonts.GetUiFont(font);
        }

        // public T GetUi<T>(string name) where T : BaseUiComponent
        // {
        //     return (T)_componentLookup[name];
        // }
        #endregion

        #region Decontructor
        ~UiBuilder()
        {
            DisposeInternal();
        }

        public void Dispose()
        {
            DisposeInternal();
            //Need this because there is a global GC class that causes issues
            // ReSharper disable once RedundantNameQualifier
            System.GC.SuppressFinalize(this);
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
            //UiFrameworkPool.FreeHash(ref _componentLookup);
            Root = null;
        }
        #endregion

        #region Add UI
        public void AddComponent(BaseUiComponent component, BaseUiComponent parent)
        {
            component.Parent = parent.Name;
            component.Name = GetComponentName();
            //_componentLookup[component.Name] = component;
            _components.Add(component);
        }

        public string GetComponentName()
        {
            return string.Concat(_baseName, _components.Count.ToString());
        }

        public UiSection Section(BaseUiComponent parent, UiPosition pos)
        {
            UiSection section = UiSection.Create(pos, null);
            AddComponent(section, parent);
            return section;
        }

        public UiPanel Panel(BaseUiComponent parent, UiColor color, UiPosition pos, UiOffset? offset = null)
        {
            UiPanel panel = UiPanel.Create(pos, offset, color);
            AddComponent(panel, parent);
            return panel;
        }

        public UiButton EmptyCommandButton(BaseUiComponent parent, UiColor color, UiPosition pos, string cmd)
        {
            UiButton button = UiButton.CreateCommand(pos, null, color, cmd);
            AddComponent(button, parent);
            return button;
        }

        public UiButton EmptyCloseButton(BaseUiComponent parent, UiColor color, UiPosition pos, string close)
        {
            UiButton button = UiButton.CreateClose(pos, null, color, close);
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
            WebImage(button, url, UiPosition.FullPosition);
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

            UiImage image = UiImage.Create(pos, null, color, png);
            AddComponent(image, parent);
            return image;
        }

        public UiImage Image(BaseUiComponent parent, string png, UiPosition pos)
        {
            return Image(parent, png, pos, UiColors.StandardColors.White);
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos, UiColor color)
        {
            UiItemIcon image = UiItemIcon.Create(pos, null, color, itemId);
            AddComponent(image, parent);
            return image;
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos)
        {
            return ItemIcon(parent, itemId, pos, UiColors.StandardColors.White);
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos, UiColor color)
        {
            UiItemIcon image = UiItemIcon.Create(pos, null, color, itemId, skinId);
            AddComponent(image, parent);
            return image;
        }

        public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos)
        {
            return ItemIcon(parent, itemId, skinId, pos, UiColors.StandardColors.White);
        }

        public UiRawImage WebImage(BaseUiComponent parent, string url, UiPosition pos)
        {
            return WebImage(parent, url, pos, UiColors.StandardColors.White);
        }
        
        public UiRawImage WebImage(BaseUiComponent parent, string url, UiPosition pos, UiColor color)
        {
            if (!url.StartsWith("http"))
            {
                throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use Image instead");
            }

            UiRawImage image = UiRawImage.CreateUrl(pos, null, color, url);
            AddComponent(image, parent);
            return image;
        }
        
        public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiPosition pos)
        {
            return TextureImage(parent, texture, pos, UiColors.StandardColors.White);
        }
        
        public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiPosition pos, UiColor color)
        {
            UiRawImage image = UiRawImage.CreateTexture(pos, null, color, texture);
            AddComponent(image, parent);
            return image;
        }

        public UiLabel Label(BaseUiComponent parent, string text, int size, UiColor textColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = UiLabel.Create(pos, null, textColor, text, size, _font, align);
            AddComponent(label, parent);
            return label;
        }

        public UiLabel LabelBackground(BaseUiComponent parent, string text, int size, UiColor textColor, UiColor backgroundColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiPanel panel = Panel(parent, backgroundColor, pos);
            UiLabel label = UiLabel.Create(UiPosition.FullPosition, null, textColor, text, size, _font, align);
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

        public UiInput Input(BaseUiComponent parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            parent = Panel(parent, backgroundColor, pos);
            UiInput input = UiInput.Create(UiPosition.FullPosition, null, textColor, text, fontSize, cmd, _font, align, charsLimit, isPassword, readOnly, lineType);
            AddComponent(input, parent);
            return input;
        }

        public void Border(BaseUiComponent parent, UiColor color, int width = 0, BorderMode border = BorderMode.Top | BorderMode.Bottom | BorderMode.Left | BorderMode.Right)
        {
            if (HasBorderFlag(border, BorderMode.Top))
            {
                UiPanel panel = UiPanel.Create(UiPosition.Top, new UiOffset(0, -1, 0, width), color);
                AddComponent(panel, parent);
            }

            if (HasBorderFlag(border, BorderMode.Left))
            {
                UiPanel panel = UiPanel.Create(UiPosition.Left, new UiOffset(-width, -width, 1, 0), color);
                AddComponent(panel, parent);
            }

            if (HasBorderFlag(border, BorderMode.Bottom))
            {
                UiPanel panel = UiPanel.Create(UiPosition.Bottom, new UiOffset(0, -width, 0, 1), color);
                AddComponent(panel, parent);
            }

            if (HasBorderFlag(border, BorderMode.Right))
            {
                UiPanel panel = UiPanel.Create(UiPosition.Right, new UiOffset(-1, 0, width, 0), color);
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
                if (component is UiRawImage)
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
                if (component is UiRawImage)
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
}