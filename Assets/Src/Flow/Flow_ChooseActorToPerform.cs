using System;
using UnityEngine;

public class Flow_ChooseActorToPerform : IFlowController
{
    public void Setup()
    {
        BattleSceneController.Main.PerformMenu.OnMove += Move;
        BattleSceneController.Main.PerformMenu.OnAttack += Attack;
        BattleSceneController.Main.PerformMenu.OnTurn += TurnAround;
    }

    public void Start()
    {
        if ((BattleSceneController.Main.PerformedMovement &&
             BattleSceneController.Main.PerformedAttack) ||
            GameData.CurrentBattle.CurrentPlayer.AvailableActors.Count == GameData.CurrentBattle.CurrentPlayer.UsedActors.Count)
        {
            BattleSceneController.Main.SwitchFlow(FlowState.FinishPlayerTurn);
            return;
        }

        if (BattleSceneController.Main.PerformedMovement || BattleSceneController.Main.PerformedAttack)
            BattleSceneController.Main.HeadsupText.text = GameData.CurrentBattle.CurrentPlayer.Name + "'s turn (Space to go to active warrior)";
        else
            BattleSceneController.Main.HeadsupText.text = GameData.CurrentBattle.CurrentPlayer.Name + "'s turn (choose a warrior to perform)";
    }

    void Choose(Vector2 position)
    {
        if (BoardUtils.IsInsideBoard(position))
        {
            BattleActor act = GameData.CurrentBattle.Board.GetActorAt(position);
            if (act != null && act.Owner == GameData.CurrentBattle.CurrentPlayer && !GameData.CurrentBattle.IsActorUsed(act))
            {
                if (act == BattleSceneController.Main.SelectedActor ||
                    (!BattleSceneController.Main.PerformedMovement && !BattleSceneController.Main.PerformedAttack))
                {
                    BattleSceneController.Main.PerformMenu.Activate(act, !BattleSceneController.Main.PerformedMovement && !BattleSceneController.Main.PerformedTurn,
                                                                         !BattleSceneController.Main.PerformedAttack  && !BattleSceneController.Main.PerformedTurn,
                                                                         !BattleSceneController.Main.PerformedAttack && !BattleSceneController.Main.PerformedMovement &&
                                                                         !BattleSceneController.Main.PerformedTurn);
                }
            }
        }
    }

    void Move(BattleActor actor)
    {
        BattleSceneController.Main.PerformMenu.Deactivate();
        BattleSceneController.Main.SelectedActor = actor;

        BattleSceneController.Main.SwitchFlow(FlowState.PerformMovementLocation);
    }

    void Attack(BattleActor actor)
    {
        BattleSceneController.Main.PerformMenu.Deactivate();
        BattleSceneController.Main.SelectedActor = actor;

        BattleSceneController.Main.SwitchFlow(FlowState.PerformAttackLocation);
    }

    void TurnAround(BattleActor actor)
    {
        BattleSceneController.Main.PerformMenu.Deactivate();
        BattleSceneController.Main.SelectedActor = actor;
        BattleSceneController.Main.PerformedTurn = true;

        actor.SetInverted(!actor.Inverted);
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Choose(BoardUtils.ScreenToBoardPosition(Input.mousePosition));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (BattleSceneController.Main.SelectedActor != null && (BattleSceneController.Main.PerformedAttack || BattleSceneController.Main.PerformedMovement))
            {
                Choose(BattleSceneController.Main.SelectedActor.Position);
            }
        }
    }

    public void Update()
    {
        
    }
}