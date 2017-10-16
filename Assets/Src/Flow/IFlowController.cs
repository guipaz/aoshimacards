using System;

public interface IFlowController
{
    void Setup();
    void Start();
    void HandleInput();
    void Update();
}

public enum FlowState
{
    AddingWarriors,

    ChooseActorToPerform,
    PerformMovementLocation,
    PerformAttackLocation,

    FinishPlayerTurn,

    EnemyTurn,
    GameFinished
}