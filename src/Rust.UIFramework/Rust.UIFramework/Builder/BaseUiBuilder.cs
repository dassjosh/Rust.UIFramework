using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Builder
{
    public abstract class BaseUiBuilder : BasePoolable
    {
        protected string RootName;

        public string GetRootName() => RootName;
        
        public abstract byte[] GetBytes();
        
        /// <summary>
        /// Warning this is only recommend to use for debugging purposes
        /// </summary>
        /// <returns></returns>
        public string GetJsonString() => Encoding.UTF8.GetString(GetBytes());
        
        #region Add UI
        public void AddUi(BasePlayer player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            AddUi(new SendInfo(player.Connection));
        }

        public void AddUi(Connection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            AddUi(new SendInfo(connection));
        }

        public void AddUi(List<Connection> connections)
        {
            if (connections == null) throw new ArgumentNullException(nameof(connections));
            AddUi(new SendInfo(connections));
        }

        public void AddUi()
        {
            AddUi(new SendInfo(Net.sv.connections));
        }

        protected abstract void AddUi(SendInfo send);

        protected void AddUi(SendInfo send, JsonFrameworkWriter writer)
        {
            if (ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc))
            {
                writer.WriteToNetwork();
                Net.sv.write.Send(send);
            }
        }
        
        protected void AddUi(SendInfo send, byte[] bytes)
        {
            if (ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc))
            {
                Net.sv.write.BytesWithSize(bytes);
                Net.sv.write.Send(send);
            }
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

        #region Destroy UI
        public void DestroyUi(BasePlayer player)
        {
            if (!player) throw new ArgumentNullException(nameof(player));
            DestroyUi(player, RootName);
        }

        public void DestroyUi(Connection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            DestroyUi(new SendInfo(connection), RootName);
        }

        public void DestroyUi(List<Connection> connections)
        {
            if (connections == null) throw new ArgumentNullException(nameof(connections));
            DestroyUi(new SendInfo(connections), RootName);
        }

        public void DestroyUi()
        {
            DestroyUi(RootName);
        }
        
        public static void DestroyUi(BasePlayer player, string name)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
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
        #endregion
    }
}