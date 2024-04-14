using System;
using System.Collections.Generic;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Builder
{
    public abstract class BaseBuilder : BasePoolable
    {
        protected string RootName;

        public string GetRootName() => RootName;
        #region Add UI
        public void AddUi(BasePlayer player)
        {
            if (!player) throw new ArgumentNullException(nameof(player));
            AddUi(new SendInfo(player.Connection));
        }

        public void AddUi(Connection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            AddUi(new SendInfo(connection));
        }

        /// <summary>
        /// Sends ui to players asynchronously and returns the builder to the pool.
        /// </summary>
        /// <param name="connections">All connections to send the ui to.</param>
        /// <param name="freeConnections">Whether or not to return the connections list to the pool.</param>
        public void AddUi(List<Connection> connections, bool freeConnections = false)
        {
            if (connections == null) throw new ArgumentNullException(nameof(connections));
            AddUi(new SendInfo(connections), freeConnections);
        }

        public void AddUi()
        {
            AddUi(new SendInfo(Net.sv.connections));
        }

        protected abstract void AddUi(SendInfo send);
        protected abstract void AddUi(SendInfo send, bool freeConnections);

        internal void AddUi(SendInfo send, JsonFrameworkWriter writer)
        {
            NetWrite write = ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc);
            if (write != null)
            {
                writer.WriteToNetwork(write);
                write.Send(send);
            }
        }
        
        protected void AddUi(SendInfo send, byte[] bytes)
        {
            NetWrite write = ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc);
            if (write != null)
            {
                write.BytesWithSize(bytes);
                write.Send(send);
            }
        }

        private static NetWrite ClientRPCStart(string funcName)
        {
            if (!Net.sv.IsConnected() || CommunityEntity.ServerInstance.net == null)
            {
                return null;
            }

            NetWrite write = Net.sv.StartWrite();
            write.PacketID(Message.Type.RPCMessage);
            write.EntityID(CommunityEntity.ServerInstance.net.ID);
            write.UInt32(StringPool.Get(funcName));
            return write;
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
            if (!player) throw new ArgumentNullException(nameof(player));
            DestroyUi(new SendInfo(player.Connection), name);
        }

        public static void DestroyUi(string name)
        {
            DestroyUi(new SendInfo(Net.sv.connections), name);
        }

        public static void DestroyUi(SendInfo send, string name)
        {
            CommunityEntity.ServerInstance.ClientRPC(new RpcTarget
            {
                Function = UiConstants.RpcFunctions.DestroyUiFunc,
                Connections = send
            }, name);
        }
        #endregion

        #region JSON
                        
        public abstract byte[] GetBytes();
        
        /// <summary>
        /// Warning this is only recommend to use for debugging purposes
        /// </summary>
        /// <returns></returns>
        public string GetJsonString() => Encoding.UTF8.GetString(GetBytes());
        #endregion

        #region Pooling
        protected override void EnterPool()
        {
            RootName = null;
        }
        #endregion
    }
}