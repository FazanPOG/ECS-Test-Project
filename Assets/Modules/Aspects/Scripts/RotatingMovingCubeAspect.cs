using Modules.Components.Scripts;
using Unity.Entities;
using Unity.Transforms;

namespace Modules.Aspects.Scripts
{
    public readonly partial struct RotatingMovingCubeAspect : IAspect
    {
        public readonly RefRW<LocalTransform> localTransform;
        public readonly RefRO<RotateSpeed> rotateSpeed;
        public readonly RefRO<Movement> movement;

        public void MoveAndRotate(float deltaTime)
        {
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * deltaTime);
            localTransform.ValueRW = localTransform.ValueRO.Translate(movement.ValueRO.movementVector * deltaTime);
        }
    }
}
