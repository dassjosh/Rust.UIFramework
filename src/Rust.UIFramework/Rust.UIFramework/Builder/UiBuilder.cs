using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder : BaseUiBuilder
    {
        public BaseUiComponent Root;

        private bool _needsMouse;
        private bool _needsKeyboard;
        private bool _autoDestroy = true;
        
        private string _font;

        private List<BaseUiComponent> _components;
        private List<BaseUiControl> _controls;
        private List<BaseUiComponent> _anchors;

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
            RootName = name;
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

        public void EnableAutoDestroy(bool enabled = true)
        {
            _autoDestroy = enabled;
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

        protected override void EnterPool()
        {
            FreeComponents();

            Root = null;
            _needsKeyboard = false;
            _needsMouse = false;
            _font = null;
            RootName = null;
            _autoDestroy = true;
        }

        private void FreeComponents()
        {
            if (_components != null)
            {
                int count = _components.Count;
                for (int index = 0; index < count; index++)
                {
                    _components[index].Dispose();
                }
                
                UiFrameworkPool.FreeList(_components);
            }

            if (_controls != null)
            {
                int count = _controls.Count;
                for (int index = 0; index < count; index++)
                {
                    _controls[index].Dispose();
                }
                
                UiFrameworkPool.FreeList(_controls);
            }

            if (_anchors != null)
            {
                int count = _anchors.Count;
                for (int index = 0; index < count; index++)
                {
                    _anchors[index].Dispose();
                }

                UiFrameworkPool.FreeList(_anchors);
            }
        }

        protected override void LeavePool()
        {
            _components = UiFrameworkPool.GetList<BaseUiComponent>();
            _controls = UiFrameworkPool.GetList<BaseUiControl>();
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

            if (_components == null)
            {
                throw new Exception("Components List is null. Was UiBuilder not created from pool?");
            }

            if (_controls == null)
            {
                throw new Exception("Controls List is null. Was UiBuilder not created from pool?");
            }
            
            int count;
            if (_controls.Count != 0)
            {
                count = _controls.Count;
                for (int index = 0; index < count; index++)
                {
                    BaseUiControl control = _controls[index];
                    control.RenderControl(this);
                }
            }

            _components[0].WriteRootComponent(writer, _needsMouse, _needsKeyboard, _autoDestroy);

            count = _components.Count;
            for (int index = 1; index < count; index++)
            {
                _components[index].WriteComponent(writer);
            }


            if (_anchors != null)
            {
                count = _anchors.Count;
                for (int index = 0; index < count; index++)
                {
                    _anchors[index].WriteComponent(writer);
                }
            }

            writer.WriteEndArray();
            return writer;
        }

        public CachedUiBuilder ToCachedBuilder()
        {
            CachedUiBuilder cached = CachedUiBuilder.CreateCachedBuilder(this);
            if (!Disposed)
            {
                Dispose();
            }
            return cached;
        }
        
        public override byte[] GetBytes()
        {
            JsonFrameworkWriter writer = CreateWriter();
            byte[] bytes = writer.ToArray();
            writer.Dispose();
            return bytes;
        }
        
        protected override void AddUi(SendInfo send)
        {
            JsonFrameworkWriter writer = CreateWriter();
            AddUi(send, writer);
            writer.Dispose();
            if (!Disposed)
            {
                Dispose();
            }
        }
        #endregion
    }
}