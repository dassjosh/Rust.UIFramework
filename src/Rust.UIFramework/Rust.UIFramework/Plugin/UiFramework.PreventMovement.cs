using UnityEngine;

namespace UI.Framework.Rust.Plugin
{
    public partial class UiFramework
    {
        private const string ChairPrefab = "assets/prefabs/vehicle/seats/standingdriver.prefab";
        
        private uint _prefabId;

        public void InitPreventMovement()
        {
            if (_prefabId == 0)
            {
                _prefabId = StringPool.Get(ChairPrefab);
            }
        }
        
        public void PreventMovement(BasePlayer player)
        {
            InitPreventMovement();

            BaseMountable currentMount = player.GetMounted();
            if (currentMount != null)
            {
                return;
            }

            Transform transform = player.transform;
            BaseVehicleSeat chair = GameManager.server.CreateEntity(ChairPrefab, transform.position, transform.rotation) as BaseVehicleSeat;
            chair.Spawn();
            player.MountObject(chair);
        }

        public void AllowMovement(BasePlayer player)
        {
            InitPreventMovement();
            BaseMountable mounted = player.GetMounted();
            if (mounted != null && mounted.prefabID == _prefabId && mounted.parentEntity.uid == 0)
            {
                player.DismountObject();
                mounted.Kill();
            }
        }
    }
}