using UnityEngine;

namespace UI.Framework.Rust.Plugin
{
    public partial class UiFramework
    {
        private const string ChairPrefab = "assets/prefabs/vehicle/seats/standingdriver.prefab";
        private const ulong PreventMovementSkinId = ulong.MaxValue - 51234; 
        
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
            chair.skinID = PreventMovementSkinId;
            chair.Spawn();
            player.MountObject(chair);
        }

        public void AllowMovement(BasePlayer player)
        {
            InitPreventMovement();
            BaseMountable mounted = player.GetMounted();
            if (mounted != null && mounted.prefabID == _prefabId && mounted.parentEntity.uid == 0 && mounted.skinID == PreventMovementSkinId)
            {
                player.DismountObject();
                mounted.Kill();
            }
        }
    }
}