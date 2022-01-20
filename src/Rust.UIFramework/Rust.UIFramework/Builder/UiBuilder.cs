using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Oxide.Plugins;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Enums;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using UI.Framework.Rust.UiElements;
using UnityEngine;
using Pool = Facepunch.Pool;
using Net = Network.Net;

namespace UI.Framework.Rust.Builder
{
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

        public UiBuilder(UiColor color, UiPosition pos, bool useCursor, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, null, useCursor, name, Constants.UiLayers.GetLayer(parent))
        {
        }

        public UiBuilder(UiColor color, UiPosition pos, UiOffset offset, bool useCursor, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, offset, useCursor, name, Constants.UiLayers.GetLayer(parent))
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
            _font = Constants.UiFonts.GetUiFont(font);
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
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, Constants.RpcFunctions.AddUiFunc, json);
        }

        public static void AddUi(List<Connection> connections, string json)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, Constants.RpcFunctions.AddUiFunc, json);
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
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, Constants.RpcFunctions.DestroyUiFunc, name);
        }

        public static void DestroyUi(List<Connection> connections, string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, Constants.RpcFunctions.DestroyUiFunc, name);
        }
        #endregion
    }
}