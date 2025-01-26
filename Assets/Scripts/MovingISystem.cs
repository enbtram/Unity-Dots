using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;

[BurstCompile]
public partial struct MovingISystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        
        float deltaTime = SystemAPI.Time.DeltaTime;

        JobHandle jobHandle = new MoveJob{
            deltaTime = deltaTime
        }.ScheduleParallel(state.Dependency);

        jobHandle.Complete();

        new TestReachedTargetPositionJob{
            randomComponent = randomComponent
        }.Run();
    }
}

[BurstCompile]
public partial struct MoveJob : IJobEntity{

    public float deltaTime;
    [BurstCompile]
    public void Execute(MoveToPositionAspect moveToPositionAspect){
        moveToPositionAspect.Move(deltaTime);
    }
}

[BurstCompile]

public partial struct TestReachedTargetPositionJob : IJobEntity{
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;
    [BurstCompile]
    public void Execute(MoveToPositionAspect moveToPositionAspect){
        moveToPositionAspect.TestReachedTargetPosition(randomComponent);
    }
}