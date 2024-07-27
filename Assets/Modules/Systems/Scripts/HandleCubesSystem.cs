using Modules.Aspects.Scripts;
using Modules.Components.Scripts;
using Unity.Entities;
using Unity.Transforms;

namespace Modules.Systems.Scripts
{
    public partial struct HandleCubesSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (RotatingMovingCubeAspect cubeAspect 
                in SystemAPI.Query<RotatingMovingCubeAspect>().WithAll<RotatingCube>().WithAll<RotatingCube>())
            {
                cubeAspect.MoveAndRotate(SystemAPI.Time.DeltaTime);
            }
        }
    }
}
