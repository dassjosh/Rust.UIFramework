using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Net = Network.Net;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder : IDisposable
    {
        public BaseUiComponent Root;

        private bool _needsMouse;
        private bool _needsKeyboard;
        private bool _disposed;
        
        private string _rootName;
        private string _componentBaseName;
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
            _rootName = name;
            _componentBaseName = name + "_";
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
            //ReSharper disable once RedundantNameQualifier
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

        #region JSON
        public string ToJson()
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

            return writer.ToJson();
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
            DestroyUi(player, _rootName);
        }

        public void DestroyUi(Connection connection)
        {
            DestroyUi(connection, _rootName);
        }

        public void DestroyUi(List<Connection> connections)
        {
            DestroyUi(connections, _rootName);
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
        
        #region Destroy & Add UI
        public void DestroyAndAddUi(BasePlayer player)
        {
            DestroyAndAddUi(player.Connection);
        }

        public void DestroyAndAddUi(Connection connection)
        {
            string json = ToJson();
            DestroyUi(connection, _rootName);
            AddUi(connection, json);
        }

        public void DestroyAndAddUi(List<Connection> connections)
        {
            string json = ToJson();
            DestroyUi(connections, _rootName);
            AddUi(connections, json);
        }

        public void DestroyAndAddUi()
        {
            string json = ToJson();
            DestroyUi(Net.sv.connections, _rootName);
            AddUi(Net.sv.connections, json);
        }
        #endregion
    }
}