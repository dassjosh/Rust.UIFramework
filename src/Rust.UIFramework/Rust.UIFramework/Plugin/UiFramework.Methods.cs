using System.Collections.Generic;
using Network;
using UI.Framework.Rust.Builder;

namespace UI.Framework.Rust.Plugin
{
    public partial class UiFramework
    {
        #region JSON Sending
        public void DestroyUi(BasePlayer player, string name)
        {
            UiBuilder.DestroyUi(player, name);
        }

        public void DestroyUi(List<Connection> connections, string name)
        {
            UiBuilder.DestroyUi(connections, name);
        }

        private void DestroyUiAll(string name)
        {
            UiBuilder.DestroyUi(name);
        }
        #endregion
    }
}