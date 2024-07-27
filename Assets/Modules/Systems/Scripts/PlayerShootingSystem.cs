using System;
using Modules.Components.Scripts;
using Modules.Configs.Scripts;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Modules.Systems.Scripts
{
    public partial class PlayerShootingSystem : SystemBase
    {
        public event EventHandler OnShoot;
        
        protected override void OnCreate()
        {
            RequireForUpdate<Player>();
        }

        protected override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
                EntityManager.SetComponentEnabled<Stunned>(playerEntity, true);
            }
            
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
                EntityManager.SetComponentEnabled<Stunned>(playerEntity, false);
            }
            
            if(Input.GetKeyDown(KeyCode.Space) == false)
                return;

            SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();
            
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
            
            foreach ((RefRO<LocalTransform> localTransform, Entity entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<Stunned>().WithEntityAccess())
            {
                Entity entityInstance = entityCommandBuffer.Instantiate(spawnCubesConfig.cubePrefabEntity);
                entityCommandBuffer.SetComponent(entityInstance, new LocalTransform
                {
                    Position = localTransform.ValueRO.Position,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
                
                OnShoot?.Invoke(entity, EventArgs.Empty);
            }
            
            entityCommandBuffer.Playback(EntityManager);
        }
    }
}
