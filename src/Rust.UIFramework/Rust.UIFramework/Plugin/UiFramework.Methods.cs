using System.Collections.Generic;
using Network;

namespace UI.Framework.Rust.Plugin
{
    public partial class UiFramework
    {
        #region JSON Sending
        public void DestroyUi(BasePlayer player, string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(player.Connection), null, "DestroyUI", name);
        }

        public void DestroyUi(List<Connection> connections, string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, "DestroyUI", name);
        }

        private void DestroyUiAll(string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(Net.sv.connections), null, "DestroyUI", name);
        }
        #endregion
    }
}