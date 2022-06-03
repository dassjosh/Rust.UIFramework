using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
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

            if (_components != null)
            {
                _components[0].WriteRootComponent(writer, _needsMouse, _needsKeyboard);

                int count = _components.Count;
                for (int index = 1; index < count; index++)
                {
                    _components[index].WriteComponent(writer);
                }
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
}