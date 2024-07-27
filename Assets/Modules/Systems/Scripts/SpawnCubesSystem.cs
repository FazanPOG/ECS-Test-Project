using Modules.Configs.Scripts;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace Modules.Systems.Scripts
{
    public partial class SpawnCubesSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<SpawnCubesConfig>();
        }

        protected override void OnUpdate()
        {
            this.Enabled = false;

            SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();

            for (int i = 0; i < spawnCubesConfig.amountToSpawn; i++)
            {
                Entity entity = EntityManager.Instantiate(spawnCubesConfig.cubePrefabEntity);
                EntityManager.SetComponentData(entity, new LocalTransform
                {
                    Position = new float3(Random.Range(-10f, +5f), 0.6f, Random.Range(-4f, +7f)),
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
            }
        }
    }
}
