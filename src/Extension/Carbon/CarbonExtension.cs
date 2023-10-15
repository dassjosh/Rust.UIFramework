#if CARBON
using System;
using API.Assembly;
using Carbon;

namespace Oxide.Ext.UiFramework
{
    public class CarbonExtension : ICarbonExtension
    {
        public void Awake(EventArgs args)
        {
            Logger.Log("Carbon.Ext.UiFramework Awake");
        }

        public void OnLoaded(EventArgs args)
        {
            Logger.Log("Carbon.Ext.UiFramework Loaded");
        }

        public void OnUnloaded(EventArgs args)
        {
            Logger.Log("Carbon.Ext.UiFramework Unloaded");
        }
    }
}
#endif