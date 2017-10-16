using System;
using UnityEngine;

public class Flow_PerformAttack : IFlowController
{
    GameObject currentPattern;

    public void Setup()
    {
    }

    public void Start()
    {
        BattleSceneController.Main.HeadsupText.text = "Choose the attack location (ESC to cancel)";
        currentPattern = BattleSceneController.Main.SelectedActor.Type.Pattern.GetVisualObject(PatternFlags.Attack, BattleSceneController.Main.SelectedActor.Position,
                                                                                               BattleSceneController.Main.SelectedActor.Inverted);
    }

    void Choose(Vector2 position)
    {
        if (BoardUtils.IsInsideBoard(position) &&
            BattleSceneController.Main.SelectedActor.CanAttackAt(position))
        {
            BattleObject obj = GameData.CurrentBattle.Board.GetObjectAt(position);
            obj.TakeDamage(BattleSceneController.Main.SelectedActor.Type.Attack);

            BattleSceneController.Main.PerformedAttack = true;
            GameObject.Destroy(currentPattern);

            if (obj.Properties.ContainsKey("isOrb") && obj.Properties["isOrb"] == "true")
                GameData.CurrentBattle.EndGameWithWinner(obj.Owner.GetEnemy());
            else
                BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
        }
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Choose(BoardUtils.ScreenToBoardPosition(Input.mousePosition));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Destroy(currentPattern);
            BattleSceneController.Main.PerformMenu.Deactivate();
            BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
        }
    }

    public void Update()
    {

    }
}