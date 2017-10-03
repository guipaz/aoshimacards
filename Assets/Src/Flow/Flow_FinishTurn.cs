using System;

public class Flow_FinishTurn : IFlowController
{
    public void Setup()
    {
        BattleSceneController.Main.PerformMenu.OnFinish = Finish;
    }

    public void Start()
    {
        Finish(BattleSceneController.Main.SelectedActor);
    }

    void Finish(BattleActor actor)
    {
        BattleSceneController.Main.PerformMenu.Deactivate();
        BattleSceneController.Main.SelectedActor = null;
        BattleSceneController.Main.PerformedMovement = false;
        BattleSceneController.Main.PerformedAttack = false;

        BattleSceneController.Main.SwitchFlow(FlowState.EnemyTurn);
    }

    public void Update()
    {

    }

    public void HandleInput()
    {

    }
}
