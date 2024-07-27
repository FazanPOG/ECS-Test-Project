using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Components.Scripts
{
    public class MovementAuthoring : MonoBehaviour
    {
        public float3 movementVector;
        
        public class Baker : Baker<MovementAuthoring>
        {
            public override void Bake(MovementAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Movement
                {
                    movementVector = new float3(Random.Range(-1f, +1f), 0, Random.Range(-1f, +1f))
                });
            }
        }
    }

    public struct Movement : IComponentData
    {
        public float3 movementVector;
    }
}
