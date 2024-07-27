using Modules.Components.Scripts;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Modules.Systems.Scripts
{
    public partial struct RotatingCubeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotateSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            return;
            
            /*
            foreach ((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed) 
                in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
            {
                localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
            }
            */

            RotatingCubeJob rotatingCubeJob = new RotatingCubeJob()
            {
                deltaTime = SystemAPI.Time.DeltaTime
            };

            rotatingCubeJob.ScheduleParallel();
        }
    }

    [BurstCompile]
    [WithAll(typeof(RotatingCube))]
    public partial struct RotatingCubeJob : IJobEntity
    {
        public float deltaTime;
        
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            localTransform = localTransform.RotateY(rotateSpeed.value * deltaTime);
        }
    }
}
