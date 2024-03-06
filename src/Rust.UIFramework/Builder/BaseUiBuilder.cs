using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Callbacks;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
    public abstract partial class BaseUiBuilder : BaseBuilder
    {
        protected readonly List<BaseUiComponent> Components = new();
        protected readonly List<BaseUiControl> Controls = new();
        protected readonly List<BaseUiComponent> Anchors = new();
        
        protected string Font;
        protected static string GlobalFont;

        static BaseUiBuilder()
        {
            SetGlobalFont(UiFont.RobotoCondensedRegular);
        }
        
        public void EnsureCapacity(int capacity)
        {
            if (Components.Capacity < capacity)
            {
                Components.Capacity = capacity;
            }
        }
        
        public void SetCurrentFont(UiFont font)
        {
            Font = UiFontCache.GetUiFont(font);
        }
        
        public static void SetGlobalFont(UiFont font)
        {
            GlobalFont = UiFontCache.GetUiFont(font);
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
            SendUiCallback.Start(this, send);
        }
        
        public JsonFrameworkWriter CreateWriter()
        {
            int count = Controls.Count;
            if (count != 0)
            {
                for (int index = 0; index < count; index++)
                {
                    BaseUiControl control = Controls[index];
                    control.RenderControl(this);
                }
            }
            
            JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
            writer.WriteStartArray();
            WriteComponentsInternal(writer);
            writer.WriteEndArray();
            return writer;
        }

        protected abstract void WriteComponentsInternal(JsonFrameworkWriter writer);
        
        protected override void EnterPool()
        {
            base.EnterPool();
            FreeComponents();
            Font = null;
        }

        private void FreeComponents()
        {
            int count = Components.Count;
            for (int index = 0; index < count; index++)
            {
                Components[index].Dispose();
            }
                
            Components.Clear();

            count = Controls.Count;
            for (int index = 0; index < count; index++)
            {
                Controls[index].Dispose();
            }
                
            Controls.Clear();
            
            count = Anchors.Count;
            for (int index = 0; index < count; index++)
            {
                Anchors[index].Dispose();
            }

            Anchors.Clear();
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            Font = GlobalFont;
        }
    }
}