using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Net = Network.Net;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        #region Destroy & Add UI
        public void DestroyAndAddUi(BasePlayer player)
        {
            DestroyAndAddUi(new SendInfo(player.Connection));
        }

        public void DestroyAndAddUi(Connection connection)
        {
            DestroyAndAddUi(new SendInfo(connection));
        }

        public void DestroyAndAddUi(List<Connection> connections)
        {
            DestroyAndAddUi(new SendInfo(connections));
        }

        public void DestroyAndAddUi()
        {
            DestroyAndAddUi(new SendInfo(Net.sv.connections));
        }

        private void DestroyAndAddUi(SendInfo send)
        {
            JsonFrameworkWriter writer = CreateWriter();
            DestroyUi(send, _rootName);
            AddUi(send, writer);
            UiFrameworkPool.Free(ref writer);
        }
        #endregion
    }
}