using System;
using UnityEngine;

public class Flow_PerformMovement : IFlowController
{
    GameObject currentPattern;

    public void Setup()
    {
    }

    public void Start()
    {
        BattleSceneController.Main.HeadsupText.text = "Choose the movement location (ESC to cancel)";
        currentPattern = BattleSceneController.Main.SelectedActor.Type.Pattern.GetVisualObject(PatternFlags.Movement, BattleSceneController.Main.SelectedActor.Position,
                                                                                               BattleSceneController.Main.SelectedActor.Inverted);
    }

    void Choose(Vector2 position)
    {
        if (BoardUtils.IsInsideBoard(position) &&
            BattleSceneController.Main.SelectedActor.CanMoveTo(position) &&
            BoardUtils.IsPositionEmpty(position))
        {
            BattleSceneController.Main.SelectedActor.SetPosition(position);
            BattleSceneController.Main.PerformedMovement = true;
            GameObject.Destroy(currentPattern);
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