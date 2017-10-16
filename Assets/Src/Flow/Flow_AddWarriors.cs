using System;
using UnityEngine;

public class Flow_AddWarriors : IFlowController
{
    int addWarriors_index;
    int addedPlayers = 0;

    public void Setup()
    {
    }

    public void Start()
    {
        BattleSceneController.Main.HeadsupText.gameObject.SetActive(true);
        addWarriors_index = -1;
        Next();
    }

    void Next()
    {
        bool adding = false;
        while (addWarriors_index + 1 < GameData.CurrentBattle.CurrentPlayer.AvailableActors.Count)
        {
            addWarriors_index++;
            adding = true;
            BattleObject obj = GameData.CurrentBattle.CurrentPlayer.AvailableActors[addWarriors_index];
            if (!(obj is BattleActor))
                continue;

            BattleActor actor = obj as BattleActor;
            BattleSceneController.Main.HeadsupText.text = "Place " + GameData.CurrentBattle.CurrentPlayer.Name + "'s " + actor.Type.Name;
            break;
        }

        if (!adding && addWarriors_index + 1 >= GameData.CurrentBattle.CurrentPlayer.AvailableActors.Count)
        {
            addedPlayers++;
            if (addedPlayers == 2)
            {
                BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
            }
            else
            {
                GameData.CurrentBattle.CurrentPlayer = GameData.CurrentBattle.CurrentPlayer.GetEnemy();
                BattleSceneController.Main.SwitchFlow(FlowState.AddingWarriors);
            }
        }
    }

    void Place(Vector2 at, BattleActor actor)
    {
        if (!BoardUtils.IsInsideBoard(at) || !BoardUtils.IsPositionEmpty(at))
            return;
        
        if ((GameData.CurrentBattle.CurrentPlayer == GameData.CurrentBattle.PlayerA &&
            at.x >= (GameData.CurrentBattle.Board.Width - GameData.CurrentBattle.Board.NeutralSize) / 2) ||
            (GameData.CurrentBattle.CurrentPlayer == GameData.CurrentBattle.PlayerB &&
            at.x < (GameData.CurrentBattle.Board.Width - GameData.CurrentBattle.Board.NeutralSize) / 2 + GameData.CurrentBattle.Board.NeutralSize))
            return;

        actor.SetPosition(at);
        actor.GameObject.SetActive(true);

        Next();
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Place(BoardUtils.ScreenToBoardPosition(Input.mousePosition), GameData.CurrentBattle.CurrentPlayer.AvailableActors[addWarriors_index] as BattleActor);
        }
    }

    public void Update()
    {

    }
}

