using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Net = Network.Net;

namespace Oxide.Ext.UiFramework.Builder
{
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
}