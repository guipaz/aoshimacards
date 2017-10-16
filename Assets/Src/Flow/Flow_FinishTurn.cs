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
        GameData.CurrentBattle.RegisterUsedActor(actor);

        if (GameData.CurrentBattle.IsRoundOver())
        {
            GameData.CurrentBattle.FinishRound();
        }

        BattleSceneController.Main.PerformMenu.Deactivate();
        BattleSceneController.Main.SelectedActor = null;
        BattleSceneController.Main.PerformedMovement = false;
        BattleSceneController.Main.PerformedAttack = false;
        BattleSceneController.Main.PerformedTurn = false;

        GameData.CurrentBattle.CurrentPlayer = GameData.CurrentBattle.CurrentPlayer.GetEnemy();
        BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
    }

    public void Update()
    {

    }

    public void HandleInput()
    {

    }
}
