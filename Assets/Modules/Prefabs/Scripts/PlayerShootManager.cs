using System;
using Modules.Systems.Scripts;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Modules.Prefabs.Scripts
{
    public class PlayerShootManager : MonoBehaviour
    {
        [SerializeField] private ShootPopup prefab;
        
        private void Start()
        {
            PlayerShootingSystem playerShootingSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();
            playerShootingSystem.OnShoot += PlayerShootingSystemOnOnShoot;
        }

        private void PlayerShootingSystemOnOnShoot(object sender, EventArgs e)
        {
            Entity playerEntity = (Entity) sender;
            LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
            Instantiate(prefab, localTransform.Position, Quaternion.identity);
        }
    }
}
